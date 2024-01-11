<%@ Page Title="" Language="C#" MasterPageFile="~/template_admin.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="travel.admin.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderAdmin" runat="server" >
    <asp:GridView ID="gvListPost" DataKeyNames="SongID" runat="server" AutoGenerateColumns="false"  Width="297px">
        <Columns>
        <asp:BoundField DataField="SongName" HeaderText="Tên bài hát" />
        <asp:BoundField DataField="TenArtist" HeaderText="Tên tác giả" />
        <asp:BoundField DataField="GenreName" HeaderText="Thể loại" />
        <asp:ButtonField ButtonType="Link" HeaderText="Xóa" Text="X" CommandName="select" />
        </Columns>
    </asp:GridView>
</asp:Content>


