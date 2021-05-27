using MedicalTests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalTestsMVC.Models
{
    public class ErrorsListViewModel
    {
        public IEnumerable<Oshibki> Oshibki;

        public SelectList Modules { get; set; }
        public SelectList Categories { get; set; }

        public SelectList Razrabotchiki { get; set; }

        public SelectList Status { get; set; }
    }
}