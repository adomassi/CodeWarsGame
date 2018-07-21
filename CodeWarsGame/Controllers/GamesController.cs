using System.Web.Mvc;
using CodeWarsGame.Helpers;
using CodeWarsGame.Models;
using CodeWarsGame.Repositories;

namespace CodeWarsGame.Controllers
{
    public class GamesController : Controller
    {
        private readonly GamesRepository _gamesRepository;
        public GamesController()
        {
            _gamesRepository = new GamesRepository();
        }
       
        public ActionResult SuccessfullTasks(string playerName)
        {
            return View(_gamesRepository.GetSuccessfullTaskNames(playerName));
        }
       
        public ActionResult HiScores()
        {
            return View(_gamesRepository.GetAllGames());
        }
        
        public ActionResult Game()
        {
            return View();
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Game([Bind(Include = "Id,PlayerName,TaskId,TaskSucceeded")]
            Player player, string textAnswer, int selectedTaskId)
        {

            if (ModelState.IsValid)
            {
                var res = new SubmissionValidation().ValidateSubmission(textAnswer, selectedTaskId);

                if (!res.Success)
                {
                    ModelState.AddModelError("textAnswer", res.Output);
                }
                else
                {
                    _gamesRepository.AddSubmissionToScores(player, selectedTaskId);
                    ViewBag.SuccessMessage = res.Output;
                }
            }

            return View(player);
        }
    }
}