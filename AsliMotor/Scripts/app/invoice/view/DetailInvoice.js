define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../app/invoice/action/changeuangmuka',
    '../../../app/invoice/action/changepaymenttype',
    '../../../app/invoice/action/bayaruangangsuran',
    '../../../app/invoice/action/pelunasan',
    '../../../app/invoice/action/changeuangangsuran',
    '../../../app/invoice/action/changesukubunga',
    '../../../app/invoice/action/changelamaangsuran',
    '../../../app/invoice/action/changeduedate',
    '../../../app/invoice/action/changeinvoicedate',
    '../../../app/invoice/action/changeproduct',
    '../../../app/invoice/action/changecustomer',
    '../../../app/invoice/action/changeHargaJual',
    '../../../app/invoice/action/changeTerm',
    '../../../app/invoice/action/printSuratPeringatan',
    '../../../libs/homejs/ButtonField',
    '../../../libs/Date',
    '../../../libs/Currency'
], function ($, _, Backbone, ns, am) {
    ns.define("am.invoice.view");
    am.invoice.view.DetailInvoice = Backbone.View.extend({
        className: "detail-invoice",
        render: function () {
            var billing = new am.invoice.view.DetailInvoice.Billing({ model: this.model });
            var subheader = new am.invoice.view.DetailInvoice.SubHeader({ model: this.model });
            var product = new am.invoice.view.DetailInvoice.Product({ model: this.model });
            var payment = new am.invoice.view.DetailInvoice.Payment({
                model: this.model
            });
            this.$el.html(billing.render().el);
            this.$el.append(subheader.render().el);
            this.$el.append(product.render().el);
            this.$el.append(payment.render().el);
            return this;
        }
    });

    am.invoice.view.DetailInvoice.Billing = Backbone.View.extend({
        className: "detail-invoice-billing header",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var telp = this.model.get("CustomerPhone") || '-';
            var custName = this.model.get("CustomerName") || '-';
            var address = this.model.get("CustomerAddress") || '-';
            var city = this.model.get("CustomerCity") || '-';
            var region = this.model.get("CustomerRegion") || '-';
            var html = "";
            html += "<div class='title'>" + custName + "</div>";
            html += "<div class='description'>" + address + "</div>";
            html += "<div class='description'>" + city + ", " + region + "</div>";
            html += "<div class='description'>Telp. " + telp + "</div>";
            html += "<div id='edit-customer'><button class='btn btn-mini btn-success'><i class='icon-pencil icon-white'></button></div>";
            this.$el.html(html);
            return this;
        },
        events: {
            'hover': 'showBtnEdit',
            'mouseleave': 'hideBtnEdit',
            'click #edit-customer': 'showEditDialog'
        },
        showBtnEdit: function () {
            $("#edit-customer").show();
        },
        hideBtnEdit: function () {
            $("#edit-customer").hide();
        },
        showEditDialog: function (ev) {
            ev.preventDefault();
            am.invoice.action.changeCustomer().execute(this.model);
        }
    });

    am.invoice.view.DetailInvoice.SubHeader = Backbone.View.extend({
        className: "detail-invoice-subheader header",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var invoiceNo = this.model.get("InvoiceNo") || "-";
            var date = this.model.get("InvoiceDate") ? this.model.get("InvoiceDate").toDate() : "-";
            var status = this.model.get("Status");
            var duedate = this.model.get("DueDate") ? this.model.get("DueDate").toDate() : "-";
            if (status == 0) status = "Booking";
            else if (status == 1) status = "Kredit";
            else if (status == 2) status = "Lunas";
            else if (status == 3) status = "Batal";
            else if (status == 4) status = "Sudah di Tarik";

            var invoiceDateButton = new HomeJS.components.ButtonField({
                model: this.model,
                id: "invoiceDate",
                name: "invoiceDate",
                title: "Ubah Tanggal Transaksi",
                dataIndex: "InvoiceDate",
                icon: "icon-pencil icon-white",
                labelname: "Tanggal",
                style: "float:left;margin-right:118px;",
                renderer: function (data) {
                    return data.toDate();
                },
                action: am.invoice.action.changeInvoiceDate({ model: this.model })
            });

            var dueDateButton = new HomeJS.components.ButtonField({
                model: this.model,
                id: "dueDate",
                name: "dueDate",
                title: "Ubah Tanggal Jatuh Tempo",
                dataIndex: "DueDate",
                icon: "icon-pencil icon-white",
                labelname: "Tanggal Jatuh Tempo",
                style: "float:left;margin-right:40px;",
                renderer: function (data) {
                    return data.toDate();
                },
                action: am.invoice.action.changeDueDate({ model: this.model })
            });
            var html = "";
            html += "<div class='clearfix'><div>No. Faktur</div><div class='transaction-no'>" + invoiceNo + "</div></div>";
            html += "<div class='clearfix'><div>Status</div><div class='transaction-no'>" + status + "</div></div>";
            //html += "<div class='clearfix'><div>Tanggal</div><div>" + date + "</div></div>";
            this.$el.html(html);
            if (date != '-')
                this.$el.append(invoiceDateButton.render().el);
            if (this.model.get("Status") == 1 && duedate != '-')
                this.$el.append(dueDateButton.render().el);
            return this;
        }
    });

    am.invoice.view.DetailInvoice.Product = Backbone.View.extend({
        className: "detail-invoice-product header",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var type = this.model.get("Type") || '-';
            var noRangka = this.model.get("NoRangka") || "-";
            var noMesin = this.model.get("NoMesin") || "-";
            var warna = this.model.get("Warna") || "-";
            var noPolisi = this.model.get("NoPolisi") || "-";
            var tahun = this.model.get("Tahun") || "-";
            var html = "";
            html += "<div class='clearfix'><div>Type</div><div class='transaction-no'>" + type + "</div></div>";
            html += "<div class='clearfix'><div>Nomor Rangka</div><div>" + noRangka + "</div></div>";
            html += "<div class='clearfix'><div>Nomor Mesin</div><div>" + noMesin + "</div></div>";
            html += "<div class='clearfix'><div>Warna</div><div>" + warna + "</div></div>";
            html += "<div class='clearfix'><div>Tahun</div><div>" + tahun + "</div></div>";
            html += "<div class='clearfix'><div>Nomor Polisi</div><div>" + noPolisi + "</div></div>";
            html += "<div id='edit-product'><button class='btn btn-mini btn-success'><i class='icon-pencil icon-white'></button></div>";
            this.$el.html(html);
            return this;
        },
        events: {
            'hover': 'showBtnEdit',
            'mouseleave': 'hideBtnEdit',
            'click #edit-product': 'showEditDialog'
        },
        showBtnEdit: function () {
            $("#edit-product").show();
        },
        hideBtnEdit: function () {
            $("#edit-product").hide();
        },
        showEditDialog: function (ev) {
            ev.preventDefault();
            am.invoice.action.changeProduct().execute(this.model);
        }
    });

    am.invoice.view.DetailInvoice.Payment = Backbone.View.extend({
        className: "detail-invoice-payment header",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var self = this;
            var price = this.model.get("Price") ? this.model.get("Price").toCurrency() : '-';
            var debitNote = this.model.get("DebitNote") ? this.model.get("DebitNote").toCurrency() : '-';
            var uangMuka = this.model.get("UangMuka") ? this.model.get("UangMuka").toCurrency() : '-';
            var sukuBunga = this.model.get("SukuBunga") ? this.model.get("SukuBunga").toCurrency() : '-';
            var lamaAngsuran = this.model.get("LamaAngsuran") ? this.model.get("LamaAngsuran") : '-';
            var term = this.model.get("TermName") ? this.model.get("TermName") : '-';
            var uangAngsuran = this.model.get("AngsuranBulanan") ? this.model.get("AngsuranBulanan").toCurrency() : '-';
            var noSP = this.model.get("SuratPerjanjianNo") || "-";

            var hargaJualButton = new HomeJS.components.ButtonField({
                model: this.model,
                id: "hargaJual",
                name: "hargaJual",
                title: "Ubah Harga Jual",
                dataIndex: "Price",
                icon: "icon-pencil icon-white",
                labelname: "Harga Jual",
                style: "float:left;margin-right:69px;",
                renderer: function (data) {
                    return data.toCurrency();
                },
                action: am.invoice.action.changeHargaJual({ model: this.model })
            });
            var uangMukaButton = new HomeJS.components.ButtonField({
                model: this.model,
                id: "uangMuka",
                name: "uangMuka",
                title: "Ubah Uang Muka",
                dataIndex: "UangMuka",
                icon: "icon-pencil icon-white",
                labelname: "Uang Muka",
                renderer: function (data) {
                    return data.toCurrency();
                },
                action: am.invoice.action.changeUangMuka({ model: this.model })
            });
            var uangAngsuranButton = new HomeJS.components.ButtonField({
                model: this.model,
                id: "uangAngsuran",
                name: "uangAngsuran",
                title: "Ubah Uang Angsuran Bulanan",
                dataIndex: "AngsuranBulanan",
                icon: "icon-pencil icon-white",
                labelname: "Uang Angsuran",
                style: "float:left;margin-right:40px;",
                renderer: function (data) {
                    return data.toCurrency();
                },
                action: am.invoice.action.changeUangAngsuran({ model: this.model })
            });
            var sukuBungaButton = new HomeJS.components.ButtonField({
                model: this.model,
                id: "sukuBunga",
                name: "sukuBunga",
                title: "Ubah Suku Bunga",
                dataIndex: "SukuBunga",
                icon: "icon-pencil icon-white",
                labelname: "Suku Bunga",
                style: "float:left;margin-right:62px;",
                renderer: function (data) {
                    return data.round() + "%";
                },
                action: am.invoice.action.changeSukuBunga({ model: this.model })
            });
            var lamaAngsuranButton = new HomeJS.components.ButtonField({
                model: this.model,
                id: "lamaAngsuran",
                name: "lamaAngsuran",
                title: "Ubah Lama Angsuran",
                dataIndex: "LamaAngsuran",
                icon: "icon-pencil icon-white",
                labelname: "Lama Angsuran",
                style: "float:left;margin-right:40px;",
                renderer: function (data) {
                    //var totalBulanAngsuran = (data * self.model.get("TermValue")) / 30;
                    var banyakCicilan = self.model.get("BanyakCicilan");
                    if (banyakCicilan != data)
                        return banyakCicilan + " kali selama " + data + " Bulan";
                    return data + " Bulan";
                },
                action: am.invoice.action.changeLamaAngsuran({ model: this.model })
            });
            var termButton = new HomeJS.components.ButtonField({
                model: this.model,
                id: "term",
                name: "term",
                title: "Ubah Termin Pembayaran",
                dataIndex: "TermName",
                icon: "icon-pencil icon-white",
                labelname: "Termin Pembayaran",
                style: "float:left;margin-right:12px;",
                action: am.invoice.action.changeTerm({ model: this.model })
            });
            var html = "";
            if (noSP != "-")
                html += "<div class='clearfix'><div>No. SP</div><div>" + noSP + "</div></div>";
            if (debitNote != '-')
                html += "<div class='clearfix'><div>Uang Tanda Jadi</div><div>" + debitNote + "</div></div>";
            this.$el.html(html);
            if (price != '-')
                this.$el.append(hargaJualButton.render().el);
            if (uangMuka != '-')
                this.$el.append(uangMukaButton.render().el);
            if (lamaAngsuran != '-')
                this.$el.append(lamaAngsuranButton.render().el);
            if (sukuBunga != '-')
                this.$el.append(sukuBungaButton.render().el);
            if (term != '-')
                this.$el.append(termButton.render().el);
            if (uangAngsuran != '-')
                this.$el.append(uangAngsuranButton.render().el);
            return this;
        }
    });

    return am;
});