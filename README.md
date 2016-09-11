# CSharp_Communications_TCP
Demonstrates Tcp communications using TcpListener &amp; TcpClient classes

---

Developed with Visual Studio 2015 Community

---

####Console Apps

The console apps simply demonstrate sending text from TcpClient to TcpListener. The example sends 3 messages every second, the third instructs the listener to close.

|Sender App| Receiver App|
|----------|-------------|
|[BasicClient](https://github.com/Apollo013/CSharp_Communications_TCP/blob/master/BasicClient/Program.cs)|[BasicServer](https://github.com/Apollo013/CSharp_Communications_TCP/blob/master/BasicServer/Program.cs)|


####ReceiveRespond Apps are the same as above except a response is sent to the client

---

####Wpf Apps

The WPF apps allow you to open an image using 'OpenFileDialog' and display it on the 'senders' view. This view contains a 'send' button that sends the image to the receiver who then displays the image on it's view.

|Sender App| Receiver App|
|----------|-------------|
|[WpfImageSender](https://github.com/Apollo013/CSharp_Communications_TCP/blob/master/WpfImageSender/ViewModels/ImageViewModel.cs)|[WpfImageReceiver](https://github.com/Apollo013/CSharp_Communications_TCP/blob/master/WpfImageReceiver/ViewModels/ImageViewModel.cs)|


---

####DOT NET Classes/Features
|Comms|WPF|Streaming|Misc|
|-------|----------|------------------------------|------|
|TcpListener|MVVM|BinaryWriter|Image |
|TcpClient|ValueConverters|NetworkStream|BitmapImage|
|IPAddress|Custom Relay Commands (ICommand)|BufferedStream|StringBuilder|
|Dns|INotifyPropertyChanged|MemoryStream|Thread|

---

###Resources
|Title|Author|Website|
|-----|------|-------|
|[Using TCP Services](https://msdn.microsoft.com/en-us/library/k8azesy5(v=vs.110).aspx)||MSDN|
|[TCP level communication with C# .NET: the server](https://dotnetcodr.com/2016/03/02/tcp-level-communication-with-c-net-the-server/)|Andras Nemes|dotnetcodr.com|
|[TCP level communication with C# .NET: the client](https://dotnetcodr.com/2016/03/10/tcp-level-communication-with-c-net-the-client/)|Andras Nemes|dotnetcodr.com|
|[How to give System.Drawing.Image data to System.Windows.Media.ImageSource (WPF) to display?](https://social.msdn.microsoft.com/Forums/vstudio/en-US/833ca60f-6a11-4836-bb2b-ef779dfe3ff0/how-to-give-systemdrawingimage-data-to-systemwindowsmediaimagesource-wpf-to-display?forum=wpf)| | Visual Studio Forum |
|[How to send image on network using socket programming in C# - Part 1](https://www.youtube.com/watch?v=_AXVfzoWnks)| Mr Socket | YouTube |

