<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5054.aspx.cs" Inherits="FrmAVS5054" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function IsValide() {
            var Mobile = document.getElementById('<%=TxtMobile.ClientID%>').value;
            var Massage = document.getElementById('<%=TxtMsg.ClientID%>').value;

            var message = '';
            if (Mobile == "") {
                message = 'Please Enter Mobile No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtMobile.ClientID %>').focus();
                return false;
            }
            if (Massage == "") {
                message = 'Please Enter Massage....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtMsg.ClientID %>').focus();
                return false;
            }
        }
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        function CheckMaxCount(txtBox, e, maxLength) {
            if (txtBox) {
                var SMSSize, count;
                count = parseInt((txtBox.value.length) / 160)+1;
                var Len = 160 * count;
                var lenght = Len - txtBox.value.length;
                if (txtBox.value.length > Len) {
                    document.getElementById('<%=hdnCount.ClientID%>').value = parseInt(count) + 1;
                    document.getElementById('lblMsgLength').innerHTML = count + '/' + lenght;
                }
                document.getElementById('lblMsgLength').innerHTML = count + '/' + lenght;
                if (!checkSpecialKeys(e)) {
                    document.getElementById('lblMsgLength').innerHTML = "0";
                    return (txtBox.value.length <= maxLength)
                }
            }
        }
        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function CallonLoad() {
            // Create an instance on TransliterationControl with the required
            // options.
            var varComment = $("input[name='<%=RDBLANG.UniqueID%>']:checked").val();

            if (varComment == 0) {
                var optionsEng = {
                    sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                    destinationLanguage:
                [google.elements.transliteration.LanguageCode.ENGLISH],
                    shortcutKey: 'ctrl+g',
                    transliterationEnabled: true
                };

                var control = new google.elements.transliteration.TransliterationControl(optionsEng);
            }
            else if (varComment == 1) {
                var optionsMar = {
                    sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,

                    destinationLanguage:
                [google.elements.transliteration.LanguageCode.MARATHI],
                    shortcutKey: 'ctrl+g',
                    transliterationEnabled: true
                };

                var control = new google.elements.transliteration.TransliterationControl(optionsMar);
            }

            control.makeTransliteratable(['<%=TxtMsg.ClientID%>']);
        }
        google.setOnLoadCallback(CallonLoad);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Bulk SMS
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="portlet-body">
                                        <div class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">SMS Type<span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="Rdb_Specific" runat="server" Text="Member" AutoPostBack="true" GroupName="TYPE1" OnCheckedChanged="Rdb_Specific_CheckedChanged" />
                                                    <asp:RadioButton ID="Rdb_All" runat="server" Text="Non Member" AutoPostBack="true" GroupName="TYPE1" OnCheckedChanged="Rdb_All_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Mobile No<span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtMobile" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Mobile No"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12">
                                                <%-- <label class="control-label col-md-2">Choose Type<span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control">
                                                      
                                                    </asp:DropDownList>
                                                </div>--%>
                                                <label class="control-label col-md-2">Language<span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:RadioButtonList ID="RDBLANG" runat="server" OnSelectedIndexChanged="RDBLANG_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="0">English</asp:ListItem>
                                                        <asp:ListItem Value="1">Marathi</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Message<span class="required">* </span></label>
                                                <div class="col-md-4" id="DivMsgE" runat="server">
                                                    <asp:TextBox ID="TxtMsgE" runat="server" CssClass="form-control" TextMode="MultiLine"  placeholder="Write Message Here.." onkeyUp="return CheckMaxCount(this,event,160);"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4" visible="false" id="DivMsg" runat="server">
                                                    <asp:TextBox ID="TxtMsg" runat="server" CssClass="form-control" onclick="CallonLoad()" TextMode="MultiLine" placeholder="येथे माचकुर लिहा ..." onkeyUp="return CheckMaxCount(this,event,160);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="Div1" class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12" >
                                                <div class="col-md-5" style="text-align:right">
                                                <label id="lblMsgLength" style="color:red">160</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">SMS Sending Date</label>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtSMSDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                                    <asp:CalendarExtender ID="txtBMeetDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtSMSDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" style="text-align: center">

                                                <asp:Button ID="Btn_send" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="Btn_send_Click" TabIndex="16" OnClientClick="javascript:return IsValide();" />
                                                <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" TabIndex="21" />
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
    <asp:HiddenField ID="hdnCount" runat="server" Value="1" />
</asp:Content>

