using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h2oAPI.Data;

public interface IAuthRepository
{
     int Register(string username, string password);
}
