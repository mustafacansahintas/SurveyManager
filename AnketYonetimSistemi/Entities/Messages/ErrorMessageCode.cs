using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Messages
{
    public enum ErrorMessageCode
    {
        
        UsernameOrPassWrong = 152,
        UserNotFound = 153,
        ProfileCouldNotUpdated = 154,
        UserCouldNotRemove = 155,
        UserCouldNotFind = 156,
        UserCouldNotInserted = 157
    }
}
