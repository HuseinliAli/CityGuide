using CityGuideAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CityGuideAPI.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<City> Cities { get; set; }
    }
}
