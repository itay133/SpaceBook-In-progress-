var CompanyWorkspacesController = function ($scope, $http, $location, $window, Api) {

    $scope.currentPage = 1;
    $scope.itemsperpage = 20;
    $scope.data = {
        workspaces: {
            totalitems: 10,
            List: []
        }
    };

    function GetData() {

    }
    GetData();

    $scope.pageChanged = function () {
        GetData();
    };

    Api.GetApiCall("Company", "GetWorkSpace", function (response) {
        $scope.WorkspaceList = response.result;
        $scope.Img = response.result.SpacesImages;
        console.log(response);
        //$scope.data.workspaces.List = $scope.WorkspaceList;
        $scope.$watch('currentPage + itemsperpage', function () {
            $scope.begin = (($scope.currentPage - 1) * $scope.itemsperpage);
            $scope.end = $scope.begin + $scope.itemsperpage;

            $scope.data.workspaces.List = $scope.WorkspaceList.slice($scope.begin, $scope.end);
        });
    });


};
CompanyWorkspacesController.$inject = ['$scope', '$http', '$location', '$window', 'Api'];

