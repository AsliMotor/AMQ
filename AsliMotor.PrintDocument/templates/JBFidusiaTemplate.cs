﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class JBFidusiaTemplate
    {
        public const string DEFAULT = @"
            <style>
                div, table{ font-size:13px; }
                .rowheight{padding-top:5px;}
                .indent{padding-left:50px;}
                table td{vertical-align:top;}
                table th{vertical-align:top;}
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
            <div style='text-align:center;font-weight:bold;font-size:18px;'>PERJANJIAN JUAL BELI DENGAN PEMBAYARAN ANGSURAN</div>
            <div style='text-align:center;'>
                <table style='margin:0 auto;'>
                    <tr><td style='width:60px;text-align:left;'>Nomor</td><td style='width:30px;'>:</td><td style='text-align:left;'>$suratperjanjianno$</td></tr>
                </table>
            </div>
            <div style='padding-top:30px;'>
                Perjanjian Penyerahan Hak Milik Secara fiducia ini  dibuat  dan ditanda tangani tanggal $suratperjanjiandate$ oleh dan antara :					
            </div>
            <div class='indent rowheight'>
                <table>
                    <tr>
                        <td style='width:20px;text-align:left;'>1. </td>
                        <td style='text-align:left;ver'>
                            $organization.OrganizationName$ berkedudukan di $organization.OrganizationAddress$ $organization.City$, dalam hal ini									
	                        diwakili oleh $organization.Pimpinan$. Yang bertindak dalam kedudukannya sebagai PIMPINAN dari dan
	                        karenanya untuk dan atas nama $organization.OrganizationName$ $organization.OrganizationAddress$ $organization.City$.									
	                        Selanjutnya disebut 'PIHAK PERTAMA' (Penjual): menyatakan mampu dan tidak pailit dan sanggup untuk pengambilan angsuran		
	                        kredit Kendaraan Roda Dua MERK $merk$
                        </td>
                    </tr>
                    <tr>
                        <td style='width:20px;text-align:left;'>2. </td>
                        <td style='text-align:left;'>
                            $custname$ Pekerjaan: $job$ beralamat di $billingaddress$ $city$
	                        Kartu Tanda Penduduk No.$noktp$
	                        dikeluarkan oleh: $ktppublisher$ Tanggal $ktpdate$	
	                        Selanjutnya disebut 'PIHAK KEDUA' (Pembeli):
                        </td>
                    </tr>
                </table>
            </div>
            <div style='padding-top:20px;text-align:center;'>
                M E N G I N G A T			
            </div>
            <div class='rowheight'>
                <table>
                    <tr>
                        <td style='text-align:left;'>-</td>
                        <td style='text-align:left;'>
                            Bahwa   PIHAK  PERTAMA dan  PIHAK  KEDUA  telah menanda tangani suatu Perjanjian
                            Jual Beli dengan Pembayaran Angsuran  (selanjutnya disebut Perjanjian Jual Beli)  atas  1 (satu)
                            buah kendaraan sepeda motor MERK $merk$ dengan spesifikasi sebagai berikut:
                            <div class='indent'>
                                <table>
                                    <tr><td style='width:100px;text-align:left;'>Model/Type</td><td style='width:30px;'>:</td><td style='text-align:left;'>$type$</td></tr>
                                    <tr><td style='text-align:left;'>Warna</td><td>:</td><td style='text-align:left;'>$warna$</td></tr>
                                    <tr><td style='text-align:left;'>Tahun</td><td>:</td><td style='text-align:left;'>$tahun$</td></tr>
                                    <tr><td style='text-align:left;'>Nomor Mesin</td><td>:</td><td style='text-align:left;'>$nomesin$</td></tr>
                                    <tr><td style='text-align:left;'>Nomor Rangka</td><td>:</td><td style='text-align:left;'>$norangka$</td></tr>
                                    <tr><td style='text-align:left;'>Nomor Polisi</td><td>:</td><td style='text-align:left;'>$nopolisi$</td></tr>
                                </table>
                            </div>
                            <div>
                                (selanjutnya disebut sebagai 'KENDARAAN')
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style='text-align:left;'>-</td>
                        <td style='text-align:left;'>
                            Bahwa sesuai  dengan ketentuan-ketentuan Perjanjian Jual Beli, pada tanggal ditanda tangani
                            Perjanjian ini PIHAK KEDUA mengaku masih berhutang kepada PIHAK PERTAMA uang
                            sejumlah <b>Rp $sisahutang$</b> (<b>$sisahutangterbilang$</b>)
                            yang merupakan sisa Harga Pembelian kendaraan termaksud yang harus dibayar dalam waktu
                            $lamaangsuran$ ($lamaangsuranterbilang$) bulan dengan $banyakcicilan$ kali angsuran (selanjutnya disebut 'Sisa Harga Pembelian').
                        </td>
                    </tr>
                    <tr>
                        <td style='text-align:left;'>-</td>
                        <td style='text-align:left;'>
                            Bahwa sebagai  jaminan atas  pembayaran  angsuran  dari  sisa Harga Pembelian kendaraan
                            tersebut, PIHAK KEDUA setuju untuk menyerahkan dan  PIHAK PERTAMA  setuju untuk
                            menerima  penyerahan  hak  milik  atas kendaraan  secara  fiducia  dengan  syarat-syarat  dan
                            ketentuan-ketentuan tersebut dalam Perjanjian ini. Maka karenanya berdasarkan kesepakatan
                            kesepakatan tersebut diatas kedua belah pihak setuju untuk menanda tangani dan melaksana-
                            kan Perjanjian ini dengan syarat-syarat dan ketentuan-ketentuan dibawah ini :									
                        </td>
                    </tr>
                </table>
            </div>
            <div class='indent'>
                <div class='rowheight'>
                    Pasal 1 <br />
                    PENYERAHAN HAK
                    <div class='indent'>
	                    Sebagai jaminan atas pembayaran dari Sisa Harga Pembelian yang masih terhitung kepada
                        PIHAK PERTAMA  menurut Perjanjian Jual Beli, maka  PIHAK KEDUA dengan ini me-
                        nyerahkan secara fiducia semua hak titel dan kepentingan PIHAK KEDUA atas kendaraan.
                        Sehubungan  dengan  hal  tersebut, PIHAK  KEDUA  dengan  ini  menyerahkan  kepada 
                        PIHAK PERTAMA  atas 1 (satu) buah  Buku  Bukti  Pemilikan  Kendaraan  Bermotor 
                        (BPKB) atas kendaraan serta bukti-bukti pemilikan lainnya dari kendaraan.								
                    </div>
                </div>
                <div class='rowheight'>
                    Pasal 2	<br />							
                    FIDUCIA
                    <div class='rowheight indent'>
                        <table>
                            <tr>
                                <td style='width:20px;text-align:left;'>1. </td>
                                <td style='text-align:left;'>
                                    PIHAK PERTAMA dengan ini member hak ini dan kepercayaan kepada PIHAK KEDUA
                                    dan PIHAK KEDUA  sebagai pihak  yang  diberi   hak  dan  kepercayaan  oleh  PIHAK
							        PERTAMA dan karenanya untuk dan atas nama PIHAK PERTAMA dengan ini setuju dan
                                    mengikatkan  diri  untuk  memegang dan menggunakan / memakai kendaraan termaksud
                                    secara  wajar  sesuai dengan cara-cara pemakaian yang  diberikan  dalam  Buku Petunjuk
                                    Pemakaian dan Perawatan Kendaraan.								
                                    Hak PIHAK KEDUA untuk memegang dan menggunakan/memakai kendaraan termaksud
                                    dapat  dicabut  setiap	waktu  oleh   PIHAK  PERTAMA  dalam  hal   PIHAK  KEDUA
                                    wanprestasi  atau  cidera janji  atau  melanggar,  melalaikan  kewajiban-kewajiban  dalam
                                    Perjanjian  ini, Perjanjian  Jual  Beli   dan  dokumen-dokumen  atau  surat-surat  lain  yang
                                    berhubungan.								
                                </td>
                            </tr>
                            <tr>
                                <td style='width:20px;text-align:left;'>2. </td>
                                <td style='text-align:left;'>
                                    PIHAK KEDUA wajib dan dengan ini  mengikatkan  diri  dengan  biayanya  sendiri  untuk
                                    menjaga  dan  Merawat  kendaraan dimaksud sesuai dengan  petunjuk-petunjuk  dan cara-
                                    cara yang diberikan didalam Buku Petunjuk Pemakaian dan Perawatan serta Buku Garansi
                                    dan  Service, dalam  hal diperlukan service   Perbaikan  atas  kendaraan  PIHAK KEDUA
                                    hanya dapat melakukannya pada bengkel yang telah ditunjuk oleh PIHAK PERTAMA.								
                                </td>
                            </tr>
                            <tr>
                                <td style='width:20px;text-align:left;'>3. </td>
                                <td style='text-align:left;'>
                                    Selama sisa Harga Pembelian  yang masih terhutang  belum  terbayar lunas,  maka PIHAK
                                    KEDUA  menjamin  dan  dengan  ini menegaskan   untuk   selalu   memenuhi  ketentuan-
                                    ketentuan yang dimaksud pada Pasal – 7 Perjanjian Jual Beli.								
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class='rowheight'>
                    Pasal 3 <br />
                    RESIKO DAN TANGGUNG JAWAB
		            <div class='rowheight indent'>
                        Dengan disetujui dan diakui oleh PIHAK KEDUA  bahwa  dilakukannya P enyerahan Hak
                        Milik Secara Fiducia atas kendaraan oleh  PIHAK KEDUA kepada PIHAK PERTAMA,
                        maka segala resiko, kerugian, kebakaran, kerusakan  hancur,  kehilangan  dicuri orang dan
                        beban yang menyangkut kendaraan  termaksud  tetap  merupakan resiko, kerugian, keba-
                        karan, kerusakan hancur, kehilangan dicuri orang dan beban PIHAK KEDUA sendiri.								
                    </div>
                </div>
                <div class='rowheight'>
                    Pasal 4 <br />
                    SURAT KUASA
		            <div class='rowheight indent'>
                        Sebagai  jaminan  serta  untuk memperlancar  pelaksanaan   hak-hak  PIHAK PERTAMA
                        sebagaimana   dimaksud dalam Perjanjian Jual  Beli  dengan ini PIHAK KEDUA setuju dan
                        mengikatkan diri untuk   menandatangani  dan  memberikan  kepada  PIHAK PERTAMA
                        sebagai berikut :								
                    </div>
                    <div class='rowheight' style='padding-left:100px;'>
                        <table>
                            <tr>
                                <td style='text-align:left;'>1. </td>
                                <td style='text-align:left;'>
                                    Surat  Kuasa  yang  member hak  dan  kekuasaan  kepada  PIHAK PERTAMA  untuk
                                    melakukan  segala  tindakan pengurusan  dan  pemilikan  atas  kendaraan  sebagaimana
                                    dimaksud pada Pasal – 8 Perjanjian  Jual Beli.
                                </td>
                            </tr>
                            <tr>
                                <td style='text-align:left;'>2. </td>
                                <td style='text-align:left;'>
                                    Diakui dan disetujui oleh PIHAK KEDUA  bahwah  kuasa-kuasa yang dimaksud diatas
                                    dengan alasan Apapun, tidak  dapat  dicabut kembali selama sisa Harga Pembelian yang
                                    terhutang belum terbayar  Lunas seluruhnya.							
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class='rowheight'>
                    Pasal 5 <br />
                    PAJAK DAN BIAYA-BIAYA
		            <div class='rowheight indent'>
                        PIHAK  KEDUA  setuju  dan dengan  ini  menyatakan dengan tegas bahwah semua pajak,
                        bea iuran  wajib dan biaya-biaya atau  ongkos-ongkos  sehubungan  dengan  Perjanjian  ini
                        merupakan beban dan tanggung jawab PIHAK KEDUA sendiri.								
                    </div>
                </div>
                <div class='rowheight'>
                    Pasal 6 <br />
                    WANPRESTASI
		            <div class='rowheight indent'>
                        Dalam  hal PIHAK KEDUA oleh	PIHAK  PERTAMA  dianggap wanprestasi atau cidera
                        janji sebagaimana dimaksud pada Pasal – 10 Perjanjian, maka seluruh ketentuan-ketentuan
                        Pasal – 10 Perjanjian Jual Beli tanpa kecuali juga berlaku.								
                    </div>
                </div>
                <div class='rowheight'>
                    Pasal 7 <br />
                    DOMISILI
		            <div class='rowheight indent'>
                        Untuk maksud Perjanjian ini dan	pelaksanaannya kedua belah pihak setuju memilih domisili
                        hukum  tak  berubah  pada Kantor  Panitera  Pengadilan  Negeri  $organization.City$ di
                        $organization.City$.
                    </div>
                </div>
            </div>

            <div class='rowheight'>
                Demikianlah Perjanjian ini dibuat dan ditanda tangani oleh kedua belah pihak pada tanggal 
                sebagaimana disebutkan diatas dalam rangkap dua yang bermaterai cukup dan keduanya mempunyai
                kekuatan pembuktian yang sama.										
            </div>
            <div>
                <div style='float:left;'>
                    <div style='padding-top:50px;padding-left:50px;text-align:center;'>
                        PIHAK KEDUA
                    </div>
                    <div style='padding-top:70px;padding-left:50px;text-align:center;'>
                        ( $custname$ )
                    </div>
                </div>
                <div style='float:right;'>
                    <div style='padding-top:50px;padding-right:50px;text-align:center;'>
                        $organization.City$, $currentdate$
                        <br />
                        $organization.OrganizationName$
                    </div>
                    <div style='padding-top:70px;padding-right:50px;text-align:center;'>
                        ( $organization.Pimpinan$ )
                        <br />
                        Pimpinan
                    </div>
                </div>
            </div>
            <div style='clear:both;'>
                <div>
                    <div style='text-align:center'>Mengetahui/Menyetujui sebagai saksi:</div>
                    <div style='text-align:center;'>Istri/Suami/Ayah/Wali/Atasan atau Penjamin</div>
                    <div style='padding-top:70px;text-align:center;'>
                        ( ......................... )
                    </div>
                </div>
            </div>
        ";
    }
}
