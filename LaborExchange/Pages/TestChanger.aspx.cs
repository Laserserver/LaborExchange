using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaborExchange.Pages
{
    public partial class TestChanger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += BtnSave_Click;
            btnNew.Click += BtnNew_Click;
            btnDelete.Click += BtnDelete_Click;
            if (IsPostBack)
                return;
            if (int.TryParse(Request.Params["ID"], out int iid))
            {
                using (var context = new LaborExchangeEntities())
                {
                    var t = (from c in context.CompanyType where c.ID == iid select c).FirstOrDefault();
                    if (t != null)
                    {
                        lblOldName.Text = t.Type;
                        txtNewName.Text = t.Type;
                    }
                    else
                    {
                        Response.Redirect("MainPage.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("MainPage.aspx");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            using (var context = new LaborExchangeEntities())
            {
                if (txtNewName.Text == "")
                    return;
                int id = int.Parse(Request.Params["ID"]);
                var t = (from c in context.CompanyType where c.ID == id select c).FirstOrDefault();
                if (t != null)
                    context.CompanyType.Remove(t);
                context.SaveChanges();
                Response.Redirect("MainPage.aspx");
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            using (var context = new LaborExchangeEntities())
            {
                if (txtNewName.Text == "")
                    return;
                CompanyType ct = new CompanyType {Type = txtNewName.Text};
                context.CompanyType.Add(ct);
                context.SaveChanges();
                Response.Redirect("MainPage.aspx");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (var context = new LaborExchangeEntities())
            {
                if (txtNewName.Text == "")
                    return;
                int id = int.Parse(Request.Params["ID"]);
                var t = (from c in context.CompanyType where c.ID == id select c).FirstOrDefault();
                if (t != null)
                    t.Type = txtNewName.Text;
                context.SaveChanges();
                Response.Redirect("MainPage.aspx");
            }
        }
    }
}