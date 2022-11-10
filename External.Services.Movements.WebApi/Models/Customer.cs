using System.ComponentModel;
using System.Runtime.Serialization;

namespace External.Services.Movements.WebApi.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
