<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmODList.aspx.cs" Inherits="FrmODList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isvalidate()
        {
            var Sec, FBrcd, TBrcd, FSubgl, TSubgl, FAcc, TAcc;
            Sec = document.getElementById('<%=DdlActivity.ClientID%>').value;
            FBrcd = document.getElementById('<%=TxtFBrID.ClientID%>').value;
            TBrcd = document.getElementById('<%=TxtTBrID.ClientID%>').value;
            FSubgl = document.getElementById('<%=TxtFSubgl.ClientID%>').value;
            TSubgl = document.getElementById('<%=TxtTSubgl.ClientID%>').value;
            FAcc = document.getElementById('<%=TxtFACID.ClientID%>').value;
            TAcc = document.getElementById('<%=TxtTACID.ClientID%>').value;
            var message = '';

            if (Sec == "0")
            {
                message = 'Select Secure / Unsecure Type ...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=DdlActivity.ClientID%>').focus();
                return false;
            }
            if (FBrcd == "")
            {
                message = 'Enter From Branch....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFBrID.ClientID%>').focus();
                return false;
            }
            if (TBrcd == "")
            {
                message = 'Enter To Branch....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTBrID.ClientID%>').focus();
                return false;
            }
            if (FSubgl == "")
            {
                message = 'Enter From Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFSubgl.ClientID%>').focus();
                return false;
            }
            if (TSubgl == "")
            {
                message = 'Enter To Product Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTSubgl.ClientID%>').focus();
                return false;
            }
            if (FAcc == "")
            {
                message = 'Enter From AccNo...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFACID.ClientID%>').focus();
                return false;
            }
            if (TAcc == "")
            {
                message = 'Enter To AccNo...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTACID.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function FormatIt(obj)
        {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

        <%-- Only alphabet --%>
        function onlyAlphabets(e, t)
        {
            try
            {
                if (window.event)
                {
                    var charCode = window.event.keyCode;
                }
                else if (e)
                {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }
        <%-- Only Numbers --%>
        function isNumber(evt)
        {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        OverDue Account List
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">A/C Type</label>
                                            <div class="col-md-9">
                                                <asp:RadioButtonList ID="rbtnType" runat="server" RepeatDirection="Horizontal" CssClass="radio-list">
                                                    <asp:ListItem Text="Only Court File" Value="1" style="margin: 15px;" Selected="True" />
                                                    <asp:ListItem Text="Normal A/C" Value="2" style="margin: 25px;" />
                                                    <asp:ListItem Text="All A/C's" Value="3" style="margin: 25px;" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <asp:RadioButtonList ID="Rdeatils" RepeatDirection="Horizontal" Style="width: 380px;" runat="server">
                                                    <asp:ListItem Text="Details" Value="1" />
                                                    <asp:ListItem Text="Summary" Value="2" />
                                                    <asp:ListItem Text="Ref_AgentWise" Value="3" />
                                                    <asp:ListItem Text="Ref_CustWise" Value="4" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">From Branch</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">To Branch</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">From Product</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFSubgl" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">To Product</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTSubgl" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">From Account</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFACID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">To Account</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTACID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">AsOnDate</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAsonDate" onkeyup="FormatIt(this)" MaxLength="10" onkeypress="javascript:return isNumber(event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtAsonDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAsonDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Reference / Recomm</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRef" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Select Type </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlODActivity" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">OD Amount</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtODAmt" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Select Type </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlODInstActivity" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">OD Installment</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtODInst" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">From Sanction</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFSan" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">To Sanction</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTSan" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Sanction From Date <span class="required"></span></label>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-2">Sanction To Date <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-2">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Account Status</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlAccActivity" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Sec/UnSecure Type<span class="required">* </span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlActivity" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <asp:CheckBox ID="CHK_With_Address" runat="server" Text="With_Address" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label"></label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="btnClear_Click" OnClientClick="Javascript:return validate();" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="btnExit_Click" OnClientClick="Javascript:return validate();" />
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

