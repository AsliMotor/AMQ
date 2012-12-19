define([
    'underscore',
    'backbone',
    'namespace',
    '../model/RoleSelectionList'
],
function (_, Backbone, ns, RoleSelectionList) {
    ns.define('am.manageuser.view');
    am.manageuser.view.RoleSelectionView = Backbone.View.extend({
        className: 'control-group',
        initialize: function () {
            this.collection = new RoleSelectionList();
            this.collection.on('reset', this.addAllRoles, this);
        },
        events: {
            'change select': 'setRole'
        },
        render: function () {
            this.addAllRoles();
            this.loadData();
            return this;
        },
        addAllRoles: function () {
            html = "<label class='control-label'>" + this.options.title + "</label>\
                    <div class='controls controls-row'>\
                        <select></select>\
                        <div class='help-inline'></div>\
                    </div>";
            this.$el.html(html);
            this.collection.forEach(function (role) {
                this.addRole(role);
            }, this);
            if (this.collection.length > 0) {
                var roleName = this.collection.at(0).get('id');
                this.model.set('role', roleName);
            }
        },
        addRole: function (role) {
            var option = $("<option></option>");
            option.attr('id', role.get('id'));
            option.html(role.get('name'));

            $('select', this.$el).append(option);
        },
        setRole: function (e) {
            var selectedRole = $('option:selected', this.$el);
            this.model.set('role', selectedRole.attr('id'));
        },
        loadData: function () {
            this.collection.fetch();
        }
    });

    return am.manageuser.view.RoleSelectionView;
});