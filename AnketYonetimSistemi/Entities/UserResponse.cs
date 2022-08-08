using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual _User user { get; set; }

        public int ResponseId { get; set; }

        public virtual Response response { get; set; }
    }
}
