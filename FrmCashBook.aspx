<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCashBook.aspx.cs" Inherits="FrmCashBook" %>

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

        function PrintStat() {
            debugger;
            var shell = new ActiveXObject("WScript.Shell");
            var path = '"C:/AVS/PrintT.bat"';
            shell.run(path, 1, false);
        }
        function DeleteStat() {
            debugger;
            var shell = new ActiveXObject("WScript.Shell");
            var path = '"C:/AVS/Delete.bat"';
            shell.run(path, 1, false);
        }




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnView" />
           

        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Cash Book
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
                                                        <div class="col-md-2">
                                                            <asp:RadioButtonList ID="Rdeatils" RepeatDirection="Horizontal" Style="width: 380px;" runat="server">
                                                                <asp:ListItem Text="Details" Value="1" />
                                                                <asp:ListItem Text="Summary" Value="2" />
                                                                <asp:ListItem Text="ALL Details" Value="3" />
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">

                                                        <div class="col-md-2">
                                                            <label class="control-label">From Date</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtFDT" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDT">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDT">
                                                            </asp:CalendarExtender>
                                                        </div>

                                                        <div class="col-lg-7">
                                                            <div class="col-md-3">
                                                                <label class="control-label">To Date </label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtTDT" Width="110%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TxtTDT_Extender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDT">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="TxtTDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDT">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                             <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <label class="control-label">Please enter text report name <span class="required">*</span></label>
                                                </div>
                                        <div class="col-md-3">
                                                    <asp:TextBox ID="TxtRptName" placeholder="Please enter text report name"  CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                                                  
                                              </div>


                                    </div>
                                </div>
                                        
                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 12px;">
                                                        <div class="col-lg-6">
                                                            <asp:Button ID="Submit" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="Submit_Click" />
                                                            <asp:Button ID="Print" runat="server" Text="Cash Book Report" CssClass="btn btn-primary" OnClick="Print_Click" />
                                                            
                                                            <asp:Button ID="BtnView" runat="server" Text="Download" CssClass="btn btn-primary" OnClick="BtnView_Click" OnClientClick="DeleteStat();"/>
                                                            <asp:Button ID="Button1" runat="server" Text="Print" CssClass="btn btn-primary" OnClientClick="PrintStat();" />


                                                            <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />
                                                        
                                                            <asp:HiddenField ID="location" runat="server" />
                                                            
                                                              <br />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12" style="height: 50%">
                                                <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="GrdCashBook" DataKeyNames="id"
                                                                        runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="True"
                                                                        OnRowDataBound="GrdCashBook_RowDataBound" ShowHeader="False" OnRowCreated="GrdCashBook_RowCreated"
                                                                        EditRowStyle-BackColor="#FFFF99" EmptyDataText="Record Not Found">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="id" HeaderText="Id" Visible="false" ItemStyle-Width="60">
                                                                                <ItemStyle Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="CSUBGL" HeaderText="Sub Gl" ItemStyle-Width="60">
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="CAACNO" HeaderText="Account No" ItemStyle-Width="60">
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="CACCNAME" HeaderText="Account Name" ItemStyle-Width="80">
                                                                                <ItemStyle Width="80px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="CAMOUNT" HeaderText="Amount" ItemStyle-Width="60" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DSUBGL" HeaderText="Sub Gl" ItemStyle-Width="60">
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DAACNO" HeaderText="Account No" ItemStyle-Width="60">
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DACCNAME" HeaderText="Account Name" ItemStyle-Width="80">
                                                                                <ItemStyle Width="80px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="DAMOUNT" HeaderText="Amount" ItemStyle-Width="60" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <SelectedRowStyle BackColor="#66FF99" />
                                                                        <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                                                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                                    </asp:GridView>
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="padding: 1.4%; height: 200px;">
                                            <!-- after click show passbook details -->
                                            <div style="border: 1px solid #3598dc">
                                                <div class="portlet-title">
                                                    <div class="caption" style="text-align: center;">
                                                        <strong>Cash Book Details</strong>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12" style="margin-top: 12px;">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Opening Balance</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtOPBAL" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Closing Balance</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtCLBAL" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px;">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Total</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtTotal" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- after click show passbook details -->
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


