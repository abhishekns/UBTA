using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using ubta.automation.ui;
using ubta.Common;

namespace ubta.automation.uiTestRunner
{
    class Program
    {
        private static string executable = Constants.ASSEMBLY_DIR + @"\ubta.automation.uiTest.exe";

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

            //ae = uiaHelper.GetAutomationElement(txtBoxName);
            //uiaHelper.SetContentValue(ae, "set by test runner");

            var snap = uiaHelper.GetSnapShot(ae);

            uiaHelper.EndProcess();
        }
    }
}
