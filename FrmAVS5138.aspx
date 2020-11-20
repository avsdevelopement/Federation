<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5138.aspx.cs" Inherits="FrmAVS5138" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
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

        function PreviousDate(obj) {
            debugger;
            var FromDate = document.getElementById('<%=txtEDate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=WorkingDate.ClientID%>').value;
            var TallyDate = document.getElementById('<%=TallyDate.ClientID%>').value;

            var FDate = FromDate.substring(0, 2);
            var FMonth = FromDate.substring(3, 5);
            var FYear = FromDate.substring(6, 10);
            var FromDate = new Date(FYear, FMonth - 1, FDate);

            var TDate = TallyDate.substring(0, 2);
            var TMonth = TallyDate.substring(3, 5);
            var TYear = TallyDate.substring(6, 10);
            var TallyDate1 = new Date(TYear, TMonth - 1, TDate);

            if (FromDate <= TallyDate1) {
                window.alert("Back date not allow less than tally date : " + TallyDate + "...!!\n");
                document.getElementById('<%=txtEDate.ClientID %>').value = WorkingDate;
                document.getElementById('<%=txtEDate.ClientID%>').focus();
                return false;
            }
        }


        function PrevDate(obj) {
            debugger;
            var FromDate = document.getElementById('<%=txtTrfDate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=WorkingDate.ClientID%>').value;
            var TallyDate = document.getElementById('<%=TallyDate.ClientID%>').value;

            var FDate = FromDate.substring(0, 2);
            var FMonth = FromDate.substring(3, 5);
            var FYear = FromDate.substring(6, 10);
            var FromDate = new Date(FYear, FMonth - 1, FDate);

            var TDate = TallyDate.substring(0, 2);
            var TMonth = TallyDate.substring(3, 5);
            var TYear = TallyDate.substring(6, 10);
            var TallyDate1 = new Date(TYear, TMonth - 1, TDate);

            if (FromDate <= TallyDate1) {
                window.alert("Back date not allow less than tally date : " + TallyDate + "...!!\n");
                document.getElementById('<%=txtTrfDate.ClientID %>').value = WorkingDate;
                document.getElementById('<%=txtTrfDate.ClientID%>').focus();
                return false;
            }
        }

        
        
    </script>

    <script type="text/javascript">
        function ValidSub(obj) {
            debugger;
            if (document.getElementById('<%=txtTrfDate.ClientID%>').value == "") {
                document.getElementById('<%=lblError1.ClientID%>').value == "Enter transfer date.";
                document.getElementById('<%=lblError1.ClientID%>').style.color = "Red";
                return false;
            }
            else {
                return true;
            }
            
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        AVS_AllFields
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-2" style="width: 120px">Entry Date : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtEDate" onblur="PreviousDate()" runat="server" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-1">Set No : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtSetNo" runat="server" placeholder="Set No" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtSetNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <input type="hidden" id="TallyDate" runat="server" value="" />
                                                <input type="hidden" id="WorkingDate" runat="server" value="" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-2">Total Debit :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTotalDebit" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Total Credit :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTotalCredit" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblStatus" Text="" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;"></div>

                                    <div id="divDetailInfo" runat="server" visible="false">
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d">Update Details : </strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">GlCode : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGlCode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" />
                                                </div>
                                                <label class="control-label col-md-2" style="width: 120px">SubGlCode : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPrCode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" />
                                                </div>
                                                <label class="control-label col-md-2" style="width: 120px">AccNo : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAccNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Amount : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" />
                                                </div>
                                                <label class="control-label col-md-2" style="width: 120px">Particulars : <span class="required">*</span></label>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtParti1" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">TrxType : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTrxType" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" />
                                                </div>
                                                <label class="control-label col-md-2" style="width: 120px">Activity : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtActivity" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" />
                                                </div>
                                                <label class="control-label col-md-2" style="width: 120px">PmtMode : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPmtMode" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">InstNo : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtInstNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" MaxLength="06" />
                                                </div>
                                                <label class="control-label col-md-2" style="width: 120px">InstDate : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtInstDate" CssClass="form-control" onkeyup="FormatIt(this)" placeholder="DD/MM/YYYY" runat="server" />
                                                </div>
                                                <%--<label class="control-label col-md-2" style="width: 120px">HeadDesc : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtHeadDesc" CssClass="form-control" runat="server" />
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="btnDisplay" runat="server" CssClass="btn blue" Text="Display" OnClick="btnDisplay_Click" />
                                        <asp:Button ID="btnModify" runat="server" Visible="false" CssClass="btn blue" Text="Modify" OnClick="btnModify_Click" />
                                        <asp:Button ID="btnInsert" runat="server" Visible="false" CssClass="btn blue" Text="Insert" OnClick="btnInsert_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="ClearAll" OnClick="btnClear_Click" />
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

    <div class="row">
        <div class="col-md-12">
            <div class="table-scrollable" style="height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                <asp:Label ID="lblVoucher" runat="server" Text="Voucher Details : " BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdVoucher" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModify" runat="server" CommandName="select" CommandArgument='<%#Eval("ScrollNo")%>' OnClick="lnkModify_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cancel" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkCancel" runat="server" CommandName="select" CommandArgument='<%#Eval("ScrollNo")%>' OnClick="lnkCancel_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transfer" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTransfer" runat="server" CommandName="select" CommandArgument='<%#Eval("ScrollNo")%>' OnClick="lnkTransfer_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GlCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlCode" runat="server" Text='<%# Eval("GlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SubGlCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AccNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("Particulars") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actiivity" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActiivity" runat="server" Text='<%# Eval("Activity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PmtMode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPmtMode" runat="server" Text='<%# Eval("PmtMode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ScrollNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScrollNo" runat="server" Text='<%# Eval("ScrollNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TrxType" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrxType" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Maker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Text='<%# Eval("Maker") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCheker" runat="server" Text='<%# Eval("Cheker") %>'></asp:Label>
                                            </ItemTemplate>
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

    <div class="row">
        <div class="col-md-12">
            <div class="table-scrollable" style="height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                <asp:Label ID="Label1" runat="server" Text="Need to Authorize : " BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdAuthorize" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server" CommandName="select" CommandArgument='<%#Eval("ScrollNo")%>' OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GlCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlCode" runat="server" Text='<%# Eval("GlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SubGlCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AccNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("Particulars") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actiivity" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActiivity" runat="server" Text='<%# Eval("Activity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PmtMode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPmtMode" runat="server" Text='<%# Eval("PmtMode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ScrollNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScrollNo" runat="server" Text='<%# Eval("ScrollNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TrxType" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrxType" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Maker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Text='<%# Eval("Maker") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCheker" runat="server" Text='<%# Eval("Cheker") %>'></asp:Label>
                                            </ItemTemplate>
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

    <div id="DivVoucher" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 70%">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-header-title">Voucher Details</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet-body">

                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">EntryDate : </label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtEDate1" Enabled="false" CssClass="form-control" runat="server" />
                                        </div>
                                        <label class="control-label col-md-2">SetNo : </label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtSetNo1" Enabled="false" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Total Debit : </label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtTotalDr" Enabled="false" CssClass="form-control" runat="server" />
                                        </div>
                                        <label class="control-label col-md-2">Total Credit : </label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtTotalCr" Enabled="false" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div id="divReason" runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Reason : </label>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlReason" CssClass="form-control" runat="server" />
                                        </div>
                                        <asp:label id="lblError1" runat="server" class="control-label col-md-6"> </asp:label>
                                    </div>
                                </div>

                                <div id="divTransfer" runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Transfer Date : </label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtTrfDate" onblur="PrevDate()" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" CssClass="form-control" runat="server" />
                                        </div>
                                        <asp:label id="lblError2" runat="server" class="control-label col-md-6"> </asp:label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnTransfer" Visible="false" Text="Transfer" runat="server" CssClass="btn blue" OnClick="btnTransfer_Click" OnClientClick="ValidSub();" />
                                        <asp:Button ID="btnAuthorize" Visible="false" Text="Authorize" runat="server" CssClass="btn blue" OnClick="btnAuthorize_Click" />
                                        <asp:Button ID="btnCancel" Visible="false" Text="Cancel" runat="server" CssClass="btn blue" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnClose2" Text="Close" runat="server" CssClass="btn blue" data-dismiss="modal" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-scrollable" style="height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                                <table class="table table-striped table-bordered table-hover" width="100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="grdTransaction" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="GlCode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGlCode" runat="server" Text='<%# Eval("GlCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SubGlCode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AccNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("Particulars") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Actiivity" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActiivity" runat="server" Text='<%# Eval("Activity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PmtMode" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPmtMode" runat="server" Text='<%# Eval("PmtMode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="TrxType" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTrxType" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label>
                                                            </ItemTemplate>
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
            </div>
        </div>
    </div>
</asp:Content>

