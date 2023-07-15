using System.ComponentModel.DataAnnotations;

namespace FirstProject.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? OwnerId { get; set; }
        public User? Owner { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public float InitialCost { get; set; }
        public PriceLossPeriod priceLossPeriod { get; set; }
        public float PriceLoss { get; set; }
        public float CurrentCost => CalculateCurrentCost();
        private float CalculateCurrentCost()
        {
            int periodDays = GetPeriodDays(priceLossPeriod);
            int ownershipDays = (DateTime.Now - PurchaseDate).Days;
            int periodCount = ownershipDays / periodDays;
            return InitialCost - (PriceLoss * periodCount);
        }

        private int GetPeriodDays(PriceLossPeriod period)
        {
            return period switch
            {
                PriceLossPeriod.Daily => 1,
                PriceLossPeriod.Weekly => 7,
                PriceLossPeriod.Monthly => 30,
                PriceLossPeriod.Yearly => 365,
                _ => throw new ArgumentException("Invalid PriceLossPeriod value."),
            };
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
