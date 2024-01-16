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
    public class HeroiController : ControllerBase
    {
        private readonly HeroiContext _context;
        public HeroiController(HeroiContext context)
        {
            _context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(new Heroi());
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult Post(Heroi heroi)
        {
            try
            {
                _context.Herois.Add(heroi);
                _context.SaveChanges();
                return Ok("Bazinga");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: "+ ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi heroi)
        {
            try
            {
               if(_context.Herois.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    _context.Herois.Update(heroi);
                    _context.SaveChanges();
                    return Ok("Bazinga");

                }

                return Ok("Não encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
