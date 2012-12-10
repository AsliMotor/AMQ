using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class SuratPernyataanMampuTemplate
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
                <div style='text-align:center;font-weight:bold;font-size:18px;'>SURAT PERNYATAAN MAMPU</div>
                <div style='padding-top:50px;'>Saya yang bertanda tangan dibawah ini :</div>
                <div class='indent rowheight'>
                    <table>
                        <tr><td style='width:100px;text-align:left;'>Nama</td><td style='width:30px;'>:</td><td style='text-align:left;'>$custname$</td></tr>
                        <tr><td style='text-align:left;'>Tanggal Lahir</td><td>:</td><td style='text-align:left;'>$birthday$</td></tr>
                        <tr><td style='text-align:left;'>Jenis Kelamin</td><td>:</td><td style='text-align:left;'>$gender$</td></tr>
                        <tr><td style='text-align:left;'>Alamat</td><td>:</td><td style='text-align:left;'>$billingaddress$</td></tr>
                        <tr><td style='text-align:left;'>Pekerjaan</td><td>:</td><td style='text-align:left;'>$job$</td></tr>
                    </table>
                </div>
                <div class='rowheight'>
                    Dengan ini menyatakan mampu dan tidak pailit dan sanggup untuk pengambilan angsuran
                    kredit Kendaraan Roda Dua <b>MERK $merk$</b> dari <b>$organization.OrganizationName$ $organization.OrganizationAddress$ $organization.city$</b>
                    <div class='indent rowheight'>
                        <table>
                            <tr><td style='width:100px;text-align:left;'>Model/Type</td><td style='width:30px;'>:</td><td style='text-align:left;'>$type$</td></tr>
                            <tr><td style='text-align:left;'>Warna</td><td>:</td><td style='text-align:left;'>$warna$</td></tr>
                            <tr><td style='text-align:left;'>Tahun</td><td>:</td><td style='text-align:left;'>$tahun$</td></tr>
                            <tr><td style='text-align:left;'>Nomor Mesin</td><td>:</td><td style='text-align:left;'>$nomesin$</td></tr>
                            <tr><td style='text-align:left;'>Nomor Rangka</td><td>:</td><td style='text-align:left;'>$norangka$</td></tr>
                            <tr><td style='text-align:left;'>Nomor Polisi</td><td>:</td><td style='text-align:left;'>$nopolisi$</td></tr>
                        </table>
                    </div>
                </div>
                <div class='rowheight'>
                    Saya sanggup dan mampu dan patuh terhadap syarat-syarat yang di bawah ini:
                    <div class='indent rowheight'>
                        <table>
                            <tr><td style='width:20px;text-align:left;'>1.</td><td style='text-align:left;'>Saya sanggup dikenakan denda sesuai dengan Surat Perjanjian Jual Beli. 1 Bh Buku Petunjuk</td></tr>
                            <tr><td style='text-align:left;'>2. </td><td style='text-align:left;'>Saya bersedia diambil kembali angsuran selama 2 (dua) bulan.</td></tr>
                            <tr><td style='text-align:left;'>3. </td><td style='text-align:left;'>Saya bersedia dikenakan denda Rp. 25.000,- (dua puluh lima ribu Rupiah) apabila saya ambil kembali kendaraan yang tunggak kredit selama 2 (dua) bulan.</td></tr>
                            <tr><td style='text-align:left;'>4. </td><td style='text-align:left;'>Saya bersedia kendaraan saya	diambil penuh oleh Toko  $organization.OrganizationName$, apabila saya tunggak kredit selama dua setengah bulan, semua angsuran dan uang muka saya juga bersedia dihapuskan.</td></tr>
                            <tr><td style='text-align:left;'>5. </td><td style='text-align:left;'>Saya bersedia tidak diberikan perpanjangan STNK, apabila saya tunggak kredit selama 1 (satu) bulan keatas.</td></tr>
                            <tr><td style='text-align:left;'>6. </td><td style='text-align:left;'>Saya bersedia langsung datang bayar angsuran ke Kantor $organization.OrganizationName$.</td></tr>
                            <tr><td style='text-align:left;'>7. </td><td style='text-align:left;'>Saya bersedia tanpa alasan mengenai tunggakan kredit kendaraan yang saya kredit, saya tidak akan mencari alasan-alasan untuk menutup kesalahan saya.</td></tr>
                            <tr><td style='text-align:left;'>8. </td><td style='text-align:left;'>Saya bersedia apabila saya pindah rumah saya akan beritahukan kepada $organization.OrganizationName$, alamat saya yang baru.</td></tr>
                            <tr><td style='text-align:left;'>9. </td><td style='text-align:left;'>Saya bersedia tidak akan menggugat $organization.OrganizationName$, apabila kendaraan saya sudah dikembalikan atau ditarik ataupun diambil $organization.OrganizationName$, sehubungan dengan tunggakan kredit kendaraan adalah kesalahan saya.</td></tr>
                            <tr><td style='text-align:left;'>9. </td><td style='text-align:left;'>Saya bersedia membayar angsuran kredit sampai lunas, apabila dikemudian hari kendaraan saya terbakar, rusak, hancur, dan hilang dicuri orang.</td></tr>
                        </table>
                    </div>
                </div>
                <div style='padding-top:30px;'>
                    Demikianlah Surat Pernyataan Mampu ini saya perbuat dengan sadar, dan penuh tanggung jawab, surat ini dapat dijadikan pegangan/pedoman oleh pihak yang berkepentingan.
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
