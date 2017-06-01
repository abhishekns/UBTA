#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : DefaultExecuter.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using ubta.Common;
using ubta.Reflection;

namespace ubta.ExecuterLib
{
	/// <summary>
	/// Description of DefaultExecuter.
	/// </summary>
	public class DefaultExecuter
	{
        private List<string> myAssemblies = new List<string>();

		private TypeHelper myTypeHelper = new TypeHelper();
		
		public TypeHelper TypeHelper
		{
			get
			{
				return myTypeHelper;
			}
		}

        public IList<string> Assemblies
        {
            get
            {
                return myAssemblies;
            }
        }

        protected virtual string DefaultConfigFile
        {
            get
            {
                return Environment.ExpandEnvironmentVariables(@"%UBTA_HOME%\ubta.ExecuterLib\Config\Config.txt");
            }
        }

        protected virtual XmlNode GetRoot(XmlDocument doc)
        {
            return doc[Constants.ROOT_ELEMENT_NODE_NAME];
        }

		public void ExecuteCalls(XmlNode calls)
		{
            if (null != calls)
            {
                foreach (XmlNode instance in calls.ChildNodes)
                {
                    foreach (XmlNode method in instance.ChildNodes)
                    {
                        ExecuteMethod(method);
                    }
                }
            }
        }

        private void ValHandler(object source, ValidationEventArgs vea)
        {
            Console.WriteLine(vea.Message);
        }

        public void ExecuteMethod(XmlNode anode_in)
		{
            string typeName = NameHelper.GetTypeName(anode_in.ParentNode);
            TypeExtension te = myTypeHelper.GetType(typeName);
            string name = anode_in.ParentNode.Attributes[Constants.NAME_NODE_ATTRIBUTE].Value;
            object target = null;
            if (anode_in.IsConstructorNode())
            {
                target = te.MH.Invoke(anode_in);
                VariableManager.Instance[name] = target;
            }
            else
            {
                target = VariableManager.Instance[name];
                if (null == target)
                {
                    target = Activator.CreateInstance(te.T);
                    VariableManager.Instance[name] = target;
                }
                object retValue = te.MH.Invoke(target, anode_in);
                XmlNode n = anode_in.LastChild;
                if (n == null)
                {
                    n = anode_in;
                }
                if (n.IsReturnNode())
                {
                    string retName = anode_in.LastChild.Attributes[Constants.NAME_NODE_ATTRIBUTE].Value;
                    VariableManager.Instance[retName] = retValue;
                }
            }
		}

        public void ExecuteXmlFile(string path)
        {
            XmlDocument doc = SchemaHelper.SetupDoc(path, Constants.DEFAULT_SCHEMA_DIR, new GetRoot(GetRoot));
            SetupExec();
            ExecuteCalls(doc[Constants.ROOT_ELEMENT_NODE_NAME]);
        }

        public void SetupExec()
        {
            myTypeHelper.Init(myAssemblies);
        }
       
    }
}