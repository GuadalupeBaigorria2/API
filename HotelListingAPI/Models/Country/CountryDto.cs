using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Country
{
 public class CountryDto
 {
 public int Id { get; set; }
 public string Name { get; set; }
 public string ShortName { get; set; }
 
 public virtual IList<Hotel.HotelDto> Hotels { get; set; }
 }
}