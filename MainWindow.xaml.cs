using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Windows.Threading;

namespace UARTconnection
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public VModel vmodel;
        public UARTConnection mainconnection;

        public MainWindow()
        {
            InitializeComponent();
            vmodel = new VModel(new Model());
            mainconnection = new UARTConnection();
            DataContext = vmodel;
        }
        public void timertick(object sender, EventArgs e)
        {
         
            byte[] message = new byte[4];
            message[0] = (byte)'*';
            message[1] = (byte)(vmodel.MotorPower*(vmodel.Direction));
            message[2] = (byte)vmodel.LightBrightness;
            message[3] = (byte)'-';
            mainconnection.UARTWrite(message);
            vmodel.SendingData = "MotorPower: " + vmodel.MotorPower + "\n" + "Direction: " + vmodel.Direction + "\n" + "LightBrightness: " + vmodel.LightBrightness;
        }
       
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(timertick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                Button1.Content = "Start";
            }
            else
            {
                mainconnection.InitializePort();
                timer.Start();
                Button1.Content = "Stop";
            }
        }

        private void MainWindow1_Closed(object sender, EventArgs e)
        {
            mainconnection.UARTDisconnect();
            Environment.Exit(0);
        }

        private void TextBox_COMport_TextChanged(object sender, TextChangedEventArgs e)
        {
            int port;
            if (Int32.TryParse(TextBox_COMport.Text, out port))
            {
                UARTModel.COMport = "COM" + TextBox_COMport.Text;
            }
            else
            {
                Console.WriteLine("Это не НОРМАЛЬНЫЙ ввод!!");
            }
        }

        private void PadioButton_115200_Checked(object sender, RoutedEventArgs e)
        {
            UARTModel.BaudRate = 115200;
        }

        private void PadioButton_9600_Checked(object sender, RoutedEventArgs e)
        {
            UARTModel.BaudRate = 9600;
        }

        private void MainWindow1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                vmodel.Direction = 1;
            }
            else if (e.Key == Key.S)
            {
                vmodel.Direction = -1;
            }
            else
            {
                vmodel.Direction = 0;
            }
        }

        private void MainWindow1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                vmodel.Direction = 0;
            }
            if (e.Key == Key.S)
            {
                vmodel.Direction = 0;
            }
            if (e.Key == Key.Q)
            {
                if(vmodel.MotorPower <= 100)vmodel.MotorPower += 10;
            }
            else if (e.Key == Key.E)
            {
                if(vmodel.MotorPower >= -100)vmodel.MotorPower -= 10;
            }
            if (e.Key == Key.R)
            {
                vmodel.LightBrightness = 1;
            }
            else if (e.Key == Key.F)
            {
                vmodel.LightBrightness = 0;
            }
        }
    }
}
