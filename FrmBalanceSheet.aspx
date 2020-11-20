<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmBalanceSheet.aspx.cs" Inherits="FrmBalanceSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                BalanceSheet
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2" >
                                                            <label class="control-label" style="padding-left: 22px">Branch Code</label>
                                                        </div>
                                                        <div class="col-md-2" >
                                                            <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0" align="center">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-5">
                                                            <asp:RadioButtonList ID="RBSel" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RBSel_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="1">&nbsp;As On Date</asp:ListItem>
                                                                <asp:ListItem Value="2" style="padding-left: 10px">&nbsp;N-Format</asp:ListItem>
                                                                <asp:ListItem Value="3" style="padding-left: 10px">&nbsp;Marathi BS</asp:ListItem>
                                                                <asp:ListItem Value="4" style="padding-left: 10px">&nbsp;N-Format Marathi BS</asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>
                                                         <div class="col-md-2" style="margin-top:2px">
                                                            <label class="control-label" id="lblCurrentLang" runat="server" visible="false" >Current Language Is</label>
                                                        </div>
                                                        <div class="col-md-2"  style="margin-top:2px">
                                                            

                                                            <asp:TextBox ID="txtCurrentLang" runat="server" CssClass="form-control" Enabled="false" Visible="false"></asp:TextBox>

                                                        </div>


                                                    </div>
                                                </div>


                                                <%--  <div class="col-lg-12">
                                                          <div class="col-md-4" style="margin-left:35px" >
                                                        
                                                        <div class="col-md-6" >
                                                            <asp:RadioButtonList ID="RBSel" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RBSel_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="1">&nbsp;As On Date</asp:ListItem>
                                                                <asp:ListItem Value="2" style="margin-left:10px">&nbsp;N-Format</asp:ListItem>

                                                            </asp:RadioButtonList>


                                                        </div>

                                                                  <div class="col-md-4" >
                                                          <asp:TextBox ID="txtCurrentLang" runat="server"  CssClass="form-control" ></asp:TextBox>


                                                        </div>
                                                    </div>
                                                </div>--%>

                                                <div class="row" style="margin: 7px 0 7px 0" align="center" runat="server" visible="false">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-4" style="width: 500px">
                                                            <div class="col-md-4">
                                                                <label class="control-label" style="margin-left: -14px">Report Language</label>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:RadioButtonList ID="rdbLangSelection" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbLangSelection_SelectedIndexChanged" AutoPostBack="true" Style="font-size: 22px;">


                                                                    <asp:ListItem Value="3" style="margin-left: 25px">&nbsp;Shivaji&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp </asp:ListItem>
                                                                    <asp:ListItem Value="4">&nbsp;Sarjudas&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp </asp:ListItem>
                                                                    <asp:ListItem Value="5" Selected="True">&nbsp;&nbsp;Default</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div runat="server" id="divOnDate" visible="false">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label"  style="margin-left:20px">As On Date</label>
                                                            </div>
                                                            <div class="col-md-3" style="margin-left: -50px">
                                                                <asp:TextBox ID="TxtFDT" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDT">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDT">
                                                                </asp:CalendarExtender>
                                                            </div>

                                                            <div class="col-lg-7">
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <div class="col-md-7" >
                                                                <asp:CheckBox ID="CHK_SKIP_BR" runat="server" Text="SKIP_Branch ADJ" Style="width: 100px;margin-left:20px" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                            <div class="col-lg-6">
                                                                <asp:Button ID="Submit" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="Submit_Click" />
                                                                <asp:Button ID="Report" runat="server" Text="Balance Sheet Report" OnClick="Report_Click" CssClass="btn btn-primary" />
                                                                <asp:Button ID="TextReport" runat="server" CssClass="btn btn-primary" Text="Download Text Report" OnClick="TextReport_Click"
                                                                    OnClientClick="return UserConfirmation();" />
                                                                <br />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div runat="server" id="divDate" visible="false">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label" style="margin-left:20px" >From Date</label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtFromDate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server" ></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <label class="control-label">To Date</label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtToDate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtToDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <div class="col-lg-7">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="col-md-5">
                                                            <asp:CheckBox ID="CHK_SKIP_BRN" runat="server" Text="SKIP_Branch ADJ" Style="width: 100px; margin; margin-left: 11px" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                            <div class="col-lg-6">
                                                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                                                <asp:Button ID="btnBal" runat="server" Text="Balance Sheet Report" OnClick="btnBal_Click" CssClass="btn btn-primary" />
                                                                <asp:Button ID="btnDown" runat="server" CssClass="btn btn-primary" Text="Download Text Report" OnClick="btnDown_Click"
                                                                    OnClientClick="return UserConfirmation();" />
                                                                <br />
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
            </div>
            <div class="row" style="margin: 7px 0 7px 0">
                <div class="col-lg-12" style="height: 50%">
                    <div class="table-scrollable" style="height: 450px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">

                        <asp:GridView ID="GrdBalance" DataKeyNames="id"
                            runat="server" AutoGenerateColumns="False" Width="100%"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="True"
                            OnRowDataBound="GrdBalance_RowDataBound" ShowHeader="False"
                            EditRowStyle-BackColor="#FFFF99" EmptyDataText="Record Not Found">
                            <Columns>
                                <asp:BoundField DataField="CRGL" ItemStyle-Width="80">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRGLN" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRSGL" ItemStyle-Width="60">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRGLRID" ItemStyle-Width="80">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRGLTP" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRBAL" ItemStyle-Width="60">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="DRGL" ItemStyle-Width="60">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRSGL" ItemStyle-Width="80">
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRGLN" ItemStyle-Width="60">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRGLRID" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRGLTP" ItemStyle-Width="60">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRBAL" ItemStyle-Width="60">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="#66FF99" />
                            <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        </asp:GridView>
                        <asp:GridView ID="GridAll" DataKeyNames="id"
                            runat="server" AutoGenerateColumns="False" Width="100%"
                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="True"
                            OnRowDataBound="GridAll_RowDataBound" ShowHeader="False"
                            EditRowStyle-BackColor="#FFFF99" EmptyDataText="Record Not Found">
                            <Columns>
                                <asp:BoundField DataField="CRGL" ItemStyle-Width="80">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRGLN" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRSGL" ItemStyle-Width="60">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRGLRID" ItemStyle-Width="80">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CRGLTP" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="PCRBAL" ItemStyle-Width="60">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRBAL" ItemStyle-Width="60">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="DRGL" ItemStyle-Width="60">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRSGL" ItemStyle-Width="80">
                                    <ItemStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRGLN" ItemStyle-Width="60">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRGLRID" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DRGLTP" ItemStyle-Width="60">
                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="PDRBAL" ItemStyle-Width="60">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="DRBAL" ItemStyle-Width="60">
                                    <ItemStyle Width="60px" />
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="#66FF99" />
                            <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

