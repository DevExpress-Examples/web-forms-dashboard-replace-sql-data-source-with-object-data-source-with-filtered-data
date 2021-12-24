using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Native.Sql;
using DevExpress.DataAccess.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ASPxDashboard {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            ASPxDashboard1.SetDashboardStorage(new CustomDashboardFileStorage(Server.MapPath("~/App_Data/Dashboards/")));
        }

        protected void ASPxDashboard1_DataLoading(object sender, DataLoadingWebEventArgs e) {
            if (e.DataId.StartsWith("ods|")) {
                string[] names = e.DataSourceName.Split("|".ToCharArray());
                List<DashboardSqlDataSource> dataSources = (List<DashboardSqlDataSource>)Session["ds" + e.DashboardId];
                DashboardSqlDataSource dataSource = dataSources.First(ds => ds.ComponentName == names[1]);
                SqlQuery query = dataSource.Queries.First(q => q.Name == names[2]);

                XElement dsXML = dataSource.SaveToXml();
                DevExpress.DataAccess.Sql.SqlDataSource sqlDS = new DevExpress.DataAccess.Sql.SqlDataSource();
                sqlDS.LoadFromXml(dsXML);
                sqlDS.ConnectionName = dataSource.ConnectionName;
                sqlDS.Fill(query.Name);

                ResultSet rSet = ((IListSource)sqlDS).GetList() as ResultSet;
                ResultTable rTable = rSet.Tables.First(t => t.TableName == query.Name);

                if (query.Name == "Invoices") {
                    var dt = ConvertResultTableToDataTable(rTable);
                    for (int i = dt.Rows.Count - 1; i >= 0; i--) {
                        if (((DateTime)dt.Rows[i]["OrderDate"]).Year < 2016)
                            dt.Rows.RemoveAt(i);
                    }
                    e.Data = dt;
                }
                else
                    e.Data = rTable;
            }
        }

        private DataTable ConvertResultTableToDataTable(ResultTable resultTable) {
            DataTable dataTable = new DataTable(resultTable.TableName);
            
            resultTable.Columns.ForEach(col => dataTable.Columns.Add(new DataColumn(col.Name, col.PropertyType)));
            
            foreach (ResultRow resultRow in resultTable) {
                DataRow newRow = dataTable.NewRow();
                foreach (var column in resultTable.Columns) {
                    newRow[column.Name] = column.GetValue(resultRow);
                }
                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }
    }

    public class CustomDashboardFileStorage : DashboardFileStorage {
        public CustomDashboardFileStorage(string workingDirectory) : base(workingDirectory) { }
        protected override XDocument LoadDashboard(string dashboardID) {
            Dashboard dashboard = new Dashboard();
            dashboard.LoadFromXDocument(base.LoadDashboard(dashboardID));
            List<DashboardSqlDataSource> dataSources = dashboard.DataSources.OfType<DashboardSqlDataSource>().ToList();
            HttpContext.Current.Session["ds" + dashboardID] = dataSources;
           
            foreach (var query in dataSources.SelectMany(ds => ds.Queries.Select(q => new { DataSource = ds, Query = q }))) {
                var odsId = "ods|" + query.DataSource.ComponentName + "|" + query.Query.Name;
                var ods = new DashboardObjectDataSource(odsId);
                
                ods.DataId = odsId;
                dashboard.DataSources.Add(ods);

                foreach (var item in dashboard.Items.OfType<DataDashboardItem>().Where(i => Object.ReferenceEquals(i.DataSource, query.DataSource) && i.DataMember == query.Query.Name)) {
                    item.DataMember = "";
                    item.DataSource = ods;
                }
                foreach (var parameter in dashboard.Parameters.Select(p => p.LookUpSettings).OfType<DynamicListLookUpSettings>().Where(p => Object.ReferenceEquals( p.DataSource, query.DataSource) && p.DataMember == query.Query.Name)) {
                    parameter.DataMember = "";
                    parameter.DataSource = ods;
                }
            }

            return dashboard.SaveToXDocument();
        }
    }
}