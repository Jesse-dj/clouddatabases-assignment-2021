using Online_Store_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Repositories
{
    public class EstateRepository : IEstateRepository
    {
        public async Task<Estate> AddAsync(Estate entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Estate>> AllIncludingAsync(params Expression<Func<Estate, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Estate entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteWhereAsync(Expression<Func<Estate, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Estate>> FindBy(Expression<Func<Estate, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Estate>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Estate> GetSingleAsync(uint id)
        {
            throw new NotImplementedException();
        }

        public async Task<Estate> GetSingleAsync(Expression<Func<Estate, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<Estate> GetSingleAsync(Expression<Func<Estate, bool>> predicate, params Expression<Func<Estate, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<Estate> UpdateAsync(Estate entity)
        {
            throw new NotImplementedException();
        }
    }
}
