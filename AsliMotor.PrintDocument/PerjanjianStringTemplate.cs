using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class PerjanjianStringTemplate
    {
        public const string DEFAULT = @"
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
                    SURAT PERJANJIAN SEWA BELI
                    Nomor		: $PerjanjianNo$
    				Tanggal 	: $InvoiceDate$
Yang bertanda tangan dibawah ini :
I.	$org.Name$ , sebuah toko penjualan kendaraan roda 2, berkedudukan dijalan Raja Ali Haji No.1,2,3,4 & 5 selanjutnya disebut : PEMILIK
II.	a.  ………………..: Bertempat tinggal di : …………………………
DIkeluarkan oleh    : 
b………Bertempat tinggal di :…………………………………………….
Nomor KTP :……………..			tanggal : …………………..
Dikeluarkan oleh : …………………………
Selanjutnya disebut : PENYEWA
III.	c. ….Bertempat tinggal di :…………………..
Nomor KTP :………………….				Tanggal :
Dikeluarkan oleh :
Selanjutnya disebut : PENJAMIN
Pemilik dan penyewa telah bermufakat untuk mengadakan perjanjian Sewa Beli dengan syarat-syarat dan ketentuan sebagai berikut :
PASAL 1
PENYERAHAN BARANG
Atas permohonan penyewa tanggal ……………….. maka pemilik telah menyetujui dan memperkenankan penyewa untuk menyewa 1 ( satu ) unit sepeda motor merk …….………. Pada saat penanda tanganan surat ini, pemilik atau orang yang ditunjuk oleh pemilik telah menyerahkan kepada penyewa dan dengan ini penyewa mengaku telah menerima penyerahan dari pemilik 1 (satu) unit sepeda motor merk………………….lengkap dengan semua peralatan dan perlengkapannya, dalam keadaan baik, 100% baru tanpa cacat dengan spesifikasi sebagai berikut :
MERK :		MODEL/TYPE :		TAHUN :		WARNA 	NO.RANGKA :		NO.MESIN:


PASAL II
PENETAPAN HARGA
Pemilik dan penyewa sudah menyepakati seluruh harga sepeda motor ………………..tersebut termasuk biaya sewa beli sebesar Rp……………………
(………………………………………………………………………….)
PASAL III
CARA PEMBAYARAN
1.	Penyewa berkewajiban membayar seluruh harga sewa yang tersebut pada pasal II perjanjian ini kepada pemilik dan pada saat Surat Perjanjian Sewa Beli ini ditanda tangani, Penyewa akan membayar uang muka sebesar Rp……………..(…………………………………….) akan dibayar pada tiap-tiap tanggal …………….berturut-turut selama …………………..(…) bulan, terhitung sejak bulan ……………………. sampai dengan bulan ………………….dengan masing-masing uang sewa bulanan sebesar Rp………………..(………………………………………………..) setiap kali pembayaran uang sewa dilakukan oleh penyewa tanpa harus ada tagihan terlebih dahulu dari pemilik, pada kantor ASLI MOTOR SIQ di DABO SINGKEP jalan RAJA ALI HAJI NO.1,2,3,4& 5 pembayaran termasuk hanya dapat dibuktikan dengan kwitansi sah dari pemilik atau Toko…dan harus dilakukan dengan uang tunai.
2.	Apabila pembayaran uang sewa berupa Cheque atau Bilyet Giro, maka pembayaran yang dimaksud baru dianggap sah jika Cheque atau Bilyet Giro tersebut dapat diluangkan atau dipindah bukukan.
3.	Apabila terdapat satu kali uang sewa bulanan belum atau tidak  dibayar dengan alasan apapun juga, maka uang sewa untuk bulanan berikutnya dianggap sebagai uang sewa yang belum atau tidak dibayar tanpa mengurangi denda yang harus dibayar oleh karena terlambatnya pembayaran uang sewa yang dimaksud.
4.	Menyimpang dari ketentuan pembayaran pada ayat 1 ( pertama ) tersebut diatas, penyewa selalu berhak untuk membayar sekaligus beberapa uang sewa sebelum jatuhnya tempo pembayaran seperti yang telah ditetapkan diatas dan pemilik akan memberikan potongan pembayaran uang sewa kepada penyewa dan jumlah uang sewa yang dipercepat pembayaran nya tersebut. 


PASAL IV
SANKSI-SANKSI

1.	Apabila penyewa lalai membayar uang sewa atau terlambat dari tanggal jatuh tempo pembayaran uang sewa , penyewa dikenakan denda administrasi sebesar 0,5% sehari terhitung mualai tanggal jatuh tempo pembayaran uang sewa tersebut sampai dengn tanggal uang sewa bersangkutan dibayar lunas.
2.	Pemilik atau orang yang ditunjuk oleh pemilik jika perlu bantuan alat Negara atau pihak yang Berwajib berhak mengambil kembali / menarik kembali / menguasai kembali sepeda motor tersebut termasuk berikut peralatan dan perlengkapan tanpa melalui Pengadilan Negeri / Surat Teguran Juru Sita.

Untuk itu dapat memasuki segala tempat yang dipergunakan atau dibawah pengawasan penyewa atau pemakai yang sekiranya dapat dipergunakan untuk penempatan sepeda motor tersebut :
a.	Apabila penyewa tidak membayar uang sewa atau salah satu uang sewa bulanan sejak jatuh temponya.
b.	Apabila penyewa melanggar salah satu pasal Surat Perjanjian Sewa Beli ini.
c.	Apabila Penyewa meninggal dunia.
d.	apabila penyewa dinyatakan pailit ditaruh dibawah pengampunan atau harta kekayaan penyewa sebagai atau selanjutnya disita yang berwenang untuk itu.
e.	Apabila harta benda penyewa dinilai mundur sedemikian rupa hingga menurut pendapat pemilik sudah tidak mungkin melanjutkan pembayaran uang sewa.
f.	Apabila Penyewa wan prestasi dan penjamin tidak atau tidak dapat menyelesaikan atau melanjutkan kewajiban penyewa. 
g.	 Apabila penyewa tidak bertempat tinggal lagi pada alamat tersebut diatas atau pindah tugas kedaerah lain.

3.	Apabila Sepeda Motor yang tersebut dalam pasal 1 Surat Perjanjian ini telah ditahan atau ditarik kembali oleh pemilik dari penyewa karena salah satu sebab yang termasuk dalam salah satu ayat-ayat tersebut diatas, pemilik masih memberikan tenggangan selama 15 hari terhitung sejak penarikan Sepeda Motor termasuk kepada penyewa untuk menyelesaikan pembayaran seluruh uang sewa ( baik yang tertunggak maupun yang belum jatuh tempo ) berikut denda administrasi dan menbayar biaya penarikan motor sebesar Rp.100.000,- ( seratus ribu rupiah ) kepada pemilik apabila sampai dengan akhir batas waktu tersebut penyewa masih belum atau tidak menyelesaikan pembayaran tersebut  maka surat perjanjian sewa beli ini menjadi batal dengan sendirinya dengan arti seluruh uang sewa yang telah dibayar kepada Pemilik dan sepeda motor tersebut menjadi milik pemilik sepenuhnya.
Dalam kejadian tersebut diatas dari pihak yang mengikatkan diri dalam perjanjian ini melepaskan ketentuan pasal 1266 dan 1267 kitab undang-undang perdata.

PASAL V
PEMILIK SEPEDA MOTOR

1.	Meskipun berlaku ketentuan perjanjian ini, pemilik dapat mengijin kan penyewa untuk memperoleh sebuah buku pemilik kendaraan bermotor ( BPKB ) dan sebuah Surat Tanda Kendaraan Bermotor ( STNK ) ataspenyewa  dan penyewa menyetujui untuk menyerahkan BPKB itu kepada pemilik yang akan menyimpannnya sampai penyewa telah melunasi semua uang sewa sebagai mana yang telah tersebut dalam pasak III ayat 1.
2.	Berhubung Buku Pemilik Kendaraan bermotor sudah tertulis nama penyewa dengan ini member kuasa kepada pemilik untuk menjual, menyerahkan sepeda motor tersebut dengan harga dan syarat-syarat yang disetujui oleh yang menerima kuasa, serta menerima pembayaran dan lain-lain.

PASAL VI
SYARAT-SYARAT DAN TINDAKAN

Syarat-syarat perjanjian sewa Beli ini di Tanda Tangani, Pemyewa Wajib memenuhi syart-syarat ini :
1.	Penyewa atas biaya sendiri akan merawat sepeda motor tersebut dalam keadaan yang baik menurut pendapat pemilk apabila diperlukan penggantian suku cadang dari sepeda motor tersebut maka penyewa wajib menggantikan dengan suku cadang yang asli atau pemilik sendiri berhak menentukan penggantian termaksud atas beban penyewa dan dalam hal ini penyewa tidak dapat mengajukan keberatannnya kepada pemilik.
2.	Pemyewa akan segera memberitahukan kepada pemilik, apabila terdapat kerusakan berat dan jika terjadi kehilangan atau kehancuran, maka penyewa wajib mennganti sepeda motor tersebut atas biaya sendiri dengan suatu sepeda motor yang sejenis dan bernilai sama, jika dikehendaki oleh pemilik , maka penyewa wajib memperlihatkan sepeda motor tersebut kepada pemilik untuk diperiksa.

3. Penyewa dengan ini menegaskan bahwa segala kejadian yang terjadi atas sepeda motor tersebut            adalah menjadi tanggung jawab Penyewa sepenuhnya dan tidak dapat menjadi alasan untuk penundaan pembayaran uang sewa yang telah ditetapkan pada pasal III ayat 1.
4. Biaya perpanjangan STNK ,Jasa Raharja dan Pajak-pajak lainnya untuk tahun berikutnya tanggung jawab Penyewa sendiri.
5. Apabila Penyewa akan pindah tempat tinggal dari alamat tersebut di atas ke alamat yang lain sewaktu masa sewa sepeda motor berlangsung,maka wajib memberitahukan perpindahan itu secara tertulis kepada Pemilik paling lambat 7 (Tujuh) hari sebelum perpindahan Penyewa dilaksanakan.
6. Penyewa tidak berhak untuk menjual, menyerahkan , memindahtangankan , menggadaikan , menyewakan lagi  , memperdagangkan , menjadikan sebagai jaminan atau yang disebut dengan istilah lain , melepas hak sewa sepeda motor termaksud , juga tidak diperkenankan memindahkan dari tempat pendaftaran semula ke tempat atau daerah Kepolisian lain , oleh karena status sepeda motor tersebut bukan milik Penyewa , sehingga salah satu atau lebih tindakan di atas merupakan pelanggaran terhadap Kitab Undang-undang Pidana.
7. Kuasa-kuasa dan jaminan tersebut dalam perjanjian ini merupakan bagian yang sangat penting dan tidak terpisahkan dari Surat Perjanjian Sewa Beli , dengan tidak adanya kuasa-kuasa dan jaminan-jaminan tersebut, Perjanjian ini tidak dibuat atau tidak akan dibuat oleh karena itu kuasa-kuasa dan jaminan-jaminan tersebut akan batal atau tidak dapat dibatalkan karena alasan-alasan apapun juga dan tidak terkecuali kepada sebab-sebab yang tercantum dalam pasal 1813 Kitab Undang-undang Hukum Perdata.
						PASAL  VII
				                 BERAKHIRNYA  JUAL BELI
Bila Penyewa telah membayar seluruh harga sewa berikut denda Administrasi ( kalau ada ) maka sewa beli ini akan berakhir dan Penyewa lantas menjadi Pemilik dari sepeda motor termaksud.
						PASAL VIII
				         TANGGUNG JAWAB PENJAMIN
Penjamin dengan sukarela mengikatkan diri dalam Perjanjian ini untuk bertindak sebagai Penjamin yang telah terlebih dahulu melepaskan hak-hak istimewa yang diberi Undang-undang kepada seorang Penjamin/ Penanggung, dengan ini Penjamin mengaku dan menyatakan dengan tegas sanggup dan bertanggung jawab sepenuhnya untuk melunaskan seluruh uang sewa sepeda motor termaksud atau menjadi kewajiban Penyewa apabila terjadi tunggakan uang sewa oleh Penyewa atau Penyewa dinyatakan WAN PRESTASI ( tidak memenuhi kewajibannya ) oleh pemilik.
PASAL IX
KETENTUAN-KETENTUAN LAINNYA
1.Segala sesuatu atau tidak atau belum diatur dalam surat Perjanjian sewa Beli ini, akan ditetapkan dengan pemufakatan pemilik dan penyewa secara musyawarah.
2.Segala perselisihan dan perbedaan pendapat antara pemilik dan penyewa yang terjadi dan berkenaan dengan Surat Perjanjian Sewa Beli ini demikian juga terhadap hal-hal yang dianggap sebagai perselisihan, akan diselesaikan secara musyawarah dan mufakat.
3.Apabila penyelesaian secara musyawarah atau mufakat tidak mungkin tercapai maka para pihak yang mengikat diri dalam perjanjian ini memilih tempat kediaman hokum yang tetap dan tidak berubah (domicile) di kantor Pengadilan Negeri.
						PASAL X
					               PENUTUP
Surat perjanjian sewa beli ini berlaku sejak :
1.Ditanda tangani bersama oleh para pihak yang mengikat diri dalam perjanjian ini.
2.Uang muka seperti tersebut pada pasal III ayat I sudah dilakukan oleh Penyewa dan telah diterima oleh Pemilik.

Demikian surat Perjanjian Sewa Beli ini diperbuat dengan akal sehat serta jiwa yang waras tanpa ada unsure paksaan atau tekanandalam bentuk apapun serta telah dibaca atau disuruh bacakan dan telah dipahami isinya. Untuk itu pemilik dan penyewa menurunkan tanda tangan yang diikuti para saksi / penjamin.

							Diperbuat di Dabo Singkep pada tanggal tersebut di atas.
      PEMILIK									PENYEWA


   		

					     PENJAMIN


				      			
          SAKSI										        SAKSI



												

        ";
    }
}
