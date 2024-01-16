using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly HeroiContext _context;
        public BatalhaController(HeroiContext context)
        {
            _context = context;
        }
        // GET: api/<BatalhaController>
        [HttpGet]
        public ActionResult Get()
        {

            try
            {
                return Ok(new Batalha());
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public ActionResult Post(Batalha batalha)
        {
            try
            {
                _context.Batalhas.Add(batalha);
                _context.SaveChanges();
                return Ok("Bazinga");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id,  Batalha batalha)
        {
            try
            {
                if (_context.Batalhas.AsNoTracking().FirstOrDefault(b => b.Id == id) != null)
                {
                    _context.Batalhas.Update(batalha);
                    _context.SaveChanges();
                    return Ok("Bazinga");
                }
                return Ok("não Encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
