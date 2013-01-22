define([
    'underscore',
    'backbone',
    'namespace'
], function (_, Backbone, ns) {
    ns.define('am.statementreport.model');
   
    am.statementreport.model.TransactionListingReport = Backbone.Model.extend();
   
    am.statementreport.model.TransactionListingReports = Backbone.Collection.extend({
        url: 'statementreport/transactionlisting',
        model: am.statementreport.model.TransactionListingReport,
        SumTotalDebit: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Debit") }, 0);
        },
        SumTotalKredit: function () {
            return this.reduce(function (memo, value) { return memo + value.get("Kredit") }, 0);
        }
    });
    return am;
});