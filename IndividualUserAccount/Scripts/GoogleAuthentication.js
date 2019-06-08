/// <reference path="jquery-3.3.1.js" />
/// <reference path="jquery-3.3.1.min.js" />

function getAccessToken() {

    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {

            }
        }
    }
}

#access_token =
    GcltAem9kmXu4TzqYn_Kc0HwC1DstY & token_type=bearer & expires_in=10 & state=v0oBZgQRKr0Lp5eYNZfOy_SID1GhykANpuwKWEtIa081