<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 301px;
        }
        .auto-style3 {
            width: 50%;
        }
         .auto-style3 {
            width: 50%;
        }
        .auto-style4 {
            text-align: center;
        }
        .auto-style5 {
            text-align: right;
            font-weight: bold;
        }
        .auto-style6 {
            width: 80%;

            border: medium;
            text-align: center;
           
        }
        .auto-style7 {
            text-align: right;
            height: 24px;
            font-weight: 700;
        }
        .auto-style8 {
            text-align: left;
            font-weight: normal;
        }
        .auto-style9 {
            text-align: right;
            font-weight: 700;
            height: 27px;
        }
        .auto-style10 {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center></center>
    <table class="auto-style1">
        <tr>
            <td colspan="2">
                <img alt="" class="auto-style2" src="TimeTilco/Staff/1/profile.jpg" id="picProfile" runat="server" /></td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style8">نام و نام خانوادگی:</td>
            <td class="auto-style5">
                <asp:Literal ID="litFullname" runat="server"></asp:Literal>
            </td>
        </tr>

        <tr>
            <td class="auto-style8">سمت:</td>
            <td class="auto-style5">
                <asp:Literal ID="litSemat" runat="server"></asp:Literal>
            </td>
        </tr>
        

        <tr>
            <td class="auto-style8">واحد:</td>
            <td class="auto-style5">
                <asp:Literal ID="litUnit" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">شماره تماس:</td>
            <td class="auto-style5">
                <asp:Literal ID="litMobile" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="auto-style4" colspan="2"><br /><center>
                <table align="center" class="auto-style6" border="1" style="border: dotted;">
                    <tr>
                        <td bgcolor="#669999" class="auto-style10">وضعیت حضور(امروز)</td>
                    </tr>
                    <tr>
                        <td class="auto-style9">&nbsp&nbsp زمان ورود:</td>
                    </tr>
                    <tr>
                        <td class="w3-right-align"><center>
                <asp:Literal ID="litTimein" runat="server" Text="00:00"></asp:Literal></center>
                        </td>
                    </tr>
                    <tr>
                        <td class="w3-right-align">&nbsp&nbspتاخیر:</td>
                    </tr>
                    <tr>
                        <td class="auto-style7"><center>
                <asp:Literal ID="litTakhir" runat="server" Text="00:00"></asp:Literal></center>
                        </td>
                    </tr>
                    </table></center>
            </td>
        </tr>
        <tr>
            <td class="auto-style5" colspan="2" align="right">
                


            <asp:Button Text="&lt; بازگشت " runat="server" ID="btnStart" 
            Width="116px" class="button" onclick="btnStart_Click"/>
    


            </td>
        </tr>
        </table>
</asp:Content>

