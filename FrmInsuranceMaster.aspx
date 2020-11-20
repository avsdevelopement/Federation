<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInsuranceMaster.aspx.cs" Inherits="FrmInsuranceMaster" %>

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

        <%-- Only alphabet --%>
        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

    <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
         function StartDate(obj) {
            debugger;
            var StartDate = document.getElementById('<%=Txtstartdate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=WorkingDate.ClientID%>').value;

             var frdate = StartDate.substring(0, 2);
             var frmonth = StartDate.substring(3, 5);
             var fryear = StartDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (frmyDate > wmyDate) {
                message = 'Start date cannot be greater than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=Txtstartdate.ClientID %>').value = "";
                document.getElementById('<%=Txtstartdate.ClientID%>').focus();
                return false;
            }
         }
        function ExpiryDate(obj) {
            debugger;
            var ExpiryDate = document.getElementById('<%=Txtexpirydate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=WorkingDate.ClientID%>').value;

            var frdate = ExpiryDate.substring(0, 2);
            var frmonth = ExpiryDate.substring(3, 5);
            var fryear = ExpiryDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (frmyDate < wmyDate) {
                message = 'Expiry date cannot be less than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=Txtexpirydate.ClientID %>').value = "";
                document.getElementById('<%=Txtexpirydate.ClientID%>').focus();
                return false;
            }
        }
        
        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        Insurance master
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>


                <div class="portlet-body">
                    <%--Div 1--%>
                    <div class="row" style="margin: 7px 0 12px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Select </label>
                            <div class="col-md-3">
                                <asp:Button ID="btnAddNew" runat="server" CssClass="btn default" Text="Add new " OnClick="btnAddNew_Click" AccessKey="1" />
                            </div>
                            <asp:Label ID="lblstatus" runat="server" CssClass="control-label col-md-3" Text="Add New" Style="color: blueviolet;"></asp:Label>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Product Type <span class="required">*</span></label>

                                    <div class="col-md-1">
                                        <asp:TextBox ID="txttype" CssClass="form-control" runat="server" onkeypress="return isNumber(event)" OnTextChanged="txttype_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:TextBox ID="txttynam" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txttynam_TextChanged"></asp:TextBox>
                                        <div id="Custlist2" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txttynam"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetGlName" CompletionListElementID="Custlist2">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <label class="control-label col-md-1">AccNo</label>
                                    <div class="col-lg-1">
                                        <asp:TextBox ID="txtaccno" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:TextBox ID="TxtAccName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged"></asp:TextBox>
                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetAccName" CompletionListElementID="CustList">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                    <div runat="server" id="TblDiv_MainWindow">
                        <div id="Depositdiv" runat="server">
                            

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">PolicyNo<span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtpolicyNo" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" CssClass="form-control" placeholder="No" OnTextChanged="TxtpolicyNo_TextChanged" runat="server" TabIndex="1" />
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Policy Amt <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtInsamt" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="TxtInsamt_TextChanged" placeholder="Amount" runat="server" TabIndex="2"></asp:TextBox>
                                    </div>
                                      <div class="col-md-2">
                                                    <input type="hidden" id="WorkingDate" runat="server" value="" />
                                                </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Start Date: <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="Txtstartdate"  onkeyup="FormatIt(this)" MaxLength="10" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtstartdate_TextChanged" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server" onblur="StartDate()"   TabIndex="3"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="Txtstartdate" >
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txtstartdate" >
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Expiry Date: <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="Txtexpirydate" onkeyup="FormatIt(this)" MaxLength="10" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtexpirydate_TextChanged" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server" onblur="ExpiryDate()" TabIndex="4"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="Txtexpirydate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txtexpirydate" >
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Close Date <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="Txtclosedate" onkeyup="FormatIt(this)" MaxLength="10" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtclosedate_TextChanged" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server" TabIndex="5"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="Txtclosedate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txtclosedate">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Premium Amt<span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtPremamt" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtPremamt_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Amount" runat="server" TabIndex="6" />
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Pol status <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="DDlPolstatus" runat="server" OnTextChanged="DDlPolstatus_TextChanged" CssClass="form-control" TabIndex="7">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Open</asp:ListItem>
                                            <asp:ListItem Value="2">Close</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Insurance Com <span class="required"></span></label>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TxtIname" CssClass="form-control" OnTextChanged="TxtIname_TextChanged" placeholder="Name" runat="server" TabIndex="8"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Description: <span class="required"></span></label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="Txtdesc" CssClass="form-control" TextMode="MultiLine" OnTextChanged="Txtdesc_TextChanged" placeholder="Descp" runat="server" TabIndex="9"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Endorsement Status <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="DDlEndordement" runat="server" OnTextChanged="DDlEndordement_TextChanged" CssClass="form-control" TabIndex="10">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <label class="control-label col-md-2">Sent Date <span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="Txtsentdate" onkeyup="FormatIt(this)" OnTextChanged="Txtsentdate_TextChanged" MaxLength="10" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server" TabIndex="11"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="Txtsentdate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txtsentdate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>


                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">ReceivedDate<span class="required"></span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtRecDate" onkeyup="FormatIt(this)" OnTextChanged="TxtRecDate_TextChanged" MaxLength="10" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server" TabIndex="12"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtRecDate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtRecDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" OnClientClick="Javascript:return IsValide()" TabIndex="13" />
                                        <asp:Button ID="Btnclear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="Btnclear_Click" TabIndex="14" />
                                        <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="BtnExit_Click" TabIndex="15" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" id="Div_grid" runat="server">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdinsurance" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" OnPageIndexChanging="grdinsurance_PageIndexChanging"
                                    PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" OnSelectedIndexChanged="grdinsurance_SelectedIndexChanged"
                                    OnRowDataBound="grdinsurance_RowDataBound" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Srno" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Prdcode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PRDCODE" runat="server" Text='<%# Eval("PRDCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acctno" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCTNO" runat="server" Text='<%# Eval("ACCTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Custname" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="MakerName" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MakerName" runat="server" Text='<%# Eval("MakerName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PolicyNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PolicyNo" runat="server" Text='<%# Eval("PolicyNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PolicyNo")+","+ Eval("PRDCODE")+","+Eval("ACCTNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkModify_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PolicyNo")+","+ Eval("PRDCODE")+","+Eval("ACCTNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PolicyNo")+","+ Eval("PRDCODE")+","+Eval("ACCTNO")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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


