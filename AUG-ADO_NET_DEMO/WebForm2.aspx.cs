using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AUG_ADO_NET_DEMO
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        static string CS = ConfigurationManager.ConnectionStrings["csTOtest"].ConnectionString;
        SqlConnection con = new SqlConnection(CS);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //BindData();
                DataTable dt = new DataTable();
                SqlDataAdapter d = new SqlDataAdapter("Select * from Student", con);
                d.Fill(dt);
                DropDownList2.DataSource = dt;
                DropDownList2.DataTextField = "stream";
                DropDownList2.DataValueField = "stream";
                DropDownList2.DataBind();
            }

        }
        protected void GetDataFromDB() {
            string selectQuery = "Select * from Student";
            SqlDataAdapter da = new SqlDataAdapter(selectQuery,con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Student");
            //Set the ID col as primary key
            ds.Tables["Student"].PrimaryKey = new DataColumn[] { ds.Tables["Student"].Columns["studentId"]};
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);

            GridView1.DataSource = ds;
            GridView1.DataBind();

            Label1.Text = "Data loaded from the database";
        }
        protected void GetDataFromCache() {
            if (Cache["DATASET"] != null) {
                GridView1.DataSource = (DataSet)Cache["DATASET"];
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.EditIndex = -1;
            GridView1.SelectedIndex = -1;
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {
            GetDataFromCache();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetDataFromCache();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetDataFromCache();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            #region
            //TextBox Id = GridView1.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox;
            //string id = Id.Text;

            //int id = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            //TextBox Name = GridView1.Rows[e.RowIndex].Cells[1].Controls[0] as TextBox;
            //string name = Name.Text;


            //TextBox Stream = GridView1.Rows[e.RowIndex].FindControl("streamTB") as TextBox;
            //string stream = Stream.Text;

            //TextBox Marks = GridView1.Rows[e.RowIndex].FindControl("marksTB") as TextBox;
            //string marks = Marks.Text;

            //SqlCommand cmd = new SqlCommand("update Student set studentName='"+name+"'," +
            //    "stream='"+stream+"',marks='"+marks+"' where studentId='"+id+"'",con);

            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            //GridView1.EditIndex = -1;
            //BindData();
            #endregion

            DataSet dataSet = (DataSet)Cache["DATASET"];
            //find datarow to edit using the primarykey
            DataRow dataRow = dataSet.Tables["Student"].Rows.Find(e.Keys["studentId"]);
            //Update the datarow values
            dataRow["studentName"] = e.NewValues["studentName"];
            //dataRow["stream"] = e.NewValues["stream"];
            dataRow["stream"] = ((DropDownList)(GridView1.Rows[e.RowIndex].FindControl("DropDownList1"))).SelectedValue.ToString();
            dataRow["marks"] = e.NewValues["marks"];
            //Overwrite the dataset in the cache
            Cache.Insert("DATASET", dataSet, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            GridView1.EditIndex = -1;
            //Reload the data from the cache
            GetDataFromCache();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            #region
            //int id = Int32.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            //SqlCommand cmd = new SqlCommand("delete Student where studentId='" + id + "'", con);

            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
            //GridView1.EditIndex = -1;
            //BindData();
            #endregion

            DataSet dataSet = (DataSet)Cache["DATASET"];
            dataSet.Tables["Student"].Rows.Find(e.Keys["studentId"]).Delete();
            Cache.Insert("DATASET", dataSet, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            GetDataFromCache();
        }

        protected void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            if (Cache["DATASET"] != null) {
                string selectQuery = "Select * from Student";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectQuery, con);

                //Update command 
                string strUpdateCommand = "update Student set studentName=@studentName,stream=@stream,marks=@marks" +
                    " where studentId=@studentId";
                SqlCommand updateCommand = new SqlCommand(strUpdateCommand, con);
                //Specify the parameters
                updateCommand.Parameters.Add("@studentName", SqlDbType.NVarChar, 50, "studentName");
                updateCommand.Parameters.Add("@stream", SqlDbType.NVarChar, 50, "stream");
                updateCommand.Parameters.Add("@marks", SqlDbType.NVarChar, 50, "marks");
                updateCommand.Parameters.Add("@studentId", SqlDbType.Int,0, "studentId");
                //attach the updateCOmmand with the dataAdapter 
                dataAdapter.UpdateCommand = updateCommand;

                string strDeleteCommand = "Delete from Student where studentId=@studentId";
                SqlCommand deleteCommand = new SqlCommand(strDeleteCommand, con);
                deleteCommand.Parameters.Add("@studentId", SqlDbType.Int, 0, "studentId");
                dataAdapter.DeleteCommand = deleteCommand;

                //Update the underlying database
                dataAdapter.Update((DataSet)Cache["DATASET"], "Student");
                Label1.Text = "Database Table Updated";
            }
        }

        protected void btnGetDataFromDB_Click(object sender, EventArgs e)
        {
            GetDataFromDB();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DataSet dataSet = (DataSet)Cache["DATASET"];
                    DropDownList DropDownList1 = (e.Row.FindControl("DropDownList1")) as DropDownList;
                    DropDownList1.DataSource = dataSet.Tables["Student"].Rows.Cast<DataRow>().Select(r => r.Field<string>("stream")).Distinct();
                    //DropDownList1.DataSource = dataSet.Tables["Student"];
                    //DropDownList1.DataTextField = "stream";
                    //DropDownList1.DataValueField = "stream";
                    DropDownList1.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Cache["DATASET"];
            DataRow row = ds.Tables["Student"].NewRow();
            row["studentId"] = 101;
            //ds.Tables["Student"].Rows.Add(row);

            Label2.Text = "";
            foreach (DataRow dr in ds.Tables["Student"].Rows) {
                if (dr.RowState == DataRowState.Deleted)
                {
                    Label2.Text += dr["studentId", DataRowVersion.Original].ToString() + " " + dr.RowState.ToString() + "<br />";
                }
                else {
                    Label2.Text += dr["studentId", DataRowVersion.Current].ToString() + " " + dr.RowState.ToString() + "<br />";
                }
            }

            Response.Write(row.RowState.ToString());
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //DataSet ds = (DataSet)Cache["DATASET"];
            //if (ds.HasChanges())
            //{
            //    ds.RejectChanges();
            //    Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            //    GetDataFromCache();
            //    Label3.ForeColor = System.Drawing.Color.Green;
            //    Label3.Text = "Changes UNDONE";
            //}
            //else {
            //    Label3.ForeColor = System.Drawing.Color.Red;
            //    Label3.Text = "No Changes to UNDO";
            //}

            DataSet ds = (DataSet)Cache["DATASET"];
            ds.AcceptChanges();
            Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            GetDataFromCache();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label4.Text = DropDownList2.SelectedValue.ToString();
        }
    }
}