using DemoStore.Core.Entities.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoStore.Core.Specifications
{
    public class ProductFilterPaginatedSpecification: BaseSpecification<Product>
    {
        public ProductFilterPaginatedSpecification(int skip, int take, string orderBy, string search = "")
            : base(p => 
            (p.Name.ToLower().Contains(search.Trim().ToLower()) || 
            (p.Sku.ToLower().Contains(search.Trim().ToLower()))))
        {
            ApplyPaging(skip, take);

            switch (orderBy)
            {
                default:
                    ApplyOrderBy(o => o.Name);
                    break;
            }
        }
    }
}
