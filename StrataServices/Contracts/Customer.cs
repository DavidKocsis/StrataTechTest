namespace StrataServices.Contracts
{
    public class Customer
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public CustomerType Type { get; set; }

        public decimal TwelveMonthTotal { get; set; }
    }

    public enum CustomerType
    {
        Standard,
        Silver,
        Gold
    }
}
