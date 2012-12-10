using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class SuratPernyataanTemplate
    {
        public const string DEFAULT = @"
                <style>
                    div, table{ font-size:13px; }
                    .rowheight{padding-top:8px;}
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
                <div style='text-align:center;font-weight:bold;font-size:18px;'>SURAT PERNYATAAN</div>
                <div style='padding-top:50px;'>Yang bertanda tangan dibawah ini :</div>
                <div class='indent rowheight'>
                    <table>
                        <tr><td style='width:100px;text-align:left;'>Nama</td><td style='width:30px;'>:</td><td style='text-align:left;'>$custname$</td></tr>
                        <tr><td style='text-align:left;'>Tanggal Lahir</td><td>:</td><td style='text-align:left;'>$birthday$</td></tr>
                        <tr><td style='text-align:left;'>Jenis Kelamin</td><td>:</td><td style='text-align:left;'>$gender$</td></tr>
                        <tr><td style='text-align:left;'>Alamat</td><td>:</td><td style='text-align:left;'>$billingaddress$ - $city$</td></tr>
                    </table>
                </div>
                <div class='rowheight'>
                    Dengan ini menyatakan sebenarnya bahwa saya sama sekali tidak keberatan dan tidak akan
                    menuntut baik secara  pidana maupun perdata terhadap <b>$organization.OrganizationName$</b> yang akan
                    menarik atau mengambil kendaraan <b>MERK $merk$</b> type <b>$type$</b> saya tersebut dengan meminta
                    bantuan pihak berwajib, oleh karena saya terlambat ataupun menunggak dalam pembayaran
                    Surat Perjanjian Jual Beli No. <b>$suratperjanjianno$</b> Tanggal <b>$suratperjanjiandate$</b>
                </div>
                <div style='padding-top:30px;'>
                    Demikian Surat Pernyataan ini dibuat dengan sebenarnya dalam keadaan sadar 
                    Nomor Mesin <b>$nomesin$</b> Nomor Rangka <b>$norangka$</b> Nomor Polisi <b>$nopolisi$</b> dan tanpa adanya tekanan dan paksaan dari pihak manapun.								
                </div>
                <div style='padding-top:100px;padding-right:50px;text-align:right;'>
                    $organization.City$, $currentdate$
                </div>
                <div style='padding-top:70px;padding-right:110px;text-align:right;'>
                    <b>$custname$</b>
                </div>
            ";
    }
}
