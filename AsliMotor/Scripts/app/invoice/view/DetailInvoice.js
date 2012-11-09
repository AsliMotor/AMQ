define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
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
            var payment = new am.invoice.view.DetailInvoice.Payment({ model: this.model, changeUangMuka: this.options.changeUangMuka });
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
            this.$el.html(html);
            return this;
        }
    });

    am.invoice.view.DetailInvoice.SubHeader = Backbone.View.extend({
        className: "detail-invoice-subheader header",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var date = this.model.get("InvoiceDate") ? this.model.get("InvoiceDate").toDate() : "-";
            var status = this.model.get("Status");
            var duedate = this.model.get("DueDate") ? this.model.get("DueDate").toDate() : "-";
            if (status == 0) status = "Booking";
            else if (status == 1) status = "Kredit";
            else if (status == 2) status = "Lunas";
            var html = "";
            html += "<div class='clearfix'><div>Status</div><div class='transaction-no'>" + status + "</div></div>";
            html += "<div class='clearfix'><div>Tanggal</div><div>" + date + "</div></div>";
            if (this.model.get("Status") == 1)
                html += "<div class='clearfix'><div>Jatuh Tempo</div><div>" + duedate + "</div></div>";
            this.$el.html(html);
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
            html += "<div class='clearfix'><div>Warna / Tahun</div><div>" + warna + " / " + tahun + "</div></div>";
            html += "<div class='clearfix'><div>Nomor Polisi</div><div>" + noPolisi + "</div></div>";
            this.$el.html(html);
            return this;
        }
    });

    am.invoice.view.DetailInvoice.Payment = Backbone.View.extend({
        className: "detail-invoice-payment header",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var price = this.model.get("Price") ? this.model.get("Price").toCurrency() : '-';
            var debitNote = this.model.get("DebitNote") ? this.model.get("DebitNote").toCurrency() : '-';
            var uangMuka = this.model.get("UangMuka") ? this.model.get("UangMuka").toCurrency() : '-';
            var uangAngsuran = this.model.get("AngsuranBulanan") ? this.model.get("AngsuranBulanan").toCurrency() : '-';
            var noSP = this.model.get("SuratPerjanjianNo") || "-";
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
                action: this.options.changeUangMuka
            });
            var html = "";
            if (noSP != "-")
                html += "<div class='clearfix'><div>No. SP</div><div>" + noSP + "</div></div>";
            html += "<div class='clearfix'><div>Harga Jual</div><div class='price'>" + price + "</div></div>";
            if (debitNote != '-')
                html += "<div class='clearfix'><div>Uang Tanda Jadi</div><div>" + debitNote + "</div></div>";
            if (uangAngsuran != '-')
                html += "<div class='clearfix'><div>Uang Angsuran</div><div>" + uangAngsuran + "</div></div>";
            this.$el.html(html);
            if (uangMuka != '-')
                this.$el.append(uangMukaButton.render().el);
            return this;
        }
    });

    return am;
});