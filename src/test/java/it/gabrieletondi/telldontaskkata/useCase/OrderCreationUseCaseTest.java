package it.gabrieletondi.telldontaskkata.useCase;

import it.gabrieletondi.telldontaskkata.domain.Category;
import it.gabrieletondi.telldontaskkata.domain.Order;
import it.gabrieletondi.telldontaskkata.domain.OrderStatus;
import it.gabrieletondi.telldontaskkata.domain.Product;
import it.gabrieletondi.telldontaskkata.doubles.InMemoryProductCatalog;
import it.gabrieletondi.telldontaskkata.doubles.TestOrderRepository;
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog;
import org.junit.Test;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;

import static org.hamcrest.Matchers.hasSize;
import static org.hamcrest.Matchers.is;
import static org.junit.Assert.assertThat;

public class OrderCreationUseCaseTest {
    private final TestOrderRepository orderRepository = new TestOrderRepository();
    private Category food = new Category() {{
        setName("food");
        setTaxPercentage(new BigDecimal("10"));
    }};;
    private final ProductCatalog productCatalog = new InMemoryProductCatalog(
            Arrays.<Product>asList(
                    new Product() {{
                        setName("salad");
                        setPrice(new BigDecimal("3.56"));
                        setCategory(food);
                    }},
                    new Product() {{
                        setName("tomato");
                        setPrice(new BigDecimal("4.65"));
                        setCategory(food);
                    }}
            )
    );
    private final OrderCreationUseCase useCase = new OrderCreationUseCase(orderRepository, productCatalog);

    @Test
    public void sellMultipleItems() throws Exception {
        SellItemRequest saladRequest = new SellItemRequest();
        saladRequest.setProductName("salad");
        saladRequest.setQuantity(2);

        SellItemRequest tomatoRequest = new SellItemRequest();
        tomatoRequest.setProductName("tomato");
        tomatoRequest.setQuantity(3);

        final SellItemsRequest request = new SellItemsRequest();
        request.setRequests(new ArrayList<>());
        request.getRequests().add(saladRequest);
        request.getRequests().add(tomatoRequest);

        useCase.run(request);

        final Order insertedOrder = orderRepository.getSavedOrder();
        assertThat(insertedOrder.getStatus(), is(OrderStatus.CREATED));
        assertThat(insertedOrder.getTotal(), is(new BigDecimal("23.20")));
        assertThat(insertedOrder.getTax(), is(new BigDecimal("2.13")));
        assertThat(insertedOrder.getCurrency(), is("EUR"));
        assertThat(insertedOrder.getItems(), hasSize(2));
        assertThat(insertedOrder.getItems().get(0).getProduct().getName(), is("salad"));
        assertThat(insertedOrder.getItems().get(0).getProduct().getPrice(), is(new BigDecimal("3.56")));
        assertThat(insertedOrder.getItems().get(0).getQuantity(), is(2));
        assertThat(insertedOrder.getItems().get(0).getTaxedAmount(), is(new BigDecimal("7.84")));
        assertThat(insertedOrder.getItems().get(0).getTax(), is(new BigDecimal("0.72")));
        assertThat(insertedOrder.getItems().get(1).getProduct().getName(), is("tomato"));
        assertThat(insertedOrder.getItems().get(1).getProduct().getPrice(), is(new BigDecimal("4.65")));
        assertThat(insertedOrder.getItems().get(1).getQuantity(), is(3));
        assertThat(insertedOrder.getItems().get(1).getTaxedAmount(), is(new BigDecimal("15.36")));
        assertThat(insertedOrder.getItems().get(1).getTax(), is(new BigDecimal("1.41")));
    }

    @Test(expected = UnknownProductException.class)
    public void unknownProduct() throws Exception {
        SellItemsRequest request = new SellItemsRequest();
        request.setRequests(new ArrayList<>());
        SellItemRequest unknownProductRequest = new SellItemRequest();
        unknownProductRequest.setProductName("unknown product");
        request.getRequests().add(unknownProductRequest);

        useCase.run(request);
    }
}
