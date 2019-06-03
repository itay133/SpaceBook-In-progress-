var CompanyController = function ($scope, $http, ) {
    //Add listing by form
    $scope.GetWorkSpace = function () {
        $http.get('api/Company/GetWorkSpace').then(function (response) {
            if (response.data != null) {
                $scope.workspace = response.data;
            }
            else
                alert("עליך להתחבר למערכת")
        })
    }
    $scope.GetWorkSpace();
   
    $scope.Delete = function (workspaceID) {
        alert("now I can delete " + workspaceID)
        var config = {
            params: workspaceID,
            headers: { Accept: 'application/json' }
        };

        var WorkSpace = JSON.stringify(workspaceID)

        jQuery.ajax({
            type: "POST",
            url: "/api/Company/DeleteWorkSpace",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: WorkSpace,
            success: function (response) {
                alert("המתחם נוסף בהצלחה");
                if (response.data != null) {
                    $scope.ClearUserData = function () {
                        $("#workSpaceData input").each(function () {
                            $(this).val("");
                        });
                    }
                }


                $scope.Space = response.data;
            }
        });
        $http.post('api/Company/DeleteWorkSpace').then(function (response) {
        })
    }
    $scope.GetCompany = function () {
        $http.get('api/Company/GetCompany').then(function (response) {
            if (response.data != null) {
                $scope.company = response.data;
            }
            else
                alert("עליך להתחבר למערכת")
        })
    }
    //$scope.AddListing = function () {
    //    alert("Hi");
    //    var jsonObj = {};
    //    $("#addListing input").each(function () {
    //        var key = $(this).attr('name');
    //        var val = $(this).val();
    //        alert(val);
    //    });

    //    var config = {
    //        params: jsonObj,
    //        headers: { 'Accept': 'application/json' }
    //    };
    //    $scope.onChange = function (files) {
    //        if (files[0] == undefined) return;
    //        $scope.fileExt = files[0].name.split(".").pop()
    //    }

    //    $scope.isImage = function (ext) {
    //        if (ext) {
    //            return ext == "jpg" || ext == "jpeg" || ext == "gif" || ext == "png"
    //        }
    //    }



    //    //var usersRoles = new Array;
    //    //jQuery("#dualSelectRoles2 option").each(function () {
    //    //    usersRoles.push(jQuery(this).val());
    //    //});
    //    //console.log(usersRoles);
    //    var Company = JSON.stringify(jsonObj);
    //    jQuery.ajax({
    //        type: "POST",
    //        url: "/api/Company/AddListing",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        data: Company,
    //        success: function (data) { alert(data); },
    //        failure: function (errMsg) {
    //            alert(errMsg);
    //        }
    //    });

    //}
}
CompanyController.$inject = ['$scope', '$http'];
