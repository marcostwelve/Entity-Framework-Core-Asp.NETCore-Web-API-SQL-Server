using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Repo
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly HeroiContext _context;
        public EFCoreRepository(HeroiContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SavaCHangeAsync()
        {
            return  (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Heroi[]> GetAllHerois(bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if(incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.batalha);

            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
               .Include(h => h.Identidade)
               .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.batalha);

            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Heroi[]> GetHeroisByName(string name, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _context.Herois
               .Include(h => h.Identidade)
               .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas).ThenInclude(heroib => heroib.batalha);

            }
            query = query.AsNoTracking().Where(h => h.Nome.Contains(name)).OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha[]> GetAllBatalhas(bool incluirHerois = false)
        {
            IQueryable<Batalha> query = _context.Batalhas;

            if (incluirHerois)
            {
                query = query.Include(h => h.HeroiBatalhas).ThenInclude(heroib => heroib.Heroi);

            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha> GetBatalhaById(int id, bool incluirBatalha = false)
        {
            IQueryable<Batalha> query = _context.Batalhas;

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroiBatalhas).ThenInclude(heroib => heroib.Heroi);

            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
