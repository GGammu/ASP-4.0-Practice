<%@ Page Title="" Language="C#" MasterPageFile="~/Bottom.master" AutoEventWireup="true" CodeFile="Schedule.aspx.cs" Inherits="Schedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphContent" Runat="Server">
    <table class="tbl01" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:5px;"></td>
            <td class="td01"></td>
        </tr>
        <tr>
            <td></td>
            <td class="td03">
                <img src="images/title_icon.gif" />
                &nbsp;&nbsp;&nbsp;
                웹 일정관리
            </td>
        </tr>
        <tr>
            <td></td>
            <td class="td01"></td>
        </tr>
        <tr>
            <td></td>
            <td style="height:15"></td>
        </tr>
    </table>
    <table class="tbl01" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:5px;"></td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Calendar ID="sCalendar" runat="server" BackColor="White" BorderColor="White" Font-Names="Verdana" Font-Size="9pt"
                                ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="250px" BorderWidth="1px" OnSelectionChanged="sCalendar_SelectionChanged">
                                <SelectedDayStyle BackColor="#3333339" ForeColor="White" />
                                <TodayDayStyle BackColor="#CCCCCC" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <NextPrevStyle Font-Bold="true" Font-Size="8pt" ForeColor="#3333333" VerticalAlign="Bottom" />
                                <DayHeaderStyle Font-Bold="true" Font-Size="8pt" />
                                <TitleStyle BackColor="White" Font-Bold="true" Font-Size="12pt" ForeColor="#333333" BorderStyle="None" Height="50px" />
                            </asp:Calendar>
                        </td>
                        <td style="width:15px;"></td>
                        <td style="width:290px; vertical-align:top;">
                            <asp:PlaceHolder ID="phSchedule" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="height:15;"></td>
        </tr>
    </table>
</asp:Content>

