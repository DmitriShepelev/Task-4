using Task_4.Models;

namespace Task_4.BLL.Abstractions
{
    public interface IDtoParser<DTOEntity>
    {
        public Client Client { get; set; }
        public Product Product { get; set; }
        public Manager Manager { get; set; }

        public Order Order { get; set; }
        void Parse(DTOEntity item);
    }
}
