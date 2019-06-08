/// <reference path="jquery-3.3.1.js" />
/// <reference path="jquery-3.3.1.min.js" />

function getAccessToken() {
    alert();
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistered(accessToken);
            }
        }
    }
};

function isUserRegistered(accessToken) {

    $.ajax({
        url: 'api/Account/UserInfo',
        method: 'GET',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            if (response.hasRegistered) {
                alert('registerd');
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('userName', response.Email);
                window.location.href = 'data.html';
            }
            else {
                alert('not registerd');
                signupExternalUser(accessToken);
            }
        }

    });
}

function signupExternalUser(accessToken) {
    alert('came to sign up');
    $.ajax({
        url: 'api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            window.location.href = "http://localhost:61541/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A61541%2Fhtml%2FLogin.html&state=v0oBZgQRKr0Lp5eYNZfOy_SID1GhykANpuwKWEtIa081";
        }

    });
}