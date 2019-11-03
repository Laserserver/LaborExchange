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

        class Ent2
        {
            public int ID { get; set; }
            public string UserType { get; set; }
            public string Username { get; set; }
            public string Pass { get; set; }
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
                ent = (from s
                        in context.CompanyType
                       select new Ent
                       {
                           Type = s.Type,
                           ID = s.ID
                       }).ToList();

                dgUsers.DataSource = (from c in context.Logins
                    select new Ent2
                    {
                        Username = c.Username,
                        ID = c.ID,
                        UserType = c.UserType,
                        Pass = c.Pass
                    }).ToList();
                dgUsers.DataBind();
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