﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsliMotor.PrintDocuments
{
    public class SIStringTemplate
    {
        public const string DEFAULT = @"<style>
	              table tr td
	              {
	                  padding:3px 0px;
	              }
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
                    <table width='100%'  border='0' cellspacing='0' cellpadding='0' style='font-size:13px; font-family:Tahoma;'>
		                <tr>
			                <td colspan='3' align='center'><div style='font-weight:bold; font-size:18px; letter-spacing:2px; margin-top: 10px; margin-bottom: 15px;'>KWITANSI</div></td>
		                </tr>
                        <tr>
			                <td style='padding-left:10px;width:150px;'>
				                Nomor Kwitansi
                            </td>
						    <td>:</td>
						    <td style='padding-left:10px;'>
							    $SupplierInvoiceNo$
						    </td>
                        </tr>
                        <tr>
			                <td style='padding-left:10px;'>
				                Tanggal
                            </td>
						    <td>:</td>
						    <td style='padding-left:10px;'>$SIDate$</td>
		                </tr>
                        <tr>
			                <td style='padding-left:10px;'>
                                Sudah Terima Dari
                            </td>
                            <td>:</td>
                            <td style='padding-left:10px;'>
							    <b>$OrganizationName$</b>
						    </td>
                        </tr>
                        <tr>
			                <td style='padding-left:10px;'>
                                Uang Sejumlah
                            </td>
                            <td>:</td>
                            <td style='padding-left:10px;'>$terbilang$</td>
                        </tr>
                        <tr>
			                <td style='padding-left:10px;vertical-align:top;'>
                                Untuk Pembayaran
                            </td>
                            <td style='vertical-align:top;'>:</td>
                            <td style='padding-left:10px;'>1 (Satu) Unit Sepeda Motor dengan spesifikasi sebagai berikut:
                                <table width='100%' style='margin-left:10px;margin-top:7px;font-size:13px; font-family:Tahoma;'>
                                    <tr>
                                        <td width='150px'>Merk</td>
                                        <td width='20px'>:</td>
                                        <td>$merk$</td>
                                    </tr>
                                    <tr>
                                        <td width='150px'>Nomor Polisi</td>
                                        <td width='20px'>:</td>
                                        <td>$nopolisi$</td>
                                    </tr>
                                    <tr>
                                        <td width='150px'>Type</td>
                                        <td width='20px'>:</td>
                                        <td>$type$</td>
                                    </tr>
                                    <tr>
                                        <td width='150px'>Tahun</td>
                                        <td width='20px'>:</td>
                                        <td>$tahun$</td>
                                    </tr>
                                    <tr>
                                        <td width='150px'>Nomor Rangka</td>
                                        <td width='20px'>:</td>
                                        <td>$norangka$</td>
                                    </tr>
                                    <tr>
                                        <td width='150px'>Nomor Mesin</td>
                                        <td width='20px'>:</td>
                                        <td>$nomesin$</td>
                                    </tr>
                                    <tr>
                                        <td width='150px'>Nomor BPKB</td>
                                        <td width='20px'>:</td>
                                        <td>$nobpkb$</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style='padding-left:10px;vertical-align:top;'>Keterangan</td>
                            <td>:</td>
                            <td style='padding-left:10px;'>$note$</td>
                        </tr>
                        <tr>
                            <td style='padding-left:10px;'>
                                <div style='margin-top:6pt; padding:6pt; border:2pt solid;background:#CACACA; font-size:12px; min-width:150px;'>Rp
                                    <span style='float:right;font-weight:bold;font-family:Tahoma;font-size:11pt;'>$total$</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan='3'  align='right' style='padding-top:100px;padding-right:50px;'>
                                $organization.City$, $currentdate$
                            </td>
                        </tr>
                        <tr>
                            <td colspan='3' align='right' style='padding-top:100px;padding-right:60px;'>
                                .......................................
                            </td>
                        </tr>
                    </table>
                </div>";
    }
}
