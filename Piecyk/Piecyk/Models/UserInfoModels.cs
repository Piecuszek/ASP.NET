using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Piecyk.Models
{
    public class UserInfoModels
    {
        public string ID { get; set; }

        [Required, Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required, Display(Name = "Telefon"), DataType(DataType.PhoneNumber)]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Numer telefonu musi składać się z 9 cyfr.")]
        public int PhoneNo { get; set; }

        public string Email { get; set; }

        public virtual ICollection<CarModels> Car { get; set; } // można dodawać i usuwać elementy, nie posiada indexów
        // IEnumerable<> - tylko do odczytu; pozwala na użycie pętli for each
        // IList <> - dziedziczy po IColl i IEn' posiada indeksy
    }
}