Insert into "Roles" ("Name", "Description") values (N'permissions',N'Quyền người dùng');
Insert into "Roles" ("Name", "Description") values (N'groups',N'Nhóm người dùng');
Insert into "Roles" ("Name", "Description") values (N'users',N'Tài khoản người dùng');
Insert into "Roles" ("Name", "Description") values (N'groupuser',N'Nhóm - người dùng');
Insert into "Roles" ("Name", "Description") values (N'menumanager',N'Menu hệ thống');
Insert into "Roles" ("Name", "Description") values (N'configexcel',N'Cấu hình excel');
Insert into "Group" ("Title", "Code") values (N'Quản trị hệ thống','01');

Insert into "Permission" ("Title", "Code") values (N'Thêm mới','01');
Insert into "Permission" ("Title", "Code") values (N'Sửa','02');
Insert into "Permission" ("Title", "Code") values (N'Xóa','03');
Insert into "Permission" ("Title", "Code") values (N'Phê duyệt','04');
Insert into "Permission" ("Title", "Code") values (N'Quyền chức năng','05');

Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Quản trị hệ thống','/-', 1,NULL, ',1,',NULL, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Menu hệ thống','/admin/menumanager', 1,NULL, ',1,',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Tài khoản','/admin/users', 2,NULL, ',1,',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Quyền','/admin/permissions', 3,NULL, '1',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Nhóm người dùng','/admin/groups', 4,NULL, ',1,',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Đối tượng quản lý','/admin/roles', 5,NULL, ',1,',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Tài liệu','/admin/documents', 5,NULL, ',1,',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Cấu hình export excel','/admin/configexcel/1', 7,NULL, ',1,',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Cấu hình import excel','/admin/configexcel/2', 8,NULL, ',1,',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Nhật ký hệ thống','/-', 5,NULL, ',1,',1, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Nhật ký đăng nhập','/admin/logs/login', 1,NULL, ',1,',10, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Nhật ký truy cập','/admin/logs/access', 2,NULL, ',1,',10, 0, 1);
Insert into "MenuManager" ("Title","Url","Stt","Icon","Groups","ParentId","IsBlank","IsShow") values (N'Nhật ký thay đổi','/admin/logs/change', 1,NULL, ',1,',10, 0, 1);



