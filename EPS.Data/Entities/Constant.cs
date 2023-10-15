using EPS.Utils.Repository.Audit;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{

    public enum PrivilegeList
    {
        [Description("Thêm mới")]
        Add = 1,
        [Description("Sửa")]
        Edit = 2,
        [Description("Xóa")]
        Delete = 3,
        [Description("Phê duyệt")]
        Approved = 4,
        [Description("Quyền chức năng")]
        Permission = 5,
    }
    public enum StatusLogs
    {
        [Description("Thành công")]
        Success = 1,
        [Description("Thất bại")]
        Error = 2,
    }
    public enum ActionLogs
    {
        [Description("Tạo mới")]
        Add = 1,
        [Description("Lưu nháp")]
        Draft = 2,
        [Description("Đồng bộ")]
        Sync = 3,
        [Description("Import")]
        Import = 4,
        [Description("Sửa")]
        Edit = 5,
        [Description("Xóa")]
        Delete = 6,
        [Description("Export")]
        Export = 7,
        [Description("Khóa")]
        Lock = 8,
        [Description("Mở khóa")]
        Unlock = 9,
        [Description("Phê duyệt")]
        Approved = 10,
        [Description("Từ chối")]
        Pending = 11,
        [Description("Ẩn")]
        Hide,
        [Description("Hiện")]
        Show,
        [Description("Đăng nhập")]
        Login,
        [Description("Truy cập trang web")]
        Access,
    }
    public enum DOITUONG
    {
        [Description("Tài khoản")]
        USERS = 1,
        [Description("Nhóm người dùng")]
        GROUPS = 2,
        [Description("Đối tượng")]
        ROLES = 3,
        [Description("Menu hệ thống")]
        MENUMANAGERS = 4,
        [Description("Tài liệu")]
        DOCUMENT = 5,
        [Description("Login")]
        Login,
        [Description("Access")]
        Access,
        [Description("Change")]
        Change,
        [Description("Cấu hình excel")]
        CONFIGEXCEL,
        [Description("Tài liệu hướng dẫn sử dụng")]
        HUONGDANSUDUNG,
        [Description("Phông")]
        PHONG,
        [Description("Bảng mẫu")]
        FORMMAU,
        [Description("Quyền")]
        PERMISSION,
        [Description("Đơn vị cho thuê")]
        DONVICHOTHUE,
        [Description("Đơn vị vận hành tòa nhà")]
        DONVIVANHANHTOANHA,
        [Description("Trạng thái hồ sơ")]
        TRANGTHAIHSPL,
        [Description("Nguồn gốc hình thành")]
        NGUONGOC,
        [Description("Tình hình khai thác sử dụng")]
        TINHHINHKHAITHAC,
        [Description("Loại hồ sơ")]
        LOAIHOSO,
        [Description("Đơn vị nội bộ")]
        DONVINOIBO,
        [Description("Loại tài sản")]
        LOAITAISAN,
        [Description("Nhóm tài sản")]
        NHOMTAISAN,
        [Description("Địa bàn")]
        DIADIEM,
        [Description("Hiện trạng")]
        HIENTRANG,
        [Description("Thông báo")]
        NOTIFICATION,
        [Description("Phòng ban")]
        PHONGBAN,
        [Description("Phương án sử dụng được phê duyệt")]
        PHUONGANSD,
        [Description("Cấp công trình")]
        CAPCONGTRINH,
        [Description("Thời điểm cổ phần hóa")]
        THOIDIEMCPH,
        [Description("Thông báo đơn vị")]
        THONGBAODONVI,
        [Description("Hình thức thanh lý")]
        HINHTHUCTHANHLY,
        [Description("Loại biến động")]
        LOAIBIENDONG,
        [Description("Phương án sản xuất được phê duyệt")]
        PHUONGANSX,
        [Description("Tình trạng phê duyệt phương án sản xuẩt")]
        TINHTRANGPASX,
        [Description("Lý do biến động")]
        LYDOBIENDONG,
        [Description("Mục đích sử dụng")]
        MUCDICHSUDUNG,
        [Description("Trạng thái sử dụng")]
        TRANGTHAISUDUNG,
        [Description("Thời hạn sử dụng")]
        THOIHANSUDUNG,
        [Description("Quản lý hồ sơ tài sản")]
        HOSOTAISAN,
        [Description("Danh mục tài sản")]
        ASTAISAN,
        [Description("Bảng liên kết giữa hồ sơ tài sản và tài sản")]
        REFHOSOTAISAN,
        [Description("Bảng tạo liên kết giữa nhà và đất")]
        REFNHATRENDAT,
        [Description("Biến động tài sản đất")]
        HIDAT,
        [Description("Biến động tài sản nhà")]
        HINHA,
        [Description("Quản lý sửa chữa bảo dưỡng")]
        SUACHUABAODUONG,
        [Description("Quản lý sửa chữa bảo dưỡng")]
        VANHANHTOANHA,
        [Description("Quản lý sửa chữa bảo dưỡng")]
        KIEMTRAVANHANH,
        //{rendercodekhongxoa}































    }
}
