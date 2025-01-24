﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Projekt.BasicTheme;

namespace Projekt.Forms
{
    public partial class OthersForm : Form
    {
        public OthersForm()
        {
            InitializeComponent();
            ReallyCenterToScreen(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DWMNCRENDERINGPOLICY renderingPolicy = DWMNCRENDERINGPOLICY.DWMNCRP_DISABLED;
            int hr;
            hr = DwmSetWindowAttribute(Handle, DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, renderingPolicy, sizeof(DWMNCRENDERINGPOLICY));
            if (hr != 0)
            {
                throw Marshal.GetExceptionForHR(hr);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
