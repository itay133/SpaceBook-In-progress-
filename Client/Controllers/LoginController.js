var LoginController = function ($scope, Api, BusinessAuthService, $window) {

    //=================Paterns=================//
    $scope.eml_add = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
    $scope.pass_add = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";
    //===============Paterns End===============//

    
    $scope.Login = function (Company) {
        Api.PostApiCall("Login", "Login", Company, function (response) {
            console.log(response);
            if (response.result == null) {
                swal({
                    title: "שם משתמש או סיסמא אינם נכונים",
                    text: "",
                    icon: "error",
                    button: "אישור",
                });
            } else {
                // Save user in local storage as string
                $window.localStorage.setItem('company', response.result.CompanyName); 
                BusinessAuthService.authenticate();
                window.open('#/HomePage', '_self');
            }
        });

    }; 

    //check if there is a session 
    $scope.isAuth = function () {
        $scope.company = localStorage.getItem("company");
        return BusinessAuthService.isAuth();
    } 
  

    //logout: kile session
    $scope.Logout = function () {
        BusinessAuthService.logout();
        $window.localStorage.setItem('company', null);
        window.open('#/HomePage', '_self');
    }

};

LoginController.$inject = ['$scope', 'Api', 'BusinessAuthService','$window'];