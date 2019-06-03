var ShoppingCartService = function () {

    this.order = [];

    //get the order list from the workspace controller
    this.loadOrder = function () {
        if (sessionStorage.getItem('Cart')) {
            this.order = JSON.parse(sessionStorage.getItem('Cart'));
        } else {
            this.order = [];
        }

    }
    //adding stand into the shopping cart
    this.addStand = function (stand) {
        this.order.push(stand);
        console.log(this.stand);
        sessionStorage.setItem('Cart', JSON.stringify(this.order));
    }

    this.getOrder = function () {
        this.order = JSON.parse(sessionStorage.getItem('Cart'));
        return this.order;
    }
    this.emptyOrder = function () {
        this.order = null;
    }


    this.deleteItem = function (index) {
        this.order = this.order.splice(index, 1);
        sessionStorage.setItem('Cart', JSON.stringify(this.order));
        this.order = JSON.parse(sessionStorage.getItem('Cart')); 
    }


    this.loadOrder();

}
