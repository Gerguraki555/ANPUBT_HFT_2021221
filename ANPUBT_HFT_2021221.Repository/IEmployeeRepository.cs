using System.Linq;
using ANPUBT_HFT_2021221.Models;

namespace ANPUBT_HFT_2021221.Repository
{
    public interface IEmployeeRepository
    {
        void Create(Employee e);
        void Delete(int id);
        Employee Read(int id);
        IQueryable<Employee> ReadAll();
        void Update(Employee e);
    }
}