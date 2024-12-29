namespace Car_Management.DTOs
{
    public class UpdateMaintenanceDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ServiceType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int GarageId { get; set; }
    }

}
