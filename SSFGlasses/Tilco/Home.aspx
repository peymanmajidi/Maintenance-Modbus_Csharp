<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 151px;
            height: 143px;
        }
        .style4
        {
            text-align: center;
            width: 228px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>
    لیست کارمندان حاضر در کارخانه:
    </h3>
    <asp:Button ID="Button1" runat="server" 
        onclick="Button1_Click" Text="به روز رسانی" Visible="False" />
    <asp:Timer ID="timerTriger" runat="server" Interval="1000" 
        ontick="timerTriger_Tick" EnableViewState="False">
    </asp:Timer>
  

    <br /> 
    
     
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" 
        RenderMode="Inline">
        <Triggers>
         <asp:AsyncPostBackTrigger ControlID="timerTriger" EventName="Tick" />
        </Triggers>
 
        <ContentTemplate>
            
            
           <div id="divnew" runat="server" visible=false>
    <asp:HyperLink ID="lnkProfile" runat="server">
         <table class="style2">
        <tr>
            <td align="center" style="background-color: #00FFFF">
                آخرین نفر وارد شده:</td>
        </tr>
        <tr>
            <td align="center">
                    <img id="imgProfile" alt="" border="5" class="style3"  runat=server
                        src="TimeTilco/Staff/1/profile.jpg" /></td>
        </tr>
        <tr>
            <td align="center"><asp:Literal ID="litFullname" runat="server" ></asp:Literal>
                </td>
        </tr>
    </table>
    </asp:HyperLink>
    </div>

            
            

            <asp:GridView ID="GridView1" runat="server" CellPadding="3" 
                GridLines="Horizontal" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:ImageField DataImageUrlField="ax" HeaderText="تصویر">
                        <ControlStyle Height="25px" Width="25px" />
                    </asp:ImageField>
                    <asp:HyperLinkField DataTextField="fullname" HeaderText="نام و نام خانوادگی" DataNavigateUrlFields="staffid" DataNavigateUrlFormatString="~/ProfilePage.aspx?staffid={0}"/>
                    <asp:BoundField DataField="time" HeaderText="زمان ورود"/>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>
            
            
            
            

        </ContentTemplate>
   </asp:UpdatePanel>
    <br />

</asp:Content>

