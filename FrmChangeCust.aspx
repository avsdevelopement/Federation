<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmChangeCust.aspx.cs" Inherits="FrmChangeCust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Table ID="tbl" runat="server">
        <asp:TableRow>
            <asp:TableCell Style="width:70px">
                <asp:Label ID="lblCust" runat="server" Text="Cust No"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="txtCust" runat="server" CssClass="form-control"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell colspan="2" style="padding-top:20px">
                <asp:Button ID="btnShow" runat="server" Text="Show Record" OnClick="btnShow_Click" CssClass="btn blue" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update Records" OnClick="btnUpdate_Click" CssClass="btn blue" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:GridView ID="griddetails" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>Prod Code</HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("AT") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Cust no</HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("CustNo") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Cust Name</HeaderTemplate>
                <ItemTemplate>
                    <%#Eval("CustName") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:HiddenField ID="hdnLast" runat="server" Value="0" />
</asp:Content>

