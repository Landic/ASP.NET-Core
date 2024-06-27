using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.DTO;
using MusicPortal.Infrastructure;

namespace MusicPortal.Controllers
{
    [Culture]
    public class MusicController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISongService _songService;
        private readonly IGenreService _genreService;
        private readonly IPerformerService _performerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILangService _langService;

        public MusicController(IUserService userService, ISongService songService, IGenreService genreService, IPerformerService performerService, IWebHostEnvironment webHostEnvironment, ILangService langService)
        {
            _userService = userService;
            _songService = songService;
            _genreService = genreService;
            _performerService = performerService;
            _webHostEnvironment = webHostEnvironment;
            _langService = langService;
        }

        public async Task<IActionResult> Index(int genre = 0, int performer = 0, int page = 1,
            SortState sortOrder = SortState.TitleAsc)
        {
            if (HttpContext.Session.GetString("Login") != null)
            {
                var songs = await _songService.GetSongs();
                if (genre != 0)
                {
                    songs = songs.Where(s => s.GenreId == genre);
                }
                if (performer != 0)
                {
                    songs = songs.Where(s => s.ArtistId == performer);
                }

                songs = sortOrder switch
                {
                    SortState.TitleDesc => songs.OrderByDescending(s => s.Title).ToList(),
                    SortState.TitleAsc => songs.OrderBy(s => s.Title).ToList(),
                    SortState.GenreDesc => songs.OrderByDescending(s => s.Genre).ToList(),
                    SortState.GenreAsc => songs.OrderBy(s => s.Genre).ToList(),
                    SortState.PerformerDesc => songs.OrderByDescending(s => s.Performer).ToList(),
                    SortState.PerformerAsc => songs.OrderBy(s => s.Performer).ToList(),
                    _ => songs.OrderBy(s => s.Title).ToList(),
                };



                var count = songs.Count();
                var items = songs.Skip((page - 1) * 6).Take(6);



                HttpContext.Session.SetString("path", Request.Path);
                ViewBag.Languages = _langService.languageList();
                return base.View(new Models.Index(items, new Page(count, page, 6), new Filter(await _genreService.GetGenres(), await _performerService.GetPerformers(), genre, performer), new SortVM(sortOrder)));
            }
            else
            {
                return Redirect("/Authorization/Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Authorization/Login");
        }

        public async Task<IActionResult> DeleteSong(int songId)
        {
            var song = await _songService.GetSongById(songId);
            string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, song.Path!.TrimStart('/'));
            System.IO.File.Delete(oldFilePath);
            await _songService.DeleteSong(songId);
            return RedirectToAction("Index");
        }

        public IActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Club/Index";
            List<string> cultures = _langService.languageList().Select(t => t.ShortName).ToList()!;
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); 
            Response.Cookies.Append("lang", lang, option);
            return Redirect(returnUrl);
        }

        



        public async Task<IActionResult> ToUsers() => View("~/Views/Music/Users.cshtml", await _userService.GetUsers());

        public async Task<IActionResult> Authorize(int userId)
        {
            var user = await _userService.GetUserById(userId);
            user.IsAuthorized = !user.IsAuthorized;
            _userService.UpdateUser(user);
            await _userService.Save();
            return View("~/Views/Music/Users.cshtml", await _userService.GetUsers());
        }

        public async Task<IActionResult> DeleteUser(int userId)
        {
            _userService?.DeleteUser(userId);
            await _userService!.Save();
            return View("~/Views/Music/Users.cshtml", await _userService.GetUsers());
        }

        




        public async Task<IActionResult> ToGenres() => View("~/Views/Music/Genres.cshtml", await _genreService.GetGenres());

        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            await _genreService.DeleteGenre(genreId);
            await _genreService.Save();
            return View("~/Views/Music/Genres.cshtml", await _genreService.GetGenres());
        }

        




        public IActionResult ToAddGenre() => View("~/Views/Music/AddGenre.cshtml");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGenre(GenreDTO model)
        {
            if (ModelState.IsValid)
            {
                await _genreService.AddGenre(model);
                await _genreService.Save();
                return RedirectToAction("ToGenres");
            }
            return View("~/Views/Music/AddGenre.cshtml", model);
        }

        



        public async Task<IActionResult> ToEditGenre(int genreId) => View("~/Views/Music/EditGenre.cshtml", await _genreService.GetGenreById(genreId));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(GenreDTO model)
        {
            if (ModelState.IsValid)
            {
                _genreService.UpdateGenre(model);
                await _genreService.Save();
                return RedirectToAction("ToGenres");
            }
            return View("~/Views/Music/EditGenre.cshtml", model);
        }

        



        public async Task<IActionResult> ToPerformers() => View("~/Views/Music/Performers.cshtml", await _performerService.GetPerformers());

        public async Task<IActionResult> DeletePerformer(int performerId)
        {
            await _performerService.DeletePerformer(performerId);
            await _performerService.Save();
            return View("~/Views/Music/Performers.cshtml", await _performerService.GetPerformers());
        }

        




        public IActionResult ToAddPerformer() => View("~/Views/Music/AddPerformer.cshtml");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerformer(PerformerDTO model)
        {
            if (ModelState.IsValid)
            {
                await _performerService.AddPerformer(model);
                await _performerService.Save();
                return RedirectToAction("ToPerformers");
            }
            return View("~/Views/Music/AddPerformer.cshtml", model);
        }

        




        public async Task<IActionResult> ToEditPerformer(int performerId) => View("~/Views/Music/EditPerformer.cshtml", await _performerService.GetPerformerById(performerId));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPerformer(PerformerDTO model)
        {
            if (ModelState.IsValid)
            {
                _performerService.UpdatePerformer(model);
                await _performerService.Save();
                return RedirectToAction("ToPerformers");
            }
            return View("~/Views/Music/EditPerformer.cshtml", model);
        }

        



        public async Task<IActionResult> ToAddSong()
        {
            ViewBag.Genres = await _genreService.GetGenres();
            ViewBag.Performers = await _performerService.GetPerformers();
            return View("~/Views/Music/AddSong.cshtml");
        }

        public async Task<IActionResult> AddSong(IFormFile uploadedFile, SongDTO model)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                if (uploadedFile == null)
                {
                    ModelState.AddModelError("Path", Resources.Resource.ChooseFile);
                }
                else
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "musics");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        ModelState.AddModelError("Path", Resources.Resource.FileExist);
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    var user = await _userService.GetUserByLogin(HttpContext.Session.GetString("Login")!);
                    var genre = await _genreService.GetGenreByName(model.Genre!);
                    var performer = await _performerService.GetPerformerByFullName(model.Performer!);

                    await _songService.AddSong(new SongDTO
                    {
                        Title = model.Title,
                        Path = "/musics/" + uniqueFileName,
                        UserId = user!.Id,
                        GenreId = genre!.Id,
                        ArtistId = performer!.Id,
                        User = user.Login,
                        Genre = genre.Name,
                        Performer = performer.FullName
                    });
                    await _songService.Save();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Genres = await _genreService.GetGenres();
            ViewBag.Performers = await _performerService.GetPerformers();
            return View("~/Views/Music/AddSong.cshtml", model);
        }





        public async Task<IActionResult> ToEditSong(int songId)
        {
            ViewBag.Genres = await _genreService.GetGenres();
            ViewBag.Performers = await _performerService.GetPerformers();
            return View("~/Views/Music/EditSong.cshtml", await _songService.GetSongById(songId));
        }

        public async Task<IActionResult> EditSong(IFormFile uploadedFile, SongDTO model)
        {
            if (ModelState.IsValid)
            {
                var song = await _songService.GetSongById(model.Id);
                var genre = await _genreService.GetGenreByName(model.Genre!);
                var performer = await _performerService.GetPerformerByFullName(model.Performer!);

                song.Title = model.Title;
                song.GenreId = genre!.Id;
                song.ArtistId = genre.Id;
                song.Genre = genre.Name;
                song.Performer = performer.FullName;

                if (uploadedFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "musics");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, song.Path!.TrimStart('/'));
                    System.IO.File.Delete(oldFilePath);

                    song.Path = "/musics/" + uniqueFileName;
                }
                else song.Path = song.Path;

                _songService.UpdateSong(song);
                await _songService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.Genres = await _genreService.GetGenres();
            ViewBag.Performers = await _performerService.GetPerformers();
            return View("~/Views/Music/EditSong.cshtml", model);
        }
    }
}
