using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWeb.Models.ViewModel
{
    public class UpdateCategoryRequest
    {
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Category Order")]
        [Range(1, 100)]
        public int CategoryOrder { get; set; }
    }
}
