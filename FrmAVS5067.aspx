<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5067.aspx.cs" Inherits="FrmAVS5067" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function IsValide() {
            var ddlprefix = document.getElementById('<%=Txtprodcode.ClientID%>').value;
            var message = '';

            if (txtcstno == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Productcode number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=Txtprodcode.ClientID %>').focus();
                return false;
            }
        }

    </script>

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
    <div class="portlet box green" id="Div1">
        <div class="portlet-title">
            <div class="caption">
                DDS Patch
            </div>
        </div>
        <div class="portlet-body form">
            <div class="form-horizontal">
                <div class="form-wizard">
                    <div class="form-body">
                        <div class="tab-content">
                            <div class="tab-pane active" id="tab1">
                                <asp:Table ID="TblDiv_MainWindow" runat="server" Width="100%">
                                    <asp:TableRow ID="Tbl_R1" runat="server">
                                        <asp:TableCell ID="Tbl_c1" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Branch No</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBrcd" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div2" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Product Code<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtprodcode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtprodcode_TextChanged" AutoPostBack="true" TabIndex="2"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="3" OnTextChanged="txtname_TextChanged"></asp:TextBox>
                                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtname"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="getglname" CompletionListElementID="CustList3">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="DIVACC" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Account No</label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtaccno" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <asp:TextBox ID="TxtAccName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged" TabIndex="5"></asp:TextBox>
                                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetAccName" CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Opening Date</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtopendate" CssClass="form-control" placeholder="DD/MM/YYYY" Enabled="false" onkeyup="FormatIt(this);CheckForFutureDate()" onkeypress="javascript:return isNumber (event)" runat="server" TabIndex="6"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtopendate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtopendate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtOpenDate1" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server" onkeyup="FormatIt(this);CheckForFutureDate()" onkeypress="javascript:return isNumber (event)" TabIndex="7"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtOpenDate1_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtOpenDate1">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Last Int Date</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtLastindate" CssClass="form-control" placeholder="DD/MM/YYYY" Enabled="false" onkeyup="FormatIt(this);CheckForFutureDate()" onkeypress="javascript:return isNumber (event)" runat="server" TabIndex="8"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtLastindate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtLastindate1" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server" onkeyup="FormatIt(this);CheckForFutureDate()" onkeypress="javascript:return isNumber (event)" TabIndex="9"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtLastindate1_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtLastindate1">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Closing Date</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtClosedate" CssClass="form-control" placeholder="DD/MM/YYYY" Enabled="false" onkeyup="FormatIt(this);CheckForFutureDate()" onkeypress="javascript:return isNumber (event)" runat="server" TabIndex="10"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtClosedate_CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtClosedate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtClosedate1" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server" onkeyup="FormatIt(this);CheckForFutureDate()" onkeypress="javascript:return isNumber (event)" TabIndex="11"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtClosedate1_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtClosedate1">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Daily Amount</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtdailyamount" CssClass="form-control" Enabled="false" onkeypress="javascript:return isNumber (event)" runat="server" TabIndex="18"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtdaily1" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Account Type: <span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlAccType" Enabled="false" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2">Account Type New: <span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlAccType1" runat="server" CssClass="form-control" Enabled="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Acc Status: <span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlOpType" Enabled="false" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2">Acc Status New: <span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlOpType1" runat="server" CssClass="form-control" Enabled="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:TableCell>
                                        <asp:TableCell ID="TableCell2" runat="server" Width="30%" BorderStyle="Solid" BorderWidth="1px">
                                                      
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>

                                <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                    <div class="col-lg-12">
                                        <div class="col-md-6">
                                            <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-success " OnClick="Btn_Submit_Click" OnClientClick="return btnSubmit_Click();" />
                                            <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click" />
                                            <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Direct Liabilities : </strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdstandard" runat="server" AllowPaging="false" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99"
                                                    PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbrcd" runat="server" Text='<%# Eval("sysdate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Maker" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblPrdcd" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="OpenDate" ItemStyle-Width="100px">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblopendate" runat="server" Text='<%# Eval("ACCOPENDATE") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="LastIntDate" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLastInt" runat="server" Text='<%# Eval("LASTINTDATE") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CloseDate" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCloseDate" runat="server" Text='<%# Eval("CLOSINGDATE") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Daily Amt" ItemStyle-Width="90px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblDailyAmt" runat="server" Text='<%# Eval("DAILYAMT") %>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acc Status" ItemStyle-Width="80px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaccno" runat="server" Text='<%# Eval("STATUSNAME") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acc Type" ItemStyle-Width="80px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaccno" runat="server" Text='<%# Eval("TYPENAME") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <HeaderStyle BackColor="#ffce9d" />
                                                    <FooterStyle BackColor="#bbffff" />
                                                    <SelectedRowStyle BackColor="#66FF99" />
                                                    <EditRowStyle BackColor="#FFFF99" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
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

</asp:Content>


