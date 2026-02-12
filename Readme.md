<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128580348/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T556759)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# Dashboard for Web Forms - How to replace DashboardSqlDataSource with DashboardObjectDataSource with filtered data

<a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardSqlDataSource.class">DashboardSqlDataSource</a> allows requesting data in two ways:<br>1. Direct database connection: <a href="https://documentation.devexpress.com/Dashboard/17083/Main-Features/Connecting-to-a-Data-Source/Data-Processing-Modes">Server Mode</a>.<br>2. In-memory data processing: <a href="https://documentation.devexpress.com/Dashboard/17083/Main-Features/Connecting-to-a-Data-Source/Data-Processing-Modes">Client Mode</a>.<br><br>The first approach works if you configure the data source using the <a href="https://documentation.devexpress.com/Dashboard/16152/Creating-Dashboards/Creating-Dashboards-in-the-WinForms-Designer/Providing-Data/SQL-Data-Source/Working-with-Data/Using-the-Query-Builder">Query Builder</a>. In this case, it is possible to add a custom filter expression to filter requested data using the <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardWeb.ASPxDashboard.CustomFilterExpression.event">CustomFilterExpression</a> event.<br>If you load data using a custom SQL query or a stored procedure, only <a href="https://documentation.devexpress.com/Dashboard/17083/Main-Features/Connecting-to-a-Data-Source/Data-Processing-Modes">Client Data Processing Mode</a> is supported. This example demonstrates how to filter data requested from a database manually and pass it to a dashboard as <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardObjectDataSource.members">DashboardObjectDataSource</a>.<br>To accomplish this task it is necessary to define a custom DashboardStorage class and implement the LoadDashboard method to update loaded dashboards and replace the target <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardSqlDataSource.members">DashboardSqlDataSource</a> queries with new <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardObjectDataSource.members">DashboardObjectDataSource</a><u>s</u>.<br>Then, handle the <a href="https://documentation.devexpress.com/Dashboard/DevExpress.DashboardWeb.ASPxDashboard.DataLoading.event">DataLoading</a> event to provide data to the new object data sources. To learn how to request data using DashboardSqlDataSource, refer to the <a href="https://www.devexpress.com/Support/Center/p/T347509">T347509: How to get data from the Dashboard DataSource and convert it to DataTable</a> thread. 

<!-- default file list -->
## Files to Review

* [Default.aspx](./CS/ASPxDashboard/Default.aspx) (VB: [Default.aspx](./VB/ASPxDashboard/Default.aspx))
* [Default.aspx.cs](./CS/ASPxDashboard/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/ASPxDashboard/Default.aspx.vb))
<!-- default file list end -->

## Documentation

- [Data Processing Modes](https://docs.devexpress.com/Dashboard/17083/basic-concepts-and-terminology/data-processing-modes)
- [DashboardSqlDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardSqlDataSource)
- [DashboardObjectDataSource](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.DashboardObjectDataSource)

## More Examples

- [How to Register Data Sources for ASP.NET Web Forms Dashboard Control](https://github.com/DevExpress-Examples/asp-net-web-forms-dashboard-register-data-sources)
- [Dashboard for Web Forms - How to connect the Web Dashboard to an SQL database](https://github.com/DevExpress-Examples/aspxdashboard-how-to-connect-the-web-dashboard-to-an-sql-database-t409084)
<!-- feedback -->
## Does This Example Address Your Development Requirements/Objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=web-forms-dashboard-replace-sql-data-source-with-object-data-source-with-filtered-data&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=web-forms-dashboard-replace-sql-data-source-with-object-data-source-with-filtered-data&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
