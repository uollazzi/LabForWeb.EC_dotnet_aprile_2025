using LabForWeb.EC.API.Extensions;
using LabForWeb.EC.API.Models;
using LabForWeb.EC.DAL;
using LabForWeb.EC.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabForWeb.EC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly ECContext _dc;

        public CategorieController(ECContext dc)
        {
            _dc = dc;
        }

        [HttpGet] // GET /api/categorie
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> GetAll()
        {
            var data = await _dc.Categorie.Select(c => c.ToCategoriaModel()).ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")] // GET /api/categorie/76
        public async Task<ActionResult<CategoriaModel>> GetById(int id)
        {
            // esegue una query (attraverso EF) sul database che corrisponde a:
            // SELECT * FROM Categorie WHERE Id = 76
            var cat = await _dc.Categorie.SingleOrDefaultAsync(x => x.Id == id);

            if (cat == null)
            {
                // genera automaticamente una RESPONSE HTTP 404
                return NotFound();
            }
            
            return Ok(cat.ToCategoriaModel());
        }

        [HttpPost] // POST /api/categorie
        public async Task<ActionResult<CategoriaModel>> Add(CategoriaDTO item)
        {
            // oggetto da salvare sul DB
            var cat = new Categoria
            {
                Nome = item.Nome
            };

            try
            {
                _dc.Categorie.Add(cat);
                await _dc.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Ok(cat.ToCategoriaModel());
        }


        //public async Task<ActionResult<CategoriaModel>> Update([FromRoute] int id, [FromBody] CategoriaDTO item)

        [HttpPut("{id}")] // PUT /api/categorie/3
        public async Task<ActionResult<CategoriaModel>> Update(int id, CategoriaDTO item)
        {
            // oggetto da salvare sul DB
            //var cat = await _dc.Categorie.SingleOrDefaultAsync(x => x.Id == id); // alternativa
            var cat = await _dc.Categorie.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            try
            {
                cat.Nome = item.Nome;
                await _dc.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return Ok(cat.ToCategoriaModel());
        }

        [HttpDelete("{id}")] // DELETE /api/categorie/3
        public async Task<ActionResult> Delete(int id)
        {            
            var cat = await _dc.Categorie.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            try
            {
                _dc.Categorie.Remove(cat);
                await _dc.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return NoContent();
        }
    }
}
