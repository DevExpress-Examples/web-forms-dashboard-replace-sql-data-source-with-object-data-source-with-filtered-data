Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.Native.Sql
Imports DevExpress.DataAccess.Sql
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Xml.Linq

Namespace ASPxDashboard
    Partial Public Class [Default]
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            ASPxDashboard1.SetDashboardStorage(New CustomDashboardFileStorage(Server.MapPath("~/App_Data/Dashboards/")))
        End Sub

        Protected Sub ASPxDashboard1_DataLoading(ByVal sender As Object, ByVal e As DataLoadingWebEventArgs)
            If e.DataSourceName.StartsWith("ods|") Then
                Dim names() As String = e.DataSourceName.Split("|".ToCharArray())
                Dim dataSources As List(Of DashboardSqlDataSource) = DirectCast(Session("ds" & e.DashboardId), List(Of DashboardSqlDataSource))
                Dim dataSource As DashboardSqlDataSource = dataSources.First(Function(ds) ds.ComponentName = names(1))
                Dim query As SqlQuery = dataSource.Queries.First(Function(q) q.Name = names(2))

                Dim dsXML As XElement = dataSource.SaveToXml()
                Dim sqlDS As New DevExpress.DataAccess.Sql.SqlDataSource()
                sqlDS.LoadFromXml(dsXML)
                sqlDS.ConnectionName = dataSource.ConnectionName
                sqlDS.Fill(query.Name)

                Dim rSet As ResultSet = TryCast(DirectCast(sqlDS, IListSource).GetList(), ResultSet)
                Dim rTable As ResultTable = rSet.Tables.First(Function(t) t.TableName = query.Name)

                If query.Name = "Invoices" Then
                    Dim dt = ConvertResultTableToDataTable(rTable)
                    For i As Integer = dt.Rows.Count - 1 To 0 Step -1
                        If CDate(dt.Rows(i)("OrderDate")).Year < 2016 Then
                            dt.Rows.RemoveAt(i)
                        End If
                    Next i
                    e.Data = dt
                Else
                    e.Data = rTable
                End If
            End If
        End Sub
        Private Function ConvertResultTableToDataTable(ByVal resultTable As ResultTable) As DataTable
            Dim dataTable As New DataTable(resultTable.TableName)
            resultTable.Columns.ForEach(Sub(col) dataTable.Columns.Add(New DataColumn(col.Name, col.PropertyType)))
            For Each resultRow As ResultRow In resultTable
                Dim newRow As DataRow = dataTable.NewRow()
                For Each column In resultTable.Columns
                    newRow(column.Name) = column.GetValue(resultRow)
                Next column
                dataTable.Rows.Add(newRow)
            Next resultRow
            Return dataTable
        End Function
    End Class

    Public Class CustomDashboardFileStorage
        Inherits DashboardFileStorage

        Public Sub New(ByVal workingDirectory As String)
            MyBase.New(workingDirectory)
        End Sub
        Protected Overrides Function LoadDashboard(ByVal dashboardID As String) As XDocument
            Dim dashboard As New Dashboard()
            dashboard.LoadFromXDocument(MyBase.LoadDashboard(dashboardID))
            Dim dataSources As List(Of DashboardSqlDataSource) = dashboard.DataSources.OfType(Of DashboardSqlDataSource)().ToList()
            HttpContext.Current.Session("ds" & dashboardID) = dataSources
            For Each dsQuery In dataSources.SelectMany(Function(ds) ds.Queries.Select(Function(q) New With {
                Key .DataSource = ds,
                Key .Query = q
            }))
                Dim ods = New DashboardObjectDataSource("ods|" & dsQuery.DataSource.ComponentName & "|" & dsQuery.Query.Name)
                dashboard.DataSources.Add(ods)
                For Each item In dashboard.Items.OfType(Of DataDashboardItem)().Where(Function(i) Object.ReferenceEquals(i.DataSource, dsQuery.DataSource) AndAlso i.DataMember = dsQuery.Query.Name)
                    item.DataMember = ""
                    item.DataSource = ods
                Next item
                For Each parameter In dashboard.Parameters.Select(Function(p) p.LookUpSettings).OfType(Of DynamicListLookUpSettings)().Where(Function(p) Object.ReferenceEquals(p.DataSource, dsQuery.DataSource) AndAlso p.DataMember = dsQuery.Query.Name)
                    parameter.DataMember = ""
                    parameter.DataSource = ods
                Next parameter
            Next dsQuery
            Return dashboard.SaveToXDocument()
        End Function
    End Class
End Namespace