<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMinBal.aspx.cs" Inherits="FrmMinBal" %>

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
                else { return true; }
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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Minimum Balance
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row" style="margin: 7px 0 12px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Select </label>
                            <div class="col-md-3">
                                <asp:Button ID="btnAddNew" runat="server" CssClass="btn default" Text="Add new " OnClick="btnAddNew_Click" AccessKey="1" />
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Product Code <span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="Txtprdcode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="Txtprdcode_TextChanged" AutoPostBack="true" runat="server" TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="Txtprdname" TabIndex="2" CssClass="form-control" placeholder="Product Name" OnTextChanged="Txtprdname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="Txtprdname"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="getglname" CompletionListElementID="CustList">
                                </asp:AutoCompleteExtender>
                            </div>
                            <label class="control-label col-md-2">PL Credit Acct <span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="Txtaccno" TabIndex="3" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="No" OnTextChanged="Txtaccno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="Txtaccname" TabIndex="4" CssClass="form-control" placeholder="Account Name" OnTextChanged="Txtaccname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="AutoPlGlName" runat="server" TargetControlID="txtaccname"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="GetPlGlName" CompletionListElementID="CustList2">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>

                    </div>


                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                        </div>
                    </div>
                    <div runat="server" id="TblDiv_MainWindow">
                        <div id="Depositdiv" runat="server">

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Effective Date <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="Txteffcdate" TabIndex="5" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="Txteffcdate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txteffcdate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>


                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">All/Selected <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="DdlAlS" TabIndex="6" runat="server" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="A">All</asp:ListItem>
                                            <asp:ListItem Value="S">Selected</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <label class="control-label col-md-2">Acct.Type <span class="required"></span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="TxtAcctype" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="Ddlacctype" TabIndex="7" runat="server" CssClass="form-control" OnSelectedIndexChanged="Ddlacctype_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Skip Charges <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="DdlSkChrg" TabIndex="8" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <label class="control-label col-md-2">Allow TOD Balance <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="DdlTodBal" TabIndex="9" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                            <asp:ListItem Value="N">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Frequency of Appln <span class="required"></span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="TxtFno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="DDlFreType" TabIndex="10" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlFreType_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[Accct.Facility] : </strong></div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <div class="col-md-2">
                                    </div>
                                    <label class="control-label col-md-2">With Cheque Book <span class="required"></span></label>
                                    <div class="col-md-2">
                                    </div>
                                    <label class="control-label col-md-2">Without Cheque Book <span class="required"></span></label>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Min Balance Required <span class="required"></span></label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtwithChq" TabIndex="11" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="MinBal" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtwithoutChq" TabIndex="12" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="MinBal" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Charges <span class="required"></span></label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="Txtchrgw" TabIndex="13" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Charges" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="Txtchrgwit" TabIndex="14" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Charges" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Free Instances <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtFree" TabIndex="15" CssClass="form-control" onkeypress="javascript:return isNumber (event)" placeholder="No" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[Charges Parameter] : </strong></div>
                                </div>
                            </div>



                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Min.Charges <span class="required"></span></label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtMinChg" TabIndex="16" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Min" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                    </div>
                                    <label class="control-label col-md-2">Max.Charges <span class="required"></span></label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtMaxChg" TabIndex="17" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Max" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[Process Status] : </strong></div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Last Applied Date <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtAppDate" TabIndex="18" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtAppDate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAppDate">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                    <label class="control-label col-md-2">Particulars <span class="required"></span></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="Txtpartclrs" TabIndex="19" CssClass="form-control" placeholder="Name" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-4 col-md-9">
                                        <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="btn btn-success" OnClick="BtnAdd_Click" OnClientClick="javascript:return validate();" TabIndex="20" />
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" TabIndex="21" />
                                        <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" TabIndex="22" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" id="Div_grid" runat="server">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grddisplay" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" AllowPaging="True"
                                    EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" OnPageIndexChanging="grddisplay_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SrNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PRDCODE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PRDCODE" runat="server" Text='<%# Eval("PRDCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="CREDITPLACC" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CREDITPLACC" runat="server" Text='<%# Eval("CREDITPLACC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="CUSTNAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="MakerName" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MakerName" runat="server" Text='<%# Eval("MakerName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("CREDITPLACC")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkModify_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("CREDITPLACC")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("CREDITPLACC")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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

