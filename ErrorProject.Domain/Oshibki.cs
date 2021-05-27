using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalTests.Domain
{
    public class Oshibki
    {
        /// <summary>
        /// ID ошибки
        /// </summary>       
        [Key]
        public int ID_Oshibki { get; set; }

        /// <summary>
        /// ID пользователя
        /// </summary> 
        [Required(ErrorMessage = "Вы не выбрали пользователя")]
        [Display(Name = "Пользователь программного модуля")]
        public int ID_User { get; set; }

        public UserProfile User { get; set; }

        /// <summary>
        /// ID модуля
        /// </summary>
        [Required(ErrorMessage = "Вы не выбрали наименование программного модуля")]
        [Display(Name = "Программный модуль")]
        public int ID_Module { get; set; }

        public Modules Module { get; set; }

        /// <summary>
        /// ID категории
        /// </summary>   
        [Required(ErrorMessage = "Вы не выбрали категорию ошибки программного обеспечения")]
        [Display(Name = "Категория ошибки программного обеспечения")]
        public int ID_Category { get; set; }

        public Categories Category { get; set; }

        /// <summary>
        /// тема
        /// </summary>
        [Required(ErrorMessage = "Вы не ввели тему к обнаруженной ошибке программного обеспечения")]
        [Display(Name = "Тема ошибки")]
        public string Topic { get; set; }

        /// <summary>
        /// дата обнаружения ошибки
        /// </summary>     
        [Required(ErrorMessage = "Вы не заполнили дату обнаружения ошибки")]
        [Display(Name = "Дата обнаружения ошибки")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateDetection { get; set; }

        /// <summary>
        /// комментарий
        /// </summary> 
        [Required(ErrorMessage = "Вы не ввели комментарий к обнаруженной ошибке программного обеспечения")]
        [Display(Name = "Комментарий к ошибке")]
        public string Comment { get; set; }

        /// <summary>
        /// ID разработчика
        /// </summary>      
        [Required(ErrorMessage = "Вы не выбрали разработчика программного модуля")]
        [Display(Name = "Разработчик программного модуля")]
        public int ID_Razrabotchik { get; set; }

        public Razrabotchiki Razrabotchiki { get; set; }

        /// <summary>
        /// ID статуса
        /// </summary> 
        [Required(ErrorMessage = "Вы не указали статус ошибки программного обеспечения")]
        [Display(Name = "Статус ошибки программного обеспечения")]
        public int ID_Status { get; set; }

        public Statuses Status { get; set; }

        /// <summary>
        /// дата начала устранения ошибки
        /// </summary>
        [Display(Name = "Дата начала устранения ошибки")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }

        /// <summary>
        /// дата окончания устранения ошибки
        /// </summary>        
        [Display(Name = "Дата окончания устранения ошибки")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOver { get; set; }
    
    }
}