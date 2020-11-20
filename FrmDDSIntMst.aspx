<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDDSIntMst.aspx.cs" Inherits="FrmDDSIntMst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {
            var message = '';
            var membtype = document.getElementById('<% =DDLMembType.ClientID%>').value;
             if (membtype == "0") {
                 //alert("Please select Member type");
                 message = 'Please select Member type....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =DDLMembType.ClientID%>').focus();
                return false;
            }

            var membtype = document.getElementById('<% =txtproid.ClientID%>').value;
             if (membtype == "") {
                 //alert("Please enter product ID");
                 message = 'Please Enter product ID....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =txtproid.ClientID%>').focus();
                return false;
            }

            var membtype = document.getElementById('<% =txtproname.ClientID%>').value;
             if (membtype == "") {
                 //alert("Please enter product name");
                 message = 'Please Enter product name....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =txtproname.ClientID%>').focus();
                return false;
            }
            var membtype = document.getElementById('<% =ddlperiodtypefrm.ClientID%>').value;
             if (membtype == "0") {
                 // alert("Please select period to type");
                 message = 'Please select period to type....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =ddlperiodtypefrm.ClientID%>').focus();
                return false;
            }

            var membtype = document.getElementById('<% =txtperiodFrm.ClientID%>').value;
             if (membtype == "") {
                 //alert("Please enter period from");
                 message = 'Please Enter Period from....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =txtperiodFrm.ClientID%>').focus();
                return false;
            }


            var membtype = document.getElementById('<% =txtperiodTo.ClientID%>').value;
             if (membtype == "") {
                 //alert("Please enter period To");
                 message = 'Please Enter period To....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =txtperiodTo.ClientID%>').focus();
                return false;
            }

            var effectdate = document.getElementById('<% =txteffectdate.ClientID%>').value;
             if (effectdate == "") {
                 //alert("Please Enter Rate");
                 message = 'Please Enter Effect date....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =txteffectdate.ClientID%>').focus();
                return false;
            }

            var membtype = document.getElementById('<% =txtRate.ClientID%>').value;
             if (membtype == "") {
                 //alert("Please Enter Rate");
                 message = 'Please Enter Rate....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =txtRate.ClientID%>').focus();
                return false;
            }


            var Penalty = document.getElementById('<% =txtPenalty.ClientID%>').value;
             if (Penalty == "") {
                 //alert("Please Enter Rate");
                 message = 'Please Enter Penalty....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =txtPenalty.ClientID%>').focus();
                return false;
            }

            var AftrMatDate = document.getElementById('<% =TxtAftrMat.ClientID%>').value;
             if (AftrMatDate == "") {
                 //alert("Please Enter Rate");
                 message = 'Please Enter After Maturity Rate....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<% =TxtAftrMat.ClientID%>').focus();
                return false;
            }

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        DDS Interest Master
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">
                    <%--Div 1--%>
                    <div id="Depositdiv" runat="server">
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Member Type : <span class="required">*</span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DDLMembType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Deposit GlCode: <span class="required">*</span></label>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtproid" CssClass="form-control" runat="server" OnTextChanged="txtproid_TextChanged" onkeypress="javascript:return isNumber (event)"  AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtproname" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtproname_TextChanged1"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtproname"
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

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Period Type<span class="required">*</span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlperiodtypefrm" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Y">years</asp:ListItem>
                                        <asp:ListItem Value="M">Months</asp:ListItem>
                                        <asp:ListItem Value="D">Days</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtperiodFrm" CssClass="form-control" onkeypress="javascript:return isNumber (event)"  placeholder="From" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtperiodTo" CssClass="form-control"  onkeypress="javascript:return isNumber (event)"  placeholder="To" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>



                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Effective date : <span class="required">*</span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txteffectdate" onkeyup="FormatIt(this)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txteffectdate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txteffectdate">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Rate : <span class="required">*</span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtRate" CssClass="form-control" onkeypress="javascript:return isNumber (event)"  runat="server"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-1">Penalty:<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtPenalty" CssClass="form-control" onkeypress="javascript:return isNumber (event)"  runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">After Maturity Rate : <span class="required">*</span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtAftrMat" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                            <div class="col-lg-12">
                                <div class="col-md-6">

                                    <asp:Button ID="Btnsubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="Btnsubmit_Click" OnClientClick="javascript:return validate();" />
                                    <asp:Button ID="BtnModify" runat="server" Text="Modify" CssClass="btn btn-success" OnClick="BtnModify_Click" OnClientClick="javascript:return validate();" Visible="false" />
                                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="btn btn-success" OnClick="BtnDelete_Click" OnClientClick="javascript:return validate();" Visible="false" />
                                    <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" />
                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdIntrstMaster" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" AllowPaging="True"
                                    EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" OnPageIndexChanging="grdIntrstMaster_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Srno" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Effect Date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="EFFECTDT" runat="server" Text='<%# Eval("EFFECTDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cust Type">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTTYPE" runat="server" Text='<%# Eval("CUSTTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DepositGl">
                                            <ItemTemplate>
                                                <asp:Label ID="DEPOSITGL" runat="server" Text='<%# Eval("DEPOSITGL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Period Type">
                                            <ItemTemplate>
                                                <asp:Label ID="PERIODTYPE" runat="server" Text='<%# Eval("PERIODTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Period From">
                                            <ItemTemplate>
                                                <asp:Label ID="PERIODFROM" runat="server" Text='<%# Eval("PERIODFROM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PERIOD TO " Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PERIODTO" runat="server" Text='<%# Eval("PERIODTO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="RATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Penalty" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PENALTY" runat="server" Text='<%# Eval("PENALTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="After Mat Rate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AFTERMATROI" runat="server" Text='<%# Eval("AFTERMATROI") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Add New" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAdd" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkAdd_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkDelete_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
</asp:Content>

