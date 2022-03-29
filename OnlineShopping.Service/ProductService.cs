using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Database.Entity;
using OnlineShopping.Database.Repository;
using OnlineShopping.Model.Common;
using OnlineShopping.Model.Model;
using OnlineShopping.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShopping.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetBranchs()
        {
            return await _productRepository.FindAll().Select(x => x.Branch).Distinct().ToListAsync();
        }

        public async Task<PagingModel<ProductModel>> SearchProduct(SearchProductByCondition request)
        {
            var branchs = !string.IsNullOrWhiteSpace(request.Branchs) ? request.Branchs.Split(",").ToList() : null;
            var query = await _productRepository.SearchProductByCondition(request.Term, branchs);

            var result = _mapper.Map<List<ProductModel>>(query);

            result = OrderBy(result, request.OrderBy);

            return result.ToPaging(request.Page, request.Size);
        }

        public async Task<ProductModel> GetProductById(long id)
        {
            var product = await _productRepository.FindByIdAsync(id);

            return _mapper.Map<ProductModel>(product);
        }

        private List<ProductModel> OrderBy(IList<ProductModel> products, ProductOrderBy orderBy)
        {
            switch (orderBy)
            {
                case ProductOrderBy.PriceAsc:
                    return products.OrderBy(x => x.Price).ToList();
                case ProductOrderBy.PriceDesc:
                    return products.OrderByDescending(x => x.Price).ToList();
                default:
                    return products.OrderBy(x => x.Price).ToList();
            }
        }
    }
}
