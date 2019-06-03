
var myAPP_user = angular.module('myAPP_user', ['ngRoute', 'ui.bootstrap', 'chart.js', 'ngCookies', 'ngMaterial', 'ngMessages', 'angular-loading-bar', 'jkAngularCarousel']);

//Controller of myAPP//
myAPP_user.controller('U_HomePageController', U_HomePageController);
myAPP_user.controller('UserRegistrationController', UserRegistrationController);
myAPP_user.controller('U_LoginController', U_LoginController);
myAPP_user.controller('SearchResultController', SearchResultController);
myAPP_user.controller('U_ProfileController', U_ProfileController);
myAPP_user.controller('U_OrdersController', U_OrdersController);
myAPP_user.controller('WorkSpaceController', WorkSpaceController);
myAPP_user.controller('U_ChangePasswordController', U_ChangePasswordController);
myAPP_user.controller('U_ViewStandController', U_ViewStandController);
myAPP_user.controller('ShoppingCartController', ShoppingCartController);
myAPP_user.controller('DatesController', DatesController);
myAPP_user.controller('ModalInstanceCtrl', ModalInstanceCtrl);
myAPP_user.controller('StandViewController', StandViewController);


//-----------------------------Services------------------------------------------//
myAPP_user.directive('myDirectory', myDirectory);
myAPP_user.service('Api', ['$http', ApiService]);
myAPP_user.service('ShoppingCartService', ShoppingCartService);
myAPP_user.service('OrderManagementService', OrderManagementService);
myAPP_user.service('sharedService', sharedService);
myAPP_user.service('AuthService', ['$cookies', AuthService]);
myAPP_user.service('DataStorgeService', ['$cookies', AuthService]);
myAPP_user.filter('unique', unique);






//Handle Routing to be set only within client side (no server)
var configFunction = function ($routeProvider, $httpProvider) {
    $routeProvider
        .when('/HomePage',
        {
            templateUrl: 'Client/Views/U_Home.html',
            controller: U_HomePageController
        })
        .when('/Test',
        {
            templateUrl: 'Client/Views/DatesCompo.html',
            controller: ModalInstanceCtrl
        })
        .when('/StandView',
        {
            templateUrl: 'Client/Views/StandView.html',
            controller: StandViewController
        })
        .when('/UserRegistration',
        {
            templateUrl: 'Client/Views/UserRegistration.html',
            controller: UserRegistrationController
        })
        .when('/DateCompo',
        {
            templateUrl: 'Client/Views/DatesCompo.html',
            controller: DatesController
        })
        .when('/ShoppingCart',
        {
            templateUrl: 'Client/Views/ShoppingCart.html',
            controller: ShoppingCartController
        })
        .when('/U_StandWindow',
        {
            templateUrl: 'Client/Views/U_StandWindow.html',
            controller: U_ViewStandController
        })
        .when('/U_ChangePassword',
        {
            templateUrl: 'Client/Views/U_ChangePassword.html',
            controller: U_ChangePasswordController
        })
        .when('/WorkSpace',
        {
            templateUrl: 'Client/Views/WorkSpace.html',
            controller: WorkSpaceController
        })
        .when('/U_Orders',
        {
            templateUrl: 'Client/Views/U_Orders.html',
            controller: U_OrdersController
        })
        .when('/U_Profile',
        {
            templateUrl: 'Client/Views/U_ProfileEditor.html',
            controller: U_ProfileController
        })
        .when('/U_Login',
        {
            templateUrl: 'Client/Views/U_Login.html',
            controller: U_LoginController
        })
        .when('/SearchResult',
        {
            templateUrl: 'Client/Views/SearchResult.html',
            controller: SearchResultController
        })
        .when('/C_HomePage',
        {
            templateUrl: 'Views/Home/BusinessIndex.cshtml',
            controller: C_HomePageController
        })
        .otherwise({
            redirectTo: '/HomePage'
        });

};
configFunction.$inject = ['$routeProvider', '$httpProvider'];
myAPP_user.config(configFunction);



