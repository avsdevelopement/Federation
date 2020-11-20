<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDeposite.aspx.cs" Inherits="FrmDeposite" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Deposite
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">                                    
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">

                                         <ul class="nav nav-pills">
                                                    <li>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Autorize" class="btn btn-default" Style="border: 1px solid #3561dc;"><i class="fa fa-plus-circle"></i>Create</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="BtnAutorize" runat="server" Text="Autorize" class="btn btn-default" Style="border: 1px solid #3561dc;"><i class="fa fa-plus-circle"></i>Autorize</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="BtnReport" runat="server" Text="Report" class="btn btn-default" Style="border: 1px solid #3561dc;"><i class="fa fa-pencil-square-o"></i>Modify</asp:LinkButton>
                                                    </li>

                                                    <li>
                                                        <asp:LinkButton ID="BtnExit" runat="server" Text="Exit" class="btn btn-default" Style="border: 1px solid #3561dc;"><i class="fa fa-times"></i>Delete</asp:LinkButton>
                                                    </li>
                                                   

                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight:bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                            </ul> 



                                        <div class="row" style="margin:7px 0 7px 0; margin-top:20px;">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Customer No. : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TxtAgentNo" runat="server" style="Width:38%;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                     <asp:TextBox ID="TxtAgentName" runat="server" style="width:61%;height:33px;margin-left: -3px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                </div>
                                                 <label class="control-label col-md-2">Product Code : <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TextBox1" runat="server" style="Width:38%;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                     <asp:TextBox ID="TextBox2" runat="server" style="width:61%;height:33px;margin-left: -3px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                </div> 
                                                                                               
                                            </div>
                                        </div>

                                         <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="Txtdate" CssClass="form-control" PlaceHolder="Account No" runat="server"></asp:TextBox>
                                                </div>
                                                 <label class="control-label col-md-2">Account Type : <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                      <asp:DropDownList ID="ddl" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="member" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="member" Value="2"></asp:ListItem>
                                                     </asp:DropDownList>
                                                </div> 
                                                                                               
                                            </div>
                                        </div>

                                        <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Member Type: <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                     <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="member" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="member" Value="2"></asp:ListItem>
                                                     </asp:DropDownList>
                                                </div>
                                                 <label class="control-label col-md-2">Receipt No: <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TextBox3" CssClass="form-control" PlaceHolder="Receipt No" runat="server"></asp:TextBox>
                                                </div> 
                                                                                               
                                            </div>
                                        </div>
                                        <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Deposit Date: <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBox5" CssClass="form-control" type="date"  PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                       <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TextBox5">
                                                            </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TextBox5">
                                                            </asp:CalendarExtender>
                                                </div>
                                                 <label class="control-label col-md-2">Interest Payout: <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TextBox4" CssClass="form-control" PlaceHolder="Interest Payout" runat="server"></asp:TextBox>
                                                </div> 
                                                                                               
                                            </div>
                                        </div>
                                       <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Deposit Amount : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBoxDepo" CssClass="form-control" PlaceHolder="Deposit Amount" runat="server"></asp:TextBox>
                                                </div>
                                                 <label class="control-label col-md-2">Period : <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TextBox7" CssClass="form-control" PlaceHolder="Period" runat="server"></asp:TextBox>
                                                </div> 
                                                                                               
                                            </div>
                                       </div>
                                       <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Rate : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBox6" CssClass="form-control" PlaceHolder="Rate" runat="server"></asp:TextBox>
                                                </div>
                                                 <label class="control-label col-md-2">Interest Amount : <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TextBox8" CssClass="form-control" PlaceHolder="Interest Amount" runat="server"></asp:TextBox>
                                                </div> 
                                                                                               
                                            </div>
                                        </div>
                                       <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Maturity Amount : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBox9" CssClass="form-control" PlaceHolder="Maturity Amount" runat="server"></asp:TextBox>
                                                </div>
                                                 <label class="control-label col-md-2">Due Date : <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBox10" CssClass="form-control" type="date" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TextBox10">
                                                            </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TextBox10">
                                                            </asp:CalendarExtender>
                                                </div> 
                                                                                               
                                            </div>
                                        </div>

                                         <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">    
                                                  <div class="col-md-4">
                                                    Transfer Account
                                                </div>
                                            </div>
                                        </div>

                                       <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TextBox11" runat="server" style="Width:38%;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                     <asp:TextBox ID="TextBox12" runat="server" style="width:61%;height:33px;margin-left: -3px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                </div>
                                                 <label class="control-label col-md-2">Account  No : <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TextBox13" runat="server" style="Width:38%;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                     <asp:TextBox ID="TextBox14" runat="server" style="width:61%;height:33px;margin-left: -3px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                </div> 
                                                                                               
                                            </div>
                                        </div>
                                         <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">                                                
                                                <label class="control-label col-md-2">Name : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBox15" CssClass="form-control" PlaceHolder="Name" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                      

                                       
                                                                                 
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <%--<button type="button" class="btn blue" >Submit</button>--%>
                                        <%--OnClientClick="javascript:return validate();"--%>
<%--                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click"/>--%>
                                        <%--<asp:Button ID="Button1" runat="server" CssClass="btn blue" Text="Submit"  OnClick="SaveOwg" OnClientClick="javascript:return validate();"/>--%>
                                        <%--<asp:Button ID="btnUpdate" runat="server" CssClass="btn blue" Text="Delete" OnClick="UpdateOwg" Visible="false"/>
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn blue" Text="Report" OnClick="btnReport_Click"/>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--</form>-->
                </div>
            </div>
        </div>
    </div>

</asp:Content>

