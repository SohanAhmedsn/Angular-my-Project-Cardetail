using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module__10__WebAPI__Projects.Models;
using Module__10__WebAPI__Projects.ViewModels;

namespace Module__10__WebAPI__Projects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarInformationDbContext db;
        private readonly IWebHostEnvironment env;
        public CarsController(CarInformationDbContext db,IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDetail>>> GetCarDetail()
        {
            return await db.CarDetails.ToListAsync();
        }
        [HttpGet("VM")]
        public async Task<ActionResult<IEnumerable<CarViewModel>>> GetCarInformationShow()
        {
            var data = await db.CarDetails.Include(p => p.PartDetails)
                .Select(c => new CarViewModel
                {
                    CarDetailId = c.CarDetailId,
                    CarName = c.CarName,
                    LaunchDate = c.LaunchDate,
                    CarType = c.CarType,
                    Price = c.Price,
                    IsStock = c.IsStock,
                    CarModel = c.CarModel,
                    PartsCount = c.PartDetails == null ? 0 : c.PartDetails.Count(),
                    CanDelete = c.PartDetails == null ? true : ! c.PartDetails.Any()    

                }
               )
                
                .ToListAsync();
            return data;  
                
        }
        [HttpGet("{id}/PartDetails")]
        public async Task<ActionResult<IEnumerable<PartDetail>>> GetPartsOfCars(int id)
        {
            return await db.PartDetails.Where(x=>x.CarDetailId == id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CarDetail>> PostCarInputModel(CarInputModel model)
        {
            var car = new CarDetail
            {
                CarName = model.CarName,
                LaunchDate = model.LaunchDate,
                CarType = model.CarType,
                Price = model.Price,
                IsStock = model.IsStock,
                CarModel = model.CarModel,
                PartDetails = model.PartDetails,
            };
            await db.CarDetails.AddAsync(car);
            await db.SaveChangesAsync();
            return car;
        }

        [HttpPost("Upload/{id}")]
        public async Task<ActionResult<ImagePathResponse>> UploadPicture(int id, IFormFile picture)
        {
            var car = await db.CarDetails.FirstOrDefaultAsync(x=>x.CarDetailId==id);    
            if (car == null) return NotFound();
            var ext = Path.GetExtension(picture.FileName);
            string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
            string savePath = Path.Combine(this.env.WebRootPath, "Pictures", fileName);
            FileStream fs = new FileStream(savePath, FileMode.Create);
            picture.CopyTo(fs);
            fs.Close();
            car.CarModel = fileName;
            await db.SaveChangesAsync();
            return new ImagePathResponse { ImgPath = fileName };
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarDetail>> Delete(int id)
        {
            var c = await db.CarDetails.FirstOrDefaultAsync(x=>x.CarDetailId == id);    
            if(c == null) return NotFound();
            db.CarDetails.Remove(c);
            await db.SaveChangesAsync();
            return c;

        }
    }
}
