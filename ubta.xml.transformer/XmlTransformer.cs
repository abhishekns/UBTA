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
                        using (TextReader xtr = new StringReader(this._input))
                        {
                            using (XmlReader ixr = XmlReader.Create(xtr))
                            {
                                StringBuilder sb = new StringBuilder();
                                using (TextWriter tw = new StringWriter(sb))
                                {
                                    using (XmlWriter xw = XmlWriter.Create(tw))
                                    {
                                        myXslTransform.Load(xr);
                                        myXslTransform.Transform(ixr, xw);
                                        _output = sb.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
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
