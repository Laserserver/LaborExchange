<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="LaborExchange.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Main Page</title>
    <link rel="stylesheet" href="./styles/styles.css" />

</head>
<body>
<div>
    <a href="./Default.aspx">Default</a>
</div>
<div id="main">
<form id="DeauthForm" runat="server">
    <div>
        <asp:Label runat="server" ID="lblHello"/>
        
        
           
        

        <asp:Repeater runat="server" ID="rptTypes">
            <HeaderTemplate>
                <table border="2">
                    <thead>
                        <tr>
                            <th>Тип компании</th>
                        </tr>
                    </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><a href="TestChanger.aspx?ID=<%# Eval("ID")%>"><%# Eval("Type")%></a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        

    </div>
    <asp:Panel ID="pnlLogout" runat="server">
        <asp:Button runat="server" ID="btnLogout" Text="Log Out" />
    </asp:Panel>
</form>
    </div>
</body>
</html>
