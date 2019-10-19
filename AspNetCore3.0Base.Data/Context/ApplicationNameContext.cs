using AspNetCore3._0Base.Data.SeedData;
using AspNetCore3._0Base.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AspNetCore3._0Base.Data.Context
{
    public class ApplicationNameContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationNameContext(DbContextOptions<ApplicationNameContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<LogEntry> LogEntry { get; set; }
        public DbSet<LogEntryWebApi> LogEntryWebApi { get; set; }
        public DbSet<MenuPermission> MenuPermission { get; set; }
        public DbSet<Perfil> Perfil { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.SetTableName(entityType.DisplayName());
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //add here the specific configuration for each class
            EntitySeedData.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CREATED_ON") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("UPDATED_ON").IsModified = false;
                    entry.Property("UPDATED_BY").IsModified = false;
                    if (entry.Property("CREATED_ON").CurrentValue == null)
                    {
                        entry.Property("CREATED_ON").CurrentValue = DateTime.UtcNow;
                    }
                    if (entry.Property("CREATED_BY").CurrentValue == null)
                    {
                        entry.Property("CREATED_BY").CurrentValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                    }
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CREATED_ON").IsModified = false;
                    entry.Property("CREATED_BY").IsModified = false;
                    if (entry.Property("UPDATED_ON").CurrentValue == null)
                    {
                        entry.Property("UPDATED_ON").CurrentValue = DateTime.UtcNow;
                    }
                    if (entry.Property("UPDATED_BY").CurrentValue == null)
                    {
                        entry.Property("UPDATED_BY").CurrentValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                    }
                }
            }
            return base.SaveChanges();
        }

    }
}
