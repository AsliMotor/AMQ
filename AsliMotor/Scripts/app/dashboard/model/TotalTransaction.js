define([
    'jquery',
    'underscore',
    'backbone',
    'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.model');
        am.dashboard.model.TotalTransaction = Backbone.Model.extend({
            url: '/dashboard/totaltransaction'
        });
    });