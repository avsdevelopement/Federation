<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDigitalBanking.aspx.cs" Inherits="FrmDigitalBanking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function IsValide() {
            var Fbranch = document.getElementById('<%=txtFromBranch.ClientID%>').value;
            var Tbranch = document.getElementById('<%=txtToBranch.ClientID%>').value;
            var PCode = document.getElementById('<%=txtProdCode.ClientID%>').value;
            var Date = document.getElementById('<%=txtDate.ClientID%>').value;
            var Charges = document.getElementById('<%=txtCharges.ClientID%>').value;
            var PL = document.getElementById('<%=txtPlCode.ClientID%>').value;
            var message = '';

            if (Fbranch == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter From Branch....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtFromBranch.ClientID %>').focus();
                  return false;
              }
              if (Tbranch == "") {
                  //alert("Please Select Prefix.....!!");
                  message = 'Please Enter To Branch....!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  $('#<%=txtToBranch.ClientID %>').focus();
                return false;
            }
            if (PCode == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtProdCode.ClientID %>').focus();
                  return false;
              }
              if (Date == "") {
                  //alert("Please Select Prefix.....!!");
                  message = 'Please Enter Date....!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  $('#<%=txtDate.ClientID %>').focus();
                  return false;
              }
              if (Charges == "") {
                  //alert("Please Select Prefix.....!!");
                  message = 'Please Enter Charges....!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  $('#<%=txtCharges.ClientID %>').focus();
                  return false;
              }
              if (PL == "") {
                  //alert("Please Select Prefix.....!!");
                  message = 'Please Enter PL Code....!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  $('#<%=txtPlCode.ClientID %>').focus();
                  return false;
              }
        }
        function PrintGrid() {
            alert("");
            var printContent = document.getElementById('<%= GridView1.ClientID %>');
    var printWindow = window.open("All Records", 
    "Print Panel", 'left=50000,top=50000,width=0,height=0');
    printWindow.document.write(printContent.innerHTML);
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="print" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="Div1">
                        <div class="portlet-title">
                            <div class="caption">
                                Digital Banking Charges
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab1">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblFBranch" CssClass="control-label col-md-2" Text="From Branch" runat="server"></asp:Label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtFromBranch" runat="server" CssClass="form-control" placeholder="From BRCD" OnTextChanged="txtFromBranch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtFName" runat="server" CssClass="form-control" placeholder="From BRCD"></asp:TextBox>
                                                            </div>
                                                            <asp:Label ID="lblTBranch" CssClass="control-label col-md-1" Text="To Branch" runat="server"></asp:Label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtToBranch" runat="server" CssClass="form-control" placeholder="To BRCD" OnTextChanged="txtToBranch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtTName" runat="server" CssClass="form-control" placeholder="To BRCD"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label1" CssClass="control-label col-md-2" Text="Product Code" runat="server"></asp:Label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtProdCode" runat="server" CssClass="form-control" placeholder="ProductCode" OnTextChanged="txtProdCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtProdName" runat="server" CssClass="form-control" placeholder="Product Name" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoProdName" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label2" CssClass="control-label col-md-2" Text="As Of Date" runat="server"></asp:Label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Placeholder="dd/MM/yyyy"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <asp:Label ID="Label3" CssClass="control-label col-md-2" Text="Charges" runat="server"></asp:Label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtCharges" runat="server" CssClass="form-control" placeholder="Charges"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Label4" CssClass="control-label col-md-2" Text="PL A/C" runat="server"></asp:Label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtPlCode" runat="server" CssClass="form-control" placeholder="ProductCode" OnTextChanged="txtPlCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtPlName" runat="server" CssClass="form-control" placeholder="Product Name" OnTextChanged="txtPlName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtPlName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetPlGlName" CompletionListElementID="Div2" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" align="center">
                                                            <asp:Button ID="btnTrail" runat="server" Text="Trail" CssClass="btn btn-primary" OnClick="btnTrail_Click" OnClientClick="Javascript:return IsValide();" />
                                                            <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-primary" OnClick="btnApply_Click" OnClientClick="Javascript:return IsValide();" />
                                                            <asp:Button ID="btnExist" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="btnExist_Click" />
                                                           <%-- <asp:Button ID="print" runat="server" Text="Print"  CssClass="btn btn-primary" OnClick="print_Click" />--%>
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
              <div class="row" runat="server">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99"
                                            OnPageIndexChanging="GridView1_PageIndexChanging"
                                            PageIndex="10" PageSize="25"
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>

                                              <%--  <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%#Eval("Srno") %>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="table_04" HorizontalAlign="Left"></HeaderStyle>
                                                    <ItemStyle CssClass="table_02" HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="SRNO" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TransDate" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                

                                                <asp:TemplateField HeaderText="BRCD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Narration" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="A/C No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Cheque" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CustNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Credit" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CustName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Debit" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BALANCE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Balance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Charges">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Charges" runat="server" Text='<%# Eval("CHARGES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

