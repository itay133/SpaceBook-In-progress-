var HomePageController = function ($scope, $rootScope, $http, BusinessAuthService) {
    $scope.models = "not working?"

    $rootScope.$on("$routeChangeStart", (event, next, current) => {
        //if (JSON.parse(localStorage.getItem("company") == null)) {

        //}
        
    });
    //$scope.homePageHeader = "Welcome to Homepage";

    ////********** call server web api method without input paramters ************
    //$http.get('/api/HomePage/getOpenedCourses').then(function (response) {
    //    $scope.courses = response.data;
    //})

}
HomePageController.$inject = ['$scope', '$rootScope', '$http', 'BusinessAuthService'];
