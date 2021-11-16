using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TempaWeb.api.AuditTrail;
using TempaWeb.api.Helpers.Resolvers;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TempaWeb.api.Context
{

    public class BlazorDbContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _context;

        public BlazorDbContext(DbContextOptions options, IHttpContextAccessor context)
            : base(options)
        {
     
            _context = context;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public virtual async Task<int> SaveChangesAsync(bool AuditEntity = true)
        {
            if (AuditEntity) OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync();
            return result;
        }

        public DbSet<Audit> AuditLogs { get; set; }
        //   public DbSet<Product> Products { get; set; }



        private void OnBeforeSaveChanges()
        {
            var User =  (User)_context.HttpContext.Items["User"];

            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = User.Id;
                auditEntry.BrowserInfo = _context.HttpContext.Request.Headers["User-Agent"].ToString();
                auditEntry.HttpMethod = _context.HttpContext.Request.Method;
                auditEntry.AreaAccessed = _context.HttpContext.Request.Path.Value;
                auditEntry.TraceId = _context.HttpContext.TraceIdentifier;
                auditEntry.UserName = User.UserName;

                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
    }
}
