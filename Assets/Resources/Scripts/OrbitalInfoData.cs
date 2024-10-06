namespace QuantumQuasars.Data
{
    public class OrbitalInfoData
    {
        public string time;
        public float latitude;
        public float longitude;

        public OrbitalInfoData(string time, float latitude, float longitude)
        {
            this.time = time;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
