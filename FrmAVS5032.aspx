<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5032.aspx.cs" Inherits="FrmAVS5032" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
    
       <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
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
        function FunEffectDate(obj) {
            debugger;
            var Duedate = document.getElementById('<%=TxtEffectDate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=hdEntrydate.ClientID%>').value;

            var frdate = Duedate.substring(0, 2);
            var frmonth = Duedate.substring(3, 5);
            var fryear = Duedate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);
           var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (frmyDate > wmyDate) {
                message = 'Effect Date cannot be  greater than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtEffectDate.ClientID %>').value = "";
               document.getElementById('<%=TxtEffectDate.ClientID%>').focus();
                return false;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        Loan Rate Parameter
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">
                    <%--Div 1--%>
               
     <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Product Code <span class="required">*</span></label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="Txtprdcode" PlaceHolder="PNo" MaxLength="8" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="Txtprdcode_TextChanged" AutoPostBack="true" TabIndex="1" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="Txtprdname" CssClass="form-control"  PlaceHolder="PrdName" OnTextChanged="Txtprdname_TextChanged" AutoPostBack="true" TabIndex="2" runat="server"></asp:TextBox>
                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="Txtprdname"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="getglname" CompletionListElementID="CustList">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                        

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">From Amount <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtFAmt" PlaceHolder="FromAmt" MaxLength="10" onkeypress="javascript:return isNumber (event)" OnTextchanged="TxtFAmt_TextChanged" CssClass="form-control" TabIndex="3" AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">To Amount <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtTAmt" PlaceHolder="ToAmt" MaxLength="10"  onkeypress="javascript:return isNumber (event)" OnTextchanged="TxtTAmt_TextChanged" CssClass="form-control" TabIndex="4"  AutoPostBack="true" runat="server"></asp:TextBox>
                                    </div>
                                 
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Rate Of Interest<span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtROI"  PlaceHolder="ROI" MaxLength="4" onkeypress="javascript:return isNumber (event)" CssClass="form-control" TabIndex="5" runat="server"></asp:TextBox>
                                    </div>
                                      <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Effect Date <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtEffectDate" PlaceHolder="Effect date" MaxLength="10" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber (event)" CssClass="form-control" TabIndex="6"  OnBlur="FunEffectDate(this);" runat="server"></asp:TextBox>
                                         <asp:TextBoxWatermarkExtender ID="TxtEffectDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtEffectDate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="TxtEffectDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtEffectDate">
                                </asp:CalendarExtender>
                                    </div>
                                  </div>
                            </div>
                     <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Period <span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtPeriod" PlaceHolder="Period" MaxLength="5" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" TabIndex="7" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Penal Int<span class="required">*</span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtPenalInt" PlaceHolder="Penal Int" MaxLength="5"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" TabIndex="8"  runat="server"></asp:TextBox>
                                    </div>
                                 
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-4 col-md-9">
                                        <asp:Button ID="BtnSubmit" runat="server" Text="Create" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" TabIndex="9" OnClientClick="javascript:return validate();" />
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="BtnClear_Click" TabIndex="10" />
                                        <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="BtnExit_Click" TabIndex="11" OnClientClick="javascript:return validate();" />
                                        <asp:Button ID="BtnNew" runat="server" Text="AddNew" CssClass="btn btn-primary" OnClick="BtnNew_Click" TabIndex="12"  Visible="false"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                 
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdEntrydate" runat="server" value=""  />
     <div class="row" id="Div_grid" runat="server">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdDisp" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333"  PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%"  ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                      <asp:TemplateField HeaderText="ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subglcode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Subglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FromAmount" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="FromAmt" runat="server" Text='<%# Eval("FromAmt ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      

                                            <asp:TemplateField HeaderText="ToAmount" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ToAmt" runat="server" Text='<%# Eval("ToAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="ROI" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ROI" runat="server" Text='<%# Eval("ROI") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Period" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Period" runat="server" Text='<%# Eval("Period") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="PenalInt" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PenalInt" runat="server" Text='<%# Eval("PenalInt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkModify_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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

