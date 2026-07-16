using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Context;
using Portfolio.Data.Entities;

namespace Portfolio.Controllers
{
    public class ProjectTechStacksController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectTechStacksController(AppDbContext context)
        {
            _context = context;
        }

        //Eager Loading
        public IActionResult Index()
        {
            var projectTechStacks = _context.ProjectTechStacks
                .Include(x => x.Project)
                .Include(x => x.TechStack)
                .ToList()
                .GroupBy(x => x.Project.Name);

            return View(projectTechStacks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var projects = _context.Projects.ToList();
            var techStacks = _context.TechStacks.ToList();

            ViewBag.projects = (from project in projects
                                select new SelectListItem
                                {
                                    Text = project.Name,
                                    Value = project.Id.ToString()
                                }).ToList();

            ViewBag.techStacks = (from tech in techStacks
                                  select new SelectListItem
                                  {
                                      Text = tech.Name,
                                      Value = tech.Id.ToString()
                                  }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProjectTechStack projectTechStack)
        {
            bool alreadyExists = _context.ProjectTechStacks.Any(x =>
                                                                x.ProjectId == projectTechStack.ProjectId &&
                                                                x.TechStackId == projectTechStack.TechStackId);

            if (alreadyExists)
            {
                ViewBag.Error = "Bu teknoloji zaten ekli.";

                // Dropdownları tekrar doldur
                ViewBag.projects = _context.Projects.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                ViewBag.techStacks = _context.TechStacks.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

                return View();
            }

            _context.ProjectTechStacks.Add(projectTechStack);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Manage(int id)
        {
            var values = _context.ProjectTechStacks
                .Include(x => x.Project)
                .Include(x => x.TechStack)
                .Where(x => x.ProjectId == id)
                .ToList();

            return View(values);
        }

        public IActionResult Delete(int id)
        {
            var value = _context.ProjectTechStacks.Find(id);

            _context.ProjectTechStacks.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
