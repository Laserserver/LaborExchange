<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestChanger.aspx.cs" Inherits="LaborExchange.Pages.TestChanger" %>
<%@ Register Assembly="LEControls"  Namespace="LEControls"  TagPrefix="lec" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="./styles/styles.css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript" ></script>
</head>
<body>
<script type="text/javascript">
    $("#<%=nameBox.ClientID%>").on("change", function (e) { console.log(e);});
</script>
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
            <lec:NewNameBox runat="server" ID ="nameBox"></lec:NewNameBox>
            <asp:Button Text="Сохранить" runat="server" ID="btnSave" />
            <br/>
            <asp:Button Text="Добавить" runat="server" ID="btnNew" />
            <asp:Label runat="server" Visible="False" ID="errorLbl"></asp:Label>
            <br/>
            <asp:Button Text="Удалить" runat="server" ID="btnDelete" />
        </div>
        </div>
    </form>
    </div>
</body>
</html>
