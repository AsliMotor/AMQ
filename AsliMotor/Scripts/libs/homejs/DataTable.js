define(['jquery',
        'underscore',
        'backbone',
        'namespace',
        '../../libs/resizable-tables'],
    function ($, _, Backbone, ns) {
        ns.define("HomeJS.components");
        HomeJS.components.DataTable = Backbone.View.extend({
            tagName: 'table',
            className: "homejs-datatable table table-striped table-hover  table-bordered",
            initialize: function () {
                this.$el.attr('id', 'test');
                if (!this.collection)
                    throw ("Must any collection");
                this.collection.on("reset", this.addItems, this);
                this.collection.on("add", this.addItem, this);
                if (this.options.resizable) this.$el.addClass("resizable");
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
                this.$el.find("thead tr").append(new HomeJS.components.DataTable.TableHead({
                    collection: this.collection,
                    container: this,
                    spec: header
                }).el);
            },
            addItems: function () {
                if (this.options.items) {
                    this.$el.find("tbody").remove();
                    this.$el.append("<tbody></tbody>");
                    this.collection.forEach(this.addItem, this);
                }
            },
            addItem: function (item) {
                this.$el.find("tbody").append(new HomeJS.components.DataTable.Row({
                    model: item,
                    cells: this.options.items,
                    eventclick: this.options.eventclick
                }).el);
            }
        });

        HomeJS.components.DataTable.TableHead = Backbone.View.extend({
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
            },
            events: {
                'click': 'sortData'
            },
            sortData: function () {
                var dataIndex = this.options.spec.dataIndex;
                if (dataIndex) {
                    this.orderBy = (this.orderBy && this.orderBy === "-") ? "" : "-";
                    if (this.orderBy === "-") {
                        this.collection.comparator = function (item) {
                            var value = item.get(dataIndex);
                            if (typeof value === "string") {
                                value = value.toLowerCase();
                                value = value.split("");
                                value = _.map(value, function (letter) {
                                    return String.fromCharCode(-(letter.charCodeAt(0)));
                                });
                                return value;
                            }
                            else {
                                return -item.get(dataIndex);
                            }
                        }
                    } else {
                        this.collection.comparator = function (item) {
                            var value = item.get(dataIndex);
                            if (typeof value === "string") {
                                value = value.toLowerCase();
                                value = value.split("");
                                value = _.map(value, function (letter) {
                                    return String.fromCharCode((letter.charCodeAt(0)));
                                });
                                return value;
                            }
                            else {
                                return item.get(dataIndex);
                            }
                        }
                    }
                    this.collection.sort();
                    this.setCurrentSortedField();
                }
            },
            setCurrentSortedField: function () {
                $("span.currentSortedArrow", this.options.container.el).remove();
                if (this.orderBy === "-")
                    this.$el.children().append("<span class='currentSortedArrow'><img src='/Content/img/asc.gif'/></span>");
                else
                    this.$el.children().append("<span class='currentSortedArrow'><img src='/Content/img/desc.gif'/></span>");
            }
        });

        HomeJS.components.DataTable.Row = Backbone.View.extend({
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
            },
            events: {
                'click': 'trclicked'
            },
            trclicked: function () {
                console.log(this.model.toJSON());
                this.options.eventclick(this.model.toJSON());
            }
        });
    });