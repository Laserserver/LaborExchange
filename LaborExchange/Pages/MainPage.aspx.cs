using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaborExchange
{
    public partial class MainPage : System.Web.UI.Page
    {
        class Ent
        {
            public int ID { get; set; }
            public string Type { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authenticated())
            {
                Response.Redirect("Default.aspx");
            }

            
            List<Ent> ent = new List<Ent>();
            using (var context = new LaborExchangeEntities())
            {
                var ents = (from t in context.CompanyType select t).ToList();
                foreach (var s in ents)
                {
                    ent.Add(new Ent {Type = s.Type, ID = s.ID});
                }

                rptTypes.DataSource = ent;
                rptTypes.DataBind();
            }

            //lblHello.Text = $"Hello, {SQLiteLogic.SQLiteController.Instance.GetUserShowname(Request.Cookies["login"].Value)}";
                btnLogout.Click += BtnLogout_Click;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["token"] != null &&
                !string.IsNullOrEmpty(Request.Cookies["token"].Value) &&
                Request.Cookies["login"] != null &&
                !string.IsNullOrEmpty(Request.Cookies["login"].Value))
            {
                Response.Cookies["token"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["login"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Redirect("Default.aspx");
        }

        private bool Authenticated()
        {
            if (Request.Cookies["token"] != null &&
                !string.IsNullOrEmpty(Request.Cookies["token"].Value) &&
                Request.Cookies["login"] != null &&
                !string.IsNullOrEmpty(Request.Cookies["login"].Value))
            {
                return AuthController.Instance.IsAuthenticated(Request.Cookies["login"].Value, Request.Cookies["token"].Value);
            }
            return false;
        }
    }
}