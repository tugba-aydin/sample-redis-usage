using RedisExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExample.Repository
{
   public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int? id);
    }
}
