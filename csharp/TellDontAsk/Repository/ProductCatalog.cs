using System;
using TellDontAsk.Domain;

namespace TellDontAsk.Repository
{
    public interface ProductCatalog
    {
        Product getByName(String name);
    }
}
