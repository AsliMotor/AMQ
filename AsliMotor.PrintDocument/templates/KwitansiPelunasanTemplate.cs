using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class KwitansiPelunasanTemplate
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
            <div style='padding-top:15px'>
                <div style='padding-bottom:30px;text-align:center;font-weight:bold;font-size:18px;'>KWITANSI PELUNASAN</div>
                <div class='rowheight indent'>
                    <table>
                        <tr><td width='150px'>Nomor Transaksi</td><td width='20px'>:</td><td>$transactionno$</td></tr>
                        <tr><td>Sudah terima dari</td><td>:</td><td>$custname$</td></tr>
                        <tr><td>Uang sebanyak</td><td>:</td><td>$terbilang$</td></tr>
                        <tr>
                            <td>Untuk pembayaran</td><td>:</td>
                            <td>
                                <div>
                                    <table>
                                        <tr><td>Pelunasan angsuran 1 ( Satu ) Unit kendaraan dengan :</td></tr>
                                        <tr>
                                            <td>
                                                <table style='margin-left:50px'>
                                                    <tr><td width='120px'>Merk / Type</td><td width='20px'>:</td><td>$merk$ / $type$</td></tr>
                                                    <tr><td>Warna</td><td>:</td><td>$warna$</td></tr>
                                                    <tr><td>Nomor Rangka</td><td>:</td><td>$norangka$</td></tr>
                                                    <tr><td>Nomor Mesin</td><td>:</td><td>$nomesin$</td></tr>
                                                    <tr><td>Nomor Polisi</td><td>:</td><td>$nopolisi$</td></tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr><td>Detil pembayaran sebagai berikut :</td></tr>
                                        <tr>
                                            <td>
                                                <table style='margin-left:50px'>
                                                    <tr><td>Banyak cicilan</td><td width='20px'>:</td><td>$banyakcicilan$ kali</td></tr>
                                                    <tr><td>Banyak cicilan yang telah dibayar</td><td>:</td><td>$cicilanyangtelahdibayar$ kali</td></tr>
                                                    <tr><td>Angsuran bulanan</td><td>:</td><td>Rp $angsuranbulanan$</td></tr>
                                                    <tr><td>Sisa tagihan</td><td>:</td><td>Rp $sisatagihan$</td></tr>
                                                    $totaldenda$
                                                    $sisatagihanplusdenda$
                                                    <tr><td>Diskon pelunasan (1%)</td><td>:</td><td>Rp $discount$</td></tr>
                                                    <tr><td>Total yang harus dibayar</td><td>:</td><td>Rp $total$</td></tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        <tr>
                            <td colspan='3'>
                                <table style='margin-top:30px;' width='100%'>
                                    <tr>
                                        <td style='padding-left:10px;'>
                                            <div style='margin-top:6pt; padding:6pt; border:2pt solid;background:#CACACA; font-size:12px; width:200px;'>Rp
                                                <span style='float:right;font-weight:bold;font-family:Tahoma;font-size:11pt;'>$total$</span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3'  align='right' style='padding-top:100px;padding-right:50px;'>
                                            $organization.City$, $currentDate$
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' align='right' style='padding-top:60px;padding-right:60px;'>
                                            .......................................
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>";
    }
}
