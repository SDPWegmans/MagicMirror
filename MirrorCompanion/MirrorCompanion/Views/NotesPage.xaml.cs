using MagicMirror.Web;
using MirrorCompanion.Notifications;
using MirrorCompanion.Services.DTO;
using MirrorCompanion.Services.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MirrorCompanion.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotesPage : Page
    {
        #region Class Variables
        const string note_base_uri = "http://mirrorservice.azurewebsites.net/api/notes";
        #endregion
        public NotesPage()
        {
            this.InitializeComponent();
        }

        #region Page Events
        /// <summary>
        /// New Note event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewNoteButton_Click(object sender, RoutedEventArgs e)
        {
            WriteNote(NewNoteTextBox.Text);
        }

        /// <summary>
        /// Clear all notes event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearAllNotesButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAllNotes();
        }

        /// <summary>
        /// Page has loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCurrentNoteDisplay();
        }

        /// <summary>
        /// Updates the text on the current note
        /// </summary>
        private async void UpdateCurrentNoteDisplay()
        {
            var notes = (await GetAllNotes());
            if (notes.Count<Note>() > 0)
                CurrentNoteTextBlock.Text = string.Format("\"{0}\"", notes.Last<Note>().NoteText);
            else
                CurrentNoteTextBlock.Text = "There is no current note set. :(";
        }

        
        #endregion

        #region Async Data Calls
        /// <summary>
        /// Displays a popup on the UI with a messagqe and title
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        private async void ShowPopupMessage(string msg, string title = "Magic Mirror Companion")
        {
            MessageDialog dialog = new MessageDialog(msg, title);
            await dialog.ShowAsync();
        }

        /// <summary>
        /// Remove all notes
        /// </summary>
        private async void ClearAllNotes()
        {
            ServiceManager noteGetServiceManager =
                new ServiceManager(new Uri(note_base_uri));

            var notes = await noteGetServiceManager.CallService<Note[]>();

            int numDeleted = 0;

            foreach (var note in notes)
            {
                NoteRequest noteReq = new NoteRequest() { Id = note.Id, NoteText = note.NoteText };
                ServiceManager noteServiceManager = new ServiceManager(new Uri(note_base_uri + "/" + noteReq.Id));

                if (await noteServiceManager.CallDeleteService<NoteRequest>(noteReq))
                    numDeleted++;
            };
            ShowPopupMessage(string.Format("Deleted {0} notes from the DB!", numDeleted));

            UpdateCurrentNoteDisplay();
        }

        /// <summary>
        /// Get all the notes
        /// </summary>
        /// <returns>Notes task array</returns>
        private async Task<Note[]> GetAllNotes()
        {
            ServiceManager noteServiceManager =
                new ServiceManager(new Uri(note_base_uri));

            var notes = await noteServiceManager.CallService<Note[]>();
            return notes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="noteText"></param>
        private async void WriteNote(string noteText)
        {
            NoteRequest noteReq = new NoteRequest()
            {
                Id = 2,
                NoteText = noteText
            };//"{\"Id\": 3,\"NoteText\": \"" + noteText + "\"}";

            ServiceManager noteManager = new ServiceManager(new Uri(note_base_uri));
            bool successfulCall = await noteManager.CallPOSTService<NoteRequest>(noteReq);

            if (successfulCall)
                ShowPopupMessage(string.Format("Note successfully updated!\rSet to: {0}", noteText), "Note Update");
            else
                ShowPopupMessage("Note was not able to be updated at this time.", "Note Update");

            UpdateCurrentNoteDisplay();
            NewNoteTextBox.Text = "";

            //Remind the user to set a note every day! <3
            NotificationManager.ScheduleNotification("Don't forget to update your daily Note!", DateTime.Now.AddDays(1), NotificationType.Toast);
            //TODO: Pull schedule info from config
        }
        #endregion
    }
}