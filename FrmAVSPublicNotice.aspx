<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVSPublicNotice.aspx.cs" Inherits="FrmAVSPublicNotice" %>

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
                    PUBLICE NOTICE DETAILS
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

                                            <%--<li>
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Modify</asp:LinkButton>
                                            </li>

                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
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
                                                        <asp:TextBox ID="txtCaseY" MaxLength="5" CssClass="form-control" runat="server" Placeholder="YY-YY" onkeyup="Year(this)" onkeypress="javascript:return isNumber (event)" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Case No<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtCaseNO" CssClass="form-control" Placeholder="Case No" runat="server" OnTextChanged="txtCaseNO_TextChanged" TabIndex="2" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>


                                                    </div>
                                                    <label class="control-label col-md-2">Recovery Certificate issued :<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtissueno" CssClass="form-control" Placeholder="Recovery Certificate issued " runat="server" TabIndex="3" AutoPostBack="true" ></asp:TextBox>


                                                    </div>
                                                    <label class="control-label col-md-1">Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtedate" TabIndex="4" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtedate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtedate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div></div>
                                         

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Inspection_Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtIDate" TabIndex="5" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtIDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtIDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                   <label class="control-label col-md-2">From Time :<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtIFT" CssClass="form-control" Placeholder="From Time " runat="server" TabIndex="6" AutoPostBack="False" ></asp:TextBox>


                                                    </div><label class="control-label col-md-2">To Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtITTM" CssClass="form-control" Placeholder="To Time " runat="server" TabIndex="7" AutoPostBack="False" ></asp:TextBox>


                                                    </div>
                                                    
                                                </div>
                                            </div>
                                              
                                                  <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">


                                                  
                                                    
                                                    <label class="control-label col-md-2">Tenders_Open_Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtodate" TabIndex="8" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtodate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtodate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                     <label class="control-label col-md-2">From Time :<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtoFtm" CssClass="form-control" Placeholder="From Time " runat="server" TabIndex="9" AutoPostBack="False" ></asp:TextBox>


                                                    </div><label class="control-label col-md-2">To Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtottm" CssClass="form-control" Placeholder="To Time " runat="server" TabIndex="10" AutoPostBack="False" ></asp:TextBox>


                                                    </div>
                                                   
                                                </div>
                                            </div>
                                                  <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">


                                                    
                                                    <label class="control-label col-md-2">Recovery&SalesOfficer_Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtrsodate" TabIndex="11" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtrsodate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtrsodate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                     <label class="control-label col-md-2">From Time :<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtsftm" CssClass="form-control" Placeholder="From Time " runat="server" TabIndex="12" AutoPostBack="False" ></asp:TextBox>


                                                    </div><label class="control-label col-md-2">To Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtsttm" CssClass="form-control" Placeholder="To Time " runat="server" TabIndex="13" AutoPostBack="False" ></asp:TextBox>


                                                    </div>
                                                </div>
                                            </div>

                                             <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">


                                                    <label class="control-label col-md-2"> Reserve price <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtREsprice"  CssClass="form-control" runat="server" Placeholder="Reserve price"   onkeypress="javascript:return isNumber (event)" TabIndex="15"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Earnest Money Deposit<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtEaMoDe" CssClass="form-control" Placeholder="Earnest Money Deposit" runat="server" TabIndex="16" onkeypress="javascript:return isNumber (event)"></asp:TextBox>


                                                    </div>
                                                   
                                                </div>
                                            </div>
                                            <div></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-10">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" TabIndex="14" OnClick="BtnSubmit_Click" OnClientClick="Javascript:return IsValide()" />
                                            <asp:Button ID="BTNReport" runat="server" CssClass="btn btn-primary" Text="Report" TabIndex="15" OnClick="BTNReport_Click" OnClientClick="Javascript:return IsValide()" />
                                           
                                             <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" TabIndex="16" OnClick="BtnClear_Click" />
                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" OnClick="BtnExit_Click" TabIndex="17" Text="Exit" />
                                        </div>
                                        <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                             <asp:GridView ID="GridCase" runat="server" AllowPaging="True"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="LightBlue"
                                                                        PageIndex="10" PageSize="10"
                                                                        PagerStyle-CssClass="pgr" OnPageIndexChanged="GridCase_PageIndexChanged">
                                                                        <HeaderStyle Font-Bold="true" BackColor="LightBlue" HorizontalAlign="Center" />
                                                                        <AlternatingRowStyle />
                                               
                                                        <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                   <Columns>
                                                                            <%--<asp:TemplateField>
                                                                                <HeaderStyle BackColor="LightGray" ForeColor="DarkBlue" runat="server"/>

                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>' Style="padding: 50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Branch Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="BRCD1" runat="server" Text='<%# Eval("brcd") %>' Style="padding: 50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Recovery Certificate issued  ">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ISSUERecovery" runat="server" Text='<%# Eval("ISSUERecovery") %>' Style="padding: 50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="CASE YEAR">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CASEYEAR1" runat="server" Text='<%# Eval("CASEYEAR") %>' Style="padding: 50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Case No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CASENO1" runat="server" Text='<%# Eval("CaseNo") %>' Style="padding: 50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="DATE">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="APPLICTIONDATE1" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>' Style="padding: 50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Modify" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkModify" runat="server" CommandArgument='<%#Eval("ID")+"_"+Eval("brcd")+"_"+Eval("CaseNo")+"_"+Eval("CASEYEAR")%>' CommandName="select" OnClick="lnkModify_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Delete" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID")+"_"+Eval("brcd")+"_"+Eval("CaseNo")+"_"+Eval("CASEYEAR")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="VIEW" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("ID")+"_"+Eval("brcd")+"_"+Eval("CaseNo")+"_"+Eval("CASEYEAR")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                        </Columns>
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
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

