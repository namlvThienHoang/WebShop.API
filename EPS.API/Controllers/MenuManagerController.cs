using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Group;
using EPS.Service.Dtos.MenuManager;
using EPS.Utils.Service;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/menumanager")]
    [Authorize]
    public class MenuManagerController : BaseController
    {
        private AuthorizationService _userService;

        public MenuManagerController(AuthorizationService userService)
        {
            _userService = userService;
            lstALL = new List<MenuManagerGridDto>();
        }

        //list all
        [CustomAuthorize("menumanager", true)]
        [HttpGet]
        public async Task<IActionResult> GetLists([FromQuery]MenuManagerGridPaging pagingModel)
        {
            return Ok(await GetListsData(pagingModel));
        }
        private async Task<PagingResult<MenuManagerGridDto>> GetListsData( MenuManagerGridPaging pagingModel)
        {

            return await BaseService.FilterPagedAsync<MenuManager, MenuManagerGridDto>(pagingModel);
          
        }
        [HttpGet("listtree")]
        public async Task<IActionResult> GetListTree([FromQuery] MenuManagerTreeGridPaging pagingModel)
        {
            pagingModel.ItemsPerPage = -1;
            pagingModel.Page = 1;
            return Ok(await BaseService.FilterPagedAsync<MenuManager, MenuManagerTreeGridDto>(pagingModel));
        }
        [HttpGet("export/{type}")]
        public async Task<IActionResult> Export([FromQuery] MenuManagerGridPaging pagingModel, string type)
        {
        
            return await ReturnExport(await CommonAPI.Export<MenuManagerGridDto>((await GetListsData(pagingModel)).Data, type, BaseService), type);

        }
        //detail
        [CustomAuthorize("menumanager", true)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var oMenu=await BaseService.FindAsync<MenuManager, MenuManagerDetailDto>(id);    
            if(!string.IsNullOrEmpty(oMenu.Groups))
            {
                var GroupIds = oMenu.Groups.Split(",")
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Select(y => Convert.ToInt32(y)).ToList();
                foreach (var item in GroupIds)
                {
                    GroupDetailDto oGroupUser = await BaseService.FindAsync<Group, GroupDetailDto>(item);
                    oMenu.LstGroups.Add(oGroupUser);
                }
            }
            return Ok(oMenu);
        }
      
        //create
        [CustomAuthorize("menumanager", PrivilegeList.Add)]
        [HttpPost]
        public async Task<IActionResult> Create(MenuManagerCreateDto MenuManagerCreateDto)
        {
            await BaseService.CreateAsync<MenuManager, MenuManagerCreateDto>(MenuManagerCreateDto);
            await AddLogAsync(UserIdentity.FullName + ": thêm " + MenuManagerCreateDto.Title, DOITUONG.MENUMANAGERS, (int)ActionLogs.Add, (int)StatusLogs.Success);
            return Ok();
        }

        //update
        [CustomAuthorize("menumanager", PrivilegeList.Edit)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MenuManagerUpdateDto MenuManagerUpdateDto)
        {
            await BaseService.UpdateAsync<MenuManager, MenuManagerUpdateDto>(id, MenuManagerUpdateDto);
            await AddLogAsync(UserIdentity.FullName + ": sửa " + MenuManagerUpdateDto.Title, DOITUONG.MENUMANAGERS, (int)ActionLogs.Edit, (int)StatusLogs.Success);
            return Ok(true);
        }

        [CustomAuthorize("menumanager", PrivilegeList.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await BaseService.DeleteAsync<MenuManager, int>(id);
            await AddLogAsync(UserIdentity.FullName + ": xóa " + id, DOITUONG.MENUMANAGERS, (int)ActionLogs.Delete, (int)StatusLogs.Success);
            return Ok(true);
        }

        //multiple delete 
        [CustomAuthorize("menumanager", PrivilegeList.Delete)]
        [HttpDelete]
        public async Task<IActionResult> Deletes(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var MenuManagerIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                await BaseService.DeleteAsync<MenuManager, int>(MenuManagerIds);
                await AddLogAsync(UserIdentity.FullName + ": xóa danh sách " + ids, DOITUONG.MENUMANAGERS, (int)ActionLogs.Delete, (int)StatusLogs.Success);
                return Ok(true);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("selectmenu")]
        public async Task<IActionResult> GetSelectMENU()
        {
            lstALL = new List<MenuManagerGridDto>();
            MenuManagerGridPaging paging = new MenuManagerGridPaging();
            paging.ItemsPerPage = 0;
            var predicates = paging.GetPredicates();
            lstALL = BaseService.FilterPaged<MenuManager, MenuManagerGridDto>(paging, predicates.ToArray()).Data;
            getMenuCon(0, lstALL, "");
            return Ok(new PagingResult<MenuManagerGridDto> { Data = treeView });
        }
        private List<MenuManagerGridDto> treeView = new List<MenuManagerGridDto>();
        private void getMenuCon(int idCha, List<MenuManagerGridDto> lstALL, string startWith)
        {
            List<MenuManagerGridDto> tempMenu = new List<MenuManagerGridDto>();
            if (idCha == 0)
            {
                tempMenu = lstALL.Where(x => (x.ParentId == null)).OrderBy(y => y.Stt).ToList();
            }
            else
            {
                tempMenu = lstALL.Where(x => x.ParentId == idCha).OrderBy(y => y.Stt).ToList();
            }
            foreach (var item in tempMenu)
            {
                item.Title = startWith + item.Title;
                treeView.Add(item);
                getMenuCon(item.Id, lstALL, "--");
            }
        }
        List<MenuManagerGridDto> lstALL { get; set; }
        [HttpGet("treemenu")]
        public async Task<IActionResult> GetTREEMENU()
        {
            List<MenuManagerGridDto> treeView = new List<MenuManagerGridDto>();
            lstALL = new List<MenuManagerGridDto>();
            MenuManagerGridPaging param = new MenuManagerGridPaging();
            param.ItemsPerPage = -1;
            param.Page = 1;
            param.IsAllShow = true;
            var predicates = param.GetPredicates();
            lstALL = BaseService.FilterPaged<MenuManager, MenuManagerGridDto>(param, predicates.ToArray()).Data;
            // là admin thì view hết
            if (UserIdentity.IsAdministrator)
            {
                treeView = BuldTreeView(0, lstALL);
            }
            else
            {
                if (!string.IsNullOrEmpty(UserIdentity.UnitId))
                {
                    string[] tempIDs = UserIdentity.UnitId.Split(',');
                    lstALL = lstALL.Where(x => tempIDs.Any(y => x.Groups.Contains(string.Format(",{0},", y)))).ToList();
                    treeView = BuldTreeView(0, lstALL);
                }
            }
            treeView = BuldTreeView(0, lstALL);
            return Ok(new PagingResult<MenuManagerGridDto> { Data=treeView});
        }
        private List<MenuManagerGridDto> BuldTreeView(int idCha, List<MenuManagerGridDto> lstALL)
        {
            List<MenuManagerGridDto> tempMenu = new List<MenuManagerGridDto>();
            if (idCha == 0)
            {
                tempMenu = lstALL.Where(x => (x.ParentId == null)).OrderBy(y => y.Stt).ToList();
            }
            else
            {
                tempMenu = lstALL.Where(x => x.ParentId == idCha).OrderBy(y => y.Stt).ToList();
            }
            foreach (var item in tempMenu)
            {
                item.Childrens = BuldTreeView(item.Id, lstALL);
            }
            return tempMenu;
        }
    }
}
