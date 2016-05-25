using GalaSoft.MvvmLight;

namespace Versus.WPF.ViewModels
{
    public class EntityViewModel : ViewModelBase
    {
        private string _competition;
        private int _votes;
        private string _description;
        private string _imageUrl;
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        public int Votes
        {
            get { return _votes; }
            set
            {
                _votes = value;
                RaisePropertyChanged();
            }
        }

        public string Competition
        {
            get { return _competition; }
            set
            {
                _competition = value;
                RaisePropertyChanged();
            }
        }
    }
}
