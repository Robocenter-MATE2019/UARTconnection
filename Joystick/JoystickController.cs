using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Interop;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using WebCam_Capture;
using System.Diagnostics;
using System.IO;

namespace UARTconnection
{
    public class JoystickController
    {
        private Device joystick;
        private double SpeedK = 0.25;
        private bool NoneJoystick = false;
        private int[] Buttons = new int[12];

        public int[] GetButtons
        {
            get
            {
                return Buttons;
            }
        }
        public bool GetJoystick
        {
            get { return NoneJoystick; }
        }
        public void InitializeJoystick(Window window)
        {
            foreach (DeviceInstance di in
            Manager.GetDevices(
            DeviceClass.GameControl,
            EnumDevicesFlags.AttachedOnly))
            {
                joystick = new Device(di.InstanceGuid);
                break;
            }

            if (joystick == null)
            {

                //Throw exception if joystick not found.
                NoneJoystick = true;
                //throw new Exception("No joystick found.");
            }
            if (!NoneJoystick)
            {
                foreach (DeviceObjectInstance doi in joystick.Objects)
                {
                    if ((doi.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                    {
                        joystick.Properties.SetRange(
                            ParameterHow.ById,
                            doi.ObjectId,
                            new InputRange(-100, 100));
                    }
                }

                //Set joystick axis mode absolute.
                joystick.Properties.AxisModeAbsolute = true;

                //set cooperative level.
                //joystick.SetCooperativeLevel()
                joystick.SetCooperativeLevel(
                    new WindowInteropHelper(window).Handle,
                    CooperativeLevelFlags.NonExclusive |
                    CooperativeLevelFlags.Background);

                //Acquire devices for capturing.
                joystick.Acquire();
            }
        }
        public void UpdateJoystick(VModel Vmodel)
        {

            JoystickState state = joystick.CurrentJoystickState;//M

            //Capture Position.C>
            Model.vGM.button_data1 = 0;
            Model.vGM.button_data2 = 0;
            byte[] buttons = state.GetButtons();
            int[] sligterP = state.GetSlider();
            int[] viewP = state.GetPointOfView();
            for (int i = 0; i < 12; i++)
            {
                if (buttons[i] != 0)
                {
                    Buttons[i] = 1;
                }
                else
                {
                    Buttons[i] = 0;
                }
            }
            //C<
            //костыль для изменения кнопок блютуза и инвертирования камер
            int change = Buttons[0];
            Buttons[0] = Buttons[1];
            Buttons[1] = change;
            change = Buttons[3];
            Buttons[3] = Buttons[11];
            Buttons[11] = change;

            for (int i = 0; i <= 7; i++)//C>
            {
                if ((Buttons[i] == 1))
                {
                    Model.vGM.button_data1 = (sbyte)(((int)Model.vGM.button_data1) | (1 << i));
                }
            }
            for (int i = 8; i < 12; i++)
            {
                if ((Buttons[i] == 1))
                {
                    Model.vGM.button_data2 = (sbyte)(((int)Model.vGM.button_data2) | (1 << (i - 8)));
                }
            }

            if (viewP[2] == 0) viewP[0] = 0;
            Model.vGM.manipulator_p = (sbyte)viewP[0];
            if (Model.vGM.manipulator_p == (sbyte)40) Model.vGM.manipulator_p = (sbyte)1;
            if (Model.vGM.manipulator_p == (sbyte)120) Model.vGM.manipulator_p = (sbyte)3;
            if (Model.vGM.manipulator_p == (sbyte)80) Model.vGM.manipulator_p = (sbyte)2;
            if (Model.vGM.manipulator_p == (sbyte)0) Model.vGM.manipulator_p = (sbyte)4;
            if (Model.vGM.manipulator_p == (sbyte)-1) Model.vGM.manipulator_p = 0;

            if (Buttons[6] == 1)
            {
                Vmodel.SpeedMode = "1";
                SpeedK = 0.25;
            }
            if (Buttons[5] == 1)
            {
                Vmodel.SpeedMode = "2";
                SpeedK = 0.5;
            }
            if (Buttons[4] == 1)
            {
                Vmodel.SpeedMode = "3";
                SpeedK = 1.0;
            }
            
            Model.vGM.axisY_p = (sbyte)Math.Round(state.X * SpeedK);
            Model.vGM.axisX_p = (sbyte)Math.Round(state.Y * SpeedK * -1);
            Model.vGM.axisW_p = (sbyte)Math.Round(state.Z * SpeedK);
            Model.vGM.axisZ_p = (sbyte)Math.Round(state.Rz * SpeedK);

            if (sligterP[0] > -50 && sligterP[0] < 50) sligterP[0] = 0;
            Model.vGM.slighter_p = (sbyte)(sligterP[0]);//V<
        }
    } 
}
