<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmFDPrint_01.aspx.cs" Inherits="FrmFDPrint_01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function Validate() {
            var TxtAccT = document.getElementById('<%=TxtAccType.ClientID%>').value;
            var TxtAccno = document.getElementById('<%=TxtAccno.ClientID%>').value;
            var BRCD = document.getElementById('<%=TxtBRCD.ClientID%>').value;


            if (BRCD == "") {
                alert("Please enter Branch Code......!!");
                return false;
            }

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        FD Print
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>
                                             <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <asp:RadioButtonList ID="Rdb_TypePrint" runat="server" RepeatDirection="Horizontal" Width="400px">
                                                        <asp:ListItem Text="Original Copy" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Admin Copy (User Admin Only)" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Branch Code<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBRCD" OnTextChanged="TxtBRCD_TextChanged"  CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
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
                                                        <asp:TextBox ID="TxtAccType" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccType_TextChanged"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtATName" CssClass="form-control" runat="server" OnTextChanged="TxtATName_TextChanged"  AutoPostBack="true"></asp:TextBox>
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

                                                    <div class="col-md-3">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Account Number <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtAccno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccno_TextChanged"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtAccHName" CssClass="form-control" runat="server" OnTextChanged="TxtAccHName_TextChanged"  AutoPostBack="true"></asp:TextBox>
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
                                                    <div class="col-md-3">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Btn_Clear" runat="server" CssClass="btn btn-primary" Text="Clear All" OnClick="Btn_Clear_Click"  />
                                                    <asp:Button ID="Btn_View" runat="server" CssClass="btn btn-primary" Text="View Details" OnClick="Btn_View_Click"  />
                                                    <asp:Button ID="btnFrntPrint" runat="server" CssClass="btn btn-primary" Text="Front Print" OnClick="btnFrntPrint_Click"  OnClientClick="Javascript:return Validate();" />
                                                    <asp:Button ID="btnBacktPrint" runat="server" CssClass="btn btn-primary" Text="Back Print" OnClick="btnBacktPrint_Click"  OnClientClick="Javascript:return Validate();" />
                                                     <asp:Button ID="TextReport" runat="server" CssClass="btn btn-primary" Text="Duplicate Print" OnClick="TextReport_Click"  />
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
        <%--M.CUSTNAME,
        Ac.SUBGLCODE,
        G.GLNAME ,
        AC.Accno,
        DE.Accno,DE.RATEOFINT,
        DE.OPENINGDATE,DE.DUEDATE,DE.PERIOD,
        DE.INTAMT,DE.MATURITYAMT,AC.BRCD,
        DE.PRDTYPE,
        DE.RECEIPT_NO ,
        Ba.MIDNAME--%>
        <div class="row">
            <div class="col-md-12">
                <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="Gd_LBL" runat="server" Text="Preview of Deposit Details to Print"></asp:Label>
                                    <asp:GridView ID="Grid_ViewFD" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99" 
                                    PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Cust Name" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="CustName" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prd Type" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="PRDTYPE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Prd Name" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="GLNAME" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="A/C No" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("Accno") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate of Int" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="ROI" runat="server" Text='<%# Eval("RATEOFINT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening Date" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="ODate" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Due Date" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="DueDate" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="For Period" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="PERIOD" runat="server" Text='<%# Eval("PERIOD") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Principal Amt" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="PRNAMT" runat="server" Text='<%# Eval("PRNAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Intr Amount" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="IntAMT" runat="server" Text='<%# Eval("INTAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Maturity Amount" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="MATAMT" runat="server" Text='<%# Eval("MATURITYAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Br Code" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch Name" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="BRNAME" runat="server" Text='<%# Eval("MIDNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receipt No." Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="ReceiptNo" runat="server" Text='<%# Eval("RECEIPT_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                        <SelectedRowStyle BackColor="#66FF99" />
                                        <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    </asp:GridView>
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
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

