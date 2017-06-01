#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : AssemblyViewModel.cs
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
using ubta.SchemaGeneration;

namespace ubta.SchemaGenApp
{
    public class AssemblyViewModel
    {
        IAssemblyAnalyzer myAnalyzer = new AssemblyAnalyzer();
        #region Data

        readonly ReadOnlyCollection<IViewModel> _firstGeneration;
        readonly IViewModel _rootNode;

        #endregion // Data

        #region Constructor
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public AssemblyViewModel(string file_in)
        {
            ArrayList al = new ArrayList();
            _name = file_in.Substring(0, file_in.Length-4);
            al.Add(file_in);
            Hashtable ht = myAnalyzer.AnalyzeAssembly(al, true);
            _rootNode = new ViewModelImpl(ht.GetEnumerator());

            _firstGeneration = new ReadOnlyCollection<IViewModel>(
                new IViewModel[] 
                { 
                    _rootNode 
                });

        }

        #endregion // Constructor

        #region FirstGeneration

        /// <summary>
        /// Returns a read-only collection containing the first person 
        /// in the family tree, to which the TreeView can bind.
        /// </summary>
        public ReadOnlyCollection<IViewModel> FirstGeneration
        {
            get { return _firstGeneration; }
        }

        #endregion // FirstGeneration


        public Hashtable GetChecked()
        {
            Hashtable ht = new Hashtable();
            IList<IViewModel> vm = _rootNode.GetCheckedChildren(true);
            for (int i = 0; i < vm.Count; i++)
            {
                GetChecked(ht, vm[i], true);
            }

            return ht;
        }

        private void GetChecked(Hashtable ht, IViewModel iViewModel, bool status_in)
        {
            IList<IViewModel> list = iViewModel.GetCheckedChildren(status_in);
            for (int i = 0; i < list.Count; i++)
            {
                GetChecked(ht, list[i], status_in);
            }
            if (null != iViewModel.RefObj as TypeDescriptor
                && (   iViewModel.IsChecked 
                    || iViewModel.AnyChildChecked))
            {
                ht.Add(iViewModel.Name, iViewModel.RefObj);
            }
        }
    }
}