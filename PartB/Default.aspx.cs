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
        private const string EDIT_COLUMN = " ";
        private const string DELETE_COLUMN = "  ";
        System.Data.DataTable movieTable = new System.Data.DataTable();
        System.Data.DataTable cineplexTable = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CreateMovieGrid();
                AddMovieRowsToGrid();
                MovieGridView1.DataSource = movieTable;
                MovieGridView1.DataBind();

                CreateCineplexGrid();
                AddCineplexRowsToGrid();
                CineplexGridView.DataSource = cineplexTable;
                CineplexGridView.DataBind();
            }
            catch (Exception ex)
            {
                Label1.Text = ex.StackTrace;
            }
        }
        private void CreateMovieGrid()
        {
            System.Data.DataColumn tColumn = null;

            tColumn = new System.Data.DataColumn("Movie ID", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Title", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Short Description", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Long Description", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Price ($)", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Image", System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn(EDIT_COLUMN, System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn(DELETE_COLUMN, System.Type.GetType("System.String"));
            movieTable.Columns.Add(tColumn);
        }
        private void CreateCineplexGrid()
        {
            System.Data.DataColumn tColumn = null;

            tColumn = new System.Data.DataColumn("Cineplex ID", System.Type.GetType("System.String"));
            cineplexTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Location", System.Type.GetType("System.String"));
            cineplexTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Short Description", System.Type.GetType("System.String"));
            cineplexTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Long Description", System.Type.GetType("System.String"));
            cineplexTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn("Image", System.Type.GetType("System.String"));
            cineplexTable.Columns.Add(tColumn);
            tColumn = new System.Data.DataColumn(EDIT_COLUMN, System.Type.GetType("System.String"));
            cineplexTable.Columns.Add(tColumn);
        }
        private void AddMovieRowsToGrid()
        {
            foreach(Movie movie in MovieModel.Instance.GetMovies())
            {
                if (movie.status == 1)
                {
                    movieTable.Rows.Add(movie.MovieID,
                        movie.Title,
                        movie.ShortDecription,
                        movie.LongDecription,
                        movie.ImageUrl,
                        movie.price.ToString());
                }
            }
        }
        private void AddCineplexRowsToGrid()
        {
            foreach (Cineplex cineplex in CineplexModel.Instance.GetCineplex())
            {
                if (cineplex.status == 1)
                {
                    cineplexTable.Rows.Add(cineplex.CineplexID,
                        cineplex.Location,
                        cineplex.ShortDecription,
                        cineplex.LongDecription,
                        cineplex.ImageUrl);
                }
            }
        }
        protected void MovieGridView_RowDataBound(object sender,
            System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton elb = new LinkButton();
                elb.Text = "Edit";
                elb.PostBackUrl = "MovieEdit.aspx?id=" + e.Row.Cells[0].Text;
                e.Row.Cells[6].Controls.Add(elb);

                LinkButton dlb = new LinkButton();
                dlb.Text = "Delete";
                dlb.PostBackUrl = "MovieDelete.aspx?id=" + e.Row.Cells[0].Text;
                e.Row.Cells[7].Controls.Add(dlb);
            }
        }
        protected void CineplexGridView_RowDataBound(object sender,
            System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton edlb = new LinkButton();
                edlb.Text = "Edit/Delete Cineplex";
                edlb.PostBackUrl =
                    "CineplexEdit.aspx?id=" + e.Row.Cells[0].Text;
                e.Row.Cells[5].Controls.Add(edlb);
            }
        }
    }
}