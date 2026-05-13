using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Authorize(Roles = "manager")]
[Route("donors/[controller]")]
public class DonorsController : Controller
{
    private readonly IDonorsService donorsService;
    private readonly ILogger<DonorsController> logger;
    public DonorsController(IDonorsService donorsService, ILogger<DonorsController> logger)
    {
        this.donorsService = donorsService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<DtoDonors>>> GetAllDonors()
    {
            var donors = await donorsService.GetAllDonors();
            return Ok(donors);
    }

    [HttpGet]
    [Route("GetByName/{name}")]
    public async Task<ActionResult<DtoDonorsCreate>> GetByNameAsync(string name)
    {
        try
        {
            var donor = await donorsService.GetByNameAsync(name);
            return Ok(donor);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetByEmail/{email}")]
    public async Task<ActionResult<DtoDonors>> GetByEmailAsync(string email)
    {
        try
        {
            Console.WriteLine("Getting donor by email: " + email);
            var donor = await donorsService.GetByEmailAsync(email);
            return Ok(donor);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetByGift/{nameGift}")]
    public async Task<ActionResult<DtoDonors>> GetByGiftAsync(string nameGift)
    {
        try
        {
            Console.WriteLine("Getting donor by gift: " + nameGift);
            var donor = await donorsService.GetByGiftAsync(nameGift);
            return Ok(donor);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateDonorAsync(DtoDonorsCreate donor)
    {
        try {
            await donorsService.CreateDonorAsync(donor);
            return Ok("Succeded!");
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex) //---
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteDonorAsync(int id)
    {
        try
        {
            await donorsService.DeleteDonorAsync(id);
            return Ok("Deleted succssfully");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateDonorAsync(DtoDonorsUpdate donor, int id)
    {
        try
        {
            await donorsService.UpdateDonorAsync(donor, id);
            return Ok("Updated");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex) //-----
        {
            return BadRequest(ex.Message);
        }
    }
        [HttpPut]
        [Route("AddDonotation/{donorId}/{giftId}")]
        public async Task<ActionResult<string>> AddDonotationAsync(int  donorId, int giftId)
    {
        try
        {
            await donorsService.AddDonotationAsync(giftId, donorId);
            return Ok("התרומה נוספה בהצלחה!!");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<Donors> GetDonorByIdAsync(int id)
    {
        try
        {
            var donor = await donorsService.GetDonorByIdAsync(id);
            return donor;
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
    }
}



