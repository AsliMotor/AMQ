define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date'
], function ($, _, Backbone, ns, am) {
    ns.define("am.report.view");
    am.report.view.SelectMonthView = Backbone.View.extend({
        className: "select-month control-group",
        initialize: function () {
            //this.model.on('change', this.render, this);
        },
        render: function () {
            var currentYear = getCurrentYear();
            var html = "<label class='control-label'>Periode</label>";
            html += "<div class='controls controls-row'>";
            html += "<select>";
            for (var i = 2010; i <= currentYear; i++) {
                html += "<option value='" + i + "'>" + i + "</option>";
            }
            html += "</select>";
            html += "</div>";
            this.$el.html(html);
            $("select", this.$el).val(currentYear);
            return this;
        },
        events: {
            'change select': 'yearChanged'
        },
        yearChanged: function (e) {
            this.options.action($("select", this.$el).val());
        }
    });
    return am;
});