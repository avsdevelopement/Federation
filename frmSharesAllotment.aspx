<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="frmSharesAllotment.aspx.cs" Inherits="frmSharesAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function Valid() {
            debugger;
            var BranchCode = document.getElementById('<%=txtBranchCode.ClientID%>').value;
            var message = '';

            if (BranchCode == "") {
                message = 'Select branch from dropdown list first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=ddlBranchName.ClientID %>').focus();
                return false;
            }
        }

        function IsValid() {
            debugger;
            var txtBrCode = document.getElementById('<%=txtBrCode.ClientID%>').value;
            var txtAppNo = document.getElementById('<%=txtAppNo.ClientID%>').value;
            var txtcusname = document.getElementById('<%=txtcusname.ClientID%>').value;
            var message = '';

            if (txtBrCode == "") {
                message = 'Enter Branch Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtBrCode.ClientID %>').focus();
                return false;
            }

            if (txtAppNo == "") {
                message = 'Enter Application Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtAppNo.ClientID %>').focus();
                return false;
            }

            if (txtcusname == "") {
                message = 'Enter Name Of Customer...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtcusname.ClientID %>').focus();
                return false;
            }
        }

        function IsValidate() {
            debugger;
            var ddlStatus = document.getElementById('<%=ddlStatus.ClientID%>').value;
            var txtRegNo = document.getElementById('<%=txtRegNo.ClientID%>').value;
            var txtCertNo = document.getElementById('<%=txtCertNo.ClientID%>').value;
            var txtMemNo = document.getElementById('<%=txtMemNo.ClientID%>').value;
            var message = '';

            if (ddlStatus == "0") {
                message = 'Select Status First...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=ddlStatus.ClientID %>').focus();
                return false;
            }

            if (ddlStatus == "0") {
                if (txtRegNo == "") {
                    message = 'Enter Board Registration Number...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    $('#<%=txtRegNo.ClientID %>').focus();
                    return false;
                }

                if (txtCertNo == "") {
                    message = 'Enter Certificate Number...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    $('#<%=txtCertNo.ClientID %>').focus();
                    return false;
                }

                if (txtMemNo == "") {
                    message = 'Enter Member Number...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    $('#<%=txtMemNo.ClientID %>').focus();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Shares Allotment
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">

                                            <div id="DivApplications" runat="server" visible="false">

                                                <div class="row" style="margin: 5px 0 5px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Branch Name<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlBranchName" CssClass="form-control" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtBranchCode" Enabled="false" CssClass="form-control" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Working Date <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="txtWorkDate" Enabled="false" CssClass="form-control" runat="server" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 5px 0 5px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-10">
                                                            <label class="control-label ">Allotment Pending : </label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdAppDetails" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                                                        EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" Width="100%" EmptyDataText="No Remaining Appliaction for this branch">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Cust No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Customer Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="App No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAppNo" runat="server" Text='<%# Eval("AppNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="NoOfSHR">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoOfSHR" runat="server" Text='<%# Eval("NoOfSHR") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SHR Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotShrValue" runat="server" Text='<%# Eval("TotShrValue") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Enterence" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntFee" runat="server" Text='<%# Eval("EntFee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WelFare">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWelfare" runat="server" Text='<%# Eval("Other1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WelFare(Loan)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWelfareLoan" runat="server" Text='<%# Eval("MemberWelFee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Select" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkBox" runat="server" onclick="Check_Click(this)" />
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

                                                <div class="row" style="margin: 5px 0 5px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-10">
                                                            <label class="control-label ">Alloted Application : </label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdAllotedAppDetails" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                                                        EditRowStyle-BackColor="#FFFF99" Width="100%" EmptyDataText="No Alloted Appliaction for this branch">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Cust No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustNo1" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Customer Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustName1" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="App No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAppNo1" runat="server" Text='<%# Eval("AppNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Member No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMemberNo" runat="server" Text='<%# Eval("MemberNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="NoOfSHR">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoOfSHR1" runat="server" Text='<%# Eval("NoOfSHR") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SHR Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotShrValue1" runat="server" Text='<%# Eval("TotShrValue") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Enterence" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntFee1" runat="server" Text='<%# Eval("EntFee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WelFare1">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWelfare" runat="server" Text='<%# Eval("Other1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WelFare(Loan)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWelfareLoan1" runat="server" Text='<%# Eval("MemberWelFee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Set No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
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

                                            <div id="DivAppDetails" runat="server" visible="false">

                                                <div style="border: 1px solid #3598dc">

                                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Branch Code / App No <span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-lg-1">
                                                                <asp:TextBox ID="txtBrCode" Enabled="false" runat="server" placeholder="Branch Code" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <asp:TextBox ID="txtAppNo" Enabled="false" runat="server" placeholder="Application No" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-lg-1">
                                                                <label class="control-label">Name <span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-lg-4">
                                                                <asp:TextBox ID="txtcusname" Enabled="false" runat="server" placeholder="Search Customer Name" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <asp:TextBox ID="txtcustno" Enabled="false" placeholder="Customer Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label ">No Of Shares </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtNoOfShr" Enabled="false" CssClass="form-control" placeholder="No Of Shares" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label ">Share Value </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtShrValue" Enabled="false" CssClass="form-control" placeholder="Shares Value" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Total Shares </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtTotShr" Enabled="false" CssClass="form-control" placeholder="Total Shares Value" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label ">Saving Acc No </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtAccNo" Enabled="false" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtAccName" Enabled="false" Placeholder="Account Name" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Saving Amount </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtSavFee" Enabled="false" CssClass="form-control" placeholder="Saving Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Enterance Fee </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtEntFee" Enabled="false" CssClass="form-control" placeholder="Entrance Fee" runat="server"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="control-label">Welfare Fee </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtOther1" Enabled="false" CssClass="form-control" placeholder="Welfare Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Printing Fee </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtOther2" Enabled="false" CssClass="form-control" placeholder="Printing Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Member Welfare(Loan) </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMemWelFee" Enabled="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" CssClass="form-control" placeholder="Member WelFare(Loan) Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Servige Charge </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtSerChrFee" Enabled="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" CssClass="form-control" placeholder="Service Charge Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Total Amount </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtTotAmount" Enabled="false" CssClass="form-control" placeholder="Total Value" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Remark </label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtRemark" Enabled="false" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Remark" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Nominee 1 Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Full Name </label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtNomName1" Enabled="false" CssClass="form-control" placeholder="Full Name Of Nominee 1" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Birth Date </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtDOB1" Enabled="false" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-lg-1">
                                                                <asp:TextBox ID="TxtAge1" Enabled="false" CssClass="form-control" placeholder="Age" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Relation </label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="ddlRelation1" Enabled="false" Width="130px" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Nominee 2 Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Full Name </label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtNomName2" Enabled="false" CssClass="form-control" placeholder="Full Name Of Nominee 1" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Birth Date </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtDOB2" Enabled="false" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-lg-1">
                                                                <asp:TextBox ID="TxtAge2" Enabled="false" CssClass="form-control" placeholder="Age" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Relation </label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="ddlRelation2" Enabled="false" Width="130px" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                            <div id="DivApp" runat="server" visible="false">
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Status </label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="-- Select --" Value="0" />
                                                                <asp:ListItem Text="Application Allotment" Value="11" />
                                                                <asp:ListItem Text="Application Pending" Value="12" />
                                                                <asp:ListItem Text="Application Reject" Value="13" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Board Meeting Date </label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtMeetDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                                            <asp:CalendarExtender ID="txtMeetDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtMeetDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <div id="DivRegNo" runat="server">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Board Resolution No </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtRegNo" Enabled="true" runat="server" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Certificate No </label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtCertNo" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Membership No </label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtMemNo" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Remark </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtRemark1" TextMode="MultiLine" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">From Number </label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtFromNumber" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">To Number </label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtToNumber" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2">
                                                            <asp:CheckBox ID="ChkEntrance" Text="Enterance Fee" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:CheckBox ID="ChkWelfare" Text="Welfare Fee" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:CheckBox ID="ChkWelLoan" Text="Welfare Fee(Loan)" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="btnAppDetails" runat="server" Text="Show Details" CssClass="btn blue" OnClick="btnAppDetails_Click" />
                                                <asp:Button ID="btnBatchAllocate" runat="server" Text="Batch Allotment" CssClass="btn blue" OnClick="btnBatchAllocate_Click" />
                                                <asp:Button ID="btnNextDetails" runat="server" Text="Allotment" CssClass="btn blue" OnClientClick="Javascript:return IsValid();" OnClick="btnNextDetails_Click" />
                                                <asp:Button ID="btnAllocate" runat="server" Text="Submit" CssClass="btn blue" OnClientClick="Javascript:return IsValidate();" OnClick="btnAllocate_Click" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn blue" OnClick="btnClear_Click" />
                                                <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn blue" OnClick="btnExit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

