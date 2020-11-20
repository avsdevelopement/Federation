<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="Fraagentcal.aspx.cs" Inherits="Fraagentcal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <asp:Panel ID="Panel1" runat="server" Height="400px">
        <div class="row">
            <div class="col-md-12">
                <div id="form_wizard_1" class="portlet box blue">
                    <div class="portlet-title">
                        <div class="caption">
                            <strong style="color: #FFFFFF; font-family: Arial">FRM Agent Calculation</strong>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="auto-style8" style="text-align: left">
                                            <div class="auto-style10">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="auto-style9">
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Agent No&nbsp;</span>&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
               <asp:TextBox ID="agent_no" onkeypress="javascript:return isNumber (event)" OnTextChanged="agentname_txtchg" AutoPostBack="true" runat="server" Height="24px" Width="164px" TextMode="Number" ValidateRequestMode="Enabled"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="Agent_name" runat="server" Width="419px" Height="24px"></asp:TextBox>
                                                <br />
                                                <br />
            </div>
            <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="410px" Width="1000px">
                &nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;From Date :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="Start_date"  type="date" onkeypress="javascript:return isNumber (event)" OnTextChanged="collection_txtchg" AutoPostBack="true" PlaceHolder="dd/mm/yyyy"  runat="server" Width="169px" Height="24px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To Date :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="last_date" runat="server" Height="24px" onkeypress="javascript:return isNumber (event)" OnTextChanged="collection_txtchg" AutoPostBack="true" PlaceHolder="dd/mm/yyyy" type="date" Width="182px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Collection&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:TextBox ID="Total_coll" runat="server" Width="162px" Height="24px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Commission %&nbsp;&nbsp; &nbsp;
               <asp:TextBox ID="commission" onkeypress="javascript:return isNumber (event)" OnTextChanged="commission_txtchg" AutoPostBack="true" runat="server" Width="162px" Height="24px" TextMode="Number" ToolTip="Enter the commission"></asp:TextBox>
                &nbsp;&nbsp;&nbsp; %<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Commission Amt&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="com_amt" runat="server" Width="162px" Height="24px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TDS Dedcation&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="td_ded" onkeypress="javascript:return isNumber (event)" OnTextChanged=" TDAmt_txtchg" AutoPostBack="true" runat="server" Width="112px" Height="24px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp; %&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="TDamt" runat="server" Width="101px" Height="24px"></asp:TextBox>
                &nbsp;<br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Net Commission&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:TextBox ID="net_commission" runat="server" Width="162px" Height="24px"></asp:TextBox>
                &nbsp;<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Transfer Acc No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="Savingaccno"  onkeypress="javascript:return isNumber (event)" OnTextChanged="saving_txtchg" AutoPostBack="true" runat="server" Width="162px" Height="24px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="SA_name" runat="server" Width="162px" Height="24px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="SA_no" runat="server" Width="162px" Height="24px"></asp:TextBox>
                &nbsp;<br />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <div class="col-lg-12" style="text-align: center; margin-top: 12px;">
                    <asp:Button ID="Pay0" runat="server" CssClass="btn btn-primary" OnClientClick="Javascript:return IsValide();" Text="Pay"  OnClick="Pay0_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Clear0" runat="server" CssClass="btn btn-primary" OnClientClick="Javascript:return IsValide();" Text="Clear" OnClick="Clear0_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Exit0" runat="server" CssClass="btn btn-primary" OnClientClick="Javascript:return IsValide();" Text="Exit" />
                    <br />
                    <br />
                </div>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
            </asp:Panel>
        </div>
                                        <div id="tab__blue" class="tab-pane active">
                                                     
                        
                                                                             
                                                           

                                            <div class="row" style="margin: 7px 0 7px 0">


                                            <div class="row">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

