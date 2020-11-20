<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDeadStockDetails.aspx.cs" Inherits="FrmDeadStockDetails" %>

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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Dead Stock Details
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row" style="margin: 7px 0 12px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Select </label>
                            <div class="col-md-3">
                                <asp:Button ID="btnAddNew" runat="server" CssClass="btn default" Text="Add new " OnClick="btnAddNew_Click" AccessKey="1" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                        </div>
                    </div>
                     <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Product Code <span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtprdcode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="txtprdcode_TextChanged" AutoPostBack="true" runat="server" TabIndex="1"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtprdname" CssClass="form-control" placeholder="Product Name" OnTextChanged="txtprdname_TextChanged" AutoPostBack="true" runat="server" TabIndex="2"></asp:TextBox>
                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtprdname"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetGlName" CompletionListElementID="CustList">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <label class="control-label col-md-2">Account No <span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtaccno" onkeypress="javascript:return isNumber (event)" TabIndex="3" CssClass="form-control" placeholder="No" OnTextChanged="txtaccno_TextChanged1" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtaccname" TabIndex="4" CssClass="form-control" placeholder="Account Name" OnTextChanged="txtaccname_TextChanged1" AutoPostBack="true" runat="server"></asp:TextBox>
                                        <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtaccname"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                    <div runat="server" id="TblDiv_MainWindow">
                        <div id="Depositdiv" runat="server">

                           


                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Description <span class="required">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtdesc" TabIndex="5" CssClass="form-control" placeholder="Description" OnTextChanged="txtdesc_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Type Of Assest<span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtasstno"  Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="txtasstno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtasstname" CssClass="form-control" TabIndex="6" placeholder="Assest Name" Enabled="false" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-2">Date <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtdate" TabIndex="7" Enable="false" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtdate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Status<span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtstatusno" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="txtstatusno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="DDLstatusname"  TabIndex="8" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDLstatusname_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                                    </div>
                                    <label class="control-label col-md-2">Sanction Auth<span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtsanctnno" Enabled="false"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="txtsanctnno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="DDlSanction" runat="server" TabIndex="9" CssClass="form-control" OnSelectedIndexChanged="DDlSanction_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                                     </div>
                                </div>
                            </div>


                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Vendor's Name <span class="required">*</span></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtvendorname" TabIndex="10" CssClass="form-control" placeholder="Name" OnTextChanged="txtvendorname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-2">Period <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtperiod" TabIndex="11" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtperiod_TextChanged" CssClass="form-control" placeholder="Period" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">ItemDetails <span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtitemno" Enabled="false" TabIndex="12" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="txtitemno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="DDlitemno" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlitemno_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                                    </div>
                                    <label class="control-label col-md-2">Type Of Item <span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtitemtypeno" Enabled="false"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Type" OnTextChanged="txtitemtypeno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                       <asp:DropDownList ID="DDltypeofitm" TabIndex="13" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDltypeofitm_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>


                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">IdCode <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtIdCode" TabIndex="14" CssClass="form-control" OnTextChanged="TxtIdCode_TextChanged" onkeypress="javascript:return isNumber (event)" placeholder="Code" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Tr Branch/Dept <span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtbrnchno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" Enabled="false" placeholder="Type" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlbranchname" TabIndex="15" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlbranchname_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Name <span class="required">*</span></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtname" OnTextChanged="txtname_TextChanged" TabIndex="16" CssClass="form-control" placeholder="Name" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-2">Waranty/Guaranty<span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlwaranty" runat="server" CssClass="form-control" TabIndex="17">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">AMC Details <span class="required">*</span></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtamcdetail" CssClass="form-control" placeholder="Details" runat="server" TabIndex="18"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-2">Value <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtvalue" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="txtvalue_TextChanged" placeholder="Value" AutoPostBack="true" runat="server" TabIndex="19"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Bill No <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtbillno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="txtbillno_TextChanged" placeholder="BillNo" AutoPostBack="true" runat="server" TabIndex="20"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Bill Date <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtbilldate" MaxLength="10" OnTextChanged="txtbilldate_TextChanged" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server" TabIndex="21"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtbilldate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtbilldate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Cheque No </label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtchequeno" MaxLength="6" OnTextChanged="txtchequeno_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="ChequeNo" TabIndex="22" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Cheque Date</label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtchequedate" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" TabIndex="23" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtchequedate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtchequedate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Assest Location Code <span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtalocno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" Enabled="false" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlAstLoc" TabIndex="24" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAstLoc_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="control-label col-md-2">Assest SubLocation<span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtasublocno" Enabled="false" OnTextChanged="txtasublocno_TextChanged1"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="DDlASL" runat="server" TabIndex="25" CssClass="form-control" OnSelectedIndexChanged="DDlASL_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">AssestDescription <span class="required">*</span></label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtassestdesc" OnTextChanged="txtassestdesc_TextChanged" TabIndex="26" CssClass="form-control" placeholder="Description" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Purchase Date <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtpurchasedate" MaxLength="10" TabIndex="27" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtpurchasedate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtpurchasedate">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">EntryDate <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtentrydate" MaxLength="10" TabIndex="28" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" TargetControlID="txtentrydate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtentrydate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">AvblQuantity <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtquantity" OnTextChanged="txtquantity_TextChanged" TabIndex="29" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Quantity" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Value Per Unit <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtValueUnit" OnTextChanged="TxtValueUnit_TextChanged" TabIndex="30" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Value" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Purchase Value <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="Txtpurchase" OnTextChanged="Txtpurchase_TextChanged" TabIndex="31" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Value" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Value AsOnDate<span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtvalason" OnTextChanged="txtvalason_TextChanged" TabIndex="32" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Value" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">PerDep%<span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtperdep" OnTextChanged="txtperdep_TextChanged" TabIndex="33" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Percentage" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Dep Type <span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtdepno" Enabled="false"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="txtdepno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                         <asp:DropDownList ID="DDlDep" runat="server" TabIndex="34" CssClass="form-control" OnSelectedIndexChanged="DDlDep_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Book Balance<span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtbookbal" TabIndex="35" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Value" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">ClosingBalAs/Gl <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtclsgbal" onkeypress="javascript:return isNumber (event)" CssClass="form-control" Enabled="false" placeholder="Code" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-4 col-md-9">
                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" TabIndex="36" />
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" TabIndex="37" />
                                        <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" TabIndex="38" />
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
                                <asp:GridView ID="grdDead" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" OnPageIndexChanging="grdDead_PageIndexChanging"
                                    PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" OnSelectedIndexChanged="grdDead_SelectedIndexChanged"
                                    OnRowDataBound="grdDead_RowDataBound" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PrdCode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PRDCODE" runat="server" Text='<%# Eval("PRDCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accno" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCTNO" runat="server" Text='<%# Eval("ACCNO ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <%--    <asp:TemplateField HeaderText="CustName" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("ACCNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkModify_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("ACCNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("ACCNO")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
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
</asp:Content>

