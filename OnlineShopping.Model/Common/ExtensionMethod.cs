using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace OnlineShopping.Model.Common
{
    public static class ExtensionMethod
    {
        public static PagingModel<T> ToPaging<T>(this IEnumerable<T> list, int page, int size)
        {
            var totalItem = list.Count();
            var result = new PagingModel<T>
            {
                Size = size,
                Page = page,
                TotalPage = totalItem % size == 0 ? totalItem / size : totalItem / size + 1
            };

            if (result.Page > result.TotalPage)
            {
                throw new CustomException(HttpStatusCode.BadRequest, $"Page can't greater than {result.TotalPage}");
            }

            result.Items = list.Skip((page - 1) * size).Take(size).ToList();

            return result;
        }
    }
}
