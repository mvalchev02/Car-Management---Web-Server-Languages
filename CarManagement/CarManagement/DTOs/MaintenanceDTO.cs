namespace Car_Management.DTOs
{
    public class MaintenanceDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string ServiceType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int GarageId { get; set; }
        public string GarageName { get; set; }

    }
}
