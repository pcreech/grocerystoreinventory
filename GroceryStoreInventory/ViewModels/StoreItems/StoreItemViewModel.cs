using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.ViewModels.StoreItems
{
    public class StoreItemViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [RegularExpression("\\d+", ErrorMessage="Digits 0-9 Only")]
        public string Sku { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}