using MedicalTests.Domain;
using MedicalTestsMVC.Infrastructure;
using MedicalTestsMVC.Models;
using MedicalTestsMVC.Models.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MedicalTestsMVC.Controllers
{
    public class HomeController : Controller
    {
     
        public ActionResult Index()
        {
            return View();            
        }

        // GET: About
        public ActionResult About()
        {
            ViewBag.Message = "Документирование ошибок программного обеспечения";
            ViewBag.Assembly = "Версия " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Головко Яна Вячеславовна";
            return View();
        }
      
        public ActionResult Grafic()
        {
            AccountsDatabase _can = new AccountsDatabase();

            var viewModel = _can.Statuses.Select(x => new GrafikViewModel { StatusId = x.ID_Status, Name = x.NameStatus }).ToList();

            var oshibkiCount = _can.Oshibki.GroupBy(x => x.ID_Status).
                Select(x => new { Key = x.Key, Value = x.Count() }).
                ToDictionary(p => p.Key, p => p.Value);

            foreach(var status in oshibkiCount)
            {
                var row = viewModel.FirstOrDefault(x => x.StatusId == status.Key);
                
                if(row != null)
                {
                    row.Value = status.Value;
                }
            }

            //инициализируется массив, содержащий цвета в hex, для графика
            Random r = new Random();

            foreach(var row in viewModel)
            {
                row.Color = String.Format("#{0:X6}", r.Next(0x1000000));
            }

            return View(viewModel);
        }

     
        // GET: Cabinet
        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult Cabinet()
        {
            ViewBag.Message = "Личная страница.";

            return View();
        }

        //[Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        //public ActionResult AdminPanel()
        //{
        //    ViewBag.Message = "Страница администратора.";

        //    return View();
        //}
    }
}