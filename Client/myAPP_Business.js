
var myAPP_Business = angular.module('myAPP_Business', ['ngRoute', 'ui.bootstrap', 'ngCookies', 'angular-loading-bar', 'smart-table', 'ngMaterial', 'ngMessages']);
//===========================Controllers======================================//
myAPP_Business.controller('HomePageController', HomePageController);
myAPP_Business.controller('RegistrationController', RegistrationController);
myAPP_Business.controller('LoginController', LoginController);
myAPP_Business.controller('CompanyController', CompanyController);
myAPP_Business.controller('AboutCompanyController', AboutCompanyController);
myAPP_Business.controller('AddWorkSpaceController', AddWorkSpaceController);
myAPP_Business.controller('CompanyWorkspacesController', CompanyWorkspacesController);
myAPP_Business.controller('CompanyProfileController', CompanyProfileController);
myAPP_Business.controller('WorkSpaceController', WorkSpaceController);
myAPP_Business.controller('WorkspaceStandsController', WorkspaceStandsController);
myAPP_Business.controller('CompanyOrderDashboardController', CompanyOrderDashboardController);
myAPP_Business.controller('CompanyOrderInfoController', CompanyOrderInfoController);
myAPP_Business.controller('ChangePasswordController', ChangePasswordController);
//============================Services======================================//
myAPP_Business.service('Api', ['$http', ApiService]);
myAPP_Business.service('BusinessAuthService', ['$cookies', BusinessAuthService]);
myAPP_Business.service('sharedService', sharedService);
myAPP_Business.directive('myDirectory', myDirectory);
//myAPP_Business.factory('uploadService', uploadService);


//Handle Routing to be set only within client side (no server)
var configFunction = function ($routeProvider, $httpProvider) {



    $routeProvider
        .when('/HomePage',
            {
                templateUrl: '/Client/Views/Home.html',
                controller: HomePageController
            })
        .when('/CompanyProfile',
            {
                templateUrl: '/Client/Views/CompanyProfile.html',
                controller: CompanyProfileController,

            })
        .when('/ChangePassword',
            {
                templateUrl: '/Client/Views/ChangePass.html',
                controller: ChangePasswordController,

            })
        .when('/Registration',
            {
                templateUrl: '/Client/Views/Registration.html',
                controller: RegistrationController
            })
        .when('/Login',
            {
                templateUrl: '/Client/Views/Login.html',
                controller: LoginController
            })
        .when('/AddWorkSpace',
            {
                templateUrl: '/Client/Views/AddWorkSpace.html',
                controller: AddWorkSpaceController
            })
        .when('/Workspaces',
            {
                templateUrl: '/Client/Views/CompanyWorkspaces.html',
                controller: CompanyWorkspacesController
            })
        .when('/AddStand',
            {
                templateUrl: '/Client/Views/AddStand.html',
                controller: WorkspaceStandsController
            })
        .when('/AboutCompany',
            {
                templateUrl: '/Client/Views/AboutCompany.html',
                controller: AboutCompanyController
            })
        .when('/CompanyWorkspaces',
            {
                templateUrl: '/Client/Views/CompanyWorkspaces.html',
                controller: CompanyWorkspacesController
            })
        .when('/CompanyOrder',
            {
                templateUrl: '/Client/Views/CompanyOrders.html',
                controller: CompanyOrderDashboardController
            })
        .when('/OrderInfo',
            {
                templateUrl: '/Client/Views/CompanyOrderInfo.html',
                controller: CompanyOrderInfoController
            })
        .when('/U_HomePage',
            {
                templateUrl: 'Views/Home/UserIndex.cshtml',
                controller: U_HomePageController
            })
        .otherwise(
            {
                redirectTo: '/HomePage'
            });
};
//configFunction.$inject = ['$routeProvider', '$httpProvider'];
myAPP_Business.config(configFunction);