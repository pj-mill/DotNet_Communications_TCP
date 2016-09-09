using System.Windows;
using WpfImageSender.ViewModels;
using WpfImageSender.Views;

namespace WpfImageSender
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
