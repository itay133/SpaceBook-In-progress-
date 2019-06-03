var RegistrationController = function ($scope, $http, Api) {

    //=================Paterns=================//
    $scope.eml_add = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
    $scope.pass_add = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";
    //===============Paterns End===============//

    $scope.Password = function (company) {
        var password = company.Password;
        var confirmPassword = company.ConfirmPassword;

        if (confirmPassword && password !== confirmPassword) {
            $("#pass-validation").html("הססמאות אינן תואמות");
            document.getElementById("confirm-button").disabled = true;
        }
        else {
            $("#pass-validation").html("");
            document.getElementById("confirm-button").disabled = false;
        }
    };

    $scope.SubmitForm = function (company) {
        if (company != null) {
            var Company = JSON.stringify(company);
            Api.PostApiCall("Registration", "AddCompany", Company, function (response) {
                console.log(response);
                if (response.statusCode == 201) {
                    swal({
                        title: "SPACEBOOK-ברוכים הבאים ל",
                        text: "תודה רבה",
                        icon: "success",
                        button: "אישור",
                        timer: 3000,
                    });
                    window.open('#/BusinessIndex', '_self');
                }
                if (response.statusCode == 409) {
                    swal({
                        title: "פעולה נכשלה",
                        text: "משתמש או חברה קיימים במערכת",
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
    };

};
RegistrationController.$inject = ['$scope', '$http', 'Api'];