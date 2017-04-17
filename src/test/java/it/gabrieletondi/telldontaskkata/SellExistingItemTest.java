package it.gabrieletondi.telldontaskkata;

import org.junit.Test;

import java.math.BigDecimal;
import java.util.Arrays;

import static org.hamcrest.Matchers.is;
import static org.junit.Assert.assertThat;

public class SellExistingItemTest {
    private final TestOrderRepository orderRepository = new TestOrderRepository();
    private final ProductCatalog productCatalog = new InMemoryProductCatalog(
            Arrays.<Product>asList(
                    new Product() {{ setName("salad"); setPrice(new BigDecimal("3.56")); }})
    );
    private final SellItemUseCase useCase = new SellItemUseCase(orderRepository, productCatalog);

    @Test
    public void sellOneItem() throws Exception {
        final SellItemRequest request = new SellItemRequest();
        request.setProductName("salad");
        request.setQuantity(2);

        useCase.run(request);

        final Order insertedOrder = orderRepository.insertedOrder();
        assertThat(insertedOrder.getTotal(), is(new BigDecimal("7.12")));
        assertThat(insertedOrder.getCurrency(), is("EUR"));
    }
}
