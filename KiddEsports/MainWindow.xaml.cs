using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KiddEsports
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creates list for TeamDetails object.
        List<TeamDetails> teamList = new List<TeamDetails>();
        // Creates the FileManagement class to handle reading and writing to files.
        FileManagement file = new FileManagement();
        // Creates a team object for storing data for each team.
        TeamDetails team = new TeamDetails();

        bool isNewEntry = true;
        
        //Constructor which handles the initial setup of the form.
        public MainWindow()
        {
            InitializeComponent();
            SetupDataGrid();
        }
        // Method which gets the team objects from a file and uses it to populate the data grid.
        private void SetupDataGrid()
        {
            teamList = file.GetTeam();
            dgvTeamList.ItemsSource = teamList;
        }
        // Mathod that creates a new team object using the supplied data and repopulates the data grid with the updated list.
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (isNewEntry)
            {
                SaveNewEntry();
            }
            else
            {
                team.TeamName = txtTeamName.Text;
                team.PrimaryContact = txtPrimaryContact.Text;
                team.ContactPhone = txtPhoneNo.Text;
                team.ContactEmail = txtEmail.Text;
                team.CompPoints = txtCompPoints.Text;

                file.UpdateTeam(team);
                teamList = file.GetTeam();
                dgvTeamList.Items.Refresh();
            }
            ClearDataEntryField();
        }

        private void SaveNewEntry()
        {
            team.TeamName = txtTeamName.Text;
            team.PrimaryContact = txtPrimaryContact.Text;
            team.ContactPhone = txtPhoneNo.Text;
            team.ContactEmail = txtEmail.Text;
            team.CompPoints = txtCompPoints.Text;

            file.AddNewTeam(team);
            teamList = file.GetTeam();
            dgvTeamList.Items.Refresh();
        }

        private void ClearDataEntryField()
        {
            team.TeamName = "";
            team.PrimaryContact = "";
            team.ContactPhone = "";
            team.ContactEmail = "";
            team.CompPoints = "";
        }

        private void ClearTextBox()
        {
            txtTeamName.Text = "";
            txtPrimaryContact.Text = "";
            txtPhoneNo.Text = "";
            txtEmail.Text = "";
            txtCompPoints.Text = "";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void dgvTeamList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = dgvTeamList.SelectedIndex;

            if (id == -1)
            {
                return;
            }

            team = file.GetTeamByID(id);
            team.Id = id;

            txtTeamName.Text = team.TeamName;
            txtPrimaryContact.Text = team.PrimaryContact;
            txtPhoneNo.Text = team.ContactPhone;
            txtEmail.Text = team.ContactEmail;
            txtCompPoints.Text = team.CompPoints;

            isNewEntry = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure you wish to delete the {team.TeamName} team? \n(This action cannot be undone.)", "Delete team?",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                file.DeleteTeam(team);
                teamList = file.GetTeam();
                dgvTeamList.Items.Refresh();
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }
    }
}
