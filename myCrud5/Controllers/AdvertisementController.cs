using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using myCrud.Context;
using myCrud.Models;


namespace myCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private AdvertisementContext _context;

        public AdvertisementController(AdvertisementContext context)
        {
            _context = context;
        }

        private IQueryable<Advertisement> Filter(
            IQueryable<Advertisement> advertisements,
            string? filterBy,
            string? filterValue
        )
        {
            return (filterBy ?? string.Empty).ToLower() switch
            {
                "transactionnumber" => advertisements.Where(
                    s => EF.Functions.ILike(s.TransactionNumber, $"%{filterValue}%")),
                "title" => advertisements.Where(
                    s => EF.Functions.ILike(s.Title, $"%{filterValue}%")),
                "price" => advertisements.Where(
                    s => EF.Functions.ILike(s.Price.ToString(), $"%{filterValue}%")),
                "startdate" => advertisements.Where(
                    s => EF.Functions.ILike(s.StartDate.ToString(), $"%{filterValue}%")),
                "enddate" => advertisements.Where(
                    s => EF.Functions.ILike(s.EndDate.ToString(), $"%{filterValue}%")),
                "websiteurl" => advertisements.Where(
                    s => EF.Functions.ILike(s.WebsiteUrl, $"%{filterValue}%")),
                "photourl" => advertisements.Where(
                s => EF.Functions.ILike(s.PhotoUrl, $"%{filterValue}%")),
                "id" => advertisements.Where(
                    s => EF.Functions.ILike(s.Id.ToString(), $"%{filterValue}%")),
                "any" => advertisements.Where(
                    s => EF.Functions.ILike(
                        s.StartDate.ToString() + '$' +
                        s.EndDate + '$' +
                        s.Price + '$' +
                        s.Title + '$' +
                        s.Id + '$' +
                        s.TransactionNumber + '$' +
                        s.WebsiteUrl + '$' +
                        s.PhotoUrl,
                        $"%{filterValue}%")),
                _ => advertisements
            };
        }

        private IQueryable<Advertisement> Sort(
            IQueryable<Advertisement> advertisements,
            string? field
        )
        {
            if (field != null)
            {
                bool orderAsc = field.Contains("desc_");
                field = field?.Replace("desc_", "");
                IOrderedQueryable<Advertisement> sortedAdvertisements = field.ToLower() switch
                {
                    "id" => advertisements.OrderBy(s => s.Id),
                    "transactionnumber" => advertisements.OrderBy(s => s.TransactionNumber),
                    "title" => advertisements.OrderBy(s => s.Title),
                    "price" => advertisements.OrderBy(s => s.Price),
                    "startdate" => advertisements.OrderBy(s => s.StartDate),
                    "enddate" => advertisements.OrderBy(s => s.EndDate),
                    "websiteurl" => advertisements.OrderBy(s => s.WebsiteUrl),
                    "photourl" => advertisements.OrderBy(s => s.PhotoUrl),
                    _ => advertisements.OrderBy(s => s.Id)
                };
                return orderAsc ? sortedAdvertisements.Reverse() : sortedAdvertisements;
            }

            return advertisements;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Advertisement>> List(
            string? order = null,
            string? filterBy = null,
            string? filterValue = null
        )
        {
            string currentUser = User.Identity!.Name!;
            var dbAdvertisements = _context.Advertisements.AsQueryable();
            dbAdvertisements = dbAdvertisements.Where(s => s.Owner == currentUser);
            dbAdvertisements = Filter(dbAdvertisements, filterBy, filterValue);
            dbAdvertisements = Sort(dbAdvertisements, order);
            return Ok(dbAdvertisements);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Advertisement>> Retrieve(int id)
        {
            string currentUser = User.Identity!.Name!;
            Advertisement? dbAdvertisement = await _context.Advertisements.FindAsync(id);
            if (dbAdvertisement == null || dbAdvertisement.Owner != currentUser)
                return NotFound(new Dictionary<string, string> { { "detail", "Advertisement not found" } });
            return Ok(dbAdvertisement);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Advertisement>> Create(Advertisement advertisement)
        {
            string currentUser = User.Identity!.Name!;
            advertisement.Owner = currentUser;
            _context.Advertisements.Add(advertisement);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Advertisement>> Update(int id, Advertisement advertisement)
        {
            string currentUser = User.Identity!.Name!;
            Advertisement? dbAdvertisement = await _context.Advertisements.FindAsync(id);
            if (dbAdvertisement == null || dbAdvertisement.Owner != currentUser)
                return NotFound(new Dictionary<string, string> { { "detail", "Advertisement not found" } });

            dbAdvertisement.TransactionNumber = advertisement.TransactionNumber;
            dbAdvertisement.Title = advertisement.Title;
            dbAdvertisement.PhotoUrl = advertisement.PhotoUrl;
            dbAdvertisement.WebsiteUrl = advertisement.WebsiteUrl;
            dbAdvertisement.EndDate = advertisement.EndDate;
            dbAdvertisement.StartDate = advertisement.StartDate;
            dbAdvertisement.Price = advertisement.Price;

            await _context.SaveChangesAsync();
            return Ok(advertisement);
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Advertisement>> Delete(int id)
        {
            string currentUser = User.Identity!.Name!;
            Advertisement? dbAdvertisement = await _context.Advertisements.FindAsync(id);
            if (dbAdvertisement == null || dbAdvertisement.Owner != currentUser) 
                return NotFound(new Dictionary<string, string> { { "detail", "Advertisement not found" } });

            _context.Advertisements.Remove(dbAdvertisement);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
