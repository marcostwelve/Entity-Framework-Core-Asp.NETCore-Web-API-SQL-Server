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
        private readonly IEFCoreRepository _repo;
        public BatalhaController(IEFCoreRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var herois = await _repo.GetAllBatalhas(true);

                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var heroi = await _repo.GetBatalhaById(id);
                if(heroi != null)
                {
                    return Ok(heroi);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }

            return BadRequest("Não Encontrado");
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public async Task<IActionResult> Post(Batalha batalha)
        {
            try
            {
                _repo.Add(batalha);

                if(await _repo.SaveChangeAsync())
                {
                    return Ok("Bazinga");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }

            return BadRequest("não salvou");
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha batalha)
        {
            try
            {
                var heroi = await _repo.GetBatalhaById(id);
                if (heroi != null)
                {
                    _repo.Update(batalha);
                    if (await _repo.SaveChangeAsync())
                    {
                        return Ok("Bazinga");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }

            return BadRequest("Não encontrado");
        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var heroi = await _repo.GetBatalhaById(id);
                if(heroi != null)
                {
                    _repo.Delete(heroi);
                    if(await _repo.SaveChangeAsync())
                    {
                        return Ok("Bazinga");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }

            return BadRequest("Não encontrado");
        }
    }
}
