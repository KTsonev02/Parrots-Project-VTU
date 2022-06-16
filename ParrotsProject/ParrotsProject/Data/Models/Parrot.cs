using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParrotsProject.Data.Models
{
    public class Parrot : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Guid BreedId { get; set; }
        public Breed Breed { get; set; }
    }
}
