$(document).ready(function () {
    $.ajax('/Projects/ListProductByUser', {
        type: 'GET',  // http method
        success: function (data, status, xhr) {
            var htmlData = "";
            for (var i = 0; i < data.length; i++) {
                htmlData += `<a class="dropdown-item" href="/Projects/Details/${data[i].ProjectID}">${data[i].projectName}</a>`

            }
            htmlData += `<div class="dropdown-divider"></div>
                                                    <a class="dropdown-item" href="/Projects/Index">Tất cả dự án</a>`
            $("#ListProjectLayout").html(htmlData);

        },
        error: function (jqXhr, textStatus, errorMessage) {

            $("#ListProjectLyout").html(`<div class="dropdown-divider"></div>
                                    <a class="dropdown-item" href="/Projects/Index">Tất cả dự án</a>`);
        }
    });
});


function transferDetailTask(item) {
    console.log(item);
    var dataDetail = item;

    var formattedDate = dataDetail.deadLineTask.split('T')[0];



    $("#TaskIdDetail").val(dataDetail.taskId),

    $("#ProjectNameDetail").val(dataDetail.projectName);
    $("#ProjectKeyDetail").val(dataDetail.projectKey);
    $("#ProjectIdDetail").val(dataDetail.projectId);
    $("#TitleTaskDetail").val(dataDetail.titleTask);
    $("#DescriptionTaskDetail").val(dataDetail.descriptionTask);
    $("#DeadLineTaskDetail").val(formattedDate);
    $("#PriorityTaskDetail").val(dataDetail.priorityTask);
    $("#LevelTaskDetail").val(dataDetail.levelTask);
    $("#UserImplementNameDetail").val(dataDetail.userImplement);
    $("#findUserEditTask").val(dataDetail.userImplementFullName);


}


function Func_SaveEditProjectJob() {

    var data = {
        TaskId: $("#TaskIdDetail").val(),
        ProjectKey :$("#ProjectKeyDetail").val(),
        ProjectId : $("#ProjectIdDetail").val(),
        TitleTask : $("#TitleTaskDetail").val(),
        DescriptionTask :$("#DescriptionTaskDetail").val(),
        DeadLineTask : $("#DeadLineTaskDetail").val(),
        PriorityTask : $("#PriorityTaskDetail").val(),
        LevelTask : $("#LevelTaskDetail").val(),
        UserImplement: $("#UserImplementNameDetail").val(),

        
    }

    
    $.ajax('/ProjectJobs/Edit/'+$("#TaskIdDetail").val(), {
        type: 'POST',  // http method
        data: data,
        success: function (data, status, xhr) {
            location.reload();
        },
        error: function (jqXhr, textStatus, errorMessage) {
            var message = jqXhr.responText;
            $("#messageEditTask").val(message);
        }
    });
}



function FindUserForEditTask() {

    var data = $("#findUserEditTask").val();
    $.ajax('/User/SearchUser?tukhoa='+data, {
        type: 'GET',  // http method
        success: function (data, status, xhr) {
            console.log(data);
            var htmlData = "";
            for (var i = 0; i < data.length; i++) {
                htmlData += `<tr onmousedown="chonceUserEditTask('${data[i].fullName}','${data[i].username}')">
                    <td><img style="width:25px;height:25px: border-radius:50%" src="${data[i].avatar}"> ${data[i].fullName}</td>
                </tr>`

            }
           
            $("#tdobyUserEditTask").html(htmlData);

        },
        error: function (jqXhr, textStatus, errorMessage) {

        }
    });

}

function focusFindUserEditTask() {
    var myDiv = document.getElementById("divUserEditTask");
    myDiv.style.display = "block";
}

function blurFindUserEditTask() {
    var myDiv = document.getElementById("divUserEditTask");
    myDiv.style.display = "none";
}


function chonceUserEditTask(fullName, username) {
    $("#findUserEditTask").val(fullName);
    $("#UserImplementNameDetail").val(username);

}



function FindProjectForEditTask() {

    var data = $("#ProjectKeyDetail").val();
    $.ajax('/Projects/ListProduct?tukhoa=' + data, {
        type: 'GET',  // http method
        success: function (data, status, xhr) {
            console.log(data);
            var htmlData = "";
            for (var i = 0; i < data.length; i++) {
                htmlData += `<tr onmousedown="chonceProjectEditTask('${data[i].projectId}','${data[i].projectKey}')">
                    <td>${data[i].projectKey}</td>
                    <td>${data[i].projectName}</td>

                </tr>`

            }

            $("#tdobyProjectEditTask").html(htmlData);

        },
        error: function (jqXhr, textStatus, errorMessage) {

        }
    });

}

function focusFindProjectEditTask() {
    var myDiv = document.getElementById("divProjectEditTask");
    myDiv.style.display = "block";
}

function blurFindProjectEditTask() {
    var myDiv = document.getElementById("divProjectEditTask");
    myDiv.style.display = "none";
}


function chonceProjectEditTask(projectId, projectKey) {
    $("#ProjectIdDetail").val(projectId);
    $("#ProjectKeyDetail").val(projectKey);

}