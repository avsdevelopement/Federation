<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmParameterReg.aspx.cs" Inherits="FrmParameterReg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Parameter Register
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">  
                                                                                                                                                            
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">List Field: </label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtlistfield" runat="server" placeHolder="Enter List Field" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                 <label class="control-label col-md-2">List Value: </label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtlistvalue" runat="server" placeHolder="Enter List Value" CssClass="form-control" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12" >
                                               
                                                <asp:Button ID="Btnaddnew" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="Btnaddnew_Click" /> <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" />                                               
                                            </div>                                            
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-scrollable" style="height: 350PX; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdParaReg" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" Width="100%">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="LISTFIELD" HeaderText="List Field" />
                                                                            <asp:BoundField DataField="LISTVALUE" HeaderText="List Value" />
                                                                            <asp:BoundField DataField="BRCD" HeaderText="Brcd" />
                                                                            <asp:BoundField DataField="STAGE" HeaderText="stage" />                                                                       
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
    </div>

</asp:Content>

