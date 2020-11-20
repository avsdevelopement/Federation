<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDivmemPassbook.aspx.cs" Inherits="FrmDivmemPassbook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
        <%-- Only alphabet --%>
        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else {
                    return true;
                }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }


        <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }

        function SelectAll(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= GridView1009.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++)
                {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++)
                    {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox")
                        {
                            //assign the status of the Select All checkbox to the cell 
                            //checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Member Ledger List
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="Depositdiv" runat="server">
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <div class="col-md-8">
                                    <asp:RadioButtonList ID="rbtnRptType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnRptType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Div_Int Detail " Value="1" style="margin: 15px;" Selected="True" />
                                        <asp:ListItem Text="Div_Int Summary " Value="2" style="margin: 15px;" />
                                        <asp:ListItem Text="Paid / UnPaid Div_Int List " Value="3" style="margin: 15px;" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">From Branch<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="Txtfrmbrcd" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtfrmbrcd_TextChanged" AutoPostBack="true" CssClass="form-control" TabIndex="1" runat="server" />
                                </div>

                                <label class="control-label col-md-2" style="width: 120px">To Branch<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="Txttobrcd" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txttobrcd_TextChanged" AutoPostBack="true" CssClass="form-control" TabIndex="2" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div_DEP">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Division : <span class="required">* </span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DdlRecDiv" CssClass="form-control" runat="server" Enabled="True" OnSelectedIndexChanged="DdlRecDiv_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <label class="control-label col-md-2" style="width: 120px">Department : <span class="required">*  </span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DdlRecDept" CssClass="form-control" Enabled="True" OnSelectedIndexChanged="DdlRecDept_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;" runat="server" id="Div_Cust">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                    <label>From Cust No<span class="required"></span></label>
                                </div>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtCustNo" CssClass="form-control" Placeholder="Cust No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtCustNo_TextChanged" AutoPostBack="true" Style="width: 100px;" TabIndex="2"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtCustName" CssClass="form-control" Placeholder="Customer Name" OnTextChanged="txtCustName_TextChanged" AutoPostBack="true" runat="server" Style="width: 360px;" />
                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtCustName"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="GetCustNames">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                <div class="col-lg-12">
                                    <div class="col-md-2">
                                        <asp:Label ID="Lbltocust" runat="server" Text="To Cust No"></asp:Label>
                                    </div>
                                    <div class="col-md-1" runat="server" id="Div_To">
                                        <asp:TextBox ID="TxtToCustno" CssClass="form-control" Placeholder="To Cust No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="TxtToCustno_TextChanged" AutoPostBack="true" Style="width: 100px;"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4" runat="server" id="Div_ToName">
                                        <asp:TextBox ID="TxtToCustName" CssClass="form-control" Placeholder="To Customer Name" OnTextChanged="TxtToCustName_TextChanged" AutoPostBack="true" runat="server" Style="width: 360px;" />
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtToCustName"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetCustNames">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">From Date <span class="required"></span></label>
                            <div class="col-lg-2">
                                <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                </asp:CalendarExtender>
                            </div>
                            <label class="control-label col-md-2" style="width: 120px">To Date <span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0" runat="server" id="DIV1">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label">Financial Year</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="DdlAccActivity" runat="server" Enabled="True" OnSelectedIndexChanged="DdlAccActivity_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="control-label col-md-2" style="width: 120px">
                                <label class="control-label ">Total</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtAmountSpe" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0" runat="server" id="DIV5">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">DR ProductCode <span class="required"></span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtFprdcode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="TxtFprdcode_TextChanged" TabIndex="4" AutoPostBack="true" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtFprdname" CssClass="form-control" placeholder="Product Name" OnTextChanged="TxtFprdname_TextChanged" AutoPostBack="true" TabIndex="5" runat="server"></asp:TextBox>
                                <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TxtFprdname"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="getglname" CompletionListElementID="CustList1">
                                </asp:AutoCompleteExtender>
                            </div>
                            
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0" runat="server" id="DIV3">
                        <div class="col-lg-12">
                           <label class="control-label col-md-2">Post Date <span class="required"></span></label>
                            <div class="col-lg-2">
                                <asp:TextBox ID="TxtPostDt" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtPostDt">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtPostDt">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
                    

                    <div class="row" style="margin: 7px 0 7px 0; padding-top: 10px; text-align: center">
                        <div class="col-lg-12">
                            <div class="col-md-6">
                                <asp:Button ID="BtnPrint" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" TabIndex="12" />
                                <asp:Button ID="BtnCreate" runat="server" CssClass="btn btn-success" Text="Create" OnClick="BtnCreate_Click" OnClientClick="Javascript:return isvalidate();" />
                                <asp:Button ID="BtnModify" runat="server" CssClass="btn btn-success" Text="Modify" OnClick="BtnModify_Click" OnClientClick="Javascript:return isvalidate();" />
                                <asp:Button ID="BtnPost" runat="server" CssClass="btn btn-success" Text="Post" OnClick="BtnPost_Click" OnClientClick="Javascript:return isvalidate();" />
                                <asp:Button ID="BtnPostInd" runat="server" CssClass="btn btn-success" Text="Post Individual" OnClick="BtnPostInd_Click" OnClientClick="Javascript:return isvalidate();" />
                                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" TabIndex="13" />
                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" TabIndex="14" />
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="DIV2" style="margin-top: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-4">
                                <label class="control-label ">All Authorize (Div Wise)</label>
                                <asp:CheckBox ID="Chk_AllAutho" runat="server" AutoPostBack="true" OnCheckedChanged="Chk_AllAutho_CheckedChanged" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" id="DivCalculatedData" runat="server">
            <div class="col-md-12">
                <div style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover fixhead">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="Lbl_GridName" Text="" runat="server" Style="font-size: large; color: darkblue; background-color: aqua;"></asp:Label>
                                    <div class="col-md-12">
                                        <asp:Button ID="BtnUpdateAll" CssClass="btn btn-primary" runat="server" Text="Update All" OnClick="BtnUpdateAll_Click" Visible="false" />
                                    </div>

                                    <asp:GridView ID="GridView1009" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" ShowFooter="true" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="GridView1009_RowDataBound">
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
                                                    <asp:TextBox ID="txtSrNo" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' ToolTip='<%#Eval("Custno") %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Custno">
                                                <ItemTemplate>
                                                    <asp:Label ID="TxtCustNo" runat="server" Text='<%# Eval("Custno") %>' Style="width: 80px" ToolTip='<%#Eval("Custno") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustName">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustName" Text='<%# Eval("CustName") %>' ToolTip='<%#Eval("Custno") %>' CssClass="form-control" Style="width: 300px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-Width="80px" HeaderText="DIV_CR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbls1Inst" runat="server" Text='<%# Eval("DIV_CR") %>' Style="width: 80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:Label ID="lbls1Inst_tot" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Update Div">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbls1Inst_R" runat="server" Text='<%# Eval("RC_DIV") %>' Style="width: 90px" ToolTip='<%#Eval("RC_DIV") %>'></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:TextBox ID="lbls1Inst_R_tot" ItemStyle-HorizontalAlign="Center" Width="90px" Enabled="false" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="80px" HeaderText="INT_CR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbls1Bal" runat="server" Text='<%# Eval("INT_CR") %>' Style="width: 80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:Label ID="lbls1Bal_tot" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center" HeaderText="Update INT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lbls1Bal_R" runat="server" Text='<%#Eval("RC_INT") %>' ItemStyle-HorizontalAlign="Center" Width="90px" ToolTip='<%#Eval("RC_INT") %>'></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:TextBox ID="lbls1Bal_R_tot" ItemStyle-HorizontalAlign="Center" Width="90px" Enabled="false" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Total_CR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total_CR") %>' Style="width: 80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:Label ID="lblTotal_tot" runat="server" />
                                                    </div>
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Update RecDiv">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblRecDiv" runat="server" Text='<%# Eval("RecDiv") %>' Style="width: 90px" ToolTip='<%#Eval("RecDiv") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Update RecDept">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblRecDept" runat="server" Text='<%# Eval("RecDept") %>' Style="width: 90px" ToolTip='<%#Eval("RecDept") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left" HeaderText="Update ChequeNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblChequeNo" runat="server" Text='<%# Eval("INSTNO") %>' Style="width: 90px" ToolTip='<%#Eval("RecDept") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Status" Text='<%# Eval("Status") %>' ToolTip='<%#Eval("Custno") %>' CssClass="form-control" Style="width: 100px" runat="server" Enabled="false" />
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

        <asp:HiddenField ID="hdnCustNo" runat="server" Value="0" />
    </div>
    <%-- </div>--%>
</asp:Content>

