using IJobs.Models.DTOs;
using IJobs.Models;
using IJobs.Repositories.CompanyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IJobs.Services;
using IJobs.Utilities.JWTUtils;
using IJobs.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace IJobs.Services
{
    public class CompanyService : ICompanyService
    {
        public ICompanyRepository _companyRepository;
        private IJWTUtils<Company> ijwtUtils; 
        public projectContext context;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public CompanyResponseDTO Authenticate(CompanyRequestDTO model)
        {
            var company = context.Companies.FirstOrDefault(x => x.Email == model.Email);
            if (company == null || BCrypt.Net.BCrypt.Verify(model.PasswordHash, company.PasswordHash))
            {
                return null;
            }
            //JWT generation (JSON WEB TOKEN)
            var jwtToken = ijwtUtils.GenerateJWTToken(company);
            return new CompanyResponseDTO(company, jwtToken);
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            var results = _companyRepository.GetAllWithInclude();
            return (IEnumerable<Company>)results;
        }

        public IEnumerable<Company> GetByTitle(string title)
        {
            var result = _companyRepository.GetByTitle(title);
            return result;
        }
        public IEnumerable<Company> GetByTitleIncludingJobs(string title)
        {
            var result = _companyRepository.GetByTitleIncludingJobs(title);
            return result;
        }
        public void Create(Company company)
        {
            company.Id = Guid.NewGuid();
            company.DateCreated = DateTime.UtcNow;
            company.DateModified = DateTime.UtcNow;
            _companyRepository.Create(company);
            _companyRepository.Save();
                
        }

        public Company FindById(Guid? id)
        {
            return _companyRepository.FindById(id);
        }

        public async Task<Company> FindByIdAsinc(Guid? id)
        {
            return await _companyRepository.FindByIdAsinc(id);
        }
        public void Update(Company entity)
        {
            _companyRepository.Update(entity);
        }
        public bool Save()
        {
            try
            {
                return _companyRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _companyRepository.SaveAsync() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public void Delete(Company entity)
        {
            _companyRepository.Delete(entity);
        }
    }
}
