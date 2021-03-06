﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsliMotor.Products.Models;

namespace AsliMotor.Products
{
    public interface IProductRepository
    {
        Product GetProductById(Guid id, string branchId);
        Product GetByNoPolisi(string nopolisi, string branchid);
        Product GetByNoRangka(string norangka, string branchid);
        Product GetByNoMesin(string nomesin, string branchid);
        Product GetByNoBpkb(string nobpkp, string branchid);
        TypeProduct GetTypeByName(string type, string branchid);
        IList<TypeProduct> SearchType(string key, string branchid);
        IList<ProductReport> GetListView(string branchid, int offset, string status);
        IList<ProductReport> SearchListView(string branchId, int offset, string key);
        TotalProduct GetTotalList(string branchid, string status);
        IList<ProductSearch> Search(string branchid, string key);
        IList<TypeProduct> GetAllType(string branchid);
    }
}
