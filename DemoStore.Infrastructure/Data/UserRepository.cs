using DemoStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DemoStore.Core.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DemoStore.Infrastructure.Data
{
    public class UserRepository: IUserRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> CountAsync(ISpecification<ApplicationUser> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ApplicationUser>> ListAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IReadOnlyList<ApplicationUser>> ListAsync(ISpecification<ApplicationUser> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<ApplicationUser> ApplySpecification(ISpecification<ApplicationUser> spec)
        {
            return SpecificationEvaluator<ApplicationUser>.GetQuery(_dbContext.Users.AsQueryable(), spec);
        }
    }
}
