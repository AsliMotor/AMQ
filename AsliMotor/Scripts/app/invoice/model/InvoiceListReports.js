define([
    'underscore',
    'backbone',
    'namespace',
    '../model/InvoiceListReport'
], function (_, Backbone, ns, am) {
    ns.define('am.invoice.model');
    am.invoice.model.InvoiceListReports = Backbone.Collection.extend({
        initialize: function () {
            this.offset = 0;
            _.bindAll(this, 'addCollection');
        },
        url: 'invoice/lists',
        model: am.invoice.model.InvoiceListReport,
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