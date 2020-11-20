<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmIIdentityP.aspx.cs" Inherits="FrmIIdentityP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript">

        function FormatIT(obj)
        {
            if (obj.length.value == 2)
                obj.value = obj.value + "/";
            if (obj.length.value == 5)
                obj.value = obj.value + "/";
            if (obj.length.value == 11)
                alert("Please enter valid date");

        }
    </script>

    <%--<script type="text/javascript">
        function isvalidate() {

            var mname = document.getElementById('<%=txtmem.ClientID%>').value;
           // var ddlper = document.getElementById('<%=ddlprefix.ClientID%>').value;
           // var fname = document.getElementById('<%=Textname.ClientID%>').value;
            //var mname = document.getElementById('<%=Textmiddle.ClientID%>').value;
            //var lname = document.getElementById('<%=Textlast.ClientID%>').value;
            //var pan = document.getElementById('<%=txtpan.ClientID%>').value;
            //var aadhaar = document.getElementById('<%=txtuid.ClientID%>').value;
            //var message = '';


            if (mname == "") {               
                message = 'Please Enter member No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtmem.ClientID%>').focus();
                return false;
            }

            if (ddlper == "0") {
                message = 'Please select prefix....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlprefix.ClientID%>').focus();
                return false;
            }

            if (fname == "") {
                message = 'Please Enter first name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=Textname.ClientID%>').focus();
                return false;
            }

            if (mname == "") {
                message = 'Please Enter middle name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=Textmiddle.ClientID%>').focus();
                return false;
            }

            if (lname == "") {
                message = 'Please Enter last name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=Textlast.ClientID%>').focus();
                return false;
            }

            if (pan == "") {
                message = 'Please Enter pan No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtpan.ClientID%>').focus();
                return false;
            }

            if (aadhaar == "") {
                message = 'Please Enter aadhaar No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtuid.ClientID%>').focus();
                return false;
            }
        }

    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Proof of Identity
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                        <ul class="nav nav-pills">
                                            <li class="pull-right">
                                                <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Member NO.</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtmem" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-6">
                                                <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">

                                            <label class="control-label col-md-2">Name </label>

                                            <div class="col-lg-2">
                                                <asp:DropDownList ID="ddlprefix" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Miss" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Mrs" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Mr" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="Textname" Placeholder="First Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="Textmiddle" Placeholder="Middle Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="Textlast" placeholder="Last Name" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">

                                            <label class="control-label col-md-2">PAN </label>

                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtpan" Placeholder="PANCARD NO." runat="server" style="text-transform:uppercase" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <label class="control-label col-md-2">UID (Aadhaar) </label>

                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtuid" Placeholder="UId" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">

                                            <label class="control-label col-md-2">Voter ID Card</label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtvoterid" Placeholder="Voterid" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <label class="control-label col-md-2">NREGA Job Card </label>

                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtjob" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Passport number</label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtpassno" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <label class="control-label col-md-2">PassportExpiry Date </label>

                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtpassdate" placeholder="dd/mm/yyyy" runat="server" CssClass="form-control" onkeyup="FormatIT(this)" onkeypress="javascript: return isNumber (event)"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtpassdate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtpassdate">
                                    </asp:CalendarExtender>
                                            </div>

                                        </div>
                                    </div>


                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Driving License</label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtlicense" CssClass="form-control" style="text-transform:uppercase" runat="server"></asp:TextBox>
                                            </div>

                                            <label class="control-label col-md-2">licenseExpiry Date</label>

                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtlicensedat" Placeholder="dd/mm/yyyy" runat="server"  style="text-transform:uppercase" CssClass="form-control" onkeyup="FormatIT(this)" onkeypress="javascript:return isNumber (event) "></asp:TextBox>
                                                  <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtpassdate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtpassdate">
                                    </asp:CalendarExtender>
                                            </div>


                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Others</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtother" CssClass="form-control" runat="server" placeholder="Any document notified by central government"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>

                            <!--</form>-->
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

