using HRMS.Attendance.AggregrateRoot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Attendance.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        IQueryable<T> GetQueryable(); // Add this method
        Task<IEnumerable<ManageAttendence>> GetAttendancesForEmployee(int employeeId);
        Task<IEnumerable<Holiday>> GetAllHolidays();
    }
}
