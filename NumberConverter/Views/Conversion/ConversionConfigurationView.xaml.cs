using KMA.APZRPMJ2018.NumberConverter.ViewModels;

namespace KMA.APZRPMJ2018.NumberConverter.Views.Conversion
{
    /// <summary>
    /// Interaction logic for ConversionConfigurationView.xaml
    /// </summary>
    public partial class ConversionConfigurationView
    {
        public ConversionConfigurationView(Models.ConversionUiModel conversion)
        {
            InitializeComponent();
            var conversionModel = new ConversionConfigurationViewModel(conversion);
            DataContext = conversionModel;
        }
    }
}
