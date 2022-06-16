namespace ParrotsProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Breed : BaseModel
    {
        public Breed()
        {
            this.Parrots = new HashSet<Parrot> ();
        }
        [Required]
        public string Name { get; set; }
        public ICollection<Parrot> Parrots { get; set; }
    }
}
