﻿let User;
let Origin;

$(document).ready(() => {
    User = JSON.parse(sessionStorage.getItem("user"));
    if (User == null || User === "undefined") window.location = "../";
    Origin = User.origin;

    $(`[data-id="username"]`).text(User.LOGIN);
    GetListUser();

    $("#idTable").DataTable()
});

function GetListUser() {
    let formData = new FormData();

    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    $.ajax({
        type: "POST",
        url: Origin + '/Privilege/FillTable',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            var Datas = JSON.parse(result);
            console.log(Datas);

            if (Datas.type == "error") {
                alert("eeee" + Datas.msg);
                return;
            }
            if (Datas.type == "login") {
                alert(Datas.msg);
                window.location = window.location.origin;
                return;
            }

            $(`[data-id="ubody"]`).text("");



            var code = ``;
            $.each(Datas.data, function (k, v) {

                let MENUPAR1N = "", MENUPAR1R = "", MENUPAR1A = "";
                if (v.MENUPAR1 == 0) MENUPAR1N = "checked";
                if (v.MENUPAR1 == 1) MENUPAR1R = "checked";
                if (v.MENUPAR1 == 2) MENUPAR1A = "checked";

                let MENUPAR2N = "", MENUPAR2R = "", MENUPAR2A = "";
                if (v.MENUPAR2 == 0) MENUPAR2N = "checked";
                if (v.MENUPAR2 == 1) MENUPAR2R = "checked";
                if (v.MENUPAR2 == 2) MENUPAR2A = "checked";

                let MENUPAR3N = "", MENUPAR3R = "", MENUPAR3A = "";
                if (v.MENUPAR3 == 0) MENUPAR3N = "checked";
                if (v.MENUPAR3 == 1) MENUPAR3R = "checked";
                if (v.MENUPAR3 == 2) MENUPAR3A = "checked";

                let MENUPAR4N = "", MENUPAR4R = "", MENUPAR4A = "";
                if (v.MENUPAR4 == 0) MENUPAR4N = "checked";
                if (v.MENUPAR4 == 1) MENUPAR4R = "checked";
                if (v.MENUPAR4 == 2) MENUPAR4A = "checked";

                let MENUPAR5N = "", MENUPAR5R = "", MENUPAR5A = "";
                if (v.MENUPAR5 == 0) MENUPAR5N = "checked";
                if (v.MENUPAR5 == 1) MENUPAR5R = "checked";
                if (v.MENUPAR5 == 2) MENUPAR5A = "checked";

                let MENUPAR6N = "", MENUPAR6R = "", MENUPAR6A = "";
                if (v.MENUPAR6 == 0) MENUPAR6N = "checked";
                if (v.MENUPAR6 == 1) MENUPAR6R = "checked";
                if (v.MENUPAR6 == 2) MENUPAR6A = "checked";

                let MENUPAR7N = "", MENUPAR7R = "", MENUPAR7A = "";
                if (v.MENUPAR7 == 0) MENUPAR7N = "checked";
                if (v.MENUPAR7 == 1) MENUPAR7R = "checked";
                if (v.MENUPAR7 == 2) MENUPAR7A = "checked";

                let MENUPAR8N = "", MENUPAR8R = "", MENUPAR8A = "";
                if (v.MENUPAR8 == 0) MENUPAR8N = "checked";
                if (v.MENUPAR8 == 1) MENUPAR8R = "checked";
                if (v.MENUPAR8 == 2) MENUPAR8A = "checked";

                let MT0N = "", MT0R = "", MT0A = "";
                if (v.MT0 == 0) MT0N = "checked";
                if (v.MT0 == 1) MT0R = "checked";
                if (v.MT0 == 2) MT0A = "checked";

                let MT1N = "", MT1R = "", MT1A = "";
                if (v.MT1 == 0) MT1N = "checked";
                if (v.MT1 == 1) MT1R = "checked";
                if (v.MT1 == 2) MT1A = "checked";

                let MT2N = "", MT2R = "", MT2A = "";
                if (v.MT2 == 0) MT2N = "checked";
                if (v.MT2 == 1) MT2R = "checked";
                if (v.MT2 == 2) MT2A = "checked";

                let MP1N = "", MP1R = "", MP1A = "";
                if (v.MP1 == 0) MP1N = "checked";
                if (v.MP1 == 1) MP1R = "checked";
                if (v.MP1 == 2) MP1A = "checked";

                let MP2N = "", MP2R = "", MP2A = "";
                if (v.MP2 == 0) MP2N = "checked";
                if (v.MP2 == 1) MP2R = "checked";
                if (v.MP2 == 2) MP2A = "checked";

                let MP3N = "", MP3R = "", MP3A = "";
                if (v.MP3 == 0) MP3N = "checked";
                if (v.MP3 == 1) MP3R = "checked";
                if (v.MP3 == 2) MP3A = "checked";

                let MP4N = "", MP4R = "", MP4A = "";
                if (v.MP4 == 0) MP4N = "checked";
                if (v.MP4 == 1) MP4R = "checked";
                if (v.MP4 == 2) MP4A = "checked";

                let TDB0N = "", TDB0R = "", TDB0A = "";
                if (v.TDB0 == 0) TDB0N = "checked";
                if (v.TDB0 == 1) TDB0R = "checked";
                if (v.TDB0 == 2) TDB0A = "checked";

                let GEDN = "", GEDR = "", GEDA = "";
                if (v.GED == 0) GEDN = "checked";
                if (v.GED == 1) GEDR = "checked";
                if (v.GED == 2) GEDA = "checked";

                code += `
                    <tr data-userId="${v.ID}" class="text-nowrap last-hover">
                        <td>${v.PROJET}</td>
                        <td>${v.LOGIN}</td>
                        <td>${v.ROLE}</td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMENUPAR1" name="droneMENUPAR1" value="0" ${MENUPAR1N}/><label class="ml-1" for="noneMENUPAR1" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMENUPAR1" name="droneMENUPAR1" value="1" ${MENUPAR1R}/><label class="ml-1" for="readMENUPAR1" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMENUPAR1" name="droneMENUPAR1" value="2" ${MENUPAR1A}/><label class="ml-1" for="writeMENUPAR1" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMENUPAR2" name="droneMENUPAR2" value="0" ${MENUPAR2N}/><label class="ml-1" for="noneMENUPAR2" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMENUPAR2" name="droneMENUPAR2" value="1" ${MENUPAR2R}/><label class="ml-1" for="readMENUPAR2" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMENUPAR2" name="droneMENUPAR2" value="2" ${MENUPAR2A}/><label class="ml-1" for="writeMENUPAR2" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMENUPAR3" name="droneMENUPAR3" value="0" ${MENUPAR3N}/><label class="ml-1" for="noneMENUPAR3" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMENUPAR3" name="droneMENUPAR3" value="1" ${MENUPAR3R}/><label class="ml-1" for="readMENUPAR3" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMENUPAR3" name="droneMENUPAR3" value="2" ${MENUPAR3A}/><label class="ml-1" for="writeMENUPAR3" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMENUPAR4" name="droneMENUPAR4" value="0" ${MENUPAR4N}/><label class="ml-1" for="noneMENUPAR4" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMENUPAR4" name="droneMENUPAR4" value="1" ${MENUPAR4R}/><label class="ml-1" for="readMENUPAR4" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMENUPAR4" name="droneMENUPAR4" value="2" ${MENUPAR4A}/><label class="ml-1" for="writeMENUPAR4" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMENUPAR5" name="droneMENUPAR5" value="0" ${MENUPAR5N}/><label class="ml-1" for="noneMENUPAR5" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMENUPAR5" name="droneMENUPAR5" value="1" ${MENUPAR5R}/><label class="ml-1" for="readMENUPAR5" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMENUPAR5" name="droneMENUPAR5" value="2" ${MENUPAR5A}/><label class="ml-1" for="writeMENUPAR5" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMENUPAR6" name="droneMENUPAR6" value="0" ${MENUPAR6N}/><label class="ml-1" for="noneMENUPAR6" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMENUPAR6" name="droneMENUPAR6" value="1" ${MENUPAR6R}/><label class="ml-1" for="readMENUPAR6" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMENUPAR6" name="droneMENUPAR6" value="2" ${MENUPAR6A}/><label class="ml-1" for="writeMENUPAR6" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMENUPAR7" name="droneMENUPAR7" value="0" ${MENUPAR7N}/><label class="ml-1" for="noneMENUPAR7" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMENUPAR7" name="droneMENUPAR7" value="1" ${MENUPAR7R}/><label class="ml-1" for="readMENUPAR7" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMENUPAR7" name="droneMENUPAR7" value="2" ${MENUPAR7A}/><label class="ml-1" for="writeMENUPAR7" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMENUPAR8" name="droneMENUPAR8" value="0" ${MENUPAR8N}/><label class="ml-1" for="noneMENUPAR8" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMENUPAR8" name="droneMENUPAR8" value="1" ${MENUPAR8R}/><label class="ml-1" for="readMENUPAR8" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMENUPAR8" name="droneMENUPAR8" value="2" ${MENUPAR8A}/><label class="ml-1" for="writeMENUPAR8" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMT0" name="droneMT0" value="0" ${MT0N}/><label class="ml-1" for="noneMT0" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMT0" name="droneMT0" value="1" ${MT0R}/><label class="ml-1" for="readMT0" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMT0" name="droneMT0" value="2" ${MT0A}/><label class="ml-1" for="writeMT0" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMT1" name="droneMT1" value="0" ${MT1N}/><label class="ml-1" for="noneMT1" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMT1" name="droneMT1" value="1" ${MT1R}/><label class="ml-1" for="readMT1" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMT1" name="droneMT1" value="2" ${MT1A}/><label class="ml-1" for="writeMT1" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMT2" name="droneMT2" value="0" ${MT2N}/><label class="ml-1" for="noneMT2" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMT2" name="droneMT2" value="1" ${MT2R}/><label class="ml-1" for="readMT2" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMT2" name="droneMT2" value="2" ${MT2A}/><label class="ml-1" for="writeMT2" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMP1" name="droneMP1" value="0" ${MP1N}/><label class="ml-1" for="noneMP1" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMP1" name="droneMP1" value="1" ${MP1R}/><label class="ml-1" for="readMP1" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMP1" name="droneMP1" value="2" ${MP1A}/><label class="ml-1" for="writeMP1" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMP2" name="droneMP2" value="0" ${MP2N}/><label class="ml-1" for="noneMP2" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMP2" name="droneMP2" value="1" ${MP2R}/><label class="ml-1" for="readMP2" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMP2" name="droneMP2" value="2" ${MP2A}/><label class="ml-1" for="writeMP2" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMP3" name="droneMP3" value="0" ${MP3N}/><label class="ml-1" for="noneMP3" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMP3" name="droneMP3" value="1" ${MP3R}/><label class="ml-1" for="readMP3" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMP3" name="droneMP3" value="2" ${MP3A}/><label class="ml-1" for="writeMP3" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneMP4" name="droneMP4" value="0" ${MP4N}/><label class="ml-1" for="noneMP4" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readMP4" name="droneMP4" value="1" ${MP4R}/><label class="ml-1" for="readMP4" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeMP4" name="droneMP4" value="2" ${MP4A}/><label class="ml-1" for="writeMP4" style="font-weight:normal">All</label>
                            </div>
                        </td>

                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneTDB0" name="droneTDB0" value="0" ${TDB0N}/><label class="ml-1" for="noneTDB0" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readTDB0" name="droneTDB0" value="1" ${TDB0R}/><label class="ml-1" for="readTDB0" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeTDB0" name="droneTDB0" value="2" ${TDB0A}/><label class="ml-1" for="writeTDB0" style="font-weight:normal">All</label>
                            </div>
                        </td>
                        
                        <td text-align:center>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="noneGED" name="droneGED" value="0" ${GEDN}/><label class="ml-1" for="noneGED" style="font-weight:normal">None</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="readGED" name="droneGED" value="1" ${GEDR}/><label class="ml-1" for="readGED" style="font-weight:normal">Read</label>
                            </div></br>
                            <div class="form-check form-check-inline">
                                <input type="radio" id="writeGED" name="droneGED" value="2" ${GEDA}/><label class="ml-1" for="writeGED" style="font-weight:normal">All</label>
                            </div>
                        </td>
                        
                        <td class="elerfr" style="font-weight: bold; text-align:center">
                            <div onclick="SavePRIV('${v.ID}')"><i class="fa fa-save fa-lg text-danger"></i></div>
                        </td>
                    </tr >`;
            });

            $(`[data-id="ubody"]`).append(code);
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });
}

function SavePRIV(id) {
    if (!confirm("Etes-vous sûr de vouloir supprimer l'utilisateur ?")) return;
    let formData = new FormData();

    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    formData.append("UserId", id);

    $.ajax({
        type: "POST",
        url: Origin + '/User/DeleteUser',
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
                alert(Datas.msg);
                $(`[data-userId="${id}"]`).remove();
                return;
            }
        },
        error: function () {
            alert("Connexion Problems");
        }
    });
}



