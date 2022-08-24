using System;
using System.Collections.Generic;
using System.IO;
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

        public List<TeamDetails> GetTeam()
        {
            teamList.Clear();
            try
            {
                using (StreamReader reader = new StreamReader("TeamDetails.txt"))
                {
                    string line;
                    while (String.IsNullOrWhiteSpace(line = reader.ReadLine()) == false)
                    {
                        string[] temp = line.Split(",");
                        TeamDetails team = new TeamDetails(temp[0], temp[1], temp[2], temp[3], temp[4]);

                        teamList.Add(team);
                    }
                    return teamList;
                }
            }
            catch (Exception e)
            {
                return new List<TeamDetails>();
            }
        }

        public void UpdateTeam(TeamDetails team)
        {
            teamList[team.Id] = team;
            SaveDataToFile();
        }

        public void AddNewTeam(TeamDetails newTeam)
        {
            teamList.Add(newTeam);
            SaveDataToFile();
        }

        private void SaveDataToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("TeamDetails.txt"))
                {
                    foreach (var team in teamList)
                    {
                        writer.WriteLine($"{team.TeamName},{team.PrimaryContact},{team.ContactPhone},{team.ContactEmail},{team.CompPoints}");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
