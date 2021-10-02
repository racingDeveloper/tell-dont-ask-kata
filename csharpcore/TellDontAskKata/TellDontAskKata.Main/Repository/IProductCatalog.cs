using TellDontAskKata.Main.Domain;

namespace TellDontAskKata.Main.Repository
{
    public interface IProductCatalog
    {
        Product GetByName(string name);
    }
}
