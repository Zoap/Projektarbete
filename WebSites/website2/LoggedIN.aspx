<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoggedIN.aspx.cs" Inherits="LoggedIN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>test</title>
	<link rel="stylesheet" type="text/css" href="style.css" />

    <!-- JavaScrip -->
    <script type="text/javascript" src="JavaScript/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="JavaScript/DropZone.js"></script>
    <script type="text/javascript">
        var selectedFile;

        $(document).ready(function () {
            dragDrop = document.getElementById("dropzoneUpload");
            dragDrop.addEventListener("dragenter", OnDragEnter, false);
            dragDrop.addEventListener("dragover", OnDragOver, false);
            dragDrop.addEventListener("drop", OnDrop, false);

            function OnDragEnter(e) {
                e.stopPropagation();
                e.preventDefault();
            }
            function OnDragOver(e) {
                e.stopPropagation();
                e.preventDefault();
            }
            function OnDrop(e) {
                console.log("Drag drop");
                e.stopPropagation();
                e.preventDefault();
                selectedFile = e.dataTransfer.files[0];
                dragDrop.getElementsByTagName('p')[0].innerText = "Selected file: " + selectedFile.name;
            }

            $("#btnUpload").click(function () {
                if (selectedFile) {
                    var data = new FormData();
                    data.append(selectedFile.name, selectedFile);
                    $.ajax({
                        type: "POST",
                        URL: "/WebHandler/UploadHandler.ashx",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            dragDrop.getElementsByTagName('p')[0].innerText = selectedFile.name + "Uploaded successfully!";
                        },
                        error: function (result) {
                            dragDrop.getElementsByTagName('p')[0].innerText = "Error with upload";
                        }
                    });
                }
                else {
                    dragDrop.getElementsByTagName('p')[0].innerText = "Please select a file";
                }
            });
        });
    </script>
    <!--- /JavaScript -->
</head>
<body>
	<div id="Wrapper">
		<div id="Header">
            <p>Logged IN!</p>
		</div>
        <form id="formFileManager" runat="server">
        <div id="mainContent">
            <div id="mainContentUpload">
			    <p>För att ladda upp en fil</p>

                    <div id="dropzoneUpload">
                        <center>
                            <input type="button" id="btnUpload" value="Upload file" />
                            <p>Drag & Drop</p>
                        </center>
                    </div>

            </div>
            <div id="mainContentMyFiles">
                <p>Upladdade filer</p>
            </div>
        </div>
        </form>
	</div>
</body>
</html>