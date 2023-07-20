using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("[controller]")]
    public class Images : Controller
    {
        // Add context for database to use it in this class (and initialize it in the constructor)
        private readonly DatabaseContext _dbcontext;

        public Images(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // GET: api/values
        [HttpGet]
        // Asynchronous task to get all the images in the database
        // and return all the values (addresses) as a list.
        public async Task<IActionResult> Get()
        {
            // Return as an OK to make sure the call is successful
            return Ok(_dbcontext.Images.ToList());
        }

        // POST api/values
        [HttpPost]
        // Asynchronous task to save an images in the database
        // and return the value (address) of the saved image
        public async Task<IActionResult> Post(IFormFile file, CancellationToken cancellationToken)
        {
            // Wait for the answer from the method/function before continuing 
            string result = await UploadFile(file);

            // Store the image address in the imageDataModel, so we can save the link to the database
            ImageDataModel imageDataModel = new ImageDataModel();
            imageDataModel.Address = result;

            // Save the data in the database
            _dbcontext.Images.Add(imageDataModel);
            _dbcontext.SaveChanges();

            return Ok(result);
        }

        // Upload the image to the server and return the filename
        private async Task<string> UploadFile(IFormFile file)
        {
            string path;
            try
            {
                // Define the folder that we will be using
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

                // Create the folder if it doesn't exist 
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                // Define the path to the image that we want to save
                path = Path.Combine(filepath, file.FileName);

                // Save the image to the server
                using var stream = new FileStream(path, FileMode.Create);

                await file.CopyToAsync(stream);
            } catch (Exception ex)
            {
                throw;
            }

            return path;
        }
    }
}

