var login = function () {
    var userName = $('#inputUserName').val();
    var password = $('#inputPassword').val();
    var rememberMeValue = $("input[name='inputRememberPassword']").prop("checked");

    if (userName.replace(/\s+/g, '') == '') {
        Swal.fire({
            icon: 'error',
            title: 'Hata...',
            text: 'Lütfen Kullanıcı Adı Giriniz!'
        });
        return;
    }
    if (password.replace(/\s+/g, '') == '') {
        Swal.fire({
            icon: 'error',
            title: 'Hata...',
            text: 'Lütfen Şifrenizi Giriniz!'
        });
        return;
    }
    var url = '/api/AccountApi/Login'
    var mdl = { UserName: userName, Password: password, RememberMe: rememberMeValue }
    $.post(url, mdl, function (d) {
        console.log(d)
        if (d.isSuccess) {
            window.location.href = '/Building/Index'
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Hata...',
                text: 'Kullanıcı adı veya şifre yanlış'
            });
        }
    })
}