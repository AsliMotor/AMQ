define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date'
], function ($, _, Backbone, ns, am) {
    ns.define("am.purchasereport.view");
    am.purchasereport.view.TotalSummaryGrafikProduk = Backbone.View.extend({
        tagName: 'div',
        className: "",
        initialize: function () {
            this.collection.on('reset', this.render, this);
            this.model.on('change', this.render, this);
        },
        render: function () {
            this.$el.html("Total pembelian produk dari periode <b>" + this.model.get("FromDate").toDateFromStringDate() + "</b> sampai <b>" + this.model.get("ToDate").toDateFromStringDate() + "</b> sebanyak ");
            this.$el.append("<b>"+this.collection.SumTotal() + "</b> unit");
            return this;
        }
    });
    return am;
});