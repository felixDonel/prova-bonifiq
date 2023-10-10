using ProvaPub.Models.Interfaces;

namespace ProvaPub.Models
{
	public class Customer : IEntity
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}
