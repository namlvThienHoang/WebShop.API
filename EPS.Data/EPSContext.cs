using EPS.Data.Entities;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace EPS.Data
{
    public partial class EPSContext : DbContext, IDataProtectionKeyContext
    {
        public EPSContext()
        {
        }

        public EPSContext(DbContextOptions<EPSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public virtual DbSet<IdentityClient> IdentityClients { get; set; }
        public virtual DbSet<IdentityRefreshToken> IdentityRefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupUser> GroupUser { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<GroupRolePermission> GroupRolePermission { get; set; }
        public virtual DbSet<MenuManager> MenuManager { get; set; }

	 //{rendercodekhongxoa}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string , you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=SRV84;Database=BCT_QLHGHN;Integrated Security=true;");
                //optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server = SRV84; Database = BASE_FORM; User Id = sa; Password = Ab@123456");
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=DESKTOP-4H2CDN2\\SQLEXPRESS;Database=BIDV;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserPrivilege>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.PrivilegeId });
            //});

            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");

            // for the other conventions, we do a metadata model loop
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.SetTableName(entityType.DisplayName());

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
        }
    }
}
