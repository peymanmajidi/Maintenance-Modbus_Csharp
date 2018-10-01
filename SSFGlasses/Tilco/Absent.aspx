<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Absent.aspx.cs" Inherits="Absent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        .style3
        {
            width: 123px;
            height: 125px;
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
    لیست کارمندان غایب:
    </h3>
    <asp:Button ID="Button1" runat="server" 
        onclick="Button1_Click" Text="به روز رسانی" Visible="False" />
    <asp:Timer ID="timerTriger" runat="server" Interval="1000" 
        ontick="timerTriger_Tick" EnableViewState="False">
    </asp:Timer>
    <br /> 
    <div id="divnew" runat="server" visible=false>
    
        <table class="style2">
            <tr>
                <td class="style4">
                    
                    <img id="imgProfile" alt="" border="5" class="style3"  runat=server
                        src="TimeTilco/Staff/1/profile.jpg" />
                </td>
                <td class="w3-center">
                    <asp:Literal ID="litFullname" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    
    </div>

     
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" 
        RenderMode="Inline">
        <Triggers>
         <asp:AsyncPostBackTrigger ControlID="timerTriger" EventName="Tick" />
        </Triggers>
         
        <ContentTemplate>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
                GridLines="None" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:ImageField DataImageUrlField="ax" HeaderText="تصویر">
                        <ControlStyle Height="25px" Width="25px" />
                    </asp:ImageField>
                    <asp:BoundField DataField="fullname" HeaderText="نام و نام خانوادگی" >
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="tag" HeaderText="علت"/>
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
        </ContentTemplate>
   </asp:UpdatePanel>
    <br />

</asp:Content>

