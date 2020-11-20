<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmActiveMem.aspx.cs" Inherits="FrmActiveMem" %>
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
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Active Member Report
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
                                                    <label class="control-label col-md-2">From Date <span class="required"></span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox> 
                                                          <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:CalendarExtender>
                                                    </div>
                                                    <label class="control-label col-md-2">To Date <span class="required"></span></label>
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
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick= "btnSubmit_Click" OnClientClick="Javascript:return Validate();" />
                                                    <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick= "btnExit_Click" OnClientClick="Javascript:return Validate();" />
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
    <div class="row">
        <div class="col-lg-12">
            
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

