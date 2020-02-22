using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Api.Services
{
    public interface IJwthandler
    {
        string CreateToken(Guid userId);
    }
}
