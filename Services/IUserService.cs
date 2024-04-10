using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h2oAPI.Services;

public interface IUserService
{
      public int Register(string username, string password);
}
