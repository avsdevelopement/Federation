<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5109.aspx.cs" Inherits="FrmAVS5109" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        In-Operative Balance Reverse
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 125px">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtProdType" runat="server" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtProdType_TextChanged" AutoPostBack="true" Placeholder="Product Code" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtProdName" runat="server" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" CssClass="form-control"></asp:TextBox>
                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoGlName" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 125px">Account No : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtAccNo" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtAccName" OnTextChanged="txtAccName_TextChanged" AutoPostBack="true" Placeholder="Search Account Name" runat="server" CssClass="form-control" />
                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtCustNo" Enabled="false" Placeholder="Customer Number" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 125px">From Date : </label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtFDate" onkeyup="FormatIt(this)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                            </div>
                                            <label class="control-label col-md-2" style="width: 100px">To Date : </label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTDate" onkeyup="FormatIt(this)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divPayment" visible="false" runat="server">
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">Payment Account Details  : </strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 125px">Product Code : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPayProdType" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtPayProdType_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtPayProdName" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="txtPayProdName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoPayGlName" runat="server" TargetControlID="txtPayProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlWiseName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 125px">Account No : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPayAccNo" CssClass="form-control" PlaceHolder="ID" runat="server" OnTextChanged="txtPayAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtPayAccName" CssClass="form-control" PlaceHolder="Search Customer Name" runat="server" OnTextChanged="txtPayAccName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoPayAccName" runat="server" TargetControlID="txtPayAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetProdWiseName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <label class="control-label col-md-2" style="width: 100px">Balance : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPayBalance" CssClass="form-control" PlaceHolder="Balance" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 125px">Narration : </label>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtNarration" Text="By Transfer" CssClass="form-control" runat="server" />
                                                  </div>
                                               </div>
                                           </div>
                                       </div>   

                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnTransfer" runat="server" CssClass="btn blue" Text="Transfer" OnClick="btnTransfer_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="btnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div id="divTransaction" class="col-md-12" runat="server" visible="false">
            <div class="table-scrollable" style="width: 100%; height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdTransaction" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                    ShowFooter="true" AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="id" runat="server" Text='<%#Eval("EntryDate")+"_"+ Eval("SetNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="EntryDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Gl Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlCode" runat="server" Text='<%# Eval("GlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Prod Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="AccNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="200px" HeaderText="Customer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="30px" HeaderText="DrCr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrCr" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
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
                    <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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

