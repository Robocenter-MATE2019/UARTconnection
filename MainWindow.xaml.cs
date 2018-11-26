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
        public JoystickController Maincontroller;
        public VModel vmodel;
        public UARTConnection mainconnection;
        public string info;
        public MainWindow()
        {
            InitializeComponent();
            vmodel = new VModel(new Model());
            Maincontroller = new JoystickController();
            mainconnection = new UARTConnection();
        }
        public void timertick(object sender, EventArgs e)
        {
            Maincontroller.UpdateJoystick(vmodel);
            info = "AxisX: " + Model.vGM.axisX_p + "\n";//state.X + "\n";
            info += "AxisY: " + Model.vGM.axisY_p + "\n";
            info += "AxisZ: " + Model.vGM.JRZ_p + "\n";//state.Rz + ";";
            info += "Lever: " + Model.vGM.axisZ_p + "\n";//state.Z + ";";
            info += "Slighter:  " + Model.vGM.slighter_p + "\n"; //sligterP[0] + ";";
            info += "PointOfView:   " + Model.vGM.manipulator_p + "\n";
            for (int i = 0; i < 12; i++) info += "Key" + i + ": " + Maincontroller.GetButtons[i] + "\n";
            string message = Model.vGM.axisX_p + "-" + Model.vGM.axisY_p + "-" + Model.vGM.JRZ_p;
            mainconnection.UARTWrite(message);

        }
        
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            mainconnection.InitializePort();
            Maincontroller.InitializeJoystick(this);
            timer.Tick += new EventHandler(timertick);
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();
           
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Data_Label.Content = info;
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
    }
}
