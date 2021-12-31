using RedisExample.Context;
using RedisExample.Model;
using RedisExample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExample.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext context;
        private readonly IRedisCacheService redisCacheService;

        public EmployeeRepository(EmployeeContext _context, IRedisCacheService _redisCacheService)
        {
            context = _context;
            redisCacheService = _redisCacheService;
        }
        public Employee GetEmployeeById(int? id)
        {
            var cacheId = redisCacheService.Get<Employee>(id.ToString());
            if (cacheId != null) return cacheId;
            else
            {
                Employee employee = context.Employees.Find(id);
                if (employee != null)
                {
                    redisCacheService.Set<Employee>(id.ToString(), employee);
                }
                return employee;
            }
        }
    }
}
