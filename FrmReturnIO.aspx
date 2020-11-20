<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmReturnIO.aspx.cs" Inherits="FrmReturnIO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
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
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Inward/Outward Return
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="tab-content">
                        <div id="error">
                        </div>
                        <div class="tab-pane active" id="tab1">
                            <asp:Table ID="TblDiv_MainWindow" runat="server">
                                <asp:TableRow ID="Tbl_R1" runat="server">
                                    <asp:TableCell ID="Tbl_c1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">

                                        <div class="row" style="margin: 7px 0 7px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-3">A/C Type : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="DdlReturnType" runat="server" CssClass="form-control" OnTextChanged="DdlReturnType_TextChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="--Select type--" Value="0"></asp:ListItem>
                                                        <%--<asp:ListItem Text="INWARD RETURN" Value="1"></asp:ListItem>--%>
                                                        <asp:ListItem Text="OUTWARD RETURN" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="row" style="margin: 7px 0 7px 0;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-3">Instr No : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtCheqNo" Placeholder="Chq No." runat="server" CssClass="form-control" OnTextChanged="TxtCheqNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-3">Instr dt : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtCheqDate" runat="server" CssClass="form-control" onkeyup="FormatIt(this);"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TTxtCheqDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtCheqDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtCheqDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtCheqDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-3">A/C Type : <span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtAccType" Placeholder="A/C Type" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccType_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="TxtAcctypeName" Placeholder="A/C Type Name" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAcctypeName_TextChanged"></asp:TextBox>
                                                            <div id="CustList5" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtAcctypeName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="CustList5">
                                                            </asp:AutoCompleteExtender>
                                                        </div></div></div>
                                                        <div class="row" style="margin: 7px 0 7px 0;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-3">A/c No : <span class="required">* </span></label>
                                                        <div id="ab" class="col-md-3">
                                                            <asp:TextBox ID="TxtAccNo" Placeholder="A/C No." runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="TxtAccName" Placeholder="A/C Holder Name" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged"></asp:TextBox>
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
                                                    </div></div></div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Information</strong></div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                </div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-3">Bank Cd : <span class="required">* </span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtBankCD" Placeholder="Bank Code" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtBankCD_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="TxtBankName" Placeholder="Bank Name" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtBankName_TextChanged"></asp:TextBox>
                                                        <%--<div id="Bank" style="height: 200px; overflow-y: scroll;"></div>--%>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="TxtBankName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="Bank"
                                                            ServiceMethod="getglname">
                                                        </asp:AutoCompleteExtender>
                                                    </div></div></div>

                                         <div class="row" style="margin: 7px 0 7px 0">
                                                      <div class="col-lg-12">
                                                    <label class="control-label col-md-3">Branch Cd : <span class="required">* </span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtBRCD" Placeholder="Br. Code" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtBRCD_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:TextBox ID="TxtBRCDName" Placeholder="Br. Name" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtBRCDName_TextChanged"></asp:TextBox>
                                                        <div id="Branch" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="TxtBRCDName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="Branch"
                                                            ServiceMethod="getglname">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                    </div>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-3">Instr Amt:<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtCAmount" Placeholder="Amount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Narration:<span class="required">*</span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtNarration" Placeholder="Narration" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-3">Return Reason : <span class="required">* </span></label>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="DdlReason" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                        
                                       </asp:TableCell>
                                  
                                    <asp:TableCell ID="TableCell1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">
                                        
                                        <%--<div class="row">--%>
                                            <div class="col-lg-12">
                                                <div class="table-scrollable" style="width:580px; height: 250px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="GrdDetails" runat="server" CellPadding="6" CellSpacing="7"
                                                                        ForeColor="#333333"
                                                                        PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                                                        BorderColor="#333300" Width="50%"
                                                                        ShowFooter="true">
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Subgl" Visible="true" HeaderStyle-BackColor="#99ffcc">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Subgl" runat="server" Text='<%# Eval("Subgl") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SubglName" Visible="true" HeaderStyle-BackColor="#99ffcc">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SubglName" runat="server" Text='<%# Eval("SubglName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="IW Sum Amt" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="IwInstrSumAmt" runat="server" Text='<%# Eval("IwInstrSumAmt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Iw Instr. Count" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="IwInstrCount" runat="server" Text='<%# Eval("IwInstrCount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="OW Sum Amt" Visible="true" HeaderStyle-BackColor="#ffcc99">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="OwInstrSumAmt" runat="server" Text='<%# Eval("OwInstrSumAmt") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Ow Instr. Count" Visible="true" HeaderStyle-BackColor="#ffcc99">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="OwInstrCount" runat="server" Text='<%# Eval("OwInstrCount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <div style="padding: 0 0 5px 0">
                                                                                        <asp:Label ID="Lbl_Numofnst" runat="server" />
                                                                                    </div>
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
                                        <%--</div>--%>
                                     
                                                <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <asp:Table ID="Tbl_Photo" runat="server">
                                                            <asp:TableRow ID="Rw_Ph1" runat="server">
                                                                <asp:TableCell ID="TblCell1" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                    <img id="Img1" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                </asp:TableCell>
                                                                <asp:TableCell ID="TblCell2" runat="server">
                                                                    <asp:Label ID="Label2" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                    <img id="Img2" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                        </asp:Table>
                                                        </div>
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot IWC<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtIwcTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot OWC<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtOwcTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot OWC-R<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtOwcRTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot IWC-R<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtIwcRTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot IWCUnp<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtIWCUTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot OWCUnp<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtOWCUTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                            </asp:TableCell>
                                          
                                </asp:TableRow>
                                </asp:Table>
                            <div class="row" style="margin: 7px 0 7px 0;">
                                <div class="col-lg-12">
                                    <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Submit_Click" />
                                    <asp:Button ID="Btn_Clear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="Btn_Clear_Click" />
                                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Btn_Exit_Click" />
                                </div>
                            </div>
                        </div>

                        </div>
                    </div>
                </div>
            </div>

            <div style="border: 1px solid #3598dc">
                <div class="row" style="margin: 7px 0 7px 0">
                    <div class="col-lg-12" style="height: 50%">
                        <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                            <asp:Label ID="LBL_IO" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            <asp:GridView ID="Grd_IOReturn" OnPageIndexChanging="Grd_IOReturn_PageIndexChanging" runat="server" CellPadding="6" CellSpacing="7" ForeColor="#333333" CssClass="mGrid" BorderWidth="1px" BorderColor="#333300" Width="100%" AllowPaging="true" OnRowDataBound="Grd_IOReturn_RowDataBound" AutoGenerateColumns="false">
                                <Columns>

                                    <asp:BoundField DataField="SET_NO" HeaderText="SET NO." HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="ENTRYDATE" HeaderText="ENTRYDATE" HeaderStyle-BackColor="#99ccff" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="AT" HeaderText="A/C TYPE" HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="AC" HeaderText="A/C NO." HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="Name" HeaderText="CUST NAME" HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="PARTICULARS" HeaderText="PARTICULARS" HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="Amount" HeaderText="AMOUNT" HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="InstNo" HeaderText="CHQ NO." HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="Date1" HeaderText="INST Date" HeaderStyle-BackColor="#99ccff" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="STAGE" HeaderText="STAGE" HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" HeaderStyle-BackColor="#99ccff" />
                                    <asp:BoundField DataField="BRCD" HeaderText="BRCD" HeaderStyle-BackColor="#99ccff" />

                                </Columns>
                            </asp:GridView>
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

