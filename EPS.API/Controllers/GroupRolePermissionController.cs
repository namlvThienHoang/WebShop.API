using EPS.API.Helpers;
using EPS.API.Models;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.GroupRolePermission;
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
    [Route("api/grouprolepermission")]
    [Authorize]
    public class GroupRolePermissionController : BaseController
    {
        private AuthorizationService _userService;

        public GroupRolePermissionController(AuthorizationService userService)
        {
            _userService = userService;
        }

        //list all
        //[CustomAuthorize(PrivilegeList.ViewGroupRolePermission, PrivilegeList.ManageGroupRolePermission)]
        [HttpGet]
        public async Task<IActionResult> GetLists([FromQuery] GroupRolePermissionGridPaging pagingModel)
        {
            var predicates = pagingModel.GetPredicates();
            return Ok(await BaseService.FilterPagedAsync<GroupRolePermission, GroupRolePermissionGridDto>(pagingModel, predicates.ToArray()));
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetListPermissions([FromQuery] GroupRolePermissionGridPaging pagingModel)
        {
            var predicates = pagingModel.GetPredicates();
            var result = await BaseService.FilterPagedAsync<GroupRolePermission, GroupRolePermissionGridDto>(pagingModel, predicates.ToArray());
            GroupRolePermissionAddDto oGroupRolePer = new GroupRolePermissionAddDto();
            oGroupRolePer.GroupId = pagingModel.GroupId;
            oGroupRolePer.RoleId = pagingModel.RoleId;
            if (result.Data.Count > 0)
            {
                foreach (var item in result.Data)
                {
                    if (item.PermissionCode.Equals("01"))
                    {
                        oGroupRolePer.Act01 = item.Value>0?true:false;
                    }
                    if (item.PermissionCode.Equals("02"))
                    {
                        oGroupRolePer.Act02 = item.Value > 0 ? true : false;
                    }
                    if (item.PermissionCode.Equals("03"))
                    {
                        oGroupRolePer.Act03 = item.Value > 0 ? true : false;
                    }
                    if (item.PermissionCode.Equals("04"))
                    {
                        oGroupRolePer.Act04 = item.Value > 0 ? true : false;
                    }
                    if (item.PermissionCode.Equals("05"))
                    {
                        oGroupRolePer.Act05 = item.Value > 0 ? true : false;
                    }
                }
            }
            return Ok(oGroupRolePer);
        }

        //detail
        //[CustomAuthorize(PrivilegeList.ViewGroupRolePermission, PrivilegeList.ManageGroupRolePermission)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await BaseService.FindAsync<GroupRolePermission, GroupRolePermissionDetailDto>(id));
        }

        //create
        //[CustomAuthorize(PrivilegeList.ManageGroupRolePermission)]
        [HttpPost]
        public async Task<IActionResult> Create(GroupRolePermissionCreateDto GroupRolePermissionCreateDto)
        {
            await BaseService.CreateAsync<GroupRolePermission, GroupRolePermissionCreateDto>(GroupRolePermissionCreateDto);
            return Ok();
        }

        [HttpPut("updateper")]
        public async Task<IActionResult> UpdatePermission(GroupRolePermissionAddDto oGroupRolePermissionAddDto)
        {
            if (oGroupRolePermissionAddDto.GroupId > 0 && oGroupRolePermissionAddDto.RoleId > 0)
            {

                //Thêm
                var pagingModelper1 = new PermissionGridPaging() { Code = "01" };
                var predicatesPer1 = pagingModelper1.GetPredicates();
                var oPermission1 = await BaseService.FilterPagedAsync<Permission, PermissionGridDto>(pagingModelper1, predicatesPer1.ToArray());

                var pagingModel1 = new GroupRolePermissionGridPaging() { PermissionId = oPermission1.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId };
                var predicates1 = pagingModel1.GetPredicates();
                var result1 = await BaseService.FilterPagedAsync<GroupRolePermission, GroupRolePermissionGridDto>(pagingModel1, predicates1.ToArray());
                if(result1.Data.Count>0)
                {
                    GroupRolePermissionUpdateDto oUpdate1 = new GroupRolePermissionUpdateDto { Id = result1.Data.FirstOrDefault().Id, PermissionId = oPermission1.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act01==true?1:0 };
                    await BaseService.UpdateAsync<GroupRolePermission, GroupRolePermissionUpdateDto>(result1.Data.FirstOrDefault().Id, oUpdate1);

                }                    
                else
                {
                    GroupRolePermissionCreateDto oCreate1 = new GroupRolePermissionCreateDto() { PermissionId = oPermission1.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value= oGroupRolePermissionAddDto.Act01 == true ? 1 : 0 };
                    await BaseService.CreateAsync<GroupRolePermission, GroupRolePermissionCreateDto>(oCreate1);
                }
                //Sửa
                var pagingModelper2 = new PermissionGridPaging() { Code = "02" };
                var predicatesPer2 = pagingModelper2.GetPredicates();
                var oPermission2 = await BaseService.FilterPagedAsync<Permission, PermissionGridDto>(pagingModelper2, predicatesPer2.ToArray());
                var pagingModel2 = new GroupRolePermissionGridPaging() { PermissionId = oPermission2.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId };
                var predicates2 = pagingModel2.GetPredicates();
                var result2 = await BaseService.FilterPagedAsync<GroupRolePermission, GroupRolePermissionGridDto>(pagingModel2, predicates2.ToArray());
                if (result2.Data.Count > 0)
                {
                    GroupRolePermissionUpdateDto oUpdate2 = new GroupRolePermissionUpdateDto { Id = result2.Data.FirstOrDefault().Id, PermissionId = oPermission2.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act02 == true ? 1 : 0 };
                    await BaseService.UpdateAsync<GroupRolePermission, GroupRolePermissionUpdateDto>(result2.Data.FirstOrDefault().Id, oUpdate2);

                }
                else
                {
                    GroupRolePermissionCreateDto oCreate2 = new GroupRolePermissionCreateDto() { PermissionId = oPermission2.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act02 == true ? 1 : 0 };
                    await BaseService.CreateAsync<GroupRolePermission, GroupRolePermissionCreateDto>(oCreate2);
                }
                //Xóa
                var pagingModelper3 = new PermissionGridPaging() { Code = "03" };
                var predicatesPer3 = pagingModelper3.GetPredicates();
                var oPermission3 = await BaseService.FilterPagedAsync<Permission, PermissionGridDto>(pagingModelper3, predicatesPer3.ToArray());
                var pagingModel3 = new GroupRolePermissionGridPaging() { PermissionId = oPermission3.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId };
                var predicates3 = pagingModel3.GetPredicates();
                var result3 = await BaseService.FilterPagedAsync<GroupRolePermission, GroupRolePermissionGridDto>(pagingModel3, predicates3.ToArray());
                if (result3.Data.Count > 0)
                {
                    GroupRolePermissionUpdateDto oUpdate3 = new GroupRolePermissionUpdateDto { Id = result3.Data.FirstOrDefault().Id, PermissionId = oPermission3.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act03 == true ? 1 : 0 };
                    await BaseService.UpdateAsync<GroupRolePermission, GroupRolePermissionUpdateDto>(result3.Data.FirstOrDefault().Id, oUpdate3);

                }
                else
                {
                    GroupRolePermissionCreateDto oCreate3 = new GroupRolePermissionCreateDto() { PermissionId = oPermission3.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act03 == true ? 1 : 0 };
                    await BaseService.CreateAsync<GroupRolePermission, GroupRolePermissionCreateDto>(oCreate3);
                }
                //Phê duyệt
                var pagingModelper4 = new PermissionGridPaging() { Code = "04" };
                var predicatesPer4 = pagingModelper4.GetPredicates();
                var oPermission4 = await BaseService.FilterPagedAsync<Permission, PermissionGridDto>(pagingModelper4, predicatesPer4.ToArray());
                var pagingModel4 = new GroupRolePermissionGridPaging() { PermissionId = oPermission4.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId };
                var predicates4 = pagingModel4.GetPredicates();
                var result4 = await BaseService.FilterPagedAsync<GroupRolePermission, GroupRolePermissionGridDto>(pagingModel4, predicates4.ToArray());
                if (result4.Data.Count > 0)
                {
                    GroupRolePermissionUpdateDto oUpdate4 = new GroupRolePermissionUpdateDto { Id = result4.Data.FirstOrDefault().Id, PermissionId = oPermission4.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act04 == true ? 1 : 0 };
                    await BaseService.UpdateAsync<GroupRolePermission, GroupRolePermissionUpdateDto>(result4.Data.FirstOrDefault().Id, oUpdate4);

                }
                else
                {
                    GroupRolePermissionCreateDto oCreate4 = new GroupRolePermissionCreateDto() { PermissionId = oPermission4.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act04 == true ? 1 : 0 };
                    await BaseService.CreateAsync<GroupRolePermission, GroupRolePermissionCreateDto>(oCreate4);
                }
                //Quyền chức năng
                var pagingModelper5 = new PermissionGridPaging() { Code = "05" };
                var predicatesPer5 = pagingModelper5.GetPredicates();
                var oPermission5 = await BaseService.FilterPagedAsync<Permission, PermissionGridDto>(pagingModelper5, predicatesPer5.ToArray());
                var pagingModel5 = new GroupRolePermissionGridPaging() { PermissionId = oPermission5.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId };
                var predicates5 = pagingModel5.GetPredicates();
                var result5 = await BaseService.FilterPagedAsync<GroupRolePermission, GroupRolePermissionGridDto>(pagingModel5, predicates5.ToArray());
                if (result5.Data.Count > 0)
                {
                    GroupRolePermissionUpdateDto oUpdate5 = new GroupRolePermissionUpdateDto { Id = result5.Data.FirstOrDefault().Id, PermissionId = oPermission5.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act05 == true ? 1 : 0 };
                    await BaseService.UpdateAsync<GroupRolePermission, GroupRolePermissionUpdateDto>(result5.Data.FirstOrDefault().Id, oUpdate5);
                }
                else
                {
                    GroupRolePermissionCreateDto oCreate5 = new GroupRolePermissionCreateDto() { PermissionId = oPermission5.Data.FirstOrDefault().Id, RoleId = oGroupRolePermissionAddDto.RoleId, GroupId = oGroupRolePermissionAddDto.GroupId, Value = oGroupRolePermissionAddDto.Act05 == true ? 1 : 0 };
                    await BaseService.CreateAsync<GroupRolePermission, GroupRolePermissionCreateDto>(oCreate5);
                }
                return Ok();
            }
            else
            {
                return BadRequest("Cập nhật quyền không thành công");
            }
        }

        //update
        //[CustomAuthorize(PrivilegeList.ManageGroupRolePermission)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GroupRolePermissionUpdateDto GroupRolePermissionUpdateDto)
        {
            await BaseService.UpdateAsync<GroupRolePermission, GroupRolePermissionUpdateDto>(id, GroupRolePermissionUpdateDto);
            return Ok(true);
        }

        //[CustomAuthorize(PrivilegeList.ManageGroupRolePermission)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await BaseService.DeleteAsync<GroupRolePermission, int>(id);
            return Ok(true);
        }

        //multiple delete 
        //[CustomAuthorize(PrivilegeList.ManageGroupRolePermission)]
        [HttpDelete]
        public async Task<IActionResult> Deletes(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest();
            }
            try
            {
                var GroupRolePermissionIds = ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                await BaseService.DeleteAsync<GroupRolePermission, int>(GroupRolePermissionIds);
                return Ok(true);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
