using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfCommon.Commands;
using WpfCommon.Observables;

namespace WpfCommon.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        #region CLOSE WINDOW COMPONENTS 
        private ICommand _closeWindowCommand;
        public ICommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null)
                {
                    _closeWindowCommand = new RelayCommand(c => CloseWindow());
                }
                return _closeWindowCommand;
            }
        }


        /// <summary>
        /// Closes the current window
        /// </summary>
        protected virtual void CloseWindow()
        {
            CurrentWindow().Close();
        }

        #endregion

        #region IMAGE DIALOG COMPONENTS
        private string _imageFilePath;
        public string ImageFilePath
        {
            get { return _imageFilePath; }
            set
            {
                _imageFilePath = value;
                OnPropertyChanged("ImageFilePath");
                ImageFileChanged?.Invoke(this, new EventArgs());
            }
        }


        private ICommand _openImageFileDialogCommand;
        public ICommand OpenImageFileDialogCommand
        {
            get
            {
                if (_openImageFileDialogCommand == null)
                {
                    _openImageFileDialogCommand = new RelayCommand(c => OpenImageFileDialog());
                }
                return _openImageFileDialogCommand;
            }
        }

        public EventHandler ImageFileChanged;

        /// <summary>
        /// Opens file dialog window and assigns selected file value to 'FilePath property;
        /// </summary>
        protected virtual void OpenImageFileDialog()
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
            dialog.DefaultExt = ".jpg";

            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                ImageFilePath = dialog.FileName;
            }
        }
        #endregion

        #region ABSTRACT

        #endregion

        #region COMMON
        /// <summary>
        /// Gets the current window object
        /// </summary>
        /// <returns></returns>
        public Window CurrentWindow()
        {
            return (from w in Application.Current.Windows.OfType<Window>()
                    where w.IsActive
                    select w as Window).FirstOrDefault();
        }
        #endregion

    }
}
