using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class SuratPernyataanKreditTemplate
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
    <div>
        <div style='text-align:center;font-weight:bold;font-size:18px;'>SURAT PERNYATAAN KREDIT</div>
	    <div style='margin-top:30px;'>Saya yang bertanda tangan dibawah ini :</div>
        <div class='rowheight indent'>
            <table>
                <tr><td width='100px'>Nama</td><td width='20px'>:</td><td>$custname$</td></tr>
                <tr><td>No. KTP</td><td>:</td><td>$noktp$</td></tr>
                <tr><td>Umur</td><td>:</td><td>$ktpdate$</td></tr>
                <tr><td>Alamat</td><td>:</td><td>$billingaddress$ - $city$</td></tr>
                <tr><td>Pekerjaan</td><td>:</td><td>$job$</td></tr>
            </table>
        </div>
	    <div class='rowheight'>
            Dengan ini menyatakan mampu dan tidak pailit dan sanggup untuk pengambilan angsuran
            kredit Kendaraan Roda Dua <b>MERK $merk$</b> dari Dealer “$organization.OrganizationName$” $organization.OrganizationAddress$ $organization.City$.
        </div>
        <div class='rowheight indent'>
            <table>
                <tr><td width='100px'>Type</td><td width='20px'>:</td><td>$type$</td></tr>
                <tr><td>Nomor POLISI</td><td>:</td><td>$nopolisi$</td></tr>
                <tr><td>Warna</td><td>:</td><td>$warna$</td></tr>
                <tr><td>Tahun</td><td>:</td><td>$tahun$</td></tr>
                <tr><td>Nomor Mesin</td><td>:</td><td>$nomesin$</td></tr>
                <tr><td>Nomor Rangka</td><td>:</td><td>$norangka$</td></tr>
            </table>
        </div>
        <div class='rowheight'>Saya sanggup, mampu dan patuh terhadap syarat-syarat di bawah ini dan menyatakan dengan sebenarnya :</div>
        <div class='rowheight indent'>
            <table>
                <tr>
                    <td width='20px'>1.</td>
                    <td>
                        Uang Muka (DP) yang saya bayar adalah sebesar <b>Rp $uangmuka$</b>
	                    ($uangmukaterbilang$)							
	                    Lamanya angsuran kredit : <b>$lamaangsuran$ Bulan</b> ($lamaangsuranterbilang$ bulan)
                        dan dibayar sebanyak <b>$banyakcicilan$</b> kali. 
	                    Besar angsuran kredit :	 <b>Rp $angsuranbulanan$</b> ($angsuranbulananterbilang$ rupiah)
                    </td>
                </tr>
                <tr>
                    <td>2.</td>
                    <td>
                        Saya bersedia membayar denda keterlambatan tunggakan angsuran sebesar 0.5% dari besar angsuran per hari.							
                    </td>
                </tr>
                <tr>
                    <td>3.</td>
                    <td>
                        Saya bersedia ditagih di rumah/kantor saya apabila saya tidak datang setor angsur-an kredit bulanan sepeda motor saya.
                    </td>
                </tr>
                <tr>
                    <td>4.</td>
                    <td>
                        Saya bersedia sepeda motor saya ditarik oleh pihak Dealer “$organization.OrganizationName$” jika terjadi tunggakan 1 bulan 2 minggu dari tanggal jatuh tempo kredit.
                    </td>
                </tr>
                <tr>
                    <td>5.</td>
                    <td>
                        Saya bersedia sepeda motor saya dijual oleh  pihak Dealer  apabila setelah terjadi
	                    penarikan 1 minggu saya masih tidak menyelesaikan tunggakan tersebut diatas dan
	                    semua angsuran dan uang muka yang telah saya bayar bersedia dihapuskan.							
                    </td>
                </tr>
                <tr>
                    <td>6.</td>
                    <td>
                        Saya bersedia dikenakan biaya penarikan sebesar Rp. 150.000,-  (Seratus lima puluh ribu rupiah)						
                    </td>
                </tr>
                <tr>
                    <td>7.</td>
                    <td>
                        Saya bersedia langsung datang bayar angsuran ke Kantor Dealer “$organization.OrganizationName$”.
                    </td>
                </tr>
                <tr>
                    <td>8.</td>
                    <td>
                        Saya bersedia memberitahukan kepada Dealer “$organization.OrganizationName$” Jika saya pindah ke alamat baru (pindah rumah).							
                    </td>
                </tr>
                <tr>
                    <td>9.</td>
                    <td>
                        Saya bersedia menyerahkan sepeda motor dengan baik ataupun dalam kondisi 100% lengkap jika terjadi penarikan dari pihak Dealer “$organization.OrganizationName$“.	
                    </td>
                </tr>
                <tr>
                    <td>10.</td>
                    <td>
                        Saya bersedia untuk tidak menjual / menggadaikan sepeda motor tersebut diatas kepada pihak lain, atau jika saya menjual akan saya laporkan kepada pihak “$organization.OrganizationName$”.							
                    </td>
                </tr>
                <tr>
                    <td>11.</td>
                    <td>
                        Surat pernyataan ini telah saya baca dengan teliti dan saya menerima ketentuan-ketentuan yang telah ditetapkan oleh pihak '$organization.OrganizationName$'.
                    </td>
                </tr>
                <tr>
                    <td>12.</td>
                    <td>
                        Jika saya melanggar / tidak mematuhi ketentuan yang telah ditetapkan oleh pihak
                        “$organization.OrganizationName$” saya bersedia diajukan pada hukum yang berlaku sesuai dengan surat perjanjian '$organization.OrganizationName$'.							
                    </td>
                </tr>
            </table>
        </div>
        <div class='rowheight'>
            Demikianlah Surat Pernyataan ini saya buat dengan sebenarnya di hadapan Pihak Pemasaran
            $organization.OrganizationName$ tanpa ada paksaan dan tuntutan dari pihak manapun juga.								
        </div>
        <div style='padding-top:20px;padding-right:50px;text-align:right;'>
            $organization.City$, $currentdate$
        </div>
        <div style='padding-top:70px;padding-right:100px;text-align:right;'>
            $custname$
        </div>
    </div>";
    }
}
