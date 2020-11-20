<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmShareApp.aspx.cs" Inherits="FrmShareApp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
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

    <script type="text/javascript">
        //For Check Disb Date.. Not allow grether than working date
        function DisbDate() {
            debugger;
            var MaxDisbDate = document.getElementById('<%=MaxDisbDate.ClientID%>').value;
            var txtDisbDate = document.getElementById('<%=TxtDOB1.ClientID%>').value;

            var date = MaxDisbDate.substring(0, 2);
            var month = MaxDisbDate.substring(3, 5);
            var year = MaxDisbDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var date1 = txtDisbDate.substring(0, 2);
            var month1 = txtDisbDate.substring(3, 5);
            var year1 = txtDisbDate.substring(6, 10);

            var myDate1 = new Date(year1, month1 - 1, date1);

            if (myDate1 > myDate) {
                message = 'Not Allow For Future Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtDOB1.ClientID %>').focus();
                ClearDate();
                return false;
            }
        }

        function ClearDate() {
            document.getElementById("<%= TxtDOB1.ClientID %>").value = document.getElementById('<%=MaxDisbDate.ClientID%>').value;
        }
    </script>

    <script type="text/javascript">
        //For Check Disb Date.. Not allow grether than working date
        function Checkdate() {
            debugger;
            var MaxDisbDate = document.getElementById('<%=MaxDisbDate.ClientID%>').value;
            var txtDisbDate = document.getElementById('<%=TxtDOB2.ClientID%>').value;

            var date = MaxDisbDate.substring(0, 2);
            var month = MaxDisbDate.substring(3, 5);
            var year = MaxDisbDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var date1 = txtDisbDate.substring(0, 2);
            var month1 = txtDisbDate.substring(3, 5);
            var year1 = txtDisbDate.substring(6, 10);

            var myDate1 = new Date(year1, month1 - 1, date1);

            if (myDate1 > myDate) {
                message = 'Not Allow For Future Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtDOB2.ClientID %>').focus();
                ClerDate();
                return false;
            }
        }

        function ClerDate() {
            document.getElementById("<%= TxtDOB2.ClientID %>").value = document.getElementById('<%=MaxDisbDate.ClientID%>').value;
        }
    </script>

    <script type="text/javascript">
        function IsValide() {
            debugger;
            var ddlAppType = document.getElementById('<%=ddlAppType.ClientID%>').value;
            var txtMemNo = document.getElementById('<%=txtMemNo.ClientID%>').value;
            var txtcustno = document.getElementById('<%=txtcustno.ClientID%>').value;
            var txtNoOfShr = document.getElementById('<%=txtNoOfShr.ClientID%>').value;
            var txtShrValue = document.getElementById('<%=txtShrValue.ClientID%>').value;
            var txtEntFee = document.getElementById('<%=txtEntFee.ClientID%>').value;
            var txtOther1 = document.getElementById('<%=txtOther1.ClientID%>').value;
            var txtOther2 = document.getElementById('<%=txtOther2.ClientID%>').value;
            var ddlPayType = document.getElementById('<%=ddlPayType.ClientID%>').value;
            var txtTotAmount = document.getElementById('<%=txtTotAmount.ClientID%>').value || 0;
            var message = '';


            if (ddlAppType == "0") {
                message = 'Select application type first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=ddlAppType.ClientID %>').focus();
                return false;
            }

            if (ddlAppType == "2") {
                if (txtMemNo == "") {
                    message = 'Enter Member Number...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    $('#<%=txtMemNo.ClientID %>').focus();
                    return false;
                }
            }

            if (txtcustno == "") {
                message = 'Enter Customer Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtcustno.ClientID %>').focus();
                return false;
            }

            if (txtNoOfShr == "") {
                message = 'Enter No Of Shares...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtNoOfShr.ClientID %>').focus();
                return false;
            }

            if (txtShrValue == "") {
                message = 'Enter Share Value...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtShrValue.ClientID %>').focus();
                return false;
            }

            if (txtEntFee == "") {
                message = 'Enter Entry Fees...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtEntFee.ClientID %>').focus();
                return false;
            }

            if (ddlPayType == "0") {
                message = 'Enter Payment details first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=ddlAppType.ClientID %>').focus();
                return false;
            }

            <%--if (ddlPayType == "2") {
                var EntryBal = parseFloat(txtTotAmount);
                var avlblBal = parseFloat(txtBalance);

                if (EntryBal > avlblBal) {
                    message = 'Balance is Not Sufficient...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    $('#<%=TxtAccNo1.ClientID %>').focus();
                    return false;
                }
            }

            if (ddlPayType == "4") {
                var EntryBal = parseFloat(txtTotAmount);
                var avlblBal = parseFloat(txtBalance);

                if (EntryBal > avlblBal) {
                    message = 'Balance is Not Sufficient...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    $('#<%=TxtAccNo1.ClientID %>').focus();
                    return false;
                }
            }--%>
        }
    </script>

    <script type="text/javascript">
        function CalShrAmt(obj) {
            debugger;
            var txtNoOfShr = document.getElementById('<%=txtNoOfShr.ClientID%>').value || 0;
            var txtShrValue = document.getElementById('<%=txtShrValue.ClientID%>').value || 0;

            var Result = (parseInt(txtNoOfShr) * parseFloat(txtShrValue));
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTotShr.ClientID %>').value = Result;
            }
        }

        function CalAmt(obj) {
            debugger;
            var txtNoOfShr = document.getElementById('<%=txtNoOfShr.ClientID%>').value || 0;
            var txtShrValue = document.getElementById('<%=txtShrValue.ClientID%>').value || 0;
            var txtSavFee = document.getElementById('<%=txtSavFee.ClientID%>').value || 0;
            var txtEntFee = document.getElementById('<%=txtEntFee.ClientID%>').value || 0;
            var txtOther1 = document.getElementById('<%=txtOther1.ClientID%>').value || 0;
            var txtOther2 = document.getElementById('<%=txtOther2.ClientID%>').value || 0;
            var txtOther3 = document.getElementById('<%=txtOther3.ClientID%>').value || 0;
            var txtOther4 = document.getElementById('<%=txtOther4.ClientID%>').value || 0;
            var txtOther5 = document.getElementById('<%=txtOther5.ClientID%>').value || 0;
            var MemWelFee = document.getElementById('<%=txtMemWelFee.ClientID%>').value || 0;
            var SerChrFee = document.getElementById('<%=txtSerChrFee.ClientID%>').value || 0;

            var Result = ((parseInt(txtNoOfShr) * parseFloat(txtShrValue)) + parseInt(txtSavFee) + parseInt(txtEntFee) + parseInt(txtOther1) + parseInt(txtOther2) + parseInt(txtOther3) + parseInt(txtOther4) + parseInt(txtOther5) + parseInt(MemWelFee) + parseInt(SerChrFee));
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTotAmount.ClientID %>').value = Result;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                 Application Details 
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills">
                                                    <li>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Delete</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="VW" class="btn btn-default" OnClick="lnkView_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>View</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkShrAllot" runat="server" Text="VW" class="btn btn-default" OnClick="lnkShrAllot_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Share Allotment</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkShrCancel" runat="server" Text="CL" class="btn btn-default" OnClick="lnkShrCancel_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Cancel Application</asp:LinkButton>
                                                    </li>
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="divShareApp" runat="server">

                                                <div class="col-md-12">
                                                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 100px; overflow-x: auto; overflow-y: auto">
                                                        <table class="table table-striped table-bordered table-hover" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        <asp:GridView ID="grdMaster" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="False"
                                                                            EditRowStyle-BackColor="#FFFF99" Width="100%">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("id")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="BRCD" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Cust No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="App No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAppNo" runat="server" Text='<%# Eval("AppNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                               <%-- <asp:TemplateField HeaderText="Share Value">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSHRValue" runat="server" Text='<%# Eval("SHRValue") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="No Of Shares" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNoOfSHR" runat="server" Text='<%# Eval("NoOfSHR") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>

                                                                                <asp:TemplateField HeaderText="Total Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("TotShrValue") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Entry Fee">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEntFee" runat="server" Text='<%# Eval("EntFee") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Other Fee1">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblOther1" runat="server" Text='<%# Eval("Other5") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                               <%-- <asp:TemplateField HeaderText="Other Fee2" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblOther2" runat="server" Text='<%# Eval("Other2") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>

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

                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Application Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                           
                                                                 <label class="control-label col-md-2">Society Type <span class="required">*</span></label>
                                                            
                                                            <div class="col-lg-2">
                                                                <asp:DropDownList ID="ddlAppType" OnSelectedIndexChanged="ApplType_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                                                   
                                                                </asp:DropDownList>
                                                            </div>

                                                            
                                                            <div class="col-md-4">
                                                                <div class="input-icon">
                                                                    <i class="fa fa-search"></i>
                                                                    <asp:TextBox ID="txtcusname" runat="server" placeholder="Search Customer Name" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtcusname_TextChanged"></asp:TextBox>
                                                                   <%-- <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtcusname" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetCustNames">
                                                                    </asp:AutoCompleteExtender>--%>
                                                                </div>
                                                            </div>
                                                             <div class="col-lg-2">
                                                                <asp:TextBox ID="txtcustno" Visible="false"   placeholder="Customer Number" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtcustno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                           <%-- --%>
                                                            <%--<div class="col-md-3">
                                                                <div class="input-icon">
                                                                    <i class="fa fa-search"></i>
                                                                    <asp:TextBox ID="txtShareAccName" Placeholder="Search Product Name" Enabled="false" OnTextChanged="txtShareAccName_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="AutoSuspAccName" runat="server" TargetControlID="txtShareAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetSuspGlName">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                       
                                                                <label class="control-label col-md-2">Membership No </label>
                                                         
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMemNo" OnTextChanged="Membership_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                            </div>
                                                        
                                                              <label class="control-label col-md-2">Total Member No<span class="required">*</span></label>
                                                          
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtShareAccNo" OnTextChanged="txtShareAccNo_TextChanged" AutoPostBack="true"  runat="server" CssClass="form-control" />
                                                            </div>
                                                           
                                                            <div class="col-md-1">
                                                                <label class="control-label"> <span class="required"></span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtAppNo" Visible="false" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            
                                                        </div>
                                                    </div>
                                                       <div id="Div3" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                           
                                                              <label class="control-label col-md-2">Address <span class="required">*</span></label>
                                                            
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" TabIndex="6"  placeholder="Enter Address" runat="server"></asp:TextBox>
                                                            </div>
                                                          
                                                              <label class="control-label col-md-1">State<span class="required">*</span></label>
                                                           
                                                            <div class="col-lg-2">
                                                                    <asp:DropDownList ID="ddstate" runat="server" CssClass="form-control" TabIndex="7" onblur="BindAddress()" OnSelectedIndexChanged="ddstate_SelectedIndexChanged"  AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                                                                </div>
                                                                   <label class="control-label col-md-2">District<span class="required">*</span> </label>
                                                                <div class="col-lg-2">
                                                                    <asp:DropDownList ID="dddistrict" runat="server" OnSelectedIndexChanged="dddistrict_SelectedIndexChanged" TabIndex="8" CssClass="form-control" onblur="BindAddress()"  AutoPostBack="true" ></asp:DropDownList>
                                                               
                                                        </div>
                                                              
                                                            </div>
                                                    </div>

                                                         <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                             
                                                                <label class="control-label col-md-2">Taluka/City <span class="required">*</span> </label>
                                                                <div class="col-lg-2">
                                                                    <asp:DropDownList ID="ddtaluka" runat="server" OnSelectedIndexChanged="ddtaluka_SelectedIndexChanged" CssClass="form-control"  onkeypress="javascript:return isAddress (event)" AutoPostBack="true" ></asp:DropDownList>
                                                                   </div>
                                                                <label class="control-label col-md-2">Ward:</label>
                                                            <div class="col-md-2">
                                                                <%-- <asp:TextBox ID="txtWard" Placeholder="Ward" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="10" AutoPostBack="true"></asp:TextBox>
                                                                --%>
                                                                <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control"  TabIndex="20"></asp:DropDownList>
                                                            </div>
                                                                <label class="control-label col-md-2">PIN Code<span class="required">*</span></label>
                                                                <div class="col-lg-2">
                                                                    <asp:TextBox ID="txtpin" placeholder="PIN CODE" MaxLength="6" runat="server" onblur="BindAddress()" CssClass="form-control" pattern="[1-9][0-9]{5}" onkeypress="javascript:return isNumber (event)" TabIndex="36"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                   
                                                    <div id="Div5" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                           
                                                               <label class="control-label col-md-2">Mobile No<span class="required">*</span></label>
                                                        
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMob" MaxLength="10" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" placeholder="Mobile No" runat="server"></asp:TextBox>
                                                            </div>
                                                            
                                                             <label class="control-label col-md-2">Email ID<span class="required">*</span></label>
                                                           
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtemailid" CssClass="form-control" placeholder="Email ID" runat="server"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-2">Permises Occupied<span class="required">*</span></label>
                                                 
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPermises"  AutoPostBack="true" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="--  Select  --" Value="0" />
                                                                    <asp:ListItem Text="YES" Value="1" />
                                                                    <asp:ListItem Text="No" Value="2" />
                                                                  
                                                                </asp:DropDownList>  


                                                            </div>
                                                        </div>
                                                    </div>

                                                 

                                                   

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                                 <label class="control-label col-md-2">Shares Amount <span class="required">*</span></label>
                                                      
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtShrValue" onkeypress="javascript:return isNumber (event)" onblur="CalAmt();CalShrAmt()" CssClass="form-control" placeholder="Shares Value" runat="server"></asp:TextBox>
                                                            </div>
                                                          
                                                                <label id="LblName1" runat="server" class="control-label col-md-2">Entrance Fee <span class="required">*</span></label>
                                                        
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtEntFee" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" placeholder="Entrance Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                             
                                                                <label id="LblName8" runat="server" class="control-label col-md-2">M.A Subscrption </label>
                                                      
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtOther5" onkeypress="javascript:return isNumber (event)"  value="0" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label id="LblName2" runat="server" class="control-label"></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtOther1" Visible="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" CssClass="form-control" placeholder="Welfare Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label id="LblName3" runat="server" class="control-label"> </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtOther2" Visible="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" value="0" CssClass="form-control" placeholder="Printing Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                     <div style="border: 1px solid #3598dc">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Commitee Details: </strong></div>
                                                        </div>
                                                    </div>

                                                   <div runat="server" id="div6" visible="true">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                
                                                                <label class="control-label col-md-2">Chairman Name:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtNamed" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" PlaceHolder=" Name" TabIndex="51" runat="server"></asp:TextBox>
                                                                </div>
                                                                  <label class="control-label col-md-1">Mobile No1:</label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtdMob1" CssClass="form-control" MaxLength="10" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" PlaceHolder="Mobile No" TabIndex="52" runat="server"></asp:TextBox>

                                                                </div>
                                                        </div>
                                                            </div>
                                                       </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">

                                                                 <label class="control-label col-md-2">Secretary Name:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtsecname" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" PlaceHolder=" Name" TabIndex="51" runat="server"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-1">Mobile No1:</label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtMobile" CssClass="form-control" MaxLength="10" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" PlaceHolder="Mobile No" TabIndex="52" runat="server"></asp:TextBox>

                                                                </div>
                                                               
                                                                


                                                            </div>
                                                        </div>
                                                    </div>
                                                       

                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Payment Mode <span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Transfer" runat="server">
                                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtProdType1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="input-icon">
                                                                        <i class="fa fa-search"></i>
                                                                        <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Acc No / Name:<span class="required"> *</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="input-icon">
                                                                        <i class="fa fa-search"></i>
                                                                        <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                                        <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label "><span class="required"></span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtBalance" Visible="false" CssClass="form-control" PlaceHolder="Balance" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divIntrument" runat="server">
                                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Instrument No:<span class="required"> *</span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" MaxLength="6" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Instrument Date:<span class="required"> *</span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divNarration" runat="server">
                                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Narration:</label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server" />
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Amount:</label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtAmount" Enabled="false" CssClass="form-control" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                                <div id="Div2" class="row"  runat="server">
                                                        <div class="col-lg-12">
                                                                <label class="control-label"><span class="required"></span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtTotShr" Visible="false" Enabled="false" CssClass="form-control" placeholder="Total Shares Value" runat="server"></asp:TextBox>
                                                            </div>
                                                             <div class="col-md-2">
                                                                <label class="control-label "> <span class="required"></span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtNoOfShr"  Visible="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt();CalShrAmt()" CssClass="form-control" placeholder="No Of Shares" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                               
                                                      

                                                     <div id="Div1" class="row" >
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label id="LblName0" runat="server"  Visible="false" class="control-label"> <span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtAccNo"  Visible="false" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtAccName" Visible="false" OnTextChanged="txtAccName_TextChanged" AutoPostBack="true" Placeholder="Account Name" runat="server" CssClass="form-control" />
                                                                <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtAccName"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="GetAccName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label id="LblName15"  runat="server"  class="control-label"></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtSavFee"  Visible="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" CssClass="form-control"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row"  >
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label id="LblName4" runat="server" class="control-label"> </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMemWelFee" visible="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" CssClass="form-control"  runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label id="LblName5" runat="server" class="control-label"> </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtSerChrFee" visible="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" value="0" CssClass="form-control"  runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label id="LblName6" runat="server" class="control-label"></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtOther3" visible="false" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" value="0" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Div4" class="row" visible="false" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label id="LblName7" runat="server" class="control-label">Mem Asst S.A Fees </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtOther4" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" value="0" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                         
                                                            <div class="col-md-2">
                                                                <label class="control-label">Total Amount<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtTotAmount" Enabled="false" CssClass="form-control" placeholder="Total Value" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Div7" class="row" visible="false"  runat="server">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Remark <span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtRemark" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Remark" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div style="border: 1px solid #3598dc" visible="false"  >
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"> </strong></div>
                                                        </div>
                                                    </div>

                                                    <div id="Div8" class="row"  runat="server" visible="false" >
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label"> </label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtNomName1" visible="false"  CssClass="form-control"  runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Div9" class="row" visible="false"  style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Date of Birth </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtDOB1" OnTextChanged="TxtDOB1_TextChanged" onkeyup="FormatIt(this); DisbDate()" onkeypress="javascript:return isNumber(event)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDOB1">
                                                                </asp:CalendarExtender>
                                                                <input type="hidden" id="MaxDisbDate" runat="server" value="" />
                                                            </div>
                                                            <div class="col-lg-1">
                                                                <asp:TextBox ID="TxtAge1" CssClass="form-control" placeholder="Age" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Relation </label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="ddlRelation1" Width="130px" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div style="border: 1px solid #3598dc" visible="false" >
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" visible="false"  style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"> </strong></div>
                                                        </div>
                                                    </div>

                                                    <div id="Div10" class="row"  runat="server" visible="false">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label"></label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtNomName2" CssClass="form-control" placeholder="Full Name Of Nominee 1" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Div11" class="row" visible="false"  style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Date of Birth </label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtDOB2" OnTextChanged="TxtDOB2_TextChanged" onkeyup="FormatIt(this); Checkdate()" onkeypress="javascript:return isNumber(event)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDOB2">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <div class="col-lg-1">
                                                                <asp:TextBox ID="TxtAge2" CssClass="form-control" placeholder="Age" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">Relation </label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="ddlRelation2" Width="130px" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                            <div id="divShareAllot" runat="server">

                                                <div class="row" style="margin: 5px 0 5px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-10">
                                                            <label class="control-label ">Additional Share Allotment Pending : </label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdAppDetails" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                                                        EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" Width="100%" EmptyDataText="No Remaining Appliaction for this branch">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Cust No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Member No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMemberNo" runat="server" Text='<%# Eval("MemberNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Customer Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="App No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAppNo" runat="server" Text='<%# Eval("AppNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Date" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="NoOfSHR">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoOfSHR" runat="server" Text='<%# Eval("NoOfSHR") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SHR Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotShrValue" runat="server" Text='<%# Eval("TotShrValue") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Enterence" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntFee" runat="server" Text='<%# Eval("EntFee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WelFare">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWelfare" runat="server" Text='<%# Eval("Other1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WelFare(Loan)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWelfareLoan" runat="server" Text='<%# Eval("MemberWelFee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Select" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkBox" runat="server" onclick="Check_Click(this)" />
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
                                    </div>

                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="Submit" runat="server" CssClass="btn blue" Text="Submit" OnClick="Submit_Click" OnClientClick="Javascript:return IsValide();" />
                                                <asp:Button ID="btnAllotment" runat="server" CssClass="btn blue" Text="Batch Allotment" OnClick="btnBatchAllocate_Click" OnClientClick="Javascript:return IsValide();" />
                                                <asp:Button ID="btnReceipt" runat="server" Text="Receipt" CssClass="btn blue" OnClick="btnReceipt_Click" Visible="false" />
                                                <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn blue" OnClick="Exit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Added by ankita on 26/06/2017--%>
            <div class="row" id="div_cashrct" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdCashRct" runat="server" AllowPaging="True"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99" OnSelectedIndexChanged="grdCashRct_SelectedIndexChanged"
                                            OnPageIndexChanging="grdOwgData_PageIndexChanging"
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PRODUCT TYPE" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="AT" runat="server" Text='<%# Eval("AT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ACC No" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ACNO" runat="server" Text='<%# Eval("ACNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CUST NAME" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Name" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Amount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="NARRATION" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Particulars" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MAKER" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MAKER" runat="server" Text='<%# Eval("MAKER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Receipt" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkPrintReceipt" runat="server" CommandName="select" class="glyphicon glyphicon-plus" OnClick="LnkPrintReceipt_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                               <%-- <asp:TemplateField HeaderText="Dens" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDens" runat="server" OnClick="lnkDens_Click" CommandArgument='<%#Eval("Dens")%>' CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

