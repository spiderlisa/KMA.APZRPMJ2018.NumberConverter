using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using KMA.APZRPMJ2018.NumberConverter.Managers;
using KMA.APZRPMJ2018.NumberConverter.DBModels;
using KMA.APZRPMJ2018.NumberConverter.Models;
using KMA.APZRPMJ2018.NumberConverter.Properties;
using KMA.APZRPMJ2018.NumberConverter.Tools;

namespace KMA.APZRPMJ2018.NumberConverter.ViewModels
{
    class MainViewViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ConversionUiModel _selectedConversion;
        private ObservableCollection<ConversionUiModel> _conversions;
        #region Commands
        private ICommand _addConversionCommand;
        private ICommand _logOutCommand;
        #endregion
        #endregion

        #region Properties
        public ObservableCollection<ConversionUiModel> Conversions
        {
            get { return _conversions; }
        }
        public ConversionUiModel SelectedConversion
        {
            get { return _selectedConversion; }
            set
            {
                _selectedConversion = value;
                OnPropertyChanged();
            }
        }
        #region Commands
        public ICommand AddConversionCommand
        {
            get
            {
                return _addConversionCommand ?? (_addConversionCommand = new RelayCommand<object>(AddConversionExecute));
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return _logOutCommand ?? (_logOutCommand = new RelayCommand<object>(LogOutExecute));
            }
        }
        #endregion
        #endregion

        #region Constructor
        public MainViewViewModel()
        {
            PropertyChanged += OnPropertyChanged;
            FillConversions();
        }
        #endregion

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedConversion")
                OnConversionChanged(_selectedConversion);
        }

        private void FillConversions()
        {
            _conversions = new ObservableCollection<ConversionUiModel>();
            StationManager.CurrentUser.Conversions = StationManager.CurrentUser.Conversions.OrderBy(o => o.Number).ToList<Conversion>();
            List<Conversion> toDelete = new List<Conversion>();
            foreach (var conv in StationManager.CurrentUser.Conversions)
            {
                if (conv.RomanNumeralValue.Equals("UNDEFINED") || conv.RomanNumeralValue.Equals(""))
                {
                    toDelete.Add(conv);
                }
                else
                {
                    _conversions.Add(new ConversionUiModel(conv));
                }
            }
            if (_conversions.Count > 0)
            {
                _selectedConversion = Conversions[0];
            }

            foreach (var conv in toDelete)
            {
                StationManager.CurrentUser.Conversions.Remove(conv);
                DbManager.DeleteConversion(conv);
            }
        }

        public void AddConversionExecute(object o)
        {
            Conversion conv = new Conversion(StationManager.CurrentUser);
            DbManager.AddConversion(conv);
            ConversionUiModel conversion = new ConversionUiModel(conv);
            _conversions.Add(conversion);
            _selectedConversion = conversion;
        }

        public void LogOutExecute(object o)
        {
            StationManager.DeleteLastSerializedUsed();
            StationManager.CurrentUser = null;
            NavigationManager.Instance.Navigate(ModesEnum.SignIn);
        }

        #region EventsAndHandlers
        #region Loader
        internal event ConversionChangedHandler ConversionChanged;
        internal delegate void ConversionChangedHandler(ConversionUiModel conversion);

        internal virtual void OnConversionChanged(ConversionUiModel conversion)
        {
            ConversionChanged?.Invoke(conversion);
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion
    }
}
