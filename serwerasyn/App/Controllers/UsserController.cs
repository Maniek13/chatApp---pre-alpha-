using serwer.App.Context;
using serwer.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serwer.App.Controllers
{
    class UsserController
    {
        private readonly ChatContext _context;

        public UsserController()
        {
            _context = new ChatContext();
        }

        public async Task<int> AddUsser(Usser usser)
        {
            try{
                _context.Ussers.Add(usser);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch{
                return -1;
            }
        }

        public int FindUsser(Usser usser)
        {
            try
            {
                var finded = _context.Ussers.SqlQuery("Select * from Ussers").ToListAsync().Result.Find(el => el.Name == usser.Name && el.Password == usser.Password);

                
                if(finded != null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
                
            }
            catch
            {
                return -1;
            }
        }

        public List<Usser> FindAllUssers()
        {
            try
            {
                return _context.Ussers.SqlQuery("Select * from Ussers").ToListAsync().Result;
            }
            catch
            {
                return null;
            }
        }

    }
}
