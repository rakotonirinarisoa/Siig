let clickedId;

const loginNameInput = $('#login-name');
const passwordInput = $('#password');

const p = $('#user-password');

const closeButton = $('#close-btn');

function showPassword(id) {
    clickedId = id;

    loginNameInput.val('');
    passwordInput.val('');

    p.text('');

    closeButton.text('Annuler');

    $('#password-modal').modal('toggle');
}

$('#get-user-password-btn').on('click', () => {
    const payload = {
        loginName: loginNameInput.val(),
        password: passwordInput.val(),
        userId: clickedId
    };

    $.ajax({
        type: 'POST',
        async: true,
        url: Origin + 'user/password',
        contentType: 'application/json',
        datatype: 'json',
        data: JSON.stringify({ ...payload }),
        success: function (result) {
            const res = JSON.parse(result);

            if (res.type === 'error') {
                p.css({ 'color': 'red' });
                p.text('Identifiants incorrects!');
            } else {
                p.css({ 'color': 'black' });
                p.text(`${res.data.login}: ${res.data.password}`);

                closeButton.text('Fermer');
            }
        },
        Error: function (_, e) {
            alert(e);
        }
    });
});