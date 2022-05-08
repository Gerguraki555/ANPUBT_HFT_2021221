using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANPUBT_HFT_2021221.Models
{

    //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True
    public class Food
    {
        public int Price { get; set; }
        public string Name { get; set; }

    }
}
