/// <reference path="../../require.js" />
/// <reference path="../../backbone.js" />
/// <reference path="../../underscore.js" />

define(['jquery',
        'underscore',
        'backbone',
        'namespace'], function ($, _, Backbone, ns) {
            ns.define('am');
            am.eventAggregator = am.eventAggregator || _.extend({}, Backbone.Events);

            return am;
        });