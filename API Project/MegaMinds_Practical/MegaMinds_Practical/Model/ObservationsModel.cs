namespace MegaMinds_Practical.Model
{
    public class ObservationsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<ObservationDataModel> Datas { get; set; } = new List<ObservationDataModel>();
    }
}
