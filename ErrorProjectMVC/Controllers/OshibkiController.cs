using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MedicalTests.Domain;
using MedicalTestsMVC.Infrastructure;
using MedicalTestsMVC.Models;
using OfficeOpenXml;

namespace MedicalTestsMVC.Controllers
{
    [Authorize] // К контроллеру получают доступ только аутентифицированные пользователи.
    public class OshibkiController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        IOshibkiRepository _oshibki;
        IUserProfileRepository _users;
        IModulesRepository _modules;
        ICategoriesRepository _categories;
        IRazrabotchikiRepository _razrabotchiki;
        IStatusesRepository _statuses;
        public OshibkiController()
            : this(new DALContext())
        {
        }

        public OshibkiController(IDALContext context)
        {
            _oshibki = context.Oshibki;
            _users = context.Users;
            _modules = context.Modules; ;
            _categories = context.Categories;
            _razrabotchiki = context.Razrabotchiki;
            _statuses = context.Statuses;
        }

        //
        // GET: /Oshibki/

        public ActionResult Index(int StatusId = 0)
        {
            ViewBag.ID_Module = new SelectList(_modules.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Module", "NameModule");
            ViewBag.ID_Category = new SelectList(_categories.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Category", "NameCategory");
            ViewBag.ID_Razrabotchik = new SelectList(_razrabotchiki.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Razrabotchik", "Surname");
            ViewBag.ID_Status = new SelectList(_statuses.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Status", "NameStatus", StatusId);

            IEnumerable<Oshibki> query = _users.CurrentUser.Oshibki;

            if (StatusId > 0)
            {
                query = query.Where(x => x.ID_Status == StatusId);
            }
            var viewModel = query.ToList();

            return View(viewModel);
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "excel")]
        public ActionResult Index(object o)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var Ep = new ExcelPackage();
            var Sheet = Ep.Workbook.Worksheets.Add("Oshibki");

            Sheet.Cells["A1"].Value = "ID";
            Sheet.Cells["B1"].Value = "User";
            Sheet.Cells["C1"].Value = "Module";
            Sheet.Cells["D1"].Value = "Category";
            Sheet.Cells["E1"].Value = "Topic";
            Sheet.Cells["F1"].Value = "Date detection";
            Sheet.Cells["G1"].Value = "Comment";
            Sheet.Cells["H1"].Value = "Developer";
            Sheet.Cells["I1"].Value = "Status";
            Sheet.Cells["J1"].Value = "Date start";
            Sheet.Cells["K1"].Value = "Date over";

            var row = 2;
            foreach (var oshibka in _users.CurrentUser.Oshibki.ToList())
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = oshibka.ID_Oshibki;
                Sheet.Cells[string.Format("B{0}", row)].Value = oshibka.User.UserName;
                Sheet.Cells[string.Format("C{0}", row)].Value = oshibka.Module.NameModule;
                Sheet.Cells[string.Format("D{0}", row)].Value = oshibka.Category.NameCategory;
                Sheet.Cells[string.Format("E{0}", row)].Value = oshibka.Topic;
                Sheet.Cells[string.Format("F{0}", row)].Value = oshibka.DateDetection.ToString();
                Sheet.Cells[string.Format("G{0}", row)].Value = oshibka.Comment;
                Sheet.Cells[string.Format("H{0}", row)].Value = oshibka.Razrabotchiki.Name;
                Sheet.Cells[string.Format("I{0}", row)].Value = oshibka.Status.NameStatus;
                Sheet.Cells[string.Format("J{0}", row)].Value = oshibka.DateStart.ToString();
                Sheet.Cells[string.Format("K{0}", row)].Value = oshibka.DateOver.ToString();
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "OshibkiReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();

            return RedirectToAction("Index");
        }

        //
        // POST: /Oshibki/
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "filter")]
        public ActionResult Index(MedicalTests.Domain.Modules modules, Categories categories, Razrabotchiki razrabotchiki, Statuses statuses)
        {
            if (modules.ID_Module != 0 || modules.ID_Module == 0 && categories.ID_Category != 0 || categories.ID_Category == 0 && razrabotchiki.ID_Razrabotchik != 0 || razrabotchiki.ID_Razrabotchik == 0 && statuses.ID_Status != 0 || statuses.ID_Status == 0) // если выбран элемент списка "Все", который прописан при формировании выпадающего списка в представлении Index
            {
                ViewBag.ID_Razrabotchik = new SelectList(_razrabotchiki.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Razrabotchik", "Surname");
                ViewBag.ID_Module = new SelectList(_modules.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Module", "NameModule");
                ViewBag.ID_Category = new SelectList(_categories.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Category", "NameCategory");
                ViewBag.ID_Status = new SelectList(_statuses.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Status", "NameStatus");
                return View(_users.CurrentUser.Oshibki.Where((t => t.ID_Module == modules.ID_Module | t.ID_Category == categories.ID_Category | t.ID_Razrabotchik == razrabotchiki.ID_Razrabotchik | t.ID_Status == statuses.ID_Status)).ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        // GET: /Oshibki/Details

        public ActionResult Details(int id)
        {
            var viewModel = _oshibki.All
                .Include(p => p.Razrabotchiki)
                .Include(p => p.Category)
                .Include(p => p.Module)
                .Include(p => p.Status)
                .Include(p => p.User)
                .FirstOrDefault(t => t.ID_Oshibki == id);

            return View(viewModel);
        }

        //
        // GET: /Oshibki/Create

        public ActionResult Create()
        {
            ViewBag.ID_User = new SelectList(_users.All.Where(t => t.ID_User == _users.CurrentUser.ID_User), "ID_User", "UserName");
            ViewBag.ID_Module = new SelectList(_modules.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Module", "NameModule");
            ViewBag.ID_Category = new SelectList(_categories.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Category", "NameCategory");
            ViewBag.ID_Razrabotchik = new SelectList(_razrabotchiki.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Razrabotchik", "Surname");
            ViewBag.ID_Status = new SelectList(_statuses.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Status", "NameStatus");
            return View();
        }

        //
        // POST: /Oshibki/

        [HttpPost]
        public ActionResult Create(Oshibki oshibki)
        {
            if (ModelState.IsValid)
            {
                oshibki.User = _users.CurrentUser;
                _oshibki.InsertOrUpdate(oshibki);
                _oshibki.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Oshibki/Edit/1

        public ActionResult Edit(int id)
        {
            ViewBag.ID_User = new SelectList(_users.All.Where(t => t.ID_User == _users.CurrentUser.ID_User), "ID_User", "UserName");
            ViewBag.ID_Module = new SelectList(_modules.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Module", "NameModule");
            ViewBag.ID_Category = new SelectList(_categories.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Category", "NameCategory");
            ViewBag.ID_Razrabotchik = new SelectList(_razrabotchiki.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Razrabotchik", "Surname");
            ViewBag.ID_Status = new SelectList(_statuses.All.Where(t => t.Owner.ID_User == _users.CurrentUser.ID_User), "ID_Status", "NameStatus");
            return View(_oshibki.All.FirstOrDefault(t => t.ID_Oshibki == id));
        }

        //
        // POST: /Oshibki/Edit/

        [HttpPost]
        public ActionResult Edit(Oshibki oshibki)
        {
            if (ModelState.IsValid)
            {
                _oshibki.InsertOrUpdate(oshibki);
                _oshibki.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /Oshibki/Delete/1

        public ActionResult Delete(int id)
        {
            var viewModel = _oshibki.All
                .Include(p => p.Razrabotchiki)
                .Include(p => p.Category)
                .Include(p => p.Module)
                .Include(p => p.Status)
                .Include(p => p.User)
                .FirstOrDefault(t => t.ID_Oshibki == id);

            return View(viewModel);
        }

        //
        // POST: /Oshibki/Delete/1

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _oshibki.Remove(_oshibki.All.FirstOrDefault(t => t.ID_Oshibki == id));
            _oshibki.Save();
            return RedirectToAction("Index");
        }

      
    }
}
