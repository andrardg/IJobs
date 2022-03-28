using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.CompanyRepository
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(projectContext context) : base(context)
        {
        }
        public List<Company> GetByTitle(string title)
        {
            return _table.Where(s => s.Name!.ToLower().Contains(title.ToLower())).ToList();
        }
        public List<Company> GetByTitleIncludingJobs(string title)
        {
            var result = _table.Where(s => s.Name!.ToLower().Contains(title.ToLower()));
            return result.Include(x => x.Jobs).ToList();
        }
        public List<Company> GetAllWithInclude()
        {
            return _table.Include(x => x.Jobs).ToList();
        }
        public List<Company> GetAllWithJoin()
        {
            var result = _table.Join(_context.Jobs, c => c.Id, j => j.CompanyId,
                (c, j) => new { c, j }).Select(obj => obj.c);
            return result.ToList();
        }

        public List<Company> GetByEmail(string email)
        {
            return _table.Where(x => x.Email!.ToLower().Equals(email.ToLower())).ToList();
        }
    }
}
