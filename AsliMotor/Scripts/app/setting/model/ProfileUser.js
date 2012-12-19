define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.setting.model');
    am.setting.model.ProfileUser = Backbone.Model.extend({
        url: '/setting/ProfileUser',
        defaults: {
            Username: '-',
            Email: '-',
            CreatedDate: '-',
            LastLoginDate: '-'
        }
    });

    return am.setting.model.ProfileUser;
});