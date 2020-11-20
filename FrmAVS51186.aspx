<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51186.aspx.cs" Inherits="FrmAVS51186" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>

        function FormatIt(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "/";
            if (obj.value.length == 5) //MONTH
                obj.value = obj.value + "/";
            if (obj.value.length == 11) //YEAR
                alert("Enter Valid Date!....");
        }
    </script>

    <script>

        function Year(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "-";
            obj.value = obj.value;
        }
    </script>
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

        function CheckFirstChar(key, txt) {
            if (key == 32 && txt.value.length <= 0) {
                return false;
            }
            else if (txt.value.length > 0) {
                if (txt.value.charCodeAt(0) == 32) {
                    txt.value = txt.value.substring(1, txt.value.length);
                    return true;
                }
            }
            return true;
        }

    </script>
    <script>

        function dateLen(dt) {
            var dt1 = dt + '';
            if (dt1.length == 1)
                dt1 = '0' + dt;

            return dt1;
        }
    </script>
    <script type="text/javascript">
        function IsValide() {

            if (prdcd == "") {
                message = 'Please Enter Case No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseNO.ClientID %>').focus();
                return false;
            }

            if (accno == "") {
                message = 'Please Enter  Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseY.ClientID %>').focus();
                return false;
            }

        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    Movement Master
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="tab-pane active" id="tab__blue">
                                        <ul class="nav nav-pills">

                                            <li>
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="1"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="2"><i class="fa fa-arrows"></i>Modify</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="3"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="4"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                            </li>

                                            <li class="pull-right">
                                                <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">


                                                    <label class="control-label col-md-2">Case Year <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseY" MaxLength="5" CssClass="form-control" runat="server" Placeholder="YY-YY" onkeyup="Year(this)" onkeypress="javascript:return isNumber (event)" TabIndex="5"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-3">Case No<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseNO" CssClass="form-control" Placeholder="Case No" OnTextChanged="txtCaseNO_TextChanged" runat="server" TabIndex="6" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                           <div class="row" style="margin: 7px 0 7px 0">


                                                  <div class="col-lg-12">
                                                      <label class="control-label col-md-2">Society Name<span class="required">*</span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtSociName" Placeholder="Society Name" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="7" ></asp:TextBox>
                                                    </div>
                                                       <%-- <label class="control-label col-md-2" >Member No:<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtMember" CssClass="form-control"  AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtMember_TextChanged"  PlaceHolder="Member No" TabIndex="8" runat="server" ></asp:TextBox>
                                                    </div>
                                                
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMemberName"  Width="280px" Placeholder="Member Name" OnTextChanged="txtMemberName_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" AutoPostBack="true"  onkeypress="javascript:return OnltAlphabets(event)" Enabled="true"  CssClass="form-control" runat="server" TabIndex="9"></asp:TextBox>
                                                  <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtMemberName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetMemberName"
                                                        CompletionListElementID="CustList2">
                                                    </asp:AutoCompleteExtender>
                                                        
                                                           </div>

                                                   <%-- <label class="control-label col-md-2">Society Name<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtSociName" Placeholder="Society Name" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="7" AutoPostBack="true"></asp:TextBox>
                                                    </div>--%>
                                                    <%-- <label class="control-label col-md-2">Recovery Officer<span class="required">*</span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtRecoveryoff" onkeypress="javascript:return isNumber (event)" Placeholder="Recovery Officer" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7" AutoPostBack="true"></asp:TextBox>
                                                    </div>--%>

                                                </div>
                                               <div class="col-lg-12"></div>
                                               </div>
                                            <div class="row">
                                                <div class="col-lg-12"></div>
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.41);"></div>
                                                        <div class="col-md-4"><strong style="color: #3598dc"></strong></div>

                                                    </div>
                                                </div>
                                                <div id="Div3" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Movement Date<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtMovDate" CssClass="form-control" TabIndex="8" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtnextfileWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtMovDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="txtnextfile_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtMovDate">
                                                            </asp:CalendarExtender>
                                                        </div>

                                                         <label class="control-label col-md-3">SRO No : <span class="required">*</span></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="TxtSRNO" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtSRNO_TextChanged" PlaceHolder="SRO No" TabIndex="9" runat="server" AutoPostBack="true" > </asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TXTSROName" Placeholder="SrNo Name" Enabled="true" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="10"></asp:TextBox>
                                                                </div>


                                                       <%-- <label class="control-label col-md-3">Movement Details<span class="required">*</span></label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtMovDetails" Placeholder="Movement Details" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7" AutoPostBack="true"></asp:TextBox>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            <div id="Div2" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                      <label class="control-label col-md-2">Movement Details<span class="required">*</span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtMovDetails" Width="350px" TextMode="MultiLine" Placeholder="Movement Details" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="11" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                         <label class="control-label col-md-2">Rcovery Amount<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtRecAmt" Placeholder="Rcovery Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="12" ></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="Div4" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                       
                                                       <%-- <label class="control-label col-md-3">Case Status<span class="required">*</span></label>
                                                         <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" TabIndex="5" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                                            </div>
                                                        <label class="control-label col-md-1">Action Status<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlActstatus" runat="server" CssClass="form-control" TabIndex="5" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                                            </div>--%>
                                                    </div>
                                                </div>
                                             <div id="Div5" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                         <label class="control-label col-md-2">Case Status : <span class="required">*</span></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtcasestatusno" CssClass="form-control" OnTextChanged="txtcasestatusno_TextChanged" onkeypress="javascript:return isNumber (event)" PlaceHolder="Case Status No" TabIndex="13" runat="server" AutoPostBack="true" > </asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtcasestatusname" Placeholder="Case Status" Enabled="true"  onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" ></asp:TextBox>
                                                                </div>
                                                          <label class="control-label col-md-2">Action Status : <span class="required">*</span></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtactionno" CssClass="form-control" OnTextChanged="txtactionno_TextChanged" onkeypress="javascript:return isNumber (event)" PlaceHolder="Action Status No" TabIndex="14" runat="server" AutoPostBack="true" > </asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtactionname" Placeholder="Action Status" Enabled="true" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="14"></asp:TextBox>
                                                                </div>
                                                       <%-- <label class="control-label col-md-2">Case Status<span class="required">*</span></label>
                                                         <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" TabIndex="5" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                                            </div>--%>
                                                       <%-- <label class="control-label col-md-3">Action Status<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlActstatus" runat="server" CssClass="form-control" TabIndex="5" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                                            </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-10">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" TabIndex="20" OnClientClick="Javascript:return IsValide()" />
                                                <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="21" />
                                                <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="22" />
                                            </div>
                                            <div class="col-md-5">
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

   <div class="row" runat="server" id="div_Grid">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GrdMovement" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GrdMovement_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRO_NO" runat="server" Text='<%# Eval("SRO_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ENTERYDATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ENTERYDATE" runat="server" Text='<%# Eval("ENTRYDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CASESTATUS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEsTATUS" runat="server" Text='<%# Eval("CASESTATUSNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="ACTIONSTATUS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ACTIONSTATUS" runat="server" Text='<%# Eval("ACTIONSTATUSNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                  <%--  <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDelete_Click1" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                        <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                  
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#FFFF99" />
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
