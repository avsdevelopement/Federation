<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRDayBook.aspx.cs" Inherits="FrmRDayBook" %>
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
                                Day Book
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
                                                            <asp:RadioButtonList ID="Rdeatils"  RepeatDirection="Horizontal" style="width : 500px; " runat="server">
                                                                <asp:ListItem Text="Details" Value ="1" />
                                                                <asp:ListItem Text="Summary" Value ="2"  />
                                                                <asp:ListItem Text="SetWiseDetails" Value ="3"  />
                                                                <asp:ListItem Text="ProductWise" Value ="4"  />
                                                                <asp:ListItem Text="ALL Details" Value ="5"  />
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                 </div>
                                                 <div class="col-lg-12">
                                                        <div class="col-md-9">
                                                             <asp:CheckBox ID="CHK_SKIP_INT"  runat="server" Text="SKIP_INT AC" style="width:100px;" />
                                                             <asp:CheckBox ID="CHK_SKIP_DAILY" runat="server" Text="SKIP_DAILY AC" />
                                                        </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                 <div class="col-lg-12">
                                                      <div class="col-md-2">
                                                            <label class="control-label">Branch Code</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                             <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                 </div>
                                                     </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">As On Date</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                    
                                                 </div>
                                                    <div class="col-md-2">
                                                          <label class="control-label"> </label>
                                                     </div>
                                                        <div class="col-lg-12">
                                                        <div class="col-md-offset-3 col-md-12">
                                                        <div class="col-md-2">
                                                            <asp:Button ID="DayBook" runat="server" Text="Day Book Report" CssClass="btn btn-primary" OnClick="DayBook_Click" />
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
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

