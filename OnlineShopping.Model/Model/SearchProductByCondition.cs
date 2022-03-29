using System.Collections.Generic;

namespace OnlineShopping.Model.Model
{
    public class SearchProductByCondition
    {
        public string Term { get; set; }
        public string Branchs { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public ProductOrderBy OrderBy { get; set; }
    }

    public enum ProductOrderBy
    {
        PriceAsc,
        PriceDesc
    }
}
