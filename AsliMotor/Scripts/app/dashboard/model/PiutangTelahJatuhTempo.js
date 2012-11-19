define([
    'jquery',
    'underscore',
    'backbone',
    'namespace'],
    function ($, _, Backbone, ns) {
        ns.define('am.dashboard.model');
        var Model = Backbone.Model.extend();
        am.dashboard.model.PiutangTelahJatuhTempo = Backbone.Collection.extend({
            url: '/home/PiutangTelahJatuhTempo',
            model: Model
        });
    });