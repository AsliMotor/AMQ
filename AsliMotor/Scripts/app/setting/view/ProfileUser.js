define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date'
], function ($, _, Backbone, ns) {
    ns.define("am.setting.view");
    am.setting.view.ProfileUser = Backbone.View.extend({
        className: "profile-user",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var username = this.model.get("Username") || '-';
            var email = this.model.get("Email") || '-';
            var createdDate = this.model.get("CreatedDate") ? this.model.get("CreatedDate").toDate() : '-';
            var lastLoginDate = this.model.get("LastLoginDate") ? this.model.get("LastLoginDate").toDate() : '-';
            var html = '';
            html += "<table class='profile-user'>";
            html += "<tr>";
            html += "<td>User Name</td>";
            html += "<td>:</td>";
            html += "<td class='last-coloumn'><strong>" + username + "<strong></td>";
            html += "</tr>";
            html += "<tr>";
            html += "<td>Email</td>";
            html += "<td>:</td>";
            html += "<td>" + email + "</td>";
            html += "</tr>";
            html += "<tr>";
            html += "<td>Tanggal Bergabung</td>";
            html += "<td>:</td>";
            html += "<td>" + createdDate + "</td>";
            html += "</tr>";
            html += "<tr>";
            html += "<td>Login Terakhir</td>";
            html += "<td>:</td>";
            html += "<td>" + lastLoginDate + "</td>";
            html += "</tr>";
            this.$el.html(html);
            return this;
        }
    });

    return am.setting.view.ProfileUser;
});