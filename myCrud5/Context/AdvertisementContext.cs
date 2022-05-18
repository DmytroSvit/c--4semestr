using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using myCrud.Models;
using myCrud.Models.Authorization;

namespace myCrud.Context
{

    public class AdvertisementContext : IdentityDbContext<User>
    {
        public AdvertisementContext(DbContextOptions<AdvertisementContext> options) : base(options) { }
        public DbSet<Advertisement> Advertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}