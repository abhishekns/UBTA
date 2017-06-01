using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace ubta.Common
{
    public delegate XmlNode GetRoot(XmlDocument doc);

    public class SchemaHelper
    {
        public static string DefaultSchemaLocation = Environment.ExpandEnvironmentVariables(@"%UBTA_HOME%\ubta.ExecuterLib\Schemas");

        public static string[] DefaultSchemas = new string[] { "ubta.Schema", "ubta.Assert", "ubta.ParamFiles" };

        public static void LoadDefaultSchemas(XmlSchemaSet xss)
        {
            for (int i = 0; i < DefaultSchemas.Length; i++)
            {
                xss.Add(LoadSchema(DefaultSchemas[i]));
            }
        }

        public static XmlSchema LoadSchema(string schema_in)
        {
            string fname = schema_in;
            if(!File.Exists(schema_in))
            {
                fname = DefaultSchemaLocation+Path.DirectorySeparatorChar+schema_in+".xsd";
            }
            if(File.Exists(fname))
            {
                using (FileStream fs = new FileStream(fname, FileMode.Open))
                {
                    return XmlSchema.Read(fs, new ValidationEventHandler(ValHandler));
                }
            }
            return null;
        }

        public static XmlDocument SetupDoc(string path, string defaultSchemaLoc_in, GetRoot getRoot_in, ref XmlDocument doc_ref)
        {
            doc_ref.XmlResolver = new DefaultXmlResolver();
            doc_ref.Load(path);
            doc_ref.Schemas.Add(SchemaHelper.GetSchemas(path, defaultSchemaLoc_in, getRoot_in));
            doc_ref.Validate(new ValidationEventHandler(ValHandler));
            return doc_ref;
        }

        public static XmlDocument SetupDoc(string path, string defaultSchemaLoc_in, GetRoot getRoot_in)
        {
            XmlDocument doc = new XmlDocument();
            return SetupDoc(path, defaultSchemaLoc_in, getRoot_in, ref doc);
        }

        public static XmlSchemaSet GetSchemas(string path, string defaultSchemaLoc_in, GetRoot getRoot_in)
        {
            XmlSchemaSet xss = new XmlSchemaSet();
            xss.XmlResolver = new DefaultXmlResolver();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode calls = getRoot_in(doc);
            if (calls.Attributes[Constants.XSI_SCHEMALOCATION] != null)
            {
                string schemaLoc = Environment.ExpandEnvironmentVariables(calls.Attributes[Constants.XSI_SCHEMALOCATION].Value.Split(' ')[1]);
                if (null != schemaLoc && File.Exists(schemaLoc))
                {
                    XmlSchema xs;
                    using (FileStream fs = new FileStream(schemaLoc, FileMode.Open))
                    {
                        xs = XmlSchema.Read(fs, new ValidationEventHandler(ValHandler));
                    }
                    xss.Add(xs);
                }
            }
            foreach (XmlAttribute xa in calls.Attributes)
            {
                if (xa.Name.Contains("xmlns"))
                {
                    string fname = defaultSchemaLoc_in + NameHelper.Clean(xa.Value, "/", StringPos.End);
                    if(!fname.EndsWith(".xsd"))
                    {
                        fname = fname + ".xsd";
                    }
                    if (File.Exists(fname)) // && !fname.EndsWith("ubta.Schema.xsd"))
                    {
                        using (FileStream fs = new FileStream(fname, FileMode.Open))
                        {
                            XmlSchema x = XmlSchema.Read(fs, new ValidationEventHandler(ValHandler));
                            xss.Add(x);
                        }
                    }
                    else if (!xa.Value.Contains(".w3."))
                    {
                        if (xss.Schemas(xa.Value).Count == 0)
                        {
                            xss.Add(xa.Value, NameHelper.Clean(xa.Value, "/", StringPos.End) + ".xsd");
                        }
                    }
                }
            }
            xss.Compile();
            return xss;
        }
        private static void ValHandler(object sender, ValidationEventArgs e)
        {
            
        }
    }
}
