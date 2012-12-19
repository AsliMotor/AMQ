define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    'view/changeLogo'
], function ($, _, Backbone, ns, am) {
    ns.define('am.organization.action');
    am.organization.action.changeLogo = function (spec) {
        var that = {};
        var execute = function (model) {
            var editor = new am.organization.view.ChangeLogo();
            editor.render();
        };
        that.execute = execute;
        return that;
    };
});