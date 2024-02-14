$(document).ready(() => {
    $(`[data-widget="pushmenu"]`).on('click', () => {
        $(`[data-action="SaveSIIG"]`).toggleClass('custom-fixed-btn');
    });
});

let clickedIdDL;

function modalLIAS(id) {

    clickedIdDL = id;

    let formData = new FormData();

    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    formData.append("IdF", clickedIdDL);

    $.ajax({
        type: "POST",
        url: Origin + '/Traitement/ModalLIAS',
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
                    <tr class="select-text">
                        <td style="font-weight: bold; text-align:center"><a href="${v.REF}" target="_blank">${v.REF}</a></td>
                        <td style="font-weight: bold; text-align:center"><a href="${v.OBJ}" target="_blank">${v.OBJ}</a></td>
                        <td style="font-weight: bold; text-align:center"><a href="${v.TITUL}" target="_blank">${v.TITUL}</a></td>
                    </tr>
                    `                });
                $('.DOCMODAL').empty();
                $('.DOCMODAL').html(contentpaie);
            }
        },
        error: function () {
            alert("Problème de connexion. ");
        }
    });

    $('#document-modal').modal('toggle');
}