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
      entity.HasKey(e => e.user_id);
      entity.Property(e => e.name).IsRequired().HasMaxLength(50);
      entity.Property(e => e.mima).IsRequired().HasMaxLength(20);
      entity.Property(e => e.email).IsRequired().HasMaxLength(200);

      entity.HasMany(e => e.user_func_group_rels)
      .WithOne(e => e.user)
      .HasForeignKey(e => e.user_id);
    });

    modelBuilder.Entity<TFunction>(entity =>
    {
      entity.HasKey(e => e.func_id);
      entity.Property(e => e.name).IsRequired().HasMaxLength(50);
      entity.Property(e => e.description).IsRequired().HasMaxLength(100);
      entity.Property(e => e.url).IsRequired().HasMaxLength(100);
      entity.Property(e => e.create_at).IsRequired();

      entity.HasMany(e => e.func_group_rels)
      .WithOne(e => e.func)
      .HasForeignKey(e => e.func_id);
    });

    modelBuilder.Entity<TFuncGroup>(entity =>
    {
      entity.HasKey(e => e.func_group_id);
      entity.Property(e => e.name).IsRequired().HasMaxLength(20);


      entity.HasMany(e => e.user_func_group_rels)
      .WithOne(e => e.func_group)
      .HasForeignKey(e => e.user_id);

      entity.HasMany(e => e.func_group_rels)
      .WithOne(e => e.func_group)
      .HasForeignKey(e => e.func_group_id);
    });

    modelBuilder.Entity<TFuncGroupRel>(entity =>
    {
      entity.HasKey(e => new { e.func_id, e.func_group_id });
    });

    modelBuilder.Entity<TUserFuncGroupRel>(entity =>
    {
      entity.HasKey(e => new { e.user_id, e.func_group_id });
    });

  }
}

