<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmUnverifyentry.aspx.cs" Inherits="FrmUnverifyentry" %>

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

        <%-- Only Numbers --%>
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
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                       Unverify Entry List
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>
                 <div class="portlet-body">
                  <div id="Depositdiv" runat="server">

                         <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Branch Code  <span class="required">*</span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtBrcd" onkeypress="javascript:return isNumber (event)" TabIndex="1"   AutoPostBack="true" CssClass="form-control"  runat="server" />
                                </div>
                                <div class="col-md-2"></div>
                                <label class="control-label col-md-2">As On Date<span class="required">*</span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtAsonDate" TabIndex="2" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtAsonDate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAsonDate">
                                </asp:CalendarExtender>
                            </div>
                             </div>
                        </div>

                       <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                            <div class="col-lg-12">
                                <div class="col-md-6">
                                    <asp:Button ID="BtnPrint" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" TabIndex="3" />
                                    <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" TabIndex="4" />
                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" TabIndex="5"/>
                                  </div>
                            </div>
                        </div>
                       </div>
                        </div>
                </div>
                            </div>
                        </div>
</asp:Content>

