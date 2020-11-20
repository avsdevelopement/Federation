<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInstrumentIssue.aspx.cs" Inherits="FrmInstrumentIssue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Sum() {
            var text1 = document.getElementById('<%= txtfrmno.ClientID %>');
            var text2 = document.getElementById('<%= txtnoleaves.ClientID %>');
            if (text1.value.length == 0 || text2.value.length == 0 || text1.value.length < 6) {
                alert('invalid number');
                $('#<%=txtfrmno.ClientID %>').focus();
                return;
            }
            var x = parseInt(text1.value);
            var y = parseInt(text2.value);
            document.getElementById('<%= txttono.ClientID %>').value = x + y;
        }
    </script>
    <script type="text/javascript">
        function IsValide() {
            var ptype = document.getElementById('<%=txttyp.ClientID%>').value;
            var accno = document.getElementById('<%=txtaccno.ClientID%>').value;
            // var txtcustnam = document.getElementById('<%=txtcustnam.ClientID%>').value;
            var Noleaves = document.getElementById('<%=txtnoleaves.ClientID%>').value;
            var frminstrm = document.getElementById('<%=txtfrmno.ClientID%>').value;
            var Toinstrm = document.getElementById('<%=txttono.ClientID%>').value;
            var message = '';

            if (ptype == "") {
                //alert("Please Enter Account Type.........!!");
                message = 'Please Enter Account Type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txttyp.ClientID %>').focus();
                return false;
            }
            if (accno == "") {
                //alert("Please Enter account Number.........!!");
                message = 'Please Enter account Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtaccno.ClientID %>').focus();
                return false;
            }

            if (Noleaves == "") {
                //alert("Please Enter Number of leaves .........!!");
                message = 'Please Enter Customer No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtnoleaves.ClientID %>').focus();
                return false;
            }

            if (frminstrm == "") {
                //alert("Please Enter From instrument  Number.........!!");
                message = 'Please Enter From instrument Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtfrmno.ClientID %>').focus();
               return false;
           }

           if (Toinstrm == "") {
               //alert("Please Enter From instrument  Number.........!!");
               message = 'Please Enter From instrument  Number....!!\n';
               $('#alertModal').find('.modal-body p').text(message);
               $('#alertModal').modal('show')
               $('#<%=txtfrmno.ClientID %>').focus();
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
                        dPayorder cheque Issue
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
                                            <li>
                                                <asp:LinkButton ID="lnkIssue" runat="server" Text="a" OnClick="lnkIssue_Click" class="btn-circle-top bg-blue-madison"><i class="fa fa-plus-circle"></i>Issue</asp:LinkButton>

                                            </li>

                                            <li>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server" Text="MD" class="btn-circle-top bg-blue-madison" OnClick="lnkAuthorize_Click"><i class="fa fa-pencil-square-o"></i>Authorize</asp:LinkButton>
                                            </li>

                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="DL" class="btn-circle-top bg-blue-madison" OnClick="lnkDelete_Click"><i class="fa fa-times"></i>Delete</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkReport" runat="server" Text="AT" class="btn-circle-top bg-blue-madison" OnClick="lnkReport_Click"><i class="fa fa-times"></i>Report</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkClose" runat="server" Text="DL" class="btn-circle-top bg-blue-madison" OnClick="lnkClose_Click"><i class="fa fa-times"></i>Close</asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Account Type</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txttyp" CssClass="form-control" runat="server" OnTextChanged="txttyp_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>

                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="txttypnam" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Account No. </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtaccno" CssClass="form-control" runat="server" OnTextChanged="txtaccno_TextChanged1" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="txtcustnam" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>


                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">No. Of Leaves </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtnoleaves" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From InstrumentNo.</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtfrmno" CssClass="form-control" runat="server" onblur="Sum()" onclientclick="Javascript: return Validate()"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">To InstrumentNo.</label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txttono" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Submit" runat="server" CssClass="btn blue" Text="Submit" OnClick="Submit_Click" OnClientClick="Javascript:return isvalidate();" />
                                                </div>
                                            </div>
                                        </div>
                                        <div>
                                            <asp:GridView ID="GridView" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99"
                                                PageSize="15"
                                                OnPageIndexChanging="GridView_PageIndexChanging"
                                                PagerStyle-CssClass="pgr" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SUBGLCODE" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PEFDate" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACCNO" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LEAVE" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LEAVE" runat="server" Text='<%# Eval("LEAVE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="INTFROM" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="INTFROM" runat="server" Text='<%# Eval("INTFROM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="INTTO" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="INTTO" runat="server" Text='<%# Eval("INTTO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("AID")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--</form>-->
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

