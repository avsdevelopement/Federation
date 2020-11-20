<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5090.aspx.cs" Inherits="FrmAVS5090" %>

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
        function IsValide() {
            var Date, Menu, Activity, Req, Status
            Date = document.getElementById('<%=Txtdate.ClientID%>').value;
            Menu = document.getElementById('<%=TxtMenu.ClientID%>').value;
            Activity = document.getElementById('<%=TxtActivity.ClientID%>').value;
            Req = document.getElementById('<%=TxtReq.ClientID%>').value;
            Status = document.getElementById('<%=TxtReqBy.ClientID%>').value;
            var message = '';

            if (Date == "") {
                alert("Please Select  Date....!!");
                return false;
            }
            else if (Menu == "0") {
                alert("Please Enter Menu....!!");
                return false;
            }
            else if (Activity == "") {
                alert("Please Enter Activity....!!");
                return false;
            }
            else if (Req == "") {
                alert("Please Enter Requirement....!!");
                return false;
            }
            else if (Status == "") {
                alert("Please Enter Requirement By...!!");
                return false;
            }

        }
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function CallonLoad() {
            // Create an instance on TransliterationControl with the required
            // options.
            var varComment = $("input[name='<%=Rbdlanguage.UniqueID%>']:checked").val();

            if (varComment == 1) {
                var optionsEng = {
                    sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                    destinationLanguage:
                [google.elements.transliteration.LanguageCode.ENGLISH],
                    shortcutKey: 'ctrl+g',
                    transliterationEnabled: true
                };

                var control = new google.elements.transliteration.TransliterationControl(optionsEng);
            }
            else if (varComment == 2) {
                var optionsMar = {
                    sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,

                    destinationLanguage:
                [google.elements.transliteration.LanguageCode.MARATHI],
                    shortcutKey: 'ctrl+g',
                    transliterationEnabled: true
                };

                var control = new google.elements.transliteration.TransliterationControl(optionsMar);
            }
           
            control.makeTransliteratable(['<%=TxtReq.ClientID%>']);
        }
        google.setOnLoadCallback(CallonLoad);
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="Btn_Submit" />
            <asp:PostBackTrigger ControlID="btnmodify" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Task Information
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                               
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-md-12">
                                                        <label class="control-label col-md-2">SrNo:<span class="required">*</span> </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtSrno" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Date:<span class="required">*</span> </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="Txtdate" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" TabIndex="1" MaxLength="10"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txtdate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-md-12">
                                                        <label class="control-label col-md-2">Menu:<span class="required">*</span> </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtMenu" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Activity:<span class="required">*</span> </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtActivity" onclick="CallonLoad()" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                  <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-md-12">
                                                            <label class="control-label col-md-4">Language <span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:RadioButtonList ID="Rbdlanguage" runat="server" RepeatDirection="Horizontal" >

                                                                    <asp:ListItem Value="1" style="padding: 5px">English</asp:ListItem>
                                                                    <%--//Hide--%>
                                                                    <asp:ListItem Value="2" style="padding: 5px">Marathi</asp:ListItem>
                                                                    <%--//showall--%>
                                                                </asp:RadioButtonList>
                                                            </div>

                                                        </div>
                                                    </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-md-12">
                                                        <label class="control-label col-md-2">Requirement:<span class="required"> *</span> </label>
                                                        <div class="col-md-10">
                                                            <asp:TextBox ID="TxtReq" onclick="CallonLoad()" runat="server" TabIndex="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                       </div>
                                                </div>

                                                 <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-md-12">
                                                         <label class="control-label col-md-2">Requirement By:<span class="required">*</span> </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtReqBy" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                                        </div>
                                                        </div>
                                                     </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-md-12">
                                                        <label class="control-label col-md-2">Select File:<span class="required"></span> </label>
                                                        <div class="col-md-4">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            <asp:Label ID="lblImage" runat="server" />
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                    <div class="col-md-12" align="center">
                                                        <asp:Button ID="Btn_Submit" runat="server" Text="Save" CssClass="btn blue " OnClick="Btn_Submit_Click" OnClientClick="return IsValide();" />
                                                        <asp:Button ID="BtnModify" runat="server" CssClass="btn blue" Text="Modify" OnClick="BtnModify_Click" Visible="false" />
                                                        <asp:Button ID="BtnDelete" runat="server" CssClass="btn blue" Text="Delete" OnClick="BtnDelete_Click" Visible="false" />
                                                        <asp:Button ID="BtnAdd" runat="server" CssClass="btn blue" Text="Add New" OnClick="BtnAdd_Click" Visible="false" />
                                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn blue" OnClick="BtnClear_Click" />
                                                        <asp:Button ID="BthExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="BthExit_Click" />

                                                    </div>
                                                </div>
                                               
                                            </div>
                                           </div>
                                      </div>
                                  </div>
                            </div>
                        </div>
                      </div>
                     <div class="col-md-12">
                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="GrdDet" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                                                        EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdDet_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Id" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Srno" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Width="4%" Visible="false" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Srno" runat="server" Text='<%# Eval("Srno") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Date" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Width="6%" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Menu" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Width="8%" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Menu" runat="server" Text='<%# Eval("Menu") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="Activity" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Width="8%" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Activity" runat="server" Text='<%# Eval("Activity") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Requirement" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Width="40%" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Requirement" runat="server" Text='<%# Eval("Requirement") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Requirement By" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Width="8%" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="RequirementBy" runat="server" Text='<%# Eval("RequirementBy") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Modify" Visible="true" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Width="4%" >
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("id")%>' CommandName="select" OnClick="LnkModify_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Delete" Visible="true" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Width="4%" >
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LnkDelete" runat="server" CommandArgument='<%#Eval("id")%>' CommandName="select" OnClick="LnkDelete_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                        <PagerStyle CssClass="pgr" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

