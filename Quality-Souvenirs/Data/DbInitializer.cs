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
                new Category{ Name="Hats" },
                new Category{ Name="Toys" },
            };
            foreach(Category category in categories)
            {
                context.Categories.Add(category);
            }

            var souvenirs = new Souvenir[]
            {
                new Souvenir{ Name="Test hat", Description="This is a test product", Price=12.8, PathOfImage=""}
            };
            foreach(Souvenir souvenir in souvenirs)
            {
                context.Souvenirs.Add(souvenir);
            }
        }
    }
}
