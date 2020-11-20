<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5115.aspx.cs" Inherits="FrmAVS5115" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        

        <%-- Only alphabet --%>
        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
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
                       Loan Interest Receivable (Recovery)
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">From Branch<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtFBrcd"   onkeypress="javascript:return isNumber(event)" CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                                                </div>
                                                    <div class="col-md-1">
                                                    <label class="control-label ">To Branch<span class="required">*</span></label>
                                                </div>
                                                  <div class="col-md-1">
                                                    <asp:TextBox ID="TxtTBrcd" onkeypress="javascript:return isNumber(event)" CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                          <div class="row" style="margin-bottom: 10px;">
                                                <div class="col-md-12">
                                                        <div class="col-md-2">
                                                    <label class="control-label ">Product Code <span class="required">*</span></label>
                                                            </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtPrd" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtPrd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtPrdName" CssClass="form-control" OnTextChanged="TxtPrdName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPrdName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetGlName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                          <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Month<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtMonth" onkeypress="javascript:return isNumber(event)" CssClass="form-control" MaxLength="2" runat="server"></asp:TextBox>
                                                </div>
                                                 <div class="col-md-1">
                                                    <label class="control-label ">Year<span class="required">*</span></label>
                                                </div>
                                                  <div class="col-md-1">
                                                    <asp:TextBox ID="TxtYear" onkeypress="javascript:return isNumber(event)" CssClass="form-control" MaxLength="4" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                    <div class="row">
                                        <div class="col-lg-12" style="text-align: center">
                                            <asp:Button ID="BtnRpt" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnRpt_Click" OnClientClick="Javascript:return Validate();" />
                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click"  />
                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" />
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
</asp:Content>

