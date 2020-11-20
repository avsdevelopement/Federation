<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5143.aspx.cs" Inherits="FrmAVS5143" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
    
       <%-- Only Numbers --%>
        function isNumber(evt)
        {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function validate()
        {
            var message = '';
            var TxtMemberNo = document.getElementById('<% =TxtMemberNo.ClientID%>').value;
            Financial = document.getElementById('<%=DdlAccActivity.ClientID%>').value;

            if (Financial == "0")
            {
                message = 'Please Select Financial Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtBankCode.ClientID%>').focus();
                return false;
            }
            if (TxtMemberNo == "")
            {
                message = 'Please Enter Member No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtMemberNo.ClientID%>').focus();
                 return false;
             }

             var TxtDivident = document.getElementById('<% =TxtDivident.ClientID%>').value;
            if (TxtDivident == "")
            {
                message = 'Please Enter Divident...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtDivident.ClientID%>').focus();
                 return false;
             }
             var TxtTAmt = document.getElementById('<% =TxtTAmt.ClientID%>').value;
            if (TxtTAmt == "")
            {
                message = 'Please Enter Deposit Interest....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtTAmt.ClientID%>').focus();
                 return false;
             }

             var TxtTotPayAmt = document.getElementById('<% =TxtTotPayAmt.ClientID%>').value;
            if (TxtTotPayAmt == "")
            {
                message = 'Please Enter Total payable amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtTotPayAmt.ClientID%>').focus();
                 return false;
             }

             var txtChq = document.getElementById('<% =TxtChq.ClientID%>').value;
            if (txtChq == "")
            {
                message = 'Please Enter Cheque No...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtChq.ClientID%>').focus();
                 return false;
            }
            
            if (txtChq.length < 6) {
                message = 'Enter 6 Digits Cheque No...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtChq.ClientID%>').focus();
                return false;
            }
           


            var txtBankCode = document.getElementById('<% =TxtBankCode.ClientID%>').value;

            if (txtBankCode == "")
            {
                message = 'Please Enter Bank Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtBankCode.ClientID%>').focus();
                 return false;
            }

            if (txtBankCode <101) {
                message = 'Please Enter Valid Bank Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtBankCode.ClientID%>').focus();
                return false;
            }

         }
    </script>


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
            control.makeTransliteratable(['txtMarathiName']);
        }
        google.setOnLoadCallback(onLoad);

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        Cheque Print
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">
                    <%--Div 1--%>
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label">Financial Year</label>
                            </div>
                            <div class="col-md-1" style="width:120px">
                                <asp:DropDownList ID="DdlAccActivity" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Member No <span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtMemberNo" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="TxtMemberNo_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtName" CssClass="form-control" OnTextChanged="TxtName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtMarathiName" CssClass="form-control"  runat="server" Visible="false"></asp:TextBox>

                                <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="autocustname" runat="server" TargetControlID="TxtName"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="GetCustNames" CompletionListElementID="CustList">
                                </asp:AutoCompleteExtender>
                            </div>

                        </div>
                    </div>


                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Dividend <span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtDivident" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtDivident_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-2">Deposit Interest <span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtTAmt" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtTAmt_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>

                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Total Payable Amount<span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtTotPayAmt" onkeypress="javascript:return isNumber (event)" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                            </div>

                             <label class="control-label col-md-2">Cheque Print Date<span class="required">*</span></label>
                            <div class="col-md-1" style="width:110px">
                                <asp:TextBox ID="txtChqPrintDate"  onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" Enabled="false" runat="server" ></asp:TextBox>
                            </div>


                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Cheque No<span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtChq" MaxLength="6" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">BankCode <span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtBankCode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-offset-2 col-md-9">
                                <asp:Button ID="BtnPrint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" />
                                <asp:Button ID="BtnBKPrint" runat="server" Text="Back print" CssClass="btn btn-primary" OnClick="BtnBKPrint_Click" OnClientClick="javascript:return validate();" />
                                <asp:Button ID="Button1" runat="server" Text="Re-Print" CssClass="btn btn-primary" OnClick="BtnReprint_Click" OnClientClick="javascript:return validate();" />
                                <asp:Button ID="BtnPost" runat="server" Text="Post" CssClass="btn btn-primary" OnClick="BtnPost_Click" OnClientClick="javascript:return validate();" />
                                <asp:Button ID="BtnCrPost" runat="server" Text="Credit Div_Int Post" CssClass="btn btn-primary" OnClick="BtnCrPost_Click" OnClientClick="javascript:return validate();" />
                            </div>
                        </div>
                    </div>
                </div>
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

