using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiCRUD.Models;

public partial class WebApiCrudContext : DbContext
{
    public WebApiCrudContext()
    {
    }

    public WebApiCrudContext(DbContextOptions<WebApiCrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07780E47BF");

            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Standard).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
