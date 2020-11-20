<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanCaseFile.aspx.cs" Inherits="FrmLoanCaseFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script>

          function FormatIt(obj) {
              if (obj.value.length == 2) //DAY
                  obj.value = obj.value + "/";
              if (obj.value.length == 5) //MONTH
                  obj.value = obj.value + "/";
              if (obj.value.length == 11) //YEAR
                  alert("Enter Valid Date!....");
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    Loan Case File
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="row" style="margin-bottom: 10px;">
                                      
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">BRCD:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBRCD" Placeholder="BRCD" CssClass="form-control" runat="server" OnTextChanged="TxtBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtBRCDName" CssClass="form-control" runat="server" OnTextChanged="TxtBRCDName_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Prod Code:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtProdCde" Placeholder="Product Code" CssClass="form-control" runat="server" OnTextChanged="TxtProdCde_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtProdName" CssClass="form-control" runat="server" OnTextChanged="TxtProdName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                     <div id="ProdList" style="height: 200px; overflow-y:scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProdName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="ProdList">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Accno:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAccno" Placeholder="Account No." CssClass="form-control" runat="server" OnTextChanged="TxtAccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtAccname" CssClass="form-control" runat="server" OnTextChanged="TxtAccname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <div id="accnamelist" style="height: 200px; overflow-y:scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoaccname" runat="server" TargetControlID="TxtAccname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="accnamelist">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                 <label class="control-label col-md-1">Cust No:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtCustno" Placeholder="Cust No." CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                 <label class="control-label col-md-1">Acc status:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAccSts" Placeholder="Account Status" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Reason:<span class="required">*</span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtReason" Placeholder="Write Here.." TextMode="MultiLine" CssClass="form-control" runat="server" OnTextChanged="TxtProdCde_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                               
                                                <label class="control-label col-md-2">Case-Of-Date:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtCaseOfDate" type="text" PlaceHolder="dd/mm/yyyy" onkeyup="FormatIt(this)" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtCaseOfDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtCaseOfDate">
                                                            </asp:CalendarExtender>
                                                </div> 
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click"/>
                                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click"/>
                                                            <asp:Button ID="BtnAuthorize" runat="server" CssClass="btn btn-primary" Text="Authorize" OnClick="BtnAuthorize_Click"/>
                                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click"/>
                                                        </div>
                                                        <div class="col-md-5">
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
</asp:Content>

