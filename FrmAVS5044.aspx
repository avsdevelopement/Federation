<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmAVS5044.aspx.cs" Inherits="FrmAVS5044" %>

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
    <script>
        function FromDateCheck(obj) {
            debugger;
            var FrSancDate = document.getElementById('<%=txtFDate.ClientID%>').value || 0;
            var Todate = document.getElementById('<%=txtTDate.ClientID%>').value;

            var frdate = FrSancDate.substring(0, 2);
            var frmonth = FrSancDate.substring(3, 5);
            var fryear = FrSancDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (frmyDate > wmyDate) {
                message = 'From date not allow greter than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFDate.ClientID %>').value = "";
                document.getElementById('<%=txtTDate.ClientID%>').focus();
                return false;
            }
        }

        function ToDateCheck(obj) {
            debugger;
            var FrSancDate = document.getElementById('<%=txtFDate.ClientID%>').value || 0;
            var ToSancDate = document.getElementById('<%=txtTDate.ClientID%>').value || 0;
             
             var frdate = FrSancDate.substring(0, 2);
             var frmonth = FrSancDate.substring(3, 5);
             var fryear = FrSancDate.substring(6, 10);
             var frmyDate = new Date(fryear, frmonth - 1, frdate);

             var todate = ToSancDate.substring(0, 2);
             var tomonth = ToSancDate.substring(3, 5);
             var toyear = ToSancDate.substring(6, 10);
             var tomyDate = new Date(toyear, tomonth - 1, todate);
                         
             if (tomyDate < frmyDate) {
                message = 'Enter to Sanction date greter than from sanction date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTDate.ClientID %>').value = "";
                document.getElementById('<%=txtTDate.ClientID%>').focus();
                return false;
            }
    }
    </script>
    <script>
        function IsValid() {
            debugger;
            var txtProdCode = document.getElementById('<%=txtProdCode.ClientID%>').value;

            var txtFDate = document.getElementById('<%=txtFDate.ClientID%>').value;
            var txtTDate = document.getElementById('<%=txtTDate.ClientID%>').value;


            var message = '';

            if (txtProdCode == "") {
                message = 'Please Enter Product Code ...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtProdCode.ClientID%>').focus();
                return false;
            }
            if (txtFDate == "") {
                message = 'Please Enter Fdate...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFDate.ClientID%>').focus();
                return false;
            }
            if (txtTDate == "") {
                message = 'Please Enter TDAte...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTDate.ClientID%>').focus();
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
                        Loan Interest Certificate
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div id="Brcd" runat="server" class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch Code <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtbrcd" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtbrcd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtbrname" CssClass="form-control" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divProd" runat="server" class="row" style="margin: 7px 0 7px 0" visible="false">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Code <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtProdCode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtProdCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtProdName" CssClass="form-control" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtProdName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetGlName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                         <div id="dATE" runat="server" class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Date <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtFDate" AutoPostBack="true"  onkeyup="FormatIt(this)" OnTextChanged="txtFDate_TextChanged" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-4">
                                                    <label class="control-label col-md-4">To Date <span class="required">*</span></label>
                                                    <div class="col-lg-6">
                                                        <asp:TextBox ID="txtTDate" AutoPostBack="true" OnTextChanged="txtTDate_TextChanged" onkeyup="FormatIt(this)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtTDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="Acno" runat="server" class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <%--  <label class="control-label col-md-2">AccNo <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtaccno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtaccname" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtaccname_TextChanged"></asp:TextBox>
                                                    <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="Autoaccname4" runat="server" TargetControlID="txtaccname"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4"
                                                            ServiceMethod="GetAccName">
                                                        </asp:AutoCompleteExtender>
                                                </div>--%>
                                                <label class="control-label col-md-2">Customer No<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtcstno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtcstno_TextChanged" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtname_TextChanged" TabIndex="4"></asp:TextBox>
                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autocustname" runat="server" TargetControlID="txtname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetCustNames" CompletionListElementID="CustList3">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-2">Outward No: <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtOutNo" CssClass="form-control" style="text-transform:uppercase" runat="server" AutoPostBack="true" OnTextChanged="TxtOutNo_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn blue" Text="Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return IsValid();" Visible="false" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="btnExit_Click"/>
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
    </div>
    <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdLogDetails" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="OUTWARD NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="OUTNO" runat="server" Text='<%# Eval("OUTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SUBGLCODE">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACCNO">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
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
    </div>
    <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdCustDetails" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BRCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SUBGLCODE">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACCNO">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPrint" runat="server" CommandArgument='<%#Eval("SUBGLCODE")+"_"+Eval("ACCNO")%>' CommandName="select" OnClick="lnkPrint_Click" class="glyphicon glyphicon-check"  OnClientClick="Javascript:return IsValid();"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Accurued">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAccured" runat="server" CommandArgument='<%#Eval("SUBGLCODE")+"_"+Eval("ACCNO")%>' CommandName="select" OnClick="LnkAccured_Click" class="glyphicon glyphicon-check"  OnClientClick="Javascript:return IsValid();"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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
    </div>
</asp:Content>


