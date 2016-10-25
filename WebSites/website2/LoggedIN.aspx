<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoggedIN.aspx.cs" Inherits="LoggedIN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload</title>
	<link rel="stylesheet" type="text/css" href="style.css" />

    <!-- JavaScript -->
    <script type="text/javascript" src="JavaScript/jquery-3.1.1.min.js"></script>
    <script type="text/javascript">
        var selectedFile;
        $(document).ready(function () {
            //Lägger till event på diven dropzoneUpload
            dragDrop = document.getElementById("dropzoneUpload");
            dragDrop.addEventListener("dragenter", OnDragEnter, false);
            dragDrop.addEventListener("dragover", OnDragOver, false);
            dragDrop.addEventListener("drop", OnDrop, false);

            //Inget händer när något dras till dropzoneUpload
            function OnDragEnter(e) {
                e.stopPropagation();
                e.preventDefault();
            }
            //Inget händer när något dras över dropzoneUpload
            function OnDragOver(e) {
                e.stopPropagation();
                e.preventDefault();
            }
            //Sparar informationen av objektet som släpps på dropzoneUpload
            function OnDrop(e) {
                e.stopPropagation();
                e.preventDefault();
                selectedFile = e.dataTransfer.files[0];
                dragDrop.getElementsByTagName('p')[0].innerText = "Selected file: " + selectedFile.name;
            }

            //Eventet som körs när man klickar på btnUpload
            $("#btnUpload").click(function () {
                //Kollar om selectedFile innehåller någon data.
                if (selectedFile) {
                    //Skapar ett objekt av FromData
                    var data = new FormData();
                    //Lägger till information om filen i objektet data
                    data.append(selectedFile.name, selectedFile);
                    //Gör en POST request till UploadHandler.ashx
                    $.ajax({
                        type: "POST",
                        url: "/WebHandler/UploadHandler.ashx",
                        contentType: false,
                        processData: false,
                        data: data,
                        //Funktionen som körs om requesten kördes utan problem
                        success: function (result) {
                            dragDrop.getElementsByTagName('p')[0].innerText = "File successfully!";
                            uploadSuccessfull();
                        },
                        //Funktionen som körs om requesten kördes med problem
                        error: function (result) {
                            dragDrop.getElementsByTagName('p')[0].innerText = "Error with upload";
                        }
                    });
                }
                //Om selectedFile inte innehåller någon data skrivs ett felmeddelande ut.
                else {
                    dragDrop.getElementsByTagName('p')[0].innerText = "Please select a file";
                }
            });

            //Gör ett postback event
            function uploadSuccessfull()
            {
                __doPostBack('success', 'uploadSuccess');
            }

            //Sätter timeout på sessionen på 5 minuter om användaren är på sidan
            function setHeartbeat() {
                setTimeout("heartbeat()", 30000);
            }
            function heartbeat() {
                $.get(
                    "/WebHandler/SessionHeartbeat.ashx",
                    null,
                    function (data) {
                        setHeartbeat();
                    },
                    "json"
                );
            }

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
                <asp:Label ID="leftEventLabel" runat="server" Visible="false"/>
            </div>
            <div id="mainContentMyFiles">
                <p>Upladdade filer</p>
                <div id="fileBrowser">
                    <div id="folderSelection">
                        <p>Existing folders:</p>
                        <div id="folderSelectionExisting" runat="server">
                        </div>
                        <div id="mainContentCreateFolder">
                            <p>Create a folder:</p>
                            <asp:TextBox ID="createFolderName" runat="server" placeholder="Name" BorderStyle="Solid" Width="120"></asp:TextBox>
                            <asp:Button ID="btnCreateFolder" OnClick="btnCreateFolder_Click" runat="server" Text="Create" />
                        </div>
                    </div>
                    <div id="fileSelection" runat="server">

                    </div>
                </div>
            </div>
        </div>
        </form>
	</div>
</body>
</html>