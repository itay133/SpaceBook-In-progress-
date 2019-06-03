var CompanyOrderDashboardController = function ($scope, $http, Api, sharedService) {

    $scope.loading = false;
    $scope.getData = function () {
        $scope.loading = true;
        Api.GetApiCall("Order", "GetCompanyOrders", function (response) {
            console.log(response.result);
            $scope.Orders = response.result;
            $scope.displayOrders = response.result;
            $scope.loading = false;
        });
      
    }
    $scope.getData();

    $scope.deleteItem = function (id, index) {
        var ID = JSON.stringify(id);
        $scope.Orders.splice(index, 1);
        Api.PostApiCall("Order", "DeleteCompanyOrder", ID, function (response) {
            swal({
                title: "!ההזמנה נמחקה",
                text:  "",
                icon: "success",
                button: "אישור",
            });
        })
        
    }

    $scope.MoreInfo = function (order) {

        sharedService.SetOrder(order);
        window.open('#/OrderInfo', '_Self');

    }


}
CompanyOrderDashboardController.$inject = ['$scope', '$http', 'Api', 'sharedService'];
//====================Demo================================//
//var CompanyOrderDashboardController = function ($scope, $http, Api, $log) {
//    $scope.loading = false;
//    $scope.getData = function () {
//        $scope.loading = true;
//        $http.get("http://dummy.restapiexample.com/api/v1/employees")
//            .then(function (response) {
//                $scope.employees = response.data;
//                $scope.loading = false;
//            });
//    }
//    $scope.getData();
//}
//CompanyOrderDashboardController.$inject = ['$scope', '$http', 'Api'];
