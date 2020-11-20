<%@ Page Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLienAcc.aspx.cs" Inherits="FrmLienAcc" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
     

      
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Lien Mark Report
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                        
                            <div style="border: 1px solid #3598dc">
                              
                                <div id="DIV_DATE" runat="server" class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label">From Branch :<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtbrcd1"  onkeypress="javascript:return isNumber(event)" placeholder="BRCD" runat="server" CssClass="form-control" TabIndex="2" Height="29px" ></asp:TextBox>
                                               
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">To Branch:<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtbrcd2" onkeypress="javascript:return isNumber(event)" placeholder="BRCD" runat="server" CssClass="form-control" TabIndex="2" Height="29px" ></asp:TextBox>
                                               
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label">From Date :<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtFdate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" TabIndex="2" Height="29px" ></asp:TextBox>
                                                <asp:CalendarExtender ID="TxtFY_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFdate">
                                                </asp:CalendarExtender>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">To Date:<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txttdate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" TabIndex="2" Height="29px" ></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txttdate">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                              

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtRpcode" onkeypress="javascript:return isNumber(event)" OnTextChanged= "txtRpcode_TextChanged" AutoPostBack="true" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtRname" OnTextChanged= "txtRname_TextChanged" AutoPostBack="true" Placeholder="Account Name" runat="server" CssClass="form-control" />

                                            </div>

                                        </div>

                                    </div>
                                       <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label">From Branch :<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                              <asp:DropDownList ID="ddlacctype" runat="server" CssClass="form-control" AutoPostBack="true" EnableViewState="true" TabIndex="8"></asp:DropDownList>
                                               
                                            </div>
                                          
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">

                                            <asp:Button ID="btOkReport" Text="Submit" runat="server" CssClass="btn blue" OnClick= "btOkReport_Click" />
                                             <asp:Button ID="btnclear" Text="Clear" runat="server" CssClass="btn blue"  />
                                             <asp:Button ID="btnexit" Text="Exit" runat="server" CssClass="btn blue"  />
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    
                </div>
            </div>
        </div>
        <div id="alertModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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
    </div>

</asp:Content>
