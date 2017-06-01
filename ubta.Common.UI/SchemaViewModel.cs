#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : SchemaViewModel.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using ubta.Common;

namespace ubta.Graphics.UI
{
    public class XmlSchemaViewModel 
    {
        #region Data

        readonly ReadOnlyCollection<ISchemaViewModel> _firstGeneration;
        readonly ISchemaViewModel _rootNode;

        #endregion // Data
        public XmlSchemaViewModel(XmlSchemaSet rootNode)
        {
            _rootNode = new SchemaViewModel(rootNode);

            _firstGeneration = new ReadOnlyCollection<ISchemaViewModel>(
                new ISchemaViewModel[] 
                { 
                    _rootNode 
                });
        }

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public ReadOnlyCollection<ISchemaViewModel> FirstGeneration
        {
            get { return _firstGeneration; }
        }
    }

    public class SchemaViewModel : ISchemaViewModel
    {
        #region Data

        private readonly ReadOnlyCollection<ISchemaViewModel> _children;
        private readonly ISchemaViewModel _parent;
        private readonly XmlSchemaObject _xso;
        private string _name = null;
        private Regex myCleanup = new Regex("_[0123456789]");
        private bool _isExpanded;
        private bool _isSelected;

        #endregion // Data

        #region Cstr

        public SchemaViewModel(string name, SchemaViewModel parent)
        {
            _name = name;
            _parent = parent;
        }

        public SchemaViewModel(string name, XmlSchemaSequence xss, SchemaViewModel parent)
        {
            _name = name;
            List<ISchemaViewModel> elems = new List<ISchemaViewModel>();
            elems.AddRange((from element in xss.Items.Cast<XmlSchemaObject>() where element is XmlSchemaElement 
                            select ((ISchemaViewModel)new SchemaViewModel(GetGenParamName(element as XmlSchemaElement), this))).ToList<ISchemaViewModel>());

            _children = new ReadOnlyCollection<ISchemaViewModel>(elems);

            _parent = parent;
        }
        public SchemaViewModel(XmlSchemaSet schemaSet_in)
        {
            _xso = null;
            List<XmlSchemaType> nodes = new List<XmlSchemaType>();
            //nodes.AddRange(schemaSet_in.GlobalAttributes.Values.Cast<XmlSchemaObject>());
            nodes.AddRange(schemaSet_in.GlobalTypes.Values.Cast<XmlSchemaType>());
            _children = new ReadOnlyCollection<ISchemaViewModel>(
                    (from child in nodes where !child.QualifiedName.Name.Equals("anyType")
                         select ((ISchemaViewModel) new SchemaViewModel(child, this))).ToList<ISchemaViewModel>());

        }
        public SchemaViewModel(XmlSchemaType xst, SchemaViewModel parent)
        {
            _xso = xst;
            _parent = parent;
            _children = GetChildren(xst);
        }
        public SchemaViewModel(XmlSchemaElement xse, SchemaViewModel parent, bool method_in)
        {
            _xso = xse;
            _parent = parent;
            XmlSchemaComplexType args = xse.ElementSchemaType as XmlSchemaComplexType;
            if (null != args && method_in)
            {
                XmlSchemaChoice choice = args.ContentTypeParticle as XmlSchemaChoice;
                List<XmlSchemaObject> overloads = new List<XmlSchemaObject>();
                if (null != choice)
                {
                    overloads.AddRange(choice.Items.Cast<XmlSchemaObject>());
                }
                else
                {
                    XmlSchemaSequence argseq = args.ContentTypeParticle as XmlSchemaSequence;
                    overloads.Add(argseq);
                }
                _children = new ReadOnlyCollection<ISchemaViewModel>(
                    (from xso in overloads
                     where true
                     select GetSchemaViewModel(xso)).ToList<ISchemaViewModel>());
            }
            else
            {
                XmlSchemaSimpleType xsst = xse.ElementSchemaType as XmlSchemaSimpleType;
            }
        }
        public SchemaViewModel(XmlSchemaElement xse, SchemaViewModel parent)
            :this(xse, parent, true)
        {
        }
        public SchemaViewModel(XmlSchemaSequence xss, SchemaViewModel parent)
        {
            _xso = xss;
            _parent = parent;
        }

        #endregion

        #region Properties

        public XmlSchemaObject XSO
        {
            get
            {
                return _xso;
            }
        }

        public ReadOnlyCollection<ISchemaViewModel> Children
        {
            get { return _children; }
        }

        public string Name
        {
            get
            {
                if (null == _name)
                {
                    _name = GetName(_xso);
                }
                return _name;
            }
        }
        #endregion // Properties

        #region Presentation Members

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        #endregion // IsExpanded

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region NameContainsText

        public bool NameContainsText(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(this.Name))
                return false;

            return this.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        #endregion // NameContainsText

        #region Parent

        public ISchemaViewModel Parent
        {
            get { return _parent; }
        }

        #endregion // Parent

        #endregion // Presentation Members

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members

        #region Private methods

        private string GetGenParamName(XmlSchemaElement xse)
        {
            StringBuilder constraints = new StringBuilder();
            int i = 0;
            constraints.Append(xse.Name).Append(":");
            foreach (XmlSchemaAttribute xsa in (xse.ElementSchemaType as XmlSchemaComplexType).Attributes)
            {
                if (i > 0)
                {
                    constraints.Append(Constants.PARAM_SEPARATOR);
                }
                constraints.Append(xsa.FixedValue);
                i++;
            }
            return constraints.ToString();
        }

        private ReadOnlyCollection<ISchemaViewModel> GetChildren(XmlSchemaType xst)
        {
            XmlSchemaComplexType xsct = xst as XmlSchemaComplexType;
            if (null != xsct)
            {
                return GetChildren(xsct);
            }
            return null;
        }

        private ReadOnlyCollection<ISchemaViewModel> GetChildren(XmlSchemaComplexType xsct_in)
        {
            XmlSchemaSequence xss = null;
            XmlSchemaChoice xsc = xsct_in.ContentTypeParticle as XmlSchemaChoice;
            List<ISchemaViewModel> list = new List<ISchemaViewModel>();
            if(null != (xss = xsct_in.ContentTypeParticle as XmlSchemaSequence))
            {

                list.Add(new SchemaViewModel(Constants.GENERIC_PARAM_NODE_NAME, xss, this));
                xsc = xss.Items[xss.Items.Count - 1] as XmlSchemaChoice;
            }
            if (null != xsc)
            {
                XmlSchemaElement xse = xsc.Items[0] as XmlSchemaElement;
                if (null != xse)
                {
                    List<XmlSchemaElement> methods = new List<XmlSchemaElement>();
                    methods.AddRange(xsc.Items.Cast<XmlSchemaElement>());
                    list.AddRange((from method in methods
                                   where true
                                   select ((ISchemaViewModel)new SchemaViewModel(method, this))).ToList<ISchemaViewModel>());
                }
            }
            return new ReadOnlyCollection<ISchemaViewModel>(list);
        }

        private string GetName(XmlSchemaObject _xso)
        {
            string name = string.Empty;
            if (null == _xso)
            {
                name = Constants.CLASSES_NODE_NAME;
            }
            else
            {
                XmlSchemaElement xse = null;
                XmlSchemaType xst = null;
                XmlSchemaSequence xss = null;
                if ((xst = _xso as XmlSchemaType) != null)
                {
                    name = xst.Name;
                }
                else if ((xse = _xso as XmlSchemaElement) != null)
                {
                    name = CleanName(xse.Name);
                }
                else if ((xss = _xso as XmlSchemaSequence) != null)
                {
                    name = GetName(xss);
                }
            }
            return name;
        }

        private string GetName(XmlSchemaSequence seq)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            for (int i = 0; i < seq.Items.Count; i++)
            {
                sb.Append(CleanName((seq.Items[i] as XmlSchemaElement).Name));
                if (i < seq.Items.Count - 1 && seq.Items.Count > 1)
                {
                    sb.Append(Constants.PARAM_SEPARATOR);
                }
            }
            sb.Append(")");
            return sb.ToString();
        }

        private string CleanName(string p)
        {
            return myCleanup.Replace(p, new MatchEvaluator(delegate(Match m)
            {
                if (m.Success)
                {
                    return string.Empty;
                }
                return m.Value;
            }));
        }

        private ISchemaViewModel GetSchemaViewModel(XmlSchemaObject xso)
        {
            XmlSchemaElement xse = null;
            XmlSchemaSequence xss = null;
            if (null != (xse = xso as XmlSchemaElement))
            {
                return (ISchemaViewModel)new SchemaViewModel(xse, this, false);
            }
            else if (null != (xss = xso as XmlSchemaSequence))
            {
                return (ISchemaViewModel)new SchemaViewModel(xss, this);
            }
            return null;
        }

        #endregion
    }
}