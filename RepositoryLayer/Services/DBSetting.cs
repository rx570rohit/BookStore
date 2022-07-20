using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class DBSetting :IDBSetting

    { 
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string _authMechanism { get; set; }
    public string _authDbName { get; set; }

     public string _host { get; set; }
     public Int32 _port { get; set; }

     public string _userName { get; set; }  
     public string _password { get; set; }  
     public bool _userTls { get; set; } 

    }
    public interface IDBSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        public string _authMechanism { get; set; }
        public string _authDbName { get; set; }

        public string _host { get; set; }
        public Int32 _port { get; set; }

        public string _userName { get; set; }
        public string _password { get; set; }
        public bool _userTls { get; set; }
    }
}
