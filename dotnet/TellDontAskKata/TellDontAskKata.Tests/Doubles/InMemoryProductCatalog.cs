using System;
using System.Collections.Generic;

namespace TellDontAskKata.Tests.Doubles
{
    using System.Linq;

    using TellDontAskKata.Domain;
    using TellDontAskKata.Repository;
    public class InMemoryProductCatalog : IProductCatalog
    {
        private readonly IEnumerable<Product> products;

        public InMemoryProductCatalog(IEnumerable<Product> products)
        {
            this.products = products;
        }

        public Product GetByName(string name)
        {
            return this.products.SingleOrDefault(item => item.Name.Equals(name));
        }
    }
}
