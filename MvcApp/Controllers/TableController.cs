using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.Services;

namespace MvcApp.Controllers
{
    public class TableController : Controller
    {
        private readonly IPhotoService _photoService;

        public TableController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            var photoList = _photoService.GetAll().Result;

            var model = new TableIndexModel()
            {
                DataTable = photoList
            };

            return View(model);
        }
    }
}