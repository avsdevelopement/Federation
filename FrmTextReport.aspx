<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTextReport.aspx.cs" Inherits="FrmTextReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Report" runat="server" Text="Text Report" OnClick="Report_Click" />    
    </div>
    </form>
</body>
</html>
