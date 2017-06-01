using System;
using System.Text;
using ubta.Common;

namespace ubta.Reflection.Editable.ClassGenerator
{
    public class Element : IElement
    {
        public Element()
        {
        }

        #region IElement Members
        private IElement myParent;
        private Type myObjectType;
        private IListEx<IElement> myChildren = new ListEx<IElement>();
        public Type ObjectType
        {
            get { return myObjectType; }
            set { myObjectType = value; }
        }
        public IElement Parent
        {
            get { return myParent; }
            set { myParent = value; }
        }
        public IListEx<IElement> Children
        {
            get { return myChildren; }
            set { myChildren = value; }
        }

        public bool AddElement(IElement e)
        {
            bool r = false;
            bool pc = false;
            if (!e.IsChildOf(this))
            {
                for (int i = 0; i < myChildren.Count && !r; i++)
                {
                    IElement t = myChildren[i];
                    if (t != e && !e.IsChildOf(t) && !t.IsChildOf(e))
                    {
                        if (t.ObjectType.IsAssignableFrom(e.ObjectType))
                        {
                            // we have a child
                            r = t.AddElement(e);
                        }
                    }
                }
            }
            if (!pc && !r)
            {
                myChildren.Add(e);
                e.Parent = this;
                r = true;
            }
            return r;
        }
        public bool IsChildOf(IElement obj)
        {
            if (obj.Children.Contains(this))
            {
                return true;
            }
            for (int i = 0; i < obj.Children.Count; i++)
            {
                if (obj.Children[i].IsChildOf(this))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int level = GetLevel();
            for (int i = 0; i < level; i++)
            {
                sb.Append("\t");
            }
            string tabs = sb.ToString();
            if (null != myObjectType)
            {
                sb.AppendLine(myObjectType.FullName);
            }
            for (int c = 0; c < myChildren.Count; c++)
            {
                sb.Append(tabs).Append(myChildren[c].ToString());
            }
            return sb.ToString();
        }

        private int GetLevel()
        {
            int i = 0;
            IElement t = this;
            while (t.Parent != null)
            {
                t = t.Parent;
                i++;
            }
            return i;
        }
    }
}
