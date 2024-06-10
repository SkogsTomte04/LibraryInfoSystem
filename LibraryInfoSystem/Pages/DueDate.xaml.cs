using LibraryInfoSystem.Tools;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LibraryInfoSystem.Pages
{
    public partial class DueDate : Page
    {
        private MongoHandler mongoHandler = new MongoHandler(DataType.Duedate);

        // Rename the property to reflect the MongoDB collection name
        public ObservableCollection<DataBaseDuedate> DuedateGames { get; set; }

        public DueDate()
        {
            InitializeComponent();
            // Initialize the collection
            DuedateGames = new ObservableCollection<DataBaseDuedate>();
            DataContext = this;
            LoadDueDateData();
        }

        private void LoadDueDateData()
        {
            // Retrieve due date data from MongoDB and add it to DuedateGames collection
            DuedateGames.Clear();
            foreach (var dueDate in mongoHandler.duedate)
            {
                DuedateGames.Add(dueDate);
            }
        }
    }
}