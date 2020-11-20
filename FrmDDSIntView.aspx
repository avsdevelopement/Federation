<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDDSIntView.aspx.cs" Inherits="FrmDDSIntView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                   DDS INTEREST MASTER REPORT
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="row" style="margin-bottom: 10px;">
                                      
                                      
                                       <%-- <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">BRCD:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFBRCD" Placeholder="From BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtFBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">BRCD:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTBRCD" Placeholder="To BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTBRCD_TextChanged1" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtTBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <div class="row" style="margin: 10px;"><strong></strong></div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Prod Code:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFPRD" Placeholder="From Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtFPRDName" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtFPRDName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="CustList">
                                                    </asp:AutoCompleteExtender>
                                                </div>

                                                <label class="control-label col-md-1">Prod Code:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTPRD" Placeholder="To Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TXtTPRDName" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="TXtTPRDName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="Div2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Cust Type: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlCustType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Period Type: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlperiodtype" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Y">years</asp:ListItem>
                                                        <asp:ListItem Value="M">Months</asp:ListItem>
                                                        <asp:ListItem Value="D">Days</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                            <div class="col-lg-12">
                                                <div class="col-md-6">

                                                    <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="Btn_Submit_Click" />
                                                    <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click" />
                                                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click" />
                                                      <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-success" OnClick= "btnreport_Click" />
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

    <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdVInterest" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="EFFECT DATE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="EFFECTDT" runat="server" Text='<%# Eval("EFFECTDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       

                                        <asp:TemplateField HeaderText="TDCUSTTYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTTYPE" runat="server" Text='<%# Eval("TDCUSTTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DEPOSITGL">
                                            <ItemTemplate>
                                                <asp:Label ID="DEPOSITGL" runat="server" Text='<%# Eval("DEPOSITGL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PERIOD TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="PERIODTYPE" runat="server" Text='<%# Eval("PERIODTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PERIOD FROM">
                                            <ItemTemplate>
                                                <asp:Label ID="PERIODFROM" runat="server" Text='<%# Eval("PERIODFROM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       

                                        <asp:TemplateField HeaderText="PERIOD TO " Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PERIODTO" runat="server" Text='<%# Eval("PERIODTO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="RATE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="RATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PENALTY" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PENALTY" runat="server" Text='<%# Eval("PENALTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BRCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PENALTY" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
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
</asp:Content>

