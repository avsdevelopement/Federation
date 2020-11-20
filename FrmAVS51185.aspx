<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true"  CodeFile="FrmAVS51185.aspx.cs" Inherits="FrmAVS51185" %>
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
            var year = document.getElementById('<%=txtCaseY.ClientID%>').value;
            var caseno = document.getElementById('<%=txtCaseNO.ClientID%>').value;
            var member = document.getElementById('<%=txtMember.ClientID%>').value;
          

          
            if (caseno == "") {
                message = 'Please Enter Case No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseNO.ClientID %>').focus();
                 return false;
             }
           
            if (year == "") {
                message = 'Please Enter  Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseY.ClientID %>').focus();
                return false;
            }
            if (member == "") {
                message = 'Please Member No ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtMember.ClientID %>').focus();
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
                   Defaulter Master
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

                                           <%-- <li>
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                            </li>
                                           <%-- <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Modify</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                            </li>--%>

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
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtCaseY" MaxLength="5" CssClass="form-control" runat="server"  Placeholder="YY-YY"  onkeyup="Year(this)" onkeypress="javascript:return isNumber (event)"  TabIndex="1"></asp:TextBox>
                                                    </div>
                                                     <label class="control-label col-md-1" >Case No<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseNO" CssClass="form-control" Placeholder="Case No" OnTextChanged="txtCaseNO_TextChanged" runat="server" TabIndex="2" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                     <label class="control-label col-md-1" >Member No:<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtMember" CssClass="form-control"  AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtMember_TextChanged" PlaceHolder="Member No" TabIndex="3" runat="server" ></asp:TextBox>
                                                    </div>
                                                
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtMemberName"  Width="330px" Placeholder="Member Name" onkeydown="return CheckFirstChar(event.keyCode, this);" AutoPostBack="true"  onkeypress="javascript:return OnltAlphabets(event)" Enabled="true" OnTextChanged="txtMemberName_TextChanged" CssClass="form-control" runat="server" TabIndex="4"></asp:TextBox>
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
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0" runat="server" id="CUSTNODIV">
                                                <div class="col-lg-12">
                                                   
                                                     <label class="control-label col-md-2" >Default Name<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtDefName" Width="360px"  onkeydown="return CheckFirstChar(event.keyCode, this);"  onkeypress="javascript:return OnltAlphabets(event)"   placeholder="Default Name" CssClass="form-control" runat="server" TabIndex="5"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                      <label class="control-label col-md-2" >Mobile No<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMob1" Width="155px" style="margin-right:2px"  onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)"  placeholder="Mobile No 1" CssClass="form-control" runat="server" TabIndex="6"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                     <label class="control-label col-md-1" >Mobile No</label>
                                                 
                                                     <div class="col-md-2">
                                                        <asp:TextBox ID="txtmob2"   Width="160px"  onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" placeholder="Mobile No 2" CssClass="form-control" runat="server" TabIndex="7" MaxLength="10"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    
                                                   <%--  <label class="control-label col-md-2" >Mobile No<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMob1"  onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Mobile No 1" CssClass="form-control" runat="server" TabIndex="7"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                     <div class="col-md-2">
                                                        <asp:TextBox ID="txtmob2"  onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Mobile No 2" CssClass="form-control" runat="server" TabIndex="7"  AutoPostBack="true"></asp:TextBox>
                                                    </div>--%>
                                                   
                                                </div>
                                            </div>

                                             <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div2">
                                                <div class="col-lg-12">
                                                   
                                                   
                                                 <%--  <label class="control-label col-md-2" >Mobile No<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMob1"  onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)"  placeholder="Mobile No 1" CssClass="form-control" runat="server" TabIndex="7"  AutoPostBack="true"></asp:TextBox>
                                                    </div>--%>
                                                     <label class="control-label col-md-2" >Default Property</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtDefaultProperty"  TextMode="MultiLine"   onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Default Property" CssClass="form-control" runat="server" TabIndex="8"  AutoPostBack="true"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-1">Correspondence Address</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtCorrespondence"  style="margin-left:3px" Width="430px"   TextMode="MultiLine" Placeholder="Correspondence Address" onkeydown="return CheckFirstChar(event.keyCode, this);"  CssClass="form-control" runat="server" TabIndex="9" AutoPostBack="true"></asp:TextBox>
                                                    </div>

                                                   
                                                   
                                                </div>
                                            </div>
                                            <div id="Div3" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <%--<label class="control-label col-md-2">Correspondence Address</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtCorrespondence"  Width="380px" Placeholder="Correspondence Address" onkeydown="return CheckFirstChar(event.keyCode, this);"  CssClass="form-control" runat="server" TabIndex="7" AutoPostBack="true"></asp:TextBox>
                                                    </div>--%>
                                                    <%-- <label class="control-label col-md-2">Ward</label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtWard"   Placeholder="Ward" onkeydown="return CheckFirstChar(event.keyCode, this);"  CssClass="form-control" runat="server" TabIndex="10" AutoPostBack="true"></asp:TextBox>
                                                    </div>--%>
                                                     <label class="control-label col-md-2">Ward:</label>
                                                        <div class="col-md-2">
                                                            <%-- <asp:TextBox ID="txtWard" Placeholder="Ward" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="10" AutoPostBack="true"></asp:TextBox>
                                                            --%>
                                                            <asp:DropDownList ID="txtWard" runat="server" CssClass="form-control" TabIndex="10"></asp:DropDownList>
                                                        </div>
                                                    <label class="control-label col-md-2" >City</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCity"   Placeholder="City" style="margin-left:3px" onkeydown="return CheckFirstChar(event.keyCode, this);"  CssClass="form-control" runat="server" TabIndex="11" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                     <label class="control-label col-md-1" style="margin-left:-25px">Pincode</label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtPincode" Placeholder="Pincode" onkeypress="javascript:return isNumber (event)" MaxLength="6" onkeydown="return CheckFirstChar(event.keyCode, this);"  CssClass="form-control" runat="server" TabIndex="12" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div4" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Occupation Details</label>
                                                    <div class="col-md-3">
                                                          <asp:DropDownList ID="ddlOccupation" runat="server" CssClass="form-control" TabIndex="25">
                                                            </asp:DropDownList>
                                                         </div>
                                                     <label class="control-label col-md-2" style="margin-right:3px">Occupation Address</label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtOccupationAdd" Width="430px"  TextMode="MultiLine" Placeholder="Occupation Address" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="13"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
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
                                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" TabIndex="14" OnClientClick="Javascript:return IsValide()" />
                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="15" />
                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="16" />
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
                                <asp:GridView ID="GrdDemand" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="GrdDemand_PageIndexChanging"
                                   PageSize="25" 
                                    PagerStyle-CssClass="pgr" Width="100%">
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

                                        <asp:TemplateField HeaderText="MEMBER No">
                                            <ItemTemplate>
                                                <asp:Label ID="MEMBER_NO" runat="server" Text='<%# Eval("MEMBERNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DEFAULTER_PROPERTY">
                                            <ItemTemplate>
                                                <asp:Label ID="DEFAULTER_PROPERTY" runat="server" Text='<%# Eval("DEFAULTER_PROPERTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="DEFAULTER NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="DEFAULTER" runat="server" Text='<%# Eval("DEFAULTER_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OCC_DETAIL">
                                            <ItemTemplate>
                                                <asp:Label ID="OCC_DETAIL" runat="server" Text='<%# Eval("OCC_DETAIL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="MOBILE1">
                                            <ItemTemplate>
                                                <asp:Label ID="MOBILE1" runat="server" Text='<%# Eval("MOBILE1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Cancel" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server"  CommandArgument='<%#Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDelete_Click1" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="View" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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