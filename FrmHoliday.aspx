<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmHoliday.aspx.cs" Inherits="FrmHoliday" %>

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
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1 " style="height: 50%">
                <div class="portlet-title">
                    <div class="caption">
                        Holiday Details
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard" style="height: 500px">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="portlet-body">

                                        <div class="row" style="margin: 7px 0 7px 0" align="center">
                                            <div class="col-lg-12" align="center">
                                                <div class="col-lg-12">
                                                    <div class="col-md-3"></div>
                                                    <div class="col-md-1">
                                                        <label class="control-label">Year</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="DDlYear" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane active" id="tab1">
                                            <asp:Table ID="TblDiv_MainWindow" runat="server" Width="100%">
                                                <asp:TableRow ID="Tbl_R1" runat="server" Width="100%">
                                                    <asp:TableCell ID="Tbl_c1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">

                                                                <div class="col-md-12">
                                                                    <div class="col-md-7">
                                                                        <asp:RadioButton ID="RbtnWeekly" runat="server" Text="Weekly Off" OnCheckedChanged="RbtnWeekly_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                                                    </div>
                                                                    <div id="div_Day" visible="false" runat="server">
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <label class="control-label col-md-2">Day<span class="required">*</span></label>
                                                                                <div class="col-md-4">
                                                                                    <asp:DropDownList ID="DdlDay" runat="server" OnSelectedIndexChanged="DdlDay_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="table-scrollable" style="width: 100%; height: 250px; overflow-x: auto; overflow-y: auto">
                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>
                                                                           
                                                                                    <asp:Label ID="lblDay" runat="server"></asp:Label>
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:TableCell>
                                                    <asp:TableCell ID="TableCell1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">
                                                        <div class="col-md-1"></div>

                                                        <div class="col-md-12">
                                                            <div class="col-md-6">
                                                                <asp:RadioButton ID="RbtnHoliday" runat="server" Text="Holiday" OnCheckedChanged="RbtnHoliday_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                                            </div>
                                                            <div id="div_holiday" visible="false" runat="server">
                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">

                                                                        <label class="control-label col-md-2">Date<span class="required">*</span></label>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="Txtdate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" MaxLength="10" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="Txtdate">
                                                                            </asp:TextBoxWatermarkExtender>
                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txtdate">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Reason<span class="required">*</span></label>
                                                                        <div class="col-md-8">
                                                                            <asp:TextBox ID="Txtreason" CssClass="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="table-scrollable" style="width: 100%; height: 250px; overflow-x: auto; overflow-y: auto">
                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:GridView ID="GrdHoliday" runat="server" Visible="false"
                                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                        EditRowStyle-BackColor="#FFFF99"
                                                                                        PagerStyle-CssClass="pgr" Width="70%">
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Id" Visible="true" HeaderStyle-Width="10px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="HOLIDAYDATE" Visible="true" HeaderStyle-Width="20px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="HOLIDAYDATE" runat="server" Text='<%# Eval("HOLIDAYDATE") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="REASON" Visible="true" HeaderStyle-Width="40px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="REASON" runat="server" Text='<%# Eval("REASON") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Delete" Visible="true" HeaderStyle-Width="10px">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("HOLIDAYDATE")+","+Eval("REASON")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>

                                                                                        </Columns>
                                                                                        <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>

                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-offset-3 col-md-9">
                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" />
                                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" />
                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>


        </div>
    </div>
</asp:Content>

