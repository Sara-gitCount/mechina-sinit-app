namespace WebApplication1.Dto
{
    public class DtoOrders
    {
        public string nameGift { get; set; }
        public int numOfOrders { get; set; } //מספר ההזמנות של מתנה מסוימת
    }

    public class GiftOrdersDto
    {
        public string GiftName { get; set; }
        public List<string> Users { get; set; }
    }
}
