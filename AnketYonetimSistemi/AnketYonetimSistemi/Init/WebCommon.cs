using AnketYonetimSistemi.Current_Session;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnketYonetimSistemi.Init
{
    public class WebCommon:ICommon
    {
        public string GetUsername()
        {
            _User user = CurrentSession.user;
            if (user!=null)
            {
                return user.UserName;
            }

            return "system";
        }
    }
}