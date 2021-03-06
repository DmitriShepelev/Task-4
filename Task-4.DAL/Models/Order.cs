namespace Task_4.DAL.Models
{
    public class Order : HaveId
    {
        public string PurchaseDate { get; set; }
        public decimal Amount { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int ManagerId { get; set; }
        public  Client Client { get; set; }
        public  Product Product { get; set; }
        public   Manager Manager { get; set; }
    }
}
