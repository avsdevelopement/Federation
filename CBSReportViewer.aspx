<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBSReportViewer.aspx.cs" Inherits="CBSReportViewer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">
            function doPrint() {
                var prtContent = document.getElementById('<%= RdlcPrint.ClientID %>');
            prtContent.border = 0; //set no border here
            var WinPrint = window.open('', '', 'left=150,top=100,width=1000,height=1000,toolbar=0,scrollbars=1,status=0,resizable=1');
            WinPrint.document.write(prtContent.outerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        </script>
        <div>
            <asp:Button ID="PrintButton" runat="server" Text="Print" OnClientClick="doPrint();" ToolTip="Print Report" />
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="RdlcPrint" runat="server" Width="100%" Height="100%" ShowBackButton="true" ShowParameterPrompts="true" AsyncRendering="False" SizeToReportContent="True" PageCountMode="Actual" ShowPrintButton="true">
                <LocalReport ReportPath="">
                    <DataSources>
                        <rsweb:ReportDataSource Name="DataSet1" DataSourceId="" />
                        <rsweb:ReportDataSource Name="DataSet2" DataSourceId="" />
                        <rsweb:ReportDataSource Name="DataSet3" DataSourceId="" />
                        <rsweb:ReportDataSource Name="DataSet4" DataSourceId="" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
