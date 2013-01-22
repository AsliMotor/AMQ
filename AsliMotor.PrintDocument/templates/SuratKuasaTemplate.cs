using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class SuratKuasaTemplate
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
                <div style='text-align:center;font-weight:bold;font-size:18px;'>SURAT KUASA</div>
                <div style='text-align:center;'>
                    <table style='margin:0 auto;'>
                        <tr><td style='width:30px;text-align:left;'>Nomor</td><td style='width:15px;'>:</td><td style='text-align:left;'>$suratperjanjianno$</td></tr>
                    </table>
                </div>
                <div style='padding-top:30px;'>Yang bertanda tangan dibawah ini :</div>
                <div style='text-align:center'><b>$custname$</b></div>
                <div style='padding-top:30px;'>
                    Pekerjaan $job$ Beralamat di $billingaddress$ $city$
                    Kartu Tanda Penduduk No	$noktp$ dikeluarkan oleh $ktppublisher$
                    tanggal	$ktpdate$ menyatakan mampu dan tidak pailit dan sanggup untuk  pengambilan angsuran
                    kredit Kendaraan Roda Dua <b>MERK $merk$</b> Perjanjian Jual Beli Dengan Pembayaran Angsuran
                    Nomor:	$suratperjanjianno$ tertanggal $suratperjanjiandate$ dan Perjanjian
                    Penyerahan Hak Milik Secara Fiducia Nomor: $suratperjanjianno$
                    MERK $merk$ Model/Type $type$
                    Warna $warna$ No. Mesin <b>$nomesin$</b> No. Rangka <b>$norangka$</b>
                    ( selanjutnya  disebut  'Kendaraan' ) oleh dan antara Pemberi Kuasa: $organization.OrganizationName$
                    berkedudukan / berkantor di $organization.OrganizationAddress$ $organization.City$ (selanjutnya disebut
                    Penerima Kuasa), dengan ini memberi kuasa penuh kepada Penerima Kuasa dengan Hak Subtitusi:
                </div>
                <div style='text-align:center;padding-top:20px;'><b>KHUSUS</b></div>
                <div class='indent rowheight'>
                    <table>
                        <tr><td style='width:10px;text-align:left;'>1.</td><td style='text-align:left;'>Untuk melakukan pemilikan atas kendaraan sebagaimana layaknya seorang pemilik.</td></tr>
                        <tr>
                            <td style='text-align:left;vertical-align:top;'>2. </td>
                            <td style='text-align:left;vertical-align:top;'>
                                Untuk menjual atau dengan cara atau nama apapun mengalihkan milik atas Kendaraan
	                            pada pihak ketiga manapun dengan prosedur dan harga yang ditetapkan sendiri oleh
	                            Penerima Kuasa, baik melalui lelang ataupun dengan penjualan langsung, melakukan
	                            perhitungan atas jumlah sisa Harga Pembelian yang terhutang dan sehubungan dengan
	                            hal-hal tersebut menandatangani perjanjian-perjanjian dan dokumen-dokumen serta
	                            surat-surat yang diperlukan.
                            </td>
                        </tr>
                        <tr>
                            <td style='text-align:left;vertical-align:top;'>3. </td>
                            <td style='text-align:left;vertical-align:top;'>
                                Pada umumnya untuk melakukan segala hal dan tindakan lain yang diperlukan
	                            sehubungan hal-hal tersebut diatas tanpa ada yang dikecualikan.
                            </td>
                        </tr>
                    </table>
                </div>
                <div style='padding-top:30px;'>
                    Demikianlah Surat Kuasa	ini dibuat dan ditanda tangani oleh Pemberi Kuasa pada hari ini
                    $ddddmmmmyyyy$ di $organization.OrganizationAddress$.		
                    dan hanya dapat dicabut kembali bilamana semua jumlah terhutang oleh Permberi Kuasa
                    kepada Penerima	Kuasa menurut Perjanjian Jual Beli Dengan Pembayaran Angsuran dan
                    Perjanjian Penyerahan Hak Milik Secara Fiducia tersebut telah terbayar lunas seluruhnya.
                </div>
                <div>
                    <div style='float:left;'>
                        <div style='padding-top:100px;padding-left:50px;text-align:center;'>
                            Penerima Kuasa,
                            <br />
                            $organization.OrganizationName$
                        </div>
                        <div style='padding-top:70px;padding-left:50px;text-align:center;'>
                            ($organization.Pimpinan$)
                            <br />
                            (Pimpinan)
                        </div>
                    </div>
                    <div style='float:right;'>
                        <div style='padding-top:100px;padding-right:50px;text-align:center;'>
                            $organization.City$, $currentdate$
                        </div>
                        <div style='padding-top:70px;padding-right:50px;text-align:center;'>
                            $custname$
                        </div>
                    </div>
                </div>";
    }
}
