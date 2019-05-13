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

            var souvenirs = new Souvenir[]
            {
                new Souvenir{ Name="Test Maori Gifts", Description="This is a test product", Price=12.8, Popularity=1, PathOfImage="/images/Products/Test Maori Gifts.png", Category=categories.Single(c => c.ID == 1)},
                new Souvenir{ Name="Large Maori Wood Hook Necklace", Description="This Maori Wood Hook Necklace is larger than most bone or greenstone necklaces, measuring approximately 6.6cm long.", Price=29.5, Popularity=1, PathOfImage="/images/Products/HookNecklace.jpg", Category=categories.Single(c => c.ID == 1)},
                new Souvenir{ Name="Maori Kauri Wheku Necklace", Description="This Maori Wheku Necklace is made from one of our most beautiful New Zealand timbers, Kauri.  Crafted from ancient swamp kauri, the timber is estimated to be more than 40,000 years old!", Price=19, Popularity=1, PathOfImage="/images/Products/WhekuNecklace.jpg", Category=categories.Single(c => c.ID == 1)},
                new Souvenir{ Name="Maori Kauri Tiki Necklace", Description="This Maori Tiki Necklace is made from one of our most beautiful New Zealand timbers, Kauri.  Crafted from ancient swamp kauri, the timber is estimated to be more than 40,000 years old!", Price=19, Popularity=1, PathOfImage="/images/Products/TikiNecklace.jpg", Category=categories.Single(c => c.ID == 1)},
                new Souvenir{ Name="NZ Paua Shell Maori Twist Necklace", Description="This NZ Paua Shell Maori Twist Necklace is a real beauty.", Price=29.95, Popularity=1, PathOfImage="/images/Products/TwistNecklace.jpg", Category=categories.Single(c => c.ID == 1)},
                new Souvenir{ Name="NZ Maori Bone and Wood Hook Necklace", Description="This NZ Maori Bone and Wood Hook Necklace hangs from a string cord,", Price=35, Popularity=1, PathOfImage="/images/Products/Bone and Wood Hook Necklace.jpg", Category=categories.Single(c => c.ID == 1)},
                new Souvenir{ Name="Test Jewellery", Description="This is a test product", Price=31.8, Popularity=2, PathOfImage="/images/Products/Test Jewellery.jpg", Category=categories.Single(c => c.ID == 2)},
                new Souvenir{ Name="Test Art", Description="This is a test product", Price=49.90, Popularity=3, PathOfImage="/images/Products/testart.jpg", Category=categories.Single(c => c.ID == 3)},
                new Souvenir{ Name="Test Homeware", Description="This is a test product", Price=27.5, Popularity=4, PathOfImage="/images/Products/Homeware.jpg", Category=categories.Single(c => c.ID == 4)},
                new Souvenir{ Name="Test Clothing", Description="This is a test product", Price=13.8, Popularity=5, PathOfImage="/images/Products/Test Clothing.jpg", Category=categories.Single(c => c.ID == 5)},
                new Souvenir{ Name="Test Bags", Description="This is a test product", Price=5.99, Popularity=6, PathOfImage="/images/Products/Test Bags.jpg", Category=categories.Single(c => c.ID == 6)},
            };
            foreach(Souvenir souvenir in souvenirs)
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
                user = new AppUser { UserName = UserName, FullName = Roles.Admin, Email = UserName };
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
