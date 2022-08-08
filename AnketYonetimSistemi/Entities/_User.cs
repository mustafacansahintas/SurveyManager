using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class _User:BaseEntities
    {
        [Required, StringLength(40)]
        public string Name { get; set; }

        [Required, StringLength(40)]
        public string Surname { get; set; }

        [Required, StringLength(40)]
        public string UserName { get; set; }

        public string Password { get; set; }

        [Required]
        public bool IsManager { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public virtual List<Response> response { get; set; }

        public virtual List<Survey> survey { get; set; }

        public virtual Company company { get; set; }

        public virtual List<UserSurvey> _usersurvey { get; set; }

        public virtual List<UserResponse> _userresponse { get; set; }



    }
}
