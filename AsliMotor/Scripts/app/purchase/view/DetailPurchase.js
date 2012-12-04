define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date',
    '../../../libs/Currency'
], function ($, _, Backbone, ns, am) {
    ns.define("am.purchase.view");
    am.purchase.view.DetailPurchase = Backbone.View.extend({
        className: "detail-purchase",
        render: function () {
            var billing = new am.purchase.view.DetailPurchase.Billing({ model: this.model });
            var subheader = new am.purchase.view.DetailPurchase.SubHeader({ model: this.model });
            var body = new am.purchase.view.DetailPurchase.Body({ model: this.model });
            var footer = new am.purchase.view.DetailPurchase.Footer({ model: this.model });
            this.$el.html(billing.render().el);
            this.$el.append(subheader.render().el);
            this.$el.append(body.render().el);
            this.$el.append(footer.render().el);
            return this;
        }
    });

    am.purchase.view.DetailPurchase.Billing = Backbone.View.extend({
        className: "detail-purchase-billing header",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var telp = this.model.get("NoTelp") || '-';
            var supplierName = this.model.get("SupplierName") || '-';
            var billingAddress = this.model.get("SupplierBillingAddress") || '-';
            var html = "";
            html += "<div class='title'>" + supplierName + "</div>";
            if (this.model.get("SupplierBillingAddress") && this.model.get("SupplierBillingAddress") != "")
                html += "<div class='description'>" + billingAddress + "</div>";
            html += "<div class='description'>Telp. " + telp + "</div>";
            this.$el.html(html);
            return this;
        }
    });

    am.purchase.view.DetailPurchase.SubHeader = Backbone.View.extend({
        className: "detail-purchase-subheader header",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var transactionNo = this.model.get("SupplierInvoiceNo") || '-';
            var date = this.model.get("SupplierInvoiceDate") ? this.model.get("SupplierInvoiceDate").toDate() : "-";
            var html = "";
            html += "<div class='clearfix'><div>No Transaksi</div><div class='transaction-no'>" + transactionNo + "</div></div>";
            html += "<div class='clearfix'><div>Tanggal</div><div>" + date + "</div></div>";
            this.$el.html(html);
            return this;
        }
    });

    am.purchase.view.DetailPurchase.Body = Backbone.View.extend({
        className: "detail-purchase-body",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var merk = this.model.get("Merk") || "-";
            var nopolisi = this.model.get("NoPolisi") || "-";
            var type = this.model.get("Type") || "-";
            var tahun = this.model.get("Tahun") || "-";
            var norangka = this.model.get("NoRangka") || "-";
            var nomesin = this.model.get("NoMesin") || "-";
            var nobpkp = this.model.get("NoBpkb") || "-";
            var warna = this.model.get("Warna") || "-";
            var harga = this.model.get("HargaBeli") ? this.model.get("HargaBeli").toCurrency() : "-";
            var charge = this.model.get("Charge") ? this.model.get("Charge").toCurrency() : "-";
            var html = "";
            html += "<div class='clearfix'><div class='a'>Merk</div><div class='b'>:</div><div class='c'>" + merk + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Nomor Polisi</div><div class='b'>:</div><div class='c'>" + nopolisi + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Type</div><div class='b'>:</div><div class='c'>" + type + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Tahun</div><div class='b'>:</div><div class='c'>" + tahun + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Nomor Rangka</div><div class='b'>:</div><div class='c'>" + norangka + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Nomor Mesin</div><div class='b'>:</div><div class='c'>" + nomesin + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Nomor BPKB</div><div class='b'>:</div><div class='c'>" + nobpkp + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Warna</div><div class='b'>:</div><div class='c'>" + warna + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Harga Beli</div><div class='b'>:</div><div class='c'>" + harga + "</div></div>";
            html += "<div class='clearfix'><div class='a'>Biaya-Biaya</div><div class='b'>:</div><div class='c'>" + charge + "</div></div>";
            this.$el.html(html);
            return this;
        }
    });

    am.purchase.view.DetailPurchase.Footer = Backbone.View.extend({
        className: "detail-purchase-footer",
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var note = this.model.get("Note") || "-";
            var html = "";
            html += "<div class='clearfix'><div class='a'>Keterangan</div><div class='b'>:</div><div class='note'>" + note + "</div></div>";
            this.$el.html(html);
            return this;
        }
    });

    return am;
});