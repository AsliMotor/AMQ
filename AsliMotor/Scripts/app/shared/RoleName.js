define(['jquery', 'underscore', 'backbone', 'namespace'], function ($, _, Backbone, ns) {
    ns.define('am');

    am.RoleName = {
        ADMINISTRATOR: "Administrator",
        OWNER: "Owner",
        ADMINPURCHASE: "AdminPurchase",
        ADMINSALES: "AdminSales",
        CASHIER: "Cashier",
        ADMINISTRATOR_OWNER : "Administrator, Owner",
        ADMINISTRATOR_OWNER_ADMINSALES: "Administrator, Owner, AdminSales",
        ADMINISTRATOR_OWNER_ADMINSALES_CASHIER: "Administrator, Owner, AdminSales, Cashier",
        ADMINISTRATOR_OWNER_ADMINPURCHASE: "Administrator, Owner, AdminPurchase",
        ADMINISTRATOR_OWNER_ADMINPURCHASE_CASHIER: "Administrator, Owner, AdminPurchase, Cashier"
    };

    return am.RoleName;
});