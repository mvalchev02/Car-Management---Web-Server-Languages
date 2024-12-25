﻿namespace Car_Management.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public string LicensePlate { get; set; }
        public List<int> Garages { get; set; }
    }
}
