using QualitySouvenirs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenirs.Share;

namespace QualitySouvenirs.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, ApplicationContext context)
        {
            context.Database.EnsureCreated();

            //TODO Consider using Secret Manager Tool instead
            await EnsureUserRole(serviceProvider, "admin@z1c2.com", "Admin@z1c2");

            if (context.Souvenirs.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category{ Name="Maori Gifts", PathOfImage="/images/Categories/MaoriGifts.svg" },
                new Category{ Name="Jewellery", PathOfImage="/images/Categories/Jewellery.svg" },
                new Category{ Name="Art", PathOfImage="/images/Categories/artwork.svg" },
                new Category{ Name="Homeware", PathOfImage="/images/Categories/Homeware.svg" },
                new Category{ Name="Clothing", PathOfImage="/images/Categories/Clothing.svg" },
                new Category{ Name="Bags", PathOfImage="/images/Categories/Bags.svg" },
            };
            foreach(Category category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]
            {
                new Supplier{ Name="Countdown Ltd", WorkPhoneNumber="1234567", Email="business@countdown.com", Address="179 Carrington Road" },
                new Supplier{ Name="New World Ltd", WorkPhoneNumber="1234567", Email="business@newworld.com", Address="179 Carrington Road" },
            };
            foreach(Supplier supplier in suppliers)
            {
                context.Suppliers.Add(supplier);
            }
            context.SaveChanges();

            var souvenirs = new Souvenir[]
            {
                new Souvenir{ Name="Test Maori Gifts", Description="This is a test product", Price=12.8, Popularity=1, Category=categories[0], Supplier=suppliers[0]},
                new Souvenir{ Name="Test Jewellery", Description="This is a test product", Price=31.8, Popularity=2, Category=categories[1], Supplier=suppliers[0]},
                new Souvenir{ Name="Test Art", Description="This is a test product", Price=49.90, Popularity=3, Category=categories[2], Supplier=suppliers[0]},
                new Souvenir{ Name="Test Homeware", Description="This is a test product", Price=27.5, Popularity=4, Category=categories[3], Supplier=suppliers[1]},
                new Souvenir{ Name="Test Clothing", Description="This is a test product", Price=13.8, Popularity=5, Category=categories[4], Supplier=suppliers[1]},
                new Souvenir{ Name="Test Bags", Description="This is a test product", Price=5.99, Popularity=6, Category=categories[5], Supplier=suppliers[1]},
                new Souvenir{ Name="Test Maori Gifts 1", Description="This is a test product", Price=12.8, Popularity=1, Category=categories[0], Supplier=suppliers[0]},
                new Souvenir{ Name="Test Jewellery 1", Description="This is a test product", Price=31.8, Popularity=2, Category=categories[1], Supplier=suppliers[0]},
                new Souvenir{ Name="Test Art 1", Description="This is a test product", Price=49.90, Popularity=3, Category=categories[2], Supplier=suppliers[0]},
                new Souvenir{ Name="Test Homeware 1", Description="This is a test product", Price=27.5, Popularity=4, Category=categories[3], Supplier=suppliers[1]},
                new Souvenir{ Name="Test Clothing 1", Description="This is a test product", Price=13.8, Popularity=5, Category=categories[4], Supplier=suppliers[1]},
                new Souvenir{ Name="Test Bags 1", Description="This is a test product", Price=5.99, Popularity=6, Category=categories[5], Supplier=suppliers[1]},
            };
            foreach (Souvenir souvenir in souvenirs)
            {
                context.Souvenirs.Add(souvenir);
            }
            context.SaveChanges();
        }

        private static async Task EnsureUserRole(IServiceProvider serviceProvider, string UserName, string Password)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();
            if (userManager == null)
            {
                throw new Exception("userManager null");
            }

            //create default admin account
            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new AppUser { UserName = UserName, FullName = Roles.Admin, Email = UserName, Enabled = true };
                await userManager.CreateAsync(user, Password);
            }

            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            //create roles
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Customer))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Customer));
            }

            await userManager.AddToRoleAsync(user, Roles.Admin);
        }
    }
}
