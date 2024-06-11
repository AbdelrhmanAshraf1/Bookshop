using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [DisplayName("Publisher")]
        // Data Validation Attribute
        [Required(ErrorMessage = "You have to provide a valid name.")]                    // SQL Server -> Not null
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20 characters.")]               // MaxLength -> nvarchar(20)
        public string Name { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }


        //Navigation Property
        [ValidateNever]
        public List<Book> Books { get; set; }


    }
}
