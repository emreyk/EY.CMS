using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EY.CMS.CORE.Models
{
    public class ErrorLog:BaseEntity
    {
        public string Path { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Date { get; set; } = new DateTime();
        
    }
}
