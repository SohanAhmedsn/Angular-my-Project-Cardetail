using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Module__10__WebAPI__Projects.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Module__10__WebAPI__Projects.ViewModels
{
    public class CarViewModel
    {
        [Key]
        public int CarDetailId { get; set; }
        [Required, StringLength(50)]
        public string CarName { get; set; } = default!;
        [Required, DataType(DataType.Date)]
        public DateTime LaunchDate { get; set; } = DateTime.Today;
        [Required, StringLength(50)]
        public string CarType { get; set; } = default!;
        [Required, Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        [Required, StringLength(100)]
        public string CarModel { get; set; } = default!;
        public int PartsCount { get; set; }
        public bool CanDelete { get; set; }

    }
}
