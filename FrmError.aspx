<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmError.aspx.cs" Inherits="FrmError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <asp:Label ID="lbl" runat="server" Text="Data Enter Invalid" style="font-size:large; font-family:Verdana;font-weight:bold;font-style:italic"></asp:Label>
    </div>
        <div align="Center">
            <asp:Button ID="btn" runat="server" Text="Okay" OnClientClick="javascript:window.close();"/>
        </div>
    </form>
</body>
</html>
