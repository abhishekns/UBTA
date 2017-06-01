#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : ViewModelImpl.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     Copyright (C) Siemens AG 2010-2010 All Rights Reserved
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ubta.Common;
using ubta.SchemaGeneration;

namespace ubta.SchemaGenApp
{
    public interface IViewModel : INotifyPropertyChanged
    {
        System.Collections.ObjectModel.ReadOnlyCollection<IViewModel> Children { get; }
        object RefObj { get; }
        string Name { get; }
        IViewModel Parent { get; }
        bool IsExpanded { get; set; }
        bool IsSelected { get; set; }
        bool IsChecked { get; set; }
        bool AnyChildChecked { get; }
        IList<IViewModel> GetCheckedChildren(bool checked_in);
    }

    public class ViewModelImpl : IViewModel
    {
        #region data
        protected readonly ReadOnlyCollection<IViewModel> _children;
        protected string _name;
        protected readonly IViewModel _parent;
        private bool _isExpanded = false;
        private bool _isSelected = false;
        private bool _isChecked = true;
        protected object _refObj;
        #endregion

        #region Cstr
        public ViewModelImpl(IDictionaryEnumerator de)
        {
            _name = Constants.CLASSES_NODE_NAME;
            List<IViewModel> cs = new List<IViewModel>();
            while (de.MoveNext())
            {
                ubta.SchemaGeneration.TypeDescriptor td = de.Value as ubta.SchemaGeneration.TypeDescriptor;
                cs.Add(new ViewModelImpl(td.FullTypeName, td, this));
            }
            var t = from d in cs where true select d;
            _children = new ReadOnlyCollection<IViewModel>(t.ToList<IViewModel>());
        }

        public ViewModelImpl(string name, object obj, IViewModel parent_in)
        {
            _refObj = obj;
            _name = name;
            _parent = parent_in;
        }

        protected ViewModelImpl(string name, IViewModel parent_in)
        {
            _name = name;
            _parent = parent_in;
        }

        public ViewModelImpl(ubta.SchemaGeneration.EventDescriptor[] events, IViewModel parent_in) 
            : this("Events", parent_in)
        {         
            List<IViewModel> cs = new List<IViewModel>();
            for (int i = 0; i < events.Length; i++)
            {
                cs.Add(new ViewModelImpl(events[i].EventInformation.Name, events[i], this));
            }
            _children = new ReadOnlyCollection<IViewModel>(cs);
            UpdateChecked();
        }

        private void UpdateChecked()
        {
            if (null != _name)
            {
                if ( (_name.IndexOfAny(new char[] { '`', '\'', '[', ']', '{', '}', '<', '<' }) > -1)
                    || !_parent.IsChecked)
                {
                    IsChecked = false;
                }
            }
        }

        public ViewModelImpl(FieldDescriptor[] fields, IViewModel parent_in)
            :this("Fields", parent_in)
        {
            List<IViewModel> cs = new List<IViewModel>();
            for (int i = 0; i < fields.Length; i++)
            {
                cs.Add(new ViewModelImpl(fields[i].Information.Name, fields[i], this));
            }
            _children = new ReadOnlyCollection<IViewModel>(cs);
            UpdateChecked();
        }

        public ViewModelImpl(MethodDescriptor[] methods, IViewModel parent_in)
            :this(Constants.METHODS_NODE_NAME, parent_in)
        {
            List<IViewModel> cs = new List<IViewModel>();
            for (int i = 0; i < methods.Length; i++)
            {
                string name = methods[i].IsConstructor ? Constants.CONSTRUCTOR_NODE_NAME : methods[i].MethodInformation.Name;
                cs.Add(new ViewModelImpl(name, methods[i], this));
            }
            _children = new ReadOnlyCollection<IViewModel>(cs);
            UpdateChecked();
        }

        public ViewModelImpl(ubta.SchemaGeneration.PropertyDescriptor[] properties, IViewModel parent_in)
            :this(Constants.PROPERTIES_NODE_NAME, parent_in)
        {
            List<IViewModel> cs = new List<IViewModel>();
            for (int i = 0; i < properties.Length; i++)
            {
                cs.Add(new ViewModelImpl(properties[i].Information.Name, properties[i], this));
            }
            _children = new ReadOnlyCollection<IViewModel>(cs);
            UpdateChecked();
        }

        public ViewModelImpl(string name, ubta.SchemaGeneration.TypeDescriptor td, IViewModel parent_in)
            :this(name, parent_in)
        {
            _refObj = td;
            List<IViewModel> cs = new List<IViewModel>();
            if (null != td && null != td.EventsDescriptors && td.EventsDescriptors.Length > 0)
            {
                cs.Add(new ViewModelImpl(td.EventsDescriptors, this));
            }
            if (null != td && null != td.FieldsDescriptors && td.FieldsDescriptors.Length > 0)
            {
                cs.Add(new ViewModelImpl(td.FieldsDescriptors, this));
            }
            if (null != td && null != td.MethodsDescriptors && td.MethodsDescriptors.Length > 0)
            {
                cs.Add(new ViewModelImpl(td.MethodsDescriptors, this));
            }
            if (null != td && null != td.PropertiesDescriptors && td.PropertiesDescriptors.Length > 0)
            {
                cs.Add(new ViewModelImpl(td.PropertiesDescriptors, this));
            }
            _children = new ReadOnlyCollection<IViewModel>(cs);
            UpdateChecked();
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

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
                    // Expand all the way up to the root.
                }
            }
        }

        #endregion // IsSelected

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    this.OnPropertyChanged("IsChecked");
                    if (_children != null)
                    {
                        for (int i = 0; i < _children.Count; i++)
                        {
                            _children[i].IsChecked = _isChecked;
                        }
                    }
                }
            }
        }

        #endregion

        #region IViewModel Members

        public ReadOnlyCollection<IViewModel> Children
        {
            get { return _children; }
        }

        public string Name
        {
            get { return _name; }
        }

        public IViewModel Parent
        {
            get { return _parent; }
        }

        #endregion

        #region IViewModel Members


        public virtual IList<IViewModel> GetCheckedChildren(bool checked_in)
        {
            List<IViewModel> list = new List<IViewModel>();
            if (null == _children)
            {
                return list;
            }
            for (int i = 0; i < _children.Count; i++)
            {
                if (_children[i].IsChecked || _children[i].AnyChildChecked)
                {
                    list.Add(_children[i]);
                }
            }
            return list;
        }

        #endregion

        #region IViewModel Members


        public virtual bool AnyChildChecked
        {
            get
            {
                if (null == _children)
                {
                    return false;
                }
                for (int i = 0; i < _children.Count; i++)
                {
                    if (_children[i].IsChecked || _children[i].AnyChildChecked)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        #region IViewModel Members


        public object RefObj
        {
            get { return _refObj; }
        }

        #endregion
    }
}