using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ANPUBT_HFT_2021221.Models
{
    [Table("Restaurant")]
    public class Restaurant
    {
        
        [Required]
        public string RestaurantName { get; set; }

        [NotMapped]
        public IList<Food> Foodlist  { get; set; }
        
        public int Rating { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Restaurant_id{ get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public Restaurant()
        {
            Foodlist = new List<Food>();
            Employees = new HashSet<Employee>();
        }


    }
}
