using BlazorTemplate.api.AuditTrail;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.api.Repository.Autdit
{
   public interface IAuditRepo
    {
        IEnumerable<Audit> GetAuditLogs();
        
        Task  AddAuditLog(Audit auditEntry);
    }
}
