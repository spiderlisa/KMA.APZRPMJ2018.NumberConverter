using System.Windows;
using KMA.APZRPMJ2018.NumberConverter.Managers;

namespace KMA.APZRPMJ2018.NumberConverter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            StationManager.SerializeLastUser();
        }
    }

}
