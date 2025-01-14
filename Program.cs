﻿using System;
using System.IO;
using System.Windows.Forms;

namespace Projekt
{
    internal static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\pokladna.db"))
            {
                if (MessageBox.Show("Databáze nebyla nenalezena\nPřejete si vytvořit novou?", "Chyba databáze", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                     return;
                }
            }
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
} 
