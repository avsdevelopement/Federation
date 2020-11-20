<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5073.aspx.cs" Inherits="FrmAVS5073" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-3 col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-12 text-right">
                            <div class="huge">
                                <asp:Label ID="lblTT" runat="server" />
                            </div>
                                        <div>Today's Tranaction</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=TT">
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
                                <asp:Label ID="lblSS" runat="server" />
                            </div>
                                        <div>Sent SMS</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=SS">
                    <div class="panel-footer">
                        <span class="pull-left">View Details </span>
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
                                <asp:Label ID="lblMN" runat="server" />
                            </div>
                                        <div>Mobile No Not Exist</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=MN">
                    <div class="panel-footer">
                        <span class="pull-left">View Details</span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>
            </div>
        </div>
        <div class="col-lg-3 col-md-6">
                    </div>
          <div class="col-lg-3 col-md-6">
            <div class="panel panel-maroon">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-12 text-right">
                            <div class="huge">
                                <asp:Label ID="Label1" runat="server" />
                            </div>
                                        <div>WelCome</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=WelCome">
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
                                <asp:Label ID="Label2" runat="server" />
                            </div>
                                        <div>Payment</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=Payment">
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
                                <asp:Label ID="Label3" runat="server" />
                            </div>
                                        <div>Receipt</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=Receipt">
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
                                <asp:Label ID="Label4" runat="server" />
                            </div>
                                        <div>BirthDaySMS</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=BirthDaySMS">
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
                                <asp:Label ID="Label5" runat="server" />
                            </div>
                                        <div>LoanOverDue</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=LoanOverDue">
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
                                <asp:Label ID="Label6" runat="server" />
                            </div>
                                        <div>BeforeMaturity</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=BeforeMaturity">
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
                                <asp:Label ID="Label7" runat="server" />
                            </div>
                                        <div>OnMaturity</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=OnMaturity">
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
                                <asp:Label ID="Label8" runat="server" />
                            </div>
                                        <div>LoanSanc</div>
                        </div>
                    </div>
                </div>
                <a href="FrmAVS5073.aspx?Flag=LoanSanc">
                    <div class="panel-footer">
                        <span class="pull-left">View Details </span>
                        <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                        <div class="clearfix"></div>
                    </div>
                </a>

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
                                            <div class="portlet-body" style="height: 400px; overflow-y: auto; overflow-x: auto">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Detail Information : </strong></div>
                                                    </div>
                                                </div>

                                                <asp:GridView ID="grdDetails" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdDetails_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="grdDetails_RowDataBound">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SMS Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSmsDt" runat="server" Text='<%# Eval("sms_date") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Custno">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcustno" runat="server" Text='<%# Eval("custno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcustname" runat="server" Text='<%# Eval("custname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Brcd">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbrcd" runat="server" Text='<%# Eval("brcd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        
                                                        </asp:TemplateField>

                                                        
                                                        <asp:TemplateField HeaderText="SMS Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSMSDesc" runat="server" Text='<%# Eval("sms_description") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        
                                                        <asp:TemplateField HeaderText="Response">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblResponse" runat="server" Text='<%# Eval("response") %>'></asp:Label>
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

</asp:Content>

