<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAvs51214.aspx.cs" Inherits="FrmAvs51214" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
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

    <script>

        function Year(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "-";
            obj.value = obj.value;
        }
    </script>
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }


        function OnltAlphabets(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return true;

            return false;
        }

        function CheckFirstChar(key, txt) {
            if (key == 32 && txt.value.length <= 0) {
                return false;
            }
            else if (txt.value.length > 0) {
                if (txt.value.charCodeAt(0) == 32) {
                    txt.value = txt.value.substring(1, txt.value.length);
                    return true;
                }
            }
            return true;
        }

    </script>
    <script>

        function dateLen(dt) {
            var dt1 = dt + '';
            if (dt1.length == 1)
                dt1 = '0' + dt;

            return dt1;
        }
    </script>
    <script type="text/javascript">
        function IsValide() {

            if (prdcd == "") {
                message = 'Please Enter Case No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseNO.ClientID %>').focus();
                return false;
            }

            if (accno == "") {
                message = 'Please Enter  Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseY.ClientID %>').focus();
                return false;
            }

        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="upComplaintActions" runat ="server"   UpdateMode="Conditional" >
    <ContentTemplate >

    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption" id="DIVNAME">
                    
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="tab-pane active" id="tab__blue">
                                        <ul class="nav nav-pills">

                                            <li>
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a"  class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="1"><i class="fa fa-asterisk"></i>Society RECOVERY MASTER</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkCAse" runat="server" Visible="false" Text="VW" class="btn btn-default" OnClick="lnkCAse_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="2"><i class="fa fa-arrows"></i>CASE STATUS MASTER</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkAction" runat="server" Visible="false" Text="MD" class="btn btn-default" OnClick="lnkAction_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="3"><i class="fa fa-pencil-square-o"> </i>ACTION STATUS MASTER</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkstAcRec" runat="server" Text="RES" class="btn btn-default" OnClick="lnkstAcRec_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="3"><i class="fa fa-pencil-square-o"> </i>STATEMENT OF ACCOUNT RECOVERY</asp:LinkButton>
                                            </li>
                                           

                                            <li class="pull-right">
                                                <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">


                                                    <label class="control-label col-md-1">Case Year:<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseY" MaxLength="5" CssClass="form-control" runat="server" Placeholder="YY-YY" onkeyup="Year(this)" onkeypress="javascript:return isNumber (event)" TabIndex="5"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Case No:<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseNO" CssClass="form-control" Placeholder="Case No" OnTextChanged="txtCaseNO_TextChanged"  runat="server" TabIndex="6" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                       <label class="control-label col-md-1">Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDate" autopostback="true" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" OnTextChanged="txtDate_TextChanged" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDate">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                </div>
                                            </div>

                                             <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">


                                                  
                                                      <label class="control-label col-md-1">MemberNo:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMember"  CssClass="form-control" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"  PlaceHolder="Member No" TabIndex="7" runat="server"></asp:TextBox>
                                                            </div>

                                                  <%--  <label class="control-label col-md-2">Member Name:<span class="required">*</span></label>--%>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtSociName"  Placeholder="Society Name" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="8"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">

                                                <label class="control-label col-md-1">SRO No :<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtSRNO" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtSRNO_TextChanged" PlaceHolder="SRO No" TabIndex="9" runat="server" AutoPostBack="true"> </asp:TextBox>
                                                </div>
                                                <%--   <label class="control-label col-md-1">Name :<span class="required">*</span></label>
                                             --%>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TXTSROName" Placeholder="SrNo Name" Enabled="true" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="10"></asp:TextBox>
                                                </div>
                                                  <label class="control-label col-md-1">DefName :<span class="required">*</span></label>
                                             
                                                <div class="col-md-3">

                                                                    <asp:ListBox ID="lstarea" runat="server" CssClass="form-control" Style="height: 35px"  TabIndex="11" AutoPostBack="true"></asp:ListBox>

                                                                </div>


                                                </div>
                                               
                                                                                    
                                           
                                                                                
                                        <div id="Div2"  class="row" style="margin: 7px 0 7px 0">
                                                 <asp:label runat="server" ID="r1" class="control-label col-md-1"> RcoveryAmt:<span class="required">*</span></asp:label>
                                            
                                              <%--  <label class="control-label col-md-1">Rcovery Amt<span class="required">*</span></label>
                                             --%>   <div class="col-md-2">
                                                    <asp:TextBox ID="txtRecAmt" Placeholder="Rcovery Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="12"></asp:TextBox>
                                                </div>
                                                 <asp:label runat="server" ID="d1" class="control-label col-md-2">Paid by Defaulter<span class="required">*</span></asp:label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtamtd"  Placeholder="Amount Paid by Defaulter" OnTextChanged="txtamtd_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="13" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                           <asp:label runat="server" ID="b1" class="control-label col-md-1">Balance:<span class="required">*</span></asp:label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBalance"  Placeholder="Balance" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="14" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                        </div>
                                              <div id="Div4"  class="row" style="margin: 7px 0 7px 0">
                                                   <div>
                                                 <asp:Label runat="server" Visible="false" ID="lblra" class="control-label col-md-1"> Tot_RECAMT:<span class="required">*</span></asp:Label>
                                            </div>
                                              <%--  <label class="control-label col-md-1">Rcovery Amt<span class="required">*</span></label>
                                             --%>   <div class="col-md-2">
                                                    <asp:TextBox ID="txtrecamt1" Visible="false" Placeholder="Tot_REC Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="12"></asp:TextBox>
                                                </div>
                                                  <div>
                                                 <asp:Label runat="server" Visible="false" ID="P2" class="control-label col-md-1"> PRINCAMT:<span class="required">*</span></asp:Label>
                                            </div>
                                              <%--  <label class="control-label col-md-1">Rcovery Amt<span class="required">*</span></label>
                                             --%>   <div class="col-md-2">
                                                    <asp:TextBox ID="TXTPRINCIPL" Visible="false" Placeholder="PRINCIPAL Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="12"></asp:TextBox>
                                                </div>
                                                 
                                           <ASP:label RUNAT="server" id="o1" Visible="false" class="control-label col-md-1">OTHERCHARGES:<span class="required">*</span></ASP:label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtotherharges" visible="false" Placeholder="OTHERCHARGES" OnTextChanged="txtotherharges_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="14" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                  <asp:label runat="server" ID="pm1"  Visible="false" class="control-label col-md-1">Mode:</asp:label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPaymentMode"  Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" CssClass="form-control" TabIndex="11"></asp:DropDownList>

                                                            </div>
                                        </div>


                                             <div id="Div3"  visible="true"  class="row" style="margin: 7px 0 7px 0">
                                                 <div class="control-label col-md-1">
                                                 <asp:Label runat="server"  id="lbl1">FROMDATE:<span class="required">*</span></asp:Label>
                                                 </div>
                                                    <div class="col-md-2" id="f2" >
                                                        <asp:TextBox ID="txtfdate" autopostback="true" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" OnTextChanged="txtfdate_TextChanged" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfdate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfdate">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                 <div class="control-label col-md-1">
                                                
                                                  <asp:Label runat="server" ID="t1" >TODate:<span class="required">*</span></asp:Label>
                                                    </div>

                                                    <div class="col-md-2" id="t2" >
                                                        <asp:TextBox ID="TXTTODATE" autopostback="true" Placeholder="DD/MM/YYYY"  CssClass="form-control" runat="server" onkeyup="FormatIt(this)" OnTextChanged="TXTTODATE_TextChanged" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TXTTODATE">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TXTTODATE">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                 <div class="control-label col-md-1">
                                                 <asp:label runat="server"  id="p1" >TOTINTREST<span class="required">*</span></asp:label>
                                                
                                                     </div><div class="col-md-2" id="p2" >
                                                    <asp:TextBox ID="txtpd"  Placeholder=" TOTAL INTREST"  onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="13" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                           <%--<label class="control-label col-md-1">Balance:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TextBox3"  Placeholder="Balance" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="14" AutoPostBack="true"></asp:TextBox>
                                                </div>--%>
                                        </div>
                                                     <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div13">
                                                        <div class="col-lg-12" id="Div14">

                                                           <%-- <asp:label runat="server" ID="pm1"  Visible="false" class="control-label col-md-1">Payment Mode:</asp:label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPaymentMode"  Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" CssClass="form-control" TabIndex="11"></asp:DropDownList>

                                                            </div>--%>

                                                            <asp:label runat="server" ID="pbd" Visible="false" class="control-label col-md-1">PaidByDefAmt<span class="required">*</span></asp:label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtAmount" OnTextChanged="txtAmount_TextChanged" AutoPostBack="true"  Visible="false" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Amount" CssClass="form-control" runat="server" TabIndex="12"></asp:TextBox>
                                                            </div>
                                                            <asp:label runat="server" ID="lbltot" Visible="false" class="control-label col-md-1">Total Amount<span class="required">*</span></asp:label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txttotalamt" Visible="false" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Total Amount" CssClass="form-control" runat="server" TabIndex="14"></asp:TextBox>
                                                            </div>
                                                             <asp:label runat="server" ID="lblbal" Visible="false" class="control-label col-md-1">Balance<span class="required">*</span></asp:label>
                                                      
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtbal" Visible="false" placeholder="Balance" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" TabIndex="15"></asp:TextBox>
                                                        </div>
                                                             
                                                            
                                                        </div>
                                                    </div>
                                            <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div15" visible="false">
                                                        <div class="col-lg-12" id="Div16">
                                                            <asp:label runat="server" ID="cn1" Visible="false" class="control-label col-md-1">Cheque No</asp:label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtChequeNo" Visible="false" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Cheque No" CssClass="form-control" runat="server" TabIndex="14"></asp:TextBox>
                                                            </div>
                                                             <asp:label runat="server" ID="cd1" Visible="false" class="control-label col-md-1">Cheque Date :</asp:label>
                                                      
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtChequeDate" Visible="false" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" TabIndex="15"></asp:TextBox>
                                                        </div>
                                                        </div>
                                                  

                                                </div>

                                            <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div6" visible="false">
                                                        <div class="col-lg-12" id="Div7">
                                                          
                                                        </div>
                                                  

                                                </div>

                                     
                                        <div id="Div5" runat="server" class="row" style="margin: 7px 0 7px 0" >
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">CaseStatus:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                     <asp:DropDownList ID="ddlcasestaus" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcasestaus_SelectedIndexChanged" TabIndex="15"></asp:DropDownList>
                                                  
                                                           </div>
                                               
                                                <label class="control-label col-md-1">Action Status : <span class="required">*</span></label>
                                               
                                                    <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlActstatus" runat="server" CssClass="form-control" TabIndex="16" OnSelectedIndexChanged="ddlActstatus_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                                            </div>
                                                 <label class="control-label col-md-2">Notice:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                     <asp:DropDownList ID="ddlNotice" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlNotice_SelectedIndexChanged" AutoPostBack="true" TabIndex="15"></asp:DropDownList>
                                                  
                                                           </div>
                                                    
                                                 
                                            
                                            </div>
                                        </div>


                                              <div id="Div9" runat="server" class="row" style="margin: 7px 0 7px 0" >
                                            <div class="col-lg-12">
                                                  <label class="control-label col-md-1">Remark:<span class="required">*</span></label>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtRemark"  TextMode="MultiLine" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);"  PlaceHolder="Remark" TabIndex="17" runat="server"></asp:TextBox>
                                                                </div>
                                            </div>
                                        </div>

                                 <div class="row">
                                                <div class="col-lg-12"></div>
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.41);"></div>
                                                    <div class="col-md-4"><strong style="color: #3598dc"></strong></div>

                                                </div>
                                            </div>
                                    </div>
                                </div>
                            </div>
                         </div>
                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-10">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Button ID="BtnSubmit"  runat="server" CssClass="btn btn-primary" Text="SOC_Submit" OnClick="BtnSubmit_Click" TabIndex="20" OnClientClick="Javascript:return IsValide()" />
                                        <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="21" />
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="22" />
                                        <asp:Button ID="BtnReport" runat="server" CssClass="btn btn-primary" Visible="false" Text="Report" OnClick="BtnReport_Click" TabIndex="22" />
                                        <asp:Button ID="btnpayment" runat="server" CssClass="btn btn-primary" Text="SUBMIT" Visible="true" OnClick="btnpayment_Click" TabIndex="22" />
                                         <asp:Button ID="BtnDownloadcase" runat="server" CssClass="btn btn-primary" Text="Case_Download" OnClick="BtnDownloadcase_Click" TabIndex="22" />
                                        <asp:Button ID="BtnDownload_action" runat="server" CssClass="btn btn-primary" Text="Action_Download" OnClick="BtnDownload_action_Click" TabIndex="22" />
                                        <asp:Button ID="BtnDownload" runat="server" CssClass="btn btn-primary" Text="Socieity_Download" OnClick="BtnDownload_Click" TabIndex="22" />
                                    </div>
                                    <div class="col-md-5">
                                    </div>
                                </div>
                            </div>

                           <div class="row" runat="server" id="div_Grid" visible="false">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GrdDemand" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GrdDemand_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                     <asp:TemplateField HeaderText="SRNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SOCIeTYID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRO_NO" runat="server" Text='<%# Eval("SRO_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Notice Issue Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="NOTICE_ISS_DT" runat="server" Text='<%# Eval("NOTICE_ISS_DT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkModify" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkModify_Click1" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDelete_Click1" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                  <%--  <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                        <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                  
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#FFFF99" />
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
                            <div class="row" runat="server" id="DIVCASE" visible="false">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GRdCASE" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GRdCASE_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                     <asp:TemplateField HeaderText="SRNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SOCIeTYID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRO_NO" runat="server" Text='<%# Eval("SRO_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Notice Issue Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="NOTICE_ISS_DT" runat="server" Text='<%# Eval("NOTICE_ISS_DT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lnkedit" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="Lnkedit_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkcancle" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkcancle_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                  <%--  <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkShow" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkShow_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                        <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                  
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#FFFF99" />
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
                            <div class="row" runat="server" id="DIVACTION" visible="false">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GRDACTION" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GRDACTION_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    
                                                     <asp:TemplateField HeaderText="SRNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SOCIeTYID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRO_NO" runat="server" Text='<%# Eval("SRO_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Notice Issue Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="NOTICE_ISS_DT" runat="server" Text='<%# Eval("NOTICE_ISS_DT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkModify2" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkModify2_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete2" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDelete2_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                  <%--  <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView2" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("SOCIeTYID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView2_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                        <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                  
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#FFFF99" />
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

                            <div class="row" runat="server" id="divstate" visible="false">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="Grd_statement" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="Grd_statement_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRO_NO" runat="server" Text='<%# Eval("SRO_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSTAmd" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkSTAmd_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                   
                                                </Columns>
                                                        <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                  
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#FFFF99" />
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
                            <div class="row" runat="server" id="div8" visible="false">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GRIDVIEWACCST" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GRIDVIEWACCST_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                     
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO1" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER1" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRO_NO1" runat="server" Text='<%# Eval("SRO_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DATE1" runat="server" Text='<%# Eval("ENTRYDATE1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MEMERNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="MEMERNO" runat="server" Text='<%# Eval("MEMBERNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSTACCMD" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkSTACCMD_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                   
                                                </Columns>
                                                        <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                  
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#FFFF99" />
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
<div class="row" runat="server" id="div_allsta" visible="false">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="grd_allstatement" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="grd_allstatement_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO1" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER1" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRO_NO1" runat="server" Text='<%# Eval("SRO_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DATE1" runat="server" Text='<%# Eval("ENTRYDATE1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MEMERNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="MEMERNO" runat="server" Text='<%# Eval("MEMBERNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSTACCMDAll" runat="server" CommandArgument='<%#Eval("Id")+"_"+Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkSTACCMDAll_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LnkCAnALL" runat="server" CommandArgument='<%#Eval("Id")+"_"+Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="LnkCAnALL_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                   
                                                </Columns>
                                                        <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                  
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#FFFF99" />
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
    


    </ContentTemplate>
    <Triggers >
   
          <%--<asp:PostBackTrigger ControlID="btnExportToExcel" EventName="btnExportToExcel_Click" /> --%>
          <asp:PostBackTrigger ControlID ="BtnDownload" />
          <asp:PostBackTrigger ControlID ="BtnDownloadcase" />
          <asp:PostBackTrigger ControlID ="BtnDownload_action" />
    </Triggers>
</asp:UpdatePanel>

</asp:Content>

