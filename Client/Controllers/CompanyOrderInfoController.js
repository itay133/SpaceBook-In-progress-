var CompanyOrderInfoController = function ($scope, sharedService) {

    $scope.info = sharedService.GetOrder();



}
CompanyOrderInfoController.$inject = ['$scope','sharedService'];
