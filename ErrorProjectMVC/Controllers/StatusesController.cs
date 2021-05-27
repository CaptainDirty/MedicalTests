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
    [Authorize] // К контроллеру получают доступ только аутентифицированные пользователи.
    public class StatusesController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        IStatusesRepository _statuses;
        IUserProfileRepository _users;
        public StatusesController()
            : this(new DALContext())
        {
        }

        public StatusesController(IDALContext context)
        {
            _statuses = context.Statuses;
            _users = context.Users;
        }

        //
        // GET: /Statuses/
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ClearTable")]
        public ActionResult Index(Object sender)
        {
            MistakeDatabase _database = new MistakeDatabase();
            _database.Statuses.RemoveRange(_database.Statuses.Where(o => o.Owner.ID_User == _users.CurrentUser.ID_User));
            _database.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "LoadTable")]
        public ActionResult Index(Statuses statuses)
        {
            #region --- Ввод тестовых данных в базу данных

            Statuses component_1 = new Statuses { NameStatus = "Статусы 1", Owner = _users.CurrentUser };
            _statuses.InsertOrUpdate(component_1);
            _statuses.Save();

            Statuses component_2 = new Statuses { NameStatus = "Статусы 2", Owner = _users.CurrentUser };
            _statuses.InsertOrUpdate(component_2);
            _statuses.Save();

            Statuses component_3 = new Statuses { NameStatus = "Статусы 3", Owner = _users.CurrentUser };
            _statuses.InsertOrUpdate(component_3);
            _statuses.Save();


            #endregion --- Ввод тестовых данных в базу данных

            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            return View(_users.CurrentUser.Statuses.ToList());
        }

        //
        // GET: /Statuses/Details

        public ActionResult Details(int id)
        {
            return View(_statuses.All.FirstOrDefault(t => t.ID_Status == id));
        }

        //
        // GET: /Statuses/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Statuses/

        [HttpPost]
        public ActionResult Create(Statuses statuses)
        {
            if (ModelState.IsValid)
            {
                statuses.Owner = _users.CurrentUser;
                _statuses.InsertOrUpdate(statuses);
                _statuses.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Statuses/Edit/1

        public ActionResult Edit(int id)
        {
            return View(_statuses.All.FirstOrDefault(t => t.ID_Status == id));
        }

        //
        // POST: /Statuses/Edit/

        [HttpPost]
        public ActionResult Edit(Statuses statuses)
        {
            if (ModelState.IsValid)
            {
                _statuses.InsertOrUpdate(statuses);
                _statuses.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /Statuses/Delete/1

        public ActionResult Delete(int id)
        {
            return View(_statuses.All.FirstOrDefault(t => t.ID_Status == id));
        }

        //
        // POST: /Statuses/Delete/1

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _statuses.Remove(_statuses.All.FirstOrDefault(t => t.ID_Status == id));
            _statuses.Save();
            return RedirectToAction("Index");
        }
    }
}
