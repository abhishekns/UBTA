using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ubta.UseCase.Designer
{
    internal partial class ChooseUseCase : Form
    {
        private List<Type> types;
        private Type selectedType;

        public Type SelectedType
        {
            get { return selectedType; }
            set { selectedType = value; }
        }

        public List<Type> Types
        {
            get { return types; }
            set
            {
                types = value;
                this.cbTypes.Items.Clear();
                this.cbTypes.Items.AddRange(types.ToArray());
                this.cbTypes.SelectedIndex = 0;
            }
        }

        public ChooseUseCase()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.selectedType = this.cbTypes.SelectedItem as Type;
            this.Close();
        }
    }
}
