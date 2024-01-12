<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Template_admin.Master" AutoEventWireup="true" CodeBehind="blog.aspx.cs" Inherits="travel.admin.blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main__author">
        <div>
            <h2>Create Blog Post</h2>
        </div>
        <div class="post-creation">
            <div class="post__display">

                <asp:Image ID="preview" UseSubmitBehavior="False" runat="server" Style="max-width: 300px; max-height: 300px; margin-top: 20px;" />

                <div class="post__display-add-img">
                    <asp:FileUpload ID="addImageButton" runat="server" CssClass="yourCSSClass" onchange="triggerFileInput()" UseSubmitBehavior="False"/>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="return cancelUpload();" />
                </div>
                

                <div class="post__display-textare">
                    <asp:TextBox ID="postTitle" runat="server" CssClass="post-title" placeholder="New post title here..."></asp:TextBox>
                </div>


                <div class="post__display-dropdown">
                    <div class="post__display-dropdown-column">
                        
                        <label for="category-dropdown">Choose a category:</label>
                        <asp:DropDownList ID="categoryDropdown" runat="server">
                            <asp:ListItem Text="Select a category..." Value="" />
                        </asp:DropDownList>
                    </div>
                    <div class="post__display-dropdown-column">
                           <label for="category-dropdown">Choose a location:</label>
                           <asp:DropDownList ID="locationDropDownList" runat="server">
                               <asp:ListItem Text="Select a location..." Value="" />
                           </asp:DropDownList>
                    </div>
                </div>

                <div class="post__display-summary">
                <asp:TextBox ID="summary" Width="100%" BorderStyle="None" placeholder="Insert summary" runat="server">

                </asp:TextBox>
                </div>

            </div>
        </div>
        <div class="author-form__tooltip">
            <div class="toolbar">
                <div class="tooltip">
                    <b>
                        <asp:Button ID="btnBold" runat="server" Text="B" OnClientClick="return insertMarkdown('**', '**')" UseSubmitBehavior="False" /></b>
                    <span class="tooltiptext">Bold (CTRL+B)</span>
                </div>

                <div class="tooltip">
                    <i>
                        <asp:Button ID="btnItalic" runat="server" Text="I" OnClientClick="return insertMarkdown('_', '_')" UseSubmitBehavior="False" /></i>
                    <span class="tooltiptext">Italic (CTRL+I)</span>
                </div>

                <div class="tooltip">
                    <asp:Button ID="btnOrderedList" runat="server" Text="1." UseSubmitBehavior="False" OnClientClick="return insertList('ol');" />
                    <span class="tooltiptext">Ordered list</span>
                </div>

                <div class="tooltip">
                    <asp:Button ID="btnUnorderedList" runat="server" Text="Gạch chân" UseSubmitBehavior="False" OnClientClick="return insertList('ul');" />
                    <span class="tooltiptext">Unordered list</span>
                </div>

            </div>

            <asp:TextBox ID="article_body_markdown" Height="400px" name="article_body_markdown" runat="server" TextMode="MultiLine" CssClass="textarea"
                placeholder="Write your post content here..."></asp:TextBox>

            <asp:Button ID="saveButton" runat="server" Text="Save" OnClientClicking="return savePost()" OnClick="saveButton_Click" />
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
    <script>
        function uploadFile() {
            <%--var fileInput = document.getElementById('<%= fileToUpload.ClientID %>');
            if (fileInput.value === "") {
                alert('Please select a file.');
                return;
            }--%>
        }
        function savePost() {
            // Kiểm tra xem DropDownList có được chọn không
            var categoryDropdown = document.getElementById('<%= categoryDropdown.ClientID %>');
             var locationDropdown = document.getElementById('<%= locationDropDownList.ClientID %>');
             var selectedCategoryValue = categoryDropdown.options[categoryDropdown.selectedIndex].value;
             var selectedLocationValue = locationDropdown.options[locationDropdown.selectedIndex].value;
            var valuefile = document.getElementById('<%= addImageButton.ClientID %>').value; 
             if (selectedCategoryValue === "" || selectedLocationValue === "") {
                 alert('Please select a category and location.');
                 return false;
            }
            if (valuefile === "") {
                alert('Please choose a banner and format img');
                return false;
            }
            // Kiểm tra định dạng của file
            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
            if (!allowedExtensions.exec(valuefile)) {
                alert('Invalid file format. Please choose a valid image file.');
                return false;
            }

            return true; 
         }
        function cancelUpload() {
            // Xóa đường dẫn hình ảnh
            document.getElementById('<%= preview.ClientID %>').src = '';
        
        // Xóa giá trị của FileUpload
             document.getElementById('<%= addImageButton.ClientID %>').value = '';
         }    
        function triggerFileInput() {
            var fileInput = document.getElementById('<%= addImageButton.ClientID %>');
            var previewImage = document.getElementById('<%= preview.ClientID %>');
            if (fileInput.files && fileInput.files[0]) {
                var file = fileInput.files[0];
                var fileType = file.type;
                if (fileType.startsWith('image/')) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        previewImage.src = e.target.result;
                        previewImage.style.display = 'block';
                    };
                    reader.readAsDataURL(file);
                } else {
                    alert('Vui lòng chọn một tệp hình ảnh.');
                    fileInput.value = '';
                }
            }
        }
        function insertMarkdown(before, after) {
            const textarea = document.getElementById('<%= article_body_markdown.ClientID%>');
            const start = textarea.selectionStart;
            const end = textarea.selectionEnd;
            const text = textarea.value;
            const beforeText = text.substring(0, start);
            const afterText = text.substring(end);
            const selectedText = text.substring(start, end);
            textarea.value = beforeText + before + selectedText + after + afterText;
            textarea.focus();
            textarea.setSelectionRange(start + before.length, end + before.length);
        }
        function insertList(type) {
            const textarea = document.getElementById('<%= article_body_markdown.ClientID%>');
            const cursorPosition = textarea.selectionStart;
            const textBefore = textarea.value.substring(0, cursorPosition);
            const textAfter = textarea.value.substring(cursorPosition);
            let listSyntax;

            if (type === 'ol') {
                listSyntax = '1. ';
            } else if (type === 'ul') {
                listSyntax = '- ';
            }

            if (cursorPosition !== 0 && textBefore[textBefore.length - 1] !== '\n') {
                listSyntax = '\n' + listSyntax;
            }

            textarea.value = textBefore + listSyntax + '\n' + textAfter;
            // Move the cursor to the end of the inserted list syntax
            const newPosition = cursorPosition + listSyntax.length;
            textarea.setSelectionRange(newPosition, newPosition);
            textarea.focus();
            autoExpandTextarea({ target: textarea });
        }

        function setCursorAtEnd(event) {
            const el = event.target;
            const textLength = el.value.length;
            el.selectionStart = textLength;
            el.selectionEnd = textLength;
        }

        function autoExpandTextarea(event) {
            event.target.style.height = 'auto';
            event.target.style.height = event.target.scrollHeight + 'px';
        }

    </script>

</asp:Content>
