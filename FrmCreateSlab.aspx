<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCreateSlab.aspx.cs" Inherits="FrmCreateSlab" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="page-wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                    <div class="col-lg-12">
                    </div>
                </div>
                <div class="portlet box blue" id="form_wizard_1">
                    <div class="portlet-title">
                        <div class="caption">
                            Create Slab For Deposit / Loan
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Enter Total Slab :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTotalSlab" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtTotalSlab_TextChanged" AutoPostBack="true" />
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divInsert" runat="server" visible="false" style="margin-top: 12px; margin-bottom: 10px;">
                                            <asp:GridView ID="grdInsert" runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSrNo" Enabled ="False" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="From Slab">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFromSlab" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="To Slab">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtToSlab" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>

                                <div class="form-actions" style="margin-top: 12px; margin-bottom: 10px;">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-danger" Text="Submit" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" Text="Back" OnClick="btnBack_Click" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-12">
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
    </div>

</asp:Content>

