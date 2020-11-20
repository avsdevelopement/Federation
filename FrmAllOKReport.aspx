<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAllOKReport.aspx.cs" Inherits="FrmAllOKReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                        All Ok Report
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
                                    <div style="border: 1px solid #3598dc">
                                        
                                        <div id="Div1" class="row" style="margin: 7px 0 7px 0" runat="server">
                                             <div class="col-lg-12">
                                                <label class="control-label col-md-2">As on Date</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAsonDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtAsonDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                      <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAsonDate">
                                                            </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                             <div class="col-lg-12">
                                                <label class="control-label col-md-2">Export</label>
                                                   <div class="col-md-2">
                                            <asp:DropDownList ID="ddlExport" runat="server" CssClass="form-control">                                                              
                                                            </asp:DropDownList>
                                                       </div>
                                                 </div>

                                        </div>                                        
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Submit_Click" />
                                        
                                        &nbsp;<asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click"/>
                                        <br />
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
    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdAllOK" runat="server" 
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99" 
                                    OnPageIndexChanging="grdAllOK_PageIndexChanging" AllowPaging="true"
                                    PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PRODUCT TYPE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GLNAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="GLNAME" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AS PER P.L" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AVSM" runat="server" Text='<%# Eval("AVSM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AS PER G.L" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AVSB" runat="server" Text='<%# Eval("AVSB") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="DIFFERENCE PL-GL" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DIFF" runat="server" Text='<%# Eval("DIFF") %>'></asp:Label>
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
</asp:Content>

