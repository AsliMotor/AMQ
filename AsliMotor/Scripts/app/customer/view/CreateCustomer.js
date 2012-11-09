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
    '../../../libs/homejs/inputfield/textarea',
    '../../../libs/homejs/radiobutton'
], function ($, _, Backbone, ns, am) {
    ns.define("am.customer.view");
    am.customer.view.EditCustomer = Backbone.View.extend({
        className: "edit-customer",
        initialize: function () {
        },
        render: function () {
            var namaView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nama Pelanggan',
                placeholder: 'Ketik nama pelanggan',
                required: true,
                dataIndex: "Name"
            });

            var alamatView = new HomeJS.components.TextArea({
                model: this.model,
                title: 'Alamat',
                placeholder: 'Ketik alamat pelanggan',
                dataIndex: "Address"
            });

            var cityView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Kota',
                placeholder: 'Ketik kota pelanggan',
                dataIndex: "City"
            });

            var regionView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Kabupaten',
                placeholder: 'Ketik nama kabupaten',
                dataIndex: "Region"
            });

            var phoneView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Telpon',
                placeholder: 'Ketik nomor telpon',
                dataIndex: "Phone"
            });

            var birthdayView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal Lahir',
                dataIndex: "Birthday"
            });

            var jobView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Pekerjaan',
                placeholder: 'Ketik pekerjaan pelanggan',
                dataIndex: "Job"
            });

            var emailView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Email',
                placeholder: 'Jika ada',
                dataIndex: "Email"
            });

            var ktpNoView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Nomor KTP',
                placeholder: 'Ketik nomor ktp',
                required: true,
                dataIndex: "KTPNo"
            });

            var ktpPublisherView = new HomeJS.components.TextField({
                model: this.model,
                title: 'Penerbit',
                placeholder: 'Ketik nama penerbit ktp',
                dataIndex: "KTPPublisher"
            });

            var ktpDateView = new HomeJS.components.DateField({
                model: this.model,
                title: 'Tanggal KTP Dikeluarkan',
                dataIndex: "KTPDate"
            });

            var RadioButtonCollection = Backbone.Collection.extend();
            var radioButtonCollection = new RadioButtonCollection();
            radioButtonCollection.reset([
                           { id: "pria", title: "Pria", value: "Pria", checked: true },
                           { id: "wanita", title: "Wanita", value: "Wanita" }
                        ]);

            var genderView = new HomeJS.components.RadioButton({
                collection: radioButtonCollection,
                model: this.model,
                id: "gender",
                name: "gender",
                title: "Jenis Kelamin",
                dataIndex: "Gender",
                setModel: function (model, data) {
                    model.set("Gender", data.value);
                }
            });

            var formPanel = new HomeJS.components.FormPanel({
                formLayout: HomeJS.components.FormLayout.HORIZONTAL,
                items: [namaView, alamatView, cityView, regionView, ktpNoView, ktpPublisherView, ktpDateView, birthdayView, emailView, jobView, phoneView, genderView]
            });

            this.$el.html(formPanel.render().el);
            return this;
        }
    });

    return am;
});