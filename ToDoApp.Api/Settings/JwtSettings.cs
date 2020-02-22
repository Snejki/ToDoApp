using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Api.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public int ExpiryTime { get; set; }
        public string Issuer { get; set; }
    }
}
