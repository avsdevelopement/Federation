<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmVoucherActInfo.aspx.cs" Inherits="FrmVoucherActInfo" %>

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
    </script>
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript">
        function isValid() {
            var EDT = document.getElementById('<%=TxtDate.ClientID%>').value;
            var USR = document.getElementById('<%=TxtUserCode.ClientID%>').value;
            var SETNO = document.getElementById('<%=TxtSetNo.ClientID%>').value;
            var ACT = document.getElementById('<%=DdlActivity.ClientID%>').value;
            var MSG = '';

            if (EDT == "") {
                MSG = "Enter Date...!\n";
                $('#alertModal').find('.modal-body p').text(MSG);
                $('#alertModal').modal('show')
                $('#<%=TxtDate.ClientID %>').focus();
                return false;
            }
            
        }
            
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Voucher Activity Information
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                            </div>
                                            <div style="border: 1px solid #3598dc">

                                                <asp:Table ID="Table1" runat="server">
                                                    <asp:TableRow ID="TRow1" runat="server">
                                                        <asp:TableCell ID="TCell1" runat="server" Style="width: 30%">


                                                            <div class="row" runat="server" id="DIV_DATE">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-3">
                                                                        <label class="control-label">Date</label>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="TxtDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="2"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <br></br>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0" runat="server" id="DIV_ACT">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-3">
                                                                        <label class="control-label">Activity</label>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <asp:DropDownList ID="DdlActivity" runat="server" CssClass="form-control" TabIndex="3">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <br></br>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TCell2" runat="server" Style="width: 30%">

                                                            <div id="DIV_UC" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">User Code</label>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="TxtUserCode" CssClass="form-control" runat="server" TabIndex="4"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                                <br></br>

                                                            </div>
                                                            <div id="DIV_SETNO" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-4">
                                                                        <label class="control-label">Set No</label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="TxtSetNo" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="5"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label">Amount</label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtamount" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="5"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <br></br>

                                                            </div>

                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell1" runat="server" Style="width: 30%">

                                                            <div id="DIV1" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-3">
                                                                        <label class="control-label">Total CR</label>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="TxtCR" runat="server" Enabled="false" CssClass="form-control" BackColor="#00cc66"></asp:TextBox>
                                                                    </div>


                                                                </div>
                                                                <br></br>

                                                            </div>
                                                            <div id="DIV2" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                                <div class="col-lg-12">

                                                                    <div class="col-md-3">
                                                                        <label class="control-label">Total DR</label>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="TxtDR" runat="server" Enabled="false" CssClass="form-control" BackColor="#ff5050"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <br></br>

                                                            </div>

                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-md-offset-3 col-md-12">
                                                        <div class="col-md-11">
                                                            <asp:Button ID="Btn_Submit" runat="server" Text="Submit" OnClientClick="javascript:return isValid();" CssClass="btn btn-primary" TabIndex="5" OnClick="Btn_Submit_Click" />
                                                            <asp:Button ID="Btn_Clear" runat="server" Text="Clear All" CssClass="btn btn-primary" TabIndex="6" OnClick="Btn_Clear_Click" />
                                                            <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" TabIndex="7" OnClick="Btn_Exit_Click" />
                                                            <asp:Button ID="BtnView" runat="server" Text="View Cancel Entry" CssClass="btn btn-primary" TabIndex="8" OnClick="BtnView_Click" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div style="border: 1px solid #3598dc">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12" style="height: 50%">
                                                    <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                                                        <asp:GridView ID="GrdView" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="GrdView_RowDataBound" OnSelectedIndexChanged="GrdView_SelectedIndexChanged">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SETNO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="On Date" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="SUBGLCODE" HeaderText="Product Code" />
                                                                <asp:BoundField DataField="ACCNO" HeaderText="A/C No" />
                                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Name" />
                                                                <asp:BoundField DataField="PARTICULARS" HeaderText="Particulars" />

                                                                <asp:TemplateField HeaderText="Amount" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="AMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Type" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="TYPE" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ACTIVITY" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ACTIVITY" runat="server" Text='<%# Eval("ACTIVITY") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:BoundField DataField="BRCD" HeaderText="Br. Code" />
                                                                <asp:BoundField DataField="STAGE" HeaderText="Status" />
                                                                <asp:BoundField DataField="LOGINCODE" HeaderText="User Code" />
                                                                <asp:TemplateField HeaderText="Maker" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="MID" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Checker" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="CID" runat="server" Text='<%# Eval("VID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cancel by" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="VID" runat="server" Text='<%# Eval("CID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Receipt" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LnkPrintReceipt" runat="server" CommandName="select" CommandArgument='<%# Eval("SUBGLCODE")+","+Eval("ACCNO")+","+Eval("SETNO")%>' class="glyphicon glyphicon-plus" OnClick="LnkPrintReceipt_Click"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                                            <SelectedRowStyle BackColor="#66FF99" />
                                                            <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

