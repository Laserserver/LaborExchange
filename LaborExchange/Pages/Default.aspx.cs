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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authenticated())
            {
                Response.Redirect("MainPage.aspx");
            }
            btnSubmit.Click += BtnSubmit_Click;
            if (btnSubmit.Visible)
                btnRegister.Click += BtnPrimaryRegister_Click;
            else
            {
                btnRegister.Click += BtnSecondaryRegister_Click;
            }
        }

        private void BtnPrimaryRegister_Click(object sender, EventArgs e)
        {
            btnSubmit.Visible = false;
            pnlLogin.Visible = true;
            pnlEmployee.Visible = true;
            btnRegister.Click -= BtnPrimaryRegister_Click;
            btnRegister.Click += BtnSecondaryRegister_Click;
        }

        private void BtnSecondaryRegister_Click(object sender, EventArgs e)
        {
            string type = rbCompany.Checked ? "Company" : "Employee";
            string email = txtEmail.Text;
            string birth = txtBirth.Text;
            string phone = txtPhone.Text;
            string showname = txtShowname.Text;
            var args = new List<string>
            {
                showname, email, phone, birth
            };
            if (!AuthController.Instance.AddUser(txtLogin.Text, txtPassword.Text, type, args))
            {
                lblOutput.Text = "Не получилось зарегистрировать";
                return;
            }

            btnSubmit.Visible = true;
            pnlLogin.Visible = false;
            pnlEmployee.Visible = false;
            lblOutput.Text = "Успешно зарегистрировано";
            btnRegister.Click -= BtnSecondaryRegister_Click;
            btnRegister.Click += BtnPrimaryRegister_Click;
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

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (AuthController.Instance.CheckCreds(txtLogin.Text, txtPassword.Text))
            {
                Response.Cookies["token"].Value = AuthController.Instance.MakeToken(txtLogin.Text, AuthController.Instance.MakeHash(txtLogin.Text, txtPassword.Text));
                Response.Cookies["login"].Value = txtLogin.Text;
                Response.Redirect("MainPage.aspx");
                return;
            }

            lblOutput.Text = "User with such password does not exist";
        }

    }
}