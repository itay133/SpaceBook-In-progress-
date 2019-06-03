var BusinessAuthService = function ($cookies) {

    var isAuthIndicator = false;

    checkAuth = function () {
        //console.log(isAuthIndicator);
        isAuthIndicator = sessionStorage.getItem('Businesssession') != null;
        console.log(sessionStorage.getItem('Businesssession'));
    }


    this.authenticate = function () {
        sessionStorage.setItem('Businesssession', 'true');
        isAuthIndicator = true;
    }
    this.authenticateFalse = function () {
        sessionStorage.setItem('Businesssession', 'false');
        isAuthIndicator = false;
    }


    this.isAuth = function () {
        //console.log(isAuthIndicator);
        return isAuthIndicator;
    }

    this.logout = function () {
        sessionStorage.removeItem('Businesssession');
        isAuthIndicator = false;
    }

    checkAuth();

}
