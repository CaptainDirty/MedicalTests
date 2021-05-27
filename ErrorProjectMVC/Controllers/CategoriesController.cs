using MedicalTests.Domain;
using MedicalTestsMVC.Infrastructure;
using MedicalTestsMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MedicalTestsMVC.Controllers
{
    [Authorize]// К контроллеру получают доступ только аутентифицированные пользователи.
    public class CategoriesController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        ICategoriesRepository _categories;
        IUserProfileRepository _users;

        public CategoriesController()
            : this(new DALContext())
        {
        }

        public CategoriesController(IDALContext context)
        {
            _categories = context.Categories;
            _users = context.Users;
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ClearTable")]
        public ActionResult Index(Object sender)
        {
            MistakeDatabase _database = new MistakeDatabase();
            _database.Categories.RemoveRange(_database.Categories.Where(o => o.Owner.ID_User == _users.CurrentUser.ID_User));
            _database.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "LoadTable")]
        public ActionResult Index(Categories categories)
        {
            #region --- Ввод тестовых данных в базу данных

            Categories component_1 = new Categories { NameCategory = "Категория 1", Owner = _users.CurrentUser };
            _categories.InsertOrUpdate(component_1);
            _categories.Save();

            Categories component_2 = new Categories { NameCategory = "Категория 2", Owner = _users.CurrentUser };
            _categories.InsertOrUpdate(component_2);
            _categories.Save();

            Categories component_3 = new Categories { NameCategory = "Категория 3", Owner = _users.CurrentUser };
            _categories.InsertOrUpdate(component_3);
            _categories.Save();
            

            #endregion --- Ввод тестовых данных в базу данных

            return RedirectToAction("Index");
        }

        //
        // GET: /Categories/

        public ActionResult Index()
        {
            return View(_users.CurrentUser.Categories.ToList());
        }

        //
        // GET: /Categories/Details

        public ActionResult Details(int id)
        {
            return View(_categories.All.FirstOrDefault(t => t.ID_Category == id));
        }

        //
        // GET: /Categories/Create

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categories/
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        public ActionResult Create(Categories categories)
        {

            if (ModelState.IsValid)
            {
                categories.Owner = _users.CurrentUser;
                _categories.InsertOrUpdate(categories);
                _categories.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Categories/Edit/1
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Edit(int id)
        {
            return View(_categories.All.FirstOrDefault(t => t.ID_Category == id));
        }

        //
        // POST: /Categories/Edit/

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]        
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                _categories.InsertOrUpdate(categories);
                _categories.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /Categories/Delete/1
        // [Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult Delete(int id)
        {
            return View(_categories.All.FirstOrDefault(t => t.ID_Category == id));
        }

        //
        // POST: /Categories/Delete/1
        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _categories.Remove(_categories.All.FirstOrDefault(t => t.ID_Category == id));
            _categories.Save();
            return RedirectToAction("Index");
        }
    }
}
