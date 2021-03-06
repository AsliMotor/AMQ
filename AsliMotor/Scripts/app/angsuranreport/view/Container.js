﻿define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator'
], function ($, _, Backbone, ns, am) {
    ns.define("am.angsuranreport.view");
    am.angsuranreport.view.Container = Backbone.View.extend({
        className: "angsuran-report-container",
        initialize: function () {
        },
        render: function () {
            this.options.items.forEach(this.addItem, this);
            return this;
        },
        addItem: function (item) {
            var html = "<div class='" + item.class + "' id='" + item.id + "'></div>";
            this.$el.append(html);
            $("#" + item.id, this.$el).html(item.view.render().el);
        }
    });
    return am;
});