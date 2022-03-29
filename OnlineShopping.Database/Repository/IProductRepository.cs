using OnlineShopping.Database.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopping.Database.Repository
{
    public interface IProductRepository : IReponsitory<Product>
    {
        Task<List<Product>> SearchProductByCondition(string term, List<string> branchs);
    }
}
