<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5137.aspx.cs" Inherits="FrmAVS5137" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
          function isNumber(evt) {
              evt = (evt) ? evt : window.event;
              var charCode = (evt.which) ? evt.which : evt.keyCode;
              if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                  return false;
              }
          }

          function FormatIt(obj) {
              if (obj.value.length == 2)
                  obj.value = obj.value + "/";
              if (obj.value.length == 5)
                  obj.value = obj.value + "/";
              if (obj.value.length == 11)
                  alert("Date invalid....");
          }

          function WorkingDate(obj) {
              debugger;
              var ToDate = document.getElementById('<%=txtIssueDate.ClientID%>').value || 0;
            var WorkDate = document.getElementById('<%=WorkingDate.ClientID%>').value;

            var TDate = ToDate.substring(0, 2);
            var TMonth = ToDate.substring(3, 5);
            var TYear = ToDate.substring(6, 10);
            var ToDate = new Date(TYear, TMonth - 1, TDate);

            var WDate = WorkDate.substring(0, 2);
            var WMonth = WorkDate.substring(3, 5);
            var WYear = WorkDate.substring(6, 10);
            var WorkDate1 = new Date(WYear, WMonth - 1, WDate);

            if (ToDate > WorkDate1) {
                window.alert("Not allow greter than working date : " + WorkDate + "...!!\n");
                document.getElementById('<%=txtIssueDate.ClientID %>').value = "";
                document.getElementById('<%=txtIssueDate.ClientID%>').focus();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
      
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Cheque Stock
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            
                                            <div class="col-md-12">
                                                <asp:RadioButtonList ID="rbtnType" runat="server" OnSelectedIndexChanged="rbtnType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" Style="width: 400px;">
                                                    <asp:ListItem Text="Bank Cheque Stock" Value="1"  Selected="True" />
                                                    <asp:ListItem Text="Cheque Stock" Value="2"  />
                                                    <asp:ListItem Text="Loose Cheque" Value="3" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                     <div id="DivBank" runat="server" visible="true" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 110px">Bank Code <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBankcode" runat="server" CssClass="form-control" OnTextChanged="txtBankcode_TextChanged"  AutoPostBack="true"  onkeypress="javascript:return isNumber(event)" Placeholder="Bank Code"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control"  AutoPostBack="true" Placeholder="Search Bank Name" OnTextChanged="txtBankName_TextChanged" onkeypress="return checkQuote();"></asp:TextBox>
                                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoBankname" runat="server" TargetControlID="txtBankName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList3">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                              </div>

                                    <div id="divAccInfo" runat="server" visible="false" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 110px">Prod Type <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtProdType" runat="server" CssClass="form-control" OnTextChanged="txtProdType_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Product Type"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtProdName" runat="server" CssClass="form-control" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" onkeypress="return checkQuote();"></asp:TextBox>
                                                        <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoGlName" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 110px">Acc No <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Account Number" />
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtAccName" runat="server" CssClass="form-control" OnTextChanged="txtAccName_TextChanged" AutoPostBack="true" Placeholder="Search Account Name" onkeypress="return checkQuote();" />
                                                        <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoAccName" runat="server" TargetControlID="txtAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList2">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <label class="control-label col-md-1">Cust No </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCustNo" Enabled="false" runat="server" CssClass="form-control" Placeholder="Customer Number" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12" >
                                            <label class="control-label col-md-2" style="width: 110px">No.Of Books :<span class="required">*</span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtNoOfBooks" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="txtNoOfBooks_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-1">Book Size :<span class="required">*</span></label>
                                            <div class="col-lg-2">
                                                <asp:DropDownList ID="ddlBookSize" CssClass="form-control" OnSelectedIndexChanged="ddlBookSize_SelectedIndexChanged"  AutoPostBack="true" runat="server">
                                                    <asp:ListItem Text="--Select--" Value="0" />
                                                    <asp:ListItem Text="03" Value="3" />
                                                    <asp:ListItem Text="15" Value="15" />
                                                     <asp:ListItem Text="25" Value="25" />
                                                    <asp:ListItem Text="30" Value="30" />
                                                    <asp:ListItem Text="45" Value="45" />
                                                    <asp:ListItem Text="60" Value="60" />
                                                    <asp:ListItem Text="90" Value="90" />
                                                </asp:DropDownList>
                                            </div>
                                            <label class="control-label col-md-1">Start Num :</label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtSInstNo" onkeypress="javascript:return isNumber (event)" MaxLength="6" runat="server" CssClass="form-control" OnTextChanged="txtSInstNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-1">End Num :</label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtEInstNo" onkeypress="javascript:return isNumber (event)" MaxLength="6" runat="server" CssClass="form-control" OnTextChanged="txtEInstNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" style="width: 110px">Stock Date :</label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtIssueDate" onblur="WorkingDate()" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <input type="hidden" id="WorkingDate" runat="server" value="" />
                                            </div>
                                            <label class="control-label col-md-2" style="width: 110px">Remarks :</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" UseSubmitBehavior="false" OnClick="btnSubmit_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                        <asp:Button ID="btnAuthorize" Visible="false" runat="server" CssClass="btn blue" Text="Authorize" OnClick="btnAuthorize_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
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
            <div class="table-scrollable" style="height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
                <asp:Label ID="lblPrint" runat="server" Text="Need to Authorize" BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdCHQStock" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderStyle-Width="30px" HeaderText="SrNo" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="IssuedDate" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssuedDate" runat="server" Text='<%# Eval("IssuedDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="NoOfBooks" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoOfBooks" runat="server" Text='<%# Eval("NoOfBooks") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="BookSize" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBookSize" runat="server" Text='<%# Eval("BookSize") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="StartInsNo" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartInsNo" runat="server" Text='<%# Eval("StartInsNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Maker" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Text='<%# Eval("Maker") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Authorize" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server"  CommandArgument='<%#Eval("ID")+","+ Eval("Mid")%>' CommandName="select" class="glyphicon glyphicon-check" OnClick="lnkAuthorize_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Delete" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID")+","+ Eval("Mid")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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
</asp:Content>

