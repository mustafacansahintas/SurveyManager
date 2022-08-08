using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Survey:BaseEntities
    {
        [Required, MinLength(10), MaxLength(40)]
        public string SurveyTitle { get; set; }

        [Required]
        public bool IsAnonim { get; set; }

        public bool IsCompletely { get; set; }

        public virtual List<_User> user { get; set; }

        public virtual List<Question> question { get; set; }

        public virtual List<UserSurvey> _usersurvey { get; set; }

    }
}
