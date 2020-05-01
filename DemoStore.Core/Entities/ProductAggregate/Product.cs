using DemoStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoStore.Core.Entities.ProductAggregate
{
    public class Product: BaseEntity, IAggregateRoot
    {
        public string Sku { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }


        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int NumberAvailable { get; set; }

        public string PictureUri { get; set; }

    }
}
