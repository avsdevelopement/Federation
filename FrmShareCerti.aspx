<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmShareCerti.aspx.cs" Inherits="FrmShareCerti" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function Validate() {
            var TxtAccT = document.getElementById('<%=TxtAccType.ClientID%>').value;
            var BRCD = document.getElementById('<%=TxtBRCD.ClientID%>').value;
            

            if (BRCD == "") {
                alert("Please enter Branch Code......!!");
                return false;
            }

            if (TxtAccT == "") {
                alert("Please Select The Account Type......!!");
                return false;
            }

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
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                       Share Certificate Printing
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
                                                    <label class="control-label col-md-2">Branch Code<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBRCD"  CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="TxtBRCD_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtBrname" CssClass="form-control" runat="server" Style="text-transform: uppercase;" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Acc No<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtAccType" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged= "TxtAccType_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtATName" CssClass="form-control" runat="server"  OnTextChanged= "TxtATName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtATName"
                                                            UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                            EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetAgName" CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                     
                                                    <div class="col-md-3">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick= "Submit_Click" OnClientClick="Javascript:return Validate();" />
                                                   <asp:Button ID="Report" runat="server" CssClass="btn btn-primary" Text="Certificate" OnClick= "Report_Click" OnClientClick="Javascript:return Validate();" />
                                                    <asp:Button ID="Btn_Exit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick=  "Btn_Exit_Click" />
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
            <div class="col-md-12">
                <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="GrdCerti" runat="server" Text="Preview Share Certificate"></asp:Label>
                                    <asp:GridView ID="Grid_ViewSC" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99" 
                                    PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Cust No" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="CustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustAccNo" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="CustAccNo" runat="server" Text='<%# Eval("CustAccNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="OpenDate" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="OpenDate" runat="server" Text='<%# Eval("OPENDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="custname" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="custname" runat="server" Text='<%# Eval("custname") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="sharefrom" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="sharefrom" runat="server" Text='<%# Eval("sharefrom") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No Of Shares" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="totalshares" runat="server" Text='<%# Eval("totalshares") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="sharesvalue" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="sharesvalue" runat="server" Text='<%# Eval("sharesvalue") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="totalshareamt" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="totalshareamt" runat="server" Text='<%# Eval("totalshareamt") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="For cert_no" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="cert_no" runat="server" Text='<%# Eval("cert_no") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" />
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Report" HeaderStyle-BackColor="LightBlue">  
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAddshare" runat="server" CommandName="select" OnClick= "lnkAddshare_Click"  CommandArgument='<%#Eval("cert_no")%>' class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                        <SelectedRowStyle BackColor="#66FF99" />
                                        <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
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

