using System.Windows;
using WpfImageReceiver.ViewModels;
using WpfImageReceiver.Views;

namespace WpfImageReceiver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ImageView app = new ImageView();
            ImageViewModel dataContext = new ImageViewModel();
            app.DataContext = dataContext;
            app.Show();
        }
    }
}
