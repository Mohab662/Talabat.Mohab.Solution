using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
           _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Product))
            {
                return (IEnumerable<T>) await _context.Set<Product>().Include(P=>P.Brand).Include(P => P.Category).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<T> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _context.Set<Product>().Where(P => P.Id == id).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as T;
            }
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
