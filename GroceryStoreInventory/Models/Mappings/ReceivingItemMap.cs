using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GroceryStoreInventory.Models.Mappings
{
    public class ReceivingItemMap : EntityTypeConfiguration<ReceivingItem>
    {
        public ReceivingItemMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DateReceived)
                .IsRequired();
            Property(t => t.QuantityReceived)
                .IsRequired();
            Property(t => t.Invoice)
                .IsRequired()
                .HasMaxLength(25);

            HasRequired(t => t.StoreItem)
                .WithMany(t => t.ReceivingItems)
                .HasForeignKey(t => t.StoreItemId)
                .WillCascadeOnDelete();
                
        }
    }
}