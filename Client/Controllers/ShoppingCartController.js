var ShoppingCartController = function ($scope, ShoppingCartService, Api) {

    //get all the stands to the shopping cart
    $scope.Order = ShoppingCartService.getOrder()
    console.log($scope.Order);

    //delete stand from the shopping list
    $scope.deleteItem = function (index) {
        if ($scope.Order.length !== 0) {
            ShoppingCartService.deleteItem(index);
        }
        if ($scope.Order.length == 0) {
            sessionStorage.setItem('Cart', null);
            ShoppingCartService.emptyOrder();
        }
        $scope.ShoppingCart();
    }

    $scope.ShoppingCart = function () {
        if ($scope.Order == null) {
            sessionStorage.setItem('Cart', null);
            swal("!עגלה ריקה");
            window.open('/#/HomePage', '_self');
        } else {

            if ($scope.Order.length == 0) {
                sessionStorage.setItem('Cart', null);
                swal("!עגלה ריקה");
                window.open('/#/HomePage', '_self');
            }
        }
    }
    $scope.ShoppingCart();


    //send the order to DB
    $scope.CheckOut = function (Order) {

        //check if the user is login
        Api.GetApiCall("User", "CheckUserStatus", function (responce) {
            if (responce.result !== false) {
                //set order:Is an api for sending a reservedStad obj in order to place user order.
                Api.PostApiCall("Order", "SetOrder", Order, function (response) {
                    //wiil return HttpResponseMessage later on...
                    if (response.result) {
                        swal({
                            title: "!הזמנתך הוכנסה למערכת",
                            text: "",
                            icon: "success",
                            button: "אישור",
                        });
                        $scope.Order = [];
                        sessionStorage.removeItem('Cart');
                        window.open('/#/HomePage', '_self');
                    }
                    else {
                        swal({
                            title: "!לא ניתן לאשר הזמנה זו",
                            text: "",
                            icon: "error",
                            button: "אישור",
                        });
                    }
                })
            }
            else {
                swal({
                    title: "!עליך להתחבר למערכת",
                    text: "",
                    icon: "error",
                    button: "אישור",
                });
            }

        })
    }

}
ShoppingCartController.$inject = ['$scope', 'ShoppingCartService', 'Api'];
