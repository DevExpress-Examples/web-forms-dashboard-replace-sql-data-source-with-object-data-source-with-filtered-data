<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/ASPxDashboard/Default.aspx) (VB: [Default.aspx](./VB/ASPxDashboard/Default.aspx))
* [Default.aspx.cs](./CS/ASPxDashboard/Default.aspx.cs) (VB: [Default.aspx](./VB/ASPxDashboard/Default.aspx))
* [Global.asax](./CS/ASPxDashboard/Global.asax) (VB: [Global.asax](./VB/ASPxDashboard/Global.asax))
* [Global.asax.cs](./CS/ASPxDashboard/Global.asax.cs) (VB: [Global.asax](./VB/ASPxDashboard/Global.asax))
<!-- default file list end -->
# How to replace DashboardSqlDataSource with DashboardObjectDataSource with filtered data


<a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardSqlDataSource.class">DashboardSqlDataSource</a> allows requesting data in two ways:<br>1. Direct database connection: <a href="https://documentation.devexpress.com/Dashboard/17083/Main-Features/Connecting-to-a-Data-Source/Data-Processing-Modes">Server Mode</a>.<br>2. In-memory data processing: <a href="https://documentation.devexpress.com/Dashboard/17083/Main-Features/Connecting-to-a-Data-Source/Data-Processing-Modes">Client Mode</a>.<br><br>The first approach works if you configure the data source using the <a href="https://documentation.devexpress.com/Dashboard/16152/Creating-Dashboards/Creating-Dashboards-in-the-WinForms-Designer/Providing-Data/SQL-Data-Source/Working-with-Data/Using-the-Query-Builder">Query Builder</a>. In this case, it is possible to add a custom filter expression to filter requested data using the <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardWeb.ASPxDashboard.CustomFilterExpression.event">CustomFilterExpression</a> event.<br>If you load data using a custom SQL query or a stored procedure, only <a href="https://documentation.devexpress.com/Dashboard/17083/Main-Features/Connecting-to-a-Data-Source/Data-Processing-Modes">Client Data Processing Mode</a> is supported. This example demonstrates how to filter data requested from a database manually and pass it to a dashboard as <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardObjectDataSource.members">DashboardObjectDataSource</a>.<br>To accomplish this task it is necessary to define a custom DashboardStorage class and implement the LoadDashboard method to update loaded dashboards and replace the target <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardSqlDataSource.members">DashboardSqlDataSource</a> queries with new <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardObjectDataSource.members">DashboardObjectDataSource</a><u>s</u>.<br>Then, handle the <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardWeb.ASPxDashboard.DataLoading.event">DataLoading</a> event to provide data to the new object data sources. To learn how to request data using DashboardSqlDataSource, refer to the <a href="https://www.devexpress.com/Support/Center/p/T347509">T347509: How to get data from the Dashboard DataSource and convert it to DataTable</a> thread. 

<br/>


