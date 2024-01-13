<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Template_admin.Master" AutoEventWireup="true" CodeBehind="trangchu_admin.aspx.cs" Inherits="travel.admin.trangchu_admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmDelete() {
            return confirm("Are you sure you want to delete?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div class="hompage_admin">

            <div class="hompage_admin_list_post">
                <h1><b>Danh sách bài post</b></h1>
                <asp:GridView ID="gvBlog" runat="server" DataKeyNames="id_post" AutoGenerateColumns="false" OnRowCommand="gvBlog_RowCommand" OnRowDeleting="gvBlog_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="title" HeaderText="Tiêu đề" />
                        <asp:BoundField DataField="admin" HeaderText="Tên tác giả" />
                        <asp:BoundField DataField="category" HeaderText="Thể loại" />
                        <asp:TemplateField HeaderText="Xóa">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" Text="Xóa" CommandName="Delete" OnClientClick="return confirmDelete();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Link" HeaderText="Cập nhật" Text="Cập nhật" CommandName="Update" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="hompage_admin_dislay-column">

                <div class="hompage_admin_list_location">
                    <h1><b>Địa điểm</b></h1>
                    <asp:GridView ID="gvLocation" runat="server" DataKeyNames="id_location" AutoGenerateColumns="false" OnRowCommand="gvLocation_RowCommand" OnRowDeleting="gvLocation_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="Tên địa điểm" />
            

                            <asp:TemplateField HeaderText="Xóa">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Xóa" CommandName="Delete" OnClientClick="return confirmDelete();" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:ButtonField ButtonType="Link" HeaderText="Cập nhật" Text="Cập nhật" CommandName="Update" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="hompage_admin_list_category">


                    <h1><b>Thể loại</b></h1>

                    <asp:GridView ID="gvCategory" runat="server" DataKeyNames="id_category" AutoGenerateColumns="false" OnRowCommand="gvCategory_RowCommand" OnRowDeleting="gvCategory_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="Tên thể loại" />
                            <asp:TemplateField HeaderText="Xóa">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Xóa" CommandName="Delete" OnClientClick="return confirmDelete();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Link" HeaderText="Cập nhật" Text="Cập nhật" CommandName="Update" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    
</asp:Content>
