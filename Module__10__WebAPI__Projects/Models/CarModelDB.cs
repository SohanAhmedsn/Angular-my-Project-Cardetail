using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module__10__WebAPI__Projects.Models
{
    public class CarDetail
    {
        [Key]
        public int CarDetailId { get; set; }
        [Required,StringLength(50)]
        public string CarName { get; set; } = default!;
        [Required,DataType(DataType.Date)]
        public DateTime LaunchDate { get; set; } = DateTime.Today;
        [Required,StringLength(50)] 
        public string CarType { get; set; }= default!;
        [Required,Column(TypeName ="money"),DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        [Required,StringLength(100)]
        public string CarModel { get; set; } = default!;
        public virtual ICollection<PartDetail>? PartDetails { get; set; }=new List<PartDetail>();
        public virtual ICollection<CompanyDetail>? CompanyDetails { get; set;} =new List<CompanyDetail>();

    }
    public class PartDetail
    {
        public int PartDetailId { get; set; }
        [Required,StringLength(50)]
        public string PartsName { get; set; } = default!;
        [Required,Column(TypeName ="money"),DataType(DataType.Currency)]
        public decimal PartsPrice { get; set; }
        [Required,DataType(DataType.Date)]
        public DateTime PartsMFG { get; set; }
        [Required, ForeignKey("CarDetail")]
        public int CarDetailId { get; set; }
        public virtual CarDetail? CarDetail { get; set; }=default!;
    }
    public class CompanyDetail
    {
        public int CompanyDetailId { get; set; }
        [Required,StringLength(50)] 
        public string CompanyName { get; set; }= default!;
        public int CompanyRank { get; set; }
        [Required, StringLength(100)]
        public string CompanyInformation { get; set; } = default!;
        [Required,ForeignKey("CarDetail")]
        public int CarDetailId { get; set; }
        public virtual CarDetail? CarDetail { get; set; } = default!;
    }
    public class CarInformationDbContext: DbContext
    {
        public CarInformationDbContext(DbContextOptions<CarInformationDbContext> option) : base(option)
        {

        }
        public DbSet<CarDetail> CarDetails { get; set; } = default!;
        public DbSet<PartDetail> PartDetails { get; set; } = default!;
        public DbSet<CompanyDetail> CompanyDetails { get; set; } = default!;
        
    }
}
