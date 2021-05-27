using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalTestsMVC.Models.HomeViewModels
{
    public class GrafikViewModel
    {
        public int StatusId { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public string Color { get; set; }
    }
}