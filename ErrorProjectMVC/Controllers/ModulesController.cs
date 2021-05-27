using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalTests.Domain;
using MedicalTestsMVC.Infrastructure;
using MedicalTestsMVC.Models;

namespace MedicalTestsMVC.Controllers
{
    [Authorize]// К контроллеру получают доступ только аутентифицированные пользователи.
    public class ModulesController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        IModulesRepository _modules;
        IUserProfileRepository _users;

        public ModulesController()
            : this(new DALContext())
        {
        }

        public ModulesController(IDALContext context)
        {
            _modules = context.Modules;
            _users = context.Users;
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ClearTable")]
        public ActionResult Index(Object sender)
        {
            MistakeDatabase _database = new MistakeDatabase();
            _database.Modules.RemoveRange(_database.Modules.Where(o => o.Owner.ID_User == _users.CurrentUser.ID_User));
            _database.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "LoadTable")]
        public ActionResult Index(Modules modules)
        {
            #region --- Ввод тестовых данных в базу данных

            Modules component_1 = new Modules { NameModule = "Модуль 1", Owner = _users.CurrentUser };
            _modules.InsertOrUpdate(component_1);
            _modules.Save();

            Modules component_2 = new Modules { NameModule = "Модуль 2", Owner = _users.CurrentUser };
            _modules.InsertOrUpdate(component_2);
            _modules.Save();

            Modules component_3 = new Modules { NameModule = "Модуль 3", Owner = _users.CurrentUser };
            _modules.InsertOrUpdate(component_3);
            _modules.Save();


            #endregion --- Ввод тестовых данных в базу данных

            return RedirectToAction("Index");
        }
        //
        // GET: /Modules/

        public ActionResult Index()
        {
            //if (_modules.All.Count(t => t.NameModule == "Все") == 0)
            //{
            //    return View(_users.CurrentUser.Modules.All<_modules.All.Select(t => t.NameModule == "Все")>.ToList());
            //}
            return View(_users.CurrentUser.Modules.ToList());
        }

        //
        // GET: /Modules/Details

        public ActionResult Details(int id)
        {
            return View(_modules.All.FirstOrDefault(t => t.ID_Module == id));
        }

        //
        // GET: /Modules/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Modules/

        [HttpPost]
        public ActionResult Create(Modules modules)
        {
            if (ModelState.IsValid)
            {
                modules.Owner = _users.CurrentUser;
                _modules.InsertOrUpdate(modules);
                _modules.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Modules/Edit/1

        public ActionResult Edit(int id)
        {
            return View(_modules.All.FirstOrDefault(t => t.ID_Module == id));
        }

        //
        // POST: /Modules/Edit/

        [HttpPost]
        public ActionResult Edit(Modules modules)
        {
            if (ModelState.IsValid)
            {
                _modules.InsertOrUpdate(modules);
                _modules.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /Modules/Delete/1

        public ActionResult Delete(int id)
        {
            return View(_modules.All.FirstOrDefault(t => t.ID_Module == id));
        }

        //
        // POST: /Modules/Delete/1

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _modules.Remove(_modules.All.FirstOrDefault(t => t.ID_Module == id));
            _modules.Save();
            return RedirectToAction("Index");
        }
    }
}
