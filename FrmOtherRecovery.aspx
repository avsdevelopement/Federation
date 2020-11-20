<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmOtherRecovery.aspx.cs" Inherits="FrmOtherRecovery" %>

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
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Other Account Recovery
                        <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Branch Code<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtBRCD" OnTextChanged="TxtBRCD_TextChanged" CssClass="form-control" runat="server" placeholder="Branch Code." onkeypress="javascript:return isNumber(event)" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtBrname" CssClass="form-control" runat="server" Style="text-transform: uppercase;" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Product Code<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtAccType" CssClass="form-control" runat="server" placeholder="Product Code." onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccType_TextChanged" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlProductName" CssClass="form-control" OnSelectedIndexChanged="ddlProductName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                 </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0" id="div_custno" runat="server">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Customer No: <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtcusno" CssClass="form-control" runat="server" placeholder="Cust No." onkeypress="javascript:return isNumber (event)" OnTextChanged="txtcusno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtcusname" placeholder="Name" CssClass="form-control" runat="server" OnTextChanged="txtcusname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtcusname"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetCustNames"
                                                    CompletionListElementID="CustList">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Account Number <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtAccno" CssClass="form-control" runat="server" placeholder="Account Number." onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccno_TextChanged" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">OpeningDate Date <span class="required"></span></label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                </asp:CalendarExtender>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label col-md-12">Start/Stop <span class="required"></span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="DDLStatus" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Start" Value="1" />
                                                    <asp:ListItem Text="Stop" Value="99" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Princiapl Amount <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtPrincipal" CssClass="form-control" runat="server" placeholder="Princiapl Amount."></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Interest Rate <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtIntRate" CssClass="form-control" runat="server" placeholder="Interest Rate."></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Period <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" AutoPostBack="true" OnTextChanged="TxtPeriod_TextChanged" Style="width: 77px;"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Intrest Amount <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtIntAmt" CssClass="form-control" runat="server" placeholder="Interest Amount."></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Maturity Amount <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtMaturityAmt" CssClass="form-control" runat="server" placeholder="Maturity Amount."></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Maturity Date <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                <div class="row">
                                    <div class="col-lg-6" style="text-align: center">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Javascript:return Validate();" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="btnExit_Click" OnClientClick="Javascript:return Validate();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" runat="server" id="Div_grid">
        <div class="col-lg-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdMaster" runat="server" AllowPaging="True" CssClass="mGrid"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="grdMaster_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Cust No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Code" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Depositglcode" runat="server" Text='<%# Eval("Depositglcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acc No">
                                            <ItemTemplate>
                                                <asp:Label ID="CustAccno" runat="server" Text='<%# Eval("CustAccno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="prnamt" runat="server" Text='<%# Eval("prnamt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="rateofint" runat="server" Text='<%# Eval("rateofint") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="period">
                                            <ItemTemplate>
                                                <asp:Label ID="period" runat="server" Text='<%# Eval("period") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="openingdate" runat="server" Text='<%# Eval("openingdate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")%>' OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")%>' OnClick="lnkDelete_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
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
</asp:Content>

