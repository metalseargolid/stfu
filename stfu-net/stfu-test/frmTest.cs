﻿/**The MIT License (MIT)
* 
* Copyright (c) 2015 metalseargolid
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
**/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using stfu_shared;

namespace stfu_tray
{
    public partial class frmTest : Form
    {
        private ProcessStartInfo mVboxInfo;
        private readonly string mGuestPropertyArg = "guestproperty set alextest "; // note the trailing space

        public frmTest()
        {
            InitializeComponent();
        
            // Check if VBoxControl.exe exists
            string path = null;
            if (!(System.IO.File.Exists(path = string.Format("{0}\\VBoxControl.exe", System.Environment.GetFolderPath(Environment.SpecialFolder.System)))))
                throw new System.IO.IOException("VBoxControl.exe not found. Make sure this is a VirtualBox guest and that you have Guest Additions installed.");

            // Create the process info
            this.mVboxInfo = new ProcessStartInfo(path, this.mGuestPropertyArg);
            this.mVboxInfo.CreateNoWindow = true;
            this.mVboxInfo.LoadUserProfile = false;
            this.mVboxInfo.UseShellExecute = true;

        }

        private void btnPush_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtProp.Text))
            {
                MessageBox.Show("You need to enter a string into the box (no whitespace).");
                return;
            }

            mVboxInfo.Arguments += string.Format("\"{0}\"", this.txtProp.Text);
            Process p = Process.Start(mVboxInfo);

            // change mVBox info arguments back to the default
            mVboxInfo.Arguments = this.mGuestPropertyArg;
        }

        private void btnLogWindow_Click(object sender, EventArgs e)
        {
            using (frmError fErr = new frmError())
                fErr.ShowDialog();
        }
    }
}
