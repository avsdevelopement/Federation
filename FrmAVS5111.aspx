<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5111.aspx.cs" Inherits="FrmAVS5111" %>

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

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Daily Account Passbook Checking
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch Code<span class="required"></span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtBRCD" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtBrname" CssClass="form-control" runat="server" Enabled="false" Style="text-transform: uppercase;"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Agent Code<span class="required"></span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtAccType" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccType_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtATName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Month : <span class="required"></span></label>
                                                <div class="col-lg-1">
                                                    <asp:TextBox ID="txtMonth" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Year : <span class="required"></span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtYear" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <asp:HiddenField ID="TxtSrno" runat="server" />
                                <asp:HiddenField ID="txtAccNo" runat="server" />
                                <asp:HiddenField ID="txtName" runat="server" />
                                <asp:HiddenField ID="TxtOpening" runat="server" />
                                <asp:HiddenField ID="TxtCollection" runat="server" />
                                <asp:HiddenField ID="Txtpassbook" runat="server" />
                                <asp:HiddenField ID="txtDiff" runat="server" />
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Submit_Click" OnClientClick="Javascript:return Validate();" />
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="btnReport_Click" OnClientClick="Javascript:return Validate();" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary" Text="ClearAll" OnClick="btnClear_Click" />
                                        <asp:Button ID="Btn_Exit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="Btn_Exit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" id="divGridDetails" runat="server" visible="false">
        <div class="col-md-12">
            <div class="table-scrollable" style="height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdDaily" runat="server" AutoGenerateColumns="false" BackColor="#ffffff" CssClass="noborder fullwidth" OnRowDataBound="GrdDaily_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtSrno" Enable="false" CssClass="form-control" Enabled="false" runat="server" Text='<%#Eval("SrNo") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acc No" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAccNo" onkeypress="javascript:return isNumber(event)" Enabled="false" CssClass="form-control" runat="server" Text='<%#Eval("ACCNO") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cust Name" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server" Enabled="false" Text='<%#Eval("Custname") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening" ItemStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtOpening" CssClass="form-control" runat="server" Enabled="false" Text='<%#Eval("OpeningBal") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Collection" ItemStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtCollection" CssClass="form-control" runat="server" Enabled="false" Text='<%#Eval("Credit") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Passbook Amt" ItemStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Txtpassbook" CssClass="form-control" runat="server" Text='<%#Eval("Amount") %>' AutoPostBack="true" OnTextChanged="Txtpassbook_TextChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Diffrance" ItemStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDiff" CssClass="form-control" runat="server" Text='<%#Eval("Diff") %>' AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Check" ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlChecking" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Hidden" Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HDStatus" runat="server" Value='<%#Eval("Status") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <HeaderStyle BackColor="#99ccff" />
                                </asp:GridView>
                            </th>
                        </tr>
                    </thead>
                </table>
                <div class="row">
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-md-offset-3 col-md-9">
                            <asp:Button ID="BtnCreate" runat="server" CssClass="btn btn-primary" Text="Create" OnClick="BtnCreate_Click" OnClientClick="Javascript:return Validate();" />
                            <asp:Button ID="btnClearAll" runat="server" CssClass="btn btn-primary" Text="ClearAll" OnClick="btnClearAll_Click" />
                            <asp:Button ID="btnExit1" runat="server" CssClass="btn btn-primary" Text="Exitgrid" OnClick="btnExit1_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


