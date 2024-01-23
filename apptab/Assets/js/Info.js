let User;
let Origin;

$(document).ready(() => {
    User = JSON.parse(sessionStorage.getItem("user"));
    if (User == null || User === "undefined") window.location = "../";
    Origin = User.origin;

    $(`[data-id="username"]`).text(User.LOGIN);
    
    GetListProjet();
    GetUsers(undefined);
});
//let urlOrigin = Origin;
//let urlOrigin = "http://softwell.cloud/OPAVI";
function GetUsers(id) {
    let formData = new FormData();
    
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);

    if (!id) {
        formData.append("suser.IDPROJET", User.IDPROJET);
    } else {
        formData.append("suser.IDPROJET", id);
    }

    $.ajax({
        type: "POST",
        url: Origin + '/Etat/DetailsInfoPro',
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

            //$("#proj").val(Datas.data.PROJ);
            $("#soa").val(Datas.data.SOA);
            $("#fina").val(Datas.data.FIN);
            $("#convention").val(Datas.data.CONV);
            $("#catego").val(Datas.data.CAT);
            $("#enga").val(Datas.data.ENG);
            $("#proc").val(Datas.data.PROC);
            $("#min").val(Datas.data.MIN);
            $("#mis").val(Datas.data.MIS);
            $("#prog").val(Datas.data.PROG);
            $("#act").val(Datas.data.ACT);

            $("#proj").val(`${Datas.data.PROJ}`);
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
}

function GetListProjet() {
    let formData = new FormData();

    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    $.ajax({
        type: "POST",
        url: Origin + '/Etat/GetAllPROJET',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            var Datas = JSON.parse(result);
            console.log(Datas);

            if (Datas.type == "error") {
                alert(Datas.msg);
                return;
            }
            if (Datas.type == "login") {
                alert(Datas.msg);
                window.location = window.location.origin;
                return;
            }

            $(`[data-id="proj-list"]`).text("");
            var code = ``;
            $.each(Datas.data, function (k, v) {
                code += `
                    <option value="${v.ID}">${v.PROJET}</option>
                `;
            });
            $(`[data-id="proj-list"]`).append(code);

        },
        error: function (e) {
            console.log(e);
            alert("Problème de connexion. ");
        }
    })
}

$('#proj').on('change', () => {
    const id = $('#proj').val();

    GetUsers(id);
});