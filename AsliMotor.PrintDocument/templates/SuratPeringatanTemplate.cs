using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class SuratPeringatanTemplate
    {
        public const string DEFAULT = @"
    <style>
        .rowheight{padding-top:10px;}
        .indent{padding-left:70px;}
        table#detail td{vertical-align:top;border:1px solid #000;padding:4px;}
        table#detail th{vertical-align:top;border:1px solid #000;padding:4px;}
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
    <div style='text-align:center;font-weight:bold;font-size:18px;'>SURAT PERINGATAN MOTOR</div>
    <div style='text-align:center;'>
        <table style='margin:0 auto;font-size:12px'>
            <tr><td style='width:60px;text-align:left;'>Nomor</td><td style='width:30px;'>:</td><td style='text-align:left;'>$SuratPeringatanNo$</td></tr>
            <tr><td style='width:60px;text-align:left;'>Tanggal</td><td>:</td><td style='text-align:left;'>$SuratPeringatanDate$</td></tr>
        </table>
    </div>
    <div style='padding-top:50px;'>Kepada,</div>
    <div><b>$data.CustomerName$</b></div>
    <div class='' style='padding-left:20px;'>
        <div>$data.CustomerAddress$</div>    
        <div>$data.CustomerCity$ - $data.CustomerRegion$</div>
        <div>(telp. $data.CustomerPhone$)</div>    
    </div>
    <div style='padding-top:30px;'>
        Dengan hormat,
    </div>
    <div class='rowheight'>
        Sehubungan dengan tunggakan angsuran Saudara, atas pembelian secara kredit 1 unit kendaraan, Yang terdiri dari :
    </div>
    <div class='rowheight'>
        <table style='border:1px solid #000;' id='detail' border='0px' cellspacing='0px' cellpadding:'0px'>
            <thead>
                <tr>
                    <th style='width:30px;text-align:center;'>No</th>
                    <th style='width:125px;text-align:left;'>Angsuran Ke</th>
                    <th style='width:190px;text-align:left;'>Jatuh Tempo</th>
                    <th style='width:170px;text-align:right;'>Jumlah Angsuran</th>
                    <th style='width:160px;text-align:right;'>Denda</th>
                    <th style='width:160px;text-align:right;'>Total</th>
                </tr>
            </thead>
            <tbody>
                $Items:{
                    <tr>
                        <td style='text-align:center;'>$it.No$</td>
                        <td>Angsuran ke $it.AngsuranKe$</td>
                        <td style='text-align:left;'>$it.DueDate$</td>
                        <td style='text-align:right;'>Rp $it.StringAngsuran$</td>
                        <td style='text-align:right;'>Rp $it.StringDenda$</td>
                        <td style='text-align:right;'>Rp $it.StringTotal$</td>
                    </tr>
                }$
            </tbody>
            <tfoot>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td style='text-align:right;'><b>Total</b></td>
                    <td style='text-align:right;'><b>Rp $NetTotal$</b></td>
                </tr>
            </tfoot>
        </table>
    </div>
    <div style='padding-top:15px;'>
        Saudara tidak menyelesaikan angsuran kredit kendaraan bermotor merk <b>$data.Merk$</b> type <b>$data.Type$</b>
        warna <b>$Warna$</b> nomor rangka <b>$NoRangka$</b> nomor mesin <b>$NoMesin$</b> nomor polisi <b>$data.NoPolisi$</b>,
        maka dengan sangat terpaksa kendaraan Saudara kami <b>TARIK / AMBIL KEMBALI</b>.
    </div>
    <div style='padding-top:15px;'>
        Untuk penyelesaian lebih lanjut, harap Saudara datang ke kantor kami $organization.OrganizationName$ paling lambat 3 (tiga) hari setelah 
        tanggal penarikan dikeluarkan. Jika pada tanggal tersebut Saudara tidak datang, maka semua hak Saudara akan kami hapuskan sesuai
        dengan surat perjanjian No. <b>$data.NoSuratPerjanjian$</b> tanggal $SuratPerjanjianDate$.
    </div>
    <div style='padding-top:100px;padding-right:50px;text-align:right;'>
        $organization.City$, $currentDate$
    </div>
    <div style='padding-top:70px;padding-right:100px;text-align:right;'>
        $organization.OrganizationName$
    </div>";
    }
}
