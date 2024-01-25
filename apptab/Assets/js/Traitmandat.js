let User;
let Origin;

$(document).ready(() => {
    User = JSON.parse(sessionStorage.getItem("user"));
    if (User == null || User === "undefined") window.location = "../";
    Origin = User.origin;

    $(`[data-id="username"]`).text(User.LOGIN);
    
    GetListProjet();
    GetUsers(undefined);
    GetListLOAD();
});

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
        url: Origin + '/Traitement/DetailsInfoPro',
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

            $("#proj").val(`${Datas.data.PROJ}`);
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
}

//let urlOrigin = "http://softwell.cloud/OPAVI";
function GetListProjet() {
    let formData = new FormData();

    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    $.ajax({
        type: "POST",
        url: Origin + '/Traitement/GetAllPROJET',
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


//GENERER//
$('[data-action="GenereR"]').click(function () {
    let dd = $("#dateD").val();
    let df = $("#dateF").val();
    if (!dd || !df) {
        alert("Veuillez renseigner les dates afin de générer les mandats. ");
        return;
    }

    let formData = new FormData();
    //alert(baseName);
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDSOCIETE);

    formData.append("DateDebut", $('#dateD').val());
    formData.append("DateFin", $('#dateF').val());
    
    $.ajax({
        type: "POST",
        url: Origin + '/Traitement/Generation',
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
            if (Datas.type == "success") {
                //window.location = window.location.origin;
                ListResult = Datas.data
                contentpaie = ``;
                $.each(ListResult, function (k, v) {
                    contentpaie += `
                    <tr compteG-id="${v.No}" class="select-text">
                        <td style="font-weight: bold; text-align:center">
                            <input type="checkbox" name = "checkprod" compteg-ischecked/>
                        </td>
                        <td style="font-weight: bold; text-align:center">${v.REF}</td>
                        <td style="font-weight: bold; text-align:center">${v.OBJ}</td>
                        <td style="font-weight: bold; text-align:center">${v.TITUL}</td>
                        <td style="font-weight: bold; text-align:center">${formatCurrency(String(v.MONT).replace(",", "."))}</td>
                        <td style="font-weight: bold; text-align:center">${v.COMPTE}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATE)}</td>
                        <td class="elerfr" style="font-weight: bold; text-align:center">
                            <div onclick="deleteUser('${v.No}')"><i class="fa fa-tags fa-lg text-danger"></i></div>
                        </td>
                    </tr>`
                });

                $('.afb160Paie').empty();
                $('.afb160Paie').html(contentpaie);
            }
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
});

//SIIGLOAD//
function GetListLOAD() {
    let formData = new FormData();
    //alert(baseName);
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDSOCIETE);

    $.ajax({
        type: "POST",
        url: Origin + '/Traitement/GenerationSIIGLOAD',
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
            if (Datas.type == "success") {
                //window.location = window.location.origin;
                ListResult = Datas.data
                contentpaie = ``;
                $.each(ListResult, function (k, v) {
                    contentpaie += `
                    <tr compteG-id="${v.No}" class="select-text">
                        <td style="font-weight: bold; text-align:center">
                            <input type="checkbox" name = "checkprod" compteg-ischecked/>
                        </td>
                        <td style="font-weight: bold; text-align:center">${v.REF}</td>
                        <td style="font-weight: bold; text-align:center">${v.OBJ}</td>
                        <td style="font-weight: bold; text-align:center">${v.TITUL}</td>
                        <td style="font-weight: bold; text-align:center">${formatCurrency(String(v.MONT).replace(",", "."))}</td>
                        <td style="font-weight: bold; text-align:center">${v.COMPTE}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATE)}</td>
                        <td class="elerfr" style="font-weight: bold; text-align:center">
                            <div onclick="deleteUser('${v.No}')"><i class="fa fa-tags fa-lg text-danger"></i></div>
                        </td>
                    </tr>`
                });

                $('.traitementORDSEC').empty();
                $('.traitementORDSEC').html(contentpaie);
            }
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
}

//GENERER SIIG//
$('[data-action="GenereSIIG"]').click(function () {
    let dd = $("#dateD").val();
    let df = $("#dateF").val();
    if (!dd || !df) {
        alert("Veuillez renseigner les dates afin de générer les mandats. ");
        return;
    }

    let formData = new FormData();
    //alert(baseName);
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDSOCIETE);

    formData.append("DateDebut", $('#dateD').val());
    formData.append("DateFin", $('#dateF').val());

    $.ajax({
        type: "POST",
        url: Origin + '/Traitement/GenerationSIIG',
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
            if (Datas.type == "success") {
                //window.location = window.location.origin;
                ListResult = Datas.data
                contentpaie = ``;
                $.each(ListResult, function (k, v) {
                    contentpaie += `
                    <tr compteG-id="${v.No}" class="select-text">
                        <td style="font-weight: bold; text-align:center">
                            <input type="checkbox" name = "checkprod" compteg-ischecked/>
                        </td>
                        <td style="font-weight: bold; text-align:center">${v.REF}</td>
                        <td style="font-weight: bold; text-align:center">${v.OBJ}</td>
                        <td style="font-weight: bold; text-align:center">${v.TITUL}</td>
                        <td style="font-weight: bold; text-align:center">${formatCurrency(String(v.MONT).replace(",", "."))}</td>
                        <td style="font-weight: bold; text-align:center">${v.COMPTE}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATE) }</td>
                        <td class="elerfr" style="font-weight: bold; text-align:center">
                            <div onclick="deleteUser('${v.No}')"><i class="fa fa-tags fa-lg text-danger"></i></div>
                        </td>
                    </tr>`
                });

                $('.traitementORDSEC').empty();
                $('.traitementORDSEC').html(contentpaie);
            }
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
});

$('[data-action="SaveV"]').click(function () {
    let CheckList = $(`[compteg-ischecked]:checked`).closest("tr");
    let list = [];
    $.each(CheckList, (k, v) => {
        list.push($(v).attr("compteG-id"));
    });

    let formData = new FormData();
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDSOCIETE);

    formData.append("listCompte", list);

    formData.append("DateDebut", $('#dateD').val());
    formData.append("DateFin", $('#dateF').val());

    $.ajax({
        type: "POST",
        url: Origin + '/Traitement/GetCheckedEcritureF',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            var Datas = JSON.parse(result);
            alert(Datas.msg);
            $.each(CheckList, (k, v) => {
                list.push($(v).remove());
            });
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
});
$('.Checkall').change(function () {

    if ($('.Checkall').prop("checked") == true) {
       
        $('[compteg-ischecked]').prop("checked", true);
    } else {
        $('[compteg-ischecked]').prop("checked", false);
    }
    
});

$('[data-action="SaveSIIG"]').click(function () {
    let CheckList = $(`[compteg-ischecked]:checked`).closest("tr");
    let list = [];
    $.each(CheckList, (k, v) => {
        list.push($(v).attr("compteG-id"));
    });

    let formData = new FormData();
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDSOCIETE);

    formData.append("listCompte", list);

    //formData.append("DateDebut", $('#dateD').val());
    //formData.append("DateFin", $('#dateF').val());

    $.ajax({
        type: "POST",
        url: Origin + '/Traitement/GetCheckedEcritureORDSEC',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            var Datas = JSON.parse(result);
            alert(Datas.msg);
            $.each(CheckList, (k, v) => {
                list.push($(v).remove());
            });
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
});