using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddEsports
{
    // Class which is used to hold all the relevant details for each team.
    public class TeamDetails
    {
        public int Id;

        public string TeamName { get; set; }
        public string PrimaryContact { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string CompPoints { get; set; }

        // Blank constructor which allows the creation of a blank TeamDetails object.
        public TeamDetails()
        {

        }

        // Constructor which allows the creation of an object with all
        // properties filled with Comp points being optional.
        public TeamDetails(string teamName, string primaryContact, string contactPhone, 
            string contactEmail, string compPoints = "")
        {
            TeamName = teamName;
            PrimaryContact = primaryContact;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            CompPoints = compPoints;
        }
    }
}
