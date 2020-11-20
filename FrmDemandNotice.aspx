<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDemandNotice.aspx.cs" Inherits="FrmDemandNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                       Demand Notice 
                    </div>
                </div>
                <div class="portlet-body form">
                    <div id="DIVAC" runat="server" class="form-horizontal">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                        <div id="Div1" runat= "server">
                                            
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Branch Code <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtbranchcode1" placeholder="Branch CODE" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" ></asp:TextBox>
                                                    </div>     
                                                </div>
                                            </div>
                                                  <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Loan Code <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtloancode1" AutoPostBack="true" placeholder="Loan Code" CssClass="form-control" OnTextChanged= "txtloancode1_TextChanged"  runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                       
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtloan1name" placeholder="Loan name"   CssClass="form-control"  runat="server"></asp:TextBox>
                                                        
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Accno <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TXTACCNO" placeholder="AccNo" AutoPostBack="true" CssClass="form-control" OnTextChanged=   "TXTACCNO_TextChanged"  runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                       
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtaccname" placeholder="Acc Name"   CssClass="form-control"  runat="server"></asp:TextBox>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                              
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Last Notice" OnClick= "btnSubmit_Click"  OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick= "btnExit_Click"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <d
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
</asp:Content>

