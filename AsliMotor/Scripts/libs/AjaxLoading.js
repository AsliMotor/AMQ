define(['jquery',
        'underscore',
        'backbone',
        'namespace'], function ($, _, Backbone, ns) {
            ns.define("am.tools");
            am.tools.ShowAjaxLoading = function () {
                var html = "<div class='ajax-loading-container'><div class='loading-element'><div class='message'>Loading...</div></div></div>";
                $(document.body).append(html);
            };
            am.tools.HideAjaxLoading = function () {
                $("div.ajax-loading-container").remove();
            }
            return am;
        });