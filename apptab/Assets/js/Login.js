﻿let urlOrigin = "https://localhost:44334";
//let urlOrigin = "http://softwell.cloud/OPAVI";
$("#login_mdp, #login_user").on("keydown", (e) => {

    if (e.keyCode == 13) $("[login_connect]").click(); //login();
});

$(`[login_connect]`).click(() => {
    let formData = new FormData();

    formData.append("Users.LOGIN", $('#login_user').val());
    formData.append("Users.PWD", $('#login_mdp').val());

    $.ajax({
        type: "POST",
        url: urlOrigin + '/User/Login',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            var Datas = JSON.parse(result);
            console.log(Datas);

            if (Datas.type == "error") {

                alert(Datas.msg);
                //alert("Vérifiez vos identifiants");
                return;
            }

            let user = {
                LOGIN: $('#login_user').val(),
                PWD: $('#login_mdp').val(),
                ROLE: Datas.Data.ROLE,
                IDPROJET: Datas.Data.IDPROJET,
                origin: urlOrigin
            }

            sessionStorage.setItem("user", JSON.stringify(user));

            window.location = urlOrigin + "/Home/TdbAccueil";
        },
        error: function () {
            alert("");
        }
    });
});