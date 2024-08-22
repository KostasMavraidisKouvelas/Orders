using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Models;

namespace Orders.DataAccess.Configuration
{
    public class ProductSourceConfiguration: IEntityTypeConfiguration<ProductSource>
    {
        public void Configure(EntityTypeBuilder<ProductSource> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c=>c.Id).ValueGeneratedOnAdd();
            builder.HasData(
                new List<ProductSource>
                {
                    new ProductSource
                    {
                        Id = 1,
                        Domain = "https://fakestoreapi.com/products/",
                        IsActive = true
                    }
                });
        }
    }
}
