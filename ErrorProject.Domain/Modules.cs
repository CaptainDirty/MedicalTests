using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public class Modules
    {
        /// <summary>
        /// ID модуля
        /// </summary>
        [Key]
        public int ID_Module { get; set; }

        /// <summary>
        /// наименование модуля
        /// </summary>
        [Required(ErrorMessage = "Вы не ввели наименование программного модуля")]
        [Display(Name = "Наименование программного модуля")]
        public string NameModule { get; set; }

        public UserProfile Owner { get; set; }
    }
}