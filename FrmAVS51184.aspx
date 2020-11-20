<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51184.aspx.cs" Inherits="FrmAVS51184" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
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

        function CheckFirstChar(key, txt) {
            if (key == 32 && txt.value.length <= 0) {
                return false;
            }
            else if (txt.value.length > 0) {
                if (txt.value.charCodeAt(0) == 32) {
                    txt.value = txt.value.substring(1, txt.value.length);
                    return true;
                }
            }
            return true;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Recovery Demand Changes
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <br />
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Customer No<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCustno" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" placeholder="Customer No" runat="server" OnTextChanged="txtCustno_TextChanged" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCustName" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" OnTextChanged="txtCustName_TextChanged" onkeypress="javascript:return OnltAlphabets(event)" placeholder="Customer Name" runat="server" Style="text-transform: uppercase;" AutoPostBack="true"></asp:TextBox>
                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autocustname" runat="server" TargetControlID="txtCustName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetCustNames" CompletionListElementID="CustList3">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label">Member No<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtMemberNo" CssClass="form-control" placeholder="Member No" onkeydown="return CheckFirstChar(event.keyCode, this);" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Recovery Changes Type<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlRecovery" TabIndex="6" runat="server" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>

                                                <label class="control-label col-md-3" style="width: 280px">Date <span class="required">*</span></label>
                                                <div class="col-md-2" style="margin-left: 25px">
                                                    <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" Enabled="false" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:CalendarExtender>
                                                </div>

                                            </div>
                                        </div>



                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Product Code<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtProductCd" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" OnTextChanged="txtProductCd_TextChanged" placeholder="Pr Code" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>

                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtACCno" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="AccNo" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>

                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtProductName" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" OnTextChanged="txtProductName_TextChanged" CssClass="form-control" placeholder="Product Name" runat="server" Style="text-transform: uppercase;" AutoPostBack="true"></asp:TextBox>
                                                    <div id="Custlist2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtProductName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetGlName" CompletionListElementID="Custlist2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label">Month & Year<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtMonth" CssClass="form-control" MaxLength="2" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="MM" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtYear" CssClass="form-control" MaxLength="4" placeholder="YYYY" runat="server" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Previous Installment<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPreviousInst" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Previous Installment" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-2">
                                                    <label class="control-label">Current Installment<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCurrentInst" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Current Installment" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Reason<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtReason" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" runat="server"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                    </div>



                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Create" OnClientClick="Javascript:return Validate();" OnClick="Submit_Click" />
                                            <asp:Button ID="BtnAuthorise" runat="server" CssClass="btn btn-primary" Text="Authorise" OnClientClick="Javascript:return Validate();" OnClick="BtnAuthorise_Click" />

                                            <asp:Button ID="btnUnauthorise" runat="server" CssClass="btn btn-primary" Text="Unauthorise" OnClientClick="Javascript:return Validate();" OnClick="btnUnauthorise_Click" />


                                            <asp:Button ID="Btn_Exit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="Btn_Exit_Click" />
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
                                <asp:GridView ID="GrdAcc" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" OnPageIndexChanged="GrdAcc_PageIndexChanged"
                                    PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" OnPageIndexChanging="GrdAcc_PageIndexChanging"
                                    ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                       <asp:TemplateField HeaderText="SUBGLCODE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACCNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CUSTNAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNO ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="MakerName" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MakerName" runat="server" Text='<%# Eval("MakerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="PreviousInstm" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PreviousInstm" runat="server" Text='<%# Eval("OLDSUBS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CurrentInstm" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CurrentInstm" runat="server" Text='<%# Eval("NEWSUBS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GLCODE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="GLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BRCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="EFFECTDATE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="EFFECTDATE" runat="server" Text='<%# Eval("EFFECTDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="LMSTATUS" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LMSTATUS" runat="server" Text='<%# Eval("LMSTATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="YEAR" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="YEAR" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="MONTH" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MONTH" runat="server" Text='<%# Eval("MONTH") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="REASON" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="REASON" runat="server" Text='<%# Eval("REASON") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("SUBGL")+","+Eval("ACCNO")+","+Eval("CUSTNO")+","+Eval("BRCD")%>' CommandName="select" OnClick="LnkAutorise_Click" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UnAuthorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUnAuthorise" runat="server" CommandArgument='<%#Eval("SUBGL")+","+Eval("ACCNO")+","+Eval("CUSTNO")+","+Eval("BRCD")%>' CommandName="select" OnClick="lnkUnAuthorise_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
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
