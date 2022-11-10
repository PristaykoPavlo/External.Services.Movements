using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace External.Services.Movements.WebApi.Models
{
    public class Product
    {
        public int ProductId { get; set; } 
        public string ProductType { get; set; }
        public string ExternalAccount { get; set; }
    }

    public class ProductCustomer
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}
