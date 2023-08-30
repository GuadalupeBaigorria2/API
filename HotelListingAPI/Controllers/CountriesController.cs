using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using AutoMapper;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesController(HotelListingDbContext context, Mapper mapper)
        {
            
            _context = context;
            _mapper = mapper;
        }
        

        // GET: api/Countries/GetCountries
         [HttpGet("GetCountries")]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countryList = await _context.Countries.ToListAsync();
            var countries = 
            _mapper.Map<List<GetCountryDto>>(countryList); 
            return Ok(countries);
        }

        // GET: api/Countries/GetCountry/5
         [HttpGet("GetCountry/{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
          var country = await _context.Countries.Include(q => q.Hotels) 
            // inner join
            .FirstOrDefaultAsync(q => q.Id == id);
            if (country == null) 
            {
            return NotFound();
            }
            //  var countryDto = _mapper.Map<CountryDto>(country); Esto no seria necesario
            return Ok(country);
        }

        // PUT: api/Countries/PutCountry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutCountry/{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            // if (id != country.Id)
            // {
            //     return BadRequest();
            // }
            
            // Cuando se usa automapper no hace falta modified

            // _context.Entry(country).State = EntityState.Modified;

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
            return NotFound();
            }
            _mapper.Map(updateCountryDto, country);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries/PostCountry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostCountry")]
        public async Task<ActionResult<CountryDto>> PostCountry(HotelListing.API.Models.Country.CreateCountryDto createCountryDto)
        {
            //    var country = new Country
            //     {
            //         Name = createCountry.Name,
            //         ShortName = createCountry.ShortName,
            //     };

            var country = _mapper.Map<Country>(createCountryDto);

                _context.Countries.Add(country);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCountry", new { id = country.Id }, country);

        }

        // DELETE: api/Countries/DeleteCountry/5
        [HttpDelete("DeleteCountry/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
