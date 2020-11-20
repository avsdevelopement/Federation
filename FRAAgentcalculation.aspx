<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FRAAgentcalculation.aspx.cs" Inherits="FRA_Agent_calculation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style7 {
            height: 353px;
            width: 827px;
        }
        .auto-style8 {
            height: 0px;
            width: 830px;
        }
    </style>
</head>
<body style="height: 394px; width: 833px">
    <form id="form1" runat="server">
    <div>
    </div>
        <div class="auto-style7" style="text-align: center">
            <br />
            <strong>FRA Agent Calculation<br />
            <br />
            <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="39px">
                <strong>Agent No&nbsp;&nbsp; </strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="agent_c1" runat="server" Height="19px" Width="164px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="Agent_c2" runat="server" Width="419px"></asp:TextBox>
            </asp:Panel>
            </strong>
            <div class="auto-style8" style="text-align: left">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="2px" Height="270px" Width="825px">
                    &nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;From Date :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox2" CssClass="form-control" type="date" PlaceHolder="dd/mm/yyyy" runat="server" Width="174px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To Date :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control" PlaceHolder="dd/mm/yyyy" type="date" Width="174px"></asp:TextBox>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Collection&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox3" runat="server" Width="174px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Commission %&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox11" runat="server" Width="90px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; %<br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Commission Amt&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox4" runat="server" Width="174px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TD Dedcation&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox9" runat="server" Width="90px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; %&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox10" runat="server" Width="90px"></asp:TextBox>
                    &nbsp;<br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Net Commission&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox5" runat="server" Width="174px"></asp:TextBox>
                    &nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Saving Acc No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox6" runat="server" Width="174px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox7" runat="server" Width="174px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:TextBox ID="TextBox8" runat="server" Width="174px"></asp:TextBox>
                    &nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Pay_btn" runat="server" Text="PAY" Width="143px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="clear_btn" runat="server" Text="Clear" Width="123px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="exit_btn" runat="server" Text="Exit" Width="98px" />
                    <br />
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
