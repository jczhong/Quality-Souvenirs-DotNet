using QualitySouvenirs.Models;
using System;
using System.Linq;

namespace QualitySouvenirs.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

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
                new Souvenir{ Name="Test Maori Gifts", Description="This is a test product", Price=12.8, PathOfImage="", CategoryID=1},
                new Souvenir{ Name="Test Jewellery", Description="This is a test product", Price=31.8, PathOfImage="", CategoryID=2},
                new Souvenir{ Name="Test Art", Description="This is a test product", Price=93.8, PathOfImage="", CategoryID=3},
                new Souvenir{ Name="Test Homeware", Description="This is a test product", Price=3.8, PathOfImage="", CategoryID=4},
                new Souvenir{ Name="Test Clothing", Description="This is a test product", Price=13.8, PathOfImage="", CategoryID=5},
                new Souvenir{ Name="Test Bags", Description="This is a test product", Price=33.8, PathOfImage="", CategoryID=6},
            };
            foreach(Souvenir souvenir in souvenirs)
            {
                context.Souvenirs.Add(souvenir);
            }
            context.SaveChanges();
        }
    }
}
