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
    }
}
