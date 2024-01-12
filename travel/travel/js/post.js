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
