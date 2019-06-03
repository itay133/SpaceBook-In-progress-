var AddWorkSpaceController = function ($scope, $http, $log, Api, $mdpTimePicker) {

    $scope.totalItems = 64;
    $scope.currentPage = 4;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };


    $scope.maxSize = 5;
    $scope.bigTotalItems = 175;
    $scope.bigCurrentPage = 1;
    //=================Paterns=================//
    $scope.ph_numbr = /^\+?\d{10}$/;

    $scope.eml_add = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
    //===============Paterns End===============//

    //Creating a list that will contain working working hours inside the Workspace obj
    var workingHours = [];
    var Days = ["ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת"];
    for (var i = 0; i < 7; i++) {
        workingHours.push(
            {
                "Day": i,
                "D_Name": Days[i],
                "OpenTime": new Date(),
                "CloseTime": new Date(),
                "Selected": false
            }
        );
    }

    $scope.WorkSpace = {
        "WorkingHours": workingHours,

    }
    //=====================Time Picker=====================//

    $scope.time = new Date();

    // Optional message to display below each input field
    $scope.message = {
        hour: 'Hour is required',
        minute: 'Minute is required',
        meridiem: 'Meridiem is required'
    }

    $scope.readonly = false;

    $scope.required = true;
    //===================Time Picker End===================//

    $scope.AddWorkSpace = function (WorkSpace) {

        var NewWorkSpace = JSON.stringify(WorkSpace);
        //$scope.formdata.delete('workspace'); should clear the the data
        $scope.formdata.append('workspace', NewWorkSpace);
        Api.postWithFiles("Company", "AddWorkSpace", $scope.formdata, function (response) {
            if (response.statusCode == 200) {
                swal({
                    title: "!מתחם חדש נוסף בהצלחה",
                    text: "תודה רבה",
                    icon: "success",
                    button: "אישור",
                });
                $scope.Space = response.result;
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
        uploadService.uploadFiles($scope)
            // then() called when uploadFiles gets back
            .then(function (data) {
                // promise fulfilled
                $scope.uploading = false;
                if (data === '') {
                    alert("Done!!!")
                    $scope.formdata = new FormData();
                    $scope.data = [];
                    $scope.countFiles = '';
                    $scope.$apply;
                } else {
                    //Server Error
                    alert("Shit, What happended up there!!! " + data);
                }
            }, function (error) {
                $scope.uploading = false;
                //Server Error
                alert("Shit, What happended up there!!! " + error);
            }

            );
    };

}
AddWorkSpaceController.$inject = ['$scope', '$http', '$log', 'Api'];

