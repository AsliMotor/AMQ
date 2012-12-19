define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date'
], function ($, _, Backbone, ns, am) {
    ns.define("am.report.view");
    am.report.view.SelectYearView = Backbone.View.extend({
        tagName: 'form',
        className: "clearfix homejs-formpanel form-vertical",
        initialize: function () {
            //this.model.on('change', this.render, this);
        },
        render: function () {
            var currentYear = getCurrentYear();
            var html = "<fieldset>";
            html = "<div class='control-group inline vertical'><label class='control-label'>Dari Tahun</label>";
            html += "<div class='controls controls-row'>";
            html += "<select id='fromDate'>";
            for (var i = 2010; i <= currentYear; i++) {
                html += "<option value='" + i + "'>" + i + "</option>";
            }
            html += "</select>";
            html += "</div></div>";
            html += "<div class='control-group inline vertical'><label class='control-label'>Sampai Tahun</label>";
            html += "<div class='controls controls-row'>";
            html += "<select id='toDate'>";
            for (var i = 2010; i <= currentYear; i++) {
                html += "<option value='" + i + "'>" + i + "</option>";
            }
            html += "</select>";
            html += "</div></div>";
            html += "<fieldset>";
            this.$el.html(html);
            $("select", this.$el).val(currentYear);
            return this;
        },
        events: {
            'change select#fromDate': 'fromYearChanged',
            'change select#toDate': 'toYearChanged'
        },
        fromYearChanged: function (e) {
            this.model.set("FromYear", $("select#fromDate", this.$el).val());
        },
        toYearChanged: function (e) {
            this.model.set("ToYear", $("select#toDate", this.$el).val());
        }
    });
    return am;
});