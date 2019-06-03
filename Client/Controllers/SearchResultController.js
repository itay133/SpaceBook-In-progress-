var SearchResultController = function ($scope, $http, sharedService) {

    $scope.currentPage = 1;
    $scope.itemsperpage = 2;



    $scope.LodeWorkSpaceList = function () {
        $scope.WorkspaceList = sharedService.GetWorkSpacelist();
        var items = $scope.WorkspaceList.length;
        $scope.resultitems = $scope.WorkspaceList.length;
        $scope.data = {
            workspaces: {
                totalitems: items*5,
                List: []
            }
        };

        if ($scope.WorkspaceList.length == 0) {
            swal("לא נמצאו חללי עבודה משותפים במיקום הרצוי");
            window.open('/#/HomePage', '_self');
        }


        $scope.$watch('currentPage + itemsperpage', function () {
            $scope.begin = (($scope.currentPage - 1) * $scope.itemsperpage);
            $scope.end = $scope.begin + $scope.itemsperpage;
            $scope.data.workspaces.List = $scope.WorkspaceList.slice($scope.begin, $scope.end);
        });
    }
    $scope.LodeWorkSpaceList();


    //enter to the chosen workspace

    $scope.SelectWorkSpace = function (workspace) {
        // Save WorkSpace in local storage as string
        localStorage.setItem('WorkSpaceLS', JSON.stringify(workspace));
        sharedService.SetWorkSpace(workspace);
        window.open('/#/WorkSpace', '_self');
    }



}
SearchResultController.$inject = ['$scope', '$http', 'sharedService'];




















    //$scope.currentPage = 1;
    //$scope.itemsperpage = 10;
    //$scope.data = {
    //    workspaces: {
    //        totalitems: 100,
    //        List: []
    //    }
    //};

    // $scope.GetWorkSpace = function (text) {
    //     alert(text);
    //    var jsonObj = {};
    //    //jsonObj["searchText"] = $scope.testVal;
    //    jsonObj["searchText"] = text;
    //    var config = {
    //        params: jsonObj,
    //        headers: { 'Accept': 'application/json' }
    //    };

    //    $http.get("api/SearchResult/Search", config).then(function (response) {
    //        if (response.data != null) {
    //            $scope.WorkspaceList = response.data;
    //            $scope.Total = response.data.length;
    //        }
    //        else
    //            alert("לא קיים במערכת")

    //        $scope.$watch('currentPage + itemsperpage', function () {
    //            $scope.begin = (($scope.currentPage - 1) * $scope.itemsperpage);
    //            $scope.end = $scope.begin + $scope.itemsperpage;
    //            $scope.data.workspaces.List = $scope.WorkspaceList.slice($scope.begin, $scope.end);
    //        });

    //    })
    //}
    //$scope.GetWorkSpace();

















    //$scope.watch = function () {
    //    $scope.$watch('currentPage + numPerPage', function () {

    //        var begin = (($scope.currentPage - 1) * $scope.numPerPage)
    //            , end = begin + $scope.numPerPage;


    //        $scope.filteredTodos = workspace.slice(begin, end);

    //    });
    //}

    // $scope.numPages = function () {
    //     //alert($scope.filteredTodos.length);
    //     return Math.ceil($scope.filteredTodos.length / $scope.numPerPage);
    //};


    //$scope.setPage = function (pageNo) {
    //    $scope.currentPage = pageNo;
    //};

    //$scope.pageChanged = function () {
    //    //$log.log('Page changed to: ' + $scope.currentPage);
    //};

