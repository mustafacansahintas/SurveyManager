using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Response:BaseEntities
    {
     
        public bool IsMarked { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [StringLength(200)]
        public string Comment { get; set; }

        public virtual List<_User> user { get; set; }

        public virtual Question question { get; set; }

        public virtual List<UserResponse> _userresponse { get; set; }

    }
}
