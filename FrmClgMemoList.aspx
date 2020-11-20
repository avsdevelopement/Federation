<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmClgMemoList.aspx.cs" Inherits="FrmClgMemoList" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Clearing Memo List
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
                                  
                                        <div class="col-lg-12">
                                           <label class="control-label col-md-2">Branch Code</label>
                                           <div class="col-md-2">
                                               <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                           </div>
                                        </div>
                                        <div class="col-lg-12">
                                           <label class="control-label col-md-2">AsOnDate</label>
                                           <div class="col-md-2">
                                               <asp:TextBox ID="TxtAsonDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" CssClass="form-control"  placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAsonDate">
                                                            </asp:CalendarExtender>
                                           </div>
                                        </div>
                                                                                
                                 
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="btnPrint" runat="server" Text="Report Print" CssClass="btn btn-primary" OnClick="btnPrint_Click" />
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
</asp:Content>

