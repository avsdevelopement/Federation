<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmNewTDA.aspx.cs" Inherits="FrmNewTDA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {
            var message = '';

            var date = document.getElementById('<% =TxtProcode.ClientID%>').value;
            if (date == "") {
                //alert("Product Code is not present");
                message = 'Please Enter Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtProcode.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =txtAccNo.ClientID%>').value;
            if (date == "") {
                // alert("Account No is not present");
                message = 'Please Enter Account No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtAccNo.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =Txtcustno.ClientID%>').value;
            if (date == "") {
                //alert("Customer No is not present");
                message = 'Please Enter Customer No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =Txtcustno.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =ddlAccType.ClientID%>').value;
            if (date == "0") {
                // alert("Account type is not selected");
                message = 'Please select Account type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =ddlAccType.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =ddlOpType.ClientID%>').value;
            if (date == "0") {
                //alert("Member type is not selected");
                message = 'Please select Member type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =ddlOpType.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =ddlIntrestPay.ClientID%>').value;
            if (date == "0") {
                //alert("Interest Payout type is not selected");
                message = 'Please select Interest Payout type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =ddlIntrestPay.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =TxtDepoAmt.ClientID%>').value;
            if (date == "") {
                //alert("Deposite amount is not present");
                message = 'Please select Deposite amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtDepoAmt.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =ddlduration.ClientID%>').value;
            if (date == "0") {
                // alert("Period is not selected");
                message = 'Please select Period....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =ddlduration.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =TxtPeriod.ClientID%>').value;
            if (date == "") {
                // alert("Period is not present");
                message = 'Please select Period....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtPeriod.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =TxtRate.ClientID%>').value;
            if (date == "") {
                //alert("Rate is not present");
                message = 'Please enter Rate....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtRate.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =TxtIntrest.ClientID%>').value;
            if (date == "") {
                // alert("Interest is not present");
                message = 'Please Enter Interest....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtIntrest.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =TxtIntrest.ClientID%>').value;
            if (date == "") {
                // alert("Interest is not present");
                message = 'Please Enter Interest....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtIntrest.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =TxtMaturity.ClientID%>').value;
            if (date == "") {
                //alert("Maturity amount is not present");
                message = 'Please Enter Maturity amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtMaturity.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =DtDueDate.ClientID%>').value;
            if (date == "") {
                //alert("Due date is not present");
                message = 'Please Enter Due date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =DtDueDate.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =TxtProcode1.ClientID%>').value;
            if (date == "") {
                //alert("Product code is not present");
                message = 'Please Enter Product code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtProcode1.ClientID%>').focus();
                return false;
            }

            var date = document.getElementById('<% =TxtAccNo1.ClientID%>').value;
            if (date == "") {
                // alert("Account No is not present");
                message = 'Please Enter Account No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtAccNo1.ClientID%>').focus();
                return false;
            }

        }
    </script>

    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <script src="../../assets/global/plugins/bootstrap-tabdrop/js/bootstrap-tabdrop.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
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
                                                        <asp:LinkButton ID="BtnSubmit" runat="server" Text="Autorize" class="btn btn-default" Style="border: 1px solid #3561dc;" OnClientClick="javascript:return validate();" OnClick="BtnSubmit_Click"><i class="fa fa-plus-circle"></i>Create</asp:LinkButton>
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
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>

                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                                        <div class="col-md-3">

                                                            <asp:TextBox ID="TxtProcode" runat="server" Style="Width: 20%; height: 28px; border: 1px solid #89c4f4;" AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged"></asp:TextBox>
                                                            <asp:TextBox ID="TxtProName" runat="server" Style="width: 79%; height: 28px; margin-left: -3px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtAccNo" CssClass="form-control" PlaceHolder="Account No /  Receipt No" runat="server" AutoPostBack="true" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Customer : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="Txtcustno" CssClass="form-control" PlaceHolder="Cust No" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtcustName" CssClass="form-control" PlaceHolder="Name" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Account Type : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control" Enabled="False">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <label class="control-label col-md-2">Member Type: <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlOpType" runat="server" CssClass="form-control" Enabled="False">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%-- <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-12">
                                                 <label class="control-label col-md-2">Receipt No: <span class="required">* </span></label> 
                                                <div class="col-md-4">
                                                     <asp:TextBox ID="TextBox3" CssClass="form-control" PlaceHolder="Receipt No" runat="server" Enabled="False"></asp:TextBox>
                                                </div>                                         
                                            </div>
                                        </div>--%>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Deposit Date: <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="dtDeposDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="dtDeposDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="dtDeposDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <label class="control-label col-md-2">Interest Payout: <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlIntrestPay" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Deposit Amount : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtDepoAmt" CssClass="form-control" PlaceHolder="Deposit Amount" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Period : <span class="required">* </span></label>
                                                        <div class="col-md-2" style="margin-right: -24px;">
                                                            <asp:DropDownList ID="ddlduration" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="M">Months</asp:ListItem>
                                                                <asp:ListItem Value="D">Days</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" AutoPostBack="true" OnTextChanged="TxtPeriod_TextChanged" Style="width: 77px;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Rate : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtRate" CssClass="form-control" PlaceHolder="Rate" runat="server" Enabled="false" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Interest Amount : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtIntrest" CssClass="form-control" PlaceHolder="Interest Amount" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Maturity Amount : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtMaturity" CssClass="form-control" PlaceHolder="Maturity Amount" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Due Date : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="DtDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="DtDueDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="DtDueDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-lg-12">
                                                    <div class="col-md-3">
                                                        <h4><b>Transfer Account</b></h4>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1" style="width: 165px">Product Code : <span class="required">* </span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtProcode1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProcode1_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtProName1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProName1_TextChanged"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtProName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetGlName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                    <label class="control-label col-md-1" style="width: 160px">Account No : <span class="required">* </span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="Account No /  Receipt No" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtcustName1" CssClass="form-control" Width="270px" Placeholder="Acc Name" runat="server" AutoPostBack="true" OnTextChanged="TxtcustName1_TextChanged"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="Autoaccname4" runat="server" TargetControlID="TxtAccName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetAccName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row" style="margin: 7px 0 7px 0">
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <%--<button type="button" class="btn blue" >Submit</button>--%>
                                                <%--OnClientClick="javascript:return validate();"--%>
                                                <%--<asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click"/>--%>
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
            <div id="alertModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
                        </div>
                        <div class="modal-body">
                            <p></p>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%--<div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                               Term Deposit Details 
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills">
                                                    <li>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="a"  class="btn btn-default" Style="border: 1px solid #3561dc;padding: 6px 5px;"><i class="fa fa-arrows"></i>Add</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="a"  class="btn btn-default" Style="border: 1px solid #3561dc;padding: 6px 5px;"><i class="fa fa-asterisk"></i>Modify</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a"  class="btn btn-default" Style="border: 1px solid #3561dc;padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Delete</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkModify" runat="server" Text="MD" class="btn btn-default" Style="border: 1px solid #3561dc;padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Authorize</asp:LinkButton>
                                                    </li>
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight:bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>
                                            

                                           
                                             <!-- mycode start  -->
                                                <div style="border: 1px solid #3598dc">

                                                <div class="row" style="margin-top:12px;margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                           <label class="control-label ">Deposite GL</label>
                                                         </div>
                                                        <div class="col-md-3">
                                                              <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Miss" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Mr" Value="3"></asp:ListItem>
                                                              </asp:DropDownList>
                                                        </div>
                                                         <div class="col-md-2" style="padding:0; width:15%;padding-left: 2%;">
                                                           <label class="control-label ">Account No</label>
                                                         </div>
                                                        <div class="col-md-4" style="padding-left: 7.6%;padding-right: 0px;">
                                                             <asp:TextBox ID="TextBox1" runat="server" style="Width:48%;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox2" runat="server" style="width:50%;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                      <div class="col-md-3">
                                                        <label class="control-label">Customer No</label>
                                                       </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TextBox5" CssClass="form-control" placeholder="Customer No" runat="server"></asp:TextBox>
                                                        </div>
                                                        
                                                         <div class="col-md-3">
                                                        <label class="control-label">Customer Type</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                              <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Unbound" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Mr" Value="3"></asp:ListItem>
                                                              </asp:DropDownList>
                                                        </div>
                                                      </div>
                                                </div>

                                               

                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Member Type </label>
                                                         </div>
                                                         <div class="col-md-3">
                                                                <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Unbound" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Mr" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                         </div>
                                                        <div class="col-md-3">
                                                              <label class="control-label">Deposite Payout </label>
                                                         </div>
                                                         <div class="col-md-3">
                                                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Unbound" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Mr" Value="3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                         </div>
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Deposite Date</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox3" CssClass="form-control" type="date"  PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-3">
                                                              <label class="control-label">Period </label>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <asp:TextBox ID="TextBox4" runat="server" style="Width:34%;height:33px;border: 1px solid #89c4f4;"></asp:TextBox>
                                                             <asp:DropDownList ID="DropDownList4" runat="server" style="Width:64%;height:33px;">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Month" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="year" Value="2"></asp:ListItem>
                                                                        
                                                                </asp:DropDownList>
                                                             
                                                         </div>   
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Deposite Amount</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox10" CssClass="form-control" placeholder="Deposite Amount" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-3">
                                                              <label class="control-label">Interest </label>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <asp:TextBox ID="TextBox11" CssClass="form-control" placeholder="Interest" runat="server"></asp:TextBox>
                                                         </div>   
                                                       </div>
                                                </div>
                                               <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Int Rate</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox12" CssClass="form-control" placeholder="Int Rate" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-3">
                                                              <label class="control-label">Due Date </label>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <asp:TextBox ID="TextBox13" CssClass="form-control" type="date" placeholder="Due Date" runat="server"></asp:TextBox>
                                                         </div>   
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Matured Amount</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox14" CssClass="form-control" placeholder="Matured Amount" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-3">
                                                              
                                                         </div>
                                                         <div class="col-md-3">
                                                            
                                                         </div>   
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Transfer Customer No</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox15" CssClass="form-control" placeholder="Transfer Customer No" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-3">
                                                              <label class="control-label">Transfer Customer Name</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox16" CssClass="form-control" placeholder="Transfer Customer Name" runat="server"></asp:TextBox>
                                                         </div>   
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Transfer Customer No</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox17" CssClass="form-control" placeholder="Transfer Customer No" runat="server"></asp:TextBox>
                                                         </div>
                                                        <div class="col-md-3">
                                                              <label class="control-label">Transfer Customer Name</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox18" CssClass="form-control" placeholder="Transfer Customer Name" runat="server"></asp:TextBox>
                                                         </div>   
                                                       </div>
                                                </div>
                                                <div class="row" style="margin-bottom:10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-3">
                                                              <label class="control-label">Transfer Account type</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                             <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="months" Value="months"></asp:ListItem>
                                                                        <asp:ListItem Text="year" Value="year"></asp:ListItem>
                                                            </asp:DropDownList>
                                                         </div>
                                                        <div class="col-md-3" style="padding-right:0px;">
                                                              <label class="control-label">Transfer Customer Account</label>
                                                         </div>
                                                         <div class="col-md-3">
                                                              <asp:TextBox ID="TextBox20" CssClass="form-control" placeholder="Transfer Customer Account" runat="server"></asp:TextBox>
                                                         </div>   
                                                       </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-lg-10">
                                                     <asp:LinkButton ID="LinkButton3" runat="server" Text="MD" class="btn btn-primary">Submit</asp:LinkButton>
                                                        </div>
                                                     </div>
                                                </div>
                                                  
                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: center; margin-top:12px;">
                                                      
                                                     <asp:LinkButton ID="first" runat="server" Text="a"  class="btn btn-default" OnClientClick="javascript:return validatemine();"><i class="fa fa-arrows"></i>First</asp:LinkButton>
                                                        <asp:LinkButton ID="second" runat="server" Text="ab"  class="btn btn-default" OnClientClick="javascript:return validatemine();"><i class="fa fa-arrows"></i>First</asp:LinkButton>
                                                        <asp:LinkButton ID="third" runat="server" Text="ac"  class="btn btn-default" OnClientClick="javascript:return validatemine();"><i class="fa fa-arrows"></i>First</asp:LinkButton>
                                                       
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
            </div>--%>




