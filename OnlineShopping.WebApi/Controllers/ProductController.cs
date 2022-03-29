using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Model.Model;
using OnlineShopping.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("api/product/branch")]
        public async Task<IActionResult> GetBranchs()
        {
            var result = await _productService.GetBranchs();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/product/search")]
        public async Task<IActionResult> SearchProduct(SearchProductByCondition request)
        {
            var result = await _productService.SearchProduct(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/product/{id}")]
        public async Task<IActionResult> GetProductDetail(long id)
        {
            var result = await _productService.GetProductById(id);
            return Ok(result);
        }
    }
}
