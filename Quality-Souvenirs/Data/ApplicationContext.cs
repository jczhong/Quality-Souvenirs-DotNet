using System;
using Microsoft.EntityFrameworkCore;

namespace QualitySouvenirs.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
