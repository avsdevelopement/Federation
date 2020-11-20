<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCurrencyAdd.aspx.cs" Inherits="FrmCurrencyAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charcode = (evt.which) ? (evt.which) : evt.keyCode;
            if (charcode > 31 && (charcode < 48 && charcde > 57)) {
                return false;
            }
            return true;

        }

    </script>
    <script type="text/javascript">
        function isvalidate() {

            var vtype, notetype, nofnotes;
            vtype = document.getElementById('<%=TxtVaultType.ClientID%>').value;
            notetype = document.getElementById('<%=TxtNoteType.ClientID%>').value;
            nofnotes = document.getElementById('<%=TxtNoOfNotes.ClientID%>').value;
            if (vtype == "") {
                message = 'Please Vault Type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtVaultType.ClientID%>').focus();
                  return false;
              }
              if (notetype == "") {
                  message = 'Please Note Type....!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  document.getElementById('<%=TxtNoteType.ClientID%>').focus();
                  return false;
              }
              if (nofnotes == "") {
                  message = 'Please Number of Notes....!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  document.getElementById('<%=TxtNoOfNotes.ClientID%>').focus();
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
                        Currency Type
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div id="DIVALL" runat="server" class="form-body">

                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Entry Date : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtEntrydate" CssClass="form-control" runat="server" Enabled="false" TabIndex="1"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="DIV1" class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Vault Type <span class="required">* </span></label>

                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtVaultType" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="2"></asp:TextBox>

                                                </div>
                                                <label class="control-label col-md-2">Closing Balance <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtCloseBal" CssClass="form-control" runat="server" TabIndex="3" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="DIV2" class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Note Type <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtNoteType" CssClass="form-control" runat="server" TabIndex="4" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">No. of Notes<span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtNoOfNotes" CssClass="form-control" runat="server" TabIndex="5" AutoPostBack="true" OnTextChanged="TxtNoOfNotes_TextChanged" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div id="DIV3" class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Total<span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div id="DIVRDB" runat="server" class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <asp:RadioButtonList ID="RdbView" runat="server" RepeatDirection="Horizontal" CssClass="radio-list">
                                                    <asp:ListItem Text="Created only" Value="CO" style="margin: 15px;" Selected="True"> </asp:ListItem>
                                                    <asp:ListItem Text="Authorized Only" Value="AO" style="margin: 25px;"> </asp:ListItem>
                                                    <asp:ListItem Text="All Entries" Value="AE" style="margin: 12px;"> </asp:ListItem>
                                                </asp:RadioButtonList>
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
                                <asp:Button ID="BtnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return isvalidate();" TabIndex="6" OnClick="BtnSubmit_Click" />
                                <asp:Button ID="BtnClearAll" runat="server" CssClass="btn blue" Text="Clear All" TabIndex="7" OnClick="BtnClearAll_Click" />
                                &nbsp;<asp:Button ID="Exit" runat="server" CssClass="btn blue" Text="Exit" TabIndex="8" OnClick="Exit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--</form>-->
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdCurr" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" 
                                    OnPageIndexChanging="GrdCurr_PageIndexChanging"
                                    EditRowStyle-BackColor="#FFFF99" 
                                    OnSelectedIndexChanged="GrdCurr_SelectedIndexChanged" 
                                    PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SR NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vault Type" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="V_TYPE" runat="server" Text='<%# Eval("V_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Note Type" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="NOTE_TYPE" runat="server" Text='<%# Eval("NOTE_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="No. of Notes" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="NO_OF_NOTES" runat="server" Text='<%# Eval("NO_OF_NOTES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       <asp:TemplateField HeaderText="TOTAL" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="TOTAL_VALUE" runat="server" Text='<%# Eval("TOTAL_VALUE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="STAGE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="STAGE" runat="server" Text='<%# Eval("STAGE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkModify" runat="server" CommandName="select" OnClick="LnkModify_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutho" runat="server" CommandName="select" OnClick="LnkAutho_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" CommandName="select" OnClick="LnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
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

