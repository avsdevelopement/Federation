<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDayActivity.aspx.cs" Inherits="FrmDayActivity" %>
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
    function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;
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
                        Day Activity View
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
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-lg-6">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="Rdb_No" runat="server" RepeatDirection="Horizontal" Style="width: 300px;" OnSelectedIndexChanged="Rdb_No_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="All Branch" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Specific Branch" Value="2"></asp:ListItem>
                                                       
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                </div>

                                   <div class="row" id="Div_SPECIFIC" runat="server">
                               <div class="row" style="margin-bottom: 10px;">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch Name <span class="required">*</span></label>
                                                 <div class="col-md-2">
                                                    <asp:TextBox ID="txtBrCode" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtBrCode_TextChanged" AutoPostBack="true" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                 </div>
                                            </div>

                             <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                               <label class="control-label col-md-2">From Date <span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtfromdate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfromdate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfromdate">
                                </asp:CalendarExtender>
                            </div>
                                <label class="control-label col-md-1">To Date <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txttodate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txttodate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txttodate">
                                    </asp:CalendarExtender>
                                </div>
                             </div>
                               </div>

                             <div class="row" style="margin: 7px 0 7px 0; text-align:center">
                                     <div class="col-lg-6">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="Rdb_Status" runat="server" RepeatDirection="Horizontal" Style="width: 600px;" OnSelectedIndexChanged="Rdb_Status_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Day Open" Value="S_OPEN"></asp:ListItem>
                                                        <asp:ListItem Text="Day Close" Value="S_CLOSE"></asp:ListItem>
                                                        <asp:ListItem Text="Branch HandOver" Value="S_BRHND"></asp:ListItem>
                                                        <asp:ListItem Text="Day Re-Open" Value="S_REOPEN"></asp:ListItem>
                                                         <asp:ListItem Text="All Activity" Value="S_All Activity"></asp:ListItem>
                                                         <asp:ListItem Text="User Report" Value="6"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                         </div>
                                    </div>
           </div>
    </div>

   <div class="row" id="Div_ALL" runat="server">
       <div class="row" style="margin-bottom: 10px;">    
             <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                               <label class="control-label col-md-2">From Date <span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtfrmdateS" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfrmdateS">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfrmdateS">
                                </asp:CalendarExtender>
                            </div>
                                <label class="control-label col-md-1">To Date <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txttodateS" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txttodateS">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txttodateS">
                                    </asp:CalendarExtender>
                                </div>
                             </div>
                               </div>

           <div class="row" style="margin: 7px 0 7px 0; text-align:center">
                                     <div class="col-lg-8">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="Rbd_all" runat="server" RepeatDirection="Horizontal" Style="width: 600px;" OnSelectedIndexChanged="Rbd_all_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Day Open" Value="A_OPEN" ></asp:ListItem>
                                                        <asp:ListItem Text="Day Close" Value="A_CLOSE"></asp:ListItem>
                                                        <asp:ListItem Text="Branch HandOver" Value="ALL_BRHND"></asp:ListItem>
                                                        <asp:ListItem Text="Day Re-Open" Value="A_REOPEN"></asp:ListItem>
                                                         <asp:ListItem Text="All Activity" Value="A_All Activity"></asp:ListItem>
                                                        <asp:ListItem Text="User Report" Value="6"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                         </div>
              </div>
           </div>
    </div>      
  
                                <div class="row" id="Div_Buttons" runat="server">
                                    <div class="row" style="margin: 7px 0 7px 0; text-align:center">
                                            <div class="col-lg-12">
                                                <div class="col-md-6">
                                             <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" />
                                                   <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" />
                                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" />
                                                   <asp:Button ID="BtnReport" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BtnReport_Click" />
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
                                <asp:GridView ID="grddayactivity" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                         <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Serial" runat="server" Text='<%# Container.DataItemIndex+1%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SRNO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="BRCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BRANCHNAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BRANCHNAME" runat="server" Text='<%# Eval("BRANCHNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="DAYBEGINDATE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DAYBEGINDATE" runat="server" Text='<%# Eval("DAYBEGINDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DAYOPTIME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DAYOPTIME" runat="server" Text='<%# Eval("DAYOPTIME") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField><asp:TemplateField HeaderText="DAYOPBY" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DAYOPBY" runat="server" Text='<%# Eval("DAYOPBY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DAYCLOSEDATE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DAYCLOSEDATE" runat="server" Text='<%# Eval("DAYCLOSEDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="DAYCLOSEDTIME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DAYCLOSEDTIME" runat="server" Text='<%# Eval("DAYCLOSEDTIME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="DAYCLOSEDBY" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DAYCLOSEDBY" runat="server" Text='<%# Eval("DAYCLOSEDBY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="STATUS" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
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

