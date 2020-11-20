<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInstruIssue.aspx.cs" Inherits="FrmInstruIssue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2)
                obj.value = obj.value + "/";
            if (obj.value.length == 5)
                obj.value = obj.value + "/";
            if (obj.value.length == 11)
                alert("Date invalid....");
        }
    </script>
    <script>
        function checkQuote() {
            if (event.keyCode == 39) {
                event.keyCode = 0;
                return false;
            }
        }
    </script>
    <script>
        function Validate() {
            var Brcd = document.getElementById('<%=TxtBrcd.ClientID%>').value;
            var Bsize = document.getElementById('<%=ddlBookSize.ClientID%>').value;
            var NoOfBook = document.getElementById('<%=TxtNoOfBooks.ClientID%>').value;
            var Sno = document.getElementById('<%=TxtStartInstrNo.ClientID%>').value;
            var Eno = document.getElementById('<%=TxtEndInstrNo.ClientID%>').value;
            var InsType = document.getElementById('<%=DdlInstrType.ClientID%>').value;
            var Alpha = document.getElementById('<%=TxtAplhaCd.ClientID%>').value;
            var Subgl = document.getElementById('<%=TxtProcode.ClientID%>').value;
            var Accno = document.getElementById('<%=TxtAccno.ClientID%>').value;
            var IDate = document.getElementById('<%=TxtIssueDt.ClientID%>').value;


            if (Brcd == "") {
                alert("Invalid Brcd Number....");
                return false;
            }
            else if (Bsize == "") {
                alert("Invalid Book size....");
                return false;
            }
            else if (NoOfBook == "") {
                alert("Invalid No. of books....");
                return false;
            }
            else if (Sno == "") {
                alert("Invalid Start Instrument Number....");
                return false;
            }
            else if (Eno == "") {
                alert("Invalid End Instrument Number....");
                return false;
            }
            else if (IDate == "") {
                alert("Invalid Issue Date....");
                return false;
            }
            else if (InsType == "0") {
                alert("Invalid Instrument Type....");
                return false;
            }
            else if (Alpha == "") {
                alert("Invalid Alpha Code....");
                return false;
            }
            else if (Subgl == "") {
                alert("Invalid Product Code....");
                return false;
            }
            else if (Accno == "") {
                alert("Invalid Account number....");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Instrument Issue
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                        <ul class="nav nav-pills">
                                            <li>
                                                <asp:LinkButton ID="LnkAdd" runat="server" Text="Add" class="btn-circle-top bg-blue-madison" OnClick="LnkAdd_Click"><i class="fa fa-plus-circle"></i>Add</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="LnkAutho" runat="server" Text="Authorize" class="btn-circle-top bg-blue-madison" OnClick="LnkAutho_Click"><i class="fa fa-pencil-square-o"></i>Authorize</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="LnkDelete" runat="server" Text="Delete" class="btn-circle-top bg-blue-madison" OnClick="LnkDelete_Click"><i class="fa fa-pencil-square-o"></i>Delete</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="LnkPrint" runat="server" Text="MICR Print" class="btn-circle-top bg-blue-madison" OnClick="LnkPrint_Click"><i class="fa fa-pencil-square-o"></i>MICR Print</asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Br Code : <span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtBrcd" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtBrName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div id="Div_1" runat="server">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1" style="width: 110px">Instr Type : <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="DdlInstrType" runat="server" CssClass="form-control" OnSelectedIndexChanged="DdlInstrType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-1">Alpha Cd : </label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtAplhaCd" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1" style="width: 110px">No.Of Books :<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtNoOfBooks" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtNoOfBooks_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Book Size :<span class="required">*</span></label>
                                                    <div class="col-lg-1">
                                                        <asp:DropDownList ID="ddlBookSize" CssClass="form-control" OnSelectedIndexChanged="ddlBookSize_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            <asp:ListItem Text="--Select--" Value="0" />
                                                            <asp:ListItem Text="03" Value="3" />
                                                            <asp:ListItem Text="15" Value="15" />

                                                             <asp:ListItem Text="25" Value="25" />


                                                            <asp:ListItem Text="25" Value="25" />

                                                            <asp:ListItem Text="30" Value="30" />
                                                            <asp:ListItem Text="45" Value="45" />
                                                            <asp:ListItem Text="60" Value="60" />
                                                            <asp:ListItem Text="90" Value="90" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-1">Start Num :</label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtStartInstrNo" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtStartInstrNo_TextChanged" AutoPostBack="true" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">End Num :</label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtEndInstrNo" onkeypress="javascript:return isNumber (event)" MaxLength="6" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1" style="width: 110px">Special Sr :</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtSpecialSr" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1" style="width: 110px">Issue Date :<span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtIssueDt" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" runat="server" CssClass="form-control" OnTextChanged="TxtIssueDt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="Div_PA" runat="server">
                                            <div class="row" style="margin: 10px; border-bottom: 2px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                            <div></div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1" style="width: 110px">Prod Code : <span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtProcode" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtProcode_TextChanged" Placeholder="Prod Code" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtProName" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProName_TextChanged" Placeholder="Search product name wise"></asp:TextBox>
                                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
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
                                                    <label class="control-label col-md-1" style="width: 110px">Acc No : <span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtAccno" onkeypress="javascript:return isNumber (event)" Placeholder="Account No" OnTextChanged="TxtAccno_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtAccName" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged" CssClass="form-control" runat="server" Placeholder="Search account name wise"></asp:TextBox>
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
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
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1" style="width: 110px">Remark : </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtRemark" runat="server" onkeypress="return checkQuote();" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div id="divCharges" runat="server">
                                            <div class="row" style="margin: 10px; border-bottom: 2px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1" style="width: 85px">Free Leave</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtFreeLeave" runat="server" CssClass="form-control" placeholder="Charges Amount" OnTextChanged="txtFreeLeave_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <label class="control-label col-md-1">Used Leave</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtUsedLeave" Enabled="false" runat="server" CssClass="form-control" placeholder="CGST Charge" />
                                                    </div>
                                                    <label class="control-label col-md-1">Charge</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtPerCharge" Enabled="false" runat="server" CssClass="form-control" placeholder="SGST Charge" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1" style="width: 85px">Charges</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCharges" Enable="false" Text="0" runat="server" CssClass="form-control" placeholder="Charges Amount" OnTextChanged="txtCharges_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <label class="control-label col-md-1">CGST</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCGSTChrg" Enabled="false" Text="0" runat="server" CssClass="form-control" placeholder="CGST Charge" />
                                                    </div>
                                                    <label class="control-label col-md-1">SGST</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtSGSTChrg" Enabled="false" Text="0" runat="server" CssClass="form-control" placeholder="SGST Charge" />
                                                    </div>
                                                    <label class="control-label col-md-1">Total Chrg</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTotalChrg" Enabled="false" Text="0" runat="server" CssClass="form-control" placeholder="Total charges" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div id="Div_View" runat="server">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">From Issue Dt</label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtFDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" runat="server" CssClass="form-control" OnTextChanged="TxtFDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">To Issue Dt</label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtTDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" runat="server" CssClass="form-control" OnTextChanged="TxtTDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Chequedetails" runat="server" visible="false">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <label class="control-label col-md-1" style="width: 110px">Prod Code : <span class="required">*</span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtPrdC" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtPrdC_TextChanged" Placeholder="Prod Code" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="TxtPNameC" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtPNameC_TextChanged" Placeholder="Search product name wise"></asp:TextBox>
                                                    <div id="CustListC" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autopnamec" runat="server" TargetControlID="TxtPNameC"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="CustListC">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <label class="control-label col-md-1" style="width: 110px">Acc No : <span class="required">*</span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtAcC" onkeypress="javascript:return isNumber (event)" Placeholder="Account No" OnTextChanged="TxtAcC_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="TxtAcNameC" AutoPostBack="true" OnTextChanged="TxtAcNameC_TextChanged" CssClass="form-control" runat="server" Placeholder="Search account name wise"></asp:TextBox>
                                                    <div id="CustAcC" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccC" runat="server" TargetControlID="TxtAcNameC"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="CustAcC">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <asp:CheckBox ID="CHK_Cover_STD" runat="server" Text="Cover_Page" Style="width: 100px;" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="BtnSubC" OnClientClick="Javascript:return Validate();" runat="server" CssClass="btn blue" Text="Submit" OnClick="BtnSubC_Click" />
                                                    <%--<asp:Button ID="BtnCover" OnClientClick="Javascript:return Validate();" runat="server" CssClass="btn blue" Text="Cover Print" OnClick="BtnCover_Click" />--%>
                                                    <asp:Button ID="BtnExitC" runat="server" CssClass="btn blue" Text="Exit" OnClick="BtnExitC_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div id="divbutton" runat="server">
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnSubmit" OnClientClick="Javascript:return Validate();" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="btnExit_Click" />
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
        <div class="col-md-12">
            <div class="table-scrollable" style="height: auto; max-height: 400px; overflow-x: auto; overflow-y: auto">
                <asp:Label ID="lblPrint" runat="server" Text="Need to Authorize" BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GridShow" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderStyle-Width="30px" HeaderText="SrNo" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Create Date" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("IssueDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderText="SubGlCode" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="GlName" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Acc No" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="NoOfBooks" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoOfBook" runat="server" Text='<%# Eval("NoOfBook") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="BookSize" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBookSize" runat="server" Text='<%# Eval("BookSize") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Inst No" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInstrumentNo" runat="server" Text='<%# Eval("InstrumentNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Maker" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Text='<%# Eval("Maker") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Authorize" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("Mid")%>' CommandName="select" class="glyphicon glyphicon-check" OnClick="lnkAuthorize_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Delete" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Mid")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
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

    <div id="GridCheque" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="table-scrollable" style="height: auto; max-height: 400px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover" width="100%">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="GrdChequeP" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                        AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>

                                            <asp:TemplateField HeaderStyle-Width="30px" HeaderText="SrNo" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCSrno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Create Date" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCEdate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Amount" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCAmt" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="80px" HeaderText="SetNo" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCSetno" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Cheque No" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCChq" runat="server" Text='<%# Eval("InstrumentNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Cheque Date" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCDate" runat="server" Text='<%# Eval("InstrumentDate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="NoOfBooks" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCNoOfBook" runat="server" Text='<%# Eval("NoOfBook") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Insttype" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LnlCInsttype" runat="server" Text='<%# Eval("Insttype") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="80px" HeaderText="BookSize" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCBookSize" runat="server" Text='<%# Eval("BookSize") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Brcd" Visible="false" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCBrcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Id" Visible="false" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Subglcode" Visible="false" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCSubgl" runat="server" Text='<%# Eval("subglcode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Accno" Visible="false" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblCAcc" runat="server" Text='<%# Eval("accno") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="ChequePrint" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkCheqPrint" runat="server"  CommandArgument='<%#Eval("Brcd")+","+ Eval("subglcode")+","+Eval("accno")%>' CommandName="select" class="glyphicon glyphicon-check" OnClick="LnkCheqPrint_Click"></asp:LinkButton>
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
    </div>

</asp:Content>

