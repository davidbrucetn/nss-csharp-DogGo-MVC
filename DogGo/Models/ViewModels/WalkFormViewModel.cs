using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models.ViewModels
{
    public class WalkFormViewModel
    {
        public Walk Walk { get; set; }
        public List<Walker> Walkers { get; set; }
        public List<Dog> Dogs { get; set; }


    }
}
