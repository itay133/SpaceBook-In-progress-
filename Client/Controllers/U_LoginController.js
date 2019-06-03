var U_LoginController = function ($scope, $http, Api, AuthService, $window) {
    //=================Paterns=================//
    $scope.eml_add = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
    $scope.pass_add = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";
    //===============Paterns End===============//

    $scope.Login = function (User) {
        Api.PostApiCall("Login", "U_Login", User, function (response) {
            console.log(response.result);
            if (response.result == null) {
                swal({
                    title: "שם משתמש או סיסמא אינם נכונים",
                    text: "",
                    icon: "error",
                    button: "אישור",
                });
            }
            else {
                // Save user in local storage as string
                $window.localStorage.setItem('user', response.result.FirstName);
                AuthService.authenticate();
                window.open('/#/U_HomePage', '_self');
            }
        });
    }




    //check if there is a session 
    $scope.isAuth = function () {
        $scope.user = localStorage.getItem("user");
        return AuthService.isAuth();
    }

    //logout: kile session
    $scope.Logout = function () {
        Api.GetApiCall('User', 'Logout');
        AuthService.logout();
        window.open('/#/U_HomePage', '_self');
    }
};
U_LoginController.$inject = ['$scope', '$http', 'Api', 'AuthService','$window'];