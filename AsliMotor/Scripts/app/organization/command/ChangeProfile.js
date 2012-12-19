define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator'
], function ($, _, Backbone, ns, am) {
    ns.define('am.organization.command');

    am.organization.command.ChangeProfile = Backbone.Model.extend({
        url: '/organization/updateprofile',
        defaults: {
            OrganizationName: "",
            OrganizationAddress: "",
            City: "",
            Country: "",
            Telp: "",
            Pimpinan: ""
        },
        initialize: function () {
            this.validators = {};
            this.validators.OrganizationName = function (value, model) {
                return (value.length > 0) ? { isValid: true} : { isValid: false, message: "Nama Perusahaan harus diisi" };
            };
            this.validators.OrganizationAddress = function (value, model) {
                return (value.length > 0) ? { isValid: true} : { isValid: false, message: "Alamat Perusahaan harus diisi" };
            };
            this.validators.Pimpinan = function (value, model) {
                return (value.length > 0) ? { isValid: true} : { isValid: false, message: "Nama Pimpinan harus diisi" };
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

    am.organization.command.changeProfileCommander = function (spec) {
        var that = {};
        var createCommand = function (model) {
            var command = new am.organization.command.ChangeProfile({
                OrganizationName: model.get("OrganizationName"),
                OrganizationAddress: model.get("OrganizationAddress"),
                City: model.get("City"),
                Country: model.get("Country"),
                Telp: model.get("Telp"),
                Pimpinan: model.get("Pimpinan")
            });
            return command;
        }
        that.createCommand = createCommand;
        return that;
    };
});