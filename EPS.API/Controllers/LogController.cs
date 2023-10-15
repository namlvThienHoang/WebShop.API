using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.Data.SolrEntities;
using EPS.Service;
using EPS.Service.Dtos.Log;

using EPS.Service.Helpers;
using EPS.Utils.Common;
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
    [Route("api/logs")]
    [Authorize]
    public class LogController : BaseController
    {
        private AuthorizationService _userService;

        public LogController(AuthorizationService userService)
        {
            _userService = userService;
        }

        //list all
        //[CustomAuthorize(PrivilegeList.ViewLog, PrivilegeList.ManageLog)]
        [HttpGet]
        public async Task<IActionResult> GetLists([FromQuery] TSolrLogQuery oTSolrQuery)
        {
            return Ok(await _SolrLogServices.FilterPagedAsync(oTSolrQuery, oTSolrQuery.GetSolrQuery().ToArray()));
        }
        //list all xử lý log Tài liệu
        //[CustomAuthorize(PrivilegeList.ViewLog, PrivilegeList.ManageLog)]
        [HttpGet("logtailieu")]
        public async Task<IActionResult> GetListTaiLieus([FromQuery] TSolrLogQuery oTSolrQuery)
        {
            oTSolrQuery.DoiTuong = (int)DOITUONG.Change;
            return Ok(await _SolrLogServices.FilterPagedAsync(oTSolrQuery, oTSolrQuery.GetSolrQuery().ToArray()));
        }

       

        //detail
        //[CustomAuthorize(PrivilegeList.ViewLog, PrivilegeList.ManageLog)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await BaseService.FindAsync<Log, LogDetailDto>(id));
        }

        ////create
        ////[CustomAuthorize(PrivilegeList.ManageLog)]
        //[HttpPost]
        //public async Task<IActionResult> Create(LogCreateDto LogCreateDto)
        //{
        //    await BaseService.CreateAsync<Log, LogCreateDto>(LogCreateDto);
        //    return Ok();
        //}
        [HttpPost("access/{**url}")]
        public async Task<IActionResult> CreateAccess(string url)
        {
            await AddLogAsync( url, DOITUONG.Access,(int) ActionLogs.Access, (int)StatusLogs.Success);
            return Ok();
        }
        ////update
        //[CustomAuthorize(PrivilegeList.ManageLog)]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, LogUpdateDto LogUpdateDto)
        //{
        //    await BaseService.UpdateAsync<Log, LogUpdateDto>(id, LogUpdateDto);
        //    return Ok(true);
        //}

        //[CustomAuthorize(PrivilegeList.ManageLog)]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await BaseService.DeleteAsync<Log, int>(id);
        //    return Ok(true);
        //}

        ////multiple delete 
        //[CustomAuthorize(PrivilegeList.ManageLog)]
        //[HttpDelete]
        //public async Task<IActionResult> Deletes(string ids)
        //{
        //    if (string.IsNullOrEmpty(ids))
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var LogIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        //        await BaseService.DeleteAsync<Log, int>(LogIds);
        //        return Ok(true);
        //    }
        //    catch (FormatException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}




    }
}
