using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using WpfCommon.Commands;
using WpfCommon.ViewModels;

namespace WpfImageSender.ViewModels
{
    public class ImageViewModel : ViewModelBase
    {
        #region PROPERTIES
        private string _ipAddress;
        public string IPAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                OnPropertyChanged("IPAddress");
            }
        }

        #endregion

        #region METHODS
        private string GetIPAddress()
        {
            return (from adr in Dns.GetHostAddresses(Dns.GetHostName()).AsQueryable()
                    where adr.AddressFamily == AddressFamily.InterNetwork
                    select adr.ToString()
                    ).FirstOrDefault();
        }
        #endregion

        #region CONSTRUCTORS
        public ImageViewModel()
        {
            IPAddress = GetIPAddress();
        }
        #endregion

        #region 'SendImageCommand' COMMAND
        private ICommand _sendImageCommand;
        public ICommand SendImageCommand
        {
            get
            {
                if (_sendImageCommand == null)
                {
                    _sendImageCommand = new RelayCommand(c => SendImageToServer(), cdn => !string.IsNullOrEmpty(ImageFilePath));
                }
                return _sendImageCommand;
            }
        }

        private void SendImageToServer()
        {
            try
            {
                using (var client = new TcpClient(IPAddress, 36999))
                {
                    using (var network = client.GetStream())
                    {
                        using (var writer = new BinaryWriter(network))
                        {
                            using (var image = Image.FromFile(ImageFilePath))
                            {
                                using (var memory = new MemoryStream())
                                {
                                    image.Save(memory, image.RawFormat);
                                    byte[] data = memory.GetBuffer();
                                    writer.Write(data);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion
    }
}
