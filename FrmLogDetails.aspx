<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLogDetails.aspx.cs" Inherits="FrmLogDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<style>
.collage {
    float: left;
    margin-right: 5px;
    width: 150px;
    height: 40px;
}
    </style>--%>
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Log Details
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2"> Branch Code <span class="required">*</span></label>
                                                 <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtBRCD" runat="server" CssClass="form-control" OnTextChanged="TxtBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtBRCDName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                </div>
                                             </div>

                                       
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2"> From Date <span class="required">*</span></label>
                                                 <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtFdate" runat="server" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="DD/MM/YYYY" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                      <asp:CalendarExtender ID="TxtFdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFdate">
                                                        </asp:CalendarExtender>
                                                </div>
                                                 <label class="control-label col-md-1"> To Date <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtTDate" runat="server" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="DD/MM/YYYY" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                     <asp:CalendarExtender ID="TxtTDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                        </asp:CalendarExtender>
                                                </div>
                                                </div>
                                             </div>
                                         <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2"> Activity <span class="required">*</span></label>
                                                <div class="col-lg-3">
                                                    <asp:DropDownList ID="ddlActivity" runat="server" CssClass="form-control" AutoPostBack="true" overflow="scroll"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                         
                                <div class="row">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="Btn_Submit_Click"/>
                                         <asp:Button ID="Btnreport" runat="server" Text="Report" CssClass="btn btn-success" OnClick="Btnreport_Click"/>
                                        <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="Btn_ClearAll_Click" />
                                        <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Btn_Exit_Click" />
                                    </div>
                                    <div class="col-lg-2">
                                        
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
            <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdLogDetails" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BRCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ENTRYDATE">
                                            <ItemTemplate>
                                                <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="VID">
                                            <ItemTemplate>
                                                <asp:Label ID="VID" runat="server" Text='<%# Eval("VID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACTIVITY">
                                            <ItemTemplate>
                                                <asp:Label ID="ACTIVITY" runat="server" Text='<%# Eval("ACTIVITY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="OLDVALUE">
                                            <ItemTemplate>
                                                <asp:Label ID="OLDVALUE" runat="server" Text='<%# Eval("OLDVALUE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="NEWVALUE">
                                            <ItemTemplate>
                                                <asp:Label ID="NEWVALUE" runat="server" Text='<%# Eval("NEWVALUE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="MID">
                                            <ItemTemplate>
                                                <asp:Label ID="MID" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="ACTNO">
                                            <ItemTemplate>
                                                <asp:Label ID="ACTNO" runat="server" Text='<%# Eval("ACTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                       
                                   
                                       
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
    
</asp:Content>

