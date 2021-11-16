using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using TempaWeb.api.Repository.Autdit;
using Microsoft.Extensions.Logging;
using Entities.Models;
using TempaWeb.api.Helpers.Utils;
using Entities.Enums;
using TempaWeb.api.Context;

namespace TempaWeb.api.Helpers.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuditTrailFilter : ActionFilterAttribute, IAsyncActionFilter
    {
         IAuditRepo _auditRepo;
         ILogger<AuditTrailFilter> _logger;
        public override async void  OnActionExecuted(ActionExecutedContext context)
        {
             _auditRepo = context.HttpContext.RequestServices.GetService(typeof(IAuditRepo)) as IAuditRepo;
            var user = (User)context.HttpContext.Items["User"];
            _logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<AuditTrailFilter>)) as ILogger<AuditTrailFilter>;

            string userId = context.HttpContext.User?.Identity?.Name;
                string areaVisited = context.HttpContext.Request.Path.Value;

                _logger.LogInformation($"{userId} visisted {areaVisited}");

                var audit = new Audit();

                audit.HttpMethod = context.HttpContext.Request.Method;
                audit.TraceId = context.HttpContext.TraceIdentifier;
                audit.BrowserInfo = context.HttpContext.Request.Headers["User-Agent"].ToString();
                audit.AreaAccessed = areaVisited;
                audit.UserId = user.Id;
                audit.UserName = user.UserName;
                audit.WorkStation = WorkStationHelper.getWSSignature();
                audit.Ip = WorkStationHelper.GetUserIpAddress();
                audit.DateTime = DateTime.Now;
                audit.Type = AuditType.View.ToString();
                await  _auditRepo.AddAuditLog(audit);

        }

       
    }
}
