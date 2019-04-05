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
                new Category{ Name="Maori Gifts", PathOfImage="/images/Categories/hat.svg" },
                new Category{ Name="Jewellery", PathOfImage="/images/Categories/hat.svg" },
                new Category{ Name="Art", PathOfImage="/images/Categories/hat.svg" },
                new Category{ Name="Homeware", PathOfImage="/images/Categories/hat.svg" },
                new Category{ Name="Clothing", PathOfImage="/images/Categories/hat.svg" },
                new Category{ Name="Bags", PathOfImage="/images/Categories/hat.svg" },
            };
            foreach(Category category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            var souvenirs = new Souvenir[]
            {
                new Souvenir{ Name="Test hat", Description="This is a test product", Price=12.8, PathOfImage=""},
                new Souvenir{ Name="Test toy", Description="This is a test product", Price=3.8, PathOfImage=""}
            };
            foreach(Souvenir souvenir in souvenirs)
            {
                context.Souvenirs.Add(souvenir);
            }
            context.SaveChanges();
        }
    }
}
