using PartB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PartB
{
    public partial class Default : System.Web.UI.Page
    {
        System.Data.DataTable mytable = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CreateGrid();
                AddRowsToGrid();
                GridView1.DataSource = mytable;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.StackTrace;
            }
        }
        private void CreateGrid()
        {
            // CREATE A GRID FOR DISPLAYING A LIST OF BOOKS.

            System.Data.DataColumn tColumn = null;
            // TABLE COLUMNS.

            tColumn = new System.Data.DataColumn("Movie ID", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Title", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Short Description", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Long Description", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Price ($)", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn(" ", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("  ", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("   ", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("    ", System.Type.GetType("System.String"));
            mytable.Columns.Add(tColumn);
        }
        private void AddRowsToGrid()
        {
            foreach(Movie movie in MovieModel.Instance.GetMovies())
            {
                if (movie.status == 1)
                {
                    mytable.Rows.Add(movie.MovieID,
                        movie.Title,
                        movie.ShortDecription,
                        movie.LongDecription,
                        movie.price.ToString());
                }
            }
        }
        protected void GridView_RowDataBound(object sender,
            System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton elb = new LinkButton();
                elb.Text = "Edit";
                elb.PostBackUrl = "MovieEdit.aspx?id=" + e.Row.Cells[0].Text;
                e.Row.Cells[5].Controls.Add(elb);

                LinkButton dlb = new LinkButton();
                dlb.ID = e.Row.Cells[0].Text;
                dlb.Text = "Delete";
                dlb.Click += new EventHandler(delete_Click);
                e.Row.Cells[6].Controls.Add(dlb);

                LinkButton addToCinplexlb = new LinkButton();
                addToCinplexlb.ID = e.Row.Cells[0].Text;
                addToCinplexlb.Text = "Add to Cinplex";
                addToCinplexlb.Click += new EventHandler(delete_Click);
                e.Row.Cells[7].Controls.Add(addToCinplexlb);

                LinkButton removeFromCinplexlb = new LinkButton();
                removeFromCinplexlb.ID = e.Row.Cells[0].Text;
                removeFromCinplexlb.Text = "Remove from Cinplex";
                removeFromCinplexlb.Click += new EventHandler(delete_Click);
                e.Row.Cells[6].Controls.Add(removeFromCinplexlb);
            }
        }
        protected void delete_Click(object sender, EventArgs e)
        {
            //MovieModel.Instance.
        }
    }
}