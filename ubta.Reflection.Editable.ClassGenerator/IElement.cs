using System;
using ubta.Common;

namespace ubta.Reflection.Editable.ClassGenerator
{
    public interface IElement
    {
        IElement Parent { get; set; }
        Type ObjectType { get; set; }
        IListEx<IElement> Children { get; set; }
        bool AddElement(IElement e);
        bool IsChildOf(IElement obj);
    }
}
