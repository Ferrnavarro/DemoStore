using DemoStore.Core.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoStore.Core.Specifications
{
    public class UserFilterPaginatedSpecification: BaseSpecification<ApplicationUser>
    {
        public UserFilterPaginatedSpecification(int skip, int take, string search = "")
            : base(p =>
            (p.UserName.ToLower().Contains(search.Trim().ToLower()) ||
            (p.Name.ToLower().Contains(search.Trim().ToLower()))))
        {
            ApplyPaging(skip, take);
        }
    }
}
