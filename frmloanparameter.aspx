<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="frmloanparameter.aspx.cs" Inherits="frmloanparameter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>

    <script>

        function ConfirmBeforeDelete() {           
         

            var submitBtnText = document.getElementById('<%=btnSubmit.ClientID%>').value;
            var prdCodeName = document.getElementById('<%=TXTPRODUCTNAME.ClientID%>').value;
            var loanCategory = document.getElementById('<%=txtoirgl.ClientID%>').value;


           // alert(submitBtnText);

            if (submitBtnText.toUpperCase() == "DELETE") {

                var result = confirm("Do You Want To Delete Records?");               
                if (result == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                if (prdCodeName.trim() == "") {
                    alert("Enter Product Name");
                    return false;
                }


                else if (loanCategory.trim() == "") {
                    alert("Enter Loan Category");
                    return false;
                }


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
                        Loan Parameter
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                      <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills" style="border-bottom: 1px solid #999; padding-bottom: 10px;">

                                                    <li>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick= "lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkModify" runat="server" Text="a" class="btn btn-default" OnClick= "lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Modify</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick= "lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Delete</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick= "lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                                    </li>
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1">Product Code</label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtproductcode" Width="80" AutoPostBack="true" TabIndex="1" CssClass="form-control " onkeypress="javascript:return isNumber(event)"  runat="server" OnTextChanged="txtproductcode_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TXTPRODUCTNAME" CssClass="form-control " TabIndex="1"  PlaceHolder="product Name" runat="server"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Loan Category: </label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtoirgl" CssClass="form-control "  TabIndex="1" AutoPostBack="true" PlaceHolder="Loan Category" runat="server" OnTextChanged="txtoirgl_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                  <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">

                                        <label class="control-label col-md-1">Rate</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtrateint" CssClass="form-control " TabIndex="1" onkeypress="javascript:return isNumber(event)" PlaceHolder="rate of int" runat="server"></asp:TextBox>
                                        </div>
                                        
                                        <label class="control-label col-md-4">Period : </label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtperiod" CssClass="form-control "  TabIndex="1" onkeypress="javascript:return isNumber (event)" PlaceHolder="Period" runat="server" ></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                  <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">

                                        <label class="control-label col-md-1">PPL</label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtintre" Width="80" CssClass="form-control " TabIndex="1" onkeypress="javascript:return isNumber(event)" PlaceHolder="product code" runat="server" OnTextChanged="txtproductcode_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtintname" CssClass="form-control " TabIndex="1" PlaceHolder="product Name" runat="server"></asp:TextBox>
                                        </div>

                                         <label class="control-label col-md-2">Other Charges</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtOtherCHarges" CssClass="form-control " TabIndex="1" onkeypress="javascript:return isNumber(event)" PlaceHolder="Other Charges" runat="server"></asp:TextBox>
                                        </div>
                                      <%--  <label class="control-label col-md-1">Loan type</label>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlloantype" runat="server" TabIndex="1" CssClass="form-control " ToolTip="1039">
                                            </asp:DropDownList>

                                        </div>--%>

                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">

                                        <label class="control-label col-md-1">IntCal</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtIntCal" CssClass="form-control " TabIndex="1" PlaceHolder="Int Cal Type" runat="server"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-4">Loan Limit : </label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtloanlimit" CssClass="form-control " TabIndex="1" onkeypress="javascript:return isNumber (event)" PlaceHolder="Loan Limit" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                              
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">

                                        <label class="control-label col-md-1">Pen Int:</label>
                                             <div class="col-md-2">
                                            <asp:TextBox ID="TXtpenInt" CssClass="form-control " TabIndex="1" onkeypress="javascript:return isNumber (event)" PlaceHolder="Penal Int" runat="server"></asp:TextBox>
                                        </div>
                                    
                                        
                                        <label class="control-label col-md-4">Effect Date</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txteffectivedate" CssClass="form-control " TabIndex="1" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="txteffectivedateWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txteffectivedate">
                                            </asp:TextBoxWatermarkExtender>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txteffectivedate">
                                            </asp:CalendarExtender>
                                        </div>

                                    </div>
                                </div>
                              

                                <div class="row" style="margin: 7px 0 7px 0" >
                                    <div class="col-lg-12">

                                        <label class="control-label col-md-1">Int App </label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtIntApp" CssClass="form-control " TabIndex="1"  onkeypress="javascript:return isNumber(event)" PlaceHolder="Int App " runat="server" ></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-4">Secured</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtSecured" CssClass="form-control " TabIndex="1" AutoPostBack="true"  onkeypress="javascript:return isNumber(event)" PlaceHolder="Secured " runat="server" OnTextChanged="txtacchead_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div >
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click1" OnClientClick="Javascript:return ConfirmBeforeDelete();" />
                                    <asp:Button ID="btnclear" runat="server" CssClass="btn blue" Text="clear" OnClick= "btnclear_Click" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-scrollable" style="border: none">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:GridView ID="grdloanpara" runat="server" AllowPaging="True"
                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                            EditRowStyle-BackColor="#FFFF99"
                                                            OnPageIndexChanging="grdloanpara_PageIndexChanging"
                                                            PageIndex="10" PageSize="25"
                                                            PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="grdloanpara_SelectedIndexChanged">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Select" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkSelect" runat="server" Text="SELECT" CommandArgument='<%#Eval("LOANGLCODE")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="LOANGLCODE" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LOANCODE" runat="server" Text='<%# Eval("LOANGLCODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="LOAN NAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LOANTYPE" runat="server" Text='<%# Eval("LOANTYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="INTREC">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="INTREC" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="LOANLIMIT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LOANLIMIT" runat="server" Text='<%# Eval("LOANLIMIT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="RATE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ROI" runat="server" Text='<%# Eval("ROI") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="PENALINT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="PENALINT" runat="server" Text='<%# Eval("PENALINT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="intsub" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="intsub" runat="server" Text='<%# Eval("intsub") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Edit" Visible="true">
                                                                       <ItemTemplate>
                                                          <asp:LinkButton ID="lnkedit" runat="server"  OnClick=  "lnkedit_Click" CommandArgument='<%#Eval("LOANGLCODE")+","+ Eval("intsub")%>'  Text="MODIFY" CommandName="SELECT"  class="glyphicon glyphicon"></asp:LinkButton>
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

                        </div>
                    </div>
                </div>
            </div>
            <!--</form>-->
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

