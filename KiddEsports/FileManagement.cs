using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiddEsports
{
    public class FileManagement
    {
        // List to store the team details from the file.
        List<TeamDetails> teamList = new List<TeamDetails>();

        public TeamDetails GetTeamByID(int ID)
        {
            return teamList[ID];
        }
        public List<TeamDetails> GetTeam();

    }
}
