<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMasterListing.aspx.cs" Inherits="FrmMasterListing" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                       Master Listing
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>
                 <div class="portlet-body">
                  <div id="Depositdiv" runat="server">

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                               <label class="control-label col-md-2">All/Selected Product<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlallsel" runat="server" OnSelectedIndexChanged="ddlallsel_SelectedIndexChanged" Autopostback="true" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="A">All</asp:ListItem>
                                        <asp:ListItem Value="S">Selected</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2"></div>
                                  <label class="control-label col-md-2">Branch Code  <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtbrchcode" onkeypress="javascript:return isNumber (event)"  Enabled="false"  AutoPostBack="true" CssClass="form-control"  runat="server" />
                                </div>
                            </div>
                        </div>

                         

                      <div id="div_prd" runat="server" visible="false" class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                               
                               <label class="control-label col-md-2">Product Code <span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtprdcode" TabIndex="1" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" placeholder="Code" OnTextChanged="txtprdcode_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtprdname" TabIndex="2" CssClass="form-control" placeholder="Product Name" OnTextChanged="txtprdname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                     <div id="CustList" style="height:200px; overflow-y:scroll;" ></div>
                                    <asp:AutoCompleteExtender ID="autoFAFglname" runat="server" TargetControlID="txtprdname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetFAFGLName" CompletionListElementID="CustList">
                                                    </asp:AutoCompleteExtender>
                                </div>
                                <label class="control-label col-md-2">Account No <span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtaccno" TabIndex="3" onkeypress="javascript:return isNumber (event)"  CssClass ="form-control" placeholder="No" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtaccname" TabIndex="4" CssClass="form-control" placeholder="Account Name" OnTextChanged="txtaccname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                    <div id="CustList2" style="height:200px; overflow-y:scroll;" ></div>
                                                     <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtaccname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                        </div>

                       <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                               <label class="control-label col-md-2">All/Spec.Status<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlstatus" TabIndex="5" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="A">All</asp:ListItem>
                                        <asp:ListItem Value="S">Specific</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                       <div id="div_Status" visible="false" runat="server"  class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Specific Status<span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtstatusno" TabIndex="6" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="txtstatusno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtstatusname" TabIndex="7" CssClass="form-control" placeholder="Status Name" Enabled="false"  runat="server"></asp:TextBox>
                                </div>
                             </div>
                        </div>

                      <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                            <div class="col-lg-12">
                                <div class="col-md-6">
                                    <asp:Button ID="BthReport" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BthReport_Click" OnClientClick="javascript:return validate();" TabIndex="8"/>
                                      <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" TabIndex="9"/>
                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" TabIndex="10"/>
                                    </div>
                            </div>
                        </div>
                       </div>
                            </div>
                          </div>
                       </div>
                            </div>
                        

</asp:Content>

