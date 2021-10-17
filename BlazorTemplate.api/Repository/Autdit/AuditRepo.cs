using BlazorTemplate.api.AuditTrail;
using BlazorTemplate.api.Context;
using BlazorTemplate.api.Repository.Autdit;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.api.Repository
{
    public class AuditRepo : IAuditRepo
    {
        private readonly BlazorDbContext _blazorDbContext;

        public AuditRepo(BlazorDbContext blazorDbContext)
        {
            _blazorDbContext = blazorDbContext;
        }

        public async Task AddAuditLog(Audit auditEntry)
        {
            _blazorDbContext.AuditLogs.Add(auditEntry);
           await  _blazorDbContext.SaveChangesAsync(false);
        }

        public  IEnumerable<Audit> GetAuditLogs()
        {
            var auditLogs = _blazorDbContext.AuditLogs.ToList();
            return auditLogs;
        }

     
    }
}
