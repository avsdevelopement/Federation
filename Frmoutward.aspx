<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="Frmoutward.aspx.cs" Inherits="Frmoutward" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIt(obj) {

            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

           <%-- Only Allow For alphabet --%>
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

        <%-- Only Allow For Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="grdoutward" />
        </Triggers>
        <ContentTemplate>
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Outward RTGS
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="portlet-body">

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Outward Details</strong></div>
                                            </div>
                                        </div>
                                        <div style="border: 1px solid #3598dc">
                                            <%--<div class="col-md-2"></div>
                                            <div class="row" style="margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-lg-10">
                                                        <asp:LinkButton ID="lnkAddNew" runat="server" Text="AN" class="btn btn-primary" TabIndex="29" OnClick="lnkAddNew_Click1"> Add New</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkAuthorise" runat="server" Text="AT" class="btn btn-primary" TabIndex="30" OnClick="lnkAuthorise_Click">Authorize</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="DL" class="btn btn-primary" TabIndex="31" OnClick="lnkDelete_Click">Delete</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>--%>
                                             <div class="row" style="margin: 7px 0 12px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Select </label>
                                                <div class="col-md-3">
                                                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn default" Text="Add new " OnClick="btnAddNew_Click" AccessKey="1" />
                                                 </div>
                                                <asp:Label ID="lblstatus" runat="server" CssClass="control-label col-md-3" Text="Add New" Style="color: blueviolet;"></asp:Label>
                                            </div>
                                        </div>

                                            <div runat="server" id="TblDiv_MainWindow">
                                            <div style="border: 1px solid #3598dc">
                                                <div class="portlet-body">
                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Mode : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label ">Payment Type :<span class="required"> *</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control" TabIndex="1">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Transfer" visible="false" runat="server">

                                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtProdType1" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type" TabIndex="2"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="input-icon">
                                                                        <i class="fa fa-search"></i>
                                                                        <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server" TabIndex="3"></asp:TextBox>
                                                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Account No:<span class="required"> *</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged" TabIndex="4"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="input-icon">
                                                                        <i class="fa fa-search"></i>
                                                                        <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Search Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                                        <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Balance:<span class="required"> *</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtBalance" CssClass="form-control" PlaceHolder="Balance" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Transfer1" visible="false" runat="server">
                                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Instrument No. :</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="Txtinstno" MaxLength="6" placeholder="CHEQUE NUMBER" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="5"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Instrument Date :</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtChequeDate" MaxLength="10" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" TabIndex="6"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="DivAmount" visible="false" runat="server">
                                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Naration : <span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                           
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Transaction Type<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="DDltransctntype" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDltransctntype_SelectedIndexChanged" AutoPostBack="true" TabIndex="8">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txttransactiontype" CssClass="form-control" AutoPostBack="true" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Beneficiary Code</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtbeneficrycode" CssClass="form-control" MaxLength="13" AutoPostBack="true" runat="server" TabIndex="9" OnTextChanged="Txtbeneficrycode_TextChanged" ></asp:TextBox>
                                                    </div>


                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Beneficiary Account Number</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtbeneAccno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" MaxLength="25" AutoPostBack="true" OnTextChanged="TxtbeneAccno_TextChanged" runat="server" TabIndex="10"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Instrument Amount<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtInstAmt" onkeypress="javascript:return isNumber (event)" MaxLength="20" CssClass="form-control" AutoPostBack="true" runat="server" TabIndex="11" OnTextChanged="TxtInstAmt_TextChanged"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Beneficiary Name</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbenename" CssClass="form-control" MaxLength="200" AutoPostBack="true"  OnTextChanged="Txtbenename_TextChanged" runat="server" TabIndex="12"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Drawee Location</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtdraweloc" CssClass="form-control" MaxLength="20" AutoPostBack="true" OnTextChanged="Txtdraweloc_TextChanged" runat="server" TabIndex="13"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Print Location</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtprntloc" CssClass="form-control" MaxLength="20" AutoPostBack="true"  OnTextChanged="Txtprntloc_TextChanged" runat="server" TabIndex="14"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Bene Address 1</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbeneadd1" CssClass="form-control" MaxLength="70"  OnTextChanged="Txtbeneadd1_TextChanged"  AutoPostBack="true" runat="server" TabIndex="15"></asp:TextBox>
                                                    </div>




                                                    <label class="control-label col-md-2">Bene Address 2</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbeneadd2" CssClass="form-control" MaxLength="70"  OnTextChanged="Txtbeneadd2_TextChanged" AutoPostBack="true" runat="server" TabIndex="16"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">

                                                    <label class="control-label col-md-2">Bene Address 3</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbeneadd3" CssClass="form-control" MaxLength="70"  OnTextChanged="Txtbeneadd3_TextChanged"  AutoPostBack="true" runat="server" TabIndex="17"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Bene Address 4</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbeneadd4" CssClass="form-control" MaxLength="70"  OnTextChanged="Txtbeneadd4_TextChanged"  AutoPostBack="true" runat="server" TabIndex="18"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Bene Address 5</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbeneadd5" CssClass="form-control"   OnTextChanged="Txtbeneadd5_TextChanged" MaxLength="20" AutoPostBack="true" runat="server" TabIndex="19"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Instruction Reference Number</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtinstrefno" MaxLength="20" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtinstrefno_TextChanged" CssClass="form-control" AutoPostBack="true" runat="server" TabIndex="20"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2"></div>
                                                    <label class="control-label col-md-2">Customer Reference Number<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtcustrefno" MaxLength="20" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtcustrefno_TextChanged" CssClass="form-control" AutoPostBack="true" runat="server" TabIndex="21"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Payment Details 1</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtpayd1" MaxLength="30" CssClass="form-control" AutoPostBack="true" OnTextChanged="Txtpayd1_TextChanged" runat="server" TabIndex="22"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Payment Details 2</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtpayd2" MaxLength="30" CssClass="form-control" OnTextChanged="Txtpayd2_TextChanged" AutoPostBack="true" runat="server" TabIndex="23"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">

                                                    <label class="control-label col-md-2">Payment Details 3</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtpayd3" MaxLength="30" CssClass="form-control" OnTextChanged="Txtpayd3_TextChanged" AutoPostBack="true" runat="server" TabIndex="24"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Payment Details 4</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtpayd4" MaxLength="30" CssClass="form-control" OnTextChanged="Txtpayd4_TextChanged" AutoPostBack="true" runat="server" TabIndex="25"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Payment Details 5<span class="required">*</span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtpayd5" MaxLength="140" CssClass="form-control" OnTextChanged="Txtpayd5_TextChanged" AutoPostBack="true" runat="server" TabIndex="26"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Payment Details 6<span class="required">*</span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtpayd6" MaxLength="30" CssClass="form-control" OnTextChanged="Txtpayd6_TextChanged" AutoPostBack="true" runat="server" TabIndex="27"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Payment Details 7<span class="required">*</span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtpayd7" MaxLength="35" CssClass="form-control" OnTextChanged="Txtpayd7_TextChanged" AutoPostBack="true" runat="server" TabIndex="28"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Cheque Number</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtchequeno" Width="70%" MaxLength="6" OnTextChanged="Txtchequeno_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control" AutoPostBack="true" runat="server" TabIndex="29"></asp:TextBox>
                                                    </div>


                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Chq/Trn Date<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtchqtrndate" Width="70%" onkeyup="FormatIt(this)" OnTextChanged="Txtchqtrndate_TextChanged" onkeypress="javascript:return isNumber (event)" MaxLength="10" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server" TabIndex="30"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="Txtchqtrndate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txtchqtrndate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div class="col-md-2"></div>
                                                    <label class="control-label col-md-2">MICR Number</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtmicrno" MaxLength="15" Width="90%" OnTextChanged="Txtmicrno_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control" AutoPostBack="true" runat="server" TabIndex="31"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>


                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">IFC Code</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtifccode" MaxLength="15" Width="90%" CssClass="form-control" OnTextChanged="Txtifccode_TextChanged" AutoPostBack="true" runat="server" TabIndex="32"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2"></div>
                                                    <label class="control-label col-md-2">Bene Bank Name</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbenebankname" MaxLength="100" CssClass="form-control" OnTextChanged="Txtbenebankname_TextChanged" AutoPostBack="true" runat="server" TabIndex="33"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Bene Bank Branch Name</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbenebranchnme" MaxLength="40" CssClass="form-control" OnTextChanged="Txtbenebranchnme_TextChanged" AutoPostBack="true" runat="server" TabIndex="34"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Beneficiary email Id</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txtbeneemail" MaxLength="100" CssClass="form-control" OnTextChanged="Txtbeneemail_TextChanged" AutoPostBack="true" runat="server" TabIndex="35"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        
                                   
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" OnClientClick="Javascript:return IsValide()" TabIndex="36" />
                                                <asp:Button ID="Btnclear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="Btnclear_Click" TabIndex="37" />
                                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="BtnExit_Click" TabIndex="38" />
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
            </div>
        </div>
    </div>
    </div>

      <div class="row" id="Div_grid" runat="server">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdoutward" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" OnPageIndexChanging="grdoutward_PageIndexChanging"
                                    PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" OnSelectedIndexChanged="grdoutward_SelectedIndexChanged"
                                    OnRowDataBound="grdoutward_RowDataBound" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'>></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PaymntType" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PaymentType" runat="server" Text='<%# Eval("PaymentType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prdcode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Productcode" runat="server" Text='<%# Eval("Productcode ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AccNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AccountNo" runat="server" Text='<%# Eval("AccountNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                   

                                        <asp:TemplateField HeaderText="BeneAccNo." Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BeneficiaryAccNo" runat="server" Text='<%# Eval("BeneficiaryAccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="padding: 0 0 5px 0">
                                                    <asp:Label ID="Lbl_Numofnst" runat="server" />
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="InstrumentAmount" runat="server" Text='<%# Eval("InstrumentAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BeneName" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BeneficiaryName" runat="server" Text='<%# Eval("BeneficiaryName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BeneBankName" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BeneBankName" runat="server" Text='<%# Eval("BeneBankName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IFCcode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="IFCcode" runat="server" Text='<%# Eval("IFCcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            
                                          <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("BeneficiaryAccNo")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("BeneficiaryAccNo")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                    <SelectedRowStyle BackColor="#66FF99" />
                                    <EditRowStyle BackColor="#FFFF99" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                            </th>
                        </tr>
                    </thead>
                </table>
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

