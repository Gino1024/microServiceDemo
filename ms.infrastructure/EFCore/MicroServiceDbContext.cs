using Microsoft.EntityFrameworkCore;

public class MicroServiceDbContext : DbContext
{
  public DbSet<TUser> TUser { get; set; }
  public DbSet<TUserFuncGroupRel> TUserFuncGroupRel { get; set; }
  public DbSet<TFuncGroupRel> TFuncGroupRel { get; set; }
  public DbSet<TFuncGroup> TFunctionGroup { get; set; }
  public DbSet<TFunction> TFunction { get; set; }

  public MicroServiceDbContext(DbContextOptions<MicroServiceDbContext> options)
      : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<TUser>(entity =>
    {
      entity.HasKey(e => e.UserId);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
      entity.Property(e => e.Mima).IsRequired().HasMaxLength(20);
      entity.Property(e => e.Email).IsRequired().HasMaxLength(200);

      entity.HasMany(e => e.UserFuncGroupRels)
      .WithOne(e => e.User)
      .HasForeignKey(e => e.UserID);
    });

    modelBuilder.Entity<TFunction>(entity =>
    {
      entity.HasKey(e => e.FuncID);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
      entity.Property(e => e.Description).IsRequired().HasMaxLength(100);
      entity.Property(e => e.Url).IsRequired().HasMaxLength(100);
      entity.Property(e => e.CreateAt).IsRequired();

      entity.HasMany(e => e.FuncGroupRels)
      .WithOne(e => e.Func)
      .HasForeignKey(e => e.FuncID);
    });

    modelBuilder.Entity<TFuncGroup>(entity =>
    {
      entity.HasKey(e => e.FuncGroupID);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(20);


      entity.HasMany(e => e.UserFuncGroupRels)
      .WithOne(e => e.FuncGroup)
      .HasForeignKey(e => e.UserID);

      entity.HasMany(e => e.FuncGroupRels)
      .WithOne(e => e.FuncGroup)
      .HasForeignKey(e => e.FuncGroupID);
    });

    modelBuilder.Entity<TFuncGroupRel>(entity =>
    {
      entity.HasKey(e => new { e.FuncID, e.FuncGroupID });
    });

    modelBuilder.Entity<TUserFuncGroupRel>(entity =>
    {
      entity.HasKey(e => new { e.UserID, e.FuncGroupID });
    });

  }
}

