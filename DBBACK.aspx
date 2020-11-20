<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DBBACK.aspx.cs" Inherits="DBBACK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">  
<head id="Head1" runat="server">  
    <title>Backup and Restore DataBase</title>  
</head>  
<body>  
    <form id="form1" runat="server">  
    <div>  
        <table cellpadding="10" cellspacing="10" style="border: solid 10px red; background-color: Skyblue;"  
            width="90%" align="center">  
            <tr>  
                <td style="height: 35px; background-color: Yellow; font-weight: bold; font-size: 16pt;  
                    font-family: Times New Roman; color: Red" align="center">  
                    Backup SQL Server DataBase  
                </td>  
            </tr>  
            <tr>  
                <td align="center">  
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>  
                </td>  
            </tr>  
            <tr>  
                <td align="right">  
                    <asp:Button ID="btnBackup" runat="server" Text="Backup DataBase" OnClick="btnBackup_Click" />  
                   
                </td>  
   
            </tr>   
        </table>  
    </div>  
    </form>  
</body>  
</html>  