using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TV.Domain.Models;
using TV.Infrastructure.Repositories;
[Authorize]
public class LanguagesController : Controller
{
    private readonly ILanguagesRepository languagesRepository;
    private readonly ITVShowRepository tVShowRepository;
    private readonly ILanguageTVShowRepository languageTVShowRepository;

    public LanguagesController(ILanguagesRepository languagesRepository,
                               ITVShowRepository tVShowRepository,
                               ILanguageTVShowRepository languageTVShowRepository)
    {
        this.languagesRepository = languagesRepository;
        this.tVShowRepository = tVShowRepository;
        this.languageTVShowRepository = languageTVShowRepository;
    }
    [AllowAnonymous]
    // GET: Languages
    public IActionResult Index()
    {
        var languages = languagesRepository.GetAll();
        return View(languages);
    }

    // GET: Languages/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Languages/Create
    [HttpPost]
    public IActionResult Create(Language language)
    {
        if (ModelState.IsValid)
        {
            languagesRepository.Add(language);
            return RedirectToAction(nameof(Index));
        }
        return View(language);
    }
    public IActionResult CreateForTVShow(int id)
    {
        ViewData["TVShowId"] = id;
        return View();
    }

    [HttpPost]
    public IActionResult CreateForTVShow(int id, Language language)
    {
        if (ModelState.IsValid)
        {
            language.Id = 0;
            var l=languagesRepository.GetByName(language.Name);
            if (l==null)languagesRepository.Add(language);

            else language.Id = l.Id;

            var tvshow = tVShowRepository.GetById(id);

            if(languageTVShowRepository.IsFoundFromLanguageAndTVShow(id,language.Id))
                return RedirectToAction(nameof(Index));

            var languageTVShow = new LanguageTVShow
            {
                TVShowId = tvshow.Id,
                LanguageId = language.Id,
                TVShow = tvshow,
                Language = language
            };
            languageTVShowRepository.Add(languageTVShow);
            tvshow.LanguageTVShows.Add(languageTVShow);
            tVShowRepository.Update(tvshow);

            return RedirectToAction(nameof(Index));
        }

        return View(language);
    }


    // GET: Languages/Edit/5
    public IActionResult Edit(int id)
    {
        var language = languagesRepository.GetById(id);
        if (language == null)
        {
            return NotFound();
        }
        return View(language);
    }

    // POST: Languages/Edit/5
    [HttpPost]
    public IActionResult Edit(int id, Language language)
    {
        if (id != language.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            languagesRepository.Update(language);
            return RedirectToAction(nameof(Index));
        }
        return View(language);
    }

    // GET: Languages/Delete/5
    public IActionResult Delete(int id)
    {
        var language = languagesRepository.GetById(id);
        if (language == null)
        {
            return NotFound();
        }
        return View(language);
    }

    // POST: Languages/Delete/5
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        languagesRepository.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}
