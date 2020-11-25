using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.Tests.Doubles
{
    public class InMemoryProductCatalog : ProductCatalog
    {
        private readonly List<Product> products;

        public InMemoryProductCatalog(List<Product> products)
        {
            this.products = products;
        }

        public Product getByName(String name)
        {
            return products.FirstOrDefault(p => p.getName().Equals(name));
        }
    }
}
