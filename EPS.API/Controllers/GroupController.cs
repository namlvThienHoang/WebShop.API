using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.Data.SolrEntities;
using EPS.Service;
using EPS.Service.Dtos.Group;
using EPS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/groups")]
    [Authorize]
    public class GroupController : BaseController
    {
        private AuthorizationService _userService;
        private SolrServices<SolrLogs> _SolrLogServices;
        public GroupController(AuthorizationService userService, ISolrOperations<SolrLogs> _solrModellog)
        {
            _userService = userService;
            _SolrLogServices = new SolrServices<SolrLogs>(_solrModellog);
        }

        //list all
        [CustomAuthorize("groups", true)]
        [HttpGet]
        public async Task<IActionResult> GetLists([FromQuery]GroupGridPaging pagingModel)
        {
            var predicates = pagingModel.GetPredicates();
            return Ok(await BaseService.FilterPagedAsync<Group, GroupGridDto>(pagingModel, predicates.ToArray()));
        }

        //detail
        [CustomAuthorize("groups", true)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await BaseService.FindAsync<Group, GroupDetailDto>(id));
        }

        //create
        [CustomAuthorize("groups", PrivilegeList.Add)]
        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateDto GroupCreateDto)
        {
            await BaseService.CreateAsync<Group, GroupCreateDto>(GroupCreateDto);
            await AddLogAsync("Thêm mới: " + GroupCreateDto.Title, DOITUONG.GROUPS, (int)ActionLogs.Add, (int)StatusLogs.Success, Convert.ToString(GroupCreateDto.Id));
            return Ok();
        }

        //update
        [CustomAuthorize("groups", PrivilegeList.Edit)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GroupUpdateDto GroupUpdateDto)
        {
            await BaseService.UpdateAsync<Group, GroupUpdateDto>(id, GroupUpdateDto);
            await AddLogAsync( "Chỉnh sửa: " + GroupUpdateDto.Title, DOITUONG.GROUPS,(int) ActionLogs.Edit, (int)StatusLogs.Success, Convert.ToString(GroupUpdateDto.Id));
            return Ok(true);
        }

        [CustomAuthorize("groups", PrivilegeList.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await BaseService.DeleteAsync<Group, int>(id);
            await AddLogAsync( "Xóa: " + id, DOITUONG.GROUPS, (int)ActionLogs.Delete, (int)StatusLogs.Success, id);
            return Ok(true);
        }

        //multiple delete 
        [CustomAuthorize("groups", PrivilegeList.Delete)]
        [HttpDelete]
        public async Task<IActionResult> Deletes(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var GroupIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                await BaseService.DeleteAsync<Group, int>(GroupIds);
                foreach(var id in GroupIds)
                {
                    await AddLogAsync( "Xóa: " + id, DOITUONG.GROUPS, (int)ActionLogs.Delete, (int)StatusLogs.Success, id);
                }    
                return Ok(true);                
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }       
    }
}
