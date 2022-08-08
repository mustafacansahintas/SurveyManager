using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Question:BaseEntities
    {
        [Required,MinLength(20),MaxLength(200)]
        public string QuestionText { get; set; }

        public bool IsWritten { get; set; }

        public bool IsMultiSelect { get; set; }

        [Required]
        public int SurveyId { get; set; }

        public virtual Survey survey { get; set; }

    }

 
}
