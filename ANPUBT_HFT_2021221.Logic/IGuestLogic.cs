using System.Collections.Generic;
using ANPUBT_HFT_2021221.Models;

namespace ANPUBT_HFT_2021221.Logic
{
    public interface IGuestLogic
    {
        void Create(Guest guest);
        void Delete(int id);
        IEnumerable<string> ItalianoGuestNames();
        IEnumerable<Guest> KirksGuests();
        Guest Read(int id);
        IEnumerable<Guest> ReadAll();
        void Update(Guest guest);
    }
}