using Microsoft.EntityFrameworkCore;
using OnlineShopping.Database.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Database.Repository
{
    public class ProductRepository : OnlineShoppingRepository<Product>, IProductRepository
    {
        public ProductRepository(OnlineShoppingContext onlineShoppingContext) : base (onlineShoppingContext)
        { 
        }

        public async Task<List<Product>> SearchProductByCondition(string term, List<string> branchs)
        {
            var query = OnlineShoppingContext.Set<Product>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                query = query.Where(x => x.Name.Contains(term));
            }
            if (branchs != null && branchs.Any())
            {
                query = query.Where(x => x.Branch.Contains(x.Branch));
            }

            return await query.ToListAsync();
        }
    }
}
