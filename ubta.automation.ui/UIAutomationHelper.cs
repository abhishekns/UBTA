using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ubta.automation.ui
{
    
    public class UIAutomationHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        static uint DELETE = 0x00010000;
        static uint READ_CONTROL = 0x00020000;
        static uint WRITE_DAC = 0x00040000;
        static uint WRITE_OWNER = 0x00080000;
        static uint SYNCHRONIZE = 0x00100000;
        static uint END = 0xFFF; //if you have Windows XP or Windows Server 2003 you must change this to 0xFFFF
        static uint PROCESS_ALL_ACCESS = (DELETE | READ_CONTROL | WRITE_DAC | WRITE_OWNER | SYNCHRONIZE | END);

        private static int SLEEP_TIME = 50; // ms
        private static int ITERATIONS = 100;
        private static int FOCUS_WAIT_TIME = 100; // ms

        
        private Process _p;
        private ProcessStartInfo _psi;

        public void EndProcess()
        {
            if(_p != null)
            {
                if(!_p.HasExited)
                {
                    _p.Kill();
                    _p.Close();
                    _p.Dispose();
                    _p = null;
                }
            }
        }

        private AutomationElement _rootAutomationElement;
        private AutomationElement _current;

        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        public void SetProcessInfo(ProcessStartInfo psi)
        {
            _psi = psi;
        }

        public void StartProcess()
        {
            _p = Process.Start(_psi);
            int result = OpenProcess(PROCESS_ALL_ACCESS, false, _p.Id);
            Console.WriteLine("Open process result: {0}", result);
        }

        public void GetRootAutomationElement()
        {
            int count = 0;
            do
            {
                _rootAutomationElement = AutomationElement.RootElement.FindFirst
                    (TreeScope.Children, new PropertyCondition(AutomationElement.ProcessIdProperty,
                    _p.Id));

                ++count;
                Thread.Sleep(SLEEP_TIME);
            }
            while (_rootAutomationElement == null && count < ITERATIONS);
        }

        public AutomationElement GetAutomationElement(string name)
        {
            var ae = _rootAutomationElement.FindFirst(TreeScope.Descendants, 
                new PropertyCondition(AutomationElement.AutomationIdProperty, name));
            return ae;
        }

        public object GetSnapShot(AutomationElement ae)
        {
            Bitmap rootBmp = GetRootSnapShot() as Bitmap;
            System.Drawing.Rectangle r = new Rectangle();
            r.X = (int) (ae.Current.BoundingRectangle.Left - _rootAutomationElement.Current.BoundingRectangle.Left);
            r.Y = (int)(ae.Current.BoundingRectangle.Top - _rootAutomationElement.Current.BoundingRectangle.Top);
            r.Width = (int)ae.Current.BoundingRectangle.Width;
            r.Height = (int)ae.Current.BoundingRectangle.Height;
            Bitmap bmp = rootBmp.Clone(r, rootBmp.PixelFormat);
            //bmp.Save(@"c:\temp\part-screenshot.bmp");
            return bmp;
        }

        public object GetRootSnapShot()
        {
            //_rootAutomationElement.SetFocus();
            Bitmap bmp = new Bitmap((int)_rootAutomationElement.Current.BoundingRectangle.Width,
                (int)_rootAutomationElement.Current.BoundingRectangle.Height);
            Graphics memoryGraphics = Graphics.FromImage(bmp);
            IntPtr dc = memoryGraphics.GetHdc();
            bool success = PrintWindow(new IntPtr(_rootAutomationElement.Current.NativeWindowHandle), dc, 0);
            memoryGraphics.ReleaseHdc(dc);
            //bmp.Save(@"c:\temp\screenshot.bmp");
            return bmp;
        }

        public void Invoke(AutomationElement ae)
        {
            var pattern = ae.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            pattern.Invoke();
        }

        public object GetPropertyValue(AutomationElement ae, AutomationProperty ap)
        {
            return ae.GetCurrentPropertyValue(ap);
        }

        public void SetContentValue(AutomationElement ae, object value)
        {
            if (ae.Current.IsEnabled && ae.Current.IsKeyboardFocusable && value != null)
            {
                object pattern;
                if (ae.TryGetCurrentPattern(ValuePattern.Pattern, out pattern))
                {
                    ae.SetFocus();
                    Thread.Sleep(FOCUS_WAIT_TIME);
                    SendKeys.SendWait("^{HOME}");   // Move to start of control
                    SendKeys.SendWait("^+{END}");   // Select everything
                    SendKeys.SendWait("{DEL}");     // Delete selection
                    SendKeys.SendWait(value.ToString());
                }
            }
            else
            {
                // either combo box or list box
                AutomationPattern automationPatternFromElement = GetSpecifiedPattern(ae, "ExpandCollapsePatternIdentifiers.Pattern");

                ExpandCollapsePattern expandCollapsePattern = ae.GetCurrentPattern(automationPatternFromElement) as ExpandCollapsePattern;

                expandCollapsePattern.Expand();
                expandCollapsePattern.Collapse();

                AutomationElement listItem = ae.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, value.ToString()));

                automationPatternFromElement = GetSpecifiedPattern(listItem, "SelectionItemPatternIdentifiers.Pattern");

                SelectionItemPattern selectionItemPattern = listItem.GetCurrentPattern(automationPatternFromElement) as SelectionItemPattern;
                if(null != selectionItemPattern)
                {
                    selectionItemPattern.Select();
                }
                
            }
        }

        private AutomationPattern GetSpecifiedPattern(AutomationElement ae, string patternName)
        {
            AutomationPattern[] supportedPattern = ae.GetSupportedPatterns();

            foreach (AutomationPattern pattern in supportedPattern)
            {
                if (pattern.ProgrammaticName == patternName)
                    return pattern;
            }

            return null;
        }
    }
}
