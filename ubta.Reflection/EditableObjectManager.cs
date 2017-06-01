using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ubta.Reflection
{
    public class EditableObjectManager
    {
        private static EditableObjectManager myInstance;
        public static EditableObjectManager Instance
        {
            get
            {
                if (null == myInstance)
                {
                    myInstance = new EditableObjectManager();
                }
                return myInstance;
            }
        }
        /// <summary>
        /// Is the reflection enabled (only for expert mode)
        /// </summary>
        private bool myReflectionEnabled = true;
        /// <summary>
        /// The unique Names
        /// </summary>
        private Hashtable myNameTable = new Hashtable();
        /// <summary>
        /// The object 2 Editable object
        /// </summary>
        private IDictionary<object, EditableObject> myObjectTable = new Dictionary<object, EditableObject>();
        private List<Type> myEditableClasses = null;
        /// <summary>
        /// Get Editable object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        /// <returns>the new object</returns>
        public EditableObject GetEditableObject(object obj, params object[] args)
        {
            Type[] types;

            if (obj != null)
            {
                if (myObjectTable.ContainsKey(obj))
                {
                    return myObjectTable[obj];
                }

                // collect all Editable classes
                if (myEditableClasses == null)
                {
                    myEditableClasses = new List<Type>();
                    Assembly ass = Assembly.Load(obj.GetType().Assembly.GetName().Name + "Editable");
                    types = ass.GetTypes();
                    foreach (Type type in types)
                    {
                        Type baseType = type;
                        while (baseType != null)
                        {
                            if (baseType == typeof(EditableObject))
                            {
                                myEditableClasses.Add(type);
                                break;
                            }
                            else
                            {
                                baseType = baseType.BaseType;
                            }
                        }
                    }
                }

                // create the types list
                types = new Type[args.Length + 1];
                types[0] = obj.GetType();
                for (int i = 0; i < args.Length; i++)
                {
                    types[i + 1] = args[i].GetType();
                }

                // create the parameter list
                object[] objs = new object[args.Length + 1];
                objs[0] = obj;
                for (int i = 0; i < args.Length; i++)
                {
                    objs[i + 1] = args[i];
                }

                // find the best match
                Type bestMatch = null;
                int bestCount = Int32.MaxValue;
                foreach (Type type in myEditableClasses)
                {
                    if (type.Name.StartsWith(obj.GetType().Name))
                    {
                        bestMatch = type;
                        break;
                    }
                    ConstructorInfo cinfo = type.GetConstructor(types);
                    if (cinfo != null)
                    {
                        ParameterInfo[] pinfos = cinfo.GetParameters();
                        if (pinfos != null && pinfos.Length >= 1)
                        {
                            ParameterInfo pinfo = pinfos[0];
                            int count = 0;

                            // search throught the types
                            Type baseType = obj.GetType();
                            while (baseType != null)
                            {
                                if (baseType == pinfo.ParameterType && (bestMatch == null || count < bestCount))
                                {
                                    bestMatch = type;
                                    bestCount = count;
                                    break;
                                }
                                else
                                {
                                    baseType = baseType.BaseType;
                                    count++;
                                }
                            }

                            // search the interface list
                            foreach (Type itype in obj.GetType().GetInterfaces())
                            {
                                if (itype == pinfo.ParameterType)
                                {
                                    count = 0;
                                    baseType = obj.GetType();
                                    while (baseType != null)
                                    {
                                        if (baseType.GetInterface(itype.ToString()) == null)
                                        {
                                            break;
                                        }
                                        baseType = baseType.BaseType;
                                        count++;
                                    }
                                    if (bestMatch == null || count < bestCount)
                                    {
                                        bestMatch = type;
                                        bestCount = count;
                                    }
                                }
                            }
                        }
                    }
                }

                // create a new instance
                if (bestMatch != null)
                {
                    ConstructorInfo cinfo = bestMatch.GetConstructor(types);
                    EditableObject tstObj = cinfo.Invoke(objs) as EditableObject;

                    // parent ?
                    if (args.Length == 1 && args[0] is EditableObject)
                    {
                        (args[0] as EditableObject).Children.Add(tstObj);
                    }
                    return tstObj;
                }
            }
            return null;
        }

        /// <summary>
        /// Enables or disables the reflection tricks
        /// </summary>
        public bool EnableReflection
        {
            get
            {
                return myReflectionEnabled;
            }
            set
            {
                myReflectionEnabled = value;
            }
        }

        public IDictionary<object, EditableObject> ObjectTable
        {
            get
            {
                return myObjectTable;
            }
        }

        public bool ReflectionEnabled
        {
            get
            {
                return myReflectionEnabled;
            }
        }

    }
}
