<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Alone_Create.aspx.cs" Inherits="travel.admin.Alone_Create" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/blog.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="main__author">
                <div>
                    <h2>Create Blog Post</h2>
                </div>
                <div class="post-creation">
                    <div class="post__display">

                        <asp:Image ID="preview" runat="server" Style="max-width: 300px; max-height: 300px; margin-top: 20px;" />

                        <div class="post__display-add-img">
                            <input id="addImageButton" type="file" text="Add Image" onchange="triggerFileInput()" />
                        </div>

                        <div class="post__display-textare">
                            <asp:TextBox ID="postTitle" runat="server" CssClass="post-title" placeholder="New post title here..."></asp:TextBox>
                        </div>

                        <label for="category-dropdown">Choose a category:</label>
                        <asp:DropDownList ID="categoryDropdown" runat="server" onchange="addTag()">
                            <asp:ListItem Text="Select a category..." Value="" />

                        </asp:DropDownList>
                        <div class="tags-container" id=<div class="tooltip">
    <asp:Button ID="btnBold" runat="server" UseSubmitBehavior="False" OnClientClick="insertMarkdown('**', '**')" CssClass="btn-bold">
        <i>B</i>
    </asp:Button>
    <span class="tooltiptext">Bold (CTRL+B)</span>
</div>

<div class="tooltip">
    <asp:Button ID="btnItalic" runat="server" UseSubmitBehavior="False" OnClientClick="insertMarkdown('_', '_')" CssClass="btn-italic">
        <i>I</i>
    </asp:Button>
    <span class="tooltiptext">Italic (CTRL+I)</span>
</div>

<div class="tooltip">
    <asp:Button ID="btnOrderedList" runat="server" UseSubmitBehavior="False" CssClass="btn-ordered-list" OnClientClick="insertList('ol'); return false;">
        <i class='fa-solid fa-list-ol'></i>
    </asp:Button>
    <span class="tooltiptext">Ordered list</span>
</div>

<div class="tooltip">
    <asp:Button ID="btnUnorderedList" runat="server" UseSubmitBehavior="False" CssClass="btn-unordered-list" OnClientClick="insertList('ul'); return false;">
        <i class='fa-solid fa-list'></i>
    </asp:Button>
    <span class="tooltiptext">Unordered list</span>
</div>
                    <asp:Button ID="btnOrderedList" runat="server" UseSubmitBehavior="False" CssClass="btn-ordered-list" OnClientClick="insertList('ol'); return false;">
                                <i class='fa-solid fa-list-ol'></i>
                            </asp:Button>
                            <span class="tooltiptext">Ordered list</span>
                        </div>

                        <div class="tooltip">
                            <asp:Button ID="btnUnorderedList" runat="server" UseSubmitBehavior="False" CssClass="btn-unordered-list" OnClientClick="insertList('ul'); return false;">
                                <i class='fa-solid fa-list'></i>
                            </asp:Button>
                            <span class="tooltiptext">Unordered list</span>
                        </div>

                        <div class="tooltip">
                        </div>
                    </div>

                    <asp:TextBox ID="article_body_markdown" name="article_body_markdown" runat="server" TextMode="MultiLine" CssClass="textarea"
                        placeholder="Write your post content here..."></asp:TextBox>

                    <asp:Button ID="saveButton" runat="server" Text="Save" OnClientClick="return savePost" />


                </div>
            </div>

        </div>
    </form>
</body>
</html>
<script>
    function triggerFileInput() {

        var fileInput = document.getElementById('addImageButton');
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
        const textarea = document.getElementById('<%= article_body_markdown.ClientID %>');
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
        autoExpandTextarea({ target: textarea });ument.getElementById('<%= article_body_markdown.ClientID %>');
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
