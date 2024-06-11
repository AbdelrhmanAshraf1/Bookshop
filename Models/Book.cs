using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookShop.Models
{
    public class Book
    {
        public int Id { get; set; }



        //Foreign Key Property
        [Range(1, double.MaxValue, ErrorMessage = "select a valid Publisher.")]
        [DisplayName("Publisher")]
        public int PublisherId { get; set; }
        //Navigation property
        [ValidateNever]
        public Publisher Publisher { get; set; }

        //Foreign Key Property
        [Range(1, double.MaxValue, ErrorMessage = "select a valid Education Year.")]
        [DisplayName("Education Year")]
        public int EducationYearId { get; set; }
        //Navigation property
        [ValidateNever]
        public EducationYear EducationYear { get; set; }


        //Foreign Key Property
        [Range(1, double.MaxValue, ErrorMessage = "select a valid Subject.")]
        [DisplayName("Subject")]
        public int SubjectId { get; set; }
        //Navigation property
        [ValidateNever]
        public Subject Subject { get; set; }

        public int Price { get; set; }
        public int count { get; set; }


    }
}
