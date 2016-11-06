<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoggedIN.aspx.cs" Inherits="LoggedIN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to Project Drop</title>
    <link rel="shortcut icon" href="Images/server_inst_eyes_open.ico" />
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
    <style type="text/css">
        .auto-style3 {
            margin-left: 622px;
            cursor: pointer;
            cursor: hand;
        }
        img:hover {
            cursor: pointer;
            cursor: hand;
        }
        .auto-style4 {
            margin-left: 0;
            cursor: pointer;
            cursor: hand;
        }
    </style>
</head>
<body>
	<div id="Wrapper">
		<div id="Header">
            <p>Welcome!</p>
		</div>
        <form id="formFileManager" runat="server">
        <div id="mainContent">
            <div id="mainContentUpload">
			    <p>Drag and drop a file to upload.</p>
                <div id="dropzoneUpload">
                    <center>
                        <input type="button" id="btnUpload" value="Upload file" />
                        <p>Drag & Drop</p>
                    </center>
                </div>
                <asp:Label ID="leftEventLabel" runat="server" Visible="false"/>
            </div>
            <div id="mainContentMyFiles">
                <p>Your files and folders.</p>
                <div id="fileBrowser">
                    <div id="folderSelection">
                        <p>Existing folders:</p>
                        <div id="folderSelectionExisting" runat="server">
                        </div>
                        <div id="mainContentCreateFolder">
                            <p>Create a folder:</p>
                            <asp:TextBox ID="createFolderName" runat="server" placeholder="Name" BorderStyle="Solid" Width="120"></asp:TextBox>
                            <asp:Button ID="btnCreateFolder" OnClick="btnCreateFolder_Click" runat="server" Text="Create" CssClass="auto-style4" />
                        </div>
                    </div>
                    <div id="fileSelection" runat="server">
                    </div>
                </div>
            </div>
            <div>
                <asp:Button ID="LogOut" OnClick="btnLogOut_Click" runat="server" Text="Logout" CssClass="auto-style3" />
            </div>
        </div>
        </form>
	</div>
</body>
</html>
<footer class="footer">
    <style>
        a:link    {color:white; background-color:transparent; text-decoration:none}
        a:visited {color:white; background-color:transparent; text-decoration:none}
        a:hover {color:white; background-color:transparent; text-decoration:underline}
    </style>
    <p class="copyright"><strong><small>Copyright © 2016 ProjectDrop. Background Image By: <a href="http://www.designmastery.se">Björn Ed</a></small></strong></p>
</footer>