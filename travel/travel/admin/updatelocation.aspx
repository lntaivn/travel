<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Template_admin.Master" AutoEventWireup="true" CodeBehind="updatelocation.aspx.cs" Inherits="travel.admin.updatelocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="lay_out_location">
     <div class="location">

         <h2>Update Location</h2>
         <asp:Label ID="lblName" runat="server" Text="Location Name:"></asp:Label>
         <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

         <asp:Label ID="lblIframe" runat="server" Text="Location Iframe:"></asp:Label>
         <asp:TextBox ID="txtIframe" runat="server"></asp:TextBox>

         <asp:Button ID="btnUpdateLocation" runat="server" Text="Update Location" OnClick="btnUpdateLocation_Click" />
     </div>
 </div>
</asp:Content>
