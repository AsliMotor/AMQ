define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/inputfield/textarea',
    '../../../libs/homejs/buttontype',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/dialog/erroralert',
    '../../../libs/homejs/dialog/ModalDialog'
], function ($, _, Backbone, ns) {
    ns.define("am.organization.view");
    am.organization.view.ChangeLogo = Backbone.View.extend({
        tagName: 'div',
        className: 'change-logo',
        initialize: function () {
        },
        render: function () {
            var view = Backbone.View.extend({
                className: "upload-logo",
                initialize: function () {
                },
                render: function () {
                    var html = "";
                    html += "<form action='/Organization/UpdateLogo' method='POST' enctype='multipart/form-data'>";
                    html += "<input type='file' name='image'/>";
                    html += "<div>";
                    html += "<button type='submit' class='btn btn-success'>Simpan</button>"
                    html += "<button id='cancel' class='btn btn-danger' style='margin-left:10px;'>Batal</button>"
                    html += "</div>";
                    html += "</form>";
                    this.$el.html(html);
                    return this;
                },
                events: {
                    'click #cancel': 'close'
                },
                close: function (ev) {
                    ev.preventDefault();
                    $('#mask , .modal-dialog').fadeOut(300, function () {
                        $('#mask').remove();
                    });
                }
            });

            var updateLogoView = new view();

            var updateLogoDialog = new HomeJS.components.ModalDialog({
                title: 'Ubah Logo Perusahaan',
                view: updateLogoView
            });
            updateLogoDialog.show();
            return this;
        }
    });
});