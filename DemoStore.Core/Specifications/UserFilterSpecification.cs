using DemoStore.Core.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoStore.Core.Specifications
{
    public class UserFilterSpecification: BaseSpecification<ApplicationUser>
    {
        public UserFilterSpecification(string search = "")
            : base(p =>
            (p.UserName.ToLower().Contains(search.Trim().ToLower()) ||
            (p.Name.ToLower().Contains(search.Trim().ToLower()))))
        {
            
        }
    }
}
