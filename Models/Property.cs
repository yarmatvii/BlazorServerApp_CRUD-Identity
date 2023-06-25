using System.ComponentModel.DataAnnotations;

namespace FirstProject.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public float InitialCost { get; set; }
        public PriceLossPeriod priceLossPeriod { get; set; }
        public float PriceLoss { get; set; }
        public float CurrentCost => CalculateCurrentCost();
        private float CalculateCurrentCost()
        {
            var periodDays = GetPeriodDays(priceLossPeriod);
            var ownershipDays = (DateTime.Now - PurchaseDate).Days;
            var periodCount = ownershipDays / periodDays;
            return InitialCost - (PriceLoss * periodCount);
        }

        private int GetPeriodDays(PriceLossPeriod period)
        {
            switch (period)
            {
                case PriceLossPeriod.Daily:
                    return 1;
                case PriceLossPeriod.Weekly:
                    return 7;
                case PriceLossPeriod.Monthly:
                    return 30;
                case PriceLossPeriod.Yearly:
                    return 365;
                default:
                    throw new ArgumentException("Invalid PriceLossPeriod value.");
            }
        }
    }

    public enum PriceLossPeriod
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
