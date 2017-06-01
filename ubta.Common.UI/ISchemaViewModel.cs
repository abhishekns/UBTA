#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : ISchemaViewModel.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System.Xml.Schema;

namespace ubta.Graphics.UI
{
    public interface ISchemaViewModel
    {
        XmlSchemaObject XSO { get; }
        bool IsExpanded { get; set; }
        System.Collections.ObjectModel.ReadOnlyCollection<ISchemaViewModel> Children { get; }
        string Name { get; }
        ISchemaViewModel Parent { get; }
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}