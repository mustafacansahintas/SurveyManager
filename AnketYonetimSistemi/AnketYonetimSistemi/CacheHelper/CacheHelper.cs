using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace AnketYonetimSistemi.CacheHelper
{
    public class CacheHelper
    {

        public static List<Company> GetCompaniesFromCache() 
        {
            var result = WebCache.Get("category-cache");

            if (result == null)
            {
                CompanyManager cm = new CompanyManager();
                result = cm.List();

                WebCache.Set("company-cache", result, 20, true);
            }

            return result;
        }

        public static void RemoveCompaniesFromCache()
        {
            Remove("company-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }

    }
}