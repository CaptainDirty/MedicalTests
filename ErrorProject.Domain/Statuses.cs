using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public class Statuses
    {
        /// <summary>
        /// ID статуса
        /// </summary>
        [Key]
        public int ID_Status { get; set; }

        /// <summary>
        /// Наименование статуса
        /// </summary>    
        [Required(ErrorMessage = "Вы не ввели наименование статуса ошибки программного обеспечения")]
        [Display(Name = "Наименование статуса ошибки программного обеспечения")]
        public string NameStatus { get; set; }

        public UserProfile Owner { get; set; }


    }
}