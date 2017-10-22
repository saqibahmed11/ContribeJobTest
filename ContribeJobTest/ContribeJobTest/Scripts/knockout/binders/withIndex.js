
// Extending observable array

ko.observableArray.fn.withIndex = function (keyName) {
    var index = ko.computed(function () {
        var list = this() || [];    // the internal array
        var keys = {};              // a place for key/value
        ko.utils.arrayForEach(list, function (v) {
            if (keyName) {          // if there is a key
                keys[v[keyName]] = v;    // use it
            } else {
                keys[v] = v;
            }
        });
        return keys;
    }, this);

    // also add a handy add on function to find
    // by key ... uses a closure to access the 
    // index function created above
    this.findByKey = function (key) {
        return index()[key];
    };

    return this;
};


// todo: take it to a separate file

ko.bindingHandlers.scrollTo = {
    init: function (element, valueAccessor) {
        var model = valueAccessor();

        if (model instanceof PotentialMatch) {

            model.isHovered.subscribe(function (newVal) {
                if (newVal) {
                    $(element).parents('.window-container').animate({
                        scrollTop: $(element).offset().top
                    },
                 'slow');
                } else {
                    if (!viewModel.atleastOnePotentialMatchIsHovered()) {
                        $(element).parents('.window-container').animate({
                            scrollTop: 0
                        },
                    'slow');
                    }
                }
            });
        }
        else if (model instanceof Match) {

            model.isHovered.subscribe(function (newVal) {
                if (newVal) {
                    $(element).parents('.window-container').animate({
                        scrollTop: $(element).offset().top
                    },
                 'slow');
                } else {
                    if (!viewModel.atleastOneMatchIsHovered()) {
                        $(element).parents('.window-container').animate({
                            scrollTop: 0
                        },
                    'slow');
                    }
                }
            });
        }
    }
};