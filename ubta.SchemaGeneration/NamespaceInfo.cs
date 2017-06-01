#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : NamespaceInfo.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Collections.Generic;

namespace ubta.SchemaGeneration
{
    public struct NamespaceInfo
    {
        private string[] myNamespaceList;
        private string myCommonNamespace;

        public void AddNamespace(string aNamespace)
        {
            try
            {
                List<string> l ;
                if (null != myNamespaceList)
                {
                    l = new List<string>(myNamespaceList);
                }
                else
                {
                    l = new List<string>();
                }
                if (aNamespace != null)
                {

                    l.Add(aNamespace);
                }
                else
                {
                    l.Add(String.Empty);
                }
                myNamespaceList = l.ToArray();
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }

        }

        public bool ContainsNamespace(string Namespace)
        {
            try
            {
                if (Namespace != null)
                {
                    if (Contains(Namespace, myNamespaceList))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
            return true;
        }

        private bool Contains(string Namespace, string[] namespaceList)
        {
            for (int i = 0; i < namespaceList.Length; i++)
            {
                if (Namespace.Equals(namespaceList[i]))
                {
                    return true;
                }
            }
            return false;
        }


        public void DetermineCommonHierarchy()
        {
            try
            {
                if (myNamespaceList.Length > 0)
                {
                    string ReferenceNamespace = (string)myNamespaceList[0];
                    int iterator = 0;
                    bool isCommon = false;
                    while (iterator < ReferenceNamespace.Length)
                    {
                        string IndividualReferenceNamespace = ExtractIndividualNamespace(ReferenceNamespace, iterator);

                        for (int arrayIterator = 0 ; arrayIterator < myNamespaceList.Length ; arrayIterator++)
                        {
                            string IndividualNamespace = ExtractIndividualNamespace((string)myNamespaceList[arrayIterator], iterator);
                            if (IndividualNamespace.Equals(IndividualReferenceNamespace))
                            {
                                isCommon = true;

                            }
                            else
                            {
                                isCommon = false;
                                break;
                            }

                        }
                        if (isCommon)
                        {
                            if (myCommonNamespace != String.Empty)
                            {
                                myCommonNamespace = myCommonNamespace + "." + IndividualReferenceNamespace;
                            }
                            else
                            {
                                myCommonNamespace += IndividualReferenceNamespace;
                            }


                            iterator += IndividualReferenceNamespace.Length + 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                else
                {
                    myCommonNamespace = String.Empty;
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }

        }

        public static string ExtractIndividualNamespace(string FullNamespace, int startIndex)
        {
            string individualNamespace = String.Empty;
            try
            {
                if (null != FullNamespace && startIndex < FullNamespace.Length)
                {
                    int endIndex = FullNamespace.IndexOf('.', startIndex);
                    if (endIndex != -1)
                    {
                        individualNamespace = FullNamespace.Substring(startIndex, endIndex - startIndex);

                    }
                    else
                    {
                        individualNamespace = FullNamespace.Substring(startIndex, FullNamespace.Length - startIndex);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.write(e);
                throw;
            }
            return individualNamespace;


        }

        public string CommonNamespaceName
        {
            get
            {
                if (myCommonNamespace == null)
                {
                    myCommonNamespace = string.Empty;
                }
                return myCommonNamespace;
            }
        }

        public override bool Equals(object obj)
        {
            NamespaceInfo other = (NamespaceInfo)obj;
            foreach (string n in other.myNamespaceList)
            {
                if (!Contains(n, myNamespaceList))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int r = 0;
            if (myNamespaceList == null)
            {
                AddNamespace("System");
            }
            else
            {
                for (int i = 0; i < myNamespaceList.Length; i++)
                {
                    r ^= myNamespaceList[i].GetHashCode();
                }
            }
            return r;
        }

        public string[] Namespaces
        { get { return myNamespaceList; } }
        public static bool operator ==(NamespaceInfo person1, NamespaceInfo person2)
        {
            return person1.Equals(person2);
        }

        public static bool operator !=(NamespaceInfo person1, NamespaceInfo person2)
        {
            return (!person1.Equals(person2));
        }


    }
}