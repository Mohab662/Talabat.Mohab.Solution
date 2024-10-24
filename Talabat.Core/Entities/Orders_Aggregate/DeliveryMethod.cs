namespace Talabat.Core.Entities.Orders_Aggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, string delabveryTime, decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = delabveryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
    }
}
