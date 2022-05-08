using System.Linq;
using ANPUBT_HFT_2021221.Models;

namespace ANPUBT_HFT_2021221.Repository
{
    public interface IGuestRepository
    {
        void Create(Guest rest);
        void Delete(int id);
        Guest Read(int id);
        IQueryable<Guest> ReadAll();
        void Update(Guest rest);
    }
}