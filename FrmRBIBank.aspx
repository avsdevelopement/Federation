<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRBIBank.aspx.cs" Inherits="FrmRBIBank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        RBI Master
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Bank Code:<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtBANKCD" CssClass="form-control" Placeholder="Bank Code" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="txtBANKCD_TextChanged"></asp:TextBox>

                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Branch Code<span class="required">* </span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtBRANCHCD" CssClass="form-control" runat="server" Placeholder="Branch Code" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="txtBRANCHCD_TextChanged"></asp:TextBox>
                                                <%--<div id="Div1" style="height: 200px; overflow-y: scroll;"></div>--%>
                                                <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtBRANCHCD" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                            EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetBranchName" CompletionListElementID="Custlist2">
                                                        </asp:AutoCompleteExtender>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Bank Name:<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtDESCR" CssClass="form-control" runat="server" Placeholder="Bank Name" Enabled="true" MaxLength="510" AutoPostBack="true" OnTextChanged="txtDESCR_TextChanged"></asp:TextBox>

                                                <div id="Custlist2" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtDESCR" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                    EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetBankName" CompletionListElementID="Custlist2">
                                                </asp:AutoCompleteExtender>

                                            </div>

                                            <div id="Div1" class="col-md-2" runat="server" visible="false">
                                                <label class="control-label ">LMOYN<%--<span class="required"> *</span> --%></label>
                                            </div>
                                            <div id="Div2" class="col-md-3" runat="server" visible="false">
                                                <asp:DropDownList ID="ddlLMOYN" CssClass="form-control" runat="server" Height="29px" TabIndex="3">
                                                    <asp:ListItem Selected="True">N</asp:ListItem>
                                                    <asp:ListItem>Y</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2">

                                                <label class="control-label ">STATECD:<%--<span class="required"> *</span>--%></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtSTATECD" AutoPostBack="true" Placeholder="STATE" Enabled="true" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtSTATECD_TextChanged" CssClass="form-control"></asp:TextBox>
                                                <div id="Custlist3" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="AutoStateCD" runat="server" TargetControlID="txtSTATECD" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                    EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetStateName" CompletionListElementID="Custlist3">
                                                </asp:AutoCompleteExtender>
                                                <%--  <asp:DropDownList ID="ddlstate" CssClass="form-control" runat="server"  OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true">
                                                                   
                                                                </asp:DropDownList>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-3">
                                                             <asp:TextBox ID="txtLMOYN" AutoPostBack="true" Enabled="true" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtLMOYN_TextChanged" CssClass="form-control" ></asp:TextBox>
                                                            </div>--%>


                                    <div id="Div3" class="row" style="margin-top: 12px; margin-bottom: 10px;" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">MSYN:<%--<span class="required"> *</span>--%></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlMSYN" CssClass="form-control" runat="server" Height="29px" TabIndex="3">
                                                    <asp:ListItem Selected="True">N</asp:ListItem>
                                                    <asp:ListItem>Y</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <div class="col-md-3">
                                                              <asp:TextBox ID="txtMSYN" AutoPostBack="true" Enabled="true" runat="server" OnTextChanged="txtMSYN_TextChanged"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" ></asp:TextBox>--%>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">DDYN<%--<span class="required"> </span>--%></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlDDYN" CssClass="form-control" runat="server">
                                                    <asp:ListItem Selected="True">N</asp:ListItem>
                                                    <asp:ListItem>Y</asp:ListItem>
                                                </asp:DropDownList>


                                            </div>
                                        </div>
                                    </div>
                                    <div id="Div4" class="row" style="margin-top: 12px; margin-bottom: 10px;" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">MTYN:<%--<span class="required"> *</span>--%></label>
                                            </div>

                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlMTYN" CssClass="form-control" runat="server">
                                                    <asp:ListItem Selected="True">N</asp:ListItem>
                                                    <asp:ListItem>Y</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">TTYN<%--<span class="required"> </span>--%></label>
                                            </div>

                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlTTYN" CssClass="form-control" runat="server">
                                                    <asp:ListItem Selected="True">N</asp:ListItem>
                                                    <asp:ListItem>Y</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div id="Div5" class="row" style="margin-top: 12px; margin-bottom: 10px;" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">DDLIMIT:<%--<span class="required"> *</span>--%></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtDDLIMIT" AutoPostBack="true" Placeholder="DDLIMIT" Enabled="true" runat="server" OnTextChanged="txtDDLIMIT_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">MTLIMIT:<%--<span class="required"> *</span>--%></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtMTLIMIT" AutoPostBack="true" Placeholder="MTLIMIT" Enabled="true" runat="server" OnTextChanged="txtMTLIMIT_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>
                                            </div>


                                        </div>
                                    </div>
                                    <div id="Div6" class="row" style="margin-top: 12px; margin-bottom: 10px;" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">TTLIMIT:<%--<span class="required"> *</span>--%></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtTTLIMIT" AutoPostBack="true" Placeholder="TTLIMIT" Enabled="true" runat="server" OnTextChanged="txtTTLIMIT_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">DDCOLLBRCD:<%--<span class="required"> *</span>--%></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtDDCOLLBRCD" AutoPostBack="true" Placeholder="DDCOLLBRCD" Enabled="true" runat="server" OnTextChanged="txtDDCOLLBRCD_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Div7" class="row" style="margin-top: 12px; margin-bottom: 10px;" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">TTCOLLBRCD:<%--<span class="required"> *</span>--%></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtTTCOLLBRCD" AutoPostBack="true" Placeholder="TTCOLLBRCD" Enabled="true" runat="server" OnTextChanged="txtTTCOLLBRCD_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label ">DISTRICT:<%--<span class="required"> *</span>--%></label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtDISTRICT" AutoPostBack="true" Enabled="true" Placeholder="DISTRICT" runat="server" OnTextChanged="txtDISTRICT_TextChanged" CssClass="form-control" onkeypress="javascript:return OnltAlphabets (event)"></asp:TextBox>
                                            <div id="Custlist4" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtDISTRICT" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetDISTName" CompletionListElementID="Custlist4">
                                            </asp:AutoCompleteExtender>
                                            <%--   <asp:DropDownList ID="dddistrict" runat="server" CssClass="form-control" OnSelectedIndexChanged="dddistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                        </div>


                                        <div class="col-md-2">
                                            <label class="control-label ">MICR Code:<%--<span class="required"> *</span>--%></label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtMICRCode" Enabled="true" Placeholder="MICR Code" runat="server"
                                                CssClass="form-control"></asp:TextBox>

                                        </div>

                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-4 col-md-9">
                                            <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn blue" OnClick="btn_submit_Click" />

                                            <asp:Button ID="btnclear" runat="server" Text="ClearAll" CssClass="btn blue" OnClick="btnclear_Click" />

                                            <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="btn blue" OnClick="BtnBack_Click" />
                                            <asp:Button ID="Button2" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />
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

    <script type="text/javascript">
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
        function OnltAlphabets(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return true;

            return false;
        }
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert(" Enter valid Date");
            if (obj.value.length == 6) {
                var EnteredDate = obj.value;
                var date = EnteredDate.substring(0, 2);
                var month = EnteredDate.substring(3, 5);
                if (month == "01" || month == "03" || month == "05" || month == "07" || month == "08" || month == "10" || month == "12") {
                    if (date < "01" || date > "31") {
                        alert("Enter valid Date");
                        obj.value = "";
                    }
                }
                else if (month == "04" || month == "06" || month == "09" || month == "11") {
                    if (date < "01" || date > "30") {
                        alert("Enter valid Date");
                        obj.value = "";
                    }
                }
                else if (month == "02") {
                    if (date < "01" || date > "29") {
                        alert("Enter valid Date");
                        obj.value = "";
                    }
                }
                if (month < "01" || month > "12") {
                    alert("Enter valid Date");
                    obj.value = "";
                }
            }
        }

    </script>
</asp:Content>

