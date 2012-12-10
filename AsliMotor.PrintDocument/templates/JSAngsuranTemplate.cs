using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class JSAngsuranTemplate
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
            <div style='padding-top:15px;'>
                Perjanjian jual beli dengan	pembayaran Angsuran ini dibuat dan ditanda tangani hari ini tanggal $suratperjanjiandate$ oleh dan antara:
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
                <b>M E N G I N G A T</b>
            </div>
            <div>
            Bahwa PIHAK PERTAMA (Penjual) dan PIHAK KEDUA (Pembeli) berdasarkan
            kesepakatan-kesepakatan untuk mengadakan Perikatan/Perjanjian Jual Beli dengan
            Pembayaran Angsuran atas 1 (satu) buah kendaraan bermotor Merk $merk$	
            oleh kedua belah pihak setuju untuk menanda tangani dan melaksanakan Perjanjian ini
            dengan syarat-syarat dan ketentuan-ketentuan sebagai berikut:
            </div>
            <div class='indent'>
            <div class='rowheight'>
                Pasal 1 <br />
                JUAL-BELI
                <div class='indent'>
	                PIHAK PERTAMA dengan ini menjual dan menyerahkan kepada PIHAK KEDUA, dan
	                PIHAK KEDUA menerangkan telah membeli dan menerima penyerahan dari PIHAK
	                PERTAMA berupa 1 (satu) buah kendaraan bermotor Merk $merk$
	                dengan Spesifikasi sebagai berikut:
                </div>
                <div class='rowheight' style='padding-left:100px;'>
                    <table>
                        <tr><td style='width:100px;text-align:left;'>Model/Type</td><td style='width:30px;'>:</td><td style='text-align:left;'>$type$</td></tr>
                        <tr><td style='text-align:left;'>Warna</td><td>:</td><td style='text-align:left;'>$warna$</td></tr>
                        <tr><td style='text-align:left;'>Tahun</td><td>:</td><td style='text-align:left;'>$tahun$</td></tr>
                        <tr><td style='text-align:left;'>Nomor Mesin</td><td>:</td><td style='text-align:left;'>$nomesin$</td></tr>
                        <tr><td style='text-align:left;'>Nomor Rangka</td><td>:</td><td style='text-align:left;'>$norangka$</td></tr>
                        <tr><td style='text-align:left;'>Nomor Polisi</td><td>:</td><td style='text-align:left;'>$nopolisi$</td></tr>
                    </table>
                </div>
                <div class='indent'>
                    (selanjutnya disebut sebagai 'KENDARAAN' dalam kendaraan baru lengkap dengan
		            1 (satu) buah Buku Garansi, 1 (satu) buah Buku Petunjuk Pemakaian dan Perawatan,
		            1 (satu) set kunci-kunci dan perlengkapan standard.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 2	<br />							
                HARGA & PEMBAYARAN
                <div class='indent'>
		            Jual Beli dengan pembayaran Angsuran atas kendaraan sebagaimana dimaksud dalam
		            perjanjian ini disetujui oleh kedua belah pihak terjadi dengan harga sebesar:
                </div>
                <div class='rowheight' style='padding-left:100px;'>
                    <table>
                        <tr>
                            <td style='width:20px;text-align:left;'>1. </td>
                            <td style='text-align:left;'>Harga kendaraan tersebut diatas (on the road) adalah</td>
                            <td><b>Rp</b></td>
                            <td style='text-align:right;'><b>$price$</b></td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>2. </td>
                            <td style='text-align:left;'>Uang Muka yang telah dibayar PIHAK KEDUA (pembeli)</td>
                            <td><b>Rp</b></td>
                            <td style='text-align:right;'><b>$uangmuka$</b></td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>3. </td>
                            <td style='text-align:left;'>
                                Sisa Harga Pembelian yang menjadi hutang PIHAK KEDUA
                            </td>
                            <td><b>Rp</b></td>
                            <td style='text-align:right;'><b>$sisahutang$</b></td>
                        </tr>
                        <tr>
                            <td colspan='4' style='text-align:right;'>(<b>$sisahutangterbilang$</b>)</td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class='rowheight'>
                Pasal 3 <br />
		        <div class='rowheight indent'>
                    Sisa Harga Pembelian yang menjadi hutang PIHAK KEDUA pada pasal 2 (c) tersebut
		            diatas wajib dibayar secara	mengangsur oleh PIHAK KEDUA kepada PIHAK PERTAMA
		            dalam waktu	$lamaangsuran$	($lamaangsuranterbilang$ bulan), dengan jumlah angsuran setiap kali/bulan sebesar
		            Rp $angsuranbulanan$ ($angsuranbulananterbilang$)	
		            dalam jumlah yang sama dan dibayarkan pada tiap-tiap tanggal bulan bersangkutan.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 4 <br />
		        <div class='rowheight indent'>
                    Setiap pembayaran angsuran sebagaimana tersebut diatas harus dilakukan oleh PIHAK
		            KEDUA secara tunai dikantor PIHAK PERTAMA pada alamat di $organization.OrganizationAddress$ $organization.City$ 
                    kepada Pejabat/Karyawan/Wati PIHAK PERTAMA yang berwenang.
		            Untuk setiap pembayaran angsuran sebagaimana tersebut diatas PIHAK PERTAMA akan
		            mengeluarkan suatu bukti tanda terima (kwitansi) yang syah.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 5 <br />
		        <div class='rowheight indent'>
                    PIHAK KEDUA wajib dan mengikatkan diri untuk membayar uang angsuran atas sisa
		            harga Pembeli yang masih terhutang kepada PIHAK PERTAMA dengan tepat pada
		            jadwal yang diatur dalam pasal 2, 3 diatas, dan dalam hal terjadi kelambatan atas
		            pembayaran uang angsuran termaksud, maka untuk setiap hari keterlambatan PIHAK
		            KEDUA wajib dan mengikatkan diri untuk membayar denda sebesar 36% (tiga puluh
		            enam) per-tahun dari jumlah Angsuran yang pembayarannya telah jatuh tempo, atau dengan
		            formula sebagai berikut: jumlah hari keterlambatan x 36%/360 x jumlah angsuran yang
		            pembayarannya telah jatuh tempo. Pembayaran denda tersebut diatas harus dilakukan oleh
		            PIHAK KEDUA bersamaan dengan pembayaran yang terlambat tersebut.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 6 <br />
		        <div class='rowheight indent'>
                    PIHAK KEDUA berhak untuk mempercepat jadwal pembayaran atau dapat melunaskan
		            sisa hutangnya lebih cepat dari tempo waktu yang ditentukan dalam pasal 2, 3 diatas, maka
		            untuk bulan-bulan yang “dipercepat” itu akan diadakan pengurangan (Reduksi) pembayaran.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 7 <br />
		        <div class='rowheight indent'>
                    PIHAK KEDUA setuju dan dengan ini memberikan jaminannya utama kepada PIHAK
		            PERTAMA bahwah selama sisa Harga Pembelian yang terhutang belum terbayar lunas,
		            maka:
                </div>
                <div class='rowheight' style='padding-left:100px;'>
                    <table>
                        <tr><td style='width:20px;text-align:left;'>1. </td><td style='text-align:left;'>Kendaraan tersebut hanya akan digunakan oleh PIHAK KEDUA sendiri.</td></tr>
                        <tr>
                            <td style='text-align:left;'>2. </td>
                            <td style='text-align:left;'>
                                PIHAK KEDUA akan selalu menggunakan, merawat dan memperbaiki kendaraan
			                    tersebut dengan cara-cara yang wajar dan sebaik-baiknya sesuai dengan instruksi-instruksi  
			                    atau petunjuk-petunjuk yang diberikan dalam Buku Petunjuk Pemakaian dan
			                    Perawatan dan Buku Garansi, dan khusus untuk perawatan (service) serta perbaikan atas
			                    kendaraan tersebut, PIHAK KEDUA wajib untuk hanya melakukannya di bengkel-bengkel resmi yang ditunjuk oleh PIHAK PERTAMA.
                            </td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>3. </td>
                            <td style='text-align:left;'>
                                PIHAK KEDUA tidak akan menjual atau dengan cara atau nama apapun memindahkan
			                    hak milik dan/atau penguasaan fisik atas kendaraan tersebut kepada pihak ketiga 
			                    manapun juga.
                            </td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>4. </td>
                            <td style='text-align:left;'>
                                PIHAK KEDUA tidak akan menjamin, mempertanggungkan, menggadaikan, menyewa-kan, 
			                    meminjamkan dan/atau mengalihkan kendaraan tersebut kepada pihak ketiga
			                    manapun juga.
                            </td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>5. </td>
                            <td style='text-align:left;'>
                                PIHAK KEDUA tidak berhak untuk mengalihkan baik untuk sebagian maupun seluruh-nya
			                    hak-hak dan kewajiban-kewajibannya dalam perjanjian ini kepada pihak ketiga manapun juga.
                            </td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>6. </td>
                            <td style='text-align:left;'>
                                PIHAK KEDUA wajib menyerahkan BPKB kendaraan tersebut kepada PIHAK
			                    PERTAMA untuk disimpan sebagai jaminan dalam Pembelian Jual Beli Dengan Pembayaran angsuran.
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class='rowheight'>
                Pasal 8 <br />
                <div class='indent rowheight'>
                    <table>
                        <tr>
                            <td style='width:20px;text-align:left;'>1. </td>
                            <td style='text-align:left;'>
                                Sebagai jaminan dari pembayaran sisa Harga Pembelian yang masih terhutang, PIHAK
			                    KEDUA dengan ini menyerahkan hak milik atas kendaraan tersebut kepada PIHAK
			                    PERTAMA secara fiducia dengan syarat-syarat dan ketentuan-ketentuan sebagaimana
			                    diperinci lebih lanjut dalam Perjanjian Penyerahan Hak Milik Secara Fiducia yang
			                    ditanda tangani oleh kedua belah pihak pada tanggal ditanda tanganinya Perjanjian ini,
			                    dan disetujui pula bahwa perjanjian tersebut merupakan bagian integral atau tidak
			                    terpisahkan dari perjanjian ini dan dilampirkan pada Perjanjian ini sebagai lampiran I.
                            </td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>2. </td>
                            <td style='text-align:left;'>
                                Selanjutnya PIHAK KEDUA juga akan menanda tangani dan memberikan suatu Surat
			                    Kuasa kepada PIHAK PERTAMA yang memberi hak dan kekuasaan kepada PIHAK
			                    PERTAMA untuk melakukan segala hal dan tindakan pengurusan maupun pemilikan	atas
			                    kendaraan termaksud termasuk hak untuk menjual kendaraan tersebut dalam hal terjadi
			                    wanprestasi atau cidera janji dari PIHAK KEDUA sebagaimana dimaksud pada pasal 9 							
			                    Perjanjian ini. Surat Kuasa mana merupakan bagian integral atau tidak terpisahkan dari
			                    perjanjian ini dan dilampirkan pada Perjanjian ini sebagai lampiran II.
                            </td>
                        </tr>
                
                    </table>
                </div>
            </div>
            <div class='rowheight'>
                Pasal 9 <br />
		        <div class='rowheight indent'>
                    Dengan dilakukannya Penyerahan Hak Milik Secara Fiducia atas kendaraan tersebut
			        oleh PIHAK KEDUA kepada PIHAK PERTAMA, hal mana sama sekali tidak dapat
			        ditafsirkan bahwa tanggung jawab dan kewajiban-kewajiban PIHAK KEDUA sebagai-mana 
			        diatur dalam Perjanjian ini menjadi hapus, batal atau dikurangi. Selanjutnya
			        disetujui dan diakui oleh PIHAK KEDUA bahwa semua resiko, kerugian kehilangan dan
			        beban yang menyangkut kendaraan tersebut tetap merupakan resiko, kerugian, kehilangan dan beban PIHAK KEDUA sendiri.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 10	<br />							
                WANPRESTASI
                <div class='indent'>
		            PIHAK KEDUA dianggap wanprestasi atau cidera janji dalam hal-hal berikut dibawah ini:
                </div>
                <div class='indent rowheight'>
                    <table>
                        <tr>
                            <td style='text-align:left;'>1. </td>
                            <td style='text-align:left;'>
                                PIHAK KEDUA dengan alasan apapun tidak atau lalai atau terlambat melaksanakan
			                    kewajibannya untuk melakukan Pembayaran angsuran sebagaimana diatur dalam pasal
			                    2, 3 perjanjian ini.
                            </td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>2. </td>
                            <td style='text-align:left;'>
                                PIHAK KEDUA dengan alasan apapun tidak atau lalai melaksanakan atau memenuhi
			                    salah satu saja dari kewajiban-kewajibannya menurut Perjanjian ini dan Perjanjian
			                    Penyerahan Hak Milik Secara Fiducia serta dokumen-dokumen atau surat-surat lain
			                    yang bersehubungan.
                            </td>
                        </tr>
                        <tr>
                            <td style='text-align:left;'>2. </td>
                            <td style='text-align:left;'>
                                PIHAK KEDUA meninggal dunia, sedang ahli warisnya atau penjaminnya yang Sah
			                    tidak mau atau menolak untuk melanjutkan kewajiban dan tanggung jawab PIHAK
			                    KEDUA sebagaimana diatur dalam Perjanjian ini dan Perjanjian Penyerahan Hak Milik Secara Fiducia.
                            </td>
                        </tr>
                    </table>
                </div>
                <div class='indent rowheight'>
                    Dalam hal PIHAK KEDUA melakukan atau berada  dalam  keadaan  wanprestasi  atau
		            cidera janji, maka PIHAK PERTAMA 								dapat menyatakan dalam suatu surat pemberitahuan
		            kepada PIHAK KEDUA bahwa								PIHAK KEDUA telah wanprestasi atau cidera janji dan
		            atas penerimaan surat mana PIHAK 								KEDUA atau ahli warisnya ataupun penjaminnya yang
		            sah dalam  waktu  selambat-lambatnya 								3 ( tiga ) hari berturut-turut wajib dan mengikatkan
		            diri  untuk  menyerahkan  kepada   PIHAK  PERTAMA 								kendaraan   termaksud   berikut
		            dokumen-dokumen  pemilikannya 								termaksud STNK dan  perlengkapan lain  yang standard
		            dalam keadaan baik, dan sesuai 								dengan semua hak dan kekuasaan yang ada pada PIHAK
		            PERTAMA, sesuai dengan Perjanjian ini, 								Perjanjian Penyerahan Hak Milik Secara Fiducia
		            dan Surat Kuasa yang diberikan 								kepada PIHAK PERTAMA, maka PIHAK PERTAMA
		            berhak  menjual  kendaraan  termaksud 								dengan  cara  harga  yang  ditetapkannya sendiri,
		            dengan atau  tanpa  melalui  lelang  dan  untuk 								kemudian  mengambil   pelunasan  atas  sisa
		            Harga   Pembelian  yang  masih  terhutang dari harga penjualan kendaraan termaksud. Bila								 hasil
		            penjualan tersebut masih kurang dari sisa Harga Pembelian yang masih terhutang, 								maka 
		            PIHAK KEDUA tetap wajib untuk melunasi kekurangannya, dan bila hasil penjualan								 kendaraan
		            tersebut melebihi  sisa  Harga Pembelian  yang  masih  terhutang,  maka  								PIHAK  PERTAMA
		            akan menyerahkan / mengembalikannya kelebihan tersebut kepada PIHAK KEDUA.								
                </div>
            </div>
            <div class='rowheight'>
                Pasal 11 <br />
		        <div class='rowheight indent'>
                    Selama  sisa  Harga   Pembelian  yang 							masih  terhutang   belum  lunas   semua  menurut
			        Perjanjian ini,  maka pengurusan 							pembaharuan  S.T.N.K.P.K.B  dan  S.W.D.K.L.L.J.
			        harus diserahkan oleh 							PIHAK KEDUA kepada PIHAK PERTAMA dan semua biaya
			        untuk itu harus ditanggung oleh PIHAK KEDUA.							
                </div>
            </div>
            <div class='rowheight'>
                Pasal 12 <br />
		        <div class='rowheight indent'>
                    Atas  pelanggaran  ketentuan-ketentuan 							tersebut    diatas , maka  PIHAK  PERTAMA
			        berhak penuh menarik / mengambil  kembali  kendaraan tersebut tanpa ada 							tuntutan 
			        apapun dari PIHAK KEDUA maupun dari pihak lainnya.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 13 <br />
		        <div class='rowheight indent'>
                    Apabila terjadi tindakan moneter 							oleh Pemerintah Republik Indonesia, PIHAK KEDUA
			        dengan  ini  memberikan  persetujuan 							dan   dengan  sekaligus  juga  memberikan  kuasa
			        kepada  PIHAK PERTAMA  untuk 							menyelesaikan  sisa  jumlah  angsuran  yang  masih 
			        terhutang  PIHAK KEDUA  akibat 							moneter  tersebut,  dengan  jumlah yang ditentukan/
			        dipandang layak oleh PIHAK PERTAMA							 tanpa diperlukan terlebih dahulu persetujuan
			        dari PIHAK KEDUA oleh PIHAK PERTAMA.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 14 <br />
		        <div class='rowheight indent'>
                    Setelah semua sisa Harga Pembelian pada pasal 							2, 3 tersebut diatas dilunasi semuanya 
			        oleh  PIHAK  KEDUA  maka  hak  milik 							atas  kendaraan  tersebut  dengan sendirinya 
			        berpindah ketangan PIHAK KEDUA.
                </div>
            </div>
            <div class='rowheight'>
                Pasal 15 <br />
		        <div class='rowheight indent'>
                    Untuk  semua  akibat  hukum yang 							timbul dari Perjanjian ini dan pelaksanaannya kedua
			        belah pihak setuju telah memilih 							tempat tinggal (domicile) hukum yang tetap di Kantor
			        Panitera Pengadilan Negeri $organization.City$ di $organization.City$.
                </div>
            </div>
            </div>
            <div class='rowheight'>
                Demikian Perjanjian ini dibuat dan 										ditanda tangani oleh kedua belah pihak pada tanggal sebagai-
                mana  disebutkan  diatas  dalam  rangkap 										2  ( dua )  yang   bermaterai  cukup  dan  mempunyai
                kekuatan hukum serta pembuktian yang sama.							
            </div>
            <div>
                <div style='float:left;'>
                    <div style='padding-top:40px;padding-left:50px;text-align:center;'>
                        PIHAK KEDUA
                    </div>
                    <div style='padding-top:70px;padding-left:50px;text-align:center;'>
                        ( $custname$ )
                    </div>
                </div>
                <div style='float:right;'>
                    <div style='padding-top:40px;padding-right:50px;text-align:center;'>
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
