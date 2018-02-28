using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piecyk.Models
{
    public class CarModels
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        [Required, Display(Name = "Marka")]
        public string Mark { get; set; }

        [Required]
        public string Model { get; set; }

        [Required, Display(Name = "Rocznik")]
        public int Year { get; set; }

        [Required, StringLength(17)]
        public String VIN { get; set; }

        [Required, Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Zdjęcie")]
        public string Picture { get; set; }

        [Required, Display(Name = "Data zakupu")]
        public DateTime PurchaseDate { get; set; }

        [Required, Display(Name = "Cena zakupu")]
        public double PurchaseAmount { get; set; }

        [Display(Name = "Cena sprzedaży")]
        public double? SalesAmount { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }

        public virtual UserInfoModels UserInfo { get; set; }
        public virtual ICollection<TuningModels> Tuning { get; set; }
    }
}