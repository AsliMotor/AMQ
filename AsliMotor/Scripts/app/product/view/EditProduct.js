define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/inputfield/datefield',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/inputfield/textarea'
], function ($, _, Backbone, ns, am) {
    ns.define("am.purchase.view");
    am.product.view.EditProduct = Backbone.View.extend({
        className: "edit-product",
        initialize: function () {
        },
        render: function () {
            var merkView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Merk',
                placeholder: 'Ketik Merk kendaraan',
                required: true,
                dataIndex: "Merk"
            });

            var tipeView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Type',
                placeholder: 'Ketik Tipe Kendaraan',
                required: true,
                dataIndex: "Type"
            });

            var noPolisiView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor Polisi',
                placeholder: 'Ketik Nomor Polisi',
                required: true,
                dataIndex: "NoPolisi"
            });

            var noMesinView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor Mesin',
                placeholder: 'Ketik Nomor Mesin',
                dataIndex: "NoMesin"
            });

            var noRangkaView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor Rangka',
                placeholder: 'Ketik Nomor Rangka',
                dataIndex: "NoRangka"
            });

            var noBpkbView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor BPKB',
                placeholder: 'Ketik Nomor BPKB',
                dataIndex: "NoBpkb"
            });

            var tahunView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Tahun',
                placeholder: 'Ketik Tahun',
                type: 'number',
                dataIndex: "Tahun"
            });

            var warnaView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Warna',
                placeholder: 'Ketik Warna',
                dataIndex: "Warna"
            });

            var formPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.HORIZONTAL,
                items: [merkView, tipeView, noPolisiView, noMesinView, noRangkaView, noBpkbView, tahunView, warnaView]
            });

            this.$el.html(formPanel.render().el);
            return this;
        }
    });

    return am;
});