using Microsoft.EntityFrameworkCore;
using NoteTaker.Models.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

public class ApplicationDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Note> Notes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // postgres now
        var sqlNow = "current_timestamp";
        modelBuilder.Entity<Author>()
            .Property(a => a.DateCreated)
            .HasDefaultValueSql(sqlNow);

        modelBuilder.Entity<Author>()
            .Property(a => a.DateUpdated)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql(sqlNow);

        modelBuilder.Entity<Note>()
            .Property(n => n.DateCreated)
            .HasDefaultValueSql(sqlNow);

        modelBuilder.Entity<Note>()
            .Property(n => n.DateUpdated)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql(sqlNow);

        base.OnModelCreating(modelBuilder);
    }
}