//using Acme.BookStore.EntityFrameworkCore;
//using Acme.BookStore.Models;
//using System;
//using System.Threading.Tasks;

//namespace Acme.BookStore.Service
//{
//    public class TemplateService : ITemplateService
//    {
//        private readonly BookStoreDbContext _context;

//        public TemplateService(BookStoreDbContext context)
//        {
//            _context = context ?? throw new ArgumentNullException(nameof(context));
//        }

//        public async Task<Guid> UploadTemplate(string fileName, byte[] excelData, DateTime uploadedDateTime)
//        {
//            var template = new Template
//            {
//                FileName = fileName,
//                Excel = excelData,
//                UploadedDateTime = uploadedDateTime,
//                Status = true
//            };

//            _context.Templates.Add(template);
//            await _context.SaveChangesAsync();

//            return template.Id;
//        }
//    }
//}