using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ZealandIdScanner.Models.Sensors;
using ZealandIdScanner.Models;
using ZealandIdScanner;
using Microsoft.EntityFrameworkCore;
using ZealandIdScanner.EBbContext;

namespace ZealandIdScanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LokaleController : ControllerBase
    {
        private readonly ZealandIdContext _dbContext;

        public LokaleController(ZealandIdContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Lokale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lokaler>>> GetLokaler()
        {
            if (_dbContext.Lokaler == null)
            {
                return NotFound();
            }
            return await _dbContext.Lokaler.ToListAsync();
        }

        [HttpGet("Id/{id}")]
        public async Task<ActionResult<Lokaler>> GetLokale(int id)
        {
            if (_dbContext.Set<Lokaler>() == null)
            {
                return NotFound("DbContext can't be null");
            }
            var lokaler = await _dbContext.Lokaler.FindAsync(id);

            if (lokaler == null)
            {
                return NotFound("No Such lokale exists");
            }
            return Ok(lokaler);
        }

        [HttpPost]
        public ActionResult PostNewLokale(Lokaler lokaler)
        {
            if (lokaler == null)
            {
                return BadRequest(lokaler);
            }
            if (lokaler.LokaleId == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _dbContext.SaveChangesAsync();
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            // Assuming you have configured DbContextOptions in your application's startup
            var optionsBuilder = new DbContextOptionsBuilder<ZealandIdContext>();
            optionsBuilder.UseSqlServer("your_connection_string_here"); // Replace with your actual connection string

            using (var ctx = new ZealandIdContext(optionsBuilder.Options))
            {
                ctx.Lokaler.Add(new Lokaler()
                {
                    LokaleId = lokaler.LokaleId,
                    Navn = lokaler.Navn,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }
        //public async Task<ActionResult<Lokaler>> PostLokale(Lokaler lokale)
        //{
        //    if (lokale == null)
        //    {
        //        return BadRequest("Invalid data");
        //    }

        //    try
        //    {
        //        _dbContext.Lokaler.Add(lokale);
        //        await _dbContext.SaveChangesAsync();

        //        // Change the following line
        //        return CreatedAtAction("GetLokale", new { id = lokale.LokaleId }, lokale);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());

        //        // Return a detailed error response
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}



        //// GET: LokaleController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LokaleController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LokaleController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LokaleController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}


