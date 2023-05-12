using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models.DomainModel
{
    public class Category
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Category Order")]
        [Range(1, 100)]
        public int CategoryOrder { get; set; }
    }
}
