var CompanyProfileController = function ($scope, $http, Api) {

    Api.GetApiCall("Company", "GetCompany", function (response) {
        if (response.result != null) {
            $scope.company = response.result;
        }
        else
            alert("Somthing went wrong...");
    });


    $scope.Edit = function (company) {
        Api.PostApiCall("Company", "Update", company, function (response) {
            if (response.result != '') {
                swal({
                    title: "!עודכן בהצלחה",
                    text: "",
                    icon: "success",
                    button: "אישור",
                });
                window.open('#/CompanyProfile', '_self');
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
        Api.postWithFiles("Company", "UploadImg", $scope.formdata, function (response) {
                swal({
                    title: "!עודכן בהצלחה",
                    text: "",
                    icon: "success",
                    button: "אישור",
                    timer:3000,
                });
                $scope.formdata = new FormData();
                $scope.data = [];
                $scope.$apply;
            console.log(response);
        });
    };
}
CompanyProfileController.$inject = ['$scope', '$http', 'Api'];


//$scope.UplodeImg = function (company) {

    //    var Company = JSON.stringify(company);
    //    $scope.formdata.append('company', Company);
    //    Api.postWithFiles("Company", "Update", $scope.formdata, function (response) {
    //        if (response.result != '') {
    //            localStorage.setItem('company', response.result.CompanyName);

    //            swal({
    //                title: "!עודכן בהצלחה",
    //                text: "",
    //                icon: "success",
    //                button: "אישור",
    //            });
    //            window.open('#/CompanyProfile', '_self');
    //        }
    //        else
    //            alert("משהו השתבש, נסה שוב או פנה אלינו...")
    //    });

    //}