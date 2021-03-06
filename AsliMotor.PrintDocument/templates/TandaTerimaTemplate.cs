﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class TandaTerimaTemplate
    {
        public const string DEFAULT = @"
            <style>
                .rowheight{padding-top:10px;}
                .indent{padding-left:70px;}
                table td{vertical-align:top;}
            </style>
            <div style='display:inline-block;width:100%;padding-top:0.5cm; font-family:Tahoma;'>
                <div style='float:left'>
	                <div width='100%' style='font-weight:bold; font-size:18px'>$organization.OrganizationName$</div>
	                <div style='width:100%; font-size:14px'>$organization.OrganizationAddress$</div>
                    <div width='100%' style='font-size:14px'>$organization.City$ - $organization.Country$</div>
	                <div width='100%' style='font-size:14px'>Telp. $organization.Telp$</div>
                </div>
                <div style='float:right'>
	                <img style='float:right;height:60px;' src='data:image/png;base64,$logodata$' />
                </div>
            </div>
            <hr/>
            <div style='text-align:center;font-weight:bold;font-size:18px;'>TANDA TERIMA</div>
            <div style='padding-top:50px;'>Saya yang bertanda tangan dibawah ini :</div>
            <div class='indent rowheight'>
                <table>
                    <tr><td style='width:100px;text-align:left;'>Nama</td><td style='width:30px;'>:</td><td style='text-align:left;'>$custname$</td></tr>
                    <tr><td style='text-align:left;'>Nomor KTP</td><td>:</td><td style='text-align:left;'>$noktp$</td></tr>
                    <tr><td style='text-align:left;'>Alamat</td><td>:</td><td style='text-align:left;'>$billingaddress$</td></tr>
                    <tr><td style='text-align:left;'>Pekerjaan</td><td>:</td><td style='text-align:left;'>$job$</td></tr>
                </table>
            </div>
            <div class='rowheight'>
                Dengan ini menyatakan  mampu  dan  tidak  pailit dan  sanggup untuk  pengambilan angsuran
                kredit Kendaraan Roda Dua MERK $merk$
                Dengan ini menyatakan telah menerima 1 ( satu ) Unit sepeda motor						
                beserta perlengkapan sebagai berikut :						
            </div>
            <div class='rowheight indent'>
                <table>
                    <tr><td style='width:100px;text-align:left;'>Model/Type</td><td style='width:30px;'>:</td><td style='text-align:left;'>$type$</td></tr>
                    <tr><td style='text-align:left;'>Warna</td><td>:</td><td style='text-align:left;'>$warna$</td></tr>
                    <tr><td style='text-align:left;'>Tahun</td><td>:</td><td style='text-align:left;'>$tahun$</td></tr>
                    <tr><td style='text-align:left;'>Nomor Mesin</td><td>:</td><td style='text-align:left;'>$nomesin$</td></tr>
                    <tr><td style='text-align:left;'>Nomor Rangka</td><td>:</td><td style='text-align:left;'>$norangka$</td></tr>
                    <tr><td style='text-align:left;'>Nomor Polisi</td><td>:</td><td style='text-align:left;'>$nopolisi$</td></tr>
                    <tr>
                        <td style='text-align:left;'>Keterangan</td>
                        <td>:</td>
                        <td style='text-align:left;'>
                            <div>1 BH Helm</div>
                            <div>1 SET Spion</div>
                            <div>1 SET Toolkit</div>
                            <div>1 BH Jaket</div>
                            <div>1 Lembar STNK Sementara</div>
                            <div>1 BH Buku Petunjuk</div>
                            <div>1 BH Buku Service</div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style='padding-top:100px;padding-right:50px;text-align:right;'>
                $organization.City$, $currentdate$
            </div>
            <div style='padding-top:70px;padding-right:110px;text-align:right;'>
                $custname$
            </div>
        ";
    }
}
