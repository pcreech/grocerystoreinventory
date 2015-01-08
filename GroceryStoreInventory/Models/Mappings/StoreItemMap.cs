using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Models.Mappings
{
    public class StoreItemMap : EntityTypeConfiguration<StoreItem>
    {
        public StoreItemMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(25);
            Property(t => t.Description)
                .IsMaxLength();
            Property(t => t.Sku)
                .IsRequired()
                .HasMaxLength(25);
            Property(t => t.Brand)
                .IsRequired()
                .HasMaxLength(25);
            Property(t => t.Quantity).IsRequired();

            

        }
    }
}