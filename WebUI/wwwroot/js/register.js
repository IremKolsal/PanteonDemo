var register = function () {
    var userName = $('#inputUserName').val();
    var password = $('#inputPassword').val();
    var email = $('#inputEmail').val();

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
    if (email.replace(/\s+/g, '') == '') {
        Swal.fire({
            icon: 'error',
            title: 'Hata...',
            text: 'Lütfen Email Adresinizi Giriniz!'
        });
        return;
    }
    var url = '/api/AccountApi/Register'
    var mdl = { UserName: userName, Password: password, Email: email }
    $.post(url, mdl, function (d) {
        console.log(d)
        if (d.isSuccess) {
            Swal.fire({
                icon: 'success',
                title: 'Başarılı...',
                text: d.message
            });
            setTimeout(function () { window.location.href = '/Account/Login' }, 5000);

        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Hata...',
                text: d.message
            });
        }
    })
}