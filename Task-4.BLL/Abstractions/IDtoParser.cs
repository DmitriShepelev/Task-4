using Task_4.DAL.Models;

namespace Task_4.BLL.Abstractions
{
    public interface IDtoParser<TDtoEntity>
    {
        public Client Client { get; set; }
        public Product Product { get; set; }
        public Manager Manager { get; set; }

        public Order Order { get; set; }
        void Parse(TDtoEntity item);
    }
}
