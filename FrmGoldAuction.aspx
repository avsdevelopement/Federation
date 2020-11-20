<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/CBSMaster.master" CodeFile="FrmGoldAuction.aspx.cs" Inherits="FrmGoldAuction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isvalidate() {









        }
    </script>
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

    <%--    <script type="text/javascript">
        $(function () {
            $("input[name='RdbSingle']").click(function () {
                if ($("#chkYes").is(":checked")) {
                    $("#dvPassport").show();
                } else {
                    $("#dvPassport").hide();
                }
            });
        });
</script>--%>

    <script type="text/javascript">
        function ShowHideDiv() {
            //var chkYes = document.getElementById("RdbSingle");
            //var dvsingle = document.getElementById("Single");
            //var divmulti = document.getElementById("Multiple");

            if ($("#RdbSingle").is(":checked")) {

                $("#Single").hide();
                $("#Multiple").show();

            }
            else {
                $("#Single").show();
                $("#Multiple").hide();
            }
            //if (chkYeschecked) {
            //    dvsingle.style.display = "block";
            //    divmulti.style.display = "none";

            //}
            //else
            //{
            //    dvsingle.style.display = "none";
            //    divmulti.style.display = "block";

            //}
            //dvPassport.style.display = chkYes.checked ? "block" : "none";
        }
    </script>
    <script type="text/javascript">
        function ShowHideDiv1() {
            if ($("#RdbMultiple").is(":checked")) {
                $("#Single").show();
                $("#Multiple").hide();



            }
            else {
                $("#Single").hide();
                $("#Multiple").show();

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                       Gold Auction Notice 
                    </div>
                </div>
                <div class="portlet-body form">
                    <div id="DIVAC" runat="server" class="form-horizontal">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 7px 0;">
                                            <div class="col-lg-12">
                                            <%--    <label class="control-label col-md-2">  <span class="required">* </span></label>--%>
                                              <%--  <div class="col-md-4">
                                                      <asp:RadioButtonList ID="RdbSingle" runat="Server"  OnSelectedIndexChanged="RdbSingle_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="All" Value="1"></asp:ListItem> 
                                                           <asp:ListItem Text="Specific" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>--%>
                                              
                                                   <%-- <div class="col-lg-2">
                                                 <asp:RadioButton ID="RdbSingle" runat="server" Text="All  "  GroupName="SM" AutoPostBack="true" OnCheckedChanged= "RdbSingle_CheckedChanged" /></div>
                                                    <div class="col-lg-2">
                                                    <asp:RadioButton ID="RdbMultiple" runat="server" Text="Specific"  GroupName="SM"  AutoPostBack="true" OnCheckedChanged= "RdbMultiple_CheckedChanged" /></div>--%>
                                                  
                                                </div>
                                            </div>
                                        </div>

                                       

                                        <div id="Div1" runat= "server">
                                            
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Branch Code <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtbranchcode1" placeholder="Branch CODE" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" ></asp:TextBox>
                                                    </div>     
                                                </div>
                                            </div>
                                                  <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Loan Code <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtloancode1" AutoPostBack="true" placeholder="Loan Code" CssClass="form-control" OnTextChanged= "txtloancode1_TextChanged"  runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                       
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtloan1name" placeholder="Loan name"   CssClass="form-control"  runat="server"></asp:TextBox>
                                                        
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Accno <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TXTACCNO" placeholder="AccNo" AutoPostBack="true" CssClass="form-control" OnTextChanged=  "TXTACCNO_TextChanged"  runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                       
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtaccname" placeholder="Acc Name"   CssClass="form-control"  runat="server"></asp:TextBox>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                              
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Gold Auction notice" OnClick="btnSubmit_Click"  OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="btnExit_Click"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <d
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