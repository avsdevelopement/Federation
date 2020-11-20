<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51180.aspx.cs" Inherits="FrmAVS51180" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .form-inline .form-group {
            margin-right: 10px;
        }

        .well-primary {
            color: rgb(255, 255, 255);
            background-color: rgb(66, 139, 202);
            border-color: rgb(53, 126, 189);
        }

        .glyphicon {
            margin-right: 5px;
        }


        inset {
            border-style: inset;
        }

        /*Media Query*/
        @media (min-width: 11px) {

            .input-group {
                margin-top: -8px;
            }
        }

        .example-modal .modal {
            position: relative;
            top: auto;
            bottom: auto;
            right: auto;
            left: auto;
            display: block;
            z-index: 1;
        }


        .example-modal .modal {
            background: transparent !important;
        }
    </style>
    <script type="text/javascript">
        function ShowPopup(title, body) {

            alert(title);

            window.location = "FrmAVS51173.aspx";
        }
    </script>
    <script type="text/javascript">
        function FormatIt(obj) {

            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
            {
                alert("Please Enter valid Date");
                obj.value = "";
            }
        }
    </script>
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                            Closing Stock Details
                    </div>
                </div>

            </div>
            <div class="portlet-title">
                <div class="caption">
                    <div class="form-actions">


                        <div style="border: 1px solid #3598dc">

                            <div class="row" style="margin: 7px 0 3px 0">
                                <div class="col-lg-12">
                                    <div class="col-md-1">
                                        <label class="control-label ">BRCD</label>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtBrCd" CssClass="form-control" Width="170px" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="txtBrCd_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" TabIndex="1"></asp:TextBox>

                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label">Branch Name</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtBrCdName" runat="server" AutoPostBack="true" placeholder="Enter BRCD Name" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" TabIndex="2"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <div class="col-md-1">
                                        <label class="control-label ">Vendor ID</label>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtVendorId" CssClass="form-control" Width="170px"  onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" OnTextChanged="TxtVendorId_TextChanged" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                        <div id="Div1" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtVendorId"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetVendorName"
                                            CompletionListElementID="Div1">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label">Vendor Name</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtVendorName" runat="server" AutoPostBack="true" placeholder="Enter Vendor Name" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="txtVendorName_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtVendorName"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetVendorName"
                                            CompletionListElementID="CustList">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <div class="col-md-1">
                                        <label class="control-label">Product Id </label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtProductId" Width="170px" Placeholder="Enter Product Name" runat="server" CssClass="form-control" TabIndex="4" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="TxtProductId_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label">Product Name </label>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtProductNAme" Placeholder="Enter  Product Name" runat="server" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="txtProductNAme_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="4" AutoPostBack="true"></asp:TextBox>
                                        <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="AutoProduct" runat="server" TargetControlID="txtProductNAme"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetProductName"
                                            CompletionListElementID="CustList2">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <div class="col-md-1">
                                        <label class="control-label ">As On Date<span class="required">*</span></label>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtEntryDate" runat="server" Width="170px" onblur="WorkingDate()" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" CssClass="form-control" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            
                            <br />

                            <div class="form-actions">
                                <div class="row">

                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="49" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" TabIndex="50" />
                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="btnReport_Click" TabIndex="51" />
                                        <asp:Button ID="btnClosingStockReport" runat="server" Text="Closing Stock Report" CssClass="btn btn-primary" OnClick="btnClosingStockReport_Click" TabIndex="51" />
                                        <asp:Button ID="btnCalculate" runat="server" Text="Calculate Stock" CssClass="btn btn-primary" OnClick="btnCalculate_Click" TabIndex="51" />

                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row" id="Div_grid" runat="server">
                                <div class="col-md-12">
                                    <div class="table-scrollable" style="width: 100%">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:GridView ID="GrdStock" runat="server" CellPadding="6" CellSpacing="7"
                                                            ForeColor="#333333"
                                                           AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                                            BorderColor="#333300" Width="100%" AllowPaging="true"
                                                            ShowFooter="true">
                                                            <AlternatingRowStyle BackColor="White" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BRCD" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="VENDOR ID" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="VENDORID" runat="server" Text='<%# Eval("VENDORID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="VENDORNAME" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="VENDERNAME" runat="server" Text='<%# Eval("VENDERNAME ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PRODUCT ID." Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PRODID" runat="server" Text='<%# Eval("PRODID ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="PRODUCTNAME" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PRODNAME" runat="server" Text='<%# Eval("PRODNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="InStock" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="INQTY" runat="server" Text='<%# Eval("INQTY ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="OutStock" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="OUTQTY" runat="server" Text='<%# Eval("OUTQTY ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UseStock" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="USEQTY" runat="server" Text='<%# Eval("USEQTY ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="UnClearInQty" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="UNCLINQTY" runat="server" Text='<%# Eval("UNCLINQTY ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UnClearOutQty" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="UNCLOUTQTY" runat="server" Text='<%# Eval("UNCLOUTQTY ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UnClearUsedQty" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="UNCLUSEQTY" runat="server" Text='<%# Eval("UNCLUSEQTY ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ClearStock" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="CLSTOCK" runat="server" Text='<%# Eval("CLSTOCK ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="UnClearStock" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="UNCLSTOCK" runat="server" Text='<%# Eval("UNCLSTOCK ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="AvailStock" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="AVAILSTOCK" runat="server" Text='<%# Eval("AVAILSTOCK ") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="AllStock" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ALLSTOCK" runat="server" Text='<%# Eval("ALLSTOCK") %>'></asp:Label>
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
        </div>
</asp:Content>
