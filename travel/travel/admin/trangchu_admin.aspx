<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Template_admin.Master" AutoEventWireup="true" CodeBehind="trangchu_admin.aspx.cs" Inherits="travel.admin.trangchu_admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="hompage_admin">

            <div class="hompage_admin_list_post">
                <h1><b>Danh sách bài post</b></h1>
                <asp:GridView ID="gvBlog" runat="server" DataKeyNames="id_post" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="title" HeaderText="Tiêu đề" />
                        <asp:BoundField DataField="admin" HeaderText="Tên tác giả" />
                        <asp:BoundField DataField="category" HeaderText="Thể loại" />
                        <asp:ButtonField ButtonType="Link" HeaderText="Xóa" Text="X" CommandName="select" />
                        <asp:ButtonField ButtonType="Link" HeaderText="update" Text="select" CommandName="select" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="hompage_admin_dislay-column">

                <div class="hompage_admin_list_location">
                    <h1><b>Địa điểm</b></h1>
                    <asp:GridView ID="gvLocation" runat="server" DataKeyNames="id_location" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="Tên địa điểm" />
                            <asp:ButtonField ButtonType="Link" HeaderText="Xóa" Text="X" CommandName="select" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="hompage_admin_list_category">


                    <h1><b>Thể loại</b></h1>

                    <asp:GridView ID="gvCategory" runat="server" DataKeyNames="id_category" AutoGenerateColumns="false" >
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="Tên thể loại" />
                            <asp:ButtonField ButtonType="Link" HeaderText="Xóa" Text="X" CommandName="select" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

</asp:Content>
