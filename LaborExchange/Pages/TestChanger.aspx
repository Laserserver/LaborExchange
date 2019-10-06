<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestChanger.aspx.cs" Inherits="LaborExchange.Pages.TestChanger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="./styles/styles.css" />
</head>
<body>
<div id="navbar">
    <a href="./MainPage.aspx" class="navbar_hrefs">Main Page</a>
</div>
    <div id="main">
    <form id="form1" runat="server">
        
        <div id="formDivs">
        <div id="rdiv">
            <h4>Название типа компании:</h4>
            <asp:Label Text="text" runat="server" ID="lblOldName"/>
            <h4>Новое название:</h4>
            <asp:TextBox runat="server" ID="txtNewName"/> 
            <asp:Button Text="Сохранить" runat="server" ID="btnSave" />
            <asp:Button Text="Добавить" runat="server" ID="btnNew" />
            <asp:Button Text="Удалить" runat="server" ID="btnDelete" />
        </div>
        </div>
    </form>
    </div>
</body>
</html>
