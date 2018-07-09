using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.Services;

namespace MvcApp.Controllers.Api
{
   
    [ApiController]
    [Route("api/photos")]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _serviceProvider;

        public PhotosController(IPhotoService serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //GET /api/photos
        [HttpGet]      
        public async Task<IEnumerable<DataTable>> GetPhotos()
        {
           return await _serviceProvider.GetAll();
        }
    }
}