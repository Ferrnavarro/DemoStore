using DemoStore.Core.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoStore.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<ApplicationUser>> ListAllAsync();
        Task<IReadOnlyList<ApplicationUser>> ListAsync(ISpecification<ApplicationUser> spec);
        Task<int> CountAsync(ISpecification<ApplicationUser> spec);
    }
}
