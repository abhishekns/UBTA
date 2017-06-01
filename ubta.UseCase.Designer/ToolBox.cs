
namespace ubta.UseCase.Designer
{

    using System.Runtime.Serialization.Formatters.Binary;
    using System.Collections;
    using System.ComponentModel.Design;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Diagnostics;
    using System.Drawing.Design;
    using System.Drawing.Text;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting;
    using System.Runtime.Serialization.Formatters;
    using System.Text;
    using System.Windows.Forms.ComponentModel;
    using System.Windows.Forms.Design;
    using System.Windows.Forms;
    using System;
    using System.Workflow.Activities;
    using System.Workflow.ComponentModel;
    using System.Workflow.ComponentModel.Design;
    using System.Diagnostics.CodeAnalysis;
    using ubta.Common;
    using System.Collections.Generic;
    using System.Linq;

    #region Class Toolbox
    /// <summary>
    /// Class implementing the toolbox functionality in the sample.
    /// Toolbox displays workflow components using which the workflow can be created
    /// For more information on toolbox please refer to IToolboxService, ToolboxItem documentation
    /// 
    /// </summary>
    [ToolboxItem(false)]
    internal class Toolbox : Panel, IToolboxService
    {
        private const string CF_DESIGNER = "CF_WINOEDESIGNERCOMPONENTS";

        private IServiceProvider myServiceProvider;
        private Hashtable myCustomCreators;
        private Type myCurrentSelection;
        private ListBox myListBox = new ListBox();

        //Create the toolbox and add the toolbox entries
        public Toolbox(IServiceProvider provider)
        {
            myServiceProvider = provider;

            Text = "Toolbox";
            Size = new System.Drawing.Size(224, 350);

            myListBox.Dock = DockStyle.Fill;
            myListBox.IntegralHeight = false;
            myListBox.ItemHeight = 20;
            myListBox.DrawMode = DrawMode.OwnerDrawFixed;
            myListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            myListBox.BackColor = SystemColors.Window;
            myListBox.ForeColor = SystemColors.ControlText;
            myListBox.MouseMove += new MouseEventHandler(OnListBoxMouseMove);
            Controls.Add(myListBox);

            myListBox.DrawItem += new DrawItemEventHandler(this.OnDrawItem);
            myListBox.SelectedIndexChanged += new EventHandler(this.OnListBoxClick);
        }

        public void AddCreator(ToolboxItemCreatorCallback creator, string format)
        {
            AddCreator(creator, format, null);
        }

        public void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host)
        {
            if (creator == null || format == null)
            {
                throw new ArgumentNullException(creator == null ? "creator" : "format");
            }

            if (myCustomCreators == null)
            {
                myCustomCreators = new Hashtable();
            }
            else
            {
                string key = format;

                if (host != null) key += ", " + host.GetHashCode().ToString();

                if (myCustomCreators.ContainsKey(key))
                {
                    throw new Exception("There is already a creator registered for the format '" + format + "'.");
                }
            }

            myCustomCreators[format] = creator;
        }

        public void AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host)
        {
        }

        public void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host)
        {
        }

        public virtual void AddToolboxItem(ToolboxItem toolboxItem)
        {
        }

        public virtual void AddToolboxItem(ToolboxItem toolboxItem, IDesignerHost host)
        {
        }

        public virtual void AddToolboxItem(ToolboxItem toolboxItem, string category)
        {
        }

        public virtual void AddToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host)
        {
        }

        public CategoryNameCollection CategoryNames
        {
            get
            {
                return new CategoryNameCollection(new string[] { "UseCase" });
            }
        }

        public string SelectedCategory
        {
            get
            {
                return "UseCase";
            }
            set
            {
            }
        }

        private ToolboxItemCreatorCallback FindToolboxItemCreator(IDataObject dataObj, IDesignerHost host, out string foundFormat)
        {
            foundFormat = string.Empty;

            ToolboxItemCreatorCallback creator = null;
            if (myCustomCreators != null)
            {
                IEnumerator keys = myCustomCreators.Keys.GetEnumerator();
                while (keys.MoveNext())
                {
                    string key = (string)keys.Current;
                    string[] keyParts = key.Split(new char[] { ',' });
                    string format = keyParts[0];

                    if (dataObj.GetDataPresent(format))
                    {
                        // Check to see if the host matches.
                        if (keyParts.Length == 1 || (host != null && host.GetHashCode().ToString().Equals(keyParts[1])))
                        {
                            creator = (ToolboxItemCreatorCallback)myCustomCreators[format];
                            foundFormat = format;
                            break;
                        }
                    }
                }
            }

            return creator;
        }

        public virtual ToolboxItem GetSelectedToolboxItem()
        {
            ToolboxItem toolClass = null;
            if (this.myCurrentSelection != null)
            {
                try
                {
                    toolClass = Toolbox.GetToolboxItem(this.myCurrentSelection);
                }
                catch (TypeLoadException)
                {
                }
            }

            return toolClass;
        }

        public virtual ToolboxItem GetSelectedToolboxItem(IDesignerHost host)
        {
            return GetSelectedToolboxItem();
        }

        public object SerializeToolboxItem(ToolboxItem toolboxItem)
        {
            IComponent[] components = toolboxItem.CreateComponents();
            Activity[] activities = new Activity[components.Length];
            components.CopyTo(activities, 0);

            return CompositeActivityDesigner.SerializeActivitiesToDataObject(myServiceProvider, activities);
        }

        public ToolboxItem DeserializeToolboxItem(object dataObject)
        {
            return DeserializeToolboxItem(dataObject, null);
        }

        public ToolboxItem DeserializeToolboxItem(object data, IDesignerHost host)
        {
            IDataObject dataObject = data as IDataObject;

            if (dataObject == null)
            {
                return null;
            }

            ToolboxItem t = (ToolboxItem)dataObject.GetData(typeof(ToolboxItem));

            if (t == null)
            {
                string format;
                ToolboxItemCreatorCallback creator = FindToolboxItemCreator(dataObject, host, out format);

                if (creator != null)
                {
                    return creator(dataObject, format);
                }
            }

            return t;
        }

        public ToolboxItemCollection GetToolboxItems()
        {
            return new ToolboxItemCollection(new ToolboxItem[0]);
        }

        public ToolboxItemCollection GetToolboxItems(IDesignerHost host)
        {
            return new ToolboxItemCollection(new ToolboxItem[0]);
        }

        public ToolboxItemCollection GetToolboxItems(string category)
        {
            return new ToolboxItemCollection(new ToolboxItem[0]);
        }

        public ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host)
        {
            return new ToolboxItemCollection(new ToolboxItem[0]);
        }

        public bool IsSupported(object data, IDesignerHost host)
        {
            return true;
        }

        public bool IsSupported(object serializedObject, ICollection filterAttributes)
        {
            return true;
        }

        public bool IsToolboxItem(object dataObject)
        {
            return IsToolboxItem(dataObject, null);
        }

        public bool IsToolboxItem(object data, IDesignerHost host)
        {
            IDataObject dataObject = data as IDataObject;
            if (dataObject == null)
                return false;

            if (dataObject.GetDataPresent(typeof(ToolboxItem)))
            {
                return true;
            }
            else
            {
                string format;
                ToolboxItemCreatorCallback creator = FindToolboxItemCreator(dataObject, host, out format);
                if (creator != null)
                    return true;
            }

            return false;
        }

        public new void Refresh()
        {
        }

        public void RemoveCreator(string format)
        {
            RemoveCreator(format, null);
        }

        public void RemoveCreator(string format, IDesignerHost host)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            if (myCustomCreators != null)
            {
                string key = format;
                if (host != null)
                    key += ", " + host.GetHashCode().ToString();
                myCustomCreators.Remove(key);
            }
        }

        public virtual void RemoveToolboxItem(ToolboxItem toolComponentClass)
        {
        }

        public virtual void RemoveToolboxItem(ToolboxItem componentClass, string category)
        {
        }

        public virtual bool SetCursor()
        {
            if (this.myCurrentSelection != null)
            {
                Cursor.Current = Cursors.Cross;
                return true;
            }

            return false;
        }

        public virtual void SetSelectedToolboxItem(ToolboxItem selectedToolClass)
        {
            if (selectedToolClass == null)
            {
                myListBox.SelectedIndex = 0;
                OnListBoxClick(null, EventArgs.Empty);
            }
        }

        public void AddType(Type t)
        {
            myListBox.Items.Add(new SelfHostToolboxItem(t));
        }

        public Attribute[] GetEnabledAttributes()
        {
            return null;
        }

        public void SetEnabledAttributes(Attribute[] attrs)
        {
        }

        public void SelectedToolboxItemUsed()
        {
            SetSelectedToolboxItem(null);
        }

        internal static ToolboxItem GetToolboxItem(Type toolType)
        {
            if (toolType == null)
                throw new ArgumentNullException("toolType");

            ToolboxItem item = null;
            if ((toolType.IsPublic || toolType.IsNestedPublic) && typeof(IComponent).IsAssignableFrom(toolType) && !toolType.IsAbstract)
            {
                ToolboxItemAttribute toolboxItemAttribute = (ToolboxItemAttribute)TypeDescriptor.GetAttributes(toolType)[typeof(ToolboxItemAttribute)];
                if (toolboxItemAttribute != null && !toolboxItemAttribute.IsDefaultAttribute())
                {
                    Type itemType = toolboxItemAttribute.ToolboxItemType;
                    if (itemType != null)
                    {
                        // First, try to find a constructor with Type as a parameter.  If that
                        // fails, try the default constructor.
                        ConstructorInfo ctor = itemType.GetConstructor(new Type[] { typeof(Type) });
                        if (ctor != null)
                        {
                            item = (ToolboxItem)ctor.Invoke(new object[] { toolType });
                        }
                        else
                        {
                            ctor = itemType.GetConstructor(new Type[0]);
                            if (ctor != null)
                            {
                                item = (ToolboxItem)ctor.Invoke(new object[0]);
                                item.Initialize(toolType);
                            }
                        }
                    }
                }
                else if (!toolboxItemAttribute.Equals(ToolboxItemAttribute.None))
                {
                    item = new ToolboxItem(toolType);
                }
            }
            else if (typeof(ToolboxItem).IsAssignableFrom(toolType))
            {
                // if the type *is* a toolboxitem, just create it..
                //
                try
                {
                    item = (ToolboxItem)Activator.CreateInstance(toolType, true);
                }
                catch (Exception)
                {
                }
            }

            return item;
        }

        //Parse the toolbox.txt file embedded in resource and create the entries in toolbox
        public void AddToolboxEntries(IEnumerable<Type> items)
        {
            foreach (var t in items)
            {
                myListBox.Items.Add(new SelfHostToolboxItem(t));
            }
        }

        private void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            Graphics g = e.Graphics;
            ListBox listBox = (ListBox)sender;
            object objItem = listBox.Items[e.Index];
            SelfHostToolboxItem item = null;
            Bitmap bitmap = null;
            string text = null;

            if (objItem is string)
            {
                bitmap = null;
                text = (string)objItem;
            }
            else
            {
                item = (SelfHostToolboxItem)objItem;
                bitmap = (Bitmap)item.Glyph; // if it's not a bitmap, it's a metafile, and how likely is that?
                text = item.Name;
            }

            bool selected = false;
            bool disabled = false;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                selected = true;

            if ((int)(e.State & (DrawItemState.Disabled | DrawItemState.Grayed | DrawItemState.Inactive)) != 0)
                disabled = true;

            StringFormat format = new StringFormat();
            format.HotkeyPrefix = HotkeyPrefix.Show;

            int x = e.Bounds.X + 4;
            x += 20;

            if (selected)
            {
                Rectangle r = e.Bounds;
                r.Width--; r.Height--;
                g.DrawRectangle(SystemPens.ActiveCaption, r);
            }
            else
            {
                g.FillRectangle(SystemBrushes.Menu, e.Bounds);
                using (Brush border = new SolidBrush(Color.FromArgb(Math.Min(SystemColors.Menu.R + 15, 255), Math.Min(SystemColors.Menu.G + 15, 255), Math.Min(SystemColors.Menu.B + 15, 255))))
                    g.FillRectangle(border, new Rectangle(e.Bounds.X, e.Bounds.Y, 20, e.Bounds.Height));
            }

            if (bitmap != null)
                g.DrawImage(bitmap, e.Bounds.X + 2, e.Bounds.Y + 2, bitmap.Width, bitmap.Height);

            Brush textBrush = (disabled) ? new SolidBrush(Color.FromArgb(120, SystemColors.MenuText)) : SystemBrushes.FromSystemColor(SystemColors.MenuText);
            g.DrawString(text, Font, textBrush, x, e.Bounds.Y + 2, format);
            if (disabled)
                textBrush.Dispose();
            format.Dispose();
        }

        //Start the drag drop when user selects and drags the tool
        private void OnListBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.myListBox.SelectedItem != null)
            {
                SelfHostToolboxItem selectedItem = myListBox.SelectedItem as SelfHostToolboxItem;

                if (selectedItem == null || selectedItem.ComponentClass == null)
                    return;

                ToolboxItem toolboxItem = Toolbox.GetToolboxItem(selectedItem.ComponentClass);
                IDataObject dataObject = this.SerializeToolboxItem(toolboxItem) as IDataObject;
                DoDragDrop(dataObject, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void OnListBoxClick(object sender, EventArgs eevent)
        {
            SelfHostToolboxItem toolboxItem = myListBox.SelectedItem as SelfHostToolboxItem;
            if (toolboxItem != null)
            {
                this.myCurrentSelection = toolboxItem.ComponentClass;
            }
            else if (this.myCurrentSelection != null)
            {
                int index = this.myListBox.Items.IndexOf(this.myCurrentSelection);
                if (index >= 0)
                    this.myListBox.SelectedIndex = index;
            }
        }

        internal void Clear()
        {
            myListBox.Items.Clear();
        }
    }
    #endregion

    #region Class SelfHostToolboxItem
    internal class SelfHostToolboxItem
    {
        private string myComponentClassName;
        internal Type myComponentClass;
        private string myName;
        private string myClassName;
        private Image myGlyph;
        private Assembly myLoadingAssembly;
        public SelfHostToolboxItem(string componentClassName)
        {
            myComponentClassName = componentClassName;
        }

        public SelfHostToolboxItem(Type componentClass)
        {
            myComponentClass = componentClass;
            myComponentClassName = myComponentClass.FullName;
        }

        public Assembly LoadingAssembly
        {
            set
            {
                myLoadingAssembly = value;
            }
        }

        public override bool Equals(object obj)
        {
            SelfHostToolboxItem other = obj as SelfHostToolboxItem;
            if (null != other)
            {
                if(null != ComponentClass)
                {
                    return ComponentClass.Equals(other.ComponentClass);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            if (null != ComponentClass)
            {
                return ComponentClass.GetHashCode();
            }
            return base.GetHashCode();
        }

        public string ClassName
        {
            get
            {
                if (myClassName == null)
                {
                    myClassName = ComponentClass.FullName;
                }

                return myClassName;
            }
        }

        public Type ComponentClass
        {
            get
            {
                if (myComponentClass == null)
                {
                    if (myLoadingAssembly != null)
                    {
                        myComponentClass = myLoadingAssembly.GetType(myComponentClassName);
                    }
                    else
                    {
                        myComponentClass = Type.GetType(myComponentClassName);
                    }
                    if (myComponentClass == null)
                    {
                        int index = myComponentClassName.IndexOf(",");
                        if (index >= 0)
                            myComponentClassName = myComponentClassName.Substring(0, index);

                        foreach (AssemblyName referencedAssemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                        {
                            Assembly assembly = Assembly.Load(referencedAssemblyName);
                            if (assembly != null)
                            {
                                myComponentClass = assembly.GetType(myComponentClassName);
                                if (myComponentClass != null)
                                    break;
                            }
                        }
                        if (null != myComponentClass)
                        {
                            //Finally check in Activities dll
                            myComponentClass = typeof(SequentialWorkflowActivity).Assembly.GetType(myComponentClassName);
                        }
                    }
                }

                return myComponentClass;
            }
        }

        public string Name
        {
            get
            {
                if (myName == null)
                {
                    if (ComponentClass != null)
                        myName = ComponentClass.FullName;
                    else
                        myName = "Unknown Item";
                }

                return myName;
            }
        }

        public virtual Image Glyph
        {
            get
            {
                if (myGlyph == null)
                {
                    Type t = ComponentClass;

                    if (t == null)
                        t = typeof(Component);

                    ToolboxBitmapAttribute attr = (ToolboxBitmapAttribute)TypeDescriptor.GetAttributes(t)[typeof(ToolboxBitmapAttribute)];

                    if (attr != null)
                        myGlyph = attr.GetImage(t, false);
                }
                return myGlyph;
            }
        }

        public override string ToString()
        {
            return myComponentClassName;
        }
    }
    #endregion
}
