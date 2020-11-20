<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCommonPatch.aspx.cs" Inherits="FrmCommonPatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isvalidate() {

            var entrydate, protype, accno, nar1, nar2, bal, amt, instno, instdate;
            protype = document.getElementById('<%=TxtAccType.ClientID%>').value;
            accno = document.getElementById('<%=TxtAccno.ClientID%>').value;

            var message = '';

            if (protype == "") {
                message = 'Please Enter Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAccType.ClientID%>').focus();
                return false;
            }

            if (nar1 == "") {
                message = 'Please Enter Naration....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAccno.ClientID%>').focus();
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
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;
        }
    </script>
    <style>
        .zoom {
            transition: transform .3s; /* Animation */
            width: 200px;
            height: 200px;
            margin: 0 auto;
        }

            .zoom:hover {
                transform: scale(1.5); /* (150% zoom - Note: if the zoom is too large, it will go outside of the viewport) */
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Common Patch Update
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="Depositdiv" runat="server">
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Branch Code<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtBRCD" OnTextChanged="TxtBRCD_TextChanged" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
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
                                    <asp:TextBox ID="TxtAccType" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccType_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtATName" CssClass="form-control" runat="server" OnTextChanged="TxtATName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
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
                                    <asp:TextBox ID="TxtAccno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtAccHName" CssClass="form-control" runat="server" OnTextChanged="TxtAccHName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
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
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="DIV1">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                    <label class="control-label">Select Details</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DdlAccActivity" runat="server" Enabled="True" OnSelectedIndexChanged="DdlAccActivity_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0; padding-top: 10px; text-align: center">
                            <div class="col-lg-12">
                                <div class="col-md-6">
                                    <asp:Button ID="BtnPrint" runat="server" Text="Show" CssClass="btn btn-success" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" TabIndex="12" />
                                    <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" TabIndex="13" />
                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" TabIndex="14" />
                                    <asp:Button ID="BtnModify" runat="server" CssClass="btn btn-success" Text="Modify" OnClick="BtnModify_Click" OnClientClick="Javascript:return isvalidate();" />
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="DIV2" style="margin-top: 5px;">
                            <div class="col-lg-12">
                                <div class="col-md-2"></div>
                                <div class="col-md-4">
                                    <label class="control-label ">All Authorize</label>
                                    <asp:CheckBox ID="Chk_AllAutho" runat="server" AutoPostBack="true" OnCheckedChanged="Chk_AllAutho_CheckedChanged" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="DivCalculatedData" runat="server">
            <div class="col-md-12">
                <div style="width: 100%; height: 200px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover fixhead">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="Lbl_GridName" Text="" runat="server" Style="font-size: large; color: darkblue; background-color: aqua;"></asp:Label>
                                    <div class="col-md-12">
                                        <asp:Button ID="BtnUpdateAll" CssClass="btn btn-primary" runat="server" Text="Update All" OnClick="BtnUpdateAll_Click" Visible="false" />
                                    </div>

                                    <asp:GridView ID="GridView1009" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="GridView1009_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderStyle-Width="75px" HeaderText="Select">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="allchk" runat="server" Text="All" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" onclick="checkAll(this)" />
                                                    <%--<asp:TextBox ID="txtCol" runat="server" Height="9px" Width="9px" Enabled="false" ToolTip='<%#Eval("Custno") %>'></asp:TextBox>--%>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo" Style="width: 50px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' ToolTip='<%#Eval("Custno") %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BrCd">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrCd" Text='<%# Eval("BRCD") %>' ToolTip='<%#Eval("BRCD") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct" Text='<%# Eval("DEPOSITGLCODE") %>' ToolTip='<%#Eval("DEPOSITGLCODE") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="A/C No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccno" Text='<%# Eval("CUSTACCNO") %>' ToolTip='<%#Eval("CUSTACCNO") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="CustNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustno" runat="server" Text='<%# Eval("CUSTNO") %>' Style="width: 90px" ToolTip='<%#Eval("CUSTNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="limit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtLimit" runat="server" Text='<%# Eval("PRNAMT") %>' Style="width: 90px" ToolTip='<%#Eval("PRNAMT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="ROI">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtROI" runat="server" Text='<%# Eval("RATEOFINT") %>' Style="width: 90px" ToolTip='<%#Eval("RATEOFINT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Open Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtOpDate" runat="server" Text='<%# Eval("OPENINGDATE") %>' Style="width: 90px" ToolTip='<%#Eval("OPENINGDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Due Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtDueDate" runat="server" Text='<%# Eval("DUEDATE") %>' Style="width: 90px" ToolTip='<%#Eval("DUEDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Period">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPeriod" runat="server" Text='<%# Eval("PERIOD") %>' Style="width: 90px" ToolTip='<%#Eval("PERIOD") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Int Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIntAmt" runat="server" Text='<%# Eval("INTAMT") %>' Style="width: 90px" ToolTip='<%#Eval("INTAMT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Mature Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtMatAmt" runat="server" Text='<%# Eval("MATURITYAMT") %>' Style="width: 90px" ToolTip='<%#Eval("MATURITYAMT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Lien YN">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtLien" runat="server" Text='<%# Eval("LIENMARK") %>' Style="width: 90px" ToolTip='<%#Eval("LIENMARK") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Lien Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtLienAmt" runat="server" Text='<%# Eval("LIENAMOUNT") %>' Style="width: 90px" ToolTip='<%#Eval("LIENAMOUNT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Trf AcType">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtTrfAcType" runat="server" Text='<%# Eval("TRFACCTYPE") %>' Style="width: 90px" ToolTip='<%#Eval("TRFACCTYPE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Trf AcNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtTrfAcc" runat="server" Text='<%# Eval("TRFACCNO") %>' Style="width: 90px" ToolTip='<%#Eval("TRFACCNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtStatus" runat="server" Text='<%# Eval("LMSTATUS") %>' Style="width: 90px" ToolTip='<%#Eval("LMSTATUS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Int Pay">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIntPay" runat="server" Text='<%# Eval("INTPAYOUT") %>' Style="width: 90px" ToolTip='<%#Eval("INTPAYOUT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="LastInt Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtlastIntDT" runat="server" Text='<%# Eval("LASTINTDATE") %>' Style="width: 90px" ToolTip='<%#Eval("LASTINTDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtRemark" runat="server" Text='<%# Eval("REMARK") %>' Style="width: 90px" ToolTip='<%#Eval("REMARK") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="RECSRNO">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtRecSrno" runat="server" Text='<%# Eval("RECSRNO") %>' Style="width: 90px" ToolTip='<%#Eval("RECSRNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="RECEIPT_NO">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtRECEIPT_NO" runat="server" Text='<%# Eval("RECEIPT_NO") %>' Style="width: 90px" ToolTip='<% #Eval("RECEIPT_NO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="PTD">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPTD" runat="server" Text='<%# Eval("PTD") %>' Style="width: 90px" ToolTip='<%#Eval("PTD") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="PTM">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPTM" runat="server" Text='<%# Eval("PTM") %>' Style="width: 90px" ToolTip='<%#Eval("PTM") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="PTY">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPTY" runat="server" Text='<%# Eval("PTY") %>' Style="width: 90px" ToolTip='<%#Eval("PTY") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Stage">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtSTAGE" runat="server" Text='<%# Eval("stage") %>' Style="width: 90px" ToolTip='<%#Eval("stage") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>
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
        <div class="row" id="DivLoan" runat="server">
            <div class="col-md-12">
                <div style="width: 100%; height: 200px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover fixhead">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="Lbl_GridName1" Text="" runat="server" Style="font-size: large; color: darkblue; background-color: aqua;"></asp:Label>
                                    <div class="col-md-12">
                                        <asp:Button ID="UpdateAll_LN" CssClass="btn btn-primary" runat="server" Text="Update All" OnClick="UpdateAll_LN_Click" Visible="false" />
                                    </div>

                                    <asp:GridView ID="GridViewLoan" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="GridViewLoan_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderStyle-Width="75px" HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_1" runat="server" onclick="checkAll(this)" />
                                                    <%--<asp:TextBox ID="txtCol" runat="server" Height="9px" Width="9px" Enabled="false" ToolTip='<%#Eval("Custno") %>'></asp:TextBox>--%>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo_1" Style="width: 50px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' ToolTip='<%#Eval("Custno") %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BrCd">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrCd_1" Text='<%# Eval("BRCD") %>' ToolTip='<%#Eval("BRCD") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct_1" Text='<%# Eval("LOANGLCODE") %>' ToolTip='<%#Eval("LOANGLCODE") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="A/C No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccno_1" Text='<%# Eval("CUSTACCNO") %>' ToolTip='<%#Eval("CUSTACCNO") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="CustNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustno_1" runat="server" Text='<%# Eval("CUSTNO") %>' Style="width: 90px" ToolTip='<%#Eval("CUSTNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="limit">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtLimit_1" runat="server" Text='<%# Eval("LIMIT") %>' Style="width: 90px" ToolTip='<%#Eval("LIMIT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="INSTALLMENT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtInst_1" runat="server" Text='<%# Eval("INSTALLMENT") %>' Style="width: 90px" ToolTip='<%#Eval("INSTALLMENT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="INTRATE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtROI_1" runat="server" Text='<%# Eval("INTRATE") %>' Style="width: 90px" ToolTip='<%#Eval("INTRATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="PENAL">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPenal_1" runat="server" Text='<%# Eval("PENAL") %>' Style="width: 90px" ToolTip='<%#Eval("PENAL") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="OPENINGDATE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtOpDate_1" runat="server" Text='<%# Eval("OPENINGDATE") %>' Style="width: 90px" ToolTip='<%#Eval("OPENINGDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="DUEDATE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtDueDt_1" runat="server" Text='<%# Eval("DUEDATE") %>' Style="width: 90px" ToolTip='<%#Eval("DUEDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="STATUS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtStatus_1" runat="server" Text='<%# Eval("LMSTATUS") %>' Style="width: 90px" ToolTip='<%#Eval("LMSTATUS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="INSTDATE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtInstDT_1" runat="server" Text='<%# Eval("INSTDATE") %>' Style="width: 90px" ToolTip='<%#Eval("INSTDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="PERIOD">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPERIOD_1" runat="server" Text='<%# Eval("PERIOD") %>' Style="width: 90px" ToolTip='<%#Eval("PERIOD") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="BONDNO">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtBONDNO_1" runat="server" Text='<%# Eval("BONDNO") %>' Style="width: 90px" ToolTip='<%#Eval("BONDNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="LASTINTDATE">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtlastIntDt_1" runat="server" Text='<%# Eval("LASTINTDATE") %>' Style="width: 90px" ToolTip='<%#Eval("LASTINTDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="DISYN">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtDISYN_1" runat="server" Text='<%# Eval("DISYN") %>' Style="width: 90px" ToolTip='<%#Eval("DISYN") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="EMI">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtEMI_1" runat="server" Text='<%# Eval("EMI") %>' Style="width: 90px" ToolTip='<%#Eval("EMI") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="RecommAutho">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtRecommAutho_1" runat="server" Text='<%# Eval("RecommAutho") %>' Style="width: 90px" ToolTip='<%#Eval("RecommAutho") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Equated">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtEquated_1" runat="server" Text='<%# Eval("Equated") %>' Style="width: 90px" ToolTip='<%#Eval("Equated") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="IntFund">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIntFund_1" runat="server" Text='<%# Eval("IntFund") %>' Style="width: 90px" ToolTip='<%#Eval("IntFund") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="PLRLink">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPLRLink_1" runat="server" Text='<%# Eval("PLRLink") %>' Style="width: 90px" ToolTip='<%#Eval("PLRLink") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="LoanPurpose">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtLoanPurpose_1" runat="server" Text='<%# Eval("LoanPurpose") %>' Style="width: 90px" ToolTip='<%#Eval("LoanPurpose") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtRemark_1" runat="server" Text='<%# Eval("Remark") %>' Style="width: 90px" ToolTip='<%#Eval("Remark") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Stage">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtStage_1" runat="server" Text='<%# Eval("STAGE") %>' Style="width: 90px" ToolTip='<%#Eval("STAGE") %>'></asp:TextBox>
                                                </ItemTemplate>      
                                            </asp:TemplateField>
                                        </Columns>
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
        <div class="row" id="DivAcc" runat="server">
            <div class="col-md-12">
                <div style="width: 100%; height: 200px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover fixhead">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="Lbl_GridName2" Text="" runat="server" Style="font-size: large; color: darkblue; background-color: aqua;"></asp:Label>
                                    <div class="col-md-12">
                                        <asp:Button ID="BtnUpdateAll_Acc" CssClass="btn btn-primary" runat="server" Text="Update All" OnClick="BtnUpdateAll_Acc_Click" Visible="false" />
                                    </div>

                                    <asp:GridView ID="GridViewAcc" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="GridViewAcc_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderStyle-Width="75px" HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_2" runat="server" onclick="checkAll(this)" />
                                                    <%--<asp:TextBox ID="txtCol" runat="server" Height="9px" Width="9px" Enabled="false" ToolTip='<%#Eval("Custno") %>'></asp:TextBox>--%>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo_2" Style="width: 50px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' ToolTip='<%#Eval("Custno") %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BrCd">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrCd_2" Text='<%# Eval("BRCD") %>' ToolTip='<%#Eval("BRCD") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct_2" Text='<%# Eval("SUBGLCODE") %>' ToolTip='<%#Eval("SUBGLCODE") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="A/C No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccno_2" Text='<%# Eval("ACCNO") %>' ToolTip='<%#Eval("ACCNO") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="GlCode">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtGLCODE_2" runat="server" Text='<%# Eval("GLCODE") %>' Style="width: 90px" ToolTip='<%#Eval("GLCODE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="CustNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustno_2" runat="server" Text='<%# Eval("CUSTNO") %>' Style="width: 90px" ToolTip='<%#Eval("CUSTNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Open Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtOpDate_2" runat="server" Text='<%# Eval("OPENINGDATE") %>' Style="width: 90px" ToolTip='<%#Eval("OPENINGDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Close Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCloseDate_2" runat="server" Text='<%# Eval("CLOSINGDATE") %>' Style="width: 90px" ToolTip='<%#Eval("CLOSINGDATE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Acc Status">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtAcStatus_2" runat="server" Text='<%# Eval("ACC_STATUS") %>' Style="width: 90px" ToolTip='<%#Eval("ACC_STATUS") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Acc Type">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtAcType_2" runat="server" Text='<%# Eval("ACC_TYPE") %>' Style="width: 90px" ToolTip='<%#Eval("ACC_TYPE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Opr Type">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtOprType_2" runat="server" Text='<%# Eval("OPR_TYPE") %>' Style="width: 90px" ToolTip='<%#Eval("OPR_TYPE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="D_Period">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtDPeriod_2" runat="server" Text='<%# Eval("D_PERIOD") %>' Style="width: 90px" ToolTip='<%#Eval("D_PERIOD") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="D_Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtDAmt_2" runat="server" Text='<%# Eval("D_AMOUNT") %>' Style="width: 90px" ToolTip='<%#Eval("D_AMOUNT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="LastIntDT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtLastIntDT_2" runat="server" Text='<%# Eval("LASTINTDT") %>' Style="width: 90px" ToolTip='<%#Eval("LASTINTDT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Ref_Agent">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtRegAgt_2" runat="server" Text='<%# Eval("Ref_Agent") %>' Style="width: 90px" ToolTip='<%#Eval("Ref_Agent") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Ref_CustNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtRefCust_2" runat="server" Text='<%# Eval("Ref_custNo") %>' Style="width: 90px" ToolTip='<%#Eval("Ref_custNo") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Shr_BR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtSHRBr_2" runat="server" Text='<%# Eval("SHR_BR") %>' Style="width: 90px" ToolTip='<%#Eval("SHR_BR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Remark1">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtRemark1_2" runat="server" Text='<%# Eval("Remark1") %>' Style="width: 90px" ToolTip='<%#Eval("Remark1") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
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
        <div class="row" id="DivGlmast" runat="server">
            <div class="col-md-12">
                <div style="width: 100%; height: 200px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover fixhead">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="Lbl_GridName3" Text="" runat="server" Style="font-size: large; color: darkblue; background-color: aqua;"></asp:Label>
                                    <div class="col-md-12">
                                        <asp:Button ID="BtnUpdateAll_GL" CssClass="btn btn-primary" runat="server" Text="Update All" OnClick="BtnUpdateAll_GL_Click" Visible="false" />
                                    </div>

                                    <asp:GridView ID="GridViewGL" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="GridViewGL_RowDataBound">
                                        <Columns>

                                            <asp:TemplateField HeaderStyle-Width="75px" HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_3" runat="server" onclick="checkAll(this)" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo_3" Style="width: 50px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' ToolTip='<%#Eval("SUBGLCODE") %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BrCd">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrCd_3" Text='<%# Eval("BRCD") %>' ToolTip='<%#Eval("BRCD") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProduct_3" Text='<%# Eval("SUBGLCODE") %>' ToolTip='<%#Eval("SUBGLCODE") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="GlCode">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtGLCODE_3" runat="server" Text='<%# Eval("GLCODE") %>' Style="width: 90px" ToolTip='<%#Eval("GLCODE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="GLName">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtGLName_3" runat="server" Text='<%# Eval("GLNAME") %>' Style="width: 200px" ToolTip='<%#Eval("GLNAME") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="GL Grp">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtGLGrp_3" runat="server" Text='<%# Eval("GLGROUP") %>' Style="width: 90px" ToolTip='<%#Eval("GLGROUP") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCategory_3" runat="server" Text='<%# Eval("CATEGORY") %>' Style="width: 90px" ToolTip='<%#Eval("CATEGORY") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="ROI">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtROI_3" runat="server" Text='<%# Eval("ROI") %>' Style="width: 90px" ToolTip='<%#Eval("ROI") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="IntCalType">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIntCalType_3" runat="server" Text='<%# Eval("INTCALTYPE") %>' Style="width: 90px" ToolTip='<%#Eval("INTCALTYPE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Int App">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIntApp_3" runat="server" Text='<%# Eval("Int_App") %>' Style="width: 90px" ToolTip='<%#Eval("Int_App") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Int Pay">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtINTPAY_3" runat="server" Text='<%# Eval("INTPAY") %>' Style="width: 90px" ToolTip='<%#Eval("INTPAY") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="GL Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtGLBal_3" runat="server" Text='<%# Eval("GLBALANCE") %>' Style="width: 90px" ToolTip='<%#Eval("GLBALANCE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="PL Acc">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPLAcc_3" runat="server" Text='<%# Eval("PLACCNO") %>' Style="width: 90px" ToolTip='<%#Eval("PLACCNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="AccYN">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtAccYN_3" runat="server" Text='<%# Eval("ACCNOYN") %>' Style="width: 90px" ToolTip='<%#Eval("ACCNOYN") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Last No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtLastNo_3" runat="server" Text='<%# Eval("LASTNO") %>' Style="width: 90px" ToolTip='<%#Eval("LASTNO") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="IR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIR_3" runat="server" Text='<%# Eval("IR") %>' Style="width: 90px" ToolTip='<%#Eval("IR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="IOR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIOR_3" runat="server" Text='<%# Eval("IOR") %>' Style="width: 90px" ToolTip='<%#Eval("IOR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="IntAccYN">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtIntAccYN_3" runat="server" Text='<%# Eval("INTACCYN") %>' Style="width: 90px" ToolTip='<%#Eval("INTACCYN") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="ShortGlName">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtShortGlName_3" runat="server" Text='<%# Eval("ShortGlName") %>' Style="width: 90px" ToolTip='<%#Eval("ShortGlName") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="PL Group">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPLGrp_3" runat="server" Text='<%# Eval("PLGROUP") %>' Style="width: 90px" ToolTip='<%#Eval("PLGROUP") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Cash_DR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCashDR_3" runat="server" Text='<%# Eval("CASHDR") %>' Style="width: 90px" ToolTip='<%#Eval("CASHDR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Cash_CR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCashCR_3" runat="server" Text='<%# Eval("CASHCR") %>' Style="width: 90px" ToolTip='<%#Eval("CASHCR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="TRF_DR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtTrfDR_3" runat="server" Text='<%# Eval("TRFDR") %>' Style="width: 90px" ToolTip='<%#Eval("TRFDR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="TRF_CR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtTrfCR_3" runat="server" Text='<%# Eval("TRFCR") %>' Style="width: 90px" ToolTip='<%#Eval("TRFCR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="CLG_DR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtClgDR_3" runat="server" Text='<%# Eval("CLGDR") %>' Style="width: 90px" ToolTip='<%#Eval("CLGDR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="CLG_CR">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtClgCR_3" runat="server" Text='<%# Eval("CLGCR") %>' Style="width: 90px" ToolTip='<%#Eval("CLGCR") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="GL Marathi">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtGLMarathi_3" runat="server" Text='<%# Eval("GLMarathi") %>' Style="width: 90px" ToolTip='<%#Eval("GLMarathi") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="ImplimentDT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtImplimentDT_3" runat="server" Text='<%# Eval("ImplimentDate") %>' Style="width: 90px" ToolTip='<%#Eval("ImplimentDate") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="OpenBal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtOpenBal_3" runat="server" Text='<%# Eval("OpeningBal") %>' Style="width: 90px" ToolTip='<%#Eval("OpeningBal") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
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
            <asp:HiddenField ID="hdnCustNo" runat="server" Value="0" />
        </div>
</asp:Content>

