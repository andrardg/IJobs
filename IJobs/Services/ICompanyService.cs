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
        void Register(CompanyRequestDTO model);
        void Create(Company company);
        void Update(Guid? id, CompanyRequestDTO model);
        void Update(Company entity);
        //get all
        IEnumerable<CompanyResponseDTO> GetAllCompanies();
        CompanyResponseDTO GetById(Guid? id);
        Task<CompanyResponseDTO> GetByIdAsinc(Guid? id);
        //get by name
        IEnumerable<CompanyResponseDTO> GetByTitle(string title);
        //get by name including jobs
        IEnumerable<CompanyResponseDTO> GetByTitleIncludingJobs(string title);
        bool Save();
        Task<bool> SaveAsync();
        void Delete(Company entity);
    }
}
