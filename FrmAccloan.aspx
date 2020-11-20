<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAccloan.aspx.cs" Inherits="FrmAccloan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1
        {
            height: 23px;
        }
        .auto-style2
        {
            height: 30px;
        }
        .auto-style5
        {
            height: 18px;
        }
        .auto-style7
        {
            height: 24px;
        }
        .auto-style8
        {
            height: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
          <center> <h2> ACCOUNT STATEMENT</h2></center>
            <table style="width: 92%;">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="Label1" runat="server" Text="Account Type"></asp:Label>
                        &nbsp; </td>
                    <td class="auto-style2">
                       <asp:TextBox ID="TextBox1" runat="server" Width="117px"></asp:TextBox>
                    </td>
                    <td class="auto-style2">
                       <asp:TextBox ID="TextBox2" runat="server" Width="493px"></asp:TextBox></td>
                    <td class="auto-style2">
                        <asp:Label ID="Label4" runat="server" Text="A/c Open"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="Label5" runat="server" Text="Period"></asp:Label>
                    </td>
                    <td class="auto-style2">
                       <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>

                </tr>
                <tr>
                    <td class="auto-style8">
                        <asp:Label ID="Label2" runat="server" Text="Account No"></asp:Label>
                        &nbsp; </td>
                    <td class="auto-style8">
                       <asp:TextBox ID="TextBox4" runat="server" Width="117px"></asp:TextBox>
                    </td>
                    <td class="auto-style8">
                       <asp:TextBox ID="TextBox5" runat="server" Width="326px" style="margin-right: 0px"></asp:TextBox>&nbsp; 
                       <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                        </td>
                   
                    <td class="auto-style8">
                        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                    </td>
                    <td class="auto-style8">
                        <asp:Label ID="Label11" runat="server" Text="Inst"></asp:Label>
                    </td>
                    <td class="auto-style8">
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

                       <asp:Button ID="Button1" runat="server" Text="Refresh" />

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
</asp:Content>

