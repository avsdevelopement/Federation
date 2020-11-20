<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="AddcustDetails.aspx.cs" Inherits="AddcustDetails" %>

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

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
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
  <script type="text/javascript">

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

    </script>
  
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">

        function Validate() {
            var ddlFruits = document.getElementById("ddlCategory");
            if (ddlFruits.value == 0) {
                //If the "Please Select" option is selected display error.
                alert("Please select an option!");
                return false;
            }
            return true;
        }
</script>
     <script type="text/javascript">
         debugger;
         function TexBoxValidation() {
             var CustNo = document.getElementById("txtCustNo").value = "";
             var SocityName = document.getElementById("TxtSociety").value = "";
             var RegiNo = document.getElementById("TxtRegNo").value = "";
             if (CustNo == "" || SocityName == "" || RegiNo == "") {
                 alert("Please Fill mandotory Field!")
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
                       Additional Customer Details.
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                             <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Mem / Cust No<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCustNo" OnTextChanged="txtCustNo_TextChanged"  AutoPostBack= "true" runat="server" onkeypress="javascript:return isNumber (event)" CssClass="form-control" Height="29px"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Name Of Organisation<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtSociety" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1" Height="29px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Turnover<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtturnover" onkeydown="return (event.keyCode!=13);" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="1" Height="29px"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">No Of Employees:<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtemp" onkeydown="return (event.keyCode!=13);" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="1" Height="29px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                                 <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">location<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtloc" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1" Height="29px"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Currencies:<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtcurreny" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1" Height="29px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Other Bankers:<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtbank1" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1" Height="29px"></asp:TextBox>
                                                    </div>
                                                   
                                                </div>
                                            </div>
                                             <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-3">
                                                  <label class="control-label ">Reason To Become Customer: <span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-8">
                                                       <asp:TextBox ID="txtreson" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1" ></asp:TextBox>
                                                    </div>
                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            
                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    
                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                  
                                              </div>
                                            </div>
                                                     <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Org/Industry type<span class="required"> *</span></label>
                                                    </div>
                                                      <div class="col-md-3">
                                                        <asp:DropDownList ID="ddlorgtype" CssClass="form-control" runat="server" TabIndex="1" Height="29px"> 
                                                        <asp:ListItem Text="----Select----" Value="0" ></asp:ListItem>

                                                      </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Business Type/Line:<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtbusinesstype" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1" Height="29px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                                    

                                              <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">No of Branches<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                       <asp:TextBox ID="txtnoonbranch" Height="29px" onkeypress="javascript:return isNumber (event)" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                     <div class="col-md-2">
                                                        <label class="control-label ">date of Estd/Inc<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                       <asp:TextBox ID="txtestd" onkeyup="FormatIt(this);"  onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" TabIndex="1" Height="29px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtestd">
                                                        </asp:CalendarExtender>
                                                   </div>
                                                </div>
                                            </div>
                                               <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Place of Estd<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                       <asp:TextBox ID="txtplaceestd" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Registration No<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtregno" Height="29px" onkeydown="return (event.keyCode!=13);" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">AML rating<span class="required"> *</span></label>
                                                    </div>
                                                     <div class="col-md-3">
                                                        <asp:DropDownList ID="ddlaml" CssClass="form-control" runat="server" TabIndex="1" Height="29px"> 
                                                        <asp:ListItem Text="----Select----" Value="0" ></asp:ListItem>
                                                      </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Registering Auth.:<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtauth" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                               <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Commencement Dt:<span class="required"> *</span></label>
                                                    </div>
                                                  <div class="col-md-3">
                                                      <asp:TextBox ID="txtcommdt" onkeyup="FormatIt(this);"  onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" TabIndex="1" Height="29px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtcommdt">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Date since Customer:<span class="required"> *</span></label>
                                                    </div>
                                                   <div class="col-md-3">
                                                      <asp:TextBox ID="txtdatesc" onkeyup="FormatIt(this);"  onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" TabIndex="1" Height="29px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdatesc">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                              <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">KYC<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-lg-3">
                                                <asp:DropDownList ID="ddlTal" Height="29px" runat="server" TabIndex="1" CssClass ="form-control" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="ddlTal_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Relationship Officer:<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TXTRELOFF" Height="29px"  OnTextChanged=   "TXTRELOFF_TextChanged" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                                          <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Adhar No<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-lg-3">
                                                <asp:TextBox ID="txtadhar" Height="29px" onkeydown="return (event.keyCode!=13);" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                            </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Registering Place<span class="required"> *</span></label>
                                                    </div>
                                                   <div class="col-lg-3">
                                                <asp:TextBox ID="txtregplace" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                            </div>
                                                   
                                                </div>
                                            </div>
                                                    <div style="border: 1px solid #3598dc"></div>
                                        <div class="portlet-body">
                                            
                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    
                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                  
                                              </div>
                                            </div>
                                                    </div>
                                                      <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">H.O Address:<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                       <asp:TextBox ID="txthoadd" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">City<span class="required"> *</span></label>
                                                    </div>
                                                     <div class="col-md-3">
                                                       <asp:TextBox ID="txtcity" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                                   <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">PIN:<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                       <asp:TextBox ID="txtpin" Height="29px" onkeydown="return (event.keyCode!=13);" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Phone<span class="required"> *</span></label>
                                                    </div>
                                                     <div class="col-md-3">
                                                       <asp:TextBox ID="txtphone" Height="29px" onkeydown="return (event.keyCode!=13);" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                                 <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">FAX<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-8">
                                                       <asp:TextBox ID="txtfax" Height="29px" onkeydown="return (event.keyCode!=13);" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="1" ></asp:TextBox>
                                                    </div>
                                                  
                                                </div>
                                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-4 col-md-9">
                                       <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn blue" OnClick=  "btn_submit_Click"   OnClientClick="javascript:return IsValidate() && Validate() &&TexBoxValidation();" TabIndex="16" />
                                       <asp:Button ID="BTNMOD" runat="server" Text="MODIFY" CssClass="btn blue" OnClick=   "BTNMOD_Click"   OnClientClick="javascript:return IsValidate() && Validate() &&TexBoxValidation();" TabIndex="16" />
                                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" CssClass="btn blue" OnClick= "btnClear_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                       
                                    </div>
                                </div>
                            </div>




                             <asp:GridView ID="SocietyGrid" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="SocietyGrid_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Cust No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Society Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SOCIETY_NAME" runat="server" Text='<%# Eval(" ORGNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BUSNESS LINE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SOCIETY_REG_NO" runat="server" Text='<%# Eval("BUSLINE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TURNOVER " Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DT_REGI" runat="server" Text='<%# Eval("TURNOVER") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="NO OF BRANCHES" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="NOOFSHARES" runat="server" Text='<%# Eval("NOOFBR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="REGISTRATION NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SOC_NAME_OF_THE_REG" runat="server" Text='<%# Eval("REGNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Edit" Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" runat="server"  OnClick="lnkedit_Click" Text="MODIFY" CommandArgument='<%#Eval("CUSTNO")%>' CommandName="SELECT"  class="glyphicon glyphicon"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                                
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <SelectedRowStyle BackColor="#66FF99" />
                                    <EditRowStyle BackColor="#FFFF99" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
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

