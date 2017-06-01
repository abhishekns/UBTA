#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Window1.xaml.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     Copyright (C) Siemens AG 2010-2010 All Rights Reserved
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Collections;
using ubta.SchemaGeneration;

namespace ubta.SchemaGenApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public string[] Args
        {
            set
            {
                if (null == myAsv)
                {
                    if (value.Length > 0)
                    {
                        SetFile(value[0]);
                    }
                }
            }
        }

        public void SetFile(string fname)
        {
            myAsv = new AssemblyViewModel(fname);
            treeView1.DataContext = myAsv;
        }

        public Window1()
        {
            InitializeComponent();
        }

        private AssemblyViewModel myAsv = null;

        private void openmenu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Assembly files (*.dll)|*.dll";
            bool? t = ofd.ShowDialog();
            if (t.HasValue ? t.Value : false)
            {
                SetFile(ofd.SafeFileName);
            }
        }

        private void savemenu_Click(object sender, RoutedEventArgs e)
        {
            Hashtable ht = myAsv.GetChecked();
            SchemaWriter sw = new SchemaWriter(myAsv.Name+".xsd", myAsv.GetChecked(), myAsv.Name +".dll");
            sw.WriteSchema();
        }
    }
}