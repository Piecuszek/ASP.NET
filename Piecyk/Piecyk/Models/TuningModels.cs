using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piecyk.Models
{
    public class TuningModels
    {
        public int ID { get; set; }

        [Required, Display(Name = "Samochód")]
        public string CarID { get; set; }

        [Required, Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required, Display(Name = "Opis")]
        public string Desc { get; set; }

        [Required, Display(Name = "Koszt")]
        public double Amount { get; set; }

        public virtual CarModels Car { get; set; }
    }
}