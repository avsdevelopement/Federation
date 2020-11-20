<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAvs51213.aspx.cs" Inherits="FrmAvs51213" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

      <script>
          function isNumber(evt) {
              evt = (evt) ? evt : window.event;
              var charCode = (evt.which) ? evt.which : evt.keyCode;
              if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
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
    </script>

    

   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                 Application Details 
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills">
                                                    <li>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Delete</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="VW" class="btn btn-default" OnClick="lnkView_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>View</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkShrAllot" runat="server" Text="VW" class="btn btn-default" OnClick="lnkShrAllot_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Share Allotment</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkShrCancel" runat="server" Text="CL" class="btn btn-default" OnClick="lnkShrCancel_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Cancel Application</asp:LinkButton>
                                                    </li>
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="divShareApp" runat="server">

                                                
                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Application Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div id="Div1" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Society Type <span class="required">*</span></label>
                                                           
                                                            <div class="col-lg-2">
                                                                <asp:DropDownList ID="ddlSocietyType" OnSelectedIndexChanged="ddlSocietyType_SelectedIndexChanged" TabIndex="1" AutoPostBack="true" CssClass="form-control" runat="server">
                                                                   
                                                                </asp:DropDownList>
                                                            </div>
                                                           
                                                               <label class="control-label col-md-2">Society Name </label>
                                                          
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="txtMemName"  AutoPostBack="true" placeholder="Society Name" TabIndex="2" runat="server" CssClass="form-control" />
                                                            </div>
                                                           
                                                          
                                                        </div>
                                                    </div>

                                                    <div id="Div2" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Registration No <span class="required">*</span></label>
                                                            
                                                            <div class="col-lg-2">
                                                                <asp:TextBox ID="txtRegistration" placeholder="Registration No" CssClass="form-control" runat="server" TabIndex="3" onkeypress="javascript:return isNumber (event)"  AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Total Member No.<span class="required">*</span></label>
                                                      
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtmemnos" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" TabIndex="4" placeholder="Total Member No" runat="server"></asp:TextBox>
                                                            </div>
                                                              
                                                              <label class="control-label col-md-2">Date <span class="required">*</span></label>
                                                           
                                                            <div class="col-lg-2">
                                                                <asp:TextBox ID="txtdate" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="5"  AutoPostBack="true"></asp:TextBox>
                                                             <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtdate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                           
                                                           
                                                        </div>
                                                    </div>

                                                    <div id="Div3" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                           
                                                              <label class="control-label col-md-2">Address <span class="required">*</span></label>
                                                            
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" TabIndex="6"  placeholder="Enter Address" runat="server"></asp:TextBox>
                                                            </div>
                                                          
                                                              <label class="control-label col-md-1">State<span class="required">*</span></label>
                                                           
                                                            <div class="col-lg-2">
                                                                    <asp:DropDownList ID="ddstate" runat="server" CssClass="form-control" TabIndex="7" onblur="BindAddress()" OnSelectedIndexChanged="ddstate_SelectedIndexChanged"  AutoPostBack="true" EnableViewState="true" ></asp:DropDownList>
                                                                </div>
                                                                   <label class="control-label col-md-2">District<span class="required">*</span> </label>
                                                                <div class="col-lg-2">
                                                                    <asp:DropDownList ID="dddistrict" runat="server" OnSelectedIndexChanged="dddistrict_SelectedIndexChanged" TabIndex="8" CssClass="form-control" onblur="BindAddress()"  AutoPostBack="true" ></asp:DropDownList>
                                                               
                                                        </div>
                                                              
                                                            </div>
                                                    </div>

                                                         <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                             
                                                                <label class="control-label col-md-2">Taluka/City <span class="required">*</span> </label>
                                                                <div class="col-lg-2">
                                                                    <asp:DropDownList ID="ddtaluka" runat="server" OnSelectedIndexChanged="ddtaluka_SelectedIndexChanged" CssClass="form-control"  onkeypress="javascript:return isAddress (event)" AutoPostBack="true" ></asp:DropDownList>
                                                                   </div>
                                                                <label class="control-label col-md-2">Ward:</label>
                                                            <div class="col-md-2">
                                                                <%-- <asp:TextBox ID="txtWard" Placeholder="Ward" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="10" AutoPostBack="true"></asp:TextBox>
                                                                --%>
                                                                <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control"  TabIndex="20"></asp:DropDownList>
                                                            </div>
                                                                <label class="control-label col-md-2">PIN Code<span class="required">*</span></label>
                                                                <div class="col-lg-2">
                                                                    <asp:TextBox ID="txtpin" placeholder="PIN CODE" MaxLength="6" runat="server" onblur="BindAddress()" CssClass="form-control" pattern="[1-9][0-9]{5}" onkeypress="javascript:return isNumber (event)" TabIndex="36"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                   
                                                    <div id="Div5" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                           
                                                               <label class="control-label col-md-2">Mobile No<span class="required">*</span></label>
                                                        
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMob" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" CssClass="form-control" placeholder="Mobile No" runat="server"></asp:TextBox>
                                                            </div>
                                                            
                                                             <label class="control-label col-md-2">Email ID<span class="required">*</span></label>
                                                           
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtemailid" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" CssClass="form-control" placeholder="Email ID" runat="server"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-2">Permises Occupied<span class="required">*</span></label>
                                                 
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPermises"  AutoPostBack="true" CssClass="form-control" runat="server">
                                                                    <asp:ListItem Text="--  Select  --" Value="0" />
                                                                    <asp:ListItem Text="YES" Value="1" />
                                                                    <asp:ListItem Text="No" Value="2" />
                                                                  
                                                                </asp:DropDownList>  


                                                            </div>
                                                        </div>
                                                    </div>
                                                                                  

                                                    <div id="Div7" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">

                                                            <label class="control-label col-md-2">Share Amount<span class="required">*</span></label>
                                                    
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtShare" onkeypress="javascript:return isNumber (event)"  CssClass="form-control" placeholder="Share Amount" runat="server"></asp:TextBox>
                                                            </div>
                                                        
                                                        
                                                               <label class="control-label col-md-2">Entrance Fee <span class="required">*</span></label>
                                                    
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtEntFee" onkeypress="javascript:return isNumber (event)" onblur="CalAmt()" CssClass="form-control" placeholder="Entrance Fee" runat="server"></asp:TextBox>
                                                            </div>
                                                        
                                                             <label class="control-label col-md-2">M.A Subscrption </label>
                                                       
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtOther5" onkeypress="javascript:return isNumber (event)" placeholder="M.A Subscrption " CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                           
                                                        </div>
                                                    </div>

                                                  
                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Commitee Details: </strong></div>
                                                        </div>
                                                    </div>

                                                   <div runat="server" id="div6" visible="true">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                
                                                                <label class="control-label col-md-2">Chairman Name:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtNamed" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" PlaceHolder=" Name" TabIndex="51" runat="server"></asp:TextBox>
                                                                </div>
                                                                  <label class="control-label col-md-1">Mobile No1:</label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtdMob1" CssClass="form-control" MaxLength="10" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" PlaceHolder="Mobile No" TabIndex="52" runat="server"></asp:TextBox>

                                                                </div>
                                                        </div>
                                                            </div>
                                                       </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">

                                                                 <label class="control-label col-md-2">Secretary Name:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtsecname" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" PlaceHolder=" Name" TabIndex="51" runat="server"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-1">Mobile No1:</label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtMobile" CssClass="form-control" MaxLength="10" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" PlaceHolder="Mobile No" TabIndex="52" runat="server"></asp:TextBox>

                                                                </div>
                                                               
                                                                


                                                            </div>
                                                        </div>
                                                    </div>
                                                       



                                                

                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div id="Div13" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                        <div class="col-lg-12">
                                                            
                                                                <label class="control-label col-md-2">Payment Mode <span class="required">*</span></label>
                                                      
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Transfer" runat="server">
                                                        <div id="Div14" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                            <div class="col-lg-12">
                                                                
                                                                    <label class="control-label col-md-2">Product Code :<span class="required"> *</span></label>
                                                              
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtProdType1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="input-icon">
                                                                        <i class="fa fa-search"></i>
                                                                        <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div id="Div15" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                            <div class="col-lg-12">
                                                              
                                                                    <label class="control-label col-md-2">Acc No / Name:<span class="required"> *</span></label>
                                                          
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="input-icon">
                                                                        <i class="fa fa-search"></i>
                                                                        <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                                        <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                              
                                                                    <label class="control-label col-md-1">Balance:<span class="required"> *</span></label>
                                                               
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtBalance" CssClass="form-control" PlaceHolder="Balance" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divIntrument" runat="server">
                                                        <div id="Div16" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                            <div class="col-lg-12">
                                                                
                                                                    <label class="control-label col-md-2">Instrument No:<span class="required"> *</span></label>
                                                          
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" MaxLength="6" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>
                                                         
                                                                    <label class="control-label col-md-2">Instrument Date:<span class="required"> *</span></label>
                                                             
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divNarration" runat="server">
                                                        <div id="Div17" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                            <div class="col-lg-12">
                                                               
                                                                    <label class="control-label col-md-2">Narration:</label>
                                                          
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server" />
                                                                </div>
                                                               
                                                                    <label class="control-label col-md-1">Amount:</label>
                                                      
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtAmount" Enabled="false" CssClass="form-control" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                            <div id="divShareAllot" runat="server">

                                                <div class="row" style="margin: 5px 0 5px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-10">
                                                            <label class="control-label ">Additional Share Allotment Pending : </label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdAppDetails" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                                                        EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" Width="100%" EmptyDataText="No Remaining Appliaction for this branch">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Cust No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Member No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMemberNo" runat="server" Text='<%# Eval("MemberNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Customer Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="App No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAppNo" runat="server" Text='<%# Eval("AppNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderText="Date" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="NoOfSHR">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNoOfSHR" runat="server" Text='<%# Eval("NoOfSHR") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SHR Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotShrValue" runat="server" Text='<%# Eval("TotShrValue") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Enterence" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntFee" runat="server" Text='<%# Eval("EntFee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WelFare">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWelfare" runat="server" Text='<%# Eval("Other1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WelFare(Loan)">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblWelfareLoan" runat="server" Text='<%# Eval("MemberWelFee") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Select" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkBox" runat="server" onclick="Check_Click(this)" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
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

                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="Submit" runat="server" CssClass="btn blue" Text="Submit" OnClick="Submit_Click" OnClientClick="Javascript:return IsValide();" />
                                           <%--     <asp:Button ID="btnAllotment" runat="server" CssClass="btn blue" Text="Batch Allotment" OnClick="btnAllotment_Click" OnClientClick="Javascript:return IsValide();" />--%>
                                                <asp:Button ID="btnReceipt" runat="server" Text="Receipt" CssClass="btn blue" OnClick="btnReceipt_Click" Visible="false" />
                                                <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn blue" OnClick="Exit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Added by ankita on 26/06/2017--%>
            <div class="row" id="div_cashrct" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdCashRct" runat="server" AllowPaging="True"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99" 
                                            
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PRODUCT TYPE" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="AT" runat="server" Text='<%# Eval("AT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ACC No" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ACNO" runat="server" Text='<%# Eval("ACNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CUST NAME" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Name" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Amount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="NARRATION" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Particulars" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MAKER" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MAKER" runat="server" Text='<%# Eval("MAKER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Receipt" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkPrintReceipt" runat="server" CommandName="select" class="glyphicon glyphicon-plus" OnClick="LnkPrintReceipt_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Dens" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDens" runat="server" OnClick="lnkDens_Click" CommandArgument='<%#Eval("Dens")%>' CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

