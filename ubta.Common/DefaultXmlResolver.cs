#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : DefaultXmlResolver.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.IO;
using System.Xml;

namespace ubta.Common
{
    public class DefaultXmlResolver : XmlResolver
    {
        public override System.Net.ICredentials Credentials
        {
            set { }
        }

        /// <summary>
        /// When overridden in a derived class, maps a URI to an object containing the actual resource.
        /// </summary>
        /// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)"/>.</param>
        /// <param name="role">The current version does not use this parameter when resolving URIs. This is provided for future extensibility purposes. For example, this can be mapped to the xlink:role and used as an implementation specific argument in other scenarios.</param>
        /// <param name="ofObjectToReturn">The type of object to return. The current version only returns System.IO.Stream objects.</param>
        /// <returns>
        /// A System.IO.Stream object or null if a type other than stream is specified.
        /// </returns>
        /// <exception cref="T:System.Xml.XmlException">
        /// 	<paramref name="ofObjectToReturn"/> is not a Stream type.
        /// </exception>
        /// <exception cref="T:System.UriFormatException">
        /// The specified URI is not an absolute URI.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="absoluteUri"/> is null.
        /// </exception>
        /// <exception cref="T:System.Exception">
        /// There is a runtime error (for example, an interrupted server connection).
        /// </exception>
        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            if (File.Exists(absoluteUri.AbsolutePath))
            {
                return new FileStream(absoluteUri.AbsolutePath, FileMode.Open);
            }
            else
            {
                string filename = Path.GetFileName(absoluteUri.AbsolutePath);
                string f = Constants.DEFAULT_SCHEMA_DIR + filename;
                if (File.Exists(f))
                {
                    return new FileStream(f, FileMode.Open);
                }
                return null;
            }
        }
    }
}