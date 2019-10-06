<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LaborExchange.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Default</title>
    <link rel="stylesheet" href="./styles/styles.css" />
</head>
<body>
    <div id="navbar">
        <a href="./MainPage.aspx" class="navbar_hrefs">Main Page</a>
    </div>
    <div id="main">
        <form id="AuthForm" runat="server">
            <div class="f">
                <h1>Уже есть аккаунт?</h1>
            </div>
            <div id="wrapFormDivs">
                <div id="formDivs">
                    <div id="rdiv">
                        <span>Логин:</span>
                        <asp:TextBox runat="server" ID="txtLogin" />
                        <span>Пароль:</span>
                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                        
                        <asp:Panel runat="server" ID="pnlEmployee" Visible="False">
                            <asp:Label runat="server">Отображаемое имя:</asp:Label>
                            <asp:TextBox runat="server" ID="txtShowname"/>
                            <asp:Label runat="server">E-mail:</asp:Label>
                            <asp:TextBox runat="server" ID="txtEmail"/>
                            <asp:Label runat="server">Телефон:</asp:Label>
                            <asp:TextBox runat="server" ID="txtPhone"/>
                            <asp:Label runat="server">Дата рождения:</asp:Label>
                            <asp:TextBox runat="server" ID="txtBirth"/>
                        </asp:Panel>

                    </div>
                    <asp:Panel ID="pnlLogin" runat="server" Visible="False">
                        <div class="ldiv">
                            <div>
                                <asp:RadioButton runat="server" GroupName="type" ID="rbEmployee" Checked="True" />
                                <span>Соискатель</span>
                            </div>
                            <div>
                                <asp:RadioButton runat="server" GroupName="type" ID="rbCompany" />
                                <span>Компания</span>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="ldiv">
                        <asp:Button runat="server" ID="btnSubmit" Text="Логин" />
                        <asp:Button runat="server" ID="btnRegister" Text="Регистрация" />
                    </div>
                </div>
            </div>

            <div>
                <asp:Label ID="lblOutput" runat="server" />
            </div>
        </form>
    </div>
</body>
</html>
