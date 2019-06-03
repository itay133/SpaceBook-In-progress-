var StandViewController = function ($scope, sharedService) {

    $scope.StandsList = sharedService.GetStands();

    //add stand to the shopping cart
    $scope.AddStandToOrder = function (stand, workSpace/*, checkInDate, checkOutDate*/) {
        var checkInDate = new Date("09/10/2018");
        //var checkOutDate = setDate(checkInDate + 5);
        var checkOutDate = new Date("09/15/2018");



        var IsAvilable = true;/*CheckAvilability(stand, checkInDate,checkOutDate);*/
        if (IsAvilable) {
            OrderManagementService.AddToStandsInOrder(stand, checkInDate, checkOutDate);
        };

        $scope.Order = {
            "CompanyID": workSpace.CompanyID,
            /*"StandsInOrder":*///get from the service
        };

        var orderObj = { standID: stand.ID, Type: stand.Type, companyID: workSpace.ID, company: workSpace.Name, location: workSpace.Address.City };
        //ShoppingCartService.addStand(orderObj);


    }

}
StandViewController.$inject = ['$scope','sharedService'];