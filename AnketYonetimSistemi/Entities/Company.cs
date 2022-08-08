using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Company:BaseEntities
    {
        [Required,MinLength(4),MaxLength(20)]
        public string CompanyName { get; set; }


        public virtual List<_User> user { get; set; }
    }
}
