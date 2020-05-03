using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoStore.API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

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
        [JsonIgnore]
        public IFormFile PictureToUpload { get; set; }
    }
}
