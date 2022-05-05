using System.ComponentModel.DataAnnotations;

namespace CentreAppBlazor.Shared.Domain
{
    public class Expenses
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Cost { get; set; }
        public System.DateTime RegDateTime { get; set; } = System.DateTime.Now;
    }
}
