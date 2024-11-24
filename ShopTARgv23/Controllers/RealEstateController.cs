using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARgv23.Core.Dto;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Data;
using ShopTARgv23.Models.RealEstate;

namespace ShopTARgv23.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopTARgv23Context _context;
        private readonly IRealEstate _realEstate;
        private readonly IFileServices _fileServices;
        public RealEstateController(ShopTARgv23Context context, IRealEstate realEstate, IFileServices fileServices)
        {
            _context = context;
            _realEstate = realEstate;
            _fileServices = fileServices;
        }
        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Location = x.Location,
                    Size = x.Size,
                    RoomNumber = x.RoomNumber,
                    BuildingType = x.BuildingType,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel estate = new();

            return View("CreateUpdate", estate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Location = vm.Location,
                Size = vm.Size,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        RealEstateId = x.RealEstateId,
                    }).ToArray()
            };

            var result = await _realEstate.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var estate = await _realEstate.Details(id);
            if (estate == null) { return NotFound(); }

            var images = await _context.FileToDatabases
               .Where(x => x.RealEstateId == id)
               .Select(y => new RealEstateImageViewModel
               {
                   ImageId = y.Id,
                   RealEstateId = y.Id,
                   ImageData = y.ImageData,
                   ImageTitle = y.ImageTitle,
                   Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData)),
               }).ToArrayAsync();

            var vm = new RealEstateDetailsViewModel
            {
                Id = estate.Id,
                Location = estate.Location,
                Size = estate.Size,
                RoomNumber = estate.RoomNumber,
                BuildingType = estate.BuildingType,
                CreatedAt = estate.CreatedAt,
                ModifiedAt = estate.ModifiedAt
            };
            vm.Image.AddRange(images);


            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var estate = await _realEstate.Details(id);
            if (estate == null) { return NotFound(); }

            var images = await _context.FileToDatabases
               .Where(x => x.RealEstateId == id)
               .Select(y => new RealEstateImageViewModel
               {
                   ImageId = y.Id,
                   RealEstateId = y.Id,
                   ImageData = y.ImageData,
                   ImageTitle = y.ImageTitle,
                   Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData)),
               }).ToArrayAsync();

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = estate.Id;
            vm.Location = estate.Location;
            vm.Size = estate.Size;
            vm.RoomNumber = estate.RoomNumber;
            vm.BuildingType = estate.BuildingType;
            vm.CreatedAt = estate.CreatedAt;
            vm.ModifiedAt = estate.ModifiedAt;
            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Location = vm.Location,
                Size = vm.Size,
                RoomNumber = vm.RoomNumber,
                BuildingType = vm.BuildingType,
                ModifiedAt = DateTime.Now,
                CreatedAt = vm.CreatedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    RealEstateId = x.RealEstateId,
                }).ToArray(),
            };

            var result = await _realEstate.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var estate = await _realEstate.Details(id);

            if (estate == null)
            {
                return NotFound();
            }

            var images = await _context.FileToDatabases
              .Where(x => x.RealEstateId == id)
              .Select(y => new RealEstateImageViewModel
              {
                  ImageId = y.Id,
                  RealEstateId = y.Id,
                  ImageData = y.ImageData,
                  ImageTitle = y.ImageTitle,
                  Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
              }).ToArrayAsync();

            var vm = new RealEstateDeleteViewModel();

            vm.Id = id;
            vm.Id = estate.Id;
            vm.Location = estate.Location;
            vm.Size = estate.Size;
            vm.RoomNumber = estate.RoomNumber;
            vm.BuildingType = estate.BuildingType;
            vm.CreatedAt = estate.CreatedAt;
            vm.ModifiedAt = estate.ModifiedAt;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var estate = await _realEstate.Delete(id);

            if (estate == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(RealEstateImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId,
            };

            var image = await _fileServices.RemoveImageFromDatabase(dto);

            if(image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
