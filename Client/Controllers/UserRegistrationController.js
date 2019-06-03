//user registration
var UserRegistrationController = function ($scope, Api) {
    //=================Paterns=================//
    $scope.eml_add = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
    $scope.pass_add = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";
    //===============Paterns End===============//

    $scope.Password = function (User) {
        var password = User.Password;
        var confirmPassword = User.ConfirmPassword;

        if (confirmPassword && password !== confirmPassword) {
            $("#pass-validation").html("הססמאות אינן תואמות");
            document.getElementById("confirm-button").disabled == true;
        }
        else {
            $("#pass-validation").html("");
            document.getElementById("confirm-button").disabled == false;
        }
    };


    $scope.Registration = function (User) {
        if (User.Email != null) {
            Api.PostApiCall("Registration", "AddUser", User, function (response) {
                console.log(response);
                if (response.statusCode == 201) {
                    swal({
                        title: "SPACEBOOK-ברוכים הבאים ל",
                        text: "תודה רבה",
                        icon: "success",
                        button: "אישור",
                        timer: 3000,
                    });
                    window.open('/#/HomePage', '_self');
                }
                if (response.statusCode == 409) {
                    swal({
                        title: "פעולה נכשלה",
                        text: "המשתמש קיים במערכת",
                        icon: "error",
                        button: "אישור",
                    });
                }
            });
        }
        else {
            swal({
                title: "נא למלא את כל הטופס",
                text: "פעולה נכשלה",
                icon: "error",
                button: "אישור",
            });
        }
    }
}
UserRegistrationController.$inject = ['$scope', 'Api'];