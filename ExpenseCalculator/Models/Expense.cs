using System.ComponentModel.DataAnnotations;

namespace ExpenseCalculator.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public decimal Value { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
