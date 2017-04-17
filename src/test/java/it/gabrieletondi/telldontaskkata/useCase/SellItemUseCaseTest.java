package it.gabrieletondi.telldontaskkata.useCase;

import it.gabrieletondi.telldontaskkata.doubles.InMemoryProductCatalog;
import it.gabrieletondi.telldontaskkata.doubles.TestOrderRepository;
import it.gabrieletondi.telldontaskkata.domain.Order;
import it.gabrieletondi.telldontaskkata.domain.Product;
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog;
import org.junit.Test;

import java.math.BigDecimal;
import java.util.Arrays;

import static org.hamcrest.Matchers.hasSize;
import static org.hamcrest.Matchers.is;
import static org.junit.Assert.assertThat;

public class SellItemUseCaseTest {
    private final TestOrderRepository orderRepository = new TestOrderRepository();
    private final ProductCatalog productCatalog = new InMemoryProductCatalog(
            Arrays.<Product>asList(
                    new Product() {{ setName("salad"); setPrice(new BigDecimal("3.56")); }})
    );
    private final SellItemUseCase useCase = new SellItemUseCase(orderRepository, productCatalog);

    @Test
    public void sellOneItemWithMultipleQuantity() throws Exception {
        final SellItemRequest request = new SellItemRequest();
        request.setProductName("salad");
        request.setQuantity(2);

        useCase.run(request);

        final Order insertedOrder = orderRepository.insertedOrder();
        assertThat(insertedOrder.getTotal(), is(new BigDecimal("7.12")));
        assertThat(insertedOrder.getCurrency(), is("EUR"));
        assertThat(insertedOrder.getItems(), hasSize(1));
        assertThat(insertedOrder.getItems().get(0).getProduct().getName(), is("salad"));
        assertThat(insertedOrder.getItems().get(0).getProduct().getPrice(), is(new BigDecimal("3.56")));
        assertThat(insertedOrder.getItems().get(0).getQuantity(), is(2));
    }
}
