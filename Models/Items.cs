using ProvaPub.Models.Interfaces;

namespace ProvaPub.Models
{
    public class Items<T> : IEntity where T : class
    {
        public int Id { get; set; }
        public virtual List<T>? Item { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
    }
}
