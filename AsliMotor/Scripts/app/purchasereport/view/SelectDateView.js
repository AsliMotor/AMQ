define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date',
    '../../../libs/homejs/inputfield/datefield',
    '../../../libs/Currency',
    '../../../libs/homejs/formpanel'
], function ($, _, Backbone, ns, am) {
    ns.define("am.purchasereport.view");
    am.purchasereport.view.SelectDateView = Backbone.View.extend({
        className: "select-date",
        initialize: function () {
            //this.model.on('change', this.render, this);
        },
        render: function () {
            var fromDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Dari Periode',
                dataIndex: "FromDate"
            });
            var toDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Sampai Periode',
                date: getCurrentDate(),
                dataIndex: "ToDate"
            });
            var selectDateFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [fromDateView, toDateView],
                vertical: true
            });
            this.$el.html(selectDateFormPanel.render().el);
            return this;
        }
    });
    return am;
});