let clickedId;

function modalF(id) {
    clickedId = id;

    let formData = new FormData();

    formData.append("suser.LOGIN", User.LOGIN);
    formData.append("suser.PWD", User.PWD);
    formData.append("suser.ROLE", User.ROLE);
    formData.append("suser.IDPROJET", User.IDPROJET);

    formData.append("IdF", clickedId);

    $.ajax({
        type: "POST",
        url: Origin + '/Traitement/ModalF',
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
                        <td style="font-weight: bold; text-align:center">${v.REF}</td>
                        <td style="font-weight: bold; text-align:center">${v.OBJ}</td>
                        <td style="font-weight: bold; text-align:center">${v.TITUL}</td>
                        <td style="font-weight: bold; text-align:center">${formatDate(v.DATE)}</td>
                        <td style="font-weight: bold; text-align:center">${formatCurrency(String(v.MONT).replace(",", "."))}</td>
                    </tr>
                    `                });
                $('.afb160MODAL').empty();
                $('.afb160MODAL').html(contentpaie);
            }
        },
        error: function () {
            alert("Probl�me de connexion. ");
        }
    });

    $('#password-modal').modal('toggle');
}