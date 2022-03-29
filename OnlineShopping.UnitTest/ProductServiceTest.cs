using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OnlineShopping.Database.Entity;
using OnlineShopping.Database.Repository;
using OnlineShopping.Model.Common;
using OnlineShopping.Model.Model;
using OnlineShopping.Service;
using OnlineShopping.Service.Mapper;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineShopping.UnitTest
{
    [TestFixture]
    public class ProductServiceTest
    {
        private Mock<IProductRepository> _productRepoMock =  new Mock<IProductRepository>();
        private ProductService _productService;

        [Test]
        public async Task SearchProduct_WithCorrectPaging_ReturnListOfProduct()
        {
            //Arr
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMapper());
            });
            var mapper = mockMapper.CreateMapper();

            _productRepoMock.Setup(x => x.SearchProductByCondition(It.IsAny<string>(), It.IsAny<List<string>>())).ReturnsAsync(new List<Product> ()
            {
                new Product
                {
                    Id = 1,
                    Branch = "Apple",
                    Color = "Blue",
                    Name = "Iphone13",
                    Price = 1200,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 2,
                    Branch = "Apple",
                    Color = "Gray",
                    Name = "Iphone13 Pro",
                    Price = 1500,
                    CreatedDate = DateTime.Now
                }
            });

            _productService = new ProductService(_productRepoMock.Object, mapper);

            //Act
            var result = await _productService.SearchProduct(new SearchProductByCondition { 
                Page = 1,
                Size = 5,
                OrderBy = ProductOrderBy.PriceAsc
            });

            //Assert
            result.ShouldBeOfType<PagingModel<ProductModel>>();
            result.Page.ShouldBe(1);
            result.TotalPage.ShouldBe(1);
            result.Size.ShouldBe(5);
            result.Items.Count().ShouldBe(2);
            result.Items.First().Id.ShouldBe(1);
            result.Items.First().Branch.ShouldBe("Apple");
            result.Items.First().Color.ShouldBe("Blue");
            result.Items.First().Name.ShouldBe("Iphone13");
            result.Items.First().Price.ShouldBe(1200);
            result.Items.Last().Id.ShouldBe(2);
            result.Items.Last().Branch.ShouldBe("Apple");
            result.Items.Last().Color.ShouldBe("Gray");
            result.Items.Last().Name.ShouldBe("Iphone13 Pro");
            result.Items.Last().Price.ShouldBe(1500);
        }

        [Test]
        public async Task SearchProduct_WithInCorrectPaging_ReturnListOfProduct()
        {
            //Arr
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMapper());
            });
            var mapper = mockMapper.CreateMapper();

            _productRepoMock.Setup(x => x.SearchProductByCondition(It.IsAny<string>(), It.IsAny<List<string>>())).ReturnsAsync(new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Branch = "Apple",
                    Color = "Blue",
                    Name = "Iphone13",
                    Price = 1200,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 2,
                    Branch = "Apple",
                    Color = "Gray",
                    Name = "Iphone13 Pro",
                    Price = 1500,
                    CreatedDate = DateTime.Now
                }
            });

            _productService = new ProductService(_productRepoMock.Object, mapper);

            //Act
            var result = await Should.ThrowAsync<CustomException>(() => _productService.SearchProduct(new SearchProductByCondition
            {
                Page = 2,
                Size = 5,
                OrderBy = ProductOrderBy.PriceAsc
            }));

            //Assert
            result.ErrorCode.ShouldBe(HttpStatusCode.BadRequest);
            result.Message.ShouldBe("Page can't greater than 1");
        }
    }
}