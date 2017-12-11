using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ubta.Common;

namespace ubta.xml.transformer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IXmlTransformer _transformer;
        public MainWindow()
        {
            InitializeComponent();
            _transformer = new XmlTransformer();
            LoadTransformer(_transformer);
        }

        private void LoadTransformer(IXmlTransformer _transformer)
        {
            try
            {
                string xslString = ubta.xml.transformer.Properties.Resources.transformer;
                xsl.Text = xslString;
                _transformer.SetTransformer(xslString);
                string xmlPath = string.Format(@"{0}\ExecuteTest.SampleLib.xml", Constants.CONFIG_DIR);
                StreamReader xmlReader;
                xmlReader = new StreamReader(new FileStream(xmlPath, FileMode.Open));
                string xml = xmlReader.ReadToEnd();
                xmlIn.Text = xml;
                _transformer.SetInput(xml);
                _transformer.Transform();
                transOut.Text = _transformer.GetOutput();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error accessing resources!\n" + e.Message +"\n" + e.StackTrace);
            }
        }
    }
}
