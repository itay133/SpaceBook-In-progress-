var U_OrdersController = function ($scope, Api) {



    $scope.loading = false;
    $scope.getData = function () {
        $scope.loading = true;
        Api.GetApiCall("User", "GetUserOrders", function (response) {
            //console.log(response.result);
            $scope.Orders = response.result;
            $scope.displayOrders = response.result;

            console.log($scope.StandsInOrder);
            $scope.loading = false;
            console.log($scope.Orders);
            $scope.StandsInOrder = $scope.Orders.StandsInOrder;
        });

    }
    $scope.getData();

}
U_OrdersController.$inject = ['$scope', 'Api'];


//$scope.Orders[0].CompanyID
//$scope.Orders[0].StandsInOrder
//response.result.Orders[0].StandsInOrder[0].CheckIn
//$scope.Orders[0].StandsInOrder[0].PeriodPrice