using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }
        public string Ip { get; set; }
        public string WorkStation { get; set; }
        public string ReasonForUpdate { get; set; }
        public string TraceId { get; set; }
        public string HttpMethod { get; set; }
        public string BrowserInfo { get; set; }
        public string AreaAccessed { get; set; }
    }
}
