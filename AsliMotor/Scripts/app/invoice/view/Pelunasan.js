define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../../libs/Date',
    '../../../libs/homejs/inputfield/textfield',
    '../../../libs/homejs/button',
    '../../../libs/homejs/buttontype',
    '../../../libs/homejs/formpanel',
    '../../../libs/homejs/dialog/erroralert'
], function ($, _, Backbone, ns) {
    ns.define("am.invoice.view");
    am.invoice.view.Pelunasan = Backbone.View.extend({
        tagName: 'div',
        className: 'pelunasan',
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var self = this;
            var paymentDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal Pembayaran',
                dataIndex: "Date"
            });

            var detailView = new am.invoice.view.Pelunasan.Detail({
                model: this.model
            });

            var yesButtonView = new HomeJS.components.Button({
                title: "Simpan",
                model: this.model,
                typeButton: HomeJS.components.ButtonType.Success,
                icon: "icon-ok",
                iconColor: HomeJS.components.ButtonColor.White,
                events: {
                    "click": this.sendCommand
                }
            });
            var noBbuttonView = new HomeJS.components.Button({
                title: "Batal",
                typeButton: HomeJS.components.ButtonType.Danger,
                icon: "icon-remove",
                iconColor: HomeJS.components.ButtonColor.White,
                events: {
                    "click": function (ev) {
                        ev.preventDefault();
                        $('#mask , .modal-dialog').fadeOut(300, function () {
                            $('#mask').remove();
                        });
                    }
                }
            });

            var buttonFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [yesButtonView, noBbuttonView],
                vertical: true
            });

            var uangMukaFormPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.VERTICAL,
                items: [paymentDateView, detailView, buttonFormPanel]
            });

            var changeUangMukaDialog = new HomeJS.components.ModalDialog({
                title: 'Pelunasan Angsuran',
                model: this.model,
                view: uangMukaFormPanel
            });
            changeUangMukaDialog.show();
            return this;
        },
        sendCommand: function (ev) {
            ev.preventDefault();
            $('#mask , .modal-dialog').fadeOut(300, function () {
                $('#mask').remove();
            });
            var self = this;
            var data = {
                InvoiceId: this.model.get("InvoiceId"),
                Date: this.model.get("Date")
            };
            $.ajax({
                type: "POST",
                url: "/invoice/pelunasan",
                data: data,
                dataType: "json",
                success: function (data) {
                    if (data.error)
                        HomeJS.components.ErrorAlert(data.message);
                    else {
                        self.model.trigger("success");
                    }
                }
            });
        }
    });

    am.invoice.view.Pelunasan.Detail = Backbone.View.extend({
        className: 'pelunasan-detail',
        initialize: function () {
            this.model.on('change', this.render, this);
        },
        render: function () {
            var totalDenda = 0;
            var banyakCicilan = this.model.get("BanyakCicilan") || 0;
            var cicilanYangTelahDiBayar = this.model.get("BanyakCicilanTerbayar") || 0;
            var angsuranDikenaiDenda = [];
            var day = this.model.get("Date").split('-')[0];
            var month = this.model.get("Date").split('-')[1];
            var year = this.model.get("Date").split('-')[2];

            var date = new Date(month + "-" + day + "-" + year);
            if (this.model.get('DueDate') && this.model.get('DueDate').toDateTime() < date) {
                var dueDate = this.model.get("DueDate").toDateTime();
                var angsuranBulanan = parseInt(this.model.get("AngsuranBulanan"));
                var termValue = this.model.get("TermValue");
                var i = 1;
                while (dueDate < date && (cicilanYangTelahDiBayar + i) <= banyakCicilan) {
                    var timeDiff = Math.abs(dueDate.getTime() - date.getTime());
                    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                    var denda = (angsuranBulanan * 0.005) * diffDays;
                    totalDenda += denda;
                    angsuranDikenaiDenda[i - 1] = {
                        AngsuranKe: cicilanYangTelahDiBayar + i,
                        Denda: denda
                    };
                    dueDate = new Date(dueDate.setDate(dueDate.getDate() + termValue));
                    i++;
                }
            }

            var angsuranBulanan = this.model.get("AngsuranBulanan") || 0;
            var sisaTagihan = this.model.get("Outstanding") || 0;
            var sisaTagihanPlusDenda = sisaTagihan + totalDenda;
            var diskon = (sisaTagihan * 0.01);
            var total = sisaTagihanPlusDenda - diskon;
            var html = "";
            html += "<div><div class='first'>Banyak Cicilan</div> <div class='second'>:</div><div class='third'> " + banyakCicilan + " kali</div></div>";
            html += "<div><div class='first'>Banyak Cicilan yang telah dibayar</div><div class='second'>:</div><div class='third'>" + cicilanYangTelahDiBayar + " kali</div></div>";
            html += "<div><div class='first'>Angsuran Bulanan</div><div class='second'>:</div><div class='third'>" + angsuranBulanan.toCurrency() + "</div></div>";
            html += "<div><div class='first'>Sisa Tagihan</div><div class='second'>: </div><div class='third'>" + sisaTagihan.toCurrency() + "</div></div>";
            if (totalDenda > 0) {
                html += "<div class='detail-denda'>Angsuran yang dikenakan denda:";
                for (var i = 0; i < angsuranDikenaiDenda.length; i++) {
                    html += "<div><div class='detail-first'>Angsuran Ke " + angsuranDikenaiDenda[i].AngsuranKe + "</div><div class='detail-second'> :</div> <div class='detail-third'>" + angsuranDikenaiDenda[i].Denda.toCurrency() + "</div></div>";
                }
                html += "</div>";
                html += "<div><div class='first'>Total Denda</div><div class='second'>:</div><div class='third'>" + totalDenda.toCurrency() + "</div></div>";
                html += "<div><div class='first'>Sisa Tagihan + Denda </div><div class='second'>: </div><div class='third'>" + sisaTagihanPlusDenda.toCurrency() + "</div></div>";
            }
            html += "<div><div class='first'>Diskon Pelunasan (1%)</div><div class='second'>: </div><div class='third'>" + diskon.toCurrency() + "</div></div>";
            html += "<div><div class='first'>Total Yang Harus Di Bayar</div><div class='second'>:</div><div class='third'>" + total.toCurrency() + "</div></div>";
            this.$el.html(html);
            return this;
        }
    });
});