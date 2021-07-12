using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COVID_19_Vaccination_System.Models {
    public class UserModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMBG { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}