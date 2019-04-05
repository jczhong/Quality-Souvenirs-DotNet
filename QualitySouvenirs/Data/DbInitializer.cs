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
                new Category{ Name="Kiwiana"},
                new Category{ Name="Jewellery"},
                new Category{ Name="Art" },
                new Category{ Name="Homeware" },
                new Category{ Name="Clothing" },
                new Category{ Name="Bags" },
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
