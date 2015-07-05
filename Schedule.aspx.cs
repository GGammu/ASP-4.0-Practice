using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Schedule : System.Web.UI.Page
{
    DateTime selectedDate;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void sCalendar_SelectionChanged(object sender, EventArgs e)
    {

    }

    protected void Showtitle(DateTime dtSelected)
    {
        sCalendar.SelectedDate = dtSelected;
        selectedDate = dtSelected;
        string titleString = "<font size='2px'>";
        if (dtSelected.Date == DateTime.Today)
        {
            titleString += "오늘의 일정</font>";
        }
        else
        {
            titleString += string.Format("{0:MM월 dd일의 일정}", dtSelected.Date);
            titleString += "</font>";
        }
        titleString += "<hr />";

        Label lblTitle = new Label();
        lblTitle.Text = titleString;
        phSchedule.Controls.Add(lblTitle);
    }

    protected void ShowTodoList()
    {
        string selectString = "SELECT * FROM schedule";
        selectString += "WHERE u_name=@user AND date=@date ORDER BY time";

        DBConn conn = new DBConn();
        SqlCommand cmd = new SqlCommand(selectString, conn.GetConn());
        cmd.Parameters.AddWithValue("@user", Membership.GetUser().UserName);
        cmd.Parameters.AddWithValue("@date", string.Format("{0:yyyy-MM-dd}", selectedDate));
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            ShowTodoList(dr["serial_no"].ToString(), dr["time"].ToString(),
                dr["todo"].ToString(), dr["has_done"].ToString());
        }

        dr.Close();
        conn.Close();
    }

    private void ShowTodoList(string sn, string time, string todo, string hasDone)
    {
        string todoString = "<font size='2px'>";

        if (hasDone == "Y")
            todoString += "<strike>";

        todoString += time + " : " + todo;

        if (hasDone == "Y")
            todoString += "</strike>";

        todoString += "</font>";

        Label lblSpace = new Label();
        lblSpace.Text = "&nbsp;";

        Label lblHr = new Label();
        lblHr.Text = "<hr />";

        ImageButton btnDone = new ImageButton();
        btnDone.ImageUrl = "~/images/done.gif";
        btnDone.Width = 15;
        btnDone.Height = 15;
        btnDone.CommandName = "Done";
        btnDone.CommandArgument = sn;
        btnDone.Command += new CommandEventHandler(btnSubmit_Command);

    }

    private void btnSubmit_Command(object sender, CommandEventArgs e)
    {
        throw new NotImplementedException();
    }
}