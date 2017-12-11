using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using ubta.automation.ui;
using ubta.Common;

namespace ubta.automation.uiTestRunner
{
    class Program
    {
        private static string executable = Constants.ASSEMBLY_DIR + @"\ubta.automation.uiTest.exe";
        // picked from uitest project's xaml file defining the UI.

        private static string btnName = "ID_BOTTOM";
        private static string txtBoxName = "ID_TEXTBOX";
        static void Main(string[] args)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = executable;
            ubta.automation.ui.UIAutomationHelper uiaHelper = new ubta.automation.ui.UIAutomationHelper();
            uiaHelper.SetProcessInfo(psi);
            uiaHelper.StartProcess();
            uiaHelper.GetRootAutomationElement();
            var ae = uiaHelper.GetAutomationElement(btnName);
            uiaHelper.Invoke(ae);

            var rootSnap = uiaHelper.GetRootSnapShot() as Bitmap;
            rootSnap.Save(@"c:\temp\root_snap_orig.png");

            var snap = uiaHelper.GetSnapShot(ae) as Bitmap;
            snap.Save(@"c:\temp\ID_BOTTOM_snap.png");

            ae = uiaHelper.GetAutomationElement(txtBoxName);
            snap = uiaHelper.GetSnapShot(ae) as Bitmap;
            snap.Save(@"c:\temp\ID_TEXTBOX_snap_Orig.png");

            uiaHelper.SetContentValue(ae, "set by test runner");
            rootSnap = uiaHelper.GetRootSnapShot() as Bitmap;
            rootSnap.Save(@"c:\temp\root_snap_new.png");

            ae = uiaHelper.GetAutomationElement(txtBoxName);
            var newSnap = uiaHelper.GetSnapShot(ae) as Bitmap;
            newSnap.Save(@"c:\temp\ID_TEXTBOX_snap_New.png");

            uiaHelper.EndProcess();
        }
    }
}
