define(['jquery', 'underscore', 'backbone', 'namespace'], function ($, _, Backbone, ns) {
    ns.define('am');

    am.RoleName = {
        ADMINISTRATOR: "Administrator",
        OWNER: "Owner",
        ADMINPURCHASE: "AdminPurchase",
        ADMINSALES: "AdminSales",
        CASHIER: "Cashier",
        OWNER : "Owner",
        OWNER_ADMINSALES: "Owner, AdminSales",
        OWNER_ADMINSALES_CASHIER: "Owner, AdminSales, Cashier",
        OWNER_ADMINPURCHASE: "Owner, AdminPurchase",
        OWNER_ADMINPURCHASE_CASHIER: "Owner, AdminPurchase, Cashier",
        OWNER_ADMINISTRATOR: "Owner, Administrator"
    };

    return am.RoleName;
});