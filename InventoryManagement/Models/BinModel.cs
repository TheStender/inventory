using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class BinModel
    {
        [Display(Name = "Bin ID")]
        [Range(0, 9999999, ErrorMessage = "Please enter a valid bin number(1-9999999)")]
        [Required(ErrorMessage = "Please enter a Bin ID")]
        public int BinID { get; set; }

        [Display(Name = "Bin Name")]
        [Required(ErrorMessage = "Please enter a Bin Name")]
        public string BinName { get; set; }
    }
}