using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using System;

namespace Acme.BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }

        [HttpPost("api/home/upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                var fileName = file.FileName;
                var filePath = Path.Combine(_environment.ContentRootPath, "C:\\GeoPerform\\BookStore\\Template", fileName);

                // Check if the file already exists
                if (System.IO.File.Exists(filePath))
                {
                    // If the file exists, generate a unique filename
                    fileName = GetUniqueFileName(fileName);
                    filePath = Path.Combine(_environment.ContentRootPath, "C:\\GeoPerform\\BookStore\\Template", fileName);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return the saved filename
                return Ok(new { success = true, fileName });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            var guid = Guid.NewGuid().ToString().Substring(0, 8); // Generate a unique identifier
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{guid}{Path.GetExtension(fileName)}";
            return uniqueFileName;
        }
    }
}

//using Acme.BookStore.Models;
//using Acme.BookStore.EntityFrameworkCore;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.IO;
//using System.Threading.Tasks;

//namespace Acme.BookStore.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly IWebHostEnvironment _environment;
//        private readonly BookStoreDbContext _dbContext;

//        public HomeController(IWebHostEnvironment environment, BookStoreDbContext dbContext)
//        {
//            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
//            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
//        }

//        [HttpPost("api/home/upload")]
//        public async Task<IActionResult> UploadFile(IFormFile file)
//        {
//            try
//            {
//                if (file == null || file.Length == 0)
//                    return BadRequest("No file uploaded.");

//                // Read file into a byte array
//                byte[] fileBytes;
//                using (var ms = new MemoryStream())
//                {
//                    await file.CopyToAsync(ms);
//                    fileBytes = ms.ToArray();
//                }

//                // Save Template to database
//                var template = new Template
//                {
//                    Excel = fileBytes,
//                    UploadedDateTime = DateTime.UtcNow,
//                    Status = true // Assuming upload is successful initially
//                };
//                _dbContext.Templates.Add(template);
//                await _dbContext.SaveChangesAsync();

//                // Return success response with file metadata
//                return Ok(new
//                {
//                    success = true,
//                    fileName = file.FileName,
//                    uploadedDateTime = template.UploadedDateTime,
//                    status = template.Status ? "Success" : "Failure"
//                });
//            }
//            catch (Exception ex)
//            {
//                // Log the exception for troubleshooting
//                // logger.LogError(ex, "An error occurred during file upload.");
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }
//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Acme.BookStore.Service;

//namespace Acme.BookStore.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ITemplateService _templateService;

//        public HomeController(ITemplateService templateService)
//        {
//            _templateService = templateService;
//        }

//        [HttpPost("api/home/upload")]
//        public async Task<IActionResult> UploadFile(IFormFile file)
//        {
//            try
//            {
//                if (file == null || file.Length == 0)
//                    return BadRequest("No file uploaded.");

//                var fileName = file.FileName;

//                using (var memoryStream = new MemoryStream())
//                {
//                    await file.CopyToAsync(memoryStream);
//                    var excelData = memoryStream.ToArray();

//                    var templateId = await _templateService.UploadTemplate(fileName, excelData, DateTime.Now);

//                    return Ok(new { success = true, templateId });
//                }
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex}");
//            }
//        }
//    }
//}