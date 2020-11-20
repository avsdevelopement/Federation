<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDashBoard.aspx.cs" Inherits="FrmDashBoard" %>


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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel='stylesheet' href="global/css/main.css" type='text/css' media='all' />
    <link href="CSS/jquery.fancybox.css" rel="stylesheet" />
    <script src="js/jquery-1.8.2.min.js"></script>
    <script src="global/Scripts/popup/jquery.min.js"></script>
    <script src="js/jquery.fancybox.js"></script>
    <script src="js/jquery.fancybox.pack.js"></script>
    <style>
        a.abc {
            color: #00B0FF;
        }
    </style>
    <style>
        .th {
            text-align: center;
        }
    </style>
    <div id="page-wrapper">
        <div class="row">
            <marquee direction="left"><strong> <a href="#" style="color:limegreen">For Online Support Contact 1 : 9653165930 And Contact 2 : 9137193274</a></strong></marquee>


            <table>
                <tbody>
                    <tr>
                        <td align="left">
                            <span style="float: left; margin-top: -2.2%;">
                                <img src="global/images/launch_icon_en_IN.png" style="border: none;"></span>
                        </td>
                        <td>
                            <marquee direction="left" style="width: 910px;"><strong><asp:Literal ID="lt1" runat="server"></asp:Literal></strong></marquee>
                        </td>
                    </tr>
                </tbody>
            </table>

            <div class="col-md-12">
                <div class="col-md-12">
                    <h1 class="page-header">Dashboard</h1>
                </div>

                <div class="row">
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-green">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="lblDeposite" runat="server" />
                                        </div>
                                        In Lakh
                                        <div>Term Deposits</div>
                                    </div>
                                </div>
                            </div>
                            <a href="FrmDashBoard.aspx?Flag=DP">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details </span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>

                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-maroon">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="lblCASADep" runat="server" />
                                        </div>
                                        In Lakh
                                        <div>CASA Deposits</div>
                                    </div>
                                </div>
                            </div>
                            <a href="FrmDashBoard.aspx?Flag=CS">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-red">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="lblDDS" runat="server" />
                                        </div>
                                        In Lakh
                                        <div>DDS Collection</div>
                                    </div>
                                </div>
                            </div>
                            <a href="FrmDashBoard.aspx?Flag=PIGMY">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>


                    </div>

                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-red">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="LblDeptotal" runat="server" />
                                        </div>
                                        In Lakh
                                        <div>Total Deposit</div>
                                    </div>
                                </div>
                            </div>

                        </div>


                    </div>




                </div>

                <div class="row">
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="lblHeading" runat="server" Text=" Maturity" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="BtnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="BtnShow_Click" />
                                    </div>
                                </div>
                                <div id="Div_AccDisplay" runat="server">
                                    <div class="row">
                                        <div class="col-xs-12 text-left">
                                            <div>
                                                Account &nbsp;&nbsp;&nbsp;&nbsp;
                                         <asp:Label ID="lblAccCount" runat="server" />
                                            </div>
                                            <div>
                                                Amount &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblAmount" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <a href="FrmDashBoard.aspx?Flag=MAT">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="lblLoans" runat="server" />
                                        </div>
                                        In Lakh
                                        <div>Total Loans</div>
                                    </div>
                                </div>
                            </div>
                            <a href="FrmDashBoard.aspx?Flag=LN">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                            <a href="FrmDashBoard.aspx?Flag=LNM"><%--  Dhanya Shetty to display Maturity Loan Account--%>
                                <div class="panel-footer">
                                    <span class="pull-left">View Details Maturity </span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-red">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="lblShares" runat="server" />
                                        </div>
                                        In Lakh
                                        <div>Share Capital</div>
                                    </div>
                                </div>
                            </div>
                            <a href="FrmDashBoard.aspx?Flag=SH">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                    </div>

                    <%--  Dhanya Shetty to display Maturity Amount--%>

                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-green">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="lblInv" runat="server" />
                                        </div>
                                        In Lakh
                                        <div>Investment</div>
                                    </div>
                                </div>
                            </div>
                            <a href="FrmDashBoard.aspx?Flag=INV">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-green">
                            <div class="panel-heading">
                                <div class="row">
                                    <div class="col-xs-12 text-right">
                                        <div class="huge">
                                            <asp:Label ID="lblRef" runat="server" />
                                        </div>
                                        In Lakh
                                        <div>Reserve Fund </div>
                                    </div>
                                </div>
                            </div>
                            <a href="FrmDashBoard.aspx?Flag=Ref">
                                <div class="panel-footer">
                                    <span class="pull-left">View Details</span>
                                    <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                    <div class="clearfix"></div>
                                </div>
                            </a>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-White">
                            <div class="panel-heading">
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-4">From Date<span class="required"></span></label>
                                        <div class="col-lg-5">
                                            <%--<input type="text" id="TxtFDate" runat="server" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" />--%>
                                            <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" OnTextChanged="TxtFDate_TextChanged" AutoPostBack="true" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-4">To Date <span class="required"></span></label>
                                        <div class="col-md-5">
                                            <%--<input type="text" id="TxtTDate" runat="server" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" />--%>
                                            <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" OnTextChanged="TxtTDate_TextChanged" AutoPostBack="true" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <a href="FrmDashBoard.aspx?Flag=SRO">
                                    <div class="panel-footer">
                                        <span class="pull-left">Recovery View Details</span>
                                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                        <div class="clearfix"></div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-Orange">
                            <div class="panel-footer" style="background-color: Orange">
                                <span class="pull-left">CD Ratio</span>
                                <span class="pull-right">
                                    <asp:Label ID="lblCdRatio" runat="server" />
                                    %</span>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-White">
                            <div class="panel-footer" style="background-color: White">
                                <span class="pull-left">ABR Ratio</span>
                                <span class="pull-right">
                                    <asp:Label ID="lblABRRatio" runat="server" />
                                    %</span>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3 col-md-6">
                        <div class="panel panel-Green">
                            <div class="panel-footer" style="background-color: Green">
                                <span class="pull-left">ALR Ratio</span>
                                <span class="pull-right">
                                    <asp:Label ID="lblALRRatio" runat="server" />
                                    %</span>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <a href="FrmDashBoard.aspx?Flag=ABLR">
                            <div class="panel-footer">
                                <span class="pull-left">ABR ALR View Details</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div id="Div_LoanAccount" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue">
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="portlet-body" style="height: 400px; overflow-y: auto">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Detail Account : </strong></div>
                                                </div>
                                            </div>

                                            <asp:GridView ID="Grd_LoanAccount" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" ShowFooter="true" PagerStyle-CssClass="pgr" Width="200%" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="Grd_LoanAccount_RowDataBound">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Report" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><%--  Dhanya Shetty to display Maturity Loan Account--%>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lnk_StatementView" OnClick="Lnk_StatementView_Click" runat="server" CssClass="glyphicon glyphicon-plus" CommandName="select" CommandArgument='<%# Eval("CustAccNo")+","+Eval("LoanGlCode")+","+Eval("BrCode")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1SrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Brcd" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1Brcd" runat="server" Text='<%# Eval("BrCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="LoanGlCode" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1LoanGlCode" runat="server" Text='<%# Eval("LoanGlCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CustNo" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1CustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CustName" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1CustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CustAccNo" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1CustAccNo" runat="server" Text='<%# Eval("CustAccNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="OpeningDate" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1OpeningDate" runat="server" Text='<%# Eval("OpeningDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SanctionAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1SanAmount" runat="server" Text='<%# Eval("SanctionLimit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="LblSanction" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SanctionDate" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1SanctionDate" runat="server" Text='<%# Eval("SanctionDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="LastIntDate" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1LastIntDate" runat="server" Text='<%# Eval("LastIntDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DueDate" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1DueDate" runat="server" Text='<%# Eval("DueDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Installment" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1Installment" runat="server" Text='<%# Eval("Installment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblInstTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Principle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1Principle" runat="server" Text='<%# Eval("Principle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblPrinciTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PCrDr" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1PCrDr" runat="server" Text='<%# Eval("PCrDr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OverDueBal" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1OverDueBal" runat="server" Text='<%# Eval("OverDueBal") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblOverdueTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Interest" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1Interest" runat="server" Text='<%# Eval("Interest") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblInttTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PInterest" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1PInterest" runat="server" Text='<%# Eval("PInterest") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblPPInttTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="InterestRec" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1InterestRec" runat="server" Text='<%# Eval("InterestRec") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblIntRec" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NoticeChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1NoticeChrg" runat="server" Text='<%# Eval("NoticeChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblNtcChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ServiceChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1ServiceChrg" runat="server" Text='<%# Eval("ServiceChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblServiceChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CourtChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1CourtChrg" runat="server" Text='<%# Eval("CourtChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblCourtChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SurChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1SurChrg" runat="server" Text='<%# Eval("SurChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblSurTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%--BrCode,BrName,LoanGlCode,LoanGlName,CustNo,CustName,CustAccNo,SanctionLimit,OpeningDate,SanctionDate,DueDate,Installment,Principle,PCrDr--%>
                                                    <%--,OverDueBal,Interest,PInterest,InterestRec,NoticeChrg,ServiceChrg,CourtChrg,SurChrg,OtherChrg,BankChrg,InsChrg,LastIntDate,NoOfInst,TotalDueAmt--%>
                                                    <asp:TemplateField HeaderText="OtherChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1OtherChrg" runat="server" Text='<%# Eval("OtherChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblOtherTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BankChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1BankChrg" runat="server" Text='<%# Eval("BankChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblBankTTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="InsChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1InsChrg" runat="server" Text='<%# Eval("InsChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblInsTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NoOfInst" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1NoOfInst" runat="server" Text='<%# Eval("NoOfInst") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblNoInsTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TotalDueAmt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L1TotalDueAmt" runat="server" Text='<%# Eval("TotalDueAmt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblTtotaldue" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <FooterStyle BackColor="#bbffff" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
            <div id="Div8" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
                        </div>
                        <div class="modal-body">
                            <p></p>
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="Div_SROAccount" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue">
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="portlet-body" style="height: 400px; overflow-y: auto">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Detail Account : </strong></div>
                                                </div>
                                            </div>

                                            <asp:GridView ID="Grd_SROAccount" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" ShowFooter="true" PagerStyle-CssClass="pgr" Width="200%" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="Grd_SROAccount_RowDataBound">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Report" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><%--  Dhanya Shetty to display Maturity Loan Account--%>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lnk_SRODTView" OnClick="Lnk_SRODTView_Click" runat="server" CssClass="glyphicon glyphicon-plus" CommandName="select" CommandArgument='<%# Eval("BrCode")+","+Eval("SubGlcode")+","+Eval("BrCode")%>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2SrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="BrCode" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2BrCode" runat="server" Text='<%# Eval("BrCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SubGlcode" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2LoanGlCode" runat="server" Text='<%# Eval("SubGlcode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Glname" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2CustName" runat="server" Text='<%# Eval("Glname") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SanctionAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2SanAmount" runat="server" Text='<%# Eval("Limit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="Lbl_L2Sanction" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Oustanding Bal" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2Principle" runat="server" Text='<%# Eval("Principle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2PrinciTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                   <%-- <asp:TemplateField HeaderText="PriCrDr" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2PCrDr" runat="server" Text='<%# Eval("Pr_Cr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderText="PriCrDr" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2PCrDr" runat="server" Text='<%# Eval("Pr_Cr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2PCrDramt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Interest" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2Interest" runat="server" Text='<%# Eval("Interest") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2InttTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PInterest" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2PInterest" runat="server" Text='<%# Eval("PInterest") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2PPInttTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="InterestRec" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2InterestRec" runat="server" Text='<%# Eval("InterestRec") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2IntRec" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NoticeChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2NoticeChrg" runat="server" Text='<%# Eval("NoticeChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2NtcChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ServiceChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2ServiceChrg" runat="server" Text='<%# Eval("ServiceChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2ServiceChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CourtChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2CourtChrg" runat="server" Text='<%# Eval("CourtChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2CourtChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SurChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2SurChrg" runat="server" Text='<%# Eval("SurChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2SurTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%--BrCode,BrName,LoanGlCode,LoanGlName,CustNo,CustName,CustAccNo,SanctionLimit,OpeningDate,SanctionDate,DueDate,Installment,Principle,PCrDr--%>
                                                    <%--,OverDueBal,Interest,PInterest,InterestRec,NoticeChrg,ServiceChrg,CourtChrg,SurChrg,OtherChrg,BankChrg,InsChrg,LastIntDate,NoOfInst,TotalDueAmt--%>
                                                    <asp:TemplateField HeaderText="OtherChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2OtherChrg" runat="server" Text='<%# Eval("OtherChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2OtherTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BankChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2BankChrg" runat="server" Text='<%# Eval("BankChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2BankTTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="InsChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2InsChrg" runat="server" Text='<%# Eval("InsChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2InsTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TotalRecAmt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L2TotalDueAmt" runat="server" Text='<%# Eval("TotalRec") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L2TtotalRec" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <FooterStyle BackColor="#bbffff" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
            <div id="Div12" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
                        </div>
                        <div class="modal-body">
                            <p></p>
                            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divDetails" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue" id="form_wizard_1">
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="portlet-body" style="height: 400px; overflow-y: auto">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Detail Information : </strong></div>
                                                </div>
                                            </div>

                                            <asp:GridView ID="grdDetails" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdDetails_PageIndexChanging" ShowFooter="true" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="grdDetails_RowDataBound"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Code" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubGl" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl1" runat="server" Text="Total:" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No Of A/C" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblAccNo_tot" runat="server" />
                                                            </div>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblAmount_tot" runat="server" />
                                                            </div>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Report" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><%--  Dhanya Shetty to display Maturity Loan Account--%>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="RptM" runat="server" OnClick="RptMatuLoan_Click" CssClass="glyphicon glyphicon-plus" CommandName='<%# Eval("GLNAME") %>' CommandArgument='<%# Eval("SUBGLCODE") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <FooterStyle BackColor="#bbffff" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
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
    </div>

    <div id="DIV_MAT" runat="server" visible="false">
        <div>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="Div5">
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="portlet-body" style="height: 400px; overflow-y: auto">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Maturity Information : </strong></div>
                                                    </div>
                                                </div>

                                                <asp:GridView ID="GrdMatDetails" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" ShowFooter="true" PagerStyle-CssClass="pgr" Width="120%" OnRowDataBound="GrdMatDetails_RowDataBound" HeaderStyle-HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cust No" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("MCUSTNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="A/C No" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustAccNo" runat="server" Text='<%# Eval("CUSTACCNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deposit Type" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepGl" runat="server" Text='<%# Eval("DEPOSITGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDname" runat="server" Text='<%# Eval("DEPOSITORNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deposit Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrnAmount" runat="server" Text='<%# Eval("PRNAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Opening Date" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOpDate" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Maturity Date" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMDate" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Period" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("PERIOD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate of Interest" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRateInt" runat="server" Text='<%# Eval("RATEOFINT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Interest Amt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIntAmt" runat="server" Text='<%# Eval("INTAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Maturity  AMT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMAMT" runat="server" Text='<%# Eval("MATURITYAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Closing  Bal" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCbal" runat="server" Text='<%# Eval("CLOSINGBALANCE") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <div style="padding: 0 0 5px 0">
                                                                    <asp:Label ID="lblCbal_tot" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <FooterStyle BackColor="#bbffff" />
                                                    <SelectedRowStyle BackColor="#66FF99" />
                                                    <EditRowStyle BackColor="#FFFF99" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                            </div>
                        </div>
                    </div>
                </div>
                <div id="Div6" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
                            </div>
                            <div class="modal-body">
                                <p></p>
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <button class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <asp:Button ID="BtnReport" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="BtnReport_Click" />
            </div>
        </div>
    </div>

    <div id="DIV_TERM" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue" id="Div2">
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="portlet-body" style="height: 400px; overflow-y: auto">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Detail Information : </strong></div>
                                                </div>
                                            </div>

                                            <asp:GridView ID="grdTermDepo" runat="server" ShowFooter="true" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdTermDepo_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="grdTermDepo_RowDataBound"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Code" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubGl" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No. of Maturity A/C" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("MATURITYACCOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Maturity Balance" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("MATURITYAMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl1" runat="server" Text="Total:" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No Of A/C" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblAccNo_tot" runat="server" />
                                                            </div>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lblAmount_tot" runat="server" />
                                                            </div>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <FooterStyle BackColor="#bbffff" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
            <div id="Div3" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
                        </div>
                        <div class="modal-body">
                            <p></p>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divPigmy" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue" id="Div7">
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="portlet-body" style="height: 400px; overflow-y: auto">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Detail Information : </strong></div>
                                                </div>
                                            </div>

                                            <asp:GridView ID="GridPigmy" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GridPigmy_PageIndexChanging" ShowFooter="true" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="GridPigmy_RowDataBound"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbpSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Brcd" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbpbrcd" runat="server" Text='<%# Eval("BRCD")  %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Code" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbpSubGl" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbpDesc" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbp1" runat="server" Text="Total:" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No Of A/C" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbpAccNo" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbpAccNo_tot" runat="server" />
                                                            </div>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Balance" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbpAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbpAmount_tot" runat="server" />
                                                            </div>
                                                        </FooterTemplate>

                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <FooterStyle BackColor="#bbffff" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
            <div id="Div9" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
                        </div>
                        <div class="modal-body">
                            <p></p>
                            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="DivSRO" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue">
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="portlet-body" style="height: 400px; overflow-y: auto">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Detail Account : </strong></div>
                                                </div>
                                            </div>

                                            <asp:GridView ID="GridSRO" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GridSRO_PageIndexChanging" ShowFooter="true" PagerStyle-CssClass="pgr" Width="200%" HeaderStyle-HorizontalAlign="Center" OnRowDataBound="GridSRO_RowDataBound">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Report" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"><%--  Dhanya Shetty to display Maturity Loan Account--%>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lnk_SROView" runat="server" OnClick="Lnk_SROView_Click" CssClass="glyphicon glyphicon-plus" CommandName='<%# Eval("MidName") %>' CommandArgument='<%# Eval("BrCode") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3SrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BrCd" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3LoanGlCode" runat="server" Text='<%# Eval("BrCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Br Name" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3CustName" runat="server" Text='<%# Eval("MidName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SanctionAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3SanAmount" runat="server" Text='<%# Eval("Limit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="Lbl_L3Sanction" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Oustanding Bal" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3Principle" runat="server" Text='<%# Eval("Principle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3PrinciTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PriCrDr" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3PCrDr" runat="server" Text='<%# Eval("Pr_Cr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3PCrDramt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                  <%--  <asp:TemplateField HeaderText="PriCrDr" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3PCrDr" runat="server" Text='<%# Eval("Pr_Cr") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    --%>
                                                    <asp:TemplateField HeaderText="Interest" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3Interest" runat="server" Text='<%# Eval("Interest") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3InttTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PInterest" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3PInterest" runat="server" Text='<%# Eval("PInterest") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3PPInttTt" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="InterestRec" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3InterestRec" runat="server" Text='<%# Eval("InterestRec") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3IntRec" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NoticeChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3NoticeChrg" runat="server" Text='<%# Eval("NoticeChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3NtcChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ServiceChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3ServiceChrg" runat="server" Text='<%# Eval("ServiceChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3ServiceChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CourtChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3CourtChrg" runat="server" Text='<%# Eval("CourtChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3CourtChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SurChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3SurChrg" runat="server" Text='<%# Eval("SurChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3SurTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%--BrCode,BrName,LoanGlCode,LoanGlName,CustNo,CustName,CustAccNo,SanctionLimit,OpeningDate,SanctionDate,DueDate,Installment,Principle,PCrDr--%>
                                                    <%--,OverDueBal,Interest,PInterest,InterestRec,NoticeChrg,ServiceChrg,CourtChrg,SurChrg,OtherChrg,BankChrg,InsChrg,LastIntDate,NoOfInst,TotalDueAmt--%>
                                                    <asp:TemplateField HeaderText="OtherChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3OtherChrg" runat="server" Text='<%# Eval("OtherChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3OtherTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BankChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3BankChrg" runat="server" Text='<%# Eval("BankChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3BankTTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="InsChrg" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3InsChrg" runat="server" Text='<%# Eval("InsChrg") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3InsTChrg" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TotalRecAmt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_L3TotalDueAmt" runat="server" Text='<%# Eval("TotalRec") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="padding: 0 0 5px 0">
                                                                <asp:Label ID="lbl_L3TtotalRec" runat="server" Style="font-weight: bold" />
                                                            </div>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <FooterStyle BackColor="#bbffff" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
            <div id="Div13" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
                        </div>
                        <div class="modal-body">
                            <p></p>
                            <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="gCall">
        <script>
            jQuery(document).ready(function () {

                jQuery(".getBut").click(function () {
                    if (jQuery(".gCall").hasClass("isDown")) {

                        jQuery(".gCall").animate({
                            right: "-467"
                        }, 200, function () {
                            // Animation complete.
                            jQuery(this).removeClass("isDown");
                        });

                    } else {

                        jQuery(".gCall").animate({
                            right: "0"
                        }, 200, function () {
                            // Animation complete.
                            jQuery(this).addClass("isDown");
                        });
                    }
                });
            });

            function openpage() {
                url = '~/FrmQueryDetails.aspx';
                fillFancyBox(url, '385px', 'auto', 0, true);

            }

        </script>
        <a href="javascript:void(0)" class="getBut">
            <img src="global/images/send_query.png"></a>
        <script src='http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js'></script>


        <div id="gcallformCon">
            <form name="gcallform" id="gcallform" method="post" onsubmit="javascript: return validFormg(this);">
                <h3 class="rightLinks">Get a Call Back </h3>
                <p style="font-size: 16px;">
                    Please fill in your details for an instant call back from our executive.<br>
                    <br>
                </p>
                <p style="color: #fe0303; font-size: 12px;">
                    All fields are mandatory *&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="LBShow" runat="server" Text="Query Details" OnClick="LBShow_Click"></asp:LinkButton>
                    <%--<img id="btnRefresh" src="admin/layout/img/photo2.jpg" style="width: 18px; vertical-align: bottom;" alt="Refresh" onclick="javascript:openpage();" />--%>
                </p>

                <div class="container-fluid">
                    <div class="row" style="margin-bottom: 10px; margin-top: 10px;">
                        <div class="col-md-3">Login Id<asp:TextBox ID="TxtLoginId" CssClass="form-control" Enabled="false" placeholder="" runat="server"></asp:TextBox></div>
                        <div class="col-md-9">Name<asp:TextBox ID="TxtName" CssClass="form-control" Enabled="false" placeholder="" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col-md-3">Issue No.<asp:TextBox ID="TxIssueNo" CssClass="form-control" Enabled="false" placeholder="" runat="server"></asp:TextBox></div>
                        <div class="col-md-9">Mobile No.<asp:TextBox ID="TxtMobNo" CssClass="form-control" placeholder="" Enabled="false" runat="server"></asp:TextBox></div>
                    </div>
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col-md-12">
                            Module Related Query <span class="required">*</span><asp:DropDownList ID="ddlModuleRQ" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col-md-12">
                            Query Description<asp:TextBox ID="TxtQDesc" CssClass="form-control"
                                runat="server" TextMode="MultiLine" placeholder="Enter Text..." Rows="4" TabIndex="9"
                                Columns="20" MaxLength="60"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 10px;">
                        <div class="col-md-12">
                            <asp:Button ID="BtnSubmit" class="viewallc" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
                            <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                            <%--&nbsp;&nbsp; <a  onclick="jQuery('#gcallform')[0].reset();jQuery('#gcallform .textbox1').val('');"  class="viewall"   />Reset</a>--%>
                        </div>
                    </div>
                </div>
                <div id="QUERYDETAILS" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Query Details Screen</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="portlet box green" id="Div1">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    Issue Details
                                                </div>
                                            </div>
                                            <div class="portlet-body form">
                                                <div class="form-horizontal">
                                                    <div class="form-wizard">
                                                        <div class="form-body">
                                                            <div class="tab-content">
                                                                <div class="tab-pane active" id="tab1">
                                                                    <div class="row" style="margin-bottom: 10px;">

                                                                        <div class="row" style="margin: 10px;"><strong></strong></div>
                                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                                            <div class="col-lg-12" style="height: 50%">
                                                                                <div class="table-scrollable" style="height: auto; overflow-y: scroll; padding-bottom: 10px;">
                                                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                                                        <thead>
                                                                                            <b>Pending Work</b>
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:GridView ID="grdPending" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                                        EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="SRNO" Visible="true">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="serial" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>


                                                                                                            <asp:TemplateField HeaderText="MODULE RQ" Visible="true">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="MODULERQ" runat="server" Text='<%# Eval("ModueRQ") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="DESCRIPTION">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="QUERYDESC" runat="server" Text='<%# Eval("QueryDes") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="LOG DATE">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="ISSUEDATE" runat="server" Text='<%# Eval("ISSUEDATE") %>'></asp:Label>
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
                                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                                            <div class="col-lg-12" style="height: 50%">
                                                                                <div class="table-scrollable" style="height: auto; overflow-y: scroll; padding-bottom: 10px;">
                                                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                                                        <thead>
                                                                                            <b>Solved Work</b>
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:GridView ID="grdSolved" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                                        EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="SRNO" Visible="true">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="serial" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="SRNO" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="MODULE RQ" Visible="true">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="MODULERQ" runat="server" Text='<%# Eval("MODULERQ") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="DESCRIPTION">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="QUERYDESC" runat="server" Text='<%# Eval("QUERYDESC") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="LOG DATE">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="ISSUEDATE" runat="server" Text='<%# Eval("ISSUEDATE") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="ASSIGN DATE">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="ASSIGNDATE" runat="server" Text='<%# Eval("currenttime") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="SOLVED DATE">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="SOLVEDDATE" runat="server" Text='<%# Eval("SYSTEMDATE") %>'></asp:Label>
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
                                                                </div>

                                                            </div>
                                                            <div class="row">

                                                                <div class="col-md-6">

                                                                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" data-dismiss="modal" />
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
                </div>
            </form>
        </div>
    </div>

</asp:Content>

