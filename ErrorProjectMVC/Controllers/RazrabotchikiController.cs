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
    public class RazrabotchikiController : Controller
    {
        // Dependency Injection
        // Данные поля будут хранит ссылки на реальные репозитории или на тестовые в соответствии с параметрами переданными в конструктор
        IRazrabotchikiRepository _razrabotchiki;
        IUserProfileRepository _users;

        public RazrabotchikiController()
            : this(new DALContext())
        {
        }

        public RazrabotchikiController(IDALContext context)
        {
            _razrabotchiki = context.Razrabotchiki;
            _users = context.Users;
        }
        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "ClearTable")]
        public ActionResult Index(Object sender)
        {
            MistakeDatabase _database = new MistakeDatabase();
            _database.Razrabotchiki.RemoveRange(_database.Razrabotchiki.Where(o => o.Owner.ID_User == _users.CurrentUser.ID_User));
            _database.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "LoadTable")]
        public ActionResult Index(Razrabotchiki razrabotchiki)
        {
            #region --- Ввод тестовых данных в базу данных

            Razrabotchiki component_1 = new Razrabotchiki { Surname = "Фамилия 1", Name = "Имя 1", Thirdname = "Отчество 1", DateBirth = Convert.ToDateTime("1999-09-19"), Owner = _users.CurrentUser };
            _razrabotchiki.InsertOrUpdate(component_1);
            _razrabotchiki.Save();

            Razrabotchiki component_2 = new Razrabotchiki { Surname = "Фамилия 2", Name = "Имя 2", Thirdname = "Отчество 2", DateBirth = Convert.ToDateTime("1998-08-18"), Owner = _users.CurrentUser };
            _razrabotchiki.InsertOrUpdate(component_2);
            _razrabotchiki.Save();

            Razrabotchiki component_3 = new Razrabotchiki { Surname = "Фамилия 3", Name = "Имя 3", Thirdname = "Отчество 3", DateBirth = Convert.ToDateTime("1997-07-17"), Owner = _users.CurrentUser };
            _razrabotchiki.InsertOrUpdate(component_3);
            _razrabotchiki.Save();


            #endregion --- Ввод тестовых данных в базу данных

            return RedirectToAction("Index");
        }
        //
        // GET: /Razrabotchiki/

        public ActionResult Index()
        {
            return View(_users.CurrentUser.Razrabotchiki.ToList());
        }

        //
        // GET: /Razrabotchiki/Details

        public ActionResult Details(int id)
        {
            return View(_razrabotchiki.All.FirstOrDefault(t => t.ID_Razrabotchik == id));
        }

        //
        // GET: /Razrabotchiki/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Razrabotchiki/

        [HttpPost]
        public ActionResult Create(Razrabotchiki razrabotchiki)
        {
            if (ModelState.IsValid)
            {
                razrabotchiki.Owner = _users.CurrentUser;
                _razrabotchiki.InsertOrUpdate(razrabotchiki);
                _razrabotchiki.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Razrabotchiki/Edit/1

        public ActionResult Edit(int id)
        {
            return View( _razrabotchiki.All.FirstOrDefault(t => t.ID_Razrabotchik == id));
        }

        //
        // POST: /Razrabotchiki/Edit/

        [HttpPost]
        public ActionResult Edit(Razrabotchiki razrabotchiki)
        {
            if (ModelState.IsValid)
            {
                _razrabotchiki.InsertOrUpdate(razrabotchiki);
                _razrabotchiki.Save();
            }
            return RedirectToAction("Index");
        }

        //
        // Edit: /Razrabotchiki/Delete/1

        public ActionResult Delete(int id)
        {
            return View(_razrabotchiki.All.FirstOrDefault(t => t.ID_Razrabotchik == id));
        }

        //
        // POST: /Razrabotchiki/Delete/1

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _razrabotchiki.Remove(_razrabotchiki.All.FirstOrDefault(t => t.ID_Razrabotchik == id));
            _razrabotchiki.Save();
            return RedirectToAction("Index");
        }
    }
}
