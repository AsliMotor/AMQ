define([
    'underscore',
    'backbone',
    'namespace',
    '../model/ProductListReport'
], function (_, Backbone, ns, am) {
    ns.define('am.product.model');
    am.product.model.ProductListReports = Backbone.Collection.extend({
        initialize: function () {
            this.offset = 0;
            this.status = "Aktif";
            _.bindAll(this, 'addCollection');
        },
        url: 'product/lists',
        model: am.product.model.ProductListReport,
        fetch: function (options) {
            var options = options || {};
            options.data = { offset: this.offset++, status: this.status };
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
            var data = { offset: this.offset++, status: this.status };
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