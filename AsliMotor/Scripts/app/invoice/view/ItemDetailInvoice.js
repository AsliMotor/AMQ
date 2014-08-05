define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    'eventAggregator',
    '../../../libs/Date',
    '../../../libs/Currency'
], function ($, _, Backbone, ns, am) {
    ns.define("am.invoice.view");
    am.invoice.view.ItemDetailInvoice = Backbone.View.extend({
        className: "detail-item-invoice",
        initialize: function () {
            this.collection.on('reset', this.render, this);
        },
        render: function () {
            if (this.collection.length > 0) {
                this.$el.css('margin-top', '20px');
                var html = "";
                html += "<table class='table table-striped'>" +
                    "<thead>" +
                        "<tr>" +
                            "<th>No</th>" +
                            "<th>Angsuran</th>" +
                            "<th>No. Kwitansi</th>" +
                            "<th>Tanggal Pembayaran</th>" +
                            "<th class='right'>Besar Angsuran</th>" +
                            "<th class='right'>Denda</th>" +
                            "<th class='right'>Jumlah</th>" +
                            "<th></th>" +
                        "</tr>" +
                    "</thead>" +
                    "<tbody>" +
                    "</tbody>" +
                    "</table>";

                this.$el.html(html);
                this.collection.forEach(this.addOne, this);
            }
            return this;
        },
        addOne: function (item) {
            var itemView = new am.invoice.view.ItemDetailInvoice.Item({ model: item, invoiceId: this.options.invoiceId });
            $("tbody", this.$el).append(itemView.render().el);
        }
    });

    am.invoice.view.ItemDetailInvoice.Item = Backbone.View.extend({
        tagName: 'tr',
        render: function () {
            var transactionNo = this.model.get("ReceiveNo") || '-';
            var month = this.model.get("MonthNumber") ? this.model.get("MonthNumber") : '-';
            var monthFormated = this.model.get("MonthFormated") ? this.model.get("MonthFormated") : '-';
            var receiveDate = this.model.get("ReceiveDate") ? this.model.get("ReceiveDate").toDate() : '-';
            var angsuranBulanan = this.model.get("AngsuranBulanan") ? this.model.get("AngsuranBulanan").toCurrency() : '-';
            var denda = this.model.get("Denda") ? this.model.get("Denda").toCurrency() : '-';
            var jumlah = this.model.get("Total") ? this.model.get("Total").toCurrency() : '-';
            var status = this.model.get("Status");
            if (status == 3) {
                var html = "<td>" + month + "</td>" +
                        "<td>" + monthFormated + "</td>" +
                        "<td>" + transactionNo + "</td>" +
                        "<td>" + receiveDate + "</td>" +
                        "<td class='right'>" + angsuranBulanan + "</td>" +
                        "<td class='right'>" + denda + "</td>" +
                        "<td class='right'>" + jumlah + "</td>" +
                        "<td><i class='icon-print print-receive' title='cetak kwitansi'></i></td>";
            }
            else if (status == 4) {
                var html = "<td>Pelunasan</td>" +
                        "<td>-</td>" +
                        "<td>" + transactionNo + "</td>" +
                        "<td>" + receiveDate + "</td>" +
                        "<td class='right'>-</td>" +
                        "<td class='right'>" + denda + "</td>" +
                        "<td class='right'>" + jumlah + "</td>" +
                        "<td><i class='icon-print print-receive-pelunasan' title='cetak kwitansi pelunasan'></i></td>";
                this.$el.addClass('pelunasan');
            }
            this.$el.html(html);
            $("i.print-receive", this.$el).css('cursor', 'pointer');
            return this;
        },
        events: {
            'click i.print-receive': 'printReceive',
            'click i.print-receive-pelunasan': 'printReceivePelunasan'
        },
        printReceive: function () {
            window.open("/invoice/PrintKwitansiAngsuranBulanan/" + this.model.get('id'), 'Kwitansi Angsuran Bulanan', null, null);
        },
        printReceivePelunasan: function () {
            window.open("/invoice/PrintKwitansiPelunasan/" + this.options.invoiceId, 'Kwitansi Pelunasan', null, null);
        }
    });

    return am;
});