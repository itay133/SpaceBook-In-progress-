var sharedService = function () {

    this.Satnds = [];
    this.Order = null;
    this.WorkSpace = null;
    this.WorkSpacelist = [];




    // Stand Service Handler
    this.SetStands = function (chosen) {
        this.Satnds = chosen;
    }

    this.GetStands = function () {
        return this.Satnds;
    }

    // Workspace Service Handler 
    this.SetWorkSpace = function (chosen) {
        localStorage.setItem('WorkSpaceLS', JSON.stringify(chosen));
        this.WorkSpace = chosen;
    }

    this.SetWorkSpaceList = function (list) {
        //localStorage.setItem("list", JSON.stringify(list));
        this.WorkSpacelist = list;
    }
    this.GetWorkSpace = function () {
        return this.WorkSpace;
    }
    this.GetWorkSpacelist = function () {
        return this.WorkSpacelist;
    }

    // worksapce order Service Handler
    this.SetOrder = function (order) {
        this.Order = order;
    }

    this.GetOrder = function () {
        return this.Order;
    }

}