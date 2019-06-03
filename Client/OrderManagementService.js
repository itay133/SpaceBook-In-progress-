var OrderManagementService = function (Api, ShoppingCartService) {

    //====Format for calc diff by day or month====//
    var daysFormat = (1000 * 60 * 60 * 24);
    var monthFormat = (60 * 60 * 24 * 7 * 4);
    //===Obj==/
    this.Cart = [];
    this.ReservedStand = {
        Stand: null,
        CheckIn: null,//chosen date
        CheckOut: null,//chosen date,
        PeriodPrice: null
    }

    this.CheckAvilability = function (stand, checkInDate, checkOutDate) {

        this.ReservedStand = this.setReservedStand(stand, checkInDate, checkOutDate);

        Api.PostApiCall("Order", "CheckAvilability", this.ReservedStand, function (response) {
            console.log(response);
            if (response.statusCode == 200) {
                AddToCart(response.result);
            }
            else {
                OccuipedMessage();
                return response.result;
            }
        });
    }
    // Set reserved stand property
    this.setReservedStand = function (stand, checkInDate, checkOutDate) {
        this.ReservedStand.Stand = stand;
        this.ReservedStand.StandID = stand.ID.valueOf();
        this.ReservedStand.StandType = stand.Type;
        this.ReservedStand.WorkSpaceID = stand.WS_ID;
        this.ReservedStand.CheckIn = checkInDate;
        this.ReservedStand.CheckOut = checkOutDate;
        this.ReservedStand.PeriodPrice = this.CalacPeriodPrice(stand, checkInDate, checkOutDate);
        return this.ReservedStand;
    };

    //Calc the total total price for the given stand and rental period
    this.CalacPeriodPrice = function (stand, checkInDate, checkOutDate) {
        start = new Date(checkInDate);
        end = new Date(checkOutDate);
        var days = Math.floor((Date.UTC(end.getFullYear(), end.getMonth(), end.getDate())
            - Date.UTC(start.getFullYear(), start.getMonth(), start.getDate())) / daysFormat);
        return Math.abs(Math.round(stand.DailyRate * days));
    }

    //adding stand into the shopping cart
    var AddToCart = function (ReservedStand) {
        if (ReservedStand != null && !this.Cart.includes(ReservedStand)) {
            console.log(this.Cart.includes(ReservedStand));
            this.Cart.push(ReservedStand);
            sessionStorage.setItem('Cart', JSON.stringify(this.Cart));
            this.message();
        }
    }.bind(this)

    this.getCart = function () {
        return this.Cart;
    }

    this.deleteItem = function (index) {
        this.Cart = this.Cart.splice(index, 1);
        sessionStorage.setItem('Cart', JSON.stringify(this.Cart));
    }

    this.emptyCart = function () {
        this.Cart = [];
        sessionStorage.setItem('Cart', JSON.stringify(this.Cart));
    }

    //get the Cart list from localStorage
    this.loadOrder = function () {
        if (sessionStorage.getItem('Cart')) {
            this.Cart = JSON.parse(sessionStorage.getItem('Cart'));
        } else {
            this.Cart = [];
        }

    }


    this.message = function () {
        return swal({
            title: "תודה רבה",
            text: "עמדה נוספה לעגלה בהצלחה",
            icon: "success",
            button: "אישור",
            timer: 3000
        });
    }

    var OccuipedMessage = function () {
        return swal({
            title: "הפעולה נכשלה",
            text: "עמדה תפוסה בתאריכים שבחרת ניתן לנסות תאריכים שונים",
            icon: "success",
            button: "אישור",
        });
    }
}
