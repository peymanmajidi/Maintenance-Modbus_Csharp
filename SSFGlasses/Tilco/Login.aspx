<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" enableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style>

</style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form>
  <div class="imgcontainer">
    &nbsp;<img alt="" class="style1" src="Images/login2.png" /></div>

  <div class="container">
    <label><b>نام کاربری</b></label>
     <asp:TextBox id="txtusername" runat="server" />

    <label><b>گذرواژه</b></label>
    <asp:TextBox id="txtpassword" runat="server" TextMode="Password"  />
        
  <asp:Button Text="ورود" runat="server" ID="btnStart" 
            Width="116px" class="button" onclick="btnStart_Click"/>
    
  </div>

</form>
    </form>
</asp:Content>

