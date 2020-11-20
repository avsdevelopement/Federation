<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAvs_AllField.aspx.cs" Inherits="FrmAvs_AllField" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>
    <style type="text/css">
        .btn {
            text-decoration: none;
            border: 1px solid #000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        AVS_AllFields
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                        <div style="border: 1px solid #3598dc; padding: 10px;">
                                            <asp:Table ID="Table1" runat="server" class="fullwidth">
                                                <asp:TableRow ID="TableRow1" runat="server" Width="50%">
                                                    <asp:TableCell ID="TableCell1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">Brcd</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtbrcd" ReadOnly="true" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" TabIndex="1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">Entry Date</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtdate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server" TabIndex="2"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <label class="control-label col-md-2">SetNo</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtInstNo" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" TabIndex="3"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">PrdCode <span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtpcode" CssClass="form-control" runat="server" onkeypress="return isNumber(event)" OnTextChanged="txtpcode_TextChanged" AutoPostBack="true" TabIndex="4"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">Glcode <span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtGlcode" CssClass="form-control" runat="server" onkeypress="return isNumber(event)" OnTextChanged="TxtGlcode_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                                                </div>
                                                                <%-- <div class="col-lg-4">
                                                                    <asp:TextBox ID="txtpname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtpname_TextChanged" TabIndex="2"></asp:TextBox>
                                                                    <div id="Custlist2" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtpname"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetGlName" CompletionListElementID="Custlist2">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>--%>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">AccountNo<span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtaccno" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true" TabIndex="6"></asp:TextBox>
                                                                </div>
                                                                <%-- <div class="col-lg-4">
                                                                    <asp:TextBox ID="txtAccName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAccName_TextChanged" TabIndex="6"></asp:TextBox>
                                                                    <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtAccName"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetAccName" CompletionListElementID="CustList4">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>--%>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">Customer No</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtcstno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtcstno_TextChanged" AutoPostBack="true" TabIndex="7"></asp:TextBox>
                                                                </div>
                                                                <%--  <div class="col-lg-4">
                                                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtname_TextChanged" TabIndex="4"></asp:TextBox>
                                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autocustname" runat="server" TargetControlID="txtname"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetCustNames" CompletionListElementID="CustList3">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>--%>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">Particular</label>
                                                                <div class="col-md-5">
                                                                    <asp:TextBox ID="txtparticular" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">Amount</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtcredit" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" TabIndex="9"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">TrxType</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtdebit" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" TabIndex="10"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">Activity</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtactivity" runat="server" CssClass="form-control" TabIndex="11"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">PmtMode </label>
                                                                <div class="col-md-3">
                                                                    <asp:DropDownList ID="DdlPmtMode" runat="server" CssClass="form-control" EnableViewState="true" TabIndex="12"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">ScrollNo</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtscroll" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" TabIndex="13"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-md-12">
                                                                <label class="control-label col-md-2">Total Cr : </label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtTotalCr" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">Total Dr : </label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtTotalDr" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </asp:TableCell>
                                                    <asp:TableCell ID="TableCell2" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px" Style="vertical-align: top">
                                                        <div class="col-lg-12">
                                                            <div class="table-scrollable" style="width: 580px; height: 250px; overflow-x: auto; overflow-y: auto">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <asp:GridView ID="Grdcustdisp" runat="server" CellPadding="6" CellSpacing="7"
                                                                                    ForeColor="#333333"
                                                                                    PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                                                                    BorderColor="#333300" Width="100%"
                                                                                    ShowFooter="true">
                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="CustNo" Visible="true" HeaderStyle-BackColor="#99ffcc">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Openingdate" Visible="true" HeaderStyle-BackColor="#99ffcc">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="OPENINGDATE" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="LastIntDate" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="LASTINTDT" runat="server" Text='<%# Eval("LASTINTDT") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="AccStatus" Visible="true" HeaderStyle-BackColor="#ffcc99">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="ACC_STATUS" runat="server" Text='<%# Eval("ACC_STATUS") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Duedate" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="DUEDATE" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Lmstatus" Visible="true" HeaderStyle-BackColor="#ffcc99">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="LMSTATUS" runat="server" Text='<%# Eval("LMSTATUS") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                            </FooterTemplate>
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

                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-12" style="text-align: center">
                                                                <br />
                                                                <asp:Button ID="BtnACS" runat="server" Text="Acc_Status" CssClass="btn btn-primary" OnClick="BtnACS_Click" />
                                                                <asp:Button ID="BtnLMS" runat="server" Text="LM_Status" CssClass="btn btn-primary" OnClick="BtnLMS_Click" />
                                                                <asp:Button ID="BtnLPD" runat="server" Text="LastIntDate" CssClass="btn btn-primary" OnClick="BtnLPD_Click" />
                                                            </div>
                                                        </div>

                                                        <div id="Accdisplay" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[Account Status ] : </strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-md-12">
                                                                    <label class="control-label col-md-3">Acc Status: <span class="required"></span></label>
                                                                    <div class="col-md-4">
                                                                        <asp:RadioButtonList ID="RbdStatus" runat="server" RepeatDirection="Horizontal" >
                                                                            <asp:ListItem Value="1" style="padding: 5px">Open</asp:ListItem>
                                                                            <asp:ListItem Value="2" style="padding: 5px">Close</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Button ID="BtnAccsts" runat="server" Text="ASubmit" CssClass="btn btn-primary" OnClick="BtnAccsts_Click" OnClientClick="Javascript:return IsValide();" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div id="Lmstatus" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[LM Status ] : </strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-md-12">
                                                                    <label class="control-label col-md-3">LM Status: <span class="required"></span></label>
                                                                    <div class="col-md-4">
                                                                        <asp:RadioButtonList ID="RbdLMstatus" runat="server" RepeatDirection="Horizontal" >
                                                                            <asp:ListItem Value="1" style="padding: 5px">Open</asp:ListItem>
                                                                            <asp:ListItem Value="2" style="padding: 5px">Close</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Button ID="BtnLmsts" runat="server" Text="LSubmit" CssClass="btn btn-primary" OnClick="BtnLmsts_Click" OnClientClick="Javascript:return IsValide();" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div id="LastIntdt" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[Last Int Date ] : </strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-md-12">
                                                                    <label class="control-label col-md-3">Last Int Date: <span class="required"></span></label>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="TxtLstIntDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtLstIntDate">
                                                                        </asp:TextBoxWatermarkExtender>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtLstIntDate">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <asp:Button ID="Btnlstintdate" runat="server" Text="Lsubmit" CssClass="btn btn-primary" OnClick="Btnlstintdate_Click" OnClientClick="Javascript:return IsValide();" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>

                                            <div class="row">
                                                <div class="col-lg-6" style="text-align: center">
                                                    <br />
                                                    <asp:Button ID="Submit" runat="server" Text="Display" CssClass="btn btn-primary" OnClick="Submit_Click" OnClientClick="Javascript:return IsValide();" TabIndex="13" />
                                                    <asp:Button ID="BtnModify" runat="server" Text="Modify" CssClass="btn btn-primary" OnClick="BtnModify_Click" OnClientClick="Javascript:return IsValide();" TabIndex="14" />
                                                    <asp:Button ID="Save" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="Save_Click" OnClientClick="Javascript:return IsValide();" TabIndex="15" />
                                                    <asp:Button ID="Clear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="Clear_Click" TabIndex="16" />
                                                    <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" TabIndex="17" />
                                                    <br />
                                                    <br />
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

    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable" style="height: 250px; overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdAvs_Field" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" AllowPaging="True"
                                    EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="10" OnPageIndexChanging="grdAvs_Field_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("SCROLLNO")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BRCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ENTRYDATE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GLCODE">
                                            <ItemTemplate>
                                                    <asp:Label ID="GLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SUBGLCODE">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACCNO">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CUSTNO">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SETNO">
                                            <ItemTemplate>
                                                <asp:Label ID="SETNO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PARTICULARS">
                                            <ItemTemplate>
                                                <asp:Label ID="PARTICULARS" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACTIVITY" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACTIVITY" runat="server" Text='<%# Eval("ACTIVITY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PMTMODE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PMTMODE" runat="server" Text='<%# Eval("PMTMODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SCROLLNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SCROLLNO" runat="server" Text='<%# Eval("SCROLLNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TRXTYPE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="TRXTYPE" runat="server" Text='<%# Eval("TRXTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Status" runat="server" Text='<%# Eval("STAGE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Maker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Maker" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Checker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Checker" runat="server" Text='<%# Eval("CID") %>'></asp:Label>
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


