using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace StudyGuide.DataAccess
{
    public class DbConnectionStringProvider
    {
        private static readonly object padlock = new object();
        private static DbConnectionStringProvider instance = null;
        public string ConnectionString { get; private set; }
        public DbConnectionStringProvider() 
        {
            // Initialize the connection string from configuration
            ConnectionString = ConfigurationManager.ConnectionStrings["StudyGuide-admin"].ConnectionString;

        }

        public static DbConnectionStringProvider Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DbConnectionStringProvider();
                    }
                    return instance;
                }
            }
        }
    }
}
