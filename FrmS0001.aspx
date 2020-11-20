<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmS0001.aspx.cs" Inherits="FrmS0001" %>

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
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && iKeyCode != 13 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
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
    <script type="text/javascript">
        function IsValide() {
            var srno = document.getElementById('<%=TxtSRONo.ClientID%>').value;
            var srname = document.getElementById('<%=TXTSROName.ClientID%>').value;
            var edate = document.getElementById('<%=TxtEffectDate.ClientID%>').value;
            var brcd = document.getElementById('<%=TxtBRCD.ClientID%>').value;

            if (srno == "") {
                message = 'Please Enter Srno Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtSRONo.ClientID %>').focus();
                return false;
            }
            if (srname == "") {
                message = 'Please Enter Srno name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TXTSROName.ClientID %>').focus();
                return false;
            }
            if (edate == "DD/MM/YYYY") {
                message = 'Please Enter Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtEffectDate.ClientID %>').focus();
                return false;
            }
            if (brcd == "") {
                message = 'Please Enter Branch Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtBRCD.ClientID %>').focus();
                return false;
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    SRO MASTER
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
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Modify</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
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
                                                    <label class="control-label col-md-2">SRNO:<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtSRONo" Placeholder="Srno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" Enabled="false" OnTextChanged="TxtSRONo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TXTSROName" Placeholder="Sro name" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Effect Date:<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtEffectDate" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this);CheckForFutureDatefrm();return tabE(this,event);" OnTextChanged="TxtEffectDate_TextChanged"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtEffectDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtEffectDate">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">BRCD:<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtBRCD" Placeholder="BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Reg Offc Ref No: </label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtRegSanc" Placeholder="Reg Sanction" CssClass="form-control" runat="server" OnTextChanged="TxtRegSanc_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Board Reso No: </label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtBoardResNo" Placeholder="Board Reso No" CssClass="form-control" runat="server" OnTextChanged="TxtBoardResNo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>

                                                    <label class="control-label col-md-2">Emp-ID: </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtEmpId" Placeholder="Emp-ID" CssClass="form-control" runat="server" OnTextChanged="TxtEmpId_TextChanged"></asp:TextBox>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Remark: </label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="TxtRemark" Placeholder="Remark" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
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
                                                <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" OnClientClick="Javascript:return IsValide()" />
                                                <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" />
                                                <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" />
                                            </div>
                                            <div class="col-md-5">
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
