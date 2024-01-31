let User;
let Origin;

$(document).ready(() => {
    User = JSON.parse(sessionStorage.getItem("user"));
    if (User == null || User === "undefined") window.location = "../";
    Origin = User.origin;
    $(`[data-id="username"]`).text(User.LOGIN);
    GetUsers();
});

//let urlOrigin = Origin;
//let urlOrigin = "http://softwell.cloud/OPAVI";
function GetUsers() {
    let formData = new FormData();
    
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    $.ajax({
        type: "POST",
        url: Origin + '/SuperAdmin/DetailsMail',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            var Datas = JSON.parse(result);

            if (Datas.type == "error") {
                alert(Datas.msg);
                return;
            }
            if (Datas.type == "login") {
                alert(Datas.msg);
                window.location = window.location.origin;
                return;
            }

            $("#ParaT").val(Datas.data.MAILTRAI);
            $("#ParaV").val(Datas.data.MAILVALI);
            $("#ParaP").val(Datas.data.MAILPAYM);
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
}

$(`[data-action="UpdateUser"]`).click(function () {
    let ParaT = $("#ParaT").val();
    let ParaV = $("#ParaV").val();
    let ParaP = $("#ParaP").val();
    if (!ParaT || !ParaV || !ParaP) {
        alert("Veuillez renseigner les mails. ");
        return;
    }

    let formData = new FormData();

    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    formData.append("param.MAILTRAI", $(`#ParaT`).val());
    formData.append("param.MAILVALI", $(`#ParaV`).val());
    formData.append("param.MAILPAYM", $(`#ParaP`).val());

    $.ajax({
        type: "POST",
        url: Origin + '/SuperAdmin/UpdateMail',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            var Datas = JSON.parse(result);

            if (Datas.type == "error") {
                alert(Datas.msg);
                return;
            }
            if (Datas.type == "success") {
                alert(Datas.msg);
                return;
            }
            if (Datas.type == "login") {
                alert(Datas.msg);
                window.location = window.location.origin;
            }
        },
    });
});