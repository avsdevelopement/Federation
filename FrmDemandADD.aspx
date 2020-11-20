
<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDemandADD.aspx.cs" Inherits="FrmDemandADD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>

        function FormatIt(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "/";
            if (obj.value.length == 5) //MONTH
                obj.value = obj.value + "/";
            if (obj.value.length == 11) //YEAR
                alert("Enter Valid Date!....");
        }
    </script>

    <script>

        function Year(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "-";
            obj.value = obj.value;
        }
    </script>
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }


        function OnltAlphabets(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return true;

            return false;
        }

        function CheckFirstChar(key, txt) {
            if (key == 32 && txt.value.length <= 0) {
                return false;
            }
            else if (txt.value.length > 0) {
                if (txt.value.charCodeAt(0) == 32) {
                    txt.value = txt.value.substring(1, txt.value.length);
                    return true;
                }
            }
            return true;
        }

    </script>
    <script>

        function dateLen(dt) {
            var dt1 = dt + '';
            if (dt1.length == 1)
                dt1 = '0' + dt;

            return dt1;
        }
    </script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    Add Master
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="tab-pane active" id="tab__blue">
                                        <ul class="nav nav-pills">

                                            <li>
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click"  Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="1"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click"  Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="2"><i class="fa fa-arrows"></i>Modify</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="3"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" Visible="false" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="4"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                            </li>

                                            <li class="pull-right">
                                                <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                     <label class="control-label col-md-2">Mem No. <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMemNo"  CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtMemNo_TextChanged" placeholder="MemNo" TabIndex="5"></asp:TextBox>
                                                    </div>
                                                   
                                                    <label class="control-label col-md-3">Name <span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtName"  CssClass="form-control" runat="server" placeholder="Name" TabIndex="5"></asp:TextBox>
                                                    </div>
                                                   

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                
                                                    <label class="control-label col-md-2">Address<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" Placeholder="Address" runat="server" TabIndex="6" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                     <label class="control-label col-md-2">Pin Code<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtPincode" MaxLength="6" CssClass="form-control" Placeholder="Pin Code" runat="server" TabIndex="6" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            
                                               
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-10">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                               <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" TabIndex="20" OnClientClick="Javascript:return IsValide()" />
                                               <%--<asp:Button ID="btn_Modify" runat="server" Text="Modify"  CssClass="btn btn-primary" OnClick="btn_Modify_Click"  OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                                <asp:Button ID="BTN_AUTH" runat="server" Text="Authorize"  CssClass="btn btn-primary" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                                <asp:Button ID="btndelete" runat="server" Text="Delete"  CssClass="btn btn-primary"  OnClientClick="javascript:return IsValidate();" TabIndex="16" />--%>
                                                <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" OnClick="BtnClear_Click" Text="Clear"  TabIndex="21" />
                                                <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" OnClick="BtnExit_Click" Text="Exit"  TabIndex="22" />
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
</asp:Content>
