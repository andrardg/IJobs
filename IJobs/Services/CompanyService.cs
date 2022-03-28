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
        private IJWTUtils<Company> _ijwtUtils; 
        public projectContext _context;
        private readonly IMapper _mapper;
        public CompanyService(projectContext context, IMapper mapper, IJWTUtils<Company> ijwtUtils, ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _context = context;
            _ijwtUtils = ijwtUtils;
            _mapper = mapper;
        }

        public CompanyResponseDTO Authenticate(CompanyRequestDTO model)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Email == model.Email);
            if (company == null || BCrypt.Net.BCrypt.Verify(model.Password, company.PasswordHash))
            {
                throw new Exception("Email or password is incorrect");
                //return null;
            }
            //auth successful
            var response = _mapper.Map<CompanyResponseDTO>(company);
            //JWT generation (JSON WEB TOKEN)
            response.Token = _ijwtUtils.GenerateJWTToken(company);
            return response;

            //JWT generation (JSON WEB TOKEN)
            //var jwtToken = _ijwtUtils.GenerateJWTToken(company);
            //return new CompanyResponseDTO(company, jwtToken);
        }

        public void Register(CompanyRequestDTO model)
        {
            // validate
            if (_context.Companies.Any(x => x.Email == model.Email))
                throw new Exception("Email '" + model.Email + "' is already taken");

            // map model to new user object
            var company = _mapper.Map<Company>(model);

            // hash password
            company.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            Create(company);
            Save();
        }
        public void Create(Company company)
        {
            company.Id = Guid.NewGuid();
            company.DateCreated = DateTime.UtcNow;
            company.DateModified = DateTime.UtcNow;
            _companyRepository.Create(company);
            _companyRepository.Save();

        }
        public void Update(Guid? id, CompanyRequestDTO model)
        {
            var company = _companyRepository.GetById(id);

            // validate
            if (model.Email != company.Email && _companyRepository.GetByEmail(model.Email) == null)
                throw new Exception("Email '" + model.Email + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                company.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, company);
            Update(company);
            Save();
        }
        public void Update(Company entity)
        {
            _companyRepository.Update(entity);
        }
        public IEnumerable<CompanyResponseDTO> GetAllCompanies()
        {
            var results = _companyRepository.GetAllWithInclude();
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public CompanyResponseDTO GetById(Guid? id)
        {
            var company = _companyRepository.GetById(id);
            if (company == null)
                throw new KeyNotFoundException("Company not found");
            var response = _mapper.Map<CompanyResponseDTO>(company);
            return response;
        }
        public async Task<CompanyResponseDTO> GetByIdAsinc(Guid? id)
        {
            var company = await _companyRepository.GetByIdAsinc(id);
            if (company == null)
                throw new KeyNotFoundException("Company not found");
            var response = _mapper.Map<CompanyResponseDTO>(company);
            return response;
        }
        public IEnumerable<CompanyResponseDTO> GetByTitle(string title)
        {
            var results = _companyRepository.GetByTitle(title);
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }        
        public IEnumerable<CompanyResponseDTO> GetByTitleIncludingJobs(string title)
        {
            var results = _companyRepository.GetByTitleIncludingJobs(title);
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
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
