<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="ASPxDashboard.Default" %>

<%@ Register assembly="DevExpress.Dashboard.v17.1.Web, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.DashboardWeb" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" OnDataLoading="ASPxDashboard1_DataLoading" WorkingMode="ViewerOnly" >
        </dx:ASPxDashboard>

    </div>
    </form>
</body>
</html>