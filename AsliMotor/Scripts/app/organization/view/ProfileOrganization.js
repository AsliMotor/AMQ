define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../action/changeProfileAction',
    '../action/changeLogoAction',
    '/scripts/app/shared/AccountModel.js',
    '/scripts/app/shared/RoleName.js'
], function ($, _, Backbone, ns, evt, Action, ChangeLogoAction, AccountModel, RoleName) {
    ns.define("am.organization.view");
    am.organization.view.ProfileOrganization = Backbone.View.extend({
        className: "profile-organization",
        initialize: function () {
            this.accountModel = am.AccountModel().get();
            this.model.on('change', this.render, this);
            this.accountModel.on('change', this.createToolbar, this);
        },
        render: function () {
            var name = this.model.get("OrganizationName") || '-';
            var address = this.model.get("OrganizationAddress") || '-';
            var city = this.model.get("City") || '-';
            var country = this.model.get("Country") || '-';
            var telp = this.model.get("Telp") || '-';
            var pimpinan = this.model.get("Pimpinan") || '-';
            var html = '';
            html += "<table class='profile-organization'>";
            html += "<tr>";
            html += "<td>Nama Perusahaan</td>";
            html += "<td>:</td>";
            html += "<td class='last-coloumn'><strong>" + name + "<strong></td>";
            html += "</tr>";
            html += "<tr>";
            html += "<td>Alamat Perusahaan</td>";
            html += "<td>:</td>";
            html += "<td>" + address + "</td>";
            html += "</tr>";
            html += "<tr>";
            html += "<td>Kota</td>";
            html += "<td>:</td>";
            html += "<td>" + city + "</td>";
            html += "</tr>";
            html += "<tr>";
            html += "<td>Negara</td>";
            html += "<td>:</td>";
            html += "<td>" + country + "</td>";
            html += "</tr>";
            html += "<tr>";
            html += "<td>Telpon</td>";
            html += "<td>:</td>";
            html += "<td>" + telp + "</td>";
            html += "</tr>";
            html += "<tr>";
            html += "<td>Nama Pimpinan</td>";
            html += "<td>:</td>";
            html += "<td>" + pimpinan + "</td>";
            html += "</tr>";
            this.$el.html(html);
            this.createToolbar();
            return this;
        },
        createToolbar: function () {
            var html = '';
            var userRole = (this.accountModel.get("Role")) ? this.accountModel.get("Role")[0] : null;
            if (userRole && RoleName.OWNER_ADMINISTRATOR.indexOf(userRole) >= 0) {
                html += "<div class='toolbar'>";
                html += "<button id='change-profile' class='btn btn-success'>Ubah Profil</button>";
                html += "<button id='change-logo' class='btn btn-success' style='margin-left:10px;'>Ubah Logo Perusahaan</button>";
                this.$el.append(html);
            }
        },
        events: {
            'click #change-profile': 'changeProfile',
            'click #change-logo': 'changeLogo'
        },
        changeProfile: function () {
            am.organization.action.changeProfile().execute(this.model);
        },
        changeLogo: function () {
            am.organization.action.changeLogo().execute();
        }
    });

    return am.organization.view.ProfileOrganization;
});