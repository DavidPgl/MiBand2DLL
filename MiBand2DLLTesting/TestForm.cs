﻿using System;
using System.Windows.Forms;
using MiBand2DLL;

namespace MiBand2DLLTesting
{
    public partial class Form1 : Form
    {
        private int _numberOfChanges;

        public Form1()
        {
            InitializeComponent();
        }

        private async void ConnectBandButtonClicked(object sender, EventArgs e)
        {
            MiBand2.DeviceConnectionChanged += ConnectionStatusChanged;
            await MiBand2.ConnectBandAsync();
            await MiBand2.AuthenticateBandAsync();
        }

        private void DisconnectButtonClicked(object sender, EventArgs e)
        {
            MiBand2.DisconnectBand();
            MiBand2.DeviceConnectionChanged -= ConnectionStatusChanged;
        }

        private void CheckConnectionButtonClicked(object sender, EventArgs e)
        {
            textBox2.Text = MiBand2.Connected.ToString();
        }

        private void ConnectionStatusChanged(bool isConnected)
        {
            isConnectedTextBox.Text = isConnected.ToString();
            numberBox.Text = (++_numberOfChanges).ToString();
        }


        private async void GetHRButtonClicked(object sender, EventArgs e)
        {
            await MiBand2.StartMeasurementAsync();
            MiBand2.SubscribeToHeartRateChange(OnHeartRateChange);

            void OnHeartRateChange(int newHeartRate)
            {
                textBox1.Text = newHeartRate.ToString();
            }
        }

        private async void AskForTouchButtonClicked(object sender, EventArgs e) => await MiBand2.AskUserForTouchAsync();

        private async void StopMeasurementButtonClicked(object sender, EventArgs e)
        {
            await MiBand2.StopMeasurementAsync();
        }
    }
}