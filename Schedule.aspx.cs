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
            ShowTodo(dr["serial_no"].ToString(), dr["time"].ToString(),
                dr["todo"].ToString(), dr["has_done"].ToString());
        }

        dr.Close();
        conn.Close();
    }

    private void ShowTodo(string sn, string time, string todo, string hasDone)
    {
        string todoString = "<font size='2px'>";

        if (hasDone == "Y")
            todoString += "<strike>";

        todoString += time + " : " + todo;

        if (hasDone == "Y")
            todoString += "</strike>";

        todoString += "</font>";

        Label lblTodo = new Label();
        lblTodo.Text = todoString;

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
        lblTodo.Controls.Add(btnDone);

        ImageButton btnDelete = new ImageButton();
        btnDelete.ImageUrl = "~/images/delete.gif";
        btnDelete.Width = 15;
        btnDelete.Height = 15;
        btnDelete.CommandName = "Delete";
        btnDelete.CommandArgument = sn;
        btnDelete.Command += new CommandEventHandler(btnSubmit_Command);
        lblTodo.Controls.Add(btnDelete);

        Table table = new Table();
        TableRow tr = new TableRow();
        for (int i = 0; i < 2; i++)
        {
            TableCell td = new TableCell();
            td.VerticalAlign = VerticalAlign.Top;
            if (i == 0)
            {
                td.Width = 40;
            }

            tr.Cells.Add(td);
        }

        table.Rows.Add(tr);
        table.Rows[0].Cells[0].Controls.Add(btnDone);
        table.Rows[0].Cells[0].Controls.Add(lblSpace);
        table.Rows[0].Cells[0].Controls.Add(btnDelete);
        table.Rows[0].Cells[1].Controls.Add(lblTodo);

        phSchedule.Controls.Add(table);
        phSchedule.Controls.Add(lblHr);
    }

    protected void ShowNewTodoInsertForm()
    {
        Table table = new Table();
        table.ID = "TABLE";

        for (int i = 0; i < 3; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 0; j < 2; j++)
            {
                TableCell td = new TableCell();
                if (j % 2 == 0)
                {
                    td.HorizontalAlign = HorizontalAlign.Right;
                }
                else
                {
                    td.HorizontalAlign = HorizontalAlign.Left;
                }

                tr.Cells.Add(td);
            }
            table.Rows.Add(tr);
        }

        Label lblTodo = new Label();
        lblTodo.Text = "<font size='2px'>새할일 : </font>";
        table.Rows[0].Cells[0].Controls.Add(lblTodo);

        TextBox txtTodo = new TextBox();
        txtTodo.Width = 180;
        table.Rows[0].Cells[1].Controls.Add(txtTodo);

        Label lblTime = new Label();
        lblTime.Text = "<font size='2px'>시작 시각 : </font>";
        table.Rows[1].Cells[0].Controls.Add(lblTime);

        DropDownList ddlHour = new DropDownList();
        for (int i = 0; i < 24; i++)
        {
            ddlHour.Items.Add(string.Format("{0:D2} 시", i));
        }

        ddlHour.Items.Add("하루 중");
        table.Rows[1].Cells[1].Controls.Add(ddlHour);

        DropDownList ddlMinute = new DropDownList();
        for (int i = 0; i < 6; i++)
        {
            ddlMinute.Items.Add(string.Format("{0:2D} 분", i * 10));
        }
        table.Rows[1].Cells[1].Controls.Add(ddlMinute);

        Button btnSubmit = new Button();
        btnSubmit.Text = "등록";
        btnSubmit.CommandName = "Submit";
        table.Rows[2].Cells[1].Controls.Add(btnSubmit);
        table.Rows[2].Cells[1].HorizontalAlign = HorizontalAlign.Right;

        phSchedule.Controls.Add(table);
    }

    private void btnSubmit_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Submit")
        {
            //if (txtTodo.Text.Trim() == "") return;

            string insertString = "INSERT INTO schedule (u_name, date, time, todo, has_done) ";
            insertString = "VALUES (@user, @date, @time, @todo, 'N')";

            Table table = (Table)this.phSchedule.FindControl("TABLE");
            DropDownList ddlGetTime = (DropDownList)table.Rows[1].Cells[1].Controls[0];
            string timeString = ddlGetTime.SelectedValue.ToString();
            ddlGetTime.SelectedIndex = 0;

        }
    }
}