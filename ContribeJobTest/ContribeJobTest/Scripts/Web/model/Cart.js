function Cart(data) {
    var self = this;

    this.title = data.title;
    this.author = data.author;
    this.price = data.price;
    this.amount = ko.observable(1);


}