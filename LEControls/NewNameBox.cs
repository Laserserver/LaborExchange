using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace LEControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:NewNameBox runat=server></{0}:NewNameBox >")]
    public class NewNameBox : TextBox
    {
        private string _name;

        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public List<string> Names
        {
            private get;
            set;
        }
        
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        public string NewName
        {
            get => Names.Contains(_name) ? null : _name;
            set
            {
                //BackColor = Names.Contains(_name) ? Color.IndianRed : Color.ForestGreen;
                _name = value;
                this.Text = _name;
            }
        }
    }
}
