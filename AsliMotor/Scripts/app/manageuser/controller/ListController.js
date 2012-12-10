define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../model/UserListReports',
        'eventAggregator',
        '../action/addUserAction',
        '../../../libs/Animation',
        '../../../libs/homejs/List',
        '../../../libs/homejs/Button'],
    function ($, _, Backbone, ns) {
        ns.define('am.manageuser.controller');
        am.manageuser.controller.ListController = function () {
            var manageusersModel = new am.manageuser.model.UserListReports();

            var listUserView = new HomeJS.components.List({
                header: {
                    title: "Users",
                    description: "Daftar User"
                },
                toolbar: [{
                    type: HomeJS.components.Button,
                    title: "Tambah",
                    typeButton: HomeJS.components.ButtonType.Success,
                    icon: "icon-file",
                    iconColor: HomeJS.components.ButtonColor.White,
                    events: {
                        'click': function () {
                            am.manageuser.action.addUser().execute();
                        }
                    }
                }],
                list: {
                    resizable: true,
                    collection: manageusersModel,
                    headers: [{
                        name: "Username",
                        dataIndex: "Username",
                        width: "200px",
                        title: "Klik untuk mengurutkan berdasarkan Username"
                    }, {
                        name: "Hak Akses",
                        dataIndex: "Roles",
                        minwidth: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Hak Akses"
                    }, {
                        name: "Email",
                        dataIndex: "Email",
                        minwidth: "120px",
                        title: "Klik untuk mengurutkan berdasarkan Email"
                    }, {
                        dataIndex: "Username",
                        width: "20px",
                        title: "Delete User"
                    }],
                    items: [{
                        dataIndex: "Username"
                    }, {
                        dataIndex: "Roles"
                    }, {
                        dataIndex: "Email"
                    }, {
                        align: 'center',
                        onrender: function (value, user) {
                            var buttonDeleteUser = new HomeJS.components.Button({
                                model: user,
                                title:'',
                                size: HomeJS.components.ButtonSize.Mini,
                                icon: "icon-trash",
                                iconColor: HomeJS.components.ButtonColor.Black,
                                description: "Click to delete this user",
                                events: {
                                    'click': function () {
                                        alert("test");
                                    }
                                }
                            });
                            return buttonDeleteUser.render().el;
                        }
                    }],
                    eventclick: function (data) {
                        am.eventAggregator.trigger('showDetail', data.id);
                    }
                }
            });

            var show = function () {
                am.tools.BackAnimation(listUserView.render().el, "#main-container");
                manageusersModel.fetch();
            }
            return {
                show: show
            }
        };
        return am;
    }
);