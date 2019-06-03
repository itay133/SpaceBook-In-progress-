var ChangePasswordController = function ($scope, $http, Api) {

    $scope.Password = function (company) {
        console.log('Pass')
        var password = company.Password;
        var confirmPassword = company.ConfirmPassword;

        if (confirmPassword && password !== confirmPassword)
            $("#pass-validation").html("הססמאות אינן תואמות");

        else
            $("#pass-validation").html("");
    };
    var IsPassValid = function (company) {
        return (company.Password === company.ConfirmPassword);
    };
    $scope.ChangePassword = function (company) {
        if (IsPassValid(company)) {

            var Company = JSON.stringify(company);
            Api.PostApiCall("Company", "ChangePassword", company, function (response) {
                console.log(response);
                if (response.statusCode==200) {
                    swal({
                        title: "!הסיסמא שונתה בהצלחה",
                        text: "תודה רבה",
                        icon: "success",
                        button: "אישור",
                    });
                }
                else {
                    swal({
                        title: "משהו השתבש...",
                        text: "תודה רבה",
                        icon: "error",
                        button: "אישור",
                    });
                }
                window.open('#/CompanyProfile', '_self');
            });
        }
        else {
            swal({
                title: "הסיסמאות לא תואמות",
                text: "תודה רבה",
                icon: "error",
                button: "אישור",
            });
        }
    }

}
CompanyProfileController.$inject = ['$scope', '$http', 'Api'];
