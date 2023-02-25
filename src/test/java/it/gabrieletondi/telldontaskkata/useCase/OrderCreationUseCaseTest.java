package it.gabrieletondi.telldontaskkata.useCase;

import it.gabrieletondi.telldontaskkata.domain.Category;
import it.gabrieletondi.telldontaskkata.domain.Order;
import it.gabrieletondi.telldontaskkata.domain.OrderStatus;
import it.gabrieletondi.telldontaskkata.domain.Product;
import it.gabrieletondi.telldontaskkata.doubles.InMemoryProductCatalog;
import it.gabrieletondi.telldontaskkata.doubles.TestOrderRepository;
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Arrays;

import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

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
        assertThat(insertedOrder.getStatus()).isEqualTo(OrderStatus.CREATED);
        assertThat(insertedOrder.getTotal()).isEqualTo(new BigDecimal("23.20"));
        assertThat(insertedOrder.getTax()).isEqualTo(new BigDecimal("2.13"));
        assertThat(insertedOrder.getCurrency()).isEqualTo("EUR");
        assertThat(insertedOrder.getItems()).hasSize(2);
        assertThat(insertedOrder.getItems().get(0).getProduct().getName()).isEqualTo("salad");
        assertThat(insertedOrder.getItems().get(0).getProduct().getPrice()).isEqualTo(new BigDecimal("3.56"));
        assertThat(insertedOrder.getItems().get(0).getQuantity()).isEqualTo(2);
        assertThat(insertedOrder.getItems().get(0).getTaxedAmount()).isEqualTo(new BigDecimal("7.84"));
        assertThat(insertedOrder.getItems().get(0).getTax()).isEqualTo(new BigDecimal("0.72"));
        assertThat(insertedOrder.getItems().get(1).getProduct().getName()).isEqualTo("tomato");
        assertThat(insertedOrder.getItems().get(1).getProduct().getPrice()).isEqualTo(new BigDecimal("4.65"));
        assertThat(insertedOrder.getItems().get(1).getQuantity()).isEqualTo(3);
        assertThat(insertedOrder.getItems().get(1).getTaxedAmount()).isEqualTo(new BigDecimal("15.36"));
        assertThat(insertedOrder.getItems().get(1).getTax()).isEqualTo(new BigDecimal("1.41"));
    }

    @Test
    public void unknownProduct() throws Exception {
        SellItemsRequest request = new SellItemsRequest();
        request.setRequests(new ArrayList<>());
        SellItemRequest unknownProductRequest = new SellItemRequest();
        unknownProductRequest.setProductName("unknown product");
        request.getRequests().add(unknownProductRequest);

        assertThatThrownBy(() -> useCase.run(request)).isExactlyInstanceOf(UnknownProductException.class);
    }
}
