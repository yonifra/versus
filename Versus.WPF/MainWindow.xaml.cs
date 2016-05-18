using Versus.Common.Data;

namespace Versus.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateCompetitions();
            PopulateCategories();
            PopulateEntities();
        }

        private async void PopulateCompetitions()
        {
            var competitions = await FirebaseManager.Instance.GetAllCompetitions();

            foreach (var c in competitions)
            {
                CompetitionsList.Items.Add(c.Value);
            }
        }

        private async void PopulateCategories()
        {
            var categories = await FirebaseManager.Instance.GetAllCategories();

            foreach (var c in categories)
            {
                CategoriesList.Items.Add(c.Value);
            }
        }

        private async void PopulateEntities()
        {
            var entities = await FirebaseManager.Instance.GetAllEntities();

            foreach (var c in entities)
            {
                EntitiesList.Items.Add(c.Value);
            }
        }
    }
}
