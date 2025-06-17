namespace MegaMinds_Practical.Model
{
    public class ObservationDataModel
    {
        public int Id { get; set; }
        public int ObservationId { get; set; }
        public DateTime SamplingTime { get; set; }

        public virtual List<ObservationPropertiesModel> Properties { get; set; } = new List<ObservationPropertiesModel>();

    }
}
