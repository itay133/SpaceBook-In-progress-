var U_ProfileController = function ($scope, $http, Api, $log) {

    $scope.GetUserProfile = function () {

        $http.get('api/User/GetUserProfile').then(function (Respons) {
            if (Respons != null) {
                $scope.CurrUser = Respons.data;
                console.log(Respons);
            }
            else {
                alert("not work")
            }
        })
    }
    $scope.GetUserProfile();

    $scope.Edit = function (CurrUser) {
        Api.PostApiCall("User", "Update", CurrUser, function (response) {
            if (response.result != '') {
                localStorage.setItem('user', response.result.FirstName);
                swal({
                    title: "!עודכן בהצלחה",
                    text: "",
                    icon: "success",
                    button: "אישור",
                });
                window.open('/#/U_Profile', '_self');
            }
            else
                alert("משהו השתבש, נסה שוב או פנה אלינו...")
        });

    }

    //===================File-Uploading============================
    $scope.uploading = false;
    $scope.countFiles = '';
    $scope.data = []; //For displaying file name on browser

    $scope.getFiles = function (file) {
        $scope.formdata = new FormData();
        angular.forEach(file, function (value, key) {
            $scope.formdata.append(key, value);
            $scope.data.push({ FileName: value.name, FileLength: value.size });
            console.log(value.name);
            console.log(key);
        }
        );
        //This line is just show you there is possible to
        //send in extra parameter to server.


        $scope.countFiles = $scope.data.length == 0 ? '' : $scope.data.length + ' files selected';
        $scope.$apply();
        $scope.formdata.append('countFiles', $scope.countFiles);
        console.log(JSON.stringify($scope.formdata));

    };

    $scope.uploadFiles = function () {
        $scope.uploading = true;
        Api.postWithFiles("User", "ImgUpdate", $scope.formdata, function (response) {
            swal({
                title: "!עודכן בהצלחה",
                text: "",
                icon: "success",
                button: "אישור",
                timer: 3000,
            });
            $scope.formdata = new FormData();
            $scope.data = [];
            $scope.$apply;
            console.log(response);
        });
   
    };


}
U_ProfileController.$inject = ['$scope', '$http', 'Api', '$log'];
//this is what we have usend to upload files
//http://instinctcoder.com/angularjs-upload-multiple-files-to-asp-net-web-api/