using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Management.Data.Entities
{
    public class Maintenance
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public string CarName { get; set; }
        public Car Car { get; set; }
        public string ServiceType { get; set; }
        public DateTime ScheduledDate { get; set; }
        [ForeignKey(nameof(Garage))]
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public Garage Garage { get; set; }

    }
}
