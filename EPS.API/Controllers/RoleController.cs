using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/roles")]
    [Authorize]
    public class RoleController : BaseController
    {
        private AuthorizationService _userService;

        public RoleController(AuthorizationService userService)
        {
            _userService = userService;
        }

        //list all
        [CustomAuthorize("roles", true)]
        [HttpGet]
        public async Task<IActionResult> GetListRoles([FromQuery] RoleGridPagingDto pagingModel)
        {
            var predicates = pagingModel.GetPredicates();
            return Ok(await BaseService.FilterPagedAsync<Role, RoleGridDto>(pagingModel, predicates.ToArray()));
        }

        //detail
        [CustomAuthorize("roles", true)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            return Ok(await BaseService.FindAsync<Role, RoleDetailDto>(id));
        }

        //create
        [CustomAuthorize("roles", PrivilegeList.Add)]
        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateDto roleCreateDto)
        {
            await BaseService.CreateAsync<Role, RoleCreateDto>(roleCreateDto);
            return Ok();
        }

        //update
        [CustomAuthorize("roles", PrivilegeList.Edit)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleUpdateDto roleUpdateDto)
        {
            await BaseService.UpdateAsync<Role, RoleUpdateDto>(id, roleUpdateDto);
            return Ok(true);
        }

        [CustomAuthorize("roles", PrivilegeList.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await BaseService.DeleteAsync<Role, int>(id);
            return Ok(true);
        }

        //multiple delete 
        [CustomAuthorize("roles", PrivilegeList.Delete)]
        [HttpDelete]
        public async Task<IActionResult> DeleteRoles(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var roleIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                await BaseService.DeleteAsync<Role, int>(roleIds);
                return Ok(true);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
