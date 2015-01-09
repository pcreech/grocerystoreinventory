using GroceryStoreInventory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.ViewModels.ReceivingItems
{
    public class ReceivingItemViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StoreItemId { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(4)]
        public string Invoice { get; set; }
        [Required]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}", ApplyFormatInEditMode=true)]
        public DateTime DateReceived { get; set; }
        [Required]
        public int QuantityReceived { get; set; }

        public StoreItem StoreItem { get; set; }

    }
}