var AddStandController = function ($scope, $http) {


    $http.get('api/Company/GetWorkSpace', config).then(function (response) {
        $scope.workSpace = response.data;
        alert("HTTP");
    })

    //$scope.GetWorkSpace();
    //var obj = [];
    //var config = {
    //    params: obj,
    //    headers: { 'Accept': 'application/json' }
    //};
    //$scope.GetWorkSpace = function () {
    //    alert("Hi///");
    //    $http.get('api/Company/GetWorkSpace', config).then(function (response) {
    //        obj.push(response.data);
    //        $scope.work = response.data;
    //        $scope.List = response.data;
    //    })
    //}
    //$scope.GetWorkSpace();
    //$scope.Update = function (workspace) {

    //};
}
AddStandController.$inject = ['$scope', '$http'];

