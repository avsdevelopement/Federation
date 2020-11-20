<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSmsModule_Pareli.aspx.cs" Inherits="FrmSmsModule_Pareli" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function Validate() {




            if (BRCD == "") {
                alert("Please enter Branch Code......!!");
                return false;
            }

            if (TxtAccT == "") {
                alert("Please Select The Account Type......!!");
                return false;
            }
            if (TxtAccno == "") {
                alert("Please Enter Account Number.......!!");
                return false;
            }
            if (TxtFDT == "") {
                alert("Please Enter From Date........!!");
                return false;
            }
            if (TxtTDt == "") {
                alert("Please Enter To Date..........!!");
                return false;
            }
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
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>


    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function CallonLoad() {
            // Create an instance on TransliterationControl with the required
            // options.



            var optionsMar = {
                sourceLanguage:
            google.elements.transliteration.LanguageCode.ENGLISH,

                destinationLanguage:
            [google.elements.transliteration.LanguageCode.MARATHI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(optionsMar);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['<%=txtMessage.ClientID%>']);


        }
        google.setOnLoadCallback(CallonLoad);
    </script>


    <script>


        function SetFirstHiddenID(id) {


            // alert(id);
            document.getElementById('<%=HdnField1.ClientID %>').value = id;

            //   alert(document.getElementById('<%=HdnField1.ClientID %>').value);



        }

        function SetStatus() {

            var FirstDiv, SecondDiv;

            // alert("hello")
            $('#collapse1').on('shown.bs.collapse', function () {
                $("#HdnField1").val(("1"));



            });

            $('#collapse1').on('hidden.bs.collapse', function () {


                //  alert(" div 1closed")
                $("#HdnField1").val(("0"));

            });




            $('#collapse2').on('shown.bs.collapse', function () {
                $("#HdnField2").val(("1"));


            });

            $('#collapse2').on('hidden.bs.collapse', function () {
                //  alert("div 2 closed")
                $("#HdnField1").val(("0"));

            });




        }


    </script>


    <%-- <script>
        $(document).ready(function () {
            alert("entered in Div");
            $('#collapse1').collapse('hide');
            $('#collapse2').collapse('hide');
        });


    </script>--%>
    <script>


        function checkStatus() {
            var isExpanded1 = $('#collapse1').hasClass('in');

            //Will return true if in the process of collapsing
            //isExpanded1 = $('#collapse1').hasClass('collapsing');

            ////Will return true if collapsed
            //isExpanded1 = $('#collapse1').hasClass('collapse');


            var isExpanded2 = $('#collapse2').hasClass('in');


            //Will return true if in the process of collapsing
            //isExpanded2 = $('#collapse2').hasClass('collapsing');

            ////Will return true if collapsed
            //isExpanded2 = $('#collapse2').hasClass('collapse');


            // alert(isExpanded1);
            //  alert(isExpanded2);


            var text = $('#isExpanded1').val();





            if (isExpanded1 == false && isExpanded2 == false) {

                $("#HdnField1").val(("0"));


                // alert( $("#HdnField1").val());
                // alert("False");
                return false;
            }
            else {


                //   alert("true");
            }

            return true;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="Div1">
                        <div class="portlet-title">
                            <div class="caption">
                                SMS Module
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab1">



                                                <div class="panel-group" id="accordion">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4 class="panel-title">
                                                                <a data-toggle="collapse" id="FirstDiv" data-parent="#accordion" href="#collapse1" onclick="SetFirstHiddenID(1);">Director and Share Message</a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapse1" class="panel-collapse collapse in">
                                                            <div class="panel-body">

                                                                <!-- First panel div start-->   
                                                                <div>

                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
                                                                            <label class="control-label col-md-2">Message Type :<span class="required"></span></label>
                                                                            <div class="col-md-2">
                                                                                <asp:DropDownList ID="ddlPrdType" runat="server" CssClass="form-control" OnTextChanged="ddlPrdType_TextChanged" AutoPostBack="true">
                                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                                    <asp:ListItem Value="1" Text="Director Message"></asp:ListItem>
                                                                                    <asp:ListItem Value="4" Text="Shares Message"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
                                                                            <label class="control-label col-md-2">Account Type :<span class="required"></span></label>
                                                                            <div class="col-md-2">
                                                                                <asp:DropDownList ID="ddlCustType" runat="server" CssClass="form-control" OnTextChanged="ddlCustType_TextChanged" AutoPostBack="true" Enabled="false">
                                                                                    <asp:ListItem Value="1" Text="ALL"></asp:ListItem>
                                                                                    <asp:ListItem Value="2" Text="Specific"></asp:ListItem>
                                                                                    <%-- <asp:ListItem Value="3" Text="From And To"></asp:ListItem>--%>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtFromCustNo" CssClass="form-control" runat="server" placeholder=" Customer Number" OnTextChanged="txtFromCustNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtFromCustName" placeholder="" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-4">
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtToCustNo" CssClass="form-control" runat="server" placeholder="To Customer Number"></asp:TextBox>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtToCustName" placeholder="" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
                                                                            <label class="control-label col-md-2">Message Language :<span class="required"></span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:RadioButton ID="rdbSMSMar" GroupName="RdbSMSGrp" runat="server" Text="Marathi" AutoPostBack="true" OnCheckedChanged="rdbSMSMar_CheckedChanged" />&nbsp;&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rdbSMSEng" GroupName="RdbSMSGrp" runat="server" Text="English" Checked="true" AutoPostBack="true" OnCheckedChanged="rdbSMSEng_CheckedChanged" />

                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
                                                                            <label class="control-label col-md-2">Enter Message </label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtMessage" CssClass="form-control" runat="server" onclick="CallonLoad()" TextMode="MultiLine" Columns="50" Rows="5" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtMessageEng" CssClass="form-control" runat="server" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
                                                                            <label class="control-label col-md-2">Date </label>
                                                                            <div class="col-lg-2">
                                                                                <asp:TextBox ID="txtMessageDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                                                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtMessageDate">
                                                                                </asp:TextBoxWatermarkExtender>
                                                                                <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtMessageDate">
                                                                                </asp:CalendarExtender>
                                                                            </div>




                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">

                                                                            <label class="control-label col-md-2">Hr </label>
                                                                            <div class="col-lg-1">

                                                                                <asp:TextBox ID="txtHr" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" MaxLength="2"></asp:TextBox>
                                                                            </div>
                                                                            <label class="control-label col-md-1">Min </label>
                                                                            <div class="col-lg-1">
                                                                                <asp:TextBox ID="txtMin" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" MaxLength="2"></asp:TextBox>

                                                                            </div>



                                                                        </div>
                                                                    </div>






                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-md-offset-2 col-md-8">
                                                                            <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Send Now" OnClick="Submit_Click" OnClientClick="Javascript:return Validate();" />
                                                                            <asp:Button ID="btnSubmitLater" runat="server" CssClass="btn btn-primary" Text="Send Later" OnClick="btnSubmitLater_Click" OnClientClick="Javascript:return Validate();" Visible="false" />

                                                                            <asp:HiddenField ID="HdnField1" runat="server" Value="0" />



                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <!-- first panel div ends-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <button type="button" onclick="checkStatus()" style="visibility: hidden">Check Status</button>
                                            <asp:Button runat="server" ID="btnCheckHidden" Text="CheckHiden" OnClientClick="javascript:return checkStatus()" OnClick="btnCheckHidden_Click" Visible="false" />
                                        </div>




                                    </div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
