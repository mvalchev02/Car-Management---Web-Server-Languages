using System.ComponentModel.DataAnnotations;

namespace Car_Management.Data.Entities
{
    public class Garage
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public int Capacity { get; set; }
    }
}
