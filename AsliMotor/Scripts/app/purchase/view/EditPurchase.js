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
    am.purchase.view.EditPurchase = Backbone.View.extend({
        className: "edit-purchase",
        initialize: function () {
            //this.model.on('change', this.render, this);
        },
        render: function () {
            var siDate = this.model.get("SupplierInvoiceDate") ? this.model.get("SupplierInvoiceDate") : getCurrentDate();
            this.model.set("SupplierInvoiceDate", siDate);
            var siDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal',
                date: siDate,
                dataIndex: "SupplierInvoiceDate"
            });

            var noTransaksiView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor Transaksi',
                placeholder: 'Tercetak Otomatis',
                readonly: true,
                dataIndex: "SupplierInvoiceNo"
            });

            var supplierName = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nama Penjual',
                placeholder: 'Ketik Nama Penjual',
                dataIndex: "SupplierName"
            });

            var supplierBilling = new HomeJS.components.TextArea({
                model: this.model,
                title: 'Alamat Penjual',
                placeholder: 'Ketik Alamat Penjual',
                dataIndex: "SupplierBillingAddress"
            });

            var telpView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Telpon',
                placeholder: 'Ketik Nomor Telpon',
                dataIndex: "NoTelp"
            });

            var supplierView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [supplierName, supplierBilling, telpView],
                vertical: true
            });

            var typeView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Tipe',
                placeholder: 'Ketik Tipe Kendaraan',
                required: true,
                dataIndex: "Type"
            });

            var merkView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Merk',
                placeholder: 'Ketik Merk Kendaraan',
                required: true,
                dataIndex: "Merk"
            });

            var noPolisiView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor Polisi',
                placeholder: 'Ketik Nomor Polisi',
                required: true,
                dataIndex: "NoPolisi"
            });

            var hargaView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Harga Beli',
                dataIndex: "HargaBeli",
                placeholder: 'Ketik Harga Beli',
                type:'price',
                required: true
            });

            var chargeView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Biaya-biaya',
                dataIndex: "Charge",
                placeholder: 'Ketik biaya-biaya',
                type: 'price'
            });

            var priceView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [hargaView, chargeView],
                vertical: true
            });

            var noRangkaView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor Rangka',
                dataIndex: "NoRangka",
                placeholder: 'Ketik Nomor Rangka'
            });

            var noMesinView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor Mesin',
                dataIndex: "NoMesin",
                placeholder: 'Ketik Nomor Mesin'
            });

            var noBpkbView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor BPKB',
                dataIndex: "NoBpkb",
                placeholder: 'Ketik Nomor BPKB'
            });

            var noView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [noRangkaView, noMesinView, noBpkbView],
                vertical: true
            });

            var tahunView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Tahun',
                dataIndex: "Tahun",
                type: "Number"
            });

            var warnaView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Warna',
                dataIndex: "Warna",
                placeholder: 'Ketik Warna Kendaraan'
            });

            var additionalView = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [tahunView, warnaView],
                vertical: true
            });

            var noteView = new HomeJS.components.TextArea({
                model: this.model,
                title: 'Keterangan',
                placeholder: 'Optional',
                size: 'input-xlarge',
                dataIndex: "Note"
            });

            var formPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [noTransaksiView, siDateView, supplierView, merkView, typeView, noPolisiView, priceView, noView, additionalView, noteView]
            });

            this.$el.html(formPanel.render().el);
            return this;
        }
    });

    return am;
});