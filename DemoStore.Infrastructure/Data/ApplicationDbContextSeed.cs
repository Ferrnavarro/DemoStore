using DemoStore.Core.Entities.ProductAggregate;
using DemoStore.Core.Entities.UserAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoStore.Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(GetPreconfiguredProducts());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "Nintendo Switch",
                    Description = "Get the gaming system that lets you play the games you want, wherever you are, however you like. Includes the Nintendo Switch console and Nintendo Switch dock in black",
                    NumberAvailable = 7,
                    Price = 350,
                    Sku = "NIN-SW-BLACK-82"
                },

                new Product()
                {
                    Name = "PlayStation 4",
                    Description = "The official home of PlayStation 4 - the world's best-selling console. Available in Jet Black, Glacier White and limited edition Gold and Silver.",
                    NumberAvailable = 7,
                    Price = 450,
                    Sku = "PLAY-ST-BLACK-82"
                },

                new Product()
                {
                    Name = "Xbox One",
                    Description = "Take your gaming skills to a whole new level with Xbox gaming consoles and accessories",
                    NumberAvailable = 7,
                    Price = 400,
                    Sku = "XB-ONE-WHITE-82"
                },

                new Product()
                {
                    Name = "Nintendo Switch",
                    Description = "Get the gaming system that lets you play the games you want, wherever you are, however you like. Includes the Nintendo Switch console and Nintendo Switch dock in black",
                    NumberAvailable = 7,
                    Price = 350,
                    Sku = "NIN-SW-BLACK-82"
                }
                
            };
        }

        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "demouser",
                Email = "demouser@demo.com",
                Name = "Demo User",
                BirthDate = DateTime.Today,
            };

            await userManager.CreateAsync(defaultUser, "P@ssword123");           
           
        }
    }
}
