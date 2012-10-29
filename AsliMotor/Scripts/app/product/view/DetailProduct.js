define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date',
    '../../../libs/Currency'
], function ($, _, Backbone, ns, am) {
    ns.define("am.product.view");
    am.product.view.DetailProduct = Backbone.View.extend({
        className: "detail-product",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            this.$el.empty();
            this.options.items.forEach(this.addItem, this);
            return this;
        },
        addItem: function (item) {
            var value = this.model.get(item.dataIndex) || "-";
            var html = "<div class='clearfix'><div class='a'>" + item.title + "</div><div class='b'>:</div><div>" + value + "</div></div>";
            this.$el.append(html);
        }
    });
    return am;
});