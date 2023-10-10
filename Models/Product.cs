using ProvaPub.Models.Interfaces;

namespace ProvaPub.Models
{
	public class Product : IEntity
    {
		public int Id { get; set; }	

		public string Name { get; set; }
	}
}
