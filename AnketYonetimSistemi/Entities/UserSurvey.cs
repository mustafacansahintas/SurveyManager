using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserSurvey
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual _User user { get; set; }

        public int SurveyId { get; set; }

        public virtual Survey survey { get; set; }

        public bool IsPerCompletely { get; set; }
    }
}
