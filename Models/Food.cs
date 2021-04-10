using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Freezer_MVC.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        [Display(Name = "Was")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        [Display(Name = "Menge")]
        public string Amount { get; set; }

        [Required]
        [Display(Name = "Wann")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateIn { get; set; }                // wann wurde es eingebucht

        [Required]
        [Display(Name="Haltbarkeit")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime BestBeforeDate { get; set; }        // Haltbarkeit

        [ForeignKey("FoodSupplier")]
        [Display(Name = "Wo gekauft")]
        public int FoodSupplierId { get; set; }

        [ForeignKey("FoodGroup")]
        [Display(Name = "Gruppe")]
        public int FoodGroupId { get; set; }

        [Display(Name = "Gruppe")]
        public virtual FoodGroup FoodGroup { get; set; }

        [Display(Name = "Wo gekauft")]
        public virtual FoodSupplier FoodSupplier { get; set; }
    }
}
