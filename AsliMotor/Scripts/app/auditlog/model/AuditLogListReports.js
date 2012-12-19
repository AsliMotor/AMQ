﻿define([
    'underscore',
    'backbone',
    'namespace',
    '../model/AuditLogListReport'
], function (_, Backbone, ns, am) {
    ns.define('am.auditlog.model');
    am.auditlog.model.AuditLogListReports = Backbone.Collection.extend({
        initialize: function () {
            this.offset = 0;
            this.search = false;
            this.searchKey = "";
            _.bindAll(this, 'addCollection');
        },
        url: '/PriceChangeLog/GetListSILog',
        model: am.auditlog.model.AuditLogListReport,
        fetch: function (options) {
            var options = options || {};
            this.search = false;
            options.data = { offset: this.offset++, search: this.search };
            this.query(options);
        },
        searching: function (key) {
            this.clearAll();
            this.searchKey = key;
            this.search = true;
            this.offset = 0;
            var data = { data: { offset: this.offset++, key: this.searchKey, search: this.search} };
            this.query(data);
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
            var data = { offset: this.offset++, search: this.search, key: this.searchKey };
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
        },
        clearAll: function (options) {
            while (this.length > 0) {
                this.at(0).set({ "Id": null });
                this.deleteOne(this.at(0));
            }
        },
        deleteOne: function (item) {
            this.remove(item);
        }
    });

    return am;
});