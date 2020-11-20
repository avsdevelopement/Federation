<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSocietyMaster.aspx.cs" Inherits="FrmSocietyMaster" %>

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
             var CustNo =  document.getElementById("txtCustNo").value = "";
         var SocityName= document.getElementById("TxtSociety").value = "";
         var RegiNo = document.getElementById("TxtRegNo").value = "";
         if (CustNo == "" || SocityName == "" || RegiNo == "")
         {
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
                        ORGANISATION CUSTOMER
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
                                                        <asp:TextBox ID="txtCustNo"  AutoPostBack="true" runat="server" onkeypress="javascript:return isNumber (event)" CssClass="form-control" Height="29px" OnTextChanged="txtCustNo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Name Of Organisation<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtSociety" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2" Height="29px" OnTextChanged="TxtSociety_TextChanged"></asp:TextBox>
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
                                                        <label class="control-label ">Category<span class="required" > *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="ddlCategory" CssClass="form-control" TabIndex="2" runat="server" Height="29px"> 
                                                        <asp:ListItem Text="----Select----" Value="0" ></asp:ListItem>
<%-->--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                  
                                                </div>
                                            </div>

                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Address1<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-8">
                                                       <asp:TextBox ID="txtadd1" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2" ></asp:TextBox>
                                                    </div>
                                                  
                                                </div>
                                            </div>
                                                      <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                         <label class="control-label ">Address2<span> </span></label>
                                                    </div>
                                                       <div class="col-md-8">
                                                              <asp:TextBox ID="txtadd2" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2"></asp:TextBox>
                                                             </div>
                                                        </div>
                                            </div>
                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Road/Street<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                       <asp:TextBox ID="txtroad" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Near/Oposite<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtNear" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2" OnTextChanged="txtNear_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                               <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">State<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-lg-3 ">
                                                <asp:DropDownList ID="ddlstate" Height="29px" runat="server" CssClass="form-control"  TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>
                                            </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">District<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="ddlDist" Height="29px"  TabIndex="2" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                              <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Taluka<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-lg-3">
                                                <asp:DropDownList ID="ddlTal" Height="29px" runat="server"  CssClass="form-control" AutoPostBack="true"  TabIndex="2" EnableViewState="true" OnSelectedIndexChanged="ddlTal_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Villege/City<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtvillege" Height="29px" OnTextChanged="txtvillege_TextChanged" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                                          <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Pin Code<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-lg-3">
                                                <asp:TextBox ID="txtpin" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2" OnTextChanged="txtpin_TextChanged"></asp:TextBox>
                                            </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Date of Registration</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                       <asp:TextBox ID="TxtFY" onkeyup="FormatIt(this);"  onkeypress="javascript:return isNumber(event)"  placeholder="DD/MM/YYYY" runat="server" CssClass="form-control" TabIndex="2"  Height="29px" OnTextChanged="TxtFY_TextChanged"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TxtFY_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFY">
                                                        </asp:CalendarExtender>
                                                   </div>
                                                   
                                                   
                                                </div>
                                            </div>
                                              <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Spl Instructions</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                       <asp:TextBox ID="txtspint" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2" OnTextChanged="txtspint_TextChanged" ></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Pan Card No</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtpancard" Height="29px" onkeydown="return (event.keyCode!=13);" CssClass="form-control" runat="server" TabIndex="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-4 col-md-9">
                                      
                                        <asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="btn blue" OnClick="btn_submit_Click"  OnClientClick="javascript:return IsValidate() && Validate() &&TexBoxValidation();" TabIndex="16" />
                                        <asp:Button ID="btn_Modify" runat="server" Text="Modify" CssClass="btn blue" OnClick="btn_Modify_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                          <asp:Button ID="BTN_AUTH" runat="server" Text="Authorize" CssClass="btn blue" OnClick= "BTN_AUTH_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                         <asp:Button ID="btnclear" runat="server" Text="ClearAll" CssClass="btn blue" OnClick= "btnclear_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                         <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="btn blue" OnClick=  "btndelete_Click" OnClientClick="javascript:return IsValidate();" TabIndex="16" />
                                       
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
                                        <asp:TemplateField HeaderText="ORGANISATION NAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Society" runat="server" Text='<%# Eval(" ORGNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORGANISATION TYPE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SOCIETY_REG_NO" runat="server" Text='<%# Eval("CUSTCATG") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SPECIAL INST" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DT_REGI" runat="server" Text='<%# Eval("SPL_INST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="REGISTER DATE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="NOOFSHARES" runat="server" Text='<%# Eval("REGDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Edit" Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" runat="server"  OnClick= "lnkedit_Click" Text="MODIFY" CommandArgument='<%#Eval("CUSTNO")%>' CommandName="SELECT"  class="glyphicon glyphicon"></asp:LinkButton>
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

