using System;
using System.ComponentModel.DataAnnotations;

namespace BilingualBlazor.Models
{
    public class Person
    {
        [Required]
        public bool IsHuman { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(1, 99, ErrorMessage = "Age must be between 1 and 99.")]
        public int Age { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        [Required]
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Male, Female, Other
    }
}