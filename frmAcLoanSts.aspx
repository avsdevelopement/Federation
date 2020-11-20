<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAcLoanSts.aspx.cs" Inherits="frmAcLoanSts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 117px;
        }

        .auto-style2
        {
            width: 117px;
            height: 27px;
        }

        .auto-style3
        {
            height: 27px;
        }

        .auto-style4
        {
            height: 27px;
            width: 149px;
        }

        .auto-style5
        {
            width: 149px;
        }

        .auto-style6
        {
            height: 27px;
            width: 550px;
        }

        .auto-style7
        {
            width: 550px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table style="width: 92%;">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label1" runat="server" Text="Account Type"></asp:Label>
                        &nbsp; </td>
                    <td class="auto-style4">
                       <asp:TextBox ID="TextBox1" runat="server" Width="120px"></asp:TextBox>
                    </td>
                    <td class="auto-style6">
                       <asp:TextBox ID="TextBox2" runat="server" style="text-transform:uppercase" Width="510px"></asp:TextBox></td>
                    <td class="auto-style3">
                        <asp:Label ID="Label4" runat="server" Text="A/c Open"></asp:Label>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="Label5" runat="server" Text="Period"></asp:Label>
                    </td>
                    <td class="auto-style3">
                       <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label2" runat="server" Text="Account No"></asp:Label>
                        &nbsp; </td>
                    <td class="auto-style5">
                       <asp:TextBox ID="TextBox4" runat="server" Width="117px"></asp:TextBox>
                    </td>
                    <td class="auto-style7">
                       <asp:TextBox ID="TextBox5" runat="server" Width="326px" style="margin-right: 0px"></asp:TextBox>&nbsp; 
                       <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                        </td>
                   
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                    </td>
                    <td class="auto-style3">
                        <asp:Label ID="Label11" runat="server" Text="Inst"></asp:Label>
                    </td>
                    <td class="auto-style3">
                       <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>

                    
                    

                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label3" runat="server" Text="From Date"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style7">
                        <asp:Label ID="Label12" runat="server" Text="To Date"></asp:Label>

                   
                        &nbsp;&nbsp;

                   
                        <asp:DropDownList ID="DropDownList3" runat="server">
                        </asp:DropDownList>
                    &nbsp;&nbsp;

                       <asp:Button ID="Button1" runat="server" Text="Button" />

                    </td>
                   <td>

                       </td>
                    <td>

                        <asp:Label ID="Label13" runat="server" Text="Ref"></asp:Label>

                    </td>
                    <td>

                       <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>

                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
