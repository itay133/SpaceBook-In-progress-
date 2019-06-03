var AboutCompanyController = function ($scope, $http, ) {
    $scope.company = [];
    $http.post('api/Mail/GetCompanyInfo').then(function (response) {
        $scope.company.push(response.data);
    })

}
AboutCompanyController.$inject = ['$scope', '$http'];
