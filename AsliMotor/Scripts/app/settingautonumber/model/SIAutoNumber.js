define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.settingautonumber.model');
    am.settingautonumber.model.SIAutoNumber = Backbone.Model.extend({
        url: '/settingautonumber/GetAutoNumberSI',
        defaults: {
            Prefix: ""
        },
        initialize: function () {
            this.validators = {};
            this.validators.Prefix = function (value) {
                return value.length > 0 ? { isValid: true} : { isValid: false, message: "Prefix harus diisi" };
            };
        },
        validateItem: function (key) {
            return (this.validators[key]) ? this.validators[key](this.get(key), this) : { isValid: true };
        },
        validateAll: function () {
            var errors = [];
            for (var key in this.validators) {
                if (this.validators.hasOwnProperty(key)) {
                    var check = this.validators[key](this.get(key), this);
                    if (check.isValid === false) {
                        var e = {};
                        e.field = key;
                        e.message = check.message;
                        errors.push(e);
                    }
                }
            }
            return _.size(errors) > 0 ? { isValid: false, errors: errors} : { isValid: true };
        }
    });

    return am.settingautonumber.model.SIAutoNumber;
});