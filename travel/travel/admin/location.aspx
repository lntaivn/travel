<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Template_admin.Master" AutoEventWireup="true" CodeBehind="location.aspx.cs" Inherits="travel.admin.location" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="lay_out_location">
        <div class="location">

            <h2>Add New Location</h2>
            <asp:Label ID="lblName" runat="server" Text="Location Name:"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

            <asp:Label ID="lblIframe" runat="server" Text="Location Iframe:"></asp:Label>
            <asp:TextBox ID="txtIframe" runat="server"></asp:TextBox>


            <asp:Button ID="btnAddLocation" runat="server" Text="Add Location" OnClick="btnAddLocation_Click" />
        </div>
    </div>
</asp:Content>
