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
using AutoMapper;
using IJobs.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using IJobs.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace IJobs.Controllers
{
    //[Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UsersController(IUserService service, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _service = service;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        [System.Web.Mvc.AllowAnonymous]
        public IActionResult Authenticate( UserRequestDTO model)
        {
            var response = _service.Authenticate(model);
            return Ok(response);
        }
        [System.Web.Mvc.AllowAnonymous]
        public IActionResult Register(UserRequestDTO model)
        {
            _service.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        // GET: Users
        [System.Web.Mvc.AllowAnonymous]
        public IActionResult Index()
        {
            var results = _service.GetAllUsers();
            //var dtos = new List<UserRequestDTO>();
            //foreach (var result in results)
            //{
            //    var userDTO = _mapper.Map<UserRequestDTO>(result);
            //    dtos.Add(userDTO);
            //}
            //return View(dtos);
            return View(results);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            return View(userDTO);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,PasswordHash,Role,Id,DateCreated,DateModified")] User user)
        {
            var userDTO = _mapper.Map<UserRequestDTO>(user);
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                _service.Create(user);
                _service.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(userDTO);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _service.GetByIdAsinc(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FirstName,LastName,Email,PasswordHash,Role,Id,DateCreated,DateModified")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.DateModified = DateTime.UtcNow;
                    _service.Update(user);
                    await _service.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_service.GetByIdAsinc(id) == null)
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
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            return View(userDTO);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _service.GetByIdAsinc(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserResponseDTO>(user);
            return View(userDTO);
            //(new { message = "User deleted successfully" }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _service.GetByIdAsinc(id);
            var user = _mapper.Map<User>(response);
            _service.Delete(user);
            await _service.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
