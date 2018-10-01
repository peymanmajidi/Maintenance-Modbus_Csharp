<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <img src="Images/sales_and_staffing_interior_icon.png" />


    <p>
        به سامانه حضور غیاب تیلکو خوش آمدید</p>
    <p>
        در این سامانه میتوانید از آخرین وضعیت حضور غیاب کارکنان آگاهی پیدا کنید. ضمن 
        اینکه کارکنان و کارگران را مدیریت و بین کلیه کارکنان جستجو کنید.</p>
    <p>
        برای ورود به سامانه کلیک کنید</p>
    <p>
        <asp:Button Text="ورود" runat="server" ID="btnStart" onclick="btnStart_Click" 
            Width="116px" class="button"/>
        &nbsp;</p>




</asp:Content>

