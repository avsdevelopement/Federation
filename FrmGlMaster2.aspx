<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmGlMaster2.aspx.cs" Inherits="FrmGlMaster2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <script type="text/javascript" src="https://www.google.com/jsapi?key=YourKeyHere">
    </script>
    <script  type="text/javascript">
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage: google.elements.transliteration.LanguageCode.MARATHI, // available option English, Bengali, Marathi, Malayalam etc.
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['txtsubglmarathi']);
        }
        google.setOnLoadCallback(onLoad);

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

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function ShowHideDiv() {
            if ($("#RdbSingle").is(":checked")) {
                $("#Single").hide();
                $("#Multiple").show();
            }
            else {
                $("#Single").show();
                $("#Multiple").hide();
            }
        }
    </script>
    <script type="text/javascript">
        function ShowHideDiv1() {
            if ($("#RdbMultiple").is(":checked")) {
                $("#Single").show();
                $("#Multiple").hide();
            }
            else {
                $("#Single").hide();
                $("#Multiple").show();
            }
        }
    </script>

    <script>

        function PassHiddenValue(str) {

            var Result = str;
            document.getElementById('<%=HdnValue.ClientID %>').value = Result;

        }
    </script>

        <script>

            function ClearLabel() {

               // alert("hello");
           
                document.getElementById("lblAddMessage").innerHTML = "";
                //document.getElementById('<%=lblAddMessage.ClientID %>').value = "";

        }
    </script>


    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            -webkit-transition: background-color 2s, filter 2s,opacity 2s; /* Safari prior 6.1 */
            transition: background-color 2s, filter 2s,opacity 2s;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }

        .row1 {
            font-weight: bold;
        }


        .row2 {
            font-weight: bold;
        }



        tr.row2 td {
            padding-top: 10px;
        }

        tr.row3 td {
            padding-top: 10px;
        }
    </style>

    <%-- <script type="text/javascript">

        function isvalidate() {
            //alert("hello");
            var r = confirm("Do You Want To Apply  Modification For All Branches");
            if (r == true) {
                document.getElementById('<%=HdnValue.ClientID %>').value = "Y";
                // txt = "You pressed OK!";
            } else {
                document.getElementById('<%=HdnValue.ClientID %>').value = "N";
                //  txt = "You pressed Cancel!";
            }

            alert(document.getElementById('<%=HdnValue.ClientID %>').value);
            return true;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        General Master 
                    </div>
                </div>
                <div class="portlet-body form">
                    <div id="DIVAC" runat="server" class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">

                                        <div id="Single" runat="server" visible="true">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Gl Category<span class="required">*</span></label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="DDLGLCAT" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="16" OnSelectedIndexChanged="DDLGLCAT_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2">GL Group <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtglgroup" placeholder="GL Group" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlbsFormat" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="16" OnSelectedIndexChanged="ddlbsFormat_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">GL Code <span class="required">*</span></label>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="txtglcode" Enabled="false" CssClass="form-control" PlaceHolder="Gl code" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">SubGlcode <span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtsubglcode" Enabled="false" OnTextChanged="txtsubglcode_TextChanged" AutoPostBack="true" CssClass="form-control" PlaceHolder="SubGlCode" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="txtglg" runat="server" CssClass="form-control"></asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-1">

                                                        <asp:Button ID="Button1" runat="server" Text="Add New" CssClass="btn blue" Visible="false" OnClick="Button1_Click"  />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Subglname </label>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="TXTSUBGLNAME" placeholder="SubGl Name" OnTextChanged="TXTSUBGLNAME_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <label class="control-label col-md-2">Placcno </label>
                                                    <div class="col-lg-1">
                                                        <asp:TextBox ID="txtplaccno" placeholder="Pl accNo" OnTextChanged="txtplaccno_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="TXTPLACCNAME" placeholder="Pl accNo" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                       <label class="control-label col-md-2">Select Marathi Font </label>
                                                        <div class="col-lg-2">
                                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
                                                               <%--  <asp:ListItem Text="Select" Selected="True" />--%>
                                                             <asp:ListItem Text="Shivaji01"  Value="0" />
                                                            <asp:ListItem Text="Sarjudas" Value="1" /></asp:DropDownList>
                                                        </div>
                                                    <label class="control-label col-md-3">Subglname(marathi) </label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtsubglmarathi" placeholder="SubGl Name" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div id="PLGroup" runat="server">
                                                        <label class="control-label col-md-1">PL Group</label>
                                                        <div class="col-md-1">
                                                            <asp:DropDownList ID="DDLPLgroup" runat="server" CssClass="form-control" style="width:120px"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">INTACCYN </label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlAccYN" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="YES" Value="Y" Selected="True" />
                                                            <asp:ListItem Text="NO" Value="N" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2">Gl Operate </label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlOperate" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="YES" Value="1" Selected="True" />
                                                            <asp:ListItem Text="NO" Value="3" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Cash Debit</label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlCASHDR" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="YES" Value="Y" />
                                                            <asp:ListItem Text="NO" Value="N" Selected="True" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2">Cash Credit </label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlCASHCR" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="YES" Value="Y" />
                                                            <asp:ListItem Text="NO" Value="N" Selected="True" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Transfer Debit </label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlTRFDR" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="YES" Value="Y" />
                                                            <asp:ListItem Text="NO" Value="N" Selected="True" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2">Transfer Credit </label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlTRFCR" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="YES" Value="Y" />
                                                            <asp:ListItem Text="NO" Value="N" Selected="True" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Clearing Debit </label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlCLGDR" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="YES" Value="Y" />
                                                            <asp:ListItem Text="NO" Value="N" Selected="True" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2">Clearing Credit </label>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlCLGCR" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="YES" Value="Y" />
                                                            <asp:ListItem Text="NO" Value="N" Selected="True" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Opening Balance </label>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="txtOpeningBal" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <label class="control-label col-md-2">Impliment Date </label>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="txtImplmentDate" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" PlaceHolder="DD/MM/YYYY" onkeypress="javascript:return isNumber (event)" />
                                                        <asp:CalendarExtender ID="txtImplmentDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtImplmentDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">GlPriority </label>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="txtglpriority" palceholder="Gl Priority" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <label class="control-label col-md-2">Last No </label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtlastno" placeholder="Last No" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="DIV_DATE" runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="col-md-2 text-right">IR </label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TXTRECINT" CssClass="form-control " TabIndex="1" AutoPostBack="true" Width="80" PlaceHolder="PL Acc No " runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TXTRECINTNAME" CssClass="form-control " TabIndex="1" PlaceHolder=" Rec Int Acc Name" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="col-md-2 text-right">Penal Int head</label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TXTPLNO" CssClass="form-control " TabIndex="1" AutoPostBack="true" Width="80" PlaceHolder="PL Acc No " runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TXTPLNAME" CssClass="form-control " TabIndex="1" PlaceHolder=" PL Acc Name" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                            <div runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;" id="DivBRCDUni" visible="false">

                                                <div class="col-lg-12">
                                                    <label class="col-md-2 text-right">BRCD Unification </label>
                                                    <div class="col-md-1">
                                                        <asp:RadioButton ID="rdbUnificationYes" runat="server" onclick="PassHiddenValue('Y')" ClientIDMode="Static" Text="Yes" Checked="true" GroupName="rdbBRCDUnification" />
                                                    </div>
                                                    <div class="col-md-2"> 
                                                        <asp:RadioButton ID="rdbUnificationNo" runat="server" onclick="PassHiddenValue('N')" ClientIDMode="Static" Text="No" GroupName="rdbBRCDUnification"  />
                                                    </div>

                                                </div>
                                            </div>




                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-12 text-center">

                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="SUBMIT" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnclear" runat="server" CssClass="btn blue" Text="Clear" OnClick="btnclear_Click" />
                                        <asp:Button ID="exit" runat="server" CssClass="btn blue" Text="Exit" />
                                        <asp:HiddenField ID="HdnValue" runat="server" />

                                    </div>
                                </div>
                            </div>

                        </div>



                        <!-- ModalPopupExtender -->


                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="Button1"
                            CancelControlID="Button2" BackgroundCssClass="Background">
                        </asp:ModalPopupExtender>

                        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none; height: 150px">

                            <table>
                                <tr class="row1">

                                    <td>

                                        <asp:Label ID="lblGlCode" runat="server" class="col-md-2 text-right" Text="GlCode"></asp:Label>

                                    </td>

                                    <td>

                                        <asp:TextBox ID="txtddlGlCode" CssClass="form-control" runat="server" Font-Size="14px" Enabled="false"></asp:TextBox>

                                    </td>

                                </tr>

                            
                                <tr class="row2">


                                    <td>

                                        <asp:Label ID="lblCategory" runat="server" class="col-md-2 text-right" Text="Category"></asp:Label>

                                    </td>

                                    <td>

                                        <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control " Font-Size="14px"></asp:TextBox>

                                    </td>

                                </tr>

                                <tr class="row3">

                                    <td></td>
                                    <td>

                                        <asp:Button ID="btncategoryAdd" runat="server" CssClass="btn blue" Text="Add" OnClick="btncategoryAdd_Click" />
                                        <asp:Button ID="Button2" runat="server" CssClass="btn blue" Text="Close"  OnClick="Button2_Click"  OnClientClick="ClearLabel()"/>
                                        
                                        


                                    </td>
                                  
                                   
                                 



                                </tr>
                                
                                <tr>
                                <td>

                                </td>
                                     <td colspan="3">
                                       
                                        <asp:Label ID="lblAddMessage" runat="server" style="width:100px;margin-left:0px ; margin-top:50px;font-size:large"  ClientIDMode="Static"  ></asp:Label>
                                    </td>
                                </tr>







                            </table>




                        </asp:Panel>
                    </div>

                    <div class="col-md-12">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdAgentComm" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                            EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%" PageIndex="10" PageSize="25">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Agent Code" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="AGENTCODE" runat="server" Text='<%# Eval("AGENTCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Coll AMT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Amount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Commission">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Commission" runat="server" Text='<%# Eval("COMMISSION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="TDS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TDS" runat="server" Text='<%# Eval("TDS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agent SEC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="AGENTSEC" runat="server" Text='<%# Eval("AGENTSEC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agent AMC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="agentamc" runat="server" Text='<%# Eval("AGENTAMC") %>'></asp:Label>
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
</asp:Content>


