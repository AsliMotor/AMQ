define(['jquery',
        'underscore',
        'backbone',
        'namespace'], function ($, _, Backbone, ns) {
            ns.define("am.tools");
            am.tools.Animation = function (el, target) {
                $(target).empty();
                $(target).css('margin-left', '1500px');
                window.setTimeout(function () {
                    $(target).html(el);
                    $(target).css("margin-left", "0px");
                }, 100);
            };
            am.tools.BackAnimation = function (el, target) {
                $(target).empty();
                $(target).css('margin-left', '1500px');
                window.setTimeout(function () {
                    $(target).html(el);
                    $(target).css("margin-left", "0px");
                }, 100);
            }
            return am;
        });