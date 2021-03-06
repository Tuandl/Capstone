﻿using AutoMapper;
using Capstone.Model;
using Capstone.Service;
using Capstone.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;

        public UserRolesController(IMapper mapper, IUserRoleService UserRoleService)
        {
            _mapper = mapper;
            _userRoleService = UserRoleService;
        }

        // POST: api/UserRoles
        [HttpPost]
        public ActionResult PostUserRole(UserRoleCM model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var checkExist = _userRoleService.CheckExist(model.UserId, model.RoleID);
                if (checkExist != null) return BadRequest("Existed!");

                UserRole userRole = new UserRole();
                userRole = _mapper.Map<UserRole>(model);
                _userRoleService.Create(userRole);
                return StatusCode(201, userRole.ID);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/UserRoles
        [HttpGet("GetByUserID")]
        public ActionResult<IEnumerable<UserRole>> GetByUserID(string ID)
        {
            try
            {
                List<UserRoleVM> result = new List<UserRoleVM>();
                var data = _userRoleService.GetByUserID(ID);
                if (data.Count() == 0) return NotFound("List empty");
                foreach (var item in data)
                {
                    result.Add(_mapper.Map<UserRoleVM>(item));
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/UserRoles/5
        [HttpGet]
        public ActionResult<UserRole> GetUserRole(Guid ID)
        {
            try
            {
                var rs = _userRoleService.GetByID(ID);
                if (rs == null) return NotFound("ID not found");
                UserRoleVM result = _mapper.Map<UserRoleVM>(rs);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/UserRoles
        [HttpPut]
        public IActionResult PutUserRole(UserRoleUM model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var checkExist = _userRoleService.CheckExist(model.UserId, model.RoleID);
                if (checkExist != null) return BadRequest("Existed!");

                var userRoleInDb = _userRoleService.GetByID(model.ID);
                if (userRoleInDb == null) return BadRequest("ID not found!");
                _mapper.Map(model, userRoleInDb);
                _userRoleService.Save();
                return Ok("success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/UserRoles
        [HttpDelete]
        public ActionResult DeleteUserRole(Guid ID)
        {
            try
            {
                var userRoleInDb = _userRoleService.GetByID(ID);
                if (userRoleInDb == null) return NotFound("ID not found!");
                userRoleInDb.IsDeleted = true;
                _userRoleService.Save();
                return Ok("success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}