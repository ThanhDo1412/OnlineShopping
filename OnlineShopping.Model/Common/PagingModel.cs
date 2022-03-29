using System.Collections.Generic;
using System.Linq;

namespace OnlineShopping.Model.Common
{
    public class PagingModel<T>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
