<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmBrWiseGL.aspx.cs" Inherits="FrmBrWiseGL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Validate() {
            debugger;
            var TxtBrID = document.getElementById('<%=TxtBrID.ClientID%>').value;
            var TxtAccType = document.getElementById('<%=TxtAccType.ClientID%>').value;
            var TxtFDate = document.getElementById('<%=TxtFDate.ClientID%>').value;
            var TxtTDate = document.getElementById('<%=TxtTDate.ClientID%>').value;
            var message = '';

            if (TxtBrID == "") {
                message = 'Please Enter Branch Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtBrID.ClientID %>').focus();
                return false;
            }
            if (TxtAccType == "") {
                message = 'Please Enter Product Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtAccType.ClientID %>').focus();
                return false;
            }
            if (TxtFDate == "") {
                message = 'Please Enter From date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtFDate.ClientID %>').focus();
                return false;
            }
            if (TxtTDate == "") {
                message = 'Please Enter To Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtTDate.ClientID %>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        BranchWise GL Report
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                    </div>
                                    <div style="border: 1px solid #3598dc">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <asp:RadioButtonList ID="rbtnRptType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnRptType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Details" Value="3" />
                                                         <asp:ListItem Text="DateWise" Value="1" />
                                                        <asp:ListItem Text="Summary" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Branch Code :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Product Type :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAccType" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtAccType_TextChanged" AutoPostBack="true" Placeholder="Product Type" runat="server" CssClass="form-control" />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtATName" Placeholder="Product Name" OnTextChanged="TxtATName_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtATName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetGlName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">From Date :<span class="required">*</span></label>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-2">To Date :<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-2">
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6" style="text-align: center">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Report Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return Validate();" />
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

