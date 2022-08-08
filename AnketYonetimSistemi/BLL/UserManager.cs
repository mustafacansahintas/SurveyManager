using BLL.Abstract;
using BLL.Result;
using Entities;
using Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserManager:ManagerBase<_User>
    {
        public BusinessLayerResult<_User> LoginUser(LoginViewModel data)
        {

            BusinessLayerResult<_User> res = new BusinessLayerResult<_User>();
            res.Result = Find(e => e.UserName == data.UserName && e.Password == data.Password);

            if (res.Result == null)
            {
                
              res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı Adı adresi veya şifre uyuşmuyor.");
                
            }

          

            return res;
        }

      
    }
}
