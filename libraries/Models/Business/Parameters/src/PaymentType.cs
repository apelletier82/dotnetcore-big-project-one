namespace BigProjectOne.Libraries.Models.Business.Parameters
{
    public class PaymentType : CustomParameter
    {
        public int Value { get; set; }
        public PaymentTypeUnit Unit { get; set; }
        public bool StartFromEndOfTheMonth { get; set; }
    }
}
