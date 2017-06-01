using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Workflow.ComponentModel.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using System.Workflow.Activities;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Workflow.ComponentModel;
using ubta.Reflection;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Web.UI.Design;
using System.Reflection;

namespace ubta.UseCase.Designer
{
    [ActivityDesignerTheme(typeof(CompositeDesignerTheme))]
    [DesignerSerializer(typeof(CompositeActivityDesignerLayoutSerializer), typeof(WorkflowMarkupSerializer))]
    public class UseCaseDesigner : ActivityDesigner
    {
        private DefaultActivity parentActivity;

        // This adorner holds the glyphs that represent the Anchor property.
        private Adorner anchorAdorner = null;

        // This adorner holds the glyphs that represent the Margin and
        // Padding properties.
        private Adorner marginAndPaddingAdorner = null;

        // This defines the size of the anchor glyphs.
        internal const int glyphSize = 6;

        // This defines the size of the hit bounds for an AnchorGlyph.
        internal const int hitBoundSize = glyphSize + 4;

        // References to designer services, for convenience.
        private IComponentChangeService changeService = null;
        private ISelectionService selectionService = null;
        private BehaviorService behaviorSvc = null;

        private DesignerActionListCollection actionLists;

        /// <summary>
        /// API:no Initialize
        /// </summary>
        /// <param name="activity"></param>
        /// <apiflag>no</apiflag>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void Initialize(Activity activity)
        {
            base.Initialize(activity);

            var host = GetService(typeof(IDesignerHost)) as IDesignerHost;

            //var c = typeof(BehaviorService).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null,
            //        new Type[] { typeof(IServiceProvider), typeof(Control) }, new ParameterModifier[] { new ParameterModifier(2) });
            //try
            //{
            //    object ctrl = this.GetService(typeof(IContainer));
            //    object o = c.Invoke(new object[] { host,  ctrl });
            //    host.AddService(typeof(BehaviorService), o);
            //}
            //catch
            //{
            //}
            
            parentActivity = (DefaultActivity)activity;
            parentActivity.InstanceNameChanged += new InstanceNameChangedHandler(parentActivity_InstanceNameChanged);
            parentActivity.ParameterChanged += new ParamValueChanged(parentActivity_ParameterChanged);

            if (parentActivity.InstanceType == null)
            {
                parentActivity.InstanceTypeAvailable += new EventHandler(parentActivity_InstanceTypeAvailable);
            }
        }

        protected internal System.Collections.ObjectModel.ReadOnlyCollection<Point> GetConnectionsInternal(DesignerEdges edges)
        {
            Rectangle bounds = Bounds;
            int inputs = parentActivity.Action.Inputs();
            int outputs = parentActivity.Action.Outputs();
            List<Point> connections = new List<Point>();
            if ((edges & DesignerEdges.Left) > 0)
            {
                for (int i = 0; i < inputs; i++)
                {
                    connections.Add(new Point(bounds.Left, bounds.Top + bounds.Height / (inputs+1)));
                }
            }

            if ((edges & DesignerEdges.Top) > 0)
            {
                for (int i = 0; i < inputs; i++)
                {
                    connections.Add(new Point(bounds.Left + bounds.Width / (inputs+1), bounds.Top));
                }
            }

            if ((edges & DesignerEdges.Right) > 0)
            {
                for (int i = 0; i < outputs; i++)
                {
                    connections.Add(new Point(bounds.Right, bounds.Top + bounds.Height / (outputs+1)));
                }
            }

            if ((edges & DesignerEdges.Bottom) > 0)
            {
                for (int i = 0; i < outputs; i++)
                {
                    connections.Add(new Point(bounds.Left + bounds.Width / (outputs+1), bounds.Bottom));
                }
            }
            return connections.AsReadOnly();
        }

        public override Size MinimumSize
        {
            get
            {
                return new Size(50, 40);
            }
        }

        protected override bool EnableVisualResizing
        {
            get
            {
                return true;
            }
        }

        // This utility method creates an adorner for the anchor glyphs.
        // It then creates four AnchorGlyph objects and adds them to 
        // the adorner's Glyphs collection.
        private void InitializeAnchorAdorner()
        {
            this.anchorAdorner = new Adorner();
            this.behaviorSvc.Adorners.Add(this.anchorAdorner);

            this.anchorAdorner.Glyphs.Add(new AnchorGlyph(
                AnchorStyles.Left,
                this.behaviorSvc,
                this.changeService,
                this.selectionService,
                this,
                this.anchorAdorner)
                );

            this.anchorAdorner.Glyphs.Add(new AnchorGlyph(
                AnchorStyles.Top,
                this.behaviorSvc,
                this.changeService,
                this.selectionService,
                this,
                this.anchorAdorner)
                );

            this.anchorAdorner.Glyphs.Add(new AnchorGlyph(
                AnchorStyles.Right,
                this.behaviorSvc,
                this.changeService,
                this.selectionService,
                this,
                this.anchorAdorner)
                );

            this.anchorAdorner.Glyphs.Add(new AnchorGlyph(
                AnchorStyles.Bottom,
                this.behaviorSvc,
                this.changeService,
                this.selectionService,
                this,
                this.anchorAdorner)
                );
        }

        // This utility method creates an adorner for the margin and 
        // padding glyphs. It then creates a MarginAndPaddingGlyph and 
        // adds it to the adorner's Glyphs collection.
        private void InitializeMarginAndPaddingAdorner()
        {
            this.marginAndPaddingAdorner = new Adorner();
            this.behaviorSvc.Adorners.Add(this.marginAndPaddingAdorner);

            this.marginAndPaddingAdorner.Glyphs.Add(new MarginAndPaddingGlyph(
                this.behaviorSvc,
                this.changeService,
                this.selectionService,
                this,
                this.marginAndPaddingAdorner));
        }

        // This utility method connects the designer to various services.
        // These references are cached for convenience.
        private void InitializeServices()
        {
            // Acquire a reference to IComponentChangeService.
            this.changeService =
                GetService(typeof(IComponentChangeService))
                as IComponentChangeService;

            // Acquire a reference to ISelectionService.
            this.selectionService =
                GetService(typeof(ISelectionService))
                as ISelectionService;

            // Acquire a reference to BehaviorService.
            this.behaviorSvc =
                GetService(typeof(BehaviorService))
                as BehaviorService;
            if (behaviorSvc == null)
            {
            }
        }

        protected override System.Collections.ObjectModel.ReadOnlyCollection<DesignerAction> DesignerActions
        {
            get
            {
                return base.DesignerActions;
            }
        }

        void parentActivity_InstanceNameChanged(object sender, string oldName_in, string newName_in)
        {
            this.Invalidate();
        }

        void parentActivity_ParameterChanged(string name, object oldValue_in, object newValue_in, params object[] indices)
        {
            this.Invalidate();
        }

        void parentActivity_InstanceTypeAvailable(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// API:no OnLayoutSize
        /// </summary>
        /// <param name="e"></param>
        /// <apiflag>no</apiflag>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override Size OnLayoutSize(ActivityDesignerLayoutEventArgs e)
        {
            string tn = string.Empty;

            if (parentActivity.InstanceType != null)
            {
                tn = parentActivity.InstanceType.GetValidTypeNameForXml();
            }
            string textToDisplay = string.Format("{0} : {1}", parentActivity.InstanceName, tn);
            Font f = new Font("Arial", 9);
            SizeF s1 = e.Graphics.MeasureString(textToDisplay, f);
            textToDisplay = String.Format("{0}\n{1}", parentActivity.ActionName, parentActivity.Parameters);
            SizeF s2 = e.Graphics.MeasureString(textToDisplay, f);
            Size s = new SizeF(s1.Width > s2.Width ? s1.Width : s2.Width, s1.Height > s2.Height ? s1.Height : s2.Height).ToSize();
            return new Size(s.Width+40, s.Height+50);
        }

        /// <summary>
        /// API:no OnPaint
        /// </summary>
        /// <param name="e"></param>
        /// <apiflag>no</apiflag>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void OnPaint(ActivityDesignerPaintEventArgs e)
        {
            //Rectangle r = this.GetBounds();
            //Rectangle frameRect = new Rectangle(r.X, r.Y, r.Width - 5, r.Height - 5);
            Rectangle frameRect = new Rectangle(Location.X, Location.Y, Size.Width - 5, Size.Height - 5);
            Rectangle shadowRect = new Rectangle(frameRect.X + 5, frameRect.Y + 5, frameRect.Width, frameRect.Height);
            Rectangle pageRect = new Rectangle(frameRect.X + 4, frameRect.Y + 24, frameRect.Width - 8, frameRect.Height - 28);
            Rectangle titleRect = new Rectangle(frameRect.X + 15, frameRect.Y + 4, frameRect.Width - 30, 20);
            string tn = parentActivity.InstanceType != null ? parentActivity.InstanceType.GetValidTypeNameForXml() : string.Empty;
            string textToDisplay = string.Format("{0} : {1}", parentActivity.InstanceName, tn);
            Font f = new Font("Arial", 9);
            Brush frameBrush = new LinearGradientBrush(frameRect, Color.AntiqueWhite, Color.LightBlue, 45);

            e.Graphics.FillPath(Brushes.LightGray, shadowRect.RoundedRect());
            e.Graphics.FillPath(frameBrush, frameRect.RoundedRect());
            e.Graphics.FillPath(new LinearGradientBrush(pageRect, Color.AliceBlue, Color.Thistle, 45), pageRect.RoundedRect());

            SizeF s = e.Graphics.MeasureString(textToDisplay, f);

            float w = (titleRect.Width - s.Width) / 2;
            float x = titleRect.X + w;
            float h = (titleRect.Height - s.Height) / 2;
            float y = titleRect.Y - h;
            e.Graphics.DrawString(textToDisplay, f, Brushes.Black, x, y);

            frameRect.Inflate(20, 20);

            textToDisplay = String.Format("{0}\n{1}", parentActivity.ActionName, parentActivity.Parameters);
            pageRect.Inflate(-5, -5);

            s = e.Graphics.MeasureString(textToDisplay, f);
            h = (pageRect.Height - s.Height) / 2;
            y = pageRect.Y - h;

            s = e.Graphics.MeasureString(parentActivity.ActionName, f);
            w = (pageRect.Width - s.Width) / 2;
            x = pageRect.X + w;

            e.Graphics.DrawString(parentActivity.ActionName, new Font("Arial", 9), Brushes.Black, x, y);

            s = e.Graphics.MeasureString(parentActivity.Parameters, f);
            w = (pageRect.Width - s.Width) / 2;
            x = pageRect.X + w;
            y = pageRect.Y + 3 * h;

            e.Graphics.DrawString(parentActivity.Parameters, new Font("Arial", 9), Brushes.Black, x, y);
        }

        // CustomContextMenuEvent
        private void GroupIntoMenuEvent(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("This is the action from my Context Menu");
        }

        /// <summary>
        /// API:no Verbs
        /// </summary>
        /// <apiflag>no</apiflag>
        protected override ActivityDesignerVerbCollection Verbs
        {
            get
            {
                ActivityDesignerVerbCollection NewVerbs = new ActivityDesignerVerbCollection();
                NewVerbs.AddRange(base.Verbs);
                NewVerbs.Add(new ActivityDesignerVerb(null, DesignerVerbGroup.General, "Group Into", new EventHandler(GroupIntoMenuEvent)));
                return NewVerbs;
            }
        }

    }

    public static class GraphicsExtn
    {
        // RoundedRect
        public static GraphicsPath RoundedRect(this Rectangle frame)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 9;
            int diameter = radius * 2;

            Rectangle arc = new Rectangle(frame.Left, frame.Top, diameter, diameter);

            path.AddArc(arc, 180, 90);

            arc.X = frame.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = frame.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = frame.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            return path;
        }
    }

    // This class defines the smart tags that appear on the control
    // being designed. In this case, the Anchor property appears
    // on the smart tag and its value can be changed through a 
    // UI Type Editor created automatically by the 
    // DesignerActionService.
    public class AnchorActionList :
          System.ComponentModel.Design.DesignerActionList
    {
        // Cache a reference to the control.
        private Control relatedControl;

        //The constructor associates the control 
        //with the smart tag list.
        public AnchorActionList(IComponent component)
            : base(component)
        {
            this.relatedControl = component as Control;
        }

        // Properties that are targets of DesignerActionPropertyItem entries.
        public AnchorStyles Anchor
        {
            get
            {
                return this.relatedControl.Anchor;
            }
            set
            {
                PropertyDescriptor pdAnchor = TypeDescriptor.GetProperties(this.relatedControl)["Anchor"];
                pdAnchor.SetValue(this.relatedControl, value);
            }
        }

        // This method creates and populates the 
        // DesignerActionItemCollection which is used to 
        // display smart tag items.
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items =
                new DesignerActionItemCollection();

            // Add a descriptive header.
            items.Add(new DesignerActionHeaderItem("Anchor Styles"));

            // Add a DesignerActionPropertyItem for the Anchor
            // property. This will be displayed in a panel using
            // the AnchorStyles UI Type Editor.
            items.Add(new DesignerActionPropertyItem(
                "Anchor",
                "Anchor Style"));

            return items;
        }
    }

     #region Glyph Implementations

    // This class implements a MarginAndPaddingGlyph, which draws 
    // borders highlighting the value of the control's Margin 
    // property and the value of the control's Padding property.
    //
    // This glyph has no mouse or keyboard interaction, so its
    // related behavior class, MarginAndPaddingBehavior, has no
    // implementation.

    public class MarginAndPaddingGlyph : Glyph
    {
        private BehaviorService behaviorService = null;
        private IComponentChangeService changeService = null;
        private ISelectionService selectionService = null;
        private IDesigner relatedDesigner = null;
        private Adorner marginAndPaddingAdorner = null;
        private Control relatedControl = null;

        public MarginAndPaddingGlyph(
            BehaviorService behaviorService,
            IComponentChangeService changeService,
            ISelectionService selectionService,
            IDesigner relatedDesigner,
            Adorner marginAndPaddingAdorner)
            : base(new MarginAndPaddingBehavior())
        {
            this.behaviorService = behaviorService;
            this.changeService = changeService;
            this.selectionService = selectionService;
            this.relatedDesigner = relatedDesigner;
            this.marginAndPaddingAdorner = marginAndPaddingAdorner;

            this.relatedControl =
                this.relatedDesigner.Component as Control;

            this.changeService.ComponentChanged += new ComponentChangedEventHandler(changeService_ComponentChanged);
        }

        void changeService_ComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            if (object.ReferenceEquals(
                e.Component,
                this.relatedControl))
            {
                if (e.Member.Name == "Margin" ||
                    e.Member.Name == "Padding")
                {
                    this.marginAndPaddingAdorner.Invalidate();
                }
            }
        }

        // This glyph has no mouse or keyboard interaction, so 
        // GetHitTest can return null.
        public override Cursor GetHitTest(Point p)
        {
            return null;
        }

        // This method renders the glyph as a simple focus rectangle.
        public override void Paint(PaintEventArgs e)
        {
            ControlPaint.DrawFocusRectangle(
                    e.Graphics,
                    this.Bounds);

            ControlPaint.DrawFocusRectangle(
                    e.Graphics,
                    this.PaddingBounds);
        }

        // This glyph's Bounds property is a Rectangle defined by 
        // the value of the control's Margin property.
        public override Rectangle Bounds
        {
            get
            {
                Control c = this.relatedControl;
                Rectangle controlRect =
                    this.behaviorService.ControlRectInAdornerWindow(this.relatedControl);

                Rectangle boundsVal = new Rectangle(
                    controlRect.Left - c.Margin.Left,
                    controlRect.Top - c.Margin.Top,
                    controlRect.Width + c.Margin.Right * 2,
                    controlRect.Height + c.Margin.Bottom * 2);

                return boundsVal;
            }
        }

        // The PaddingBounds property is a Rectangle defined by 
        // the value of the control's Padding property.
        public Rectangle PaddingBounds
        {
            get
            {
                Control c = this.relatedControl;
                Rectangle controlRect =
                    this.behaviorService.ControlRectInAdornerWindow(this.relatedControl);

                Rectangle boundsVal = new Rectangle(
                    controlRect.Left + c.Padding.Left,
                    controlRect.Top + c.Padding.Top,
                    controlRect.Width - c.Padding.Right * 2,
                    controlRect.Height - c.Padding.Bottom * 2);

                return boundsVal;
            }
        }

        // There are no keyboard or mouse behaviors associated with 
        // this glyph, but you could add them to this class.
        internal class MarginAndPaddingBehavior : Behavior
        {

        }
    }

    // This class implements an AnchorGlyph, which draws grab handles
    // that represent the value of the control's Anchor property.
    //
    // This glyph has mouse and keyboard interactions, which are
    // handled by the related behavior class, AnchorBehavior.
    // Double-clicking on an AnchorGlyph causes its value to be 
    // toggled between enabled and disable states. 

    public class AnchorGlyph : Glyph
    {
        private int glyphSize = UseCaseDesigner.glyphSize;
        private int hitBoundSize = UseCaseDesigner.hitBoundSize;
        // This defines the bounds of the anchor glyph.
        protected Rectangle boundsValue;

        // This defines the bounds used for hit testing.
        // These bounds are typically different than the bounds 
        // of the glyph itself.
        protected Rectangle hitBoundsValue;

        // This is the cursor returned if hit test is positive.
        protected Cursor hitTestCursor = Cursors.Hand;

        // Cache references to services that will be needed.
        private BehaviorService behaviorService = null;
        private IComponentChangeService changeService = null;
        private ISelectionService selectionService = null;

        // Keep a reference to the designer for convenience.
        private IDesigner relatedDesigner = null;

        // Keep a reference to the adorner for convenience.
        private Adorner anchorAdorner = null;

        // Keep a reference to the control being designed.
        private Control relatedControl = null;

        // This defines the AnchorStyle which this glyph represents.
        private AnchorStyles anchorStyle;

        public AnchorGlyph(
            AnchorStyles anchorStyle,
            BehaviorService behaviorService,
            IComponentChangeService changeService,
            ISelectionService selectionService,
            IDesigner relatedDesigner,
            Adorner anchorAdorner)
            : base(new AnchorBehavior(relatedDesigner))
        {
            // Cache references for convenience.
            this.anchorStyle = anchorStyle;
            this.behaviorService = behaviorService;
            this.changeService = changeService;
            this.selectionService = selectionService;
            this.relatedDesigner = relatedDesigner;
            this.anchorAdorner = anchorAdorner;

            // Cache a reference to the control being designed.
            this.relatedControl =
                this.relatedDesigner.Component as Control;

            // Hook the SelectionChanged event. 
            this.selectionService.SelectionChanged +=
                new EventHandler(selectionService_SelectionChanged);

            // Hook the ComponentChanged event so the anchor glyphs
            // can correctly track the control's bounds.
            this.changeService.ComponentChanged +=
                new ComponentChangedEventHandler(changeService_ComponentChanged);
        }

        #region Overrides

        public override Rectangle Bounds
        {
            get
            {
                return this.boundsValue;
            }
        }

        // This method renders the AnchorGlyph as a filled rectangle
        // if the glyph is enabled, or as an open rectangle if the
        // glyph is disabled.
        public override void Paint(PaintEventArgs e)
        {
            if (this.IsEnabled)
            {
                using (Brush b = new SolidBrush(Color.Tomato))
                {
                    e.Graphics.FillRectangle(b, this.Bounds);
                }
            }
            else
            {
                using (Pen p = new Pen(Color.Tomato))
                {
                    e.Graphics.DrawRectangle(p, this.Bounds);
                }
            }
        }

        // An AnchorGlyph has keyboard and mouse interaction, so it's
        // important to return a cursor when the mouse is located in 
        // the glyph's hit region. When this occurs, the 
        // AnchorBehavior becomes active.

        public override Cursor GetHitTest(Point p)
        {
            if (hitBoundsValue.Contains(p))
            {
                return hitTestCursor;
            }

            return null;
        }

        #endregion

        #region Event Handlers

        // The AnchorGlyph objects should mimic the resize glyphs;
        // they should only be visible when the control is the 
        // primary selection. The adorner is enabled when the 
        // control is the primary selection and disabled when 
        // it is not.

        void selectionService_SelectionChanged(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(
                this.selectionService.PrimarySelection,
                this.relatedControl))
            {
                this.ComputeBounds();
                this.anchorAdorner.Enabled = true;
            }
            else
            {
                this.anchorAdorner.Enabled = false;
            }
        }

        // If any of several properties change, the bounds of the 
        // AnchorGlyph must be computed again.
        void changeService_ComponentChanged(
            object sender,
            ComponentChangedEventArgs e)
        {
            if (object.ReferenceEquals(
                e.Component,
                this.relatedControl))
            {
                if (e.Member.Name == "Anchor" ||
                    e.Member.Name == "Size" ||
                    e.Member.Name == "Height" ||
                    e.Member.Name == "Width" ||
                    e.Member.Name == "Location")
                {
                    // Compute the bounds of this glyph.
                    this.ComputeBounds();

                    // Tell the adorner to repaint itself.
                    this.anchorAdorner.Invalidate();
                }
            }
        }

        #endregion

        #region Implementation

        // This utility method computes the position and size of 
        // the AnchorGlyph in the Adorner window's coordinates.
        // It also computes the hit test bounds, which are
        // slightly larger than the glyph's bounds.
        private void ComputeBounds()
        {
            Rectangle translatedBounds = new Rectangle(
                this.behaviorService.ControlToAdornerWindow(this.relatedControl),
                this.relatedControl.Size);

            if ((this.anchorStyle & AnchorStyles.Top) == AnchorStyles.Top)
            {
                this.boundsValue = new Rectangle(
                    translatedBounds.X + (translatedBounds.Width / 2) - (glyphSize / 2),
                    translatedBounds.Y + glyphSize,
                    glyphSize,
                    glyphSize);
            }
            if ((this.anchorStyle & AnchorStyles.Bottom) == AnchorStyles.Bottom)
            {
                this.boundsValue = new Rectangle(
                    translatedBounds.X + (translatedBounds.Width / 2) - (glyphSize / 2),
                    translatedBounds.Bottom - 2 * glyphSize,
                    glyphSize,
                    glyphSize);
            }
            if ((this.anchorStyle & AnchorStyles.Left) == AnchorStyles.Left)
            {
                this.boundsValue = new Rectangle(
                    translatedBounds.X + glyphSize,
                    translatedBounds.Y + (translatedBounds.Height / 2) - (glyphSize / 2),
                    glyphSize,
                    glyphSize);
            }
            if ((this.anchorStyle & AnchorStyles.Right) == AnchorStyles.Right)
            {
                this.boundsValue = new Rectangle(
                    translatedBounds.Right - 2 * glyphSize,
                    translatedBounds.Y + (translatedBounds.Height / 2) - (glyphSize / 2),
                    glyphSize,
                    glyphSize);
            }

            this.hitBoundsValue = new Rectangle(
                this.Bounds.Left - hitBoundSize / 2,
                this.Bounds.Top - hitBoundSize / 2,
                hitBoundSize,
                hitBoundSize);
        }

        // This utility property determines if the AnchorGlyph is 
        // enabled, according to the value specified by the 
        // control's Anchor property.
        private bool IsEnabled
        {
            get
            {
                return
                    ((this.anchorStyle & this.relatedControl.Anchor) ==
                    this.anchorStyle);
            }
        }

        #endregion




        #region Behavior Implementation


        // This Behavior specifies mouse and keyboard handling when
        // an AnchorGlyph is active. This happens when 
        // AnchorGlyph.GetHitTest returns a non-null value.
        internal class AnchorBehavior : Behavior
        {
            private IDesigner relatedDesigner = null;
            private Control relatedControl = null;

            internal AnchorBehavior(IDesigner relatedDesigner)
            {
                this.relatedDesigner = relatedDesigner;
                this.relatedControl = relatedDesigner.Component as Control;
            }

            // When you double-click on an AnchorGlyph, the value of 
            // the control's Anchor property is toggled.
            //
            // Note that the value of the Anchor property is not set
            // by direct assignment. Instead, the 
            // PropertyDescriptor.SetValue method is used. This 
            // enables notification of the design environment, so 
            // related events can be raised, for example, the
            // IComponentChangeService.ComponentChanged event.

            public override bool OnMouseDoubleClick(
                Glyph g,
                MouseButtons button,
                Point mouseLoc)
            {
                base.OnMouseDoubleClick(g, button, mouseLoc);

                if (button == MouseButtons.Left)
                {
                    AnchorGlyph ag = g as AnchorGlyph;
                    PropertyDescriptor pdAnchor =
                        TypeDescriptor.GetProperties(ag.relatedControl)["Anchor"];

                    if (ag.IsEnabled)
                    {
                        // The glyph is enabled. 
                        // Clear the AnchorStyle flag to disable the Glyph.
                        pdAnchor.SetValue(
                            ag.relatedControl,
                            ag.relatedControl.Anchor ^ ag.anchorStyle);
                    }
                    else
                    {
                        // The glyph is disabled. 
                        // Set the AnchorStyle flag to enable the Glyph.
                        pdAnchor.SetValue(
                            ag.relatedControl,
                            ag.relatedControl.Anchor | ag.anchorStyle);
                    }

                }

                return true;
            }
        }


        #endregion
    }
    #endregion
}
