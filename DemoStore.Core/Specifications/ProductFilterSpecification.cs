using DemoStore.Core.Entities.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoStore.Core.Specifications
{
    public class ProductFilterSpecification: BaseSpecification<Product>
    {
        public ProductFilterSpecification(string orderBy, string search = "")
            : base(p =>
            (p.Name.ToLower().Contains(search.Trim().ToLower()) ||
            (p.Sku.ToLower().Contains(search.Trim().ToLower()))))
        {
            switch (orderBy)
            {
                default:
                    ApplyOrderBy(o => o.Name);
                    break;
            }
        }
    }
}
