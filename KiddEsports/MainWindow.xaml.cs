using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Numerics;
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
        #region Setup
        // Creates list for TeamDetails object.
        List<TeamDetails> teamList = new List<TeamDetails>();
        // Creates the FileManagement class to handle reading and writing to files.
        FileManagement file = new FileManagement();
        // Creates a team object for storing data for each team.
        TeamDetails team = new TeamDetails();

        // Sets the isNewEntry bool to true.
        bool isNewEntry = true;

        // Creates a new SoundPlayer object which is used to play wav files on events.
        SoundPlayer player = new SoundPlayer();
        string soundButtonHover = "Button_Hover.wav";
        string soundButtonSave = "Button_Save.wav";

        //Constructor which handles the initial setup of the form.
        public MainWindow()
        {
            InitializeComponent();
            SetupDataGrid();
        }

        #endregion
        #region Events
        // Mathod that creates a new team object using the supplied data and repopulates
        // the data grid with the updated list.
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Creates a new TeamDetails object if the isNewEntry bool is true.
            if (isNewEntry)
            {
                SaveNewEntry();

                player.SoundLocation = soundButtonSave;
                player.Play();
            }
            // Updates an existing TeamDetails object with the new data.
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

                player.SoundLocation = soundButtonSave;
                player.Play();
            }
            // Clears the data entry fields to ensure left over data isn't accidently saved to a new object.
            ClearDataEntryField();
            // Clears the text boxes after saving.
            ClearTextBox();
        }

        // Allows selecting a team through the datagrid and passes the ID, allowing the selected object to be modified.
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
        // Deletes the selected TeamDetails object.
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Creates a YesNo textbox asking the user if the wish to delete the object.
            // Cancels out if No is selected.
            if (MessageBox.Show($"Are you sure you wish to delete the {team.TeamName} team? " +
                $"\n(This action cannot be undone.)", "Delete team?",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            // If yes is selected, deletes the selected TeamDetails object.
            else
            {
                file.DeleteTeam(team);
                teamList = file.GetTeam();
                dgvTeamList.Items.Refresh();
            }
        }

        // Clears the text boxes and sets the isNewEntry bool to true.
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
            isNewEntry = true;
        }

        // Plays the Button_Hover.wav sound file when the user's mouse enters a button.
        private void btnNew_MouseEnter(object sender, MouseEventArgs e)
        {
            player.SoundLocation = soundButtonHover;
            player.Play();
        }
        // Shuts down the application.
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
        #region Methods
        private void SetupDataGrid()
        {
            teamList = file.GetTeam();
            dgvTeamList.ItemsSource = teamList;
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
        #endregion
        #region Helpers
        // Method which gets the team objects from a file and uses it to populate the data grid.

        // Creates a new TeamDetails object when called.

        // Clears the data entry fields when called.
        private void ClearDataEntryField()
        {
            team.TeamName = "";
            team.PrimaryContact = "";
            team.ContactPhone = "";
            team.ContactEmail = "";
            team.CompPoints = "";
        }

        // Clears the text boxes when called.
        private void ClearTextBox()
        {
            txtTeamName.Text = "";
            txtPrimaryContact.Text = "";
            txtPhoneNo.Text = "";
            txtEmail.Text = "";
            txtCompPoints.Text = "";
        }


        #endregion
    }
}
