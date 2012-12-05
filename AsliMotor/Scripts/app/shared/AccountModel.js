define(['jquery', 'underscore', 'backbone', 'namespace'], function ($, _, Backbone, ns) {
    ns.define('am');

    am.AccountModel = function () {
        var that = {};
        var get = function () {
            var result = new Backbone.Model({ username: '' });
            $.ajax('/Account/CurrentUser', {
                cache: false,
                dataType: 'json',
                success: function (data) {
                    result.set(data);
                },
                error: function (xhr, status, error) {
                    result.trigger('error', error);
                }
            });

            return result;
        };
        that.get = get;

        return that;
    };

    return am.AccountModel;
});