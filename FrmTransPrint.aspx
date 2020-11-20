<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTransPrint.aspx.cs" Inherits="FrmTransPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
      
        function IsValide() {

            var brcd,brcdName, Prod,ProdName, TRXType, TRXActivity, Amount, UserCode,Name;
            brcd = document.getElementById('<%=txtBranchCode.ClientID%>').value;
            brcdName = document.getElementById('<%=txtBranchName.ClientID%>').value;
            Prod = document.getElementById('<%=txtProdCode.ClientID%>').value;
            ProdName = document.getElementById('<%=txtProdName.ClientID%>').value;
            TRXType = document.getElementById('<%=ddlTRXTYPE.ClientID%>').value;
            TRXActivity = document.getElementById('<%=ddlTRx.ClientID%>').value;
            Amount = document.getElementById('<%=txtAmount.ClientID%>').value;
            UserCode = document.getElementById('<%=txtAccno.ClientID%>').value;
            Name = document.getElementById('<%=txtAccName.ClientID%>').value;

              var message = '';
            
              if (document.getElementById('<%=rbtBranch.ClientID%>').value = "1") {
                  if (brcd == "") {
                      message = 'Please Enter Branch Code...!!\n';
                      $('#alertModal').find('.modal-body p').text(message);
                      $('#alertModal').modal('show')
                      document.getElementById('<%=txtBranchCode.ClientID%>').focus();
                      return false;
                  }
                  if (brcdName == "") {
                      message = 'Please Enter Branch Name...!!\n';
                      $('#alertModal').find('.modal-body p').text(message);
                      $('#alertModal').modal('show')
                      document.getElementById('<%=txtBranchName.ClientID%>').focus();
                      return false;
                  }
              }

            if (document.getElementById('<%=rbtProd.ClientID%>').value = "1") {
                if (Prod == "") {
                    message = 'Please Enter Product Code...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    document.getElementById('<%=txtProdCode.ClientID%>').focus();
                      return false;
                  }
                if (ProdName == "") {
                      message = 'Please Enter Product Name...!!\n';
                      $('#alertModal').find('.modal-body p').text(message);
                      $('#alertModal').modal('show')
                      document.getElementById('<%=txtProdName.ClientID%>').focus();
                      return false;
                  }
            } 
            if (document.getElementById('<%=rbtTRX.ClientID%>').value = "1") {
                if (TRXActivity == "0") {
                    message = 'Please Select TRX Activity...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    document.getElementById('<%=ddlTRx.ClientID%>').focus();
                    return false;
                }
            }

            if (document.getElementById('<%=rbtType.ClientID%>').value = "1") {
                if (TRXType == "0") {
                    message = 'Please Select TRX Type...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    document.getElementById('<%=ddlTRXTYPE.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=rbtAmount.ClientID%>').value = "1") {
                if (Amount == "") {
                    message = 'Please Enter Amount...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    document.getElementById('<%=txtAmount.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=rbtUser.ClientID%>').value = "1") {
                if (UserCode == "") {
                    message = 'Please Enter User Code...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    document.getElementById('<%=txtAccno.ClientID%>').focus();
                      return false;
                  }
                  if (Name == "") {
                      message = 'Please Enter User Name...!!\n';
                      $('#alertModal').find('.modal-body p').text(message);
                      $('#alertModal').modal('show')
                      document.getElementById('<%=txtAccName.ClientID%>').focus();
                      return false;
                  }
              }
        }
    </script>
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Scroll Printing
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">From Date:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtfromdate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfromdate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfromdate">
                                </asp:CalendarExtender>
                            </div>
                            <label class="control-label col-md-2">To Date:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txttodate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txttodate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txttodate">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Branch Code : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbtBranch" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtBranch_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1" style="padding: 5px">Specific</asp:ListItem>
                                    <asp:ListItem Value="2" style="padding: 5px" Selected="True">All</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtBranchCode" runat="server" Placeholder="Branch Code" CssClass="form-control" OnTextChanged="txtBranchCode_TextChanged" AutoPostBack="true" Visible="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtBranchName" runat="server" placeholder="Branch Name" Style="text-transform: uppercase" autocomplete="off" CssClass="form-control" OnTextChanged="txtBranchName_TextChanged" AutoPostBack="true" Visible="false"></asp:TextBox>
                                <div id="CustList1" style="height: 200px; overflow-y: scroll;" runat="server" visible="false"></div>
                                <asp:AutoCompleteExtender ID="AutoBrName" runat="server" TargetControlID="txtBranchName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                    EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetBranch">
                                </asp:AutoCompleteExtender>
                            </div>

                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbtProd" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtProd_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1" style="padding: 5px">Specific</asp:ListItem>
                                    <asp:ListItem Value="2" style="padding: 5px" Selected="True">All</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtProdCode" runat="server" PlaceHolder="Product Code" OnTextChanged="txtProdCode_TextChanged" Style="text-transform: uppercase" autocomplete="off" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtProdName" runat="server" PlaceHolder="Product Type" OnTextChanged="txtProdName_TextChanged" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>
                                 <div id="Div1" style="height: 200px; overflow-y: scroll;" runat="server" visible="false"></div>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                    EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="Div1" ServiceMethod="GetGlName">
                                </asp:AutoCompleteExtender>
                            </div>

                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Trx Activity : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbtTRX" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtTRX_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1" style="padding: 5px">Specific</asp:ListItem>
                                    <asp:ListItem Value="2" style="padding: 5px" Selected="True">All</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlTRx" runat="server" CssClass="form-control" Visible="false">
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                    <asp:ListItem Value="3">Cash Receipt</asp:ListItem>
                                    <asp:ListItem Value="7">Transfer Receipt</asp:ListItem>
                                    <asp:ListItem Value="30">DDS</asp:ListItem>
                                    <asp:ListItem Value="31">Clearing</asp:ListItem>
                                    <asp:ListItem Value="10">Interest</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Trx Type : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbtType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1" style="padding: 5px">Specific</asp:ListItem>
                                    <asp:ListItem Value="2" style="padding: 5px" Selected="True">All</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlTRXTYPE" runat="server" CssClass="form-control" Visible="false">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Credit</asp:ListItem>
                                    <asp:ListItem Value="2">Debit</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbtAmount" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtAmount_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1" style="padding: 5px">Specific</asp:ListItem>
                                    <asp:ListItem Value="2" style="padding: 5px" Selected="True">All</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtAmount" runat="server" PlaceHolder="Amount" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">User Code : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbtUser" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtUser_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="1" style="padding: 5px">Specific</asp:ListItem>
                                    <asp:ListItem Value="2" style="padding: 5px" Selected="True">All</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtAccno" runat="server" PlaceHolder="User Code" OnTextChanged="txtAccno_TextChanged" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtAccName" runat="server" PlaceHolder="User Name" OnTextChanged="txtAccName_TextChanged" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>
                                 <div id="Div2" style="height: 200px; overflow-y: scroll;" runat="server" visible="false"></div>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                    EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="Div2" ServiceMethod="GetMemNames">
                                </asp:AutoCompleteExtender>
                            </div>

                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;" id="DivInterest" runat="server" visible="false">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Skip Interest: <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbtInterest" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" style="padding: 5px">Yes</asp:ListItem>
                                    <asp:ListItem Value="2" style="padding: 5px" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;" id="DivDDS" runat="server" visible="false">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Skip DDS/Pigmy: <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:RadioButtonList ID="rbtdds" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" style="padding: 5px">Yes</asp:ListItem>
                                    <asp:ListItem Value="2" style="padding: 5px" Selected="True">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" align="center">
                            <asp:Button ID="btnsubmit" runat="server" Text="Show Report" OnClick="btnsubmit_Click" CssClass="btn btn-success" OnClientClick="Javascript:return IsValide();"/>
                            <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" CssClass="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>




        </div>
    </div>
    <div id="alertModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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
    <asp:HiddenField ID="Flag" runat="server" Value="0" />
    <asp:HiddenField ID="BRCD" runat="server" />
</asp:Content>

