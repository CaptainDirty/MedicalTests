using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    [Table("UserProfile")]
    public class UserProfile
    {
        /// <summary>
        /// ID пользователя
        /// </summary>       
        [Key]
        public int ID_User { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required(ErrorMessage = "Вы не ввели имя пользователя")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        public ICollection<Oshibki> Oshibki { get; set; }

        public ICollection<Categories> Categories { get; set; }

        public ICollection<Modules> Modules { get; set; }

        public ICollection<Razrabotchiki> Razrabotchiki { get; set; }

        public ICollection<Statuses> Statuses { get; set; }

    }
}