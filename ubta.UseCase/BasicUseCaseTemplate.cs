using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace ubta.UseCase
{
    [DefaultPropertyAttribute("UseCaseTemplate"), BrowsableAttribute(true)]
    public class BasicUseCaseTemplate
    {

        public BasicUseCaseTemplate()
        {
        }
        public void Init(String objName, string objType, UseCaseElement[] inputs, UseCaseElement[] outputs)
        {
            Name = objName;
            Type = objType;
            Inputs = inputs;
            Outputs = outputs;
        }
        [CategoryAttribute("Name"), DescriptionAttribute("Name of the template")]
        public string Name {get;set;}
        [CategoryAttribute("Type"), DescriptionAttribute("Type of the element")]
        public string Type { get; set; }
        [CategoryAttribute("Inputs"), BrowsableAttribute(true), DescriptionAttribute("The inputs")]
        public UseCaseElement[] Inputs { get; set; }
        [CategoryAttribute("Outputs"), BrowsableAttribute(true), DescriptionAttribute("The outputs")]
        public UseCaseElement[] Outputs { get; set; }

        public void Reset()
        {
        }

        [Browsable(false)]
        public XmlNode Node
        {
            get
            {
                return null;
            }
        }

        private string myParameters = null;
        [Browsable(false)]
        private string Parameters
        {
            get
            {
                if (null == myParameters)
                {
                    int cnt = Inputs.Count();
                    int c = 0;
                    StringBuilder sb = new StringBuilder();
                    foreach (var i in Inputs)
                    {
                        sb.Append(i.Type);
                        if (c < cnt-1 && cnt > 1)
                        {
                            sb.Append(", ");
                        }
                        c++;
                    }
                    myParameters = sb.ToString();
                }
                return myParameters;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name, Parameters);
        }
    }
}
