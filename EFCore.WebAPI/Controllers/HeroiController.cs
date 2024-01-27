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
        private readonly IEFCoreRepository _repo;
        public HeroiController(IEFCoreRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var herois = await _repo.GetAllHerois();
                if(herois != null)
                {
                    return Ok(herois);

                }
                return Ok("Nenhum heroi cadastrado");

            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var heroibanco = await _repo.GetHeroiById(id);
                if(heroibanco != null)
                {
                    return Ok(heroibanco);
                }

                return BadRequest("Não encontrado");
            }
            catch(Exception ex) 
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post(Heroi heroi)
        {
            try
            {
                _repo.Add(heroi);
                if(await _repo.SaveChangeAsync())
                {
                    return Ok("Bazinga");

                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: "+ ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Heroi heroi)
        {
            try
            {
                var heroiBanco = await _repo.GetHeroiById(id);
                if (heroiBanco != null)
                {
                    heroiBanco.Nome = heroi.Nome;
                    _repo.Update(heroiBanco);
                    await _repo.SaveChangeAsync();
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroiBanco = await _repo.GetHeroiById(id);

                if (heroiBanco != null)
                {
                    _repo.Delete(heroiBanco);
                    await _repo.SaveChangeAsync();
                    return Ok("Bazinga");
                }
                return Ok("Não encontrado");
            }
            catch(Exception ex)
            {
                return BadRequest("Erro:  " + ex.Message);
            }
            
        }
    }
}
