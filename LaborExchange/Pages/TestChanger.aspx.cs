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
        protected override void OnInit(EventArgs e)
        {
            using (var context = new LaborExchangeEntities())
            {
                nameBox.Names = (from type in context.CompanyType select type.Type).ToList();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += BtnSave_Click;
            btnNew.Click += BtnNew_Click;
            btnDelete.Click += BtnDelete_Click;
            //nameBox.TextChanged += NameBox_TextChanged;
            if (IsPostBack)
                return;
            LoadData();

        }


        private void LoadData()
        {
            if (int.TryParse(Request.Params["ID"], out int iid))
            {
                using (var context = new LaborExchangeEntities())
                {
                    var t = (from c in context.CompanyType where c.ID == iid select c).FirstOrDefault();
                    if (t != null)
                    {
                        lblOldName.Text = t.Type;
                        nameBox.NewName = t.Type;
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
                if (nameBox.Text == "")
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
                if (nameBox.Text == "")
                    return;
                nameBox.NewName = nameBox.Text;
                if (nameBox.NewName == null)
                {
                    errorLbl.Visible = true;
                    errorLbl.Text = $"Название уже имеется: {nameBox.Text}";
                    return;
                }
                CompanyType ct = new CompanyType
                {
                    Type = nameBox.NewName
                };
                context.CompanyType.Add(ct);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                Response.Redirect("MainPage.aspx");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (var context = new LaborExchangeEntities())
            {
                if (nameBox.Text == "")
                    return;
                nameBox.NewName = nameBox.Text;
                if (nameBox.NewName == null)
                {
                    errorLbl.Visible = true;
                    errorLbl.Text = "Название либо используется, либо происходит изменение имени на такое же.";
                    return;
                }

                int id = int.Parse(Request.Params["ID"]);
                var t = (from c in context.CompanyType where c.ID == id select c).FirstOrDefault();
                if (t != null)
                    t.Type = nameBox.NewName;
                context.SaveChanges();
                Response.Redirect("MainPage.aspx");
            }
        }
    }
}