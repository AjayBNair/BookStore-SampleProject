//using System;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Volo.Abp.Application.Services;

//namespace BookStore.MyServices
//{
//    public class MyExcelUploadService : ApplicationService
//    {
//        private readonly IWebHostEnvironment _env;

//        public MyExcelUploadService(IWebHostEnvironment env)
//        {
//            _env = env;
//        }

//        public async Task SaveExcelFile(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//            {
//                throw new ArgumentNullException(nameof(file), "No file uploaded");
//            }

//            if (!file.ContentType.Contains("application/"))
//            {
//                throw new ArgumentException("Invalid file format. Only Excel files are allowed.");
//            }

//            var filePath = Path.Combine(_env.ContentRootPath, "Template", file.FileName); // Adjust path if needed

//            using (var stream = new FileStream(filePath, FileMode.Create))
//            {
//                await file.CopyToAsync(stream);
//            }
//        }
//    }
//}