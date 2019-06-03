var AuthService = function ($cookies) {

    var isAuthIndicator = false;

    checkAuth = function () {
        //console.log(isAuthIndicator);
        isAuthIndicator = sessionStorage.getItem('session') != null;
        console.log(sessionStorage.getItem('session'));
    }


    this.authenticate = function (user) {
        sessionStorage.setItem('session', 'true');
        isAuthIndicator = true;
    }
    this.authenticateFalse = function () {
        sessionStorage.setItem('session', 'false');
        isAuthIndicator = false;
    }


    this.isAuth = function () {
        //console.log(isAuthIndicator);
        return isAuthIndicator;
    }

    this.logout = function () {
        sessionStorage.removeItem('session');
        sessionStorage.removeItem('Cart');
        sessionStorage.removeItem('user');
        isAuthIndicator = false;
    }

    checkAuth();

}
