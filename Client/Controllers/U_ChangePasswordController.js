var U_ChangePasswordController = function ($scope, $http) {

    $scope.GetUserProfile = function () {

        $http.get('api/User/GetUserProfile').then(function (Respons) {
            if (Respons != null) {
                $scope.CurrUser = Respons.data;
            }
            else {
                alert("not work")
            }
        })
    }
    $scope.GetUserProfile();

    $scope.Edit = function () {
        var jsonObj = {};

        $("#UserData input").each(function () {
            var key = $(this).attr('name');
            var val = $(this).val();
            jsonObj[key] = val;


        });


        var config = {
            params: jsonObj,
            headers: { 'Accept': 'application/json' }
        };

        var User = JSON.stringify(jsonObj);
        jQuery.ajax({
            type: "POST",
            url: "/api/User/Update",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: User,
            success: function (response) {
                if (response == "ok") {

                    window.open('/#/U_Profile', '_self');
                }
                else
                    alert("User already exits");

            }
        });

    }
}
U_ChangePasswordController.$inject = ['$scope', '$http'];