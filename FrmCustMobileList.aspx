<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCustMobileList.aspx.cs" Inherits="FrmCustMobileList" %>

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
                        Customer Mobile List
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="Depositdiv" runat="server">

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-1">From Branch<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="Txtfrmbrcd" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                                <label class="control-label col-md-2">To Branch<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="Txttobrcd" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                            <div class="col-lg-12">
                                <div class="col-md-1">
                                    <label>From CustNo<span class="required"></span></label>
                                </div>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtCustNo" CssClass="form-control" Placeholder="Cust No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtCustNo_TextChanged" AutoPostBack="true" Style="width: 70px;" TabIndex="2"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtCustName" CssClass="form-control" Placeholder="Customer Name" OnTextChanged="txtCustName_TextChanged" AutoPostBack="true" runat="server" Style="width: 360px;" />
                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtCustName"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="GetCustNames">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="Lbltocust" runat="server" Text="To Cust No"></asp:Label>
                                </div>
                                <div class="col-md-1" runat="server" id="Div_To">
                                    <asp:TextBox ID="TxtToCustno" CssClass="form-control" Placeholder="To Cust No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="TxtToCustno_TextChanged" AutoPostBack="true" Style="width: 70px;"></asp:TextBox>
                                </div>
                                <div class="col-md-4" runat="server" id="Div_ToName">
                                    <asp:TextBox ID="TxtToCustName" CssClass="form-control" Placeholder="To Customer Name" OnTextChanged="TxtToCustName_TextChanged" AutoPostBack="true" runat="server" Style="width: 360px;" />
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtToCustName"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="GetCustNames">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                            <div class="col-lg-12">
                                <div class="col-md-1">
                                    <label class="control-label">From Date <span class="required"></span></label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 10px;">
                            <div class="col-lg-12">
                                <div class="col-md-1">
                                    <label class="control-label">Select Type :</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="DdlActivity" runat="server" CssClass="form-control" Style="width: 96%">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Exits" Value="Exits"></asp:ListItem>
                                        <asp:ListItem Text="Not Exits" Value="Not_Exits"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        
                        <div class="row" style="margin-bottom: 10px;">
                            <div class="col-lg-12">
                                <div class="col-md-1">
                                    <label class="control-label">Select Live :</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="DdlSUActivity" runat="server" CssClass="form-control" Style="width: 96%">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                            <div class="col-lg-12">
                                <div class="col-md-6">
                                    <asp:Button ID="BtnPrint" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" TabIndex="12" />
                                    <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" TabIndex="13" />
                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" TabIndex="14" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- </div>--%>
</asp:Content>

