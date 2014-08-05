define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/date'
], function ($, _, Backbone, ns, am) {
    ns.define('am.invoice.command');
    am.invoice.command.PrintSuratPeringatan = Backbone.Model.extend({
        //url: '/invoice/PrintSuratPeringatan',
        defaults: {
            Id:"",
            Date: ""
        }
    });

    am.invoice.command.PrintSuratPeringatanCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var printSuratPeringatan = new am.invoice.command.PrintSuratPeringatan({
                Id: model.get("id"),
                Date: getCurrentDate()
            });
            return printSuratPeringatan;
        }
        that.createCommand = createCommand;
        return that;
    };
});