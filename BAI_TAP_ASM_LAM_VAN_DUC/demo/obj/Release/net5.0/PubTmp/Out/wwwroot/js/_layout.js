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

    $("#ProjectNameDetail").val(dataDetail.projectName);
    $("#ProjectKeyDetail").val(dataDetail.projectKey);

    $("#ProjectIdDetail").val(dataDetail.projectId);
    $("#TitleTaskDetail").val(dataDetail.titleTask);
    $("#DescriptionTaskDetail").val(dataDetail.descriptionTask);
    $("#DeadLineTaskDetail").val(dataDetail.deadLineTask);
    $("#PriorityTaskDetail").val(dataDetail.priorityTask);
    $("#LevelTaskDetail").val(dataDetail.levelTask);
    $("#UserImplementNameDetail").val(dataDetail.userImplement);



    


}