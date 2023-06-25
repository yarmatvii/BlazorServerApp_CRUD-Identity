using FirstProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //    protected override void OnModelCreating(ModelBuilder builder)
        //    {
        //        base.OnModelCreating(builder);
        //        builder.ApplyConfiguration(new UserEntityConfiguration());
        //        builder.ApplyConfiguration(new PropertyEntityConfiguration());
        //    }
        //}

        //internal class PropertyEntityConfiguration : IEntityTypeConfiguration<Property>
        //{
        //    public void Configure(EntityTypeBuilder<Property> builder)
        //    {
        //        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        //        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        //        builder.Property(p => p.Type).HasMaxLength(50).IsRequired();
        //        builder.Property(p => p.PurchaseDate).IsRequired();
        //        builder.Property(p => p.InitialCost).HasColumnType("decimal(18,2)").IsRequired();
        //        builder.Property(p => p.priceLossPeriod).IsRequired();
        //        builder.Property(p => p.PriceLoss).HasColumnType("decimal(18,2)").IsRequired();
        //    }
        //}

        //internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
        //{
        //    public void Configure(EntityTypeBuilder<User> builder)
        //    {
        //        builder.Property(u => u.Address).HasMaxLength(100);
        //    }
    }
}