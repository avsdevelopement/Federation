<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTRFDenomFrVault.aspx.cs" Inherits="FrmTRFDenomFrVault" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<%--    <script type="text/javascript">
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
    <style type='text/css'>
        .DivScroll {
            overflow-x: hidden;
            overflow-y: auto;
            background-color: lightgray;
            width: 80%;
            height: 210px;            
        }

        .DivScroll2 {
            overflow-x: hidden;
            overflow-y: auto;
            background-color: lightgray;
            width: 60%;
            height: 180px;            
        }

        .Panel legend {
            font-size:small;
            }
    </style>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--    <div class="row">
        <div id="DIVTRFFROM" class="col-md-12" runat="server" visible="true">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        TRANSFER OF DENOMINATIONS FROM VAULT
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">User/Station No:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtStationNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">TRF From Vault No:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTRFNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                             
                                    <asp:Panel ID="Panel1" CssClass="Panel" style="margin-left:auto;margin-right:auto;" GroupingText="Denomination Table (User Selected Value)" runat="server" Width="100%" Height="280px">                
                                        <div class="DivScroll" style="margin-left:auto;margin-right:auto;height:210px;width:900px;">

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-6">
                                                        <label class="control-label">Available Denomination</label>
                                                    </div>
                                                    <div class="col-md-6">                                                      
                                                            <label class="control-label ">Take Denomination</label>                                                        
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Denomination</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Count</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Count</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label class="control-label ">Amount Take</label>
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox8" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox14" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox15" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox16" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox17" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox18" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox19" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox20" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox21" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox22" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox23" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox36" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox37" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox38" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox39" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox40" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCoinTake" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault Coins :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtVaultCoins" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSolidNtsTake" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault SolidNotes :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtVaultSolidNts" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Balance Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtBalTake" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault CashBalance :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtvaultCashBal" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                    
                                    <asp:Panel ID="Panel2" CssClass="Panel" style="margin-left:auto;margin-right:auto;" GroupingText="Denomination (This Station)" runat="server" Width="90%" Height="180px">     
                                        <div class="DivScroll2" style="margin-left:auto;margin-right:auto;height:140px;width:700px;" >

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox27" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox28" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox30" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox31" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox42" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox43" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox44" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox45" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox46" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox47" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox48" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox49" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox50" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox51" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox52" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox53" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox54" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox55" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox56" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox57" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox58" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Station Coins :</label>
                                                </div>
                                                <div class="col-md-2">
                                                  <asp:TextBox ID="txtStationCoins" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Station SolidNotes :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSolidNotes" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Station Balance :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtStationBal" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-5 col-md-7">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="OK" OnClientClick="Javascript:return isvalidate();" />
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
    </div>--%>

</asp:Content>

