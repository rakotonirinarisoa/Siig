let User;
let Origin;

$(document).ready(() => {
    User = JSON.parse(sessionStorage.getItem("user"));
    if (User == null || User === "undefined") window.location = "../";
    Origin = User.origin;

    $(`[data-id="username"]`).text(User.LOGIN);

    GetListProjet();
    GetUsers(undefined);
    GetListMANDATP();
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
    const idPROJET = $('#proj').val();

    let formData = new FormData();
    //alert(baseName);
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDSOCIETE);

    formData.append("IdPROJET", idPROJET);

    $.ajax({
        type: "POST",
        url: Origin + '/Etat/EtatMandatChange',
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
                        <td style="font-weight: bold; text-align:center">${v.REF}</td>
                        <td style="font-weight: bold; text-align:center">${v.OBJ}</td>
                        <td style="font-weight: bold; text-align:center">${v.TITUL}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATE)}</td>
                        <td style="font-weight: bold; text-align:center">${v.COMPTE}</td>
                        <td style="font-weight: bold; text-align:center">${v.PCOP}</td>
                        <td style="font-weight: bold; text-align:center">${formatCurrency(String(v.MONT).replace(",", "."))}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATEDEF)}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATETEF)}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATEBE)}</td>
                        <td style="font-weight: bold; text-align:center">${v.STAT}</td>
                    `
                    if (v.STAT == "Attente validation") {
                        contentpaie += `<td class="elerfr" style="font-weight: bold; text-align:center">
                                            <div onclick="deleteUser('${v.No}')"><i class="fa fa-times fa-lg text-danger"></i></div>
                                        </td >
                                    </tr >`
                    }
                    else {
                        contentpaie += `<td class="elerfr" style="font-weight: bold; text-align:center">
                                        </td >
                                    </tr >`
                    }
                });

                $('.traitementPROJET').empty();
                $('.traitementPROJET').html(contentpaie);
            }
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
});

//GET LISTE MANDAT PROJET//
function GetListMANDATP() {
    let formData = new FormData();
    //alert(baseName);
    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDSOCIETE);

    const id = $('#proj').val();

    $.ajax({
        type: "POST",
        url: Origin + '/Etat/EtatMandatProjet',
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
            /*<td style="font-weight: bold; text-align:center">${Intl.NumberFormat().format(String(v.MONT).replace(",", "."))}</td>*/

            if (Datas.type == "success") {
                //window.location = window.location.origin;
                ListResult = Datas.data
                contentpaie = ``;
                $.each(ListResult, function (k, v) {
                    contentpaie += `
                    <tr compteG-id="${v.No}" class="select-text">
                        <td style="font-weight: bold; text-align:center">${v.REF}</td>
                        <td style="font-weight: bold; text-align:center">${v.OBJ}</td>
                        <td style="font-weight: bold; text-align:center">${v.TITUL}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATE)}</td>
                        <td style="font-weight: bold; text-align:center">${v.COMPTE}</td>
                        <td style="font-weight: bold; text-align:center">${v.PCOP}</td>
                        <td style="font-weight: bold; text-align:center">${formatCurrency(String(v.MONT).replace(",", "."))}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATEDEF)}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATETEF)}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATEBE)}</td>
                        <td style="font-weight: bold; text-align:center">${v.STAT}</td>
                        
                    `
                    if (v.STAT == "Attente validation") {
                        contentpaie += `<td class="elerfr" style="font-weight: bold; text-align:center">
                                            <div onclick="deleteUser('${v.No}')"><i class="fa fa-times fa-lg text-danger"></i></div>
                                        </td >
                                    </tr >`
                    }
                    else {
                        contentpaie += `<td class="elerfr" style="font-weight: bold; text-align:center">
                                        </td >
                                    </tr >`
                    }
                });

                $('.traitementPROJET').empty();
                $('.traitementPROJET').html(contentpaie);
            }
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
}

//FILTRE PROJET//
$('[data-action="SearchPROJET"]').click(function () {
    let dd = $("#dateD").val();
    let df = $("#dateF").val();
    if (!dd || !df) {
        alert("Veuillez renseigner les dates afin de filtrer les mandats. ");
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
    formData.append("STAT", $('#stat').val());

    const id = $('#proj').val();

    $.ajax({
        type: "POST",
        url: Origin + '/Etat/EtatMandatProjetSEARCH',
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
                        <td style="font-weight: bold; text-align:center">${v.REF}</td>
                        <td style="font-weight: bold; text-align:center">${v.OBJ}</td>
                        <td style="font-weight: bold; text-align:center">${v.TITUL}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATE)}</td>
                        <td style="font-weight: bold; text-align:center">${v.COMPTE}</td>
                        <td style="font-weight: bold; text-align:center">${v.PCOP}</td>
                        <td style="font-weight: bold; text-align:center">${formatCurrency(String(v.MONT).replace(",", "."))}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATEDEF)}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATETEF)}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATEBE)}</td>
                        <td style="font-weight: bold; text-align:center">${v.STAT}</td>
                    `
                    if (v.STAT == "Attente validation") {
                        contentpaie += `<td class="elerfr" style="font-weight: bold; text-align:center">
                                            <div onclick="deleteUser('${v.No}')"><i class="fa fa-times fa-lg text-danger"></i></div>
                                        </td >
                                    </tr >`
                    }
                    else {
                        contentpaie += `<td class="elerfr" style="font-weight: bold; text-align:center">
                                        </td >
                                    </tr >`
                    }
                });

                $('.traitementPROJET').empty();
                $('.traitementPROJET').html(contentpaie);
            }
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
});

function deleteUser(id) {
    if (!confirm("Etes-vous sûr de vouloir annuler cette ligne ?")) return;
    let formData = new FormData();

    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    formData.append("UserId", id);

    $.ajax({
        type: "POST",
        url: Origin + '/Etat/DeleteUser',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            var Datas = JSON.parse(result);
            console.log(Datas);

            if (Datas.type == "success") {
                alert(Datas.msg);

                $('.traitementPROJET').empty();
                $('.traitementPROJET').html(contentpaie);

                GetListProjet();
                GetUsers(undefined);
                GetListMANDATP();

                return;
            }
            else {
                alert(Datas.msg);
                return;
            }
        },
        error: function () {
            alert("Connexion Problems");
        }
    });
}