using Bulky.Models.DomainModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.DomainModel
{
    public class Product
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [DisplayName("List Price")]
        [Range(200, 1000)]
        public double ListPrice { get; set; }


        [Required]
        [DisplayName("Price for 1-50")]
        [Range(200, 1000)]
        public double Price { get; set; }

        [Required]
        [DisplayName("Price for 50")]
        [Range(200, 1000)]
        public double Price50 { get; set; }

        [Required]
        [DisplayName("Price for 100")]
        [Range(200, 1000)]
        public double Price100 { get; set; }
        
        public Guid CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        //[ValidateNever]
        public Category Category { get; set; }
        //[ValidateNever]
        public string ImageUrl { get; set; }
    }
}
