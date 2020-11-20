


<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="FrmInWordClearView.aspx.cs" Inherits="FrmInWordClearView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AVS IN SO TECH</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="RdlcPrint" runat="server" Width="100%" Height="100%" AsyncRendering="False" SizeToReportContent="True" DocumentMapCollapsed="True" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="RptOutwardClearing.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource Name="DataSetOutClearTemp" DataSourceId="ObjectDataSource1" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="DataSetOutClearTempTableAdapters.OWG_201607_TEMPTableAdapter"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>

