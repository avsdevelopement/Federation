<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVerifySign.aspx.cs" Inherits="FrmVerifySign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>PHOTO SIGNATURE VIEW</title>
     <style type="text/css">
        .zoom_img img {
            margin: 50px;
            height: 100px;
            width: 100px;
            -moz-transition: -moz-transform 0.5s ease-in;
            -webkit-transition: -webkit-transform 0.5s ease-in;
            -o-transition: -o-transform 0.5s ease-in;
        }

            .zoom_img img:hover {
                -moz-transform: scale(2);
                -webkit-transform: scale(2);
                -o-transform: scale(2);
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="alertModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <div class="modal-body">
                            <p></p>
                            <%--<asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>--%>
                            <div class="zoom_img">
                                <asp:Image ID="imgPopup" runat="server" Width="150px" Height="150px" />
                                <asp:Image ID="imgSignPopup" runat="server" Width="150px" Height="150px" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
