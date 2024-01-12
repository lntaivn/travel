<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Template_admin.Master" AutoEventWireup="true" CodeBehind="trangchu_admin.aspx.cs" Inherits="travel.admin.trangchu_admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="blogContainer" runat="server">
        <asp:GridView ID="gvBlog" runat="server" DataKeyNames="id_post" AutoGenerateColumns="false" Width="297px" >
            <Columns>
                <asp:BoundField DataField="title" HeaderText="tiêu đề" />
                <asp:BoundField DataField="admin" HeaderText="Tên tác giả" />
                <asp:BoundField DataField="category" HeaderText="Thể loại" />
                <asp:ButtonField ButtonType="Link" HeaderText="Xóa" Text="X" CommandName="select" />
                <asp:ButtonField ButtonType="Link" HeaderText="update" Text="select" CommandName="select" />
            </Columns>
        </asp:GridView>

        <label>địa điểm</label>
        <asp:GridView ID="gvLocation" runat="server" DataKeyNames="id_location" AutoGenerateColumns="false" Width="297px">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="Tên địa điểm" />
                <asp:ButtonField ButtonType="Link" HeaderText="Xóa" Text="X" CommandName="select" />
            </Columns>
        </asp:GridView>

        <label>Thể loại</label>
        <asp:GridView ID="gvCategory" runat="server" DataKeyNames="id_category" AutoGenerateColumns="false" Width="297px">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="Tên thể loại" />
                <asp:ButtonField ButtonType="Link" HeaderText="Xóa" Text="X" CommandName="select" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
