var viewModel = null;
$(function () {
    viewModel = new AppViewModel();
    viewModel.fetchAllBooks();
    ko.applyBindings(viewModel);
});


function AppViewModel() {

    //declaration of variables 
    var self = this;
    self.rawBooks = ko.observableArray([]).withIndex('id');
    self.inCart = ko.observableArray([]).withIndex('id');
    self.responseFromServerArray = ko.observableArray([]).withIndex('id');
    self.searchString = ko.observable(null);

    self.searchString.subscribe(function (newVal) {

        if (newVal == null || newVal == "" || newVal == 0) {
            return self.fetchAllBooks();
        }
        else {
            return self.searchBook(newVal);
        }
    })


    self.totalPricePaid = ko.computed(function () {

        var totalPricePaid = 0;
        ko.utils.arrayForEach(self.responseFromServerArray(), function (item) {

            totalPricePaid += item.price * item.amount;
        })
        return totalPricePaid;
    });

    self.totalPrice = ko.computed(function () {

        var totalPrice = 0;
        ko.utils.arrayForEach(self.inCart(), function (item) {

            totalPrice += item.price * item.amount();
        })
        return totalPrice;
    });

   
    //Fetching data from server
    self.fetchAllBooks = function () {

        $.ajax({
            type: 'GET',
            url: apiUrl + '/BookStore/GetAll',
            contentType: false,
            processData: false
        }).done(function (result) {

            var mapped = $.map(result, function (item) { return new Book(item) });
            self.rawBooks(mapped);

        }).fail(function (error) {
            console.log(error);
        });

    }

    //Search functionality
    self.searchBook = function (newVal) {

        var d = {
            searchString: newVal
        }

        $.ajax({
            type: 'GET',
            url: apiUrl + '/BookStore/GetBooks',
            contentType: "application/json; charset=utf-8",
            data: d
        }).done(function (result) {

            console.log(result);
            var mapped = $.map(result, function (item) { return new Book(item) });
            self.rawBooks(mapped);

        }).fail(function (error) {
            console.log(error);
        });

    }

    //Adding book to Cart
    self.addToCart = function (book) {

        var alreadyInCart = false;
        ko.utils.arrayForEach(self.inCart(), function (item) {

            if (item.title == book.title && item.author == book.author) {
                var count = item.amount();
                count++;
                item.amount(count);
                alreadyInCart = true;
            }

            if (alreadyInCart)
                return;
        });

        if (!alreadyInCart) {
            var cart = new Cart(book);
            self.inCart.push(cart);
        }
    }

    //Removing book from Cart
    self.removeFromCart = function (cart) {

        var remove = false;
        ko.utils.arrayForEach(self.inCart(), function (item) {

            if (item.title == cart.title) {
                var count = item.amount();

                if (count > 1) {
                    count--;
                    item.amount(count);
                } else {
                    remove = true;
                }
            }
        });

        if (remove) {
            self.inCart.remove(function (book) {
                return book == cart;
            });
        }
    }

    //Reseting data on screen
    self.resetData = function () {
        self.responseFromServerArray.removeAll();
        self.inCart.removeAll();
    }

    //Placing order of books selected in cart
    self.placeOrder = function () {

        var cart = [];
        ko.utils.arrayForEach(self.inCart(), function (item) {

            cart.push({
                title: item.title,
                author: item.author,
                price: item.price,
                amount: item.amount
            })
        })

        $.ajax({
            type: 'POST',
            url: apiUrl + '/BookStore/PlaceOrder',
            data: { '': cart },
            dataType: 'json',
        }).done(function (result) {

            self.inCart.removeAll();
            ko.utils.arrayForEach(result, function (item) {
                self.responseFromServerArray.push(item);
            });
        }).fail(function (error) {
            console.log(error);
        });
    }
}
