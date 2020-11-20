<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5051.aspx.cs" Inherits="FrmAVS5051" %>


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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                       Recovery Parameter
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>

                </div>
                <div class="portlet-body">

                  
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Column No:<span class="required"> </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtCol" runat="server" onkeypress="javascript:return isNumber (event)"   PlaceHolder="No" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <div class="col-md-2"></div>
                            <label class="control-label col-md-2">Rec Prd:<span class="required"></span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtPno" runat="server" onkeypress="javascript:return isNumber (event)"  AutoPostBack="true" OnTextChanged="TxtPno_TextChanged" PlaceHolder="No" CssClass="form-control"></asp:TextBox>
                            </div>
                              <div class="col-md-3">
                                <asp:TextBox ID="TxtRecPname" CssClass="form-control" placeholder="Name" OnTextChanged="TxtRecPname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtRecPname"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="getglname" CompletionListElementID="CustList">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Post Prd<span class="required"> </span></label>
                            <div class="col-md-1">
                               <asp:TextBox ID="TxtPostPrd" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"   OnTextChanged="TxtPostPrd_TextChanged" PlaceHolder="No" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtPname" CssClass="form-control" placeholder="Name" OnTextChanged="TxtPname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                <div id="Div1" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="autoglpost" runat="server" TargetControlID="TxtPname"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="getglname" CompletionListElementID="Div1">
                                </asp:AutoCompleteExtender>
                            </div>
                            <label class="control-label col-md-2">Short Name<span class="required"></span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtShort" runat="server"   PlaceHolder="Name" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                        </div>
                    </div>
                    
                     <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                             <label class="control-label col-md-2">TxtExReccode:</label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtExrec" runat="server" PlaceHolder="No" AutoPostBack="true"  OnTextChanged="TxtExrec_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                            </div>
                             <div class="col-md-3">
                                <asp:TextBox ID="TxtEname" CssClass="form-control" placeholder="Name" OnTextChanged="TxtEname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="autoexname" runat="server" TargetControlID="TxtEname"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="getglname" CompletionListElementID="Div2">
                                </asp:AutoCompleteExtender>
                            </div>
                             <label class="control-label col-md-2">Short Marathi<span class="required"> </span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtMarati" runat="server"  PlaceHolder="Name" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                        </div>
                    </div>
                          <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Valuetype:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtvalue" onkeypress="javascript:return isNumber (event)"   PlaceHolder="Type"   runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2"></div>
                              <label class="control-label col-md-2">Type<span class="required"> </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txttype" runat="server"  onkeypress="javascript:return isNumber (event)"   PlaceHolder="Type" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                          </div>
                    </div>
                      <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                          
                            <label class="control-label col-md-2">Rate:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtrate" onkeypress="javascript:return isNumber (event)"   PlaceHolder="Rate"   runat="server" CssClass="form-control"></asp:TextBox>
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

                                                          <asp:TemplateField HeaderText="ColumnNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="COLUMNNO" runat="server" Text='<%# Eval("COLUMNNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Rec_Prd" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="REC_PRD" runat="server" Text='<%# Eval("REC_PRD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Post_Prd" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="POST_PRD" runat="server" Text='<%# Eval("POST_PRD ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Exreccode" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="EXRECCODE" runat="server" Text='<%# Eval("EXRECCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                      
                                                        <asp:TemplateField HeaderText="ShortName" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SHORTNAME" runat="server" Text='<%# Eval("SHORTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                  
                                                        
                                                        <asp:TemplateField HeaderText="ShotMarathi" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SHORTMARATHI" runat="server" Text='<%# Eval("SHORTMARATHI") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                       
                                                        <asp:TemplateField HeaderText="Value" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="VALUE" runat="server" Text='<%# Eval("VALUE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Type" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="TYPE" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Rate" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="RATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkModify_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
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

