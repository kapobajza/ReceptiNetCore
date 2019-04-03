using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.Models
{
    [Table("ErrorLogging")]
    public class ErrorLogging
    {
        public int ErrorLoggingId { get; set; }
        public string Path { get; set; }
        public string Message { get; set; }
        public int? StatusCode { get; set; }
        public string StackTrace { get; set; }
        public string OriginalBasePath { get; set; }
        public string OriginalQueryString { get; set; }
        public DateTime Date { get; set; }
    }
}
