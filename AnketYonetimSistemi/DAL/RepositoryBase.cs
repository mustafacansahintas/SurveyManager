﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RepositoryBase
    {
        protected static DatabaseContext context;
        private static object _lockSync = new object();

        protected RepositoryBase()
        {
            CreateContext();
        }

        public static void CreateContext()
        {
            if (context == null)
            {


                lock (_lockSync)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }

                }
            }
        }

    }
}
