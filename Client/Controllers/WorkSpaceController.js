var WorkSpaceController = function ($scope, sharedService, Api, OrderManagementService, $mdDialog, $window) {
    $scope.dataArray = [];
    var dataArray = [];
    this.uniquePlug = false;
    $scope.WorkSpace = JSON.parse(localStorage.getItem("WorkSpaceLS"));

    //this.uniqueOn = function () {

    //}

    // Get WorkSpace back from local storage
    if ($scope.WorkSpace.SpaceImages != null) {
        //var imgAmount = ('' + $scope.WorkSpace.SpaceImages).length;
        $scope.dataArray.push({
            src: '../../Images/bg-01.jpg'
        })
        var imgAmount = $scope.WorkSpace.SpaceImages.length;
        console.log("img count: " +imgAmount);
        for (var i = 0; i < imgAmount; i++) {
            dataArray.push({ src: $scope.WorkSpace.SpaceImages[i] });
            console.log("img "+$scope.WorkSpace.SpaceImages[i]);
        }
        $scope.dataArray = dataArray;
        console.log("dataArray"+$scope.dataArray)
    }
    if ($scope.WorkSpace.Stands != null) {
        //$scope.StandCount = $scope.WorkSpace.Stands.length;
    }


    //Add stand to the shopping cart
    $scope.BookStand = function (stand, workSpace) {
        $scope.showConfirm($event);//return: check in & checkout date
        console.log($scope.CheckIn, $scope.CheckOut);
        var IsAvilable = OrderManagementService.CheckAvilability(stand, $scope.CheckIn, $scope.CheckOut);
        if (IsAvilable) {
            OrderManagementService.AddToStandsInOrder(stand, checkInDate, checkOutDate);
        };

        $scope.Order = {
            "CompanyID": workSpace.CompanyID,
            /*"StandsInOrder":*///get from the service
        };

        //var orderObj = { standID: stand.ID, Type: stand.Type, companyID: workSpace.ID, company: workSpace.Name, location: workSpace.Address.City };
        //ShoppingCartService.addStand(orderObj);
    }

    //This wil open dialog for chooing a date for the shoosen stand.
    $scope.showConfirm = function (ev, stand, workSpace) {
        $mdDialog.show({
            controller: ModalInstanceCtrl,
            templateUrl: 'Client/Views/DatesCompo.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
        })
            .then(function (Dates) {
                OrderManagementService.CheckAvilability(stand, Dates.checkIn, Dates.checkOut);
            }, function () {
                $scope.status = 'You cancelled the dialog.';
            });
    };

    //Retrieve the date for the check in and check out for the dialog pop up model. 
    $scope.DatePicker = function (CheckIn, CheckOut) {
        var Dates = {
            checkIn: CheckIn,
            checkOut: CheckOut
        };
        //console.log(CheckIn, '  ', CheckOut);
        $mdDialog.hide(Dates);
    }
    //Cencel choosing date from the dialog pop up model.
    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    //Add Feedback for the current workspace:
    $scope.AddFeedback = function (feedback, WorkSpace) {
        if (feedback) {
            console.log(feedback);
            $scope.Feedback = feedback;
            $scope.Feedback.WorkSpaceID = WorkSpace.ID;
            //feedback.WorkSpaceID = WorkSpace.ID;
            Api.PostApiCall("Company", "AddFeedback", $scope.Feedback, function (response) {
                swal({
                    title: "תודה רבה",
                    text: "הביקורת שלך התקבלה במערכת",
                    icon: "success",
                    button: "אישור",
                });
            });
        }
    };



    //Carousel
    //$scope.dataArray = [
    //    {
    //        src: '../../Images/20180116_WeWork_Park_Plaza_-_Common_Areas_-_Wide-3.jpg'
    //    },
    //    {
    //        src: 'http://www.parasholidays.in/blog/wp-content/uploads/2014/05/holiday-tour-packages-for-usa.jpg'
    //    },
    //    {
    //        src: 'http://clickker.in/wp-content/uploads/2016/03/new-zealand-fy-8-1-Copy.jpg'
    //    },
    //    {
    //        src: 'http://images.kuoni.co.uk/73/indonesia-34834203-1451484722-ImageGalleryLightbox.jpg'
    //    },
    //    {
    //        src: 'http://www.holidaysaga.com/wp-content/uploads/2014/09/Day08-SIN-day-Free-City-View.jpg'
    //    },
    //    {
    //        src: 'http://images.kuoni.co.uk/73/malaysia-21747826-1446726337-ImageGalleryLightbox.jpg'
    //    },
    //    {
    //        src: 'http://www.kimcambodiadriver.com/uploads/images/tours/kim-cambodia-driver-angkor-wat.jpg'
    //    },
    //    {
    //        src: 'https://www.travcoa.com/sites/default/files/styles/flexslider_full/public/tours/images/imperialvietnam-halong-bay-14214576.jpg?itok=O-q1yr5_'
    //    }
    //];
}
WorkSpaceController.$inject = ['$scope', 'sharedService', 'Api', 'OrderManagementService', '$mdDialog', '$window'];




    //////////////////////////TEST//////////////////////
    //var pc = this;
    //pc.data = "Lorem Name Test";

    //$scope.open = function (size) {
    //    console.log('open dialog');
    //    var modalInstance = $uibModal.open({
    //        animation: false,
    //        ariaLabelledBy: 'modal-title',
    //        ariaDescribedBy: 'modal-body',
    //        templateUrl: 'Client/Views/DatesCompo.html',
    //        controller: 'ModalInstanceCtrl',
    //        controllerAs: 'pc',
    //        size: size,
    //        resolve: {
    //            data: function () {
    //                return pc.data;
    //            }
    //        }
    //    });

    //    modalInstance.result.then(function () {
    //        alert("now I'll close the modal");
    //    });
    //};

    /////////////////////////END/////////////////////

    //$scope.open = function (Stand) {
    //    Api.PostApiCall("Company", "GetStandsByType", Stand, function (response) {
    //        if (response != null) {
    //            sharedService.SetStands(response);
    //            window.open('/#/StandView', '_self');
    //        }

    //    })
    //}


//function DialogController($scope, $mdDialog) {
//    $scope.hide = function () {
//        $mdDialog.hide();
//    };

//    $scope.cancel = function () {
//        $mdDialog.cancel();
//    };

//    $scope.answer = function (answer) {
//        $mdDialog.hide(answer);
//    };
//}

    ////Rating
    //$scope.rate = 7;
    //$scope.max = 10;
    //$scope.isReadonly = false;

    //$scope.hoveringOver = function (value) {
    //    $scope.overStar = value;
    //    $scope.percent = 100 * (value / $scope.max);
    //};

    //$scope.ratingStates = [
    //    { stateOn: 'glyphicon-ok-sign', stateOff: 'glyphicon-ok-circle' },
    //    { stateOn: 'glyphicon-star', stateOff: 'glyphicon-star-empty' },
    //    { stateOn: 'glyphicon-heart', stateOff: 'glyphicon-ban-circle' },
    //    { stateOn: 'glyphicon-heart' },
    //    { stateOff: 'glyphicon-off' }
    //];