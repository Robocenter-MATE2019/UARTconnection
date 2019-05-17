using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;


namespace UARTconnection
{
    public class VModel : INotifyPropertyChanged
    {
        Model model;

        public string SendingData
        {
            get
            {
                return model.SendingData;
            }
            set
            {
                model.SendingData = value;
                OnPropertyChanged("SendingData");
            }
        }
        public int MotorPower
        {
            get
            {
                return model.MotorPower;
            }
            set
            {
                model.MotorPower = value;
            }
        }
        public int LightBrightness
        {
            get
            {
                return model.LightBrightness;
            }
            set
            {
                model.LightBrightness = value;
            }
        }
        public int Direction
        {
            get { return model.Direction; }
            set { model.Direction = value; }
        }
        
        public event PropertyChangedEventHandler PropertyChanged; // Событие, которое нужно вызывать при изменении
        public void OnPropertyChanged(string propertyName)//RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//1
        }

        public VModel(Model model)
        {
            this.model = model;
            SendingData     = "NoData";
            Direction       = 0;
            LightBrightness = 0;
            MotorPower      = 0;
        }
    }
}
