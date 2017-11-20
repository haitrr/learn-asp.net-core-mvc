using Library.Models.Catalog;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assest;
        public CatalogController(ILibraryAsset assest)
        {
            _assest = assest;
        }

        public IActionResult Index()
        {
            var assestModels = _assest.GetAll();
            var listingResult = assestModels
                .Select(result => new AssetIndexListingModel()
                {
                    Id = result.Id,
                    ImageURL = result.ImageURL,
                    AuthorOrDirector = _assest.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber = _assest.GetDeweyIndex(result.Id),
                    Title = result.Title,
                    Type = _assest.GetType(result.Id)

                }

                );

            var model = new AssestIndexModel()
            {
                Asset = listingResult
        };

            return View(model);
        }

        public IActionResult Detail(int id){
            var asset = _assest.GetById(id);
            
            var model = new AssetDetailModel{
                AssetId = id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageURL,
                AuthorOrDirector = _assest.GetAuthorOrDirector(id),
                CurrentLocation = _assest.GetCurrentLocation(id).Name,
                DeweyCallNumber = _assest.GetDeweyIndex(id),
                ISBN = _assest.GetIsbn(id)
            };

            return View(model);
        }
    }
}
