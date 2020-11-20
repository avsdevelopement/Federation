<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmAVS5057.aspx.cs" Inherits="FrmAVS5057" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function Validate() {
            var TxtAccT = document.getElementById('<%=TxtAccType.ClientID%>').value;
            var TxtAccno = document.getElementById('<%=TxtAccno.ClientID%>').value;
            var TxtFDT = document.getElementById('<%=TxtFDate.ClientID%>').value;
            var TxtTDt = document.getElementById('<%=TxtTDate.ClientID%>').value;
            var BRCD = document.getElementById('<%=TxtBRCD.ClientID%>').value;


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
    <script>
        //These functions go between the tags. //Begin function checkdate 
        function checkdate(input) {
            var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity 
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please correct and submit again.")
                input.focus()
            }
            else { //Detailed check for valid date ranges 
                var dayfield = input.value.split("/")[0]
                var monthfield = input.value.split("/")[1]
                var yearfield = input.value.split("/")[2]
                var dayobj = new Date(yearfield, monthfield - 1, dayfield)
                if ((dayobj.getDate() != dayfield) || (dayobj.getMonth() + 1 != monthfield) || (dayobj.getFullYear() != yearfield)) {
                    alert("Invalid Day, Month, or Year range detected. Please correct and submit again.");
                    input.value = "";
                }
                else {
                    returnval = true
                }
            }
            if (returnval == false) input.select()
            return returnval
        }
        function checkEnteredDates(stdateval, endateval) { //seperate the year,month and day for the first date 
            var stryear1 = stdateval.substring(6);
            var strmth1 = stdateval.substring(0, 2);
            var strday1 = stdateval.substring(5, 3);
            var date1 = new Date(stryear1, strmth1, strday1);
            //seperate the year,month and day for the second date 
            var stryear2 = endateval.substring(6);
            var strmth2 = endateval.substring(0, 2);
            var strday2 = endateval.substring(5, 3);
            var date2 = new Date(stryear2, strmth2, strday2);
            var datediffval = (date2 - date1) / 864e5;
            if (datediffval <= 0) {
                alert("Start date must be prior to end date");
                return false;
            } return true;
        }
        //Begin function validDate 
        //This makes sure that even if it somehow got past the original error check, 
        //the report won't run unless the date format is correct. 
        function validDate(ctrl) {
            if (checkdate(document.getElementById("TxtFDate")) && checkdate(document.getElementById("TxtTDate")))
            { OnExecute(ctrl) }
            if (checkEnteredDates((document.getElementById("TxtFDate")), (document.getElementById("TxtTDate"))) == true)
            { OnExecute(ctrl) }
        } //End function validDate - 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                      Saving Account Close Process
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Branch Code<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBRCD" OnTextChanged= "TxtBRCD_TextChanged" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtBrname" CssClass="form-control" runat="server" Style="text-transform: uppercase;" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Clear Balance : <span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtClearBal" placeholder="Clear Balance" CssClass="form-control" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Product Code<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtAccType" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged= "TxtAccType_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtATName" CssClass="form-control" runat="server" OnTextChanged= "TxtATName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtATName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="getglname" CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                    <label class="control-label col-md-2">UnClear Balance<span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtUnClearBal" placeholder="UnClear Balance" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Account Number <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtAccno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged= "TxtAccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtAccHName" CssClass="form-control" runat="server" OnTextChanged= "TxtAccHName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccHName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                    <label class="control-label col-md-2">Opening Date <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtOPDT" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From Date <span class="required"></span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <label class="control-label col-md-2">To Date <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <label class="control-label col-md-2">A/C Status<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtACStatus" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                             <div id="Multiple" runat="server" visible="false">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Int <span class="required"></span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtInt" placeholder="Int"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" ></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Service Charges <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtSerCharges" placeholder="Service Charges"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" ></asp:TextBox>
                                                       
                                                    </div>
                                                    <label class="control-label col-md-2">PassBook Charges <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtPassCharges" CssClass="form-control"  runat="server"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                </div>
                                           

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Check" OnClick= "Submit_Click" OnClientClick="Javascript:return Validate();" />
                                                    <%--<asp:Button ID="Btn_UnClearBal" runat="server" CssClass="btn btn-primary" Text="UnClear Balance" OnClientClick="Javascript:return Validate();" OnClick= "Btn_UnClearBal_Click" />--%>
                                                    <asp:Button ID="Btn_Clear" runat="server" CssClass="btn btn-primary" Text="Clear All" OnClick= "Btn_Clear_Click" />
                                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick= "btnPrint_Click" OnClientClick="Javascript:return Validate();" />
                                                    <asp:Button ID="Btn_Exit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick= "Btn_Exit_Click" />
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
    </div>
    <div class="row" id="Div_CLEARBAL" runat="server">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <asp:Label ID="Label1" runat="server" Text="Clear Statement" Style="font-family: Verdana; font-size: medium;" Font-Bold="true"></asp:Label>
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdAccountSTS" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging= "GrdAccountSTS_PageIndexChanging"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="EDATE" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="PARTI" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Particulars2">
                                            <ItemTemplate>
                                                <asp:Label ID="PARTI1" runat="server" Text='<%# Eval("PARTI1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cheque/Refrence">
                                            <ItemTemplate>
                                                <asp:Label ID="INSTNO" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SetNo">
                                            <ItemTemplate>
                                                <asp:Label ID="SETNO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DEBIT">
                                            <ItemTemplate>
                                                <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BALANCE" HeaderStyle-BackColor="YellowGreen" ItemStyle-BackColor="YellowGreen">
                                            <ItemTemplate>
                                                <asp:Label ID="BALANCE" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr/Cr">
                                            <ItemTemplate>
                                                <asp:Label ID="DrCrIndicator" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
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

