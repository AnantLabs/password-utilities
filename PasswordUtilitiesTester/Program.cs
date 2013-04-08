// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;
using System.Windows.Forms;

namespace PasswordUtilitiesTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UtilitiesTester());
        }
    }
}