<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="frmAVS5140.aspx.cs" Inherits="frmAVS5140" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnSubmit" />--%>
            <asp:PostBackTrigger ControlID="btnEncrypt" />
            <asp:PostBackTrigger ControlID="btnDecrypt" />


            
        </Triggers>
        <ContentTemplate>
            <asp:Panel runat="server" Height="400px">
                <div class="row">
                    <%--<div class="col-md-3">
                        <asp:Label ID="Label1" runat="server" Text="Select Dates:"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtStartDate">
                        </asp:TextBoxWatermarkExtender>
                        <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtStartDate">
                        </asp:CalendarExtender>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtTime" MaxLength="2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        TO :
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtEndDate">
                        </asp:TextBoxWatermarkExtender>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtEndDate">
                        </asp:CalendarExtender>
                    </div>--%>

                </div>
                <div class="row">

                    <div class ="col-md-5" style="padding-top:50px">

                    </div>
                </div>
                <div class="row">
                   
                  <div class="col-md-3"> 
                     
                    </div>
                    <div class="col-md-4" style="margin-left:100px">
                        <asp:Button ID="btnEncrypt" runat="server" OnClick="btnEncrypt_Click" Text="Encrypt" Visible="false" CssClass="btn btn-success"  />
                        <asp:Button ID="btnDecrypt" runat="server" OnClick="btnDecrypt_Click"  Text="Decrypt" Visible="false" CssClass="btn btn-success"/>
                        <asp:Button ID="BtnCustomerData" runat="server" OnClick="BtnCustomerData_Click"  Text="BackUp" CssClass="btn btn-success"/>
                        <%--<asp:Button ID="btnSubmit" runat="server" Width="90px" Height="40px" OnClick="btnSubmit_Click" Text="BACKUP" />--%>
                    </div>
                  
                </div>                

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

