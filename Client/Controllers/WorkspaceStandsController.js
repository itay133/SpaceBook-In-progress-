var WorkspaceStandsController = function ($scope, $http, $location, $window, Api, $uibModal, $log) {
    $scope.Stand = {};
    $scope.StandAmount = [];
    var qs = $location.search();

    //This Api will give all stand types collection inorder to prevent users mistake
    Api.GetApiCall("Company", "GetStandsType", function (response) {
        $scope.StandTypes = response.result;
    });


    for (var i = 1; i < 21; i++)
        $scope.StandAmount.push(i);

    $scope.getKey = function (type) {
        //return $scope.StandTypes[type];
        return Object.keys(type)[0];
    }

    //This method used for Sending a request to the server in order to add Stand into Workspace collection
    $scope.AddStand = function (Stand) {
        console.log(Stand);
        $scope.Stand.WS_ID = qs.id;
        var Stand = JSON.stringify($scope.Stand);
        $scope.formdata.append('stand', Stand);

        Api.postWithFiles("Company", "AddStand", $scope.formdata, function (response) {
            //need to consider what the post method will return upter adding stand to db
            if (response != null) {
                swal({
                    title: "!עמדה נוספה בהצלחה",
                    text: "תודה רבה",
                    icon: "success",
                    button: "אישור",
                });
                window.open('#/HomePage', '_self');
            }
            else {
                swal({
                    title: "!הפעולה נכשלה",
                    text: "משהו לא קשורה התרחש במערכת",
                    icon: "error",
                    button: "אישור",
                });

            }
        })
    }

    //===================File-Uploading============================
    $scope.uploading = false;
    $scope.countFiles = '';
    $scope.data = []; //For displaying file name on browser

    $scope.getFiles = function (file) {
        console.log("GetFile");
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



}
WorkspaceStandsController.$inject = ['$scope', '$http', '$location', '$window', 'Api', '$uibModal', '$log'];

