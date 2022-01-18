using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IJobs.Data;
using IJobs.Models;
using IJobs.Services;
using IJobs.Repositories.CompanyRepository;
using AutoMapper;
using IJobs.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace IJobs.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        public CompaniesController( ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Companies
        //[System.Web.Mvc.AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var results = _service.GetAllCompanies();
            var dtos = new List<CompanyRequestDTO>();
            foreach (var result in results)
            {
                var companyDTO = _mapper.Map<CompanyRequestDTO>(result);
                dtos.Add(companyDTO);
            }
            return View(dtos);
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = _service.FindById(id);
            if (company == null)
            {
                return NotFound();
            }
            var companyDTO = _mapper.Map<CompanyRequestDTO>(company);
            return View(companyDTO);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,PasswordHash,Address,Description")] Company company)
        {

            var companyDTO = _mapper.Map<CompanyRequestDTO>(company);
            if (ModelState.IsValid)
            {
                _service.Create(company);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(companyDTO);
        }
        
        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _service.FindByIdAsinc(id);
            if (company == null)
            {
                return NotFound();
            }
            var companyDTO = _mapper.Map<CompanyRequestDTO>(company);
            return View(companyDTO);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Email,PasswordHash,Address,Description,verifiedAccount,Id,DateCreated,DateModified")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    company.DateModified = DateTime.UtcNow;
                    _service.Update(company);
                    await _service.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_service.FindByIdAsinc(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var companyDTO = _mapper.Map<CompanyRequestDTO>(company);
            return View(companyDTO);
        }
        
        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _service.FindByIdAsinc(id);
            if (company == null)
            {
                return NotFound();
            }

            var companyDTO = _mapper.Map<CompanyRequestDTO>(company);
            return View(companyDTO);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var company = await _service.FindByIdAsinc(id);
            _service.Delete(company);
            await _service.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
