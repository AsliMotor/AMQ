define(['jquery',
        'underscore',
        'backbone',
        'namespace', ],
    function ($, _, Backbone, ns) {
        ns.define("am.report.view");
        am.report.view.TableData = Backbone.View.extend({
            tagName: 'table',
            className: "homejs-datatable table table-striped table-hover table-bordered",
            initialize: function () {
                if (!this.collection)
                    throw ("Must any collection");
                this.collection.on("reset", this.addItems, this);
                this.createHeader();
                this.addItems();
            },
            render: function () {
                return this;
            },
            createHeader: function () {
                if (this.options.headers) {
                    this.$el.append("<thead><tr></tr></thead>");
                    this.options.headers.forEach(this.addHeader, this);
                }
            },
            addHeader: function (header) {
                this.$el.find("thead tr").append(new am.report.view.TableData.TableHead({
                    collection: this.collection,
                    container: this,
                    spec: header
                }).el);
            },
            addItems: function () {
                this.$el.find("tbody").remove();
                if (this.options.items) {
                    this.$el.append("<tbody></tbody>");
                    this.collection.forEach(this.addItem, this);
                    this.createTotalView();
                }
            },
            addItem: function (item) {
                if (item.get("Total") > 0) {
                    this.$el.find("tbody").append(new am.report.view.TableData.Row({
                        model: item,
                        cells: this.options.items,
                        eventclick: this.options.eventclick
                    }).el);
                }
            },
            showNoDataResult: function () {
                var cellLength = this.options.items.length;
                this.$el.find("tbody").append("<tr><td colspan='" + cellLength + "' style='text-align:center;color:red;'>Tidak ada data</td></tr>");
            },
            createTotalView: function () {
                var grandTotal = this.collection.SumTotal();
                if (this.collection.length > 0 && grandTotal > 0) {
                    var cellLength = this.options.items.length;
                    this.$el.find("tbody").append("<tr><td colspan='" + cellLength + "' style='text-align:right;'><b>Total : " + grandTotal.toCurrency() + "</b></td></tr>");
                }
                else {
                    this.showNoDataResult();
                }
            }
        });

        am.report.view.TableData.TableHead = Backbone.View.extend({
            tagName: "th",
            initialize: function () {
                var header = this.options.spec;
                if (typeof header === "object") {
                    var resizable = (header.resizable || header.resizable === undefined) ? "" : "fixed";
                    var width = (header.width) ? "width:" + header.width : "";
                    var align = (header.align) ? "text-align:" + header.align : "";
                    var minwidth = (header.minwidth) ? "min-width:" + header.minwidth : "";
                    var maxwidth = (header.maxwidth) ? "max-width:" + header.maxwidth : "";
                    var styles = width + ";" + align + ";" + minwidth + ";" + maxwidth;
                    var title = (header.title) ? header.title : "";

                    this.$el.addClass(resizable);
                    this.$el.attr('title', title);
                    this.$el.attr('style', styles);
                    this.$el.html(header.name);
                }
                else {
                    this.$el.append(header);
                }
            }
        });

        am.report.view.TableData.Row = Backbone.View.extend({
            tagName: "tr",
            initialize: function () {
                this.options.cells.forEach(this.addCell, this);
            },
            addCell: function (cell) {
                if (typeof cell === "object") {
                    var align = (cell.align) ? "text-align:" + cell.align : "";
                    var styles = align;
                    var value = this.model.get(cell.dataIndex);
                    if (cell.onrender) {
                        value = cell.onrender(value);
                    }
                    this.$el.append("<td style='" + styles + "'>" + value + "</td>");
                }
            }
        });
    });