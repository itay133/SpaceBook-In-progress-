var OrdersByWSController = function ($scope, Api) {

    Api.GetApiCall("Company", "GetWorkSpace", function (response) {
        $scope.Workspace = response.result;

    }

}
OrdersByWSController.$inject = ['$scope', 'Api'];