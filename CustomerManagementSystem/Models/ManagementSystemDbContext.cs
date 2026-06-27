using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Models;

public partial class ManagementSystemDbContext : DbContext
{
    public ManagementSystemDbContext()
    {
    }

    public ManagementSystemDbContext(DbContextOptions<ManagementSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeLeaf> EmployeeLeaves { get; set; }

    public virtual DbSet<EmployeePayroll> EmployeePayrolls { get; set; }

    public virtual DbSet<Leaf> Leaves { get; set; }

    public virtual DbSet<LeaveStatus> LeaveStatuses { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<PayrollStatus> PayrollStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubModule> SubModules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserModulePermission> UserModulePermissions { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=CustomerManagementSystem;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69261CBB11C02B");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceDate).HasColumnType("datetime");
            entity.Property(e => e.BreakIn).HasColumnType("datetime");
            entity.Property(e => e.BreakOut).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkEmployeeCode).HasMaxLength(50);
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.FkEmployeeCodeNavigation).WithMany(p => p.Attendances)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.FkEmployeeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Employee");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A04F494F6EA");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClientType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED299BFF00");

            entity.HasIndex(e => e.DepartmentCode, "UQ__Departme__6EA8896D49C71DAE").IsUnique();

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC349F5C7CEE").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentCode).HasMaxLength(50);
            entity.Property(e => e.DepartmentName).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__Designat__BABD60DEC335CB77");

            entity.HasIndex(e => e.DesignationName, "UQ__Designat__372CDC23B20BC998").IsUnique();

            entity.HasIndex(e => e.DesignationCode, "UQ__Designat__B676DA1FD61AE23A").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DesignationCode).HasMaxLength(50);
            entity.Property(e => e.DesignationName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11B156A028");

            entity.HasIndex(e => e.EmployeeCode, "UQ__Employee__1F642548AB8D386D").IsUnique();

            entity.HasIndex(e => e.FkUserId, "UQ__Employee__5A7DFF8816520C85").IsUnique();

            entity.HasIndex(e => e.Cnic, "UQ__Employee__AA570FD483496A29").IsUnique();

            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Cnic)
                .HasMaxLength(25)
                .HasColumnName("CNIC");
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrentAddress).HasMaxLength(500);
            entity.Property(e => e.EmergencyContactName).HasMaxLength(150);
            entity.Property(e => e.EmergencyContactPhone).HasMaxLength(20);
            entity.Property(e => e.EmergencyContactRelation).HasMaxLength(50);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmploymentType).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName)
                .HasMaxLength(201)
                .HasComputedColumnSql("(([FirstName]+' ')+isnull([LastName],''))", true);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MaritalStatus).HasMaxLength(30);
            entity.Property(e => e.PermanentAddress).HasMaxLength(500);
            entity.Property(e => e.PersonalEmail).HasMaxLength(150);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.ProfileImage).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.FkDepartment).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkDepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Departments");

            entity.HasOne(d => d.FkDesignation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkDesignationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Designations");

            entity.HasOne(d => d.FkUser).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Users");

            entity.HasOne(d => d.ReportingManager).WithMany(p => p.InverseReportingManager)
                .HasForeignKey(d => d.ReportingManagerId)
                .HasConstraintName("FK_Employees_Manager");
        });

        modelBuilder.Entity<EmployeeLeaf>(entity =>
        {
            entity.HasKey(e => e.EmployeeLeaveId).HasName("PK__Employee__56D0D486DFB7CBAD");

            entity.Property(e => e.ApprovedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.FkEmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FkLeaveStatusId).HasDefaultValue(1);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.FkEmployeeCodeNavigation).WithMany(p => p.EmployeeLeaves)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.FkEmployeeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeLeaves_EmployeeCode");

            entity.HasOne(d => d.FkLeaveStatus).WithMany(p => p.EmployeeLeaves)
                .HasForeignKey(d => d.FkLeaveStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeLeaves_LeaveStatus");

            entity.HasOne(d => d.FkLeaves).WithMany(p => p.EmployeeLeaves)
                .HasForeignKey(d => d.FkLeavesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeLeaves_Leaves");
        });

        modelBuilder.Entity<EmployeePayroll>(entity =>
        {
            entity.HasKey(e => e.PayrollId);

            entity.ToTable("EmployeePayroll");

            entity.Property(e => e.Allowances).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bonuses).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deductions).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FkEmployeeCode).HasMaxLength(50);
            entity.Property(e => e.NetSalary)
                .HasComputedColumnSql("((([BasicSalary]+[Allowances])+[Bonuses])-[Deductions])", true)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.FkEmployeeCodeNavigation).WithMany(p => p.EmployeePayrolls)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.FkEmployeeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeePayroll_Employees");

            entity.HasOne(d => d.FkPayrollStatus).WithMany(p => p.EmployeePayrolls)
                .HasForeignKey(d => d.FkPayrollStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeePayroll_PayrollStatus");
        });

        modelBuilder.Entity<Leaf>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__Leaves__796DB9593BEC5E65");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LeaveName).HasMaxLength(100);
        });

        modelBuilder.Entity<LeaveStatus>(entity =>
        {
            entity.HasKey(e => e.LeaveStatusId).HasName("PK__LeaveSta__75EE81FA104E5C0D");

            entity.ToTable("LeaveStatus");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__Login__4DDA2818FF836E75");

            entity.ToTable("Login");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UserEmail).HasMaxLength(100);

            entity.HasOne(d => d.Fkuser).WithMany(p => p.Logins)
                .HasForeignKey(d => d.FkuserId)
                .HasConstraintName("FK__Login__FKUserId__49C3F6B7");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK__Modules__2B7477A7E223CE8A");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IconClass).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModuleName).HasMaxLength(100);
        });

        modelBuilder.Entity<PayrollStatus>(entity =>
        {
            entity.ToTable("PayrollStatus");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A5E83C608");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61608A05A00E").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubModule>(entity =>
        {
            entity.HasKey(e => e.SubModuleId).HasName("PK__SubModul__79BB06327E9B9CAA");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IconClass).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RouteUrl).HasMaxLength(300);
            entity.Property(e => e.SubModuleName).HasMaxLength(100);

            entity.HasOne(d => d.FkModule).WithMany(p => p.SubModules)
                .HasForeignKey(d => d.FkModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubModules_Modules");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C55F531CB");

            entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF83F3F7A0E").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.ForgetPassword).HasDefaultValue(false);
            entity.Property(e => e.IsActive).HasDefaultValue(false);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UserEmail).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.FkClient).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkClientId)
                .HasConstraintName("FK__Users__FkClientI__1F98B2C1");
        });

        modelBuilder.Entity<UserModulePermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__UserModu__EFA6FB2F36D00FA6");

            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkEmployeeCode).HasMaxLength(50);
            entity.Property(e => e.IsAssigned).HasDefaultValue(true);

            entity.HasOne(d => d.FkEmployeeCodeNavigation).WithMany(p => p.UserModulePermissions)
                .HasPrincipalKey(p => p.EmployeeCode)
                .HasForeignKey(d => d.FkEmployeeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserModulePermissions_Employees");

            entity.HasOne(d => d.FkModule).WithMany(p => p.UserModulePermissions)
                .HasForeignKey(d => d.FkModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserModulePermissions_Modules");

            entity.HasOne(d => d.FkSubModule).WithMany(p => p.UserModulePermissions)
                .HasForeignKey(d => d.FkSubModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserModulePermissions_SubModules");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A5570C57088");

            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.FkRoleId).HasColumnName("FkRoleID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

            entity.HasOne(d => d.FkRole).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.FkRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__FkRol__1AD3FDA4");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__FkUse__19DFD96B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
