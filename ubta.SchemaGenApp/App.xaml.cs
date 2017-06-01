#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : App.xaml.cs
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
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace ubta.SchemaGenApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string[] args;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            args = e.Args;
            this.Activated += new EventHandler(App_Activated);
        }

        void App_Activated(object sender, EventArgs e)
        {
            (MainWindow as Window1).Args = args;
        }

        void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
        }
    }
}