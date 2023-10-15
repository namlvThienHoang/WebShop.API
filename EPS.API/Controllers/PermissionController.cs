using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/permissions")]
    [Authorize]
    public class PermissionController : BaseController
    {
        private AuthorizationService _userService;

        public PermissionController(AuthorizationService userService)
        {
            _userService = userService;
        }

        //list all
        [CustomAuthorize("permissions", true)]
        [HttpGet]
        public async Task<IActionResult> GetLists([FromQuery]PermissionGridPaging pagingModel)
        {
            var predicates = pagingModel.GetPredicates();
            return Ok(await BaseService.FilterPagedAsync<Permission, PermissionGridDto>(pagingModel, predicates.ToArray()));
        }

        //detail
        [CustomAuthorize("permissions", true)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await BaseService.FindAsync<Permission, PermissionDetailDto>(id));
        }

        //create
        [CustomAuthorize("permissions", PrivilegeList.Add)]
        [HttpPost]
        public async Task<IActionResult> Create(PermissionCreateDto PermissionCreateDto)
        {
            await BaseService.CreateAsync<Permission, PermissionCreateDto>(PermissionCreateDto);
            return Ok();
        }

        //update
        [CustomAuthorize("permissions", PrivilegeList.Edit)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PermissionUpdateDto PermissionUpdateDto)
        {
            await BaseService.UpdateAsync<Permission, PermissionUpdateDto>(id, PermissionUpdateDto);
            return Ok(true);
        }

        //[CustomAuthorize(PrivilegeList.ManagePermission)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await BaseService.DeleteAsync<Permission, int>(id);
            return Ok(true);
        }

        //multiple delete 
        //[CustomAuthorize(PrivilegeList.ManagePermission)]
        [HttpDelete]
        public async Task<IActionResult> Deletes(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var PermissionIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                await BaseService.DeleteAsync<Permission, int>(PermissionIds);
                return Ok(true);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }       
    }
}
