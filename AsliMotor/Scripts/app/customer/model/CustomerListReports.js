define([
    'underscore',
    'backbone',
    'namespace',
    '../model/CustomerListReport'
], function (_, Backbone, ns, am) {
    ns.define('am.customer.model');
    am.customer.model.CustomerListReports = Backbone.Collection.extend({
        initialize: function () {
            this.offset = 0;
            _.bindAll(this, 'addCollection');
        },
        url: 'customer/lists',
        model: am.customer.model.CustomerListReport,
        fetch: function (options) {
            var options = options || {};
            options.data = { offset: this.offset++ };
            this.query(options);
        },
        query: function (options) {
            typeof (options) != 'undefined' || (options = {});
            this.trigger("fetching");
            var self = this;

            var success = options.success;
            options.success = function (resp) {
                self.trigger("fetched");
                if (success) { success(self, resp); }
            };
            return Backbone.Collection.prototype.fetch.call(this, options);
        },
        showMore: function () {
            typeof (options) != 'undefined' || (options = {});
            var data = { offset: this.offset++ };
            $.ajax({
                type: 'GET',
                url: this.url,
                data: data,
                success: this.addCollection
            });
        },
        addCollection: function (data) {
            var self = this;
            data.forEach(function (i) {
                self.add(i);
            });
        }
    });

    return am;
});