using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using ubta.Common;

namespace ubta.xml.transformer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Program
    {
        private IXmlTransformer _transformer;
        public Program()
        {
            _transformer = new XmlTransformer();
        }

        private string loadXsl(){
            try
            {
                // load from a file
                string xslString = File.ReadAllText(@"transformer.xsl");
                return xslString;
            }
            catch(Exception e)
            {
                System.Console.WriteLine("Error accessing resources!\n" + e.Message +"\n" + e.StackTrace);
            }
            return null;
        }

        private string Transform()
        {
            try
            {
                string xslString = loadXsl();
                this._transformer.SetTransformer(xslString);
                string xmlPath = string.Format(@"{0}\ExecuteTest.SampleLib.xml", Constants.CONFIG_DIR);
                StreamReader xmlReader;
                xmlReader = new StreamReader(new FileStream(xmlPath, FileMode.Open));
                string xml = xmlReader.ReadToEnd();
                this._transformer.SetInput(xml);
                this._transformer.Transform();
                return this._transformer.GetOutput();
            }
            catch(Exception e)
            {
                System.Console.WriteLine("Error accessing resources!\n" + e.Message +"\n" + e.StackTrace);
            }
            return null;
        }

        public static void Main(string[] args)
        {
            Program p = new Program();
            string output = p.Transform();
            System.Console.WriteLine(output);
        }
    }

}
