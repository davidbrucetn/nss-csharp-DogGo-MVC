using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This dog must have a name..")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Unknown or mixed if you can't identify the breed")]
        public string Breed { get; set; }
        public string Notes { get; set; }
        [DisplayName("Picture")]
        public string ImageUrl { get; set; }

        public int OwnerId { get; set; }

    }
}