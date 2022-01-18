using IJobs.Models;
using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services
{
    public interface ICompanyService
    {
        //ModelResultDTO GetDataMappedByTitle(string title);
        CompanyResponseDTO Authenticate(CompanyRequestDTO model);
        //get all
        IEnumerable<Company> GetAllCompanies();
        //get by name
        IEnumerable<Company> GetByTitle(string title);
        //get by name including jobs
        IEnumerable<Company> GetByTitleIncludingJobs(string title);
        void Create(Company company);
        Company FindById(Guid? id);
        Task<Company> FindByIdAsinc(Guid? id);
        void Update(Company entity);
        Task<bool> SaveAsync();
        bool Save();
        void Delete(Company entity);
    }
}
