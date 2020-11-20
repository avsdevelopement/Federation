<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmNotice_SRODetail.aspx.cs" Inherits="FrmNotice_SRODetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isvalidate() {

        }
    </script>

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
       <script>

           function Year(obj) {
               if (obj.value.length == 2) //DAY
                   obj.value = obj.value + "-";
               obj.value = obj.value;
           }
    </script>

    <script type="text/javascript">
        function ShowHideDiv() {

            if ($("#RdbSingle").is(":checked")) {
                $("#Single").hide();
                $("#Multiple").show();
            }
            else {
                $("#Single").show();
                $("#Multiple").hide();
            }
        }
    </script>

    <script type="text/javascript">
        function ShowHideDiv1() {
            if ($("#RdbMultiple").is(":checked")) {
                $("#Single").show();
                $("#Multiple").hide();
            }
            else {
                $("#Single").hide();
                $("#Multiple").show();

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
                        <asp:Label ID="lblheader" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div id="DIVAC" runat="server" class="form-horizontal">
                        <div class="form-body">
                            <div class="tab-content">
                                <div id="error">
                                </div>
                                <div class="tab-pane active" id="tab1">
                                    <div class="row" style="margin: 7px 0 7px 0;">
                                        <div class="col-lg-12">
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-1">Branch Name <span class="required">*</span></label>
                                        <div class="col-lg-3">
                                            <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtbranchcode1" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-1">Case Year <span class="required">*</span></label>
                                        <div class="col-lg-1">
                                            <asp:TextBox ID="txtloancode1" AutoPostBack="true" Placeholder="YY-YY"  onkeyup="Year(this)" CssClass="form-control" OnTextChanged="txtloancode1_TextChanged" runat="server"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-1">Case No <span class="required">*</span></label>
                                        <div class="col-lg-1">
                                            <asp:TextBox ID="TXTACCNO" placeholder="Case No" AutoPostBack="true" CssClass="form-control" OnTextChanged="TXTACCNO_TextChanged" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div id="Div1" runat="server" visible ="false">
                                    <div class="row" style="margin: 7px 0 7px 0; padding-top: 15px;">
                                        <div class="col-md-12">
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-2">
                                                <asp:RadioButton ID="RbnAdd1" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="RbnAdd1_CheckedChanged" Text="Present Address" groupid="baldate" GroupName="AS"></asp:RadioButton>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:RadioButton ID="RbnAdd2" runat="server" Text="permanent Address" AutoPostBack="true" OnCheckedChanged="RbnAdd2_CheckedChanged" groupid="baldate" GroupName="AS"></asp:RadioButton>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:RadioButton ID="RbnAdd3" runat="server" Text="Office Address" AutoPostBack="true" groupid="baldate" OnCheckedChanged="RbnAdd3_CheckedChanged" GroupName="AS"></asp:RadioButton>
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="TXtShowAdd" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Notice Report" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" />
                                    <asp:Button ID="btnExit" runat="server" CssClass="btn blue" Text="Back" OnClick="btnExit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
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
        <asp:HiddenField ID="hdnid" runat="server" />
</asp:Content>

