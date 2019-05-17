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

        public string SpeedMode
        {
            get { return "SpeedMode: " + Model.SpeedMode; }
            set
            {
                try
                {
                    Model.SpeedMode = Convert.ToInt16(value);
                }
                catch (FormatException)
                {
                    Model.SpeedMode = 1;
                }
                finally
                {
                    OnPropertyChanged("SpeedMode");
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed1Brush"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed2Brush"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed3Brush"));
                }
            }
        }
        public string SendingData
        {
            get
            {
                return Model.SendingData;
            }
            set
            {
                Model.SendingData = value;
                OnPropertyChanged("SendingData");
            }
        }
        #region Brushes
        public Brush Speed1Brush => GetBrush(1);
        public Brush Speed2Brush => GetBrush(2);
        public Brush Speed3Brush => GetBrush(3);
        public Brush GetBrush(int speed)
        {
            if (model == null || speed > Model.SpeedMode)
            {
                return Brushes.White;
            }
            switch (Model.SpeedMode)
            {
                case 1: return Brushes.Lime;
                case 2: return Brushes.Gold;
                default: return Brushes.Firebrick;
            }
        }
        #endregion Brushes

        public event PropertyChangedEventHandler PropertyChanged; // Событие, которое нужно вызывать при изменении
        public void OnPropertyChanged(string propertyName)//RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//1
        }

        public VModel(Model model)
        {
            this.model = model;
        }
    }
}
