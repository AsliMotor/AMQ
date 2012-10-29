define([
    'jquery',
    'underscore',
    'backbone',
    'namespace',
    '../../backbone/utils'
], function ($, _, Backbone, ns) {
    ns.define("HomeJS.components");
    HomeJS.components.TextField = Backbone.View.extend({
        tagName: 'div',
        className: 'control-group',
        initialize: function () {
            this.model.on("change:" + this.options.dataIndex, this.render, this);
        },
        render: function () {
            var value = this.model.get(this.options.dataIndex) || "";
            var required = (this.options.required === true) ? "required" : "";
            var colorLabel = (this.options.required === true) ? "#9C0000" : "";
            var readonly = (this.options.readonly === true) ? "readonly" : "";
            var placeholder = (this.options.placeholder) ? this.options.placeholder : "";
            this.id = this.options.dataIndex;

            var type = this.options.type || "text";
            var size = this.options.size || "input-large";
            var html = "";

            if (this.options.title && this.options.title != "") {
                html = "<label class='control-label' style='color:" + colorLabel + "'>" + this.options.title + "</label>\
                        <div class='controls controls-row'>\
                            <input type='" + type + "' id='" + this.options.dataIndex + "' name='" + this.options.dataIndex + "' class='" + size + "' placeholder='" + placeholder + "' value='" + value + "'  " + required + " " + readonly + "/>\
                            <div class='help-inline'></div>\
                        </div>";
            } else {
                html = "<input type='" + type + "' id='" + this.options.dataIndex + "' name='" + this.options.dataIndex + "' class='" + size + "' placeholder='" + placeholder + "' value='" + value + "'  " + required + " " + readonly + "/><div class='help-inline'></div>";
            }
            this.$el.html(html);
            return this;
        },
        events: {
            'change input': 'setValue'
        },
        setValue: function () {
            if (this.options.setValue)
                this.options.setValue(this);
            else
                this.model.set(this.options.dataIndex, $('input', this.$el).val());

            var check = this.model.validateItem(this.options.dataIndex);
            if (check.isValid === false) {
                utils.addValidationError(this.options.dataIndex, check.message);
            } else {
                utils.removeValidationError(this.options.dataIndex);
            }

            if ($('textarea', this.$el.next())[0])
                $('textarea', this.$el.next()).focus();
            else if ($('input', this.$el.next())[0])
                $('input', this.$el.next()).focus();

        }
    });
});