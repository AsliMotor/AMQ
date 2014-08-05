using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class KwitansiAngsuranTemplate
    {
        public const string DEFAULT = @"
            <style>
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
            <div style='padding-top:30px'>
                <div style='padding-bottom:20px;text-align:center;font-weight:bold;font-size:18px;'>KWITANSI ANGSURAN BULANAN</div>
                <div class='rowheight indent'>
                    <table>
                        <tr><td width='150px'>Nomor Transaksi</td><td width='20px'>:</td><td><b>$rcv.ReceiveNo$</b></td></tr>
                        <tr><td>Sudah terima dari</td><td>:</td><td>$rcv.CustomerName$</td></tr>
                        <tr><td>Uang sebanyak</td><td>:</td><td>$terbilang$</td></tr>
                        <tr>
                            <td>Untuk pembayaran</td><td>:</td><td>
                                <div>
                                    <table>
                                        <tr><td>Cicilan Bulan Ke <b>$BulanAngsuran$ ($BulanAngsuranFormated$)</b></td></tr>
                                        <tr>
                                            <table>
                                                <tr><td width='120px'>Merk / Type</td><td width='20px'>:</td><td>$rcv.Merk$ $rcv.Type$</td></tr>
                                                <tr><td>Warna</td><td>:</td><td>$rcv.Warna$</td></tr>
                                                <tr><td>Nomor Rangka</td><td>:</td><td>$rcv.NoRangka$</td></tr>
                                                <tr><td>Nomor Mesin</td><td>:</td><td>$rcv.NoMesin$</td></tr>
                                                <tr><td>Nomor Polisi</td><td>:</td><td>$rcv.NoPolisi$</td></tr>
                                                
                                            </table>
                                        </tr>
                                        <tr><td colspan='3'>Sesuai surat perjanjian No. <b>$rcv.NoSuratPerjanjian$</b> Tanggal $SuratPerjanjianDate$</tr>
                                        <tr><td colspan='3'>Dengan angsuran perbulan sebesar <b>Rp. $AngsuranBulanan$</b></td></tr>
                                        $DendaTemplate$
                                        $Deposit$
                                    </table>
                                </div>
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
                                        <td colspan='3' align='right' style='padding-top:60px;padding-right:80px;'>
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
