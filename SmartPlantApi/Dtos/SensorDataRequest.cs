namespace SmartPlantApi.Dtos
{
    public class SensorDataRequest
    {
        // Jordfugtigheden i procent
        public double SoilMoisturePercent { get; set; }

        // Lysniveauet i lux
        public double LightLevelLux { get; set; }

        // Tidspunktet for hvornår målingen blev foretaget
        public DateTimeOffset RecordedAt { get; set; }
    }
}
