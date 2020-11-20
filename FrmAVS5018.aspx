<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5018.aspx.cs" Inherits="FrmAVS5018" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <script type="text/javascript">
     function validate() {
            var message = '';

            debugger;

         var membtype = document.getElementById('<% =DDLReturn.ClientID%>').value;
         if (membtype == "0") {
             //alert("Please enter product ID");
             message = 'Please Enter Return type....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =DDLReturn.ClientID%>').focus();
                return false;
            }

            var membtype = document.getElementById('<% =Ddlacctype.ClientID%>').value;
            if (membtype == "0") {
                //alert("Please enter product ID");
                message = 'Please Enter Acc type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =Ddlacctype.ClientID%>').focus();
                return false;
            }

         var effectdate = document.getElementById('<% =txtEffectivedate.ClientID%>').value;
         if (effectdate == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Effect date....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =txtEffectivedate.ClientID%>').focus();
                return false;
            }
          var membtype = document.getElementById('<% =DDLSkipCharges.ClientID%>').value;
         if (membtype == "0") {
             //alert("Please enter period from");
             message = 'Please Enter SkipCharges....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =DDLSkipCharges.ClientID%>').focus();
                return false;
            }

         var membtype = document.getElementById('<% =DDLAllow.ClientID%>').value;
         if (membtype == "0") {
             // alert("Please select period to type");
             message = 'Please select Allow TOD....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =DDLAllow.ClientID%>').focus();
                return false;
         }


         var membtype = document.getElementById('<% =DDLFrequency.ClientID%>').value;
         if (membtype == "0") {
             //alert("Please enter period To");
             message = 'Please Enter Frequency....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =DDLFrequency.ClientID%>').focus();
                return false;
            }


         var membtype = document.getElementById('<% =txtFlatrate.ClientID%>').value;
         if (membtype == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Flat Charges....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =txtFlatrate.ClientID%>').focus();
                return false;
            }

         var membtype = document.getElementById('<% =txtInsterest.ClientID%>').value;
         if (membtype == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Flat Charges....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =txtInsterest.ClientID%>').focus();
             return false;
         }

         var membtype = document.getElementById('<% =txtdAcctid.ClientID%>').value;
         if (membtype == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Flat Charges....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =txtdAcctid.ClientID%>').focus();
             return false;
         }



         var effectdate = document.getElementById('<% =Txtmaxcharges.ClientID%>').value;
         if (effectdate == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Max Charges...!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =Txtmaxcharges.ClientID%>').focus();
             return false;
         }
         var effectdate = document.getElementById('<% =Txtmincharges.ClientID%>').value;
         if (effectdate == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Min Charges....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =Txtmincharges.ClientID%>').focus();
             return false;
         }




            var membtype = document.getElementById('<% =TxtGst.ClientID%>').value;
            if (membtype == "") {
                //alert("Please enter product name");
                message = 'Please Enter Return Type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtGst.ClientID%>').focus();
                return false;
            }

          
       



            var membtype = document.getElementById('<% =Txtgstinster.ClientID%>').value;
            if (membtype == "") {
                //alert("Please Enter Rate");
                message = 'Please Enter Flat Charges....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =Txtgstinster.ClientID%>').focus();
                return false;
            }

        
         var effectdate = document.getElementById('<% =TxtGstAmount.ClientID%>').value;
         if (effectdate == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter PL Accid....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =TxtGstAmount.ClientID%>').focus();
                return false;
            }
       
         var effectdate = document.getElementById('<% =DdlReason.ClientID%>').value;
         if (effectdate == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Particular....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =DdlReason.ClientID%>').focus();
             return false;
         }
         var effectdate = document.getElementById('<% =DDLFLatCharges.ClientID%>').value;
         if (effectdate == "0") {
             //alert("Please Enter Rate");
             message = 'Please Enter Reason...!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =DDLFLatCharges.ClientID%>').focus();
             return false;
         }


         var effectdate = document.getElementById('<% =Txtpert.ClientID%>').value;
         if (effectdate == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Particular....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =Txtpert.ClientID%>').focus();
             return false;
         }

         var effectdate = document.getElementById('<% =txtLastAppDate.ClientID%>').value;
         if (effectdate == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Particular....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =txtLastAppDate.ClientID%>').focus();
             return false;
         }
         var effectdate = document.getElementById('<% =txtParticular.ClientID%>').value;
         if (effectdate == "") {
             //alert("Please Enter Rate");
             message = 'Please Enter Particular....!!\n';
             $('#alertModal').find('.modal-body p').text(message);
             $('#alertModal').modal('show')
             document.getElementById('<% =txtParticular.ClientID%>').focus();
             return false;
         }
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        INWARD / OUTWARD RETRUN CHARGES MASTER
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Branch Code <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtBrcode" CssClass="form-control" Placeholder="Branch Code" OnTextChanged="txtBrcode_TextChanged" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBranch" CssClass="form-control" AutoPostBack="true" runat="server" />

                                                </div>

                                            
                                            
                                            <div class="col-md-2" style="text-align: right">
                                                    <label class="control-label ">Return Type :</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtTReturnType" runat="server" CssClass="form-control" OnTextChanged="TxtTReturnType_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DDLReturn" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLReturn_SelectedIndexChanged" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            
                                               </div>
                                             </div>
                                       

                                       
                                      
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Account Type </label>

                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtAccType" CssClass="form-control" runat="server" OnTextChanged="txtAccType_TextChanged" AutoPostBack="true" TabIndex="7"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="Ddlacctype" runat="server" CssClass="form-control" OnSelectedIndexChanged="Ddlacctype_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" TabIndex="8"></asp:DropDownList>
                                                    <%--<asp:TextBox ID="txtAccTypeName" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                </div>

                                                    <div  class="col-md-2" style="text-align: right">
                                                    <label class="control-label ">Effective Date :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtEffectivedate" onkeyup="FormatIt(this)" placeholder="DD/MM/YYYY" OnTextChanged="txtEffectivedate_TextChanged" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="2" Height="29px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtEffectivedate">
                                                    </asp:CalendarExtender>
                                                </div>

                                            </div>
                                            </div>
                                        </div>



                                    <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                            </div>
                                        </div>
                                        <br />
                                       
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                              
                                                <div class="col-md-2">
                                                    <label class="control-label ">Skip Charges:<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtSkipcharges" AutoPostBack="true" TabIndex="2" runat="server" OnTextChanged="TxtSkipcharges_TextChanged" CssClass="form-control" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DDLSkipCharges" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLSkipCharges_SelectedIndexChanged" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                
                                                  <div class="col-md-2" style="text-align: right">
                                                      <label class="control-label ">Allow TOD :</label>
                                                  </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtAllowTOD" runat="server" CssClass="form-control" OnTextChanged="TxtAllowTOD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DDLAllow" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLAllow_SelectedIndexChanged" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                       

                                       
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Frequency Of Appln :<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtFrequencyAppln" AutoPostBack="true" TabIndex="2" runat="server" OnTextChanged="TxtFrequencyAppln_TextChanged" CssClass="form-control" />
                                                </div>

                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DDLFrequency" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLFrequency_SelectedIndexChanged" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Charges Parameter : </strong></div>
                                            </div>
                                        </div>
                                        <br />


                                     
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Flat Rate <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtFlatrate" placeholder="Flat Rate" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2" style="text-align: right">
                                                    <label class="control-label ">Interest Rate : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtInsterest" placeholder="Interest Rate" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                

                                       
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">PL CR Acct.Id :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtdAcctid" AutoPostBack="true" runat="server" OnTextChanged="txtdAcctid_TextChanged" CssClass="form-control" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtdAccNo" AutoPostBack="true" OnTextChanged="txtdAccNo_TextChanged" runat="server" CssClass="form-control" />
                                                    <div id="AccList" style="height: 50px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglnameA" runat="server" TargetControlID="txtdAccNo"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="AccList">
                                                    </asp:AutoCompleteExtender>
                                                </div>

                                            </div>

                                        </div>


                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">MAX Charges <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="Txtmaxcharges" placeholder="MAX Charges" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2" style="text-align: right">
                                                    <label class="control-label ">Min Charges<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="Txtmincharges" placeholder="Min Charges" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                   
                                        <div class="row" style="margin-bottom: 5px;">
                                           <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">GST GL  <span class="required">*</span></label>
                                                     </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtGst" placeholder="GST GL" CssClass="form-control" OnTextChanged="TxtGst_TextChanged" AutoPostBack="true" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtGstGL" AutoPostBack="true" OnTextChanged="TxtGstGL_TextChanged" runat="server" CssClass="form-control" />
                                                    <div id="Div1" style="height: 10px;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtdAccNo"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="AccList">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                    <div  class="col-md-2" style="text-align: right">
                                                    <label class="control-label ">GST Interest<span class="required">*</span></label>
                                                </div>
                                       
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="Txtgstinster" placeholder="Min Charges" CssClass="form-control" runat="server" />
                                             </div>

                                            </div>

                                        </div>

                                                     
                                          
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">

                                                    <label class="control-label ">GST Amt<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtGstAmount" placeholder="Min Charges" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Charges Parameter : </strong></div>
                                            </div>
                                        </div>
                                        <br />
                                       

                                    
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                <label class="control-label ">Reason</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtReason" AutoPostBack="true" TabIndex="2" OnTextChanged="TxtReason_TextChanged" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="DdlReason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlReason_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                          
                                             <div class="col-md-2" style="text-align: right">
                                                <label class="control-label ">Flat chg </label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtflatChg" AutoPostBack="true" TabIndex="2" OnTextChanged="TxtflatChg_TextChanged" runat="server" CssClass="form-control" />
                                            </div>
                                          <div class="col-md-2">
                                                    <asp:DropDownList ID="DDLFLatCharges" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLFLatCharges_SelectedIndexChanged" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                  </div>
                                     
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                <label class="control-label ">Percent Charge </label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txtpert" AutoPostBack="true" TabIndex="2" runat="server" OnTextChanged="Txtpert_TextChanged" CssClass="form-control" />
                                            </div>
                                          <div class="col-md-2">
                                                    <asp:DropDownList ID="DDLPercentage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLPercentage_SelectedIndexChanged" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>

                                        </div>
                                    </div>






                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Process Status : </strong></div>
                                            </div>
                                        </div>
                                        <br />
                                      
                                        <div class="row" style="margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Last Applied Date : </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtLastAppDate" onkeyup="FormatIt(this)" CssClass="form-control" runat="server" />
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtLastAppDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>




                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Particular :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtParticular" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>

                                        <br />
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                            </div>
                                        </div>
                                        <br />
                                
                                     
                                        <div class="row" style="margin: 07px 0 1px 0">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn green" Text="Submit" OnClick="BtnSubmit_Click"  OnClientClick="javascript:return validate();"  />
                        </div>
                                   <div class="col-md-2">
                                    <asp:Button ID="BtnClear" runat="server" CssClass="btn green" Text="Clear" OnClick="BtnClear_Click1"  />
                                      </div>
                                  <div class="col-md-2">
                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn green" Text="Exit" OnClick="BtnExit_Click1"   />
                                      </div>
                                  <div class="col-md-2">
                                    <asp:Button ID="BtnModify" runat="server" CssClass="btn green" Text="Modify" OnClick="BtnModify_Click"   Visible="false"/>
                                      </div>
                               
                                  <div class="col-md-2">
                                    <asp:Button ID="BtnDelete" runat="server" CssClass="btn green" Text="Delete" OnClick="BtnDelete_Click"  Visible="false" />
                                      </div>
                                  <div class="col-md-2">
                                      <asp:Button ID="BtnAuthorize" runat="server" CssClass="btn green" Text="Authorize" OnClick="BtnAuthorize_Click" Visible="false" />
                                      </div>
                                </div>
                            </div>
                        </div>

<div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdIntrstMaster" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" AllowPaging="True"
                                    EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" OnPageIndexChanging="grdIntrstMaster_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                          <asp:TemplateField HeaderText="ID" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BRCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acct_Type" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Acct_Type" runat="server" Text='<%# Eval("Acct_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Retrun_Type" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Retrun_Type" runat="server" Text='<%# Eval("Retrun_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EffectiveDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Effective_Date" runat="server" Text='<%# Eval("Effective_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Skip_charges">
                                            <ItemTemplate>
                                                <asp:Label ID="Skip_charges" runat="server" Text='<%# Eval("Skip_charges") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Frequency">
                                            <ItemTemplate>
                                                <asp:Label ID="Frequency" runat="server" Text='<%# Eval("Frequency") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Allow">
                                            <ItemTemplate>
                                                <asp:Label ID="Allow" runat="server" Text='<%# Eval("Allow") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="PERIOD TYPE " Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PERIODTYPE2" runat="server" Text='<%# Eval("PERIODTYPE2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> --%>

                                        <asp:TemplateField HeaderText="Flat_rate " Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Flat_rate" runat="server" Text='<%# Eval("Flat_rate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Interset_rate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Interset_rate" runat="server" Text='<%# Eval("Interset_rate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PLACCNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PLACCNO" runat="server" Text='<%# Eval("PLACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Min_charges" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Min_charges" runat="server" Text='<%# Eval("Min_charges") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Max_charges" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Max_charges" runat="server" Text='<%# Eval("Max_charges") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GST_SUBGL" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="GST_SUBGL" runat="server" Text='<%# Eval("GST_SUBGL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                          <asp:TemplateField HeaderText="GST_InterestRate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="GST_InterestRate" runat="server" Text='<%# Eval("GST_InterestRate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GST_Amount" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="GST_Amount" runat="server" Text='<%# Eval("GST_Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reason" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Reason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Reason_description" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Reason_description" runat="server" Text='<%# Eval("Reason_description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Flat_Charges" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Flat_Charges" runat="server" Text='<%# Eval("Flat_Charges") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Percent_Charge" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Percent_Charge" runat="server" Text='<%# Eval("Percent_Charge") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                          <asp:TemplateField HeaderText="Last_ApplDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Last_ApplDate" runat="server" Text='<%# Eval("Last_ApplDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Particular" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Particular" runat="server" Text='<%# Eval("Particular") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Add New" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAdd" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkAdd_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkDelete_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Authorization" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthor" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkAuthor_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
          
</asp:Content>

