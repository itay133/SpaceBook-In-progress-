var DataStorgeService = function () {

    var Data = null;

    this.setItem = function (data) {
        Data = data;
        localStorage.setItem("Data", JSON.stringify(Data));
    }

    this.getItem = function () {
        return Data;
    }



    //var test = sharedService.GetWorkSpace();

    //// Save array in local storage as string
    //localStorage.setItem("test", JSON.stringify(test));

    //// Get back item "kittens" from local storage
    //var kittensFromLocalStorage = JSON.parse(localStorage.getItem("test"));

}