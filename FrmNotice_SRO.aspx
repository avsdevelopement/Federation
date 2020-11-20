<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmNotice_SRO.aspx.cs" Inherits="FrmNotice_SRO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="https://www.google.com/jsapi?key=YourKeyHere">
    </script>

    <script language="javascript" type="text/javascript">
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage: google.elements.transliteration.LanguageCode.HINDI, // available option English, Bengali, Marathi, Malayalam etc.
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['txtHindiContent']);
        }
        google.setOnLoadCallback(onLoad);

    </script>
        
    
      <script type="text/javascript">
        function isvalidate() {

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
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script>

        function Year(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "-";
            obj.value = obj.value;
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

    <script type="text/javascript">
        $(function () {
            $("[id*=btnShowPopup]").click(function () {
                ShowPopup();
                return false;
            });
        });
        function ShowPopup() {
            $("#dialog").dialog({
                title: "title",
                width: 450,
                buttons: {
                    Ok: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
        }
</script>
   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #tblGoldLoan td {
            border: solid thin black;
        }
    </style>

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Notices
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                        <asp:Label ID="lblheader" runat="server"></asp:Label>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>
            </div>
            <div id="Div2" runat="server" visible="false">
                <div class="row" style="margin: 7px 0 7px 0">
                    <div class="col-lg-12">
                        <label class="control-label col-md-1">Branch Name<span class="required">*</span></label>
                        <div class="col-lg-3">
                            <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtbranchcode1" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin: 7px 0 7px 0">
                <div class="col-lg-12">
                    <label class="control-label col-md-1">Case Year <span class="required">*</span></label>
                    <div class="col-lg-1">
                        <asp:TextBox ID="txtloancode1" AutoPostBack="true" Placeholder="YY-YY" onkeyup="Year(this)" CssClass="form-control" OnTextChanged="txtloancode1_TextChanged" runat="server"></asp:TextBox>

                    </div>

                    <label class="control-label col-md-1">Case No <span class="required">*</span></label>
                    <div class="col-lg-1">
                        <asp:TextBox ID="TXTACCNO" placeholder="Case No" AutoPostBack="true" CssClass="form-control" OnTextChanged="TXTACCNO_TextChanged" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                    </div>

                    <label class="control-label col-md-1">Date<span class="required">*</span></label>
                    <div class="col-lg-2">
                        <asp:TextBox ID="TxtNoticeIssDt" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" AutoPostBack="true" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="DD/MM/YYYY" runat="server" Enabled="false" TargetControlID="TxtNoticeIssDt">
                        </asp:TextBoxWatermarkExtender>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtNoticeIssDt">
                        </asp:CalendarExtender>
                    </div >

                    <div class="col-lg-2" runat="server" visible="true"> 
                         <asp:Button ID="BTNADDDATE" runat="server" CssClass="btn blue" Text="ADDDATE" OnClick="BTNADDDATE_Click" />
                        <asp:HiddenField ID="hdn" runat="server" />
                    </div>
                    <br />
                    <br />
                    <div id="Divdate" class="col-lg-2" runat="server" visible="false">
                        <label class="control-label col-md-3">Change date</label>
                        <asp:TextBox ID="txtdatechange" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" AutoPostBack="true" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                               <asp:CalendarExtender ID="txtdatechange_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdatechange">
                                                 </asp:CalendarExtender>
                         
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn blue" Text="Update date" OnClick="btnUpdate_Click" />
                    </div>
                       
                </div>
            </div>

            <div  id="DIVVISIT" runat="server" visible="false">

                <div class="row" style="margin: 7px 0 7px 0; padding-top: 15px;">
                    <div class="col-md-12">
                         <label class="control-label col-md-1">VisitDate<span class="required">*</span></label>
                    <div class="col-lg-2">
                        <asp:TextBox ID="TXTDATE" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" AutoPostBack="true" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="false" TargetControlID="TXTDATE">
                        </asp:TextBoxWatermarkExtender>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TXTDATE">
                        </asp:CalendarExtender>
                    </div >
                        <label class="control-label col-md-1">SymbolicDate<span class="required">*</span></label>
                    <div class="col-lg-2">
                        <asp:TextBox ID="TXTSD" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" AutoPostBack="true" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="false" TargetControlID="TXTSD">
                        </asp:TextBoxWatermarkExtender>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TXTSD">
                        </asp:CalendarExtender>
                    </div >
                        </div>
                    </div>
            </div>
            <div id="Div1" runat="server" visible="false">
                <div class="row" style="margin: 7px 0 7px 0; padding-top: 15px;">
                    <div class="col-md-12">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButton ID="RbnAdd1" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="RbnAdd1_CheckedChanged" Text="Present Address" groupid="baldate" GroupName="AS"></asp:RadioButton>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButton ID="RbnAdd2" runat="server" Text="permanent Address" AutoPostBack="true" OnCheckedChanged="RbnAdd2_CheckedChanged" groupid="baldate" GroupName="AS"></asp:RadioButton>
                        </div>
                        <div class="col-md-2">
                            <asp:RadioButton ID="RbnAdd3" runat="server" Text="Office Address" AutoPostBack="true" groupid="baldate" OnCheckedChanged="RbnAdd3_CheckedChanged" GroupName="AS"></asp:RadioButton>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="TXtShowAdd" runat="server" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--  <table class="table table-striped table-bordered table-hover" id="tblGoldLoan" visible="false" style="border: 1px solid #000000; border-collapse: collapse; width: 75%">
                <thead>
                    <tr>
                        <td>
                            <asp:Label ID="lbl1" runat="server">1</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg1" runat="server" Text="DEMAND NOTICE"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg1" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg1_Click"></asp:LinkButton>
                        </td>
                         <td>
                            <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl2" runat="server">2</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg2" runat="server" Text="NOTICE BEFORE ATTACHMENT"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg2" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg2_Click"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:TextBox ID="txtg2" runat="server" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl3" runat="server">3</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg3" runat="server" Text="ATTACHEMENT NOTICE"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg3" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg3_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl4" runat="server">4</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg4" runat="server" Text="VISIT NOTICE"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg4" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg4_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl5" runat="server">5</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg5" runat="server" Text="SYMBOLIC ATTACHMENT NOTICE"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg5" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg5_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl6" runat="server">6</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg6" runat="server" Text="PROPERTY ATTACHMENT ORDER"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg6" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg6_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl7" runat="server">7</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg7" runat="server" Text="AC ATTACHMENT ORDER"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg7" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg7_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl8" runat="server">8</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg8" runat="server" Text="INTIMATION OF VALUATION LETTER"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg8" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg8_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl9" runat="server">9</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg9" runat="server" Text="POLICE PROTECTION LETTER"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg9" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg9_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl10" runat="server">10</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg10" runat="server" Text="POSSESSION NOTICE"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg10" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg10_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl11" runat="server">11</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg11" runat="server" Text="UPSET PRICE PROPOSAL"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg11" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg11_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl12" runat="server">12</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg12" runat="server" Text="UPSET PRICE COVERING LETTER"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg12" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg12_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl13" runat="server">13</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg13" runat="server" Text="PUBLIC NOTICE"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg13" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg13_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl14" runat="server">14</asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblg14" runat="server" Text="SUSHIL SAMEER"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkg14" CssClass="glyphicon glyphicon-plus" runat="server" OnClick="lnkg14_Click">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </thead>

            </table>--%>
        <div class="row" runat="server" id="div_Grid">
            <div class="col-lg-12">
                <div class="table-scrollable" style="border: none">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="GrdNotice" runat="server" AllowPaging="True"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        EditRowStyle-BackColor="#FFFF99" OnPageIndexChanged="GrdNotice_PageIndexChanged"
                                        PageIndex="10" PageSize="25"
                                        PagerStyle-CssClass="pgr" Width="90%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="1%" ItemStyle-Width="1%" FooterStyle-Width="5%">
                                                <ItemTemplate>
                          <%--<div id="dialog" runat="server" style="display:none;height:150px;width:50px;">
                                               <asp:TextBox ID="txtdatechange" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" AutoPostBack="true" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                               <asp:CalendarExtender ID="txtdatechange_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdatechange">
                                                 </asp:CalendarExtender>--%>
                                         </div> 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SRO No" HeaderStyle-Width="5%" ItemStyle-Width="5%" FooterStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="SRO_NO" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Notice" HeaderStyle-Width="15%" ItemStyle-Width="15%" FooterStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="NAME" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ISSUEDATE" HeaderStyle-Width="10%" ItemStyle-Width="10%" FooterStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="LASTNOTICEDATE" runat="server" Text='<%# Eval("LASTNOTICEDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="true" HeaderStyle-Width="3%" ItemStyle-Width="3%" FooterStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("ID")+"_"+Eval("LASTNOTICEDATE")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Center"  />
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="" Visible="true" >
                                                  
                                                   <ItemTemplate>
                                                    <asp:LinkButton ID="DatePopup" runat="server" Text="Change Date" CommandArgument='<%#Eval("ID")+"_"+Eval("LASTNOTICEDATE")%>' CommandName="select" OnClick="DatePopup_Click" ></asp:LinkButton>
                                                </ItemTemplate>
                                                </asp:TemplateField>

                                            <asp:TemplateField HeaderText="WordFile">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="wordfile" runat="server" Text="Download" CommandArgument='<%#Eval("ID")+"_"+Eval("LASTNOTICEDATE")%>' CommandName="select" OnClick="wordfile_Click" ></asp:LinkButton>
                                                   
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Center"  />
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

        <div class="form-actions">
            <div class="row">
                <div class="col-md-offset-3 col-md-9">
                    <asp:Button ID="btnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="btnExit_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnid" runat="server" />
</asp:Content>

