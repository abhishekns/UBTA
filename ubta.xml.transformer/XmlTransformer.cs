using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace ubta.xml.transformer
{
    public class XmlTransformer : IXmlTransformer
    {
        private string _input;
        private string _transformer;
        private string _output;
        private Exception _lastError;
        public void SetInput(String input) { _input = input; }

        public void Transform() {
            XslCompiledTransform myXslTransform;
            myXslTransform = new XslCompiledTransform();
            try
            {
                using (TextReader tr = new StringReader(this._transformer))
                {
                    using (XmlReader xr = XmlReader.Create(tr))
                    {
                        myXslTransform.Load(xr);
                    }
                }
                using (StringReader sr = new StringReader(_input))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        using (StringWriter sw = new StringWriter())
                        {
                            myXslTransform.Transform(xr, null, sw);
                            _output = sw.ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _lastError = e;
                _output = "";
            }
        }

        public string GetOutput()
        {
            return _output;
        }

        public void SetTransformer(string transformer) {
            _transformer = transformer;
        }

        public Exception GetLastError()
        {
            return _lastError;
        }
    }
}
