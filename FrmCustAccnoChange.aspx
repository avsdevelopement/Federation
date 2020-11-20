<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCustAccnoChange.aspx.cs" MasterPageFile="~/CBSMaster.master" Inherits="FrmCustAccnoChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function Validate() {
            var TxtAccT = document.getElementById('<%=TxtAccType.ClientID%>').value;
            var TxtAccno = document.getElementById('<%=TxtAccno.ClientID%>').value;

            if (TxtAccT == "") {
                alert("Please Select The Account Type......!!");
                return false;
            }
            if (TxtAccno == "") {
                alert("Please Enter Account Number.......!!");
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Cust No Change Options
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>
                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Calculation Type : <span class="required">* </span></label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="RdbSingle" runat="server" Text="Acc No Change" GroupName="SM" AutoPostBack="true" OnCheckedChanged="RdbSingle_CheckedChanged" />
                                                        <asp:RadioButton ID="RdbMultiple" runat="server" Text="Cust No Change" GroupName="SM" AutoPostBack="true" OnCheckedChanged="RdbMultiple_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                            <div id="Multiple" runat="server" visible="false">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Cust No<span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtCustNo" OnTextChanged="txtCustNo_TextChanged" CssClass="form-control" runat="server" placeholder="Cust No" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtcustname" CssClass="form-control" runat="server" placeholder="Cust Name" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Product Code<span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtAccType" CssClass="form-control" runat="server" placeholder="Account Type" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccType_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtATName" CssClass="form-control" runat="server" placeholder="Account Type Name" OnTextChanged="TxtATName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <div id="CustList" style="height: 200px; overflow-y: scroll;" runat="server"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtATName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="CustList">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Account Number <span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtAccno" CssClass="form-control" runat="server" placeholder="Acc No" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtAccHName" CssClass="form-control" runat="server" placeholder="Account Name" OnTextChanged="TxtAccHName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;" runat="server"></div>
                                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccHName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                        <label class="control-label col-md-2">Opening Date <span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtOPDT" CssClass="form-control" runat="server" placeholder="Opening Date" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Cust No<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtnewCustNo" OnTextChanged="txtnewCustNo_TextChanged" CssClass="form-control" runat="server" placeholder=" New Cust No" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtnewcustname" CssClass="form-control" runat="server" placeholder=" New Cust Name" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                                    </div>
                                                </div>



                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 46px">
                                                    <div class="col-md-offset-3 col-md-9">

                                                        <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="Submit_Click" OnClientClick="Javascript:return Validate();" />
                                                        <asp:Button ID="Btn_Clear" runat="server" CssClass="btn btn-primary" Text="Clear All" OnClick="Btn_Clear_Click" />
                                                        <asp:Button ID="Btn_Exit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="Btn_Exit_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                    <div id="Single" runat="server" visible="false">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Code<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtApcode" CssClass="form-control" runat="server" placeholder="Account Type" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccType_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtpname" CssClass="form-control" runat="server" placeholder="Account Type Name" OnTextChanged="txtpname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <div id="Div2" style="height: 200px; overflow-y: scroll;" runat="server"></div>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtpname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="CustList">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Account Number <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAaccno" CssClass="form-control" runat="server" placeholder="Acc No" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAaccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtAaccname" CssClass="form-control" runat="server" placeholder="Account Name" OnTextChanged="txtAaccname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <div id="Div3" style="height: 200px; overflow-y: scroll;" runat="server"></div>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtAaccname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-2">Opening Date <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtopndate" CssClass="form-control" runat="server" placeholder="Opening Date" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">New Acc No<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtnewAccno" OnTextChanged="txtnewAccno_TextChanged" CssClass="form-control" runat="server" placeholder=" New Cust No" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtNEwAccname" CssClass="form-control" runat="server" placeholder=" New Cust Name" AutoPostBack="true" Enabled="true"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 46px">
                                            <div class="col-md-offset-3 col-md-9">

                                                <asp:Button ID="btnaccupdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnaccupdate_Click" OnClientClick="Javascript:return Validate();" />
                                                <asp:Button ID="accclear" runat="server" CssClass="btn btn-primary" Text="Clear All" OnClick="accclear_Click" />
                                                <asp:Button ID="btnaexit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="btnaexit_Click" />
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
    
    <div class="row" id="Div_CLEARBAL" runat="server">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <asp:Label ID="Label1" runat="server" Text="Cust No Details" Style="font-family: Verdana; font-size: medium;" Font-Bold="true"></asp:Label>
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GRDCust" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="GRDCust_PageIndexChanging"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SubglCode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SubglCode" runat="server" Text='<%# Eval("subglcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust No">
                                            <ItemTemplate>
                                                <asp:Label ID="PARTI" runat="server" Text='<%# Eval("custno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACC NO">
                                            <ItemTemplate>
                                                <asp:Label ID="PARTI1" runat="server" Text='<%# Eval("accno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="OpeningDate">
                                            <ItemTemplate>
                                                <asp:Label ID="INSTNO" runat="server" Text='<%# Eval("openingdate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acc_status">
                                            <ItemTemplate>
                                                <asp:Label ID="SETNO" runat="server" Text='<%# Eval("Acc_status") %>'></asp:Label>
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
            <table class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>
                            <asp:GridView ID="GRDACC" runat="server" AllowPaging="True"
                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                EditRowStyle-BackColor="#FFFF99"
                                OnPageIndexChanging="GRDACC_PageIndexChanging"
                                PageIndex="10" PageSize="25"
                                PagerStyle-CssClass="pgr" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="Subglcode" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="Year" runat="server" Text='<%# Eval("subglcode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust No" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="Month" runat="server" Text='<%# Eval("custno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acc No">
                                        <ItemTemplate>
                                            <asp:Label ID="GlName" runat="server" Text='<%# Eval("accno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Opening Date">
                                        <ItemTemplate>
                                            <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("openingdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acc_status">
                                        <ItemTemplate>
                                            <asp:Label ID="OpeningBal" runat="server" Text='<%# Eval("Acc_status") %>'></asp:Label>
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
