using Microsoft.EntityFrameworkCore;
using myCrud.Models;

namespace myCrud.Context
{

    public class AdvertisementContext : DbContext
    {
        public AdvertisementContext(DbContextOptions<AdvertisementContext> options) : base(options) { }
        public DbSet<Advertisement> Advertisements { get; set; }
    }
}