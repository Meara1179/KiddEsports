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

        // Method which returns the ID of the selected team.
        public TeamDetails GetTeamByID(int ID)
        {
            return teamList[ID];
        }

        public List<TeamDetails> GetTeam()
        {
            teamList.Clear();
            try
            {
                // Creates the reader object which is used to read from files.
                using (StreamReader reader = new StreamReader("TeamDetails.txt"))
                {
                    string line;
                    while (String.IsNullOrWhiteSpace(line = reader.ReadLine()) == false)
                    {
                        // Used to parse the CSV file and create a new TeamDetails object.
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
        #region Methods
        // Method used to update an already created TeamDetails object via its ID and update the file.
        public void UpdateTeam(TeamDetails team)
        {
            try
            {
                teamList[team.Id] = team;
                SaveDataToFile();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Method used to create a new TeamDetails object and save it to the file.
        public void AddNewTeam(TeamDetails newTeam)
        {
            teamList.Add(newTeam);
            SaveDataToFile();
        }

        // Deletes a TeamDetails object and also removes the respective entry from the file.
        public void DeleteTeam(TeamDetails team)
        {
                teamList.RemoveAt(team.Id);
                SaveDataToFile();
        }
        #endregion
        // Method referenced by the previous methods and handles writing to the file.
        private void SaveDataToFile()
        {
            try
            {
                // Creates a new writer object which handles writing to the file.
                using (StreamWriter writer = new StreamWriter("TeamDetails.txt"))
                {
                    foreach (var team in teamList)
                    {
                        writer.WriteLine($"{team.TeamName},{team.PrimaryContact}," +
                            $"{team.ContactPhone},{team.ContactEmail},{team.CompPoints}");
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
