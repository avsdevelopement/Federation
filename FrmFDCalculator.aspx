<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmFDCalculator.aspx.cs" Inherits="FrmFDCalculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function IsValide() {
            var DdlPeroid = document.getElementById('<%=DdlDuartion.ClientID%>').value;
            var PrinciAmount = document.getElementById('<%=TxtPrinciAmount.ClientID%>').value;
            var ROI = document.getElementById('<%=TxtROI.ClientID%>').value;
            


            var message = '';

            if (DdlPeroid == "0") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Customer number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=DdlDuartion.ClientID %>').focus();
                return false;

            }
            if (PrinciAmount == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Principal Amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtPrinciAmount.ClientID %>').focus();
                return false;
            }


            if (ROI == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Rate of Interest....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtROI.ClientID %>').focus();
                    return false;
                }


          


        }
    </script>
     <script>
         function isNumber(evt) {
             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                 return false;
             }
             return true;
         }
    </script>
    <script>
        
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
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Fixed Deposit Calculator
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                          <%--  <div class="col-md-12">
                                                <asp:RadioButton ID="RdbCompoundQ" Text="Compound Quaterly" GroupName="GN_Cmp" runat="server" />
                                                <asp:RadioButton ID="RdbCompoundHY" Text="Compound Half Yearly" GroupName="GN_Cmp" runat="server" />
                                                <asp:RadioButton ID="RdbCompoundM" Text="Compound Monthly" GroupName="GN_Cmp" runat="server" />

                                            </div>--%>

                                            <div class="col-lg-12">
                                                <br />
                                                <label class="control-label col-md-2">Principal Amt<span class="required">*</span></label>

                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPrinciAmount" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Rate of Interest<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtROI" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Type<span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlDuartion" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="M">Months</asp:ListItem>
                                                        <asp:ListItem Value="D">Days</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="control-label col-md-2">Period<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPeriod" AutoPostBack="true" onkeypress="return isNumber(event)" OnTextChanged="TxtPeriod_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                
                                             </div>
                                       </div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Eff. Date<span class="required">* </span></label>
                                                <div class="col-md-2">
                                                      <asp:TextBox ID="TxtEffectDate" onkeyup="FormatIt(this)" OnTextChanged="TxtEffectDate_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Mat. Date<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtMaturityDate" onkeyup="FormatIt(this)" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                
                                             </div>
                                       </div>

                                        <div class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12">
                                                
                                                <label class="control-label col-md-2">Total Interest Amount</label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtTotalIntAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Effective Yeild</label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtEffectYeild" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Total Maturity Amount</label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtTotalMatAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>

                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                       <%-- <asp:Button ID="Submit" runat="server" Text="Submit" Visible="false" CssClass="btn btn-primary" OnClick="Submit_Click" OnClientClick="Javascript:return IsValide();" />--%>
                                        <asp:Button ID="Btn_Details" runat="server" Text="Details" CssClass="btn btn-primary" OnClick="Btn_Details_Click" />
                                        <asp:Button ID="ClearAll" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="ClearAll_Click" />
                                        &nbsp;<asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="Grd_Details" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%"
                                    >
                                    <Columns>
                                        <%--Id	TypeCalc	Interest	Maturity	Rate--%>
                                        <asp:TemplateField HeaderText="SR NO." Visible="true" HeaderStyle-BackColor="#99ff99">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TYPE" HeaderStyle-BackColor="#99ff99">
                                            <ItemTemplate>
                                                <asp:Label ID="TYPE" runat="server" Text='<%# Eval("TypeCalc") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="INTEREST" HeaderStyle-BackColor="#99ff99">
                                            <ItemTemplate>
                                                <asp:Label ID="INTEREST" runat="server" Text='<%# Eval("Interest") %>'></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MATURITY" Visible="true" HeaderStyle-BackColor="#99ff99">
                                            <ItemTemplate>
                                                <asp:Label ID="MATURITI" runat="server" Text='<%# Eval("Maturity") %>'></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="RATE" Visible="true" HeaderStyle-BackColor="#99ff99">
                                            <ItemTemplate>
                                                <asp:Label ID="RATE" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle CssClass="Th" HorizontalAlign="Right" />
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
    <!--</form>-->

</asp:Content>

