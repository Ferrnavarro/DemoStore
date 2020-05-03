using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoStore.API.Dtos
{
    public class ProductCatalogDto
    {
        public IReadOnlyList<ProductDto> Products { get; set; }

        public PaginationInfoDto PaginationInfo { get; set; }

    }
}
