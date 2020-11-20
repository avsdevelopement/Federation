<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmNoticeCharges.aspx.cs" Inherits="FrmNoticeCharges" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </script>
      <script type="text/javascript">
          function Validate() {
              var Date = document.getElementById('<%=txtDate.ClientID%>').value;
            var Notice = document.getElementById('<%=txtDesc.ClientID%>').value;
            var Secured = document.getElementById('<%=RdtSecured.ClientID%>').value;
            var Charges = document.getElementById('<%=txtCharges.ClientID%>').value;
            var Taxes = document.getElementById('<%=txtTax.ClientID%>').value;
              if (Date == "DD/MM/YYYY") {
                alert("Please enter Effect Date......!!");
                return false;
            }

              if (Notice == "") {
                alert("Please Enter Notice Description......!!");
                return false;
            }
              if (Secured == "") {
                alert("Please Select notice type.......!!");
                return false;
            }
              if (Charges == "") {
                alert("Please Enter Charges........!!");
                return false;
            }
              if (Taxes == "") {
                alert("Please Enter Taxes..........!!");
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
                        Loan Notice Charges
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <!-- mycode start  -->
                                    <div style="border: 1px solid #3598dc">

                                        <div class="row"  style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Effect Date <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtDate" CssClass="form-control" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" placeholder="Effect Date" runat="server"></asp:TextBox>

                                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                    </asp:TextBoxWatermarkExtender>


                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDate">
                                                    </asp:CalendarExtender>

                                                </div>
                                                <div class="col-lg-2">
                                                    <label class="control-label">Notice Description<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtDesc" CssClass="form-control" placeholder="Notice Description" runat="server"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row"  style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Secured<span class="required">*</span></label>
                                                </div>

                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="RdtSecured" runat="server" RepeatDirection="Horizontal" Style="padding:5px">
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Charges<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCharges" CssClass="form-control" placeholder="Charges" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row"  style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Taxes<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTax" CssClass="form-control" placeholder="Taxes" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row"  style="margin: 7px 0 7px 0" align="center">
                                            <div class="col-lg-12" align="center">
                                                <div class="col-md-12">
                                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn blue" OnClientClick="Javascript:return Validate();" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="table-scrollable" id="DIV_GRID" runat="server">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:GridView ID="GrdNotice" runat="server" 
                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            EditRowStyle-BackColor="#FFFF99" 
                                                            PagerStyle-CssClass="pgr" Width="80%">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Effect Date" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%# Eval("EffectDate","{0:dd/MM/yyyy}") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Notice desc" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%# Eval("Notice_Desc") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Secured" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%# Eval("Secured") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Charges" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%# Eval("Charges") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Taxes" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <%# Eval("Taxes") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle CssClass="pgr" />
                                                            <HeaderStyle BackColor="#66FF99" HorizontalAlign="Center" />
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

