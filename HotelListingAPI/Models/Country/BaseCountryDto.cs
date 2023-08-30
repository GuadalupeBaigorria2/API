using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Country
{
    //la clase abstract no se puede instanciar
 public abstract class BaseCountryDto
 {

    [Required]
    public string Name { get; set; }
    public string ShortName { get; set; }
 
}
}