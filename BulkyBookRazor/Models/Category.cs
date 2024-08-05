using System.ComponentModel.DataAnnotations;

namespace BulkyBookRazor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Category Name")]
        [MinLength(2, ErrorMessage = "Minimun length for category name should be 4")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Display order cannot be empty")]
        [Display(Name = "Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
