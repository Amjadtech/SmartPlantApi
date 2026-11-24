namespace SmartPlantApi.Models
{
    public class SensorReading
    {
        public int Id { get; set; }
        // Jordfugtigheden i procent
        public double SoilMoisturePercent { get; set; }
        // Tidspunktet for hvornår målingen blev foretaget
        public DateTime RecordedAt { get; set; }



        public void Validate()
        {
            if (double.IsNaN(SoilMoisturePercent) ||
                double.IsInfinity(SoilMoisturePercent) ||
                SoilMoisturePercent < 0 ||
                SoilMoisturePercent > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(SoilMoisturePercent),
                    "Jordfugtighed skal være mellem 0 og 100.");
            }
        }


    }

}
