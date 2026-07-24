using Microsoft.AspNetCore.Mvc;
using Portfolio.Data.Context;

namespace Portfolio.Controllers
{
    public class UserMessageController : Controller
    {
        private readonly AppDbContext _context;

        public UserMessageController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userMessages = _context.UserMessages
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(userMessages);
        }
        public IActionResult Detail(int id)
        {
            var userMessage = _context.UserMessages.Find(id);

            if (userMessage == null)
            {
                return RedirectToAction("Index");
            }

            if (!userMessage.IsRead)
            {
                userMessage.IsRead = true;
                _context.SaveChanges();
            }

            return View(userMessage);
        }
        public IActionResult Delete(int id)
        {
            var userMessage = _context.UserMessages.Find(id);

            if (userMessage == null)
            {
                return RedirectToAction("Index");
            }

            _context.UserMessages.Remove(userMessage);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ReadMessages()
        {
            var userMessages = _context.UserMessages
                .Where(x=>x.IsRead == true)
                .OrderByDescending(x => x.Id)
                .ToList();

            return View("Index",userMessages);
        }
        public IActionResult UnreadMessages()
        {
            var userMessages = _context.UserMessages
                .Where(x => x.IsRead == false)
                .OrderByDescending(x => x.Id)
                .ToList();

            return View("Index", userMessages);
        }
    }
}
