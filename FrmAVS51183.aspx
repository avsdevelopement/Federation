<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51183.aspx.cs" Inherits="FrmAVS51183" %>

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

            window.location = "FrmAVS51178.aspx";
        }
    </script>
    <script type="text/javascript">
        var popup;
        function getCurrentIndex(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;

            //  alert(rowIndex);


        }
    </script>


    <script type="text/javascript">
        $(function () {
            $('[id*=lstDoc]').multiselect({
                includeSelectAllOption: true
            });
        });
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

        function EnterToTab() {
            //  var id = ElementID.id;

            //   alert(id);    

            if (event.keyCode == 13) {
                event.keyCode = 9;

            }

            //  alert("hello");

        }

        function EnterToTab1() {


            if (event.keyCode == 13)
                // alert("hello");
                event.keyCode = 9;

            document.getElementById('#grdInsert').focus();
            //document.getElementById('grdInsert').children(0).children(1).children(0).children(0)


        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstFruits]').multiselect
                (
                {
                    includeSelectAllOption: true
                }
            );
        });
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
                        Product Report
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="portlet-body">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <asp:RadioButtonList ID="RbtAll" runat="server" RepeatDirection="Horizontal" Width="400px">
                                                    <asp:ListItem Text="All" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Specific Vendor" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Specific Vendor Product" Value="3"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 9px 0 9px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:Label ID="Label00" runat="server" Text="Vendor ID"></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtVendorID" Width="200px" Placeholder="Enter Vendor ID" OnTextChanged="txtVendorID_TextChanged" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-1">
                                                    <asp:Label ID="Label5" runat="server" Width="120px" Text="Vendor Name"></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtVendorName" Placeholder="Enter Vendor Name" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="txtVendorName_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="true"></asp:TextBox>
                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtVendorName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetVendorName"
                                                        CompletionListElementID="CustList2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 9px 0 9px 0">
                                            <div class="col-lg-12">
                                                <div class="col-lg-1">
                                                    <label class="control-label ">Product ID.</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtProductID" Width="200px" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtProductID_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">

                                                    <asp:Label ID="Label4" Width="120px" runat="server" Text="Product Name"></asp:Label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtProductName" runat="server" AutoPostBack="true" OnTextChanged="txtProductName_TextChanged" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Enter Product Name" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="Autoglcode" runat="server" TargetControlID="txtProductName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetProductName"
                                                        CompletionListElementID="CustList">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-actions">
                                            <div class="row">

                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" CssClass="btn btn-primary" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="49" />
                                                    <asp:Button ID="btnClearAll" runat="server" Text="ClearAll" CssClass="btn btn-primary" OnClick="btnClearAll_Click" TabIndex="50" />
                                                    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-primary" TabIndex="51" OnClick="btnReport_Click" />

                                                </div>
                                            </div>
                                        </div>

                                        <asp:GridView ID="GrdProduct" runat="server" CellPadding="6" CellSpacing="7"
                                                                                    ForeColor="#333333" OnPageIndexChanging="GrdProduct_PageIndexChanging" OnRowCommand="GrdProduct_RowCommand"
                                                                                    PageIndex="10" AutoGenerateColumns="False" CssClass="mGrid"  BorderWidth="1px" OnSelectedIndexChanged="GrdProduct_SelectedIndexChanged"
                                                                                    BorderColor="#333300" Width="100%" AllowPaging="True"
                                                                                    ShowFooter="true">
                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                    <Columns>

                                                                                        <asp:TemplateField HeaderText="VENDOR ID" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="VENDORID" runat="server" Text='<%# Eval("VENDORID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="VENDOR NAME" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="VENDERNAME" runat="server" Text='<%# Eval("VENDERNAME ") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="PRODUCT ID." Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PRODID" runat="server" Text='<%# Eval("PRODID ") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="PRODUCT NAME" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PRODNAME" runat="server" Text='<%# Eval("PRODNAME") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="RATE" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="RATE" runat="server" Text='<%# Eval("RATE ") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="SGST" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="SGST" runat="server" Text='<%# Eval("SGST ") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CGST" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="CGST" runat="server" Text='<%# Eval("CGST ") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="SGSTAMT" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="SGSTAMT" runat="server" Text='<%# Eval("SGSTAMT ") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="CGSTAMT" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="CGSTAMT" runat="server" Text='<%# Eval("CGSTAMT ") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="TOTAlAMT" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="TOTALAMT" runat="server" Text='<%# Eval("TOTALAMT ") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="STATUS" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="DATE" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <%--                                                                                            <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="LnkAutorise" runat="server"  CommandArgument='<%#Eval("PRODID")%>' CommandName="Select" OnClick="LnkAutorise_Click"   class="glyphicon glyphicon-pencil" ></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>

                                                                                        <%-- <asp:TemplateField HeaderText="Delete" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkDelete1" runat="server" CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete1_Click" ></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>--%>

                                                                                        

                                                                                    </Columns>
                                                                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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
</asp:Content>

