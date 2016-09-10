using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using WpfCommon.ViewModels;

namespace WpfImageReceiver.ViewModels
{
    public class ImageViewModel : ViewModelBase
    {
        #region FIELDS
        Socket skt;
        Thread thread;
        TcpListener listener;
        #endregion

        #region PROPERTIES
        private string _localIPAddress;
        /// <summary>
        /// The IPv4 Address for this computer
        /// </summary>
        public string LocalIPAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_localIPAddress))
                {
                    _localIPAddress = GetIPAddress();
                }
                return _localIPAddress;
            }
        }

        private Image _receiverImage;
        /// <summary>
        /// The image received from the sender
        /// </summary>
        public Image ReceiverImage
        {
            get
            {
                return _receiverImage;
            }
            set
            {
                _receiverImage = value;
                OnPropertyChanged("ReceiverImage");
            }
        }


        //public BitmapImage ReceiverImage { get; set; }
        #endregion

        #region CONSTRUCTORS
        public ImageViewModel()
        {
            thread = new Thread(new ThreadStart(ReceiveImage));
            thread.IsBackground = true;
            thread.Start();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Listens for and receives an image from the sender and displays it
        /// </summary>
        private void ReceiveImage()
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(LocalIPAddress), 36999);
                listener.Start();
                skt = listener.AcceptSocket();
                using (var network = new NetworkStream(skt))
                {
                    ReceiverImage = Image.FromStream(network);
                }
                listener.Stop();
                if (skt.Connected)
                {
                    while (true)
                    {
                        ReceiveImage();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Gets the IPv4 Address for this machine
        /// </summary>
        /// <returns></returns>
        private string GetIPAddress()
        {
            return (from adr in Dns.GetHostAddresses(Dns.GetHostName()).AsQueryable()
                    where adr.AddressFamily == AddressFamily.InterNetwork
                    select adr.ToString()
                    ).FirstOrDefault();
        }
        #endregion
    }
}
