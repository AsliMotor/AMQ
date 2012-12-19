define([
    'jquery',
    'underscore',
    'backbone',
    'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.model');
        am.dashboard.model.TotalPiutangTelahJatuhTempo = Backbone.Model.extend({
            url: '/home/TotalPiutangTelahJatuhTempo'
        });
    });