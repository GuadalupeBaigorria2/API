using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        public HotelListingDbContext(DbContextOptions options) :
       base(options)
        {

        }

        // override Sobreescribe
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                Id = 1,
                Name = "Sandals Resort and Spa",
                Address = "Negril",
                CountryId = 1,
                Rating = 4.5
            },
            new Hotel
            {
                Id = 2,
                Name = "Comfort Suites",
                Address = "George Town",
                CountryId = 3,
                Rating = 4.3
            },
            new Hotel
            {
                Id = 3,
                Name = "Grand Palladium",
                Address = "Nassau",
                CountryId = 2,
                Rating = 4
            }
            );
            modelBuilder.Entity<Country>().HasData(
            new Country
            {
                Id = 1,
                Name = "Jamaica",
                ShortName = "JM"
            },
            new Country
            {
                Id = 2,
                Name = "Bahamas",
                ShortName = "BS"
            },
            new Country
            {
                Id = 3,
                Name = "Cayman Island",
                ShortName = "CI"
            }
            );

           
        }
    }
}


