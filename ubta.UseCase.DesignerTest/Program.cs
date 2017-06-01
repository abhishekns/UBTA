using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ubta.UseCase.Designer;
using System.Xml;
using ubta.Common;
using System.IO;
using System.Xml.Schema;

namespace ubta.UseCase.DesignerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] primarySchemaLocations = { string.Format(@"{0}\ubta.Assert.xsd", Constants.DEFAULT_SCHEMA_DIR) };
            string useCaseLocation = string.Format(@"{0}\ExecuteTest.SampleLib.xml", Constants.CONFIG_DIR);
            string schemaLocation = string.Format(@"{0}\SampleLib.xsd", Constants.DEFAULT_SCHEMA_DIR);
            UseCaseMarkupSerializer ucms = new UseCaseMarkupSerializer();
            XmlSchema schema;
            using (FileStream sfs = new FileStream(schemaLocation, FileMode.Open))
            {
                schema = XmlSchema.Read(sfs, new ValidationEventHandler(HandleValEvent));
            }
            XmlSchemaSet xss = new XmlSchemaSet();
            xss.XmlResolver = new DefaultXmlResolver();
            xss.Add(schema);
            for (int i = 0; i < primarySchemaLocations.Length; i++)
            {
                using (FileStream fs = new FileStream(primarySchemaLocations[i], FileMode.Open))
                {
                    xss.Add(XmlSchema.Read(fs, new ValidationEventHandler(HandleValEvent)));
                }
            }
            xss.Compile();

            XmlDocument d = new XmlDocument();
            d.Load(useCaseLocation);
            d.Schemas = xss;
            d.Validate(new ValidationEventHandler(HandleValEvent));
            ucms.Deserialize(d);
        }
        private static void HandleValEvent(object source, ValidationEventArgs vea)
        {
        }
    }
}
