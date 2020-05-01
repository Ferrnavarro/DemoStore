using DemoStore.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoStore.Core.Entities.UserAggregate
{
    public class ApplicationUser: IdentityUser, IAggregateRoot
    {

    }
}
