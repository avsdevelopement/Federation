<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTermDeposit.aspx.cs" Inherits="FrmTermDeposit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>   

           
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                               Term Deposit Account Details  
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                               
                                            </div>
                                            

                                           
                                             <!-- mycode start  -->
                                                <div style="border: 1px solid #3598dc">

                                                <div class="row" style="margin-top:12px;margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                           <label class="control-label ">GL Code:</label>
                                                         </div>
                                                          <div class="col-md-6">
                                                              <asp:TextBox ID="TextBox13" CssClass="form-control"  placeholder="GL Code" runat="server"></asp:TextBox>
                                                           </div>
                                                           <div class="col-md-3">
                                                               <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Unbound" Value="Unbound"></asp:ListItem>
                                                                        <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                           </div>
                                                         
                                                       
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                      <div class="col-md-3">
                                                        <label class="control-label">Customer No.:</label>
                                                       </div>
                                                       <div class="col-md-5">
                                                               <asp:TextBox ID="TextBox1" runat="server" style="Width:130px;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                               <asp:TextBox ID="TxtAgentName" runat="server" style="width:137px;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-3">
                                                           
                                                         </div>
                                                       </div>
                                                </div>

                                               

                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Account No:</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                                <asp:TextBox ID="TextBox8" CssClass="form-control"  placeholder="Account No" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-3">
                                                              <label class="control-label">Reciept Date:</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                               <asp:TextBox ID="TextBox9" CssClass="form-control" type="date" runat="server"></asp:TextBox>
                                                         </div>
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Deposit Amount :</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox3" CssClass="form-control"  PlaceHolder="Deposit Amount" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-2">
                                                              <label class="control-label">Monthly Installment :</label>
                                                         </div>
                                                         <div class="col-md-3 pull-right" >
                                                            <asp:TextBox ID="TextBox4" CssClass="form-control"  PlaceHolder="Monthly Installment" runat="server"></asp:TextBox>
                                                             
                                                         </div>   
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Period :</label>
                                                         </div>
                                                         <div class="col-md-1" style="padding:0px; margin-left:1.3%;margin-top: 10px;width: 23px;">
                                                            Year
                                                         </div>
                                                         <div class="col-md-1">
                                                             <asp:TextBox ID="TextBox10" CssClass="form-control" placeholder="Year" runat="server"></asp:TextBox>
                                                         </div>
                                                         <div class="col-md-1" style="padding:0px; margin-left:1.3%;margin-top: 10px;width: 33px;">
                                                            Month
                                                         </div>
                                                         <div class="col-md-1">
                                                               <asp:TextBox ID="TextBox5" CssClass="form-control" placeholder="Month" runat="server"></asp:TextBox>
                                                         </div>
                                                         <div class="col-md-1" style="padding:0px; margin-left:1.3%;margin-top: 10px;width: 23px;">
                                                            Day
                                                         </div>
                                                         <div class="col-md-1">
                                                                  <asp:TextBox ID="TextBox11" CssClass="form-control" placeholder="Day" runat="server"></asp:TextBox>
                                                         </div> 
                                                            
                                                       </div>
                                                </div>
                                               <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Rate Of Intrest  :</label>
                                                         </div>
                                                         <div class="col-md-2">
                                                               <asp:TextBox ID="TextBox6" CssClass="form-control" placeholder="Rate Of Intrest" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-2">
                                                               <label class="control-label">Installment Frequency :</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Unbound" Value="Unbound"></asp:ListItem>
                                                                        <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                              </asp:DropDownList>
                                                         </div> 
                                                             
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Maturity Date :</label>
                                                         </div>
                                                         <div class="col-md-2">
                                                               <asp:TextBox ID="TextBox7" CssClass="form-control" type="date" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-2">
                                                               <label class="control-label">Maturity Amount :</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <asp:TextBox ID="TextBox12" CssClass="form-control" placeholder="Maturity Amount" runat="server"></asp:TextBox>
                                                         </div> 
                                                       </div>
                                                </div> 
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Intreast Pay Frequency :</label>
                                                         </div>
                                                         <div class="col-md-2">
                                                              <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Unbound" Value="Unbound"></asp:ListItem>
                                                                        <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                              </asp:DropDownList>
                                                         </div>
                                                         <div class="col-md-2">
                                                               <label class="control-label">Intrest Amount :</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <asp:TextBox ID="TextBox14" CssClass="form-control" placeholder="Intrest Amount" runat="server"></asp:TextBox>
                                                         </div> 
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px; ">
                                                    <div class="col-lg-12">
                                                        <div class="col-lg-10">
                                                     <asp:LinkButton ID="LinkButton3" runat="server" Text="MD" class="btn btn-primary">Submit</asp:LinkButton>
                                                        </div>
                                                     </div>
                                                </div>
                                                  
                                                
                                            </div>
                                             <!-- mycode end  -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        
    
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

