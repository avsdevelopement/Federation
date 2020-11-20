<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmS0004.aspx.cs" Inherits="FrmS0004" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
    

          
        
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

        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>
    <script type="text/javascript">
        function IsValide() {
            var prdcd = document.getElementById('<%=txtCaseY.ClientID%>').value;
             var brcd = document.getElementById('<%=txtBrcd.ClientID%>').value;
             var accno = document.getElementById('<%=txtCaseNO.ClientID%>').value;


             if (prdcd == "") {
                 message = 'Please Enter Product Code....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 $('#<%=txtCaseY.ClientID %>').focus();
                 return false;
             }
             if (brcd == "") {
                 message = 'Please Enter Branch Code....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 $('#<%=txtBrcd.ClientID %>').focus();
                return false;
            }
            if (accno == "") {
                message = 'Please Enter Account Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseNO.ClientID %>').focus();
                return false;
            }
        }

    </script>
    <script>

        function Year(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "-";
            obj.value = obj.value;
        }
    </script>
    <style type="text/css">
        .btn {
            text-decoration: none;
            border: 1px solid #000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Follow Up Master
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form" runat="server" id="div_followup" visible="false">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">   -->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">

                                        <div style="border: 1px solid #3598dc">
                                            <asp:Table ID="Table1" runat="server">
                                                <asp:TableRow ID="TableRow1" runat="server" Style="width: 300px">
                                                    <asp:TableCell ID="TableCell1" runat="server" Style="width: 2000px">

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">Branch Code <span class="required">*</span></label>

                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtBrcd" CssClass="form-control" runat="server" onkeypress="return isNumber(event)" OnTextChanged="txtBrcd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <asp:TextBox ID="txtBrcdname" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                </div>

                                                                <label class="control-label col-md-2">Member No<span class="required">*</span></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtMemberNo" CssClass="form-control" runat="server" Placeholder="Member No" onkeypress="javascript:return isNumber (event)" TabIndex="3"></asp:TextBox>
                                                                </div>


                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="CUSTNODIV">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">Case Year<span class="required">*</span></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtCaseY" CssClass="form-control" runat="server" onkeypress="return isNumber(event)" Placeholder="YY-YY" onkeyup="Year(this)" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-1">Case No<span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtCaseNO" CssClass="form-control" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>

                                                                <label class="control-label col-md-1">SRO No</label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtSRo" CssClass="form-control" runat="server" onkeypress="return isNumber(event)" OnTextChanged="txtSRo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>

                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TXTSROName" Enabled="false" Placeholder="Sro name" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0" runat="server">
                                                            <div class="col-lg-12">
                                                            </div>
                                                        </div>
                                                        <div runat="server" class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">File Status</label>
                                                                <div class="col-lg-3">
                                                                    <asp:TextBox ID="txtFilestatus" runat="server" Text="File Assign To SRO" Enabled="false" CssClass="form-control" OnTextChanged="txtFilestatus_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <%-- <label class="control-label col-md-2">Mobile No</label>
                                                                <div class="col-lg-2">
                                                                    <asp:TextBox ID="TxtMob" runat="server" placeholder = "Mobile Number" Enabled="false" CssClass="form-control" AutoPostBack="true" ></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-1">Tel No</label>
                                                                <div class="col-lg-2">
                                                                    <asp:TextBox ID="TxtTel" runat="server" placeholder = "Telephone Number" Enabled="false" CssClass="form-control" AutoPostBack="true" ></asp:TextBox>
                                                                </div>
                                                                --%>

                                                                <label class="control-label col-md-2">Follow Up Remarks</label>
                                                                <div class="col-lg-4">
                                                                    <asp:TextBox ID="TXTREMARKS" runat="server" CssClass="form-control" OnTextChanged="TXTREMARKS_TextChanged" AutoPostBack="true" TextMode="MultiLine" MaxLength="35"></asp:TextBox>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div id="Div2" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                            <%--<div class="col-lg-12">
                                                                <label class="control-label col-md-2">Follow Up Remarks</label>
                                                                <div class="col-lg-4">
                                                                    <asp:TextBox ID="TXTREMARKS" runat="server" CssClass="form-control" OnTextChanged="TXTREMARKS_TextChanged" AutoPostBack="true" TextMode="MultiLine" MaxLength="35"></asp:TextBox>
                                                                </div>
                                                                
                                                                </div>--%>
                                                        </div>
                                                        <div id="Div3" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">Follow Up Date <span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtFdate" Enabled="false" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtFdate_TextChanged" AutoPostBack="true" placeholder="dd/mm/yyyy" TabIndex="7"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TxtFdateWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFdate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="TxtFdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFdate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <label class="control-label col-md-3">Next Follow Up Date <span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtnextfile" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" TabIndex="7"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="txtnextfileWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtnextfile">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="txtnextfile_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtnextfile">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                        </div>


                                                    </asp:TableCell>

                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Submit_Click" OnClientClick="Javascript:return IsValide();" />
                                        <asp:Button ID="Clear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="Clear_Click" />
                                        &nbsp;<asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />

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
        <!--</form>-->



        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="div_grid" visible="false">
            <div class="col-lg-12" style="height: 100%">
                <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                    <table class="table table-striped table-bordered table-hover" width="100%">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="GrdS0004" runat="server"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        EditRowStyle-BackColor="#FFFF99"
                                        PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="BRCD" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CASE YEAR" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="CASE_YEAR" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CASENO" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="CASENO" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SRONO" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FILE STATUS" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="FILE_STATUS" runat="server" Text='<%# Eval("FILE_STATUS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FOLLOW UP DATE" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="F_DATE" runat="server" Text='<%# Eval("F_DATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FOLLOW UP REMARK" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="F_REEAMRKS" runat="server" Text='<%# Eval("F_REEAMRKS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NEXT FOLLOW UP DATE" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="NEXT_F_DT" runat="server" Text='<%# Eval("NEXT_F_DT") %>'></asp:Label>
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



        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="div_maingrd" visible="true">

            <div class="col-lg-12" style="height: 100%">
                <div class="col-md-3">
                    <asp:Button ID="BtnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="BtnAddNew_Click" />
                </div>

                <label class="control-label col-md-2">Today's FollowUp </label>
                <div class="col-md-2">
                    <asp:TextBox ID="TxtFollowDt" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFollowDt">
                    </asp:TextBoxWatermarkExtender>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFollowDt">
                    </asp:CalendarExtender>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="BtnSubmit1" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit1_Click" />
                </div>
                <div class="table-scrollable" style="height: 350px; overflow-y: scroll; padding-bottom: 10px;">
                    <table class="table table-striped table-bordered table-hover" width="100%">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="GrdFollowup" runat="server"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        EditRowStyle-BackColor="#FFFF99"
                                        PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BRCD" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CASE YEAR" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="CASE_YEAR" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CASENO" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="CASENO" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Follow Up" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Add" CommandArgument='<%#Eval("ID")%>' CommandName="select" class="glyphicon glyphicon-plus" OnClick="lnkEdit_Click"></asp:LinkButton>
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


