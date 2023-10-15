using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.GroupUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/groupuser")]
    [Authorize]
    public class GroupUserController : BaseController
    {
        private AuthorizationService _userService;

        public GroupUserController(AuthorizationService userService)
        {
            _userService = userService;
        }

        //list all
        [CustomAuthorize("groupuser", true)]
        [HttpGet]
        public async Task<IActionResult> GetLists([FromQuery] GroupUserGridPaging pagingModel)
        {
            var predicates = pagingModel.GetPredicates();
            return Ok(await BaseService.FilterPagedAsync<GroupUser, GroupUserGridDto>(pagingModel, predicates.ToArray()));
        }

        //detail
        [CustomAuthorize("groupuser", true)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await BaseService.FindAsync<GroupUser, GroupUserDetailDto>(id));
        }

        //create
        [CustomAuthorize("groupuser", PrivilegeList.Add)]
        [HttpPost]
        public async Task<IActionResult> Create(GroupUserCreateDto GroupUserCreateDto)
        {
            var pagingModel = new GroupUserGridPaging() { GroupId = GroupUserCreateDto.GroupId, UserId = GroupUserCreateDto.UserId, LstGroupIds=new List<int>() };
            var predicates = pagingModel.GetPredicates();
            var result = await BaseService.FilterPagedAsync<GroupUser, GroupUserGridDto>(pagingModel, predicates.ToArray());
            if (result.Data.Count > 0)
            {
                return BadRequest("Bản ghi đã tồn tại, vui lòng load lại trang đề làm mới");
            }
            await BaseService.CreateAsync<GroupUser, GroupUserCreateDto>(GroupUserCreateDto);
            return Ok();
        }

        //update
        [CustomAuthorize("groupuser", PrivilegeList.Edit)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GroupUserUpdateDto GroupUserUpdateDto)
        {
            await BaseService.UpdateAsync<GroupUser, GroupUserUpdateDto>(id, GroupUserUpdateDto);
            return Ok(true);
        }

        [CustomAuthorize("groupuser", PrivilegeList.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await BaseService.DeleteAsync<GroupUser, int>(id);
            return Ok(true);
        }
        //create
        [CustomAuthorize("groupuser", PrivilegeList.Delete)]
        [HttpPut("remove")]
        public async Task<IActionResult> Remove(GroupUserCreateDto GroupUserCreateDto)
        {
            var pagingModel = new GroupUserGridPaging() { GroupId = GroupUserCreateDto.GroupId, UserId = GroupUserCreateDto.UserId, LstGroupIds = new List<int>() };
            var predicates = pagingModel.GetPredicates();
            var result = await BaseService.FilterPagedAsync<GroupUser, GroupUserGridDto>(pagingModel, predicates.ToArray());
            if(result.Data.Count>0)
            {
                await BaseService.DeleteAsync<GroupUser, int>(result.Data.FirstOrDefault().Id);
                return Ok(true);
            }
            return BadRequest("Không tìm thấy bản ghi");
        }
        //multiple delete 
        [CustomAuthorize("groupuser", PrivilegeList.Delete)]
        [HttpDelete]
        public async Task<IActionResult> Deletes(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var GroupUserIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                await BaseService.DeleteAsync<GroupUser, int>(GroupUserIds);
                return Ok(true);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
