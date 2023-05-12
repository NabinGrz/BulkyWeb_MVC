using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models.ViewModel
{
    public class AddCategoryRequest
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
