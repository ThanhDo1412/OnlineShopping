using OnlineShopping.Model.Common;
using OnlineShopping.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopping.Service.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<string>> GetBranchs();
        Task<PagingModel<ProductModel>> SearchProduct(SearchProductByCondition request);
        Task<ProductModel> GetProductById(long id);
    }
}
