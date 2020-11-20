<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSavingIntCal.aspx.cs" Inherits="FrmSavingIntCal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
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

        function ToBrCode(obj) {
            debugger;
            var FrBrCode = document.getElementById('<%=ddlFromBrName.ClientID%>').value || 0;
            var ToBrCode = document.getElementById('<%=ddlToBrName.ClientID%>').value || 0;

            if (parseFloat(ToBrCode) < parseFloat(FrBrCode)) {
                message = 'Select to branch greter than from branch...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtToBranch.ClientID %>').value = "";
                document.getElementById('<%=ddlToBrName.ClientID%>').focus();
                return false;
            }
        }

        function ToAccount(obj) {
            debugger;
            var FrAccNo = document.getElementById('<%=TxtFAccno.ClientID%>').value || 0;
            var ToAccNo = document.getElementById('<%=TxtTAccno.ClientID%>').value || 0;

            if (parseFloat(ToAccNo) < parseFloat(FrAccNo)) {
                message = 'Enter to account number greter than from account number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTAccno.ClientID %>').value = "";
                document.getElementById('<%=TxtTAccno.ClientID%>').focus();
                return false;
            }
        }

        function FromDate(obj) {
            debugger;
            var FrDate = document.getElementById('<%=TxtFDate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=WorkingDate.ClientID%>').value;

            var frdate = FrDate.substring(0, 2);
            var frmonth = FrDate.substring(3, 5);
            var fryear = FrDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (frmyDate > wmyDate) {
                message = 'From date not allow greter than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFDate.ClientID %>').value = "";
                document.getElementById('<%=TxtFDate.ClientID%>').focus();
                return false;
            }
        }

        function ToDate(obj) {
            debugger;
            var FrDate = document.getElementById('<%=TxtFDate.ClientID%>').value || 0;
            var ToDate = document.getElementById('<%=TxtTDate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=WorkingDate.ClientID%>').value;

            var frdate = FrDate.substring(0, 2);
            var frmonth = FrDate.substring(3, 5);
            var fryear = FrDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var todate = ToDate.substring(0, 2);
            var tomonth = ToDate.substring(3, 5);
            var toyear = ToDate.substring(6, 10);
            var tomyDate = new Date(toyear, tomonth - 1, todate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (tomyDate > wmyDate) {
                message = 'To date not allow greter than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTDate.ClientID %>').value = "";
                document.getElementById('<%=TxtTDate.ClientID%>').focus();
                return false;
            }
            else if (tomyDate < frmyDate) {
                message = 'Enter to date greter than from date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTDate.ClientID %>').value = "";
                document.getElementById('<%=TxtTDate.ClientID%>').focus();
                return false;
            }
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Saving Interest Calculation
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label ">Select Type<span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlCalType" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="--Select--" Value="0" />
                                                <asp:ListItem Text="Minimum Balance" Value="1" />
                                                <asp:ListItem Text="Daily Product" Value="2" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label ">From Branch<span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlFromBrName" CssClass="form-control" OnSelectedIndexChanged="ddlFromBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtFrBranch" Enabled="false" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label ">To Branch<span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlToBrName" onblur="ToBrCode()" CssClass="form-control" OnSelectedIndexChanged="ddlToBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtToBranch" Enabled="false" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label ">Product Code :<span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtPtype" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtPtype_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-icon">
                                                <i class="fa fa-search"></i>
                                                <asp:TextBox ID="TxtPname" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="TxtPname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPname" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label ">From Date<span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtFDate" onblur="FromDate()" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label ">To Date<span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtTDate" onblur="ToDate()" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label ">From Account<span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtFAccno" onkeypress="javascript:return isNumber (event)" PlaceHolder="From A/C No" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label ">To Account<span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtTAccno" onblur="ToAccount()" onkeypress="javascript:return isNumber (event)" PlaceHolder="To A/C No" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-md-2">
                                            <input type="hidden" id="WorkingDate" runat="server" value="" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-11">
                                        <div class="col-lg-1"></div>
                                        <asp:RadioButtonList ID="RdbSelect2" runat="server" RepeatDirection="Horizontal" CssClass="radio-list" TabIndex="8">
                                            <asp:ListItem Text="Int Report" Value="IR" style="margin: 15px;" Selected="True"> </asp:ListItem>
                                            <asp:ListItem Text="Int Post" Value="IP" style="margin: 25px;"> </asp:ListItem>
                                            <asp:ListItem Text="Post Report" Value="PR" style="margin: 25px;"> </asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                </div>

                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="BtnCalculate" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="BtnCalculate_Click" />
                                        <asp:Button ID="TrailEntry" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Trial Run" CssClass="btn btn-primary" OnClick="TrailEntry_Click" />
                                        <asp:Button ID="ApplyEntry" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Apply Entry" CssClass="btn btn-success" OnClick="ApplyEntry_Click" />
                                        <asp:Button ID="Report" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Report" CssClass="btn btn-Primary" OnClick="Report_Click" />
                                        <asp:Button ID="BtnRecalculate" runat="server" Text="Recalculate" CssClass="btn btn-primary" OnClick="BtnRecalculate_Click" />
                                        <asp:Button ID="BntClear" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="BntClear_Click" />
                                        <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdFDInt" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="GrdFDInt_PageIndexChanging"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <%-- <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EDATE" runat="server" Text='<%# Eval("EDT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="PRD CODE">
                                            <ItemTemplate>
                                                <asp:Label ID="PRD" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACCNO">
                                            <ItemTemplate>
                                                <asp:Label ID="AC" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CUSTNAME">
                                            <ItemTemplate>
                                                <asp:Label ID="parti" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="INT AMT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="DEBIT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="DEPOSIT DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="FDATE" runat="server" Text='<%# Eval("FDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MATURITY DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="TDATE" runat="server" Text='<%# Eval("TDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DAYS">
                                            <ItemTemplate>
                                                <asp:Label ID="DDIFF" runat="server" Text='<%# Eval("DAYSDF") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RATE">
                                            <ItemTemplate>
                                                <asp:Label ID="RATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PRN AMT">
                                            <ItemTemplate>
                                                <asp:Label ID="DAMT" runat="server" Text='<%# Eval("DPAMT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BRCD">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
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

    <div id="alertModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </p>

                </div>
                <div class="modal-footer">
                    <button id="btnClose" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

