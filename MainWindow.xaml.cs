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
        public string info;
        public MainWindow()
        {
            InitializeComponent();
            vmodel = new VModel(new Model());
            mainconnection = new UARTConnection();
        }
        public void timertick(object sender, EventArgs e)
        {
         
            byte[] message = new byte[4];
            message[0] = (byte)'*';
            message[1] = (byte)(Model.MotorPower*(Model.Direction));
            message[2] = (byte)Model.LightBrightness;
            message[3] = (byte)'-';
            mainconnection.UARTWrite(message);
            Model.SendingData = "MotorPower: " + Model.MotorPower + "\n" + "Direction: " + Model.Direction + "\n" + "LightBrightness: " + Model.LightBrightness;
            Data_Label.Content = info;
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
                Model.Direction = 1;
            }
            else if (e.Key == Key.S)
            {
                Model.Direction = -1;
            }
            else
            {
                Model.Direction = 0;
            }
        }

        private void MainWindow1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                Model.Direction = 0;
            }
            if (e.Key == Key.S)
            {
                Model.Direction = 0;
            }
            if (e.Key == Key.Q)
            {
                if(Model.MotorPower <= 100)Model.MotorPower += 10;
            }
            else if (e.Key == Key.E)
            {
                if(Model.MotorPower >= -100)Model.MotorPower -= 10;
            }
            if (e.Key == Key.R)
            {
                if(Model.LightBrightness <= 100)Model.LightBrightness += 10;
            }
            else if (e.Key == Key.F)
            {
                if (Model.LightBrightness >= 0) Model.LightBrightness -= 10;
            }
        }
    }
}
