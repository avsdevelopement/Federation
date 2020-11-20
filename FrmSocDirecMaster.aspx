<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSocDirecMaster.aspx.cs" Inherits="FrmSocDirecMaster" %>

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
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        SOCIETY DIRECTORS MASTER 
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Mem / Cust No<span class="required" > *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCustNo" Height="30" OnTextChanged="txtCustNo_TextChanged" MaxLength="10" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Society Name<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtSociety" Height="30" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Sr No<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">

                                                <asp:TextBox ID="TxtSrno"  Height="30" CssClass="form-control" Enabled="false" MaxLength="5" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Director Name<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-4">

                                                <asp:TextBox ID="txtDirName"   Height="30" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Designation<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtDesignation"  Height="30" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Address 1<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtAdd1"  Height="30" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Address 2<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtAdd2"  Height="30" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">City<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCity"  Height="30" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">PinCode<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPinCode"  Height="30" onkeypress="javascript:return isNumber (event)" MaxLength="7" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Official Address<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtOffAdd"  Height="30" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Native Address<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtNatAdd"  Height="30" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Mobile 1<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtMOb1" CssClass="form-control"  Height="30" onkeypress="javascript:return isNumber (event)" MaxLength="12" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Mobile 2<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtMob2" CssClass="form-control"  Height="30" onkeypress="javascript:return isNumber (event)" MaxLength="12" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">DateOfBirth<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtDOB" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"  Height="30"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1"  runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDOB">
                                                </asp:CalendarExtender>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Pan Card No<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtPanNo"  Height="30" onkeypress="javascript:return isNumber (event)" MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Aadhar Card No<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtadhar" Height="30" onkeypress="javascript:return isNumber (event)" MaxLength="13" CssClass="form-control" runat="server" OnTextChanged="txtadhar_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-offset-3 col-md-9">
                                <asp:Button ID="BtnCreat" runat="server" Text="SUBMIT" CssClass="btn blue" OnClick="BtnCreat_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                <asp:Button ID="btnModify" runat="server" Text="MODIFY" CssClass="btn blue" OnClick="btnModify_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                <asp:Button ID="btnDelete" runat="server" Text="DELETE" CssClass="btn blue" OnClick="btnDelete_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                <asp:Button ID="btnclear" runat="server" Text="CLEARALL" CssClass="btn blue" OnClick= "btnclear_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <th>
                    <asp:GridView ID="grdvoucher" runat="server" AllowPaging="True"
                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                        EditRowStyle-BackColor="#FFFF99"
                        OnPageIndexChanging="grdvoucher_PageIndexChanging"
                        PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="grdvoucher_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="SrNo" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="AT" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SocietyNo" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="ATT" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SocietyName" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="ACNO" runat="server" Text='<%# Eval("SOCIETYNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DirectorName" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="Name" runat="server" Text='<%# Eval("DirectorName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="designation" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="Amount" runat="server" Text='<%# Eval("DIRECTORNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="City" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="Particulars" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="EffectiveDate" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="CRDR" runat="server" Text='<%# Eval("EFFECTDATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Edit" Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" runat="server"  OnClick="lnkedit_Click" CommandArgument='<%#Eval("SRNO")+","+ Eval("CUSTNO")%>' Text="MODIFY" CommandName="SELECT"  class="glyphicon glyphicon"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle CssClass="pgr" />
                        <SelectedRowStyle BackColor="#66FF99" />
                        <EditRowStyle BackColor="#FFFF99" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:GridView>
                </th>
            </div>
        </div>
    </div>
    </div>
        <div id="alertModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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
    </div>

</asp:Content>

