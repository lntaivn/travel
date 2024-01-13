<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Template_admin.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="travel.admin.category" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="lay_out_location">
        <div class="location">

            <h2>New category</h2>
            <asp:Label ID="lblName" runat="server" Text="category Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

            <asp:Button ID="btnUpdatecategory" runat="server" Text="Update category" OnClick="btnUpdatecategory_Click" />
         </div>
    </div>
</asp:Content>
