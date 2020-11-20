<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5052.aspx.cs" Inherits="FrmAVS5052" %>

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

           <%-- Only Allow For alphabet --%>
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

        <%-- Only Allow For Numbers --%>
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
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        Locking Master
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>

                </div>
                <div class="portlet-body">
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Loan Sanction:<span class="required"> </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtLoanSan" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="1" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <div class="col-md-2"></div>
                            <label class="control-label col-md-2">Daily:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtdaily" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="2" OnTextChanged="Txtdaily_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Monthly:<span class="required"> </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtmonthly" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtmonthly_TextChanged" TabIndex="3" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <div class="col-md-2"></div>
                            <label class="control-label col-md-2">Locking:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtlocking" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtlocking_TextChanged" TabIndex="4" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                    </div>


                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Shares:<span class="required"> </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtshares" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="5" OnTextChanged="Txtshares_TextChanged" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <div class="col-md-2"></div>
                            <label class="control-label col-md-2">Period:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtperiod" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="6" OnTextChanged="Txtperiod_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Principle:<span class="required"> </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtprinciple" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="7" OnTextChanged="Txtprinciple_TextChanged" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <div class="col-md-2"></div>
                            <label class="control-label col-md-2">Interest:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtint" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtint_TextChanged" TabIndex="8" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Total:<span class="required"> </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txttotal" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txttotal_TextChanged" TabIndex="9" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <div class="col-md-2"></div>
                            <label class="control-label col-md-2">Req_Principle:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtReq_Principle" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtReq_Principle_TextChanged" TabIndex="10" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Difference:<span class="required"> </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtdiff" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtdiff_TextChanged" TabIndex="11" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>


                        </div>
                    </div>
                    <div class="form-actions">
                        <div class="row">
                            <div class="col-md-offset-4 col-md-9">
                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" />
                                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" />
                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" />
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
                                <asp:GridView ID="GrdDisp" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LoanSanction" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LoanSanction" runat="server" Text='<%# Eval("LoanSanction") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Daily" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Daily" runat="server" Text='<%# Eval("Daily ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Monthly" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Monthly" runat="server" Text='<%# Eval("Monthly") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Locking" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Locking" runat="server" Text='<%# Eval("Locking") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shares" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Shares" runat="server" Text='<%# Eval("Shares") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Period" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Period" runat="server" Text='<%# Eval("Period") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Principal" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Principal" runat="server" Text='<%# Eval("Principal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Int" Visible="true">
                                            <ItemTemplate>

                                                <asp:Label ID="Int" runat="server" Text='<%# Eval("Int") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Total" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ReqPrincipal" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ReqPrincipal" runat="server" Text='<%# Eval("Req_Principal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Diff" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Diff" runat="server" Text='<%# Eval("Diff") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Lnkmodify" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="Lnkmodify_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click1"></asp:LinkButton>
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
</asp:Content>

