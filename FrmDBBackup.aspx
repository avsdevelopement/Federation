<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDBBackup.aspx.cs" Inherits="FrmDBBackup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DBBackup</title>
          <script src="js/jquery-1.8.2.min.js"></script>
   <%-- <script type="text/javascript" src="XML/CommonPgVariables.js"></script>--%>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#txtTableWidth").val(_objTableWidth);
            
        });

    </script>
    <%-- <script src="RoleRights/js/getRolesRights.js" type="text/javascript"></script>--%>
   <%-- <link href="css/FastGridSearch_Final.css" rel="stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pay_central">
        <div class="hdc">
            DBBackup
        </div>
        <div class="sdpf">
            <div class="rose1">
                <table class="MainTablebdr" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 298px; margin: 10px auto 0 auto !Important; padding: 2px; vertical-align: top;">
                            <div class="MainTableHeaderDV">
                                <strong>Database Backup</strong>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="padding: 5px 0px;">
                            &nbsp; &nbsp; &nbsp; List Of Database :
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="padding: 5px 0px;">
                            <asp:DropDownList ID="ddlDownload" runat="server" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="padding: 10px 0px;">
                            <asp:Button ID="btnDatabaseBackup" runat="server" Width="70px" Text="Backup" OnClick="btnDatabaseBackup_Click" />&nbsp;
                            <asp:Button ID="btnDownLoad" runat="server" Text="Download" Width="70px" OnClick="btnDownLoad_Click" />&nbsp;
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="70px" OnClick="btnRemove_Click" />
                        </td>
                    </tr>
                </table>
                     <div style="padding-top: 10px; text-align: center; font-weight: bold;">
                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></div>
            </div>           
            </div>
         </div>
    
    </form>
</body>
</html>
