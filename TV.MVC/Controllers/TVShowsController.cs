using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Net.Mail;
using TV.Domain.Models;
using TV.Infrastructure.Repositories;
using TV.MVC.Models;
using Attachment = TV.Domain.Models.Attachment;

namespace TV.MVC.Controllers
{
    [Authorize]
    public class TVShowsController : Controller
    {
        private readonly ILogger<TVShow> logger;
        private readonly ITVShowRepository tVShowRepository;
        private readonly IAttachmentRepository attachmentRepository;
        private readonly ILanguagesRepository languagesRepository;

        public TVShowsController(
                                 ILogger<TVShow> logger,
                                 ITVShowRepository tVShowRepository,
                                 IAttachmentRepository attachmentRepository,
                                 ILanguagesRepository languagesRepository)
        {
            this.logger = logger;
            this.tVShowRepository = tVShowRepository;
            this.attachmentRepository = attachmentRepository;
            this.languagesRepository = languagesRepository;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var tvs= tVShowRepository.GetAll();
            return View(tvs);
        }
        [Route("/Details/{id:int}/{slug}")]
        [AllowAnonymous]
        public IActionResult Details(int id,string slug)
        {
            var tv= tVShowRepository.GetByIdWithLanguage(id);
            return View(tv);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TVShow model, IFormFile img)
        {
             tVShowRepository.Add(model);
             var fileName = $"{model.Id}{Path.GetExtension(img.FileName)}";
             var filePath = Path.Combine("wwwroot/img/TV_Show", fileName);
             using (var stream = new FileStream(filePath, FileMode.Create))
             {
                 img.CopyTo(stream);
             }
             var attachment = new Attachment
             {
                 Name = fileName,
                 Path = filePath,
                 FileType = img.ContentType,
                 TVShowId=model.Id
             };
             attachmentRepository.Add(attachment);
             model.Attachment = attachment;
             model.AttachmentId = attachment.Id;
             tVShowRepository.Update(model);
             return RedirectToAction("Index");
           
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var tvshow=tVShowRepository.GetById(id);
            if (tvshow == null)
                return NotFound();
            return View(tvshow);
        }
        [HttpPost]
        public ActionResult Update(int id, TVShow model, IFormFile attachment)
        {
            var tvshow = tVShowRepository.GetById(id);
            if (tvshow == null)
                return NotFound();
            tvshow.ReleaseDate = model.ReleaseDate;
            tvshow.Title = model.Title;
            tvshow.URL = model.URL;
            tvshow.Rating = model.Rating;

            if (attachment != null && attachment.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", $"{tvshow.Id}.jpg");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    attachment.CopyTo(stream); 
                }

                var attachmentEntity = attachmentRepository.GetById(id);
                if (attachmentEntity != null)
                {
                    attachmentEntity.Path = $"/img/{tvshow.Id}.jpg";
                    attachmentRepository.Update(attachmentEntity);
                }
                else
                {
                    var newAttachment = new Attachment
                    {
                        Name = $"{tvshow.Title} Image",
                        Path = $"/img/{tvshow.Id}.jpg",
                        FileType = "image/jpeg",
                        TVShowId = tvshow.Id,
                        IsDeleted = false
                    };
                    attachmentRepository.Add(newAttachment);
                }
            }

            tVShowRepository.Update(tvshow);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var tvshow = tVShowRepository.GetById(id);
            if (tvshow == null)
                return NotFound();

            return View(tvshow);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var tvshow = tVShowRepository.GetById(id);
            if (tvshow == null)
                return NotFound();

            if (tvshow.AttachmentId!=null)
            {
                var attachment = attachmentRepository.GetById(tvshow.AttachmentId);
                if (attachment != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", $"{attachment.Id}.jpg");
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    attachmentRepository.Delete(attachment.Id);
                }
            }

            tVShowRepository.Delete(id);

            return RedirectToAction("Index");
        }

        [Route("TVShows/GetByLanguage/{language}")]
        [AllowAnonymous]
        public IActionResult GetByLanguage(string language)
        {
            var tvShows = tVShowRepository.GetByLanguage(language);
            if (tvShows == null || !tvShows.Any())
            {
                return NotFound(); 
            }
            return View(tvShows); 
        }
        [AllowAnonymous]
        public IActionResult ViewAll(int id)
        {
            var tvshow=tVShowRepository.GetById(id);
            ViewBag.TVShowTitle = tvshow.Title;
            var attachment=attachmentRepository.GetById(tvshow.AttachmentId);
            if (attachment == null)
            {
                return NotFound();
            }
            return View(attachment);
        }

    }
}
