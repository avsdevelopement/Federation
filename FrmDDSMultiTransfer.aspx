<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDDSMultiTransfer.aspx.cs" Inherits="FrmDDSMultiTransfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script lang="JavaScript" type="text/javascript">
        function Runbat1() {
            var shell = new ActiveXObject("WScript.Shell");
            var path = '"C:/balaji/db2pc.bat"';
            shell.run(path, 1, false);
        }
        function Runbat2() {
            var shell = new ActiveXObject("WScript.Shell");
            var path = '"C:/balaji/pc2db.bat"';
            shell.run(path, 1, false);
        }
    </script>--%>
    <script>
        function LaunchApp() {
            debugger;
            parser = new DOMParser();
            parser.Exec("C:\\Balaji\\PC2DB.exe");

        }

        function Runbat2() {
            var shell = new ActiveXObject("WScript.Shell");
            var path = '"C:/balaji/pc2db.bat"';
            shell.run(path, 1, false);
        }
        function Runbat1() {

            var shell = new ActiveXObject("WScript.Shell");
            var path = '"C:/AVS/DeletePCTRX.bat"';
            shell.run(path, 1, false);
        }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload2" />
            <asp:PostBackTrigger ControlID="BtnAkytUpload" />
            <asp:PostBackTrigger ControlID="Create" />
            
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Multiple DDS Collection Receive
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills">
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12" style="margin-top: 15px;">
                                                        <asp:RadioButtonList ID="Rdb_Upload" runat="server" RepeatDirection="Horizontal" Width="400px" OnSelectedIndexChanged="Rdb_Upload_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="Machine Upload" Value="1" Selected="True"></asp:ListItem>
                                                            
                                                            
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label col-md-2">Agent No.</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtAgentNo" runat="server" PlaceHolder="Agent Code" CssClass="form-control" OnTextChanged="TxtAgentNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">

                                                            <asp:TextBox ID="TxtAgentName" runat="server" PlaceHolder="Agent Name" CssClass="form-control" OnTextChanged="TxtAgentName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoAgname" runat="server" TargetControlID="TxtAgentName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4"
                                                                ServiceMethod="GetAgName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <asp:Label ID="lblagent" runat="server" Text="Agent Balance (A)" class="control-label"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtAGAmt" CssClass="form-control" Placeholder="Agent Collection" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label">Total Collection (B)</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxttotalAmt" CssClass="form-control" Placeholder="Total Collection" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label">Difference (A - B)</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtDiffrence" CssClass="form-control" Placeholder="Diffrence" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="row" style="margin-bottom: 9px;">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                          <asp:CheckBox ID="Chk_ZeroBal" Text=" Allow Zero Balance Record" runat="server" Checked="true" />
                                                    </div>
                                                </div>
                                                <br />
                                                <br />
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <asp:FileUpload ID="FileUPControl" runat="server" CssClass="bold" Style="height: 23px; border: thin" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="col-lg-12">
                                                        <%--<div class="col-md-2">
                                                            <asp:Button ID="btnUpload1" runat="server" CssClass="button" Text="Upload1" />
                                                        </div>--%>
                                                        <div class="col-md-4">

                                                            <asp:Button ID="btnUpload2" runat="server" CssClass="btn btn-success" Text="Upload" OnClick="BtnUpload_Click" Visible="false" />&nbsp;&nbsp;
                                                             <asp:Button ID="BtnPosting" runat="server" CssClass="btn btn-primary" Text="Posting" OnClick="BtnPosting_Click1" Visible="false" />
                                                            
                                                          <input type="button" value="DB2PC"  onclick="Runbat1()" class="btn btn-success"  />
                                                          
                                                            <asp:Label ID="lblprocess" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                             <asp:Button ID="btnUpload" runat="server" Text="Web" CssClass="btn btn-primary" OnClick="btnUpload_Click2" Visible="false" /> 
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:Button ID="BtnAkytUpload" runat="server" Text="Akyt Upload" CssClass="btn btn-primary" OnClick="BtnAkytUpload_Click" />
                                                             <asp:Button ID="BtnAkytPosting" runat="server" Text="Akyt Post" CssClass="btn btn-primary" OnClick="BtnAkytPosting_Click" />
                                                            <asp:Button ID="BtnClearUpload" runat="server" Text="Clear Upload" CssClass="btn btn-primary" OnClick="BtnClearUpload_Click"/>
                                                            <asp:Button ID="Create" runat="server" OnClick="Create_Click" Text="Create" CssClass="btn btn-success" OnClientClick="Runbat1()" />
                                                            <asp:Button ID="Btn_Run_Exe" Visible="false" runat="server" Text="Run exe" CssClass="btn btn-success" OnClick="Btn_Run_Exe_Click" />
                                                          <input type="button" value="PC2DB"  onclick="Runbat2()" class="btn btn-success" />

                                                        </div>
                                                        <div class ="col-md-5">
                                                            
                                                           
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-9">
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Amount" Text="Change Amount" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtCAmt" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Button ID="btnchnage" Text="Change" runat="server" CssClass="btn btn-success" OnClick="btnchnage_Click" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Button ID="Btn_MobileUpload" Text="Mobile Upload" runat="server" CssClass="btn btn-success" OnClick="Btn_MobileUpload_Click" Visible="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <%--CUSTNAME,REMARK,Convert(Varchar(11),ENTRYDATE) ENTRYDATE,AGENTCODE,BRCD,TRANSACTIONDATE,ACNO,TRANSAMT,STAGE,MID,SYSTEMDATE,Ref_Agent,Glcode--%>
                                            <div class="col-lg-12">
                                                <div class="col-lg-6">
                                                    <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                                                        <asp:Label ID="Lbl_Valid" runat="server" Text="VALID ACCOUNTS" BackColor="#ccffcc" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                        <asp:Table ID="Table1" runat="server">
                                                            <asp:TableRow ID="TRow1" runat="server">
                                                                <asp:TableCell ID="Tcell1" runat="server" Style="width: 50%" class="table-scrollable">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <asp:GridView ID="gridadd" runat="server" OnPageIndexChanging="gridadd_PageIndexChanging" AutoGenerateColumns="false">
                                                                                    <Columns>


                                                                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="EDT" runat="server" Text='<%# Eval("ENTRYDATE") %>' ></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                       
                                                                                        <asp:TemplateField HeaderText="Agent No">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="AGNO" runat="server" Text='<%# Eval("AGENTCODE") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Account No">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="ACNO" runat="server" Text='<%# Eval("ACNO") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Transaction Amt">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="AddT" runat="server" Text='<%# Eval("TRANSAMT") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Remark">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Remark" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Edit" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("ACNO") + "_" +Eval("ENTRYDATE")+ "_" +Eval("POSTINGDATE")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                    </Columns>
                                                                                </asp:GridView>

                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>


                                                    </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                                                        <asp:Label ID="Lbl_INVALID" runat="server" Text="INVALID ACCOUNT" BackColor="#ffccff" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                        <asp:Table ID="Table2" runat="server">
                                                            <asp:TableRow ID="TableRow1" runat="server">
                                                                <asp:TableCell ID="Tcell2" runat="server" Style="width: 50%">
                                                                    <asp:GridView ID="Grd_NoAcc" runat="server" OnPageIndexChanging="gridadd_PageIndexChanging" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Srno" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SRNO" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="NAME" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/CNo">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="AMT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Remark">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Remark" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
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
            <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="margin-left: 1%; width: 98%">
        <div class="modal-dialog modal-lg" role="document" style="width: 96%">
            <div class="modal-content" style="border: 5px solid #4dbfc0;">
                <div class="inner_top">
                    <div class="panel panel-default">
                        <div class="panel-heading"></div>
                        <div class="panel-body">
                            <div class="col-sm-12">

                                <div class="col-lg-12">


                                    <div class="table-responsive">
                                        <div style="height: 250px; overflow: auto">
                                                    <asp:GridView ID="GridPCRX" runat="server" AutoGenerateColumns="false"   AlternatingRowStyle-CssClass="alt"
                                    EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%#Eval("No") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%#Eval("City") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%#Eval("Name") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%#Eval("Designation1") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%#Eval("Date") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%#Eval("Designation") %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                    
                                                    <div class="col-lg-12">
                                                        <div class="col-md-6">

                                                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Post" OnClick="btnPost_Click" />
                                                             <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" data-dismiss="modal" />
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
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
