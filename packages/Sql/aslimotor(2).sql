--
-- PostgreSQL database dump
--

-- Started on 2012-11-05 17:08:52

SET client_encoding = 'UTF8';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

--
-- TOC entry 354 (class 2612 OID 16386)
-- Name: plpgsql; Type: PROCEDURAL LANGUAGE; Schema: -; Owner: postgres
--

CREATE PROCEDURAL LANGUAGE plpgsql;


ALTER PROCEDURAL LANGUAGE plpgsql OWNER TO postgres;

SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 1527 (class 1259 OID 18406)
-- Dependencies: 3
-- Name: account; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE account (
    id bigint NOT NULL,
    username character varying,
    role integer,
    email character varying,
    datecreated date,
    lastactivitydate date,
    lastlogindate date,
    lastpasswordchangeddate date,
    ownerid character varying,
    branchid character varying,
    password character varying,
    name character varying
);


ALTER TABLE public.account OWNER TO aslimotor;

--
-- TOC entry 1538 (class 1259 OID 18519)
-- Dependencies: 3
-- Name: customer; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE customer (
    id uuid NOT NULL,
    name character varying,
    address character varying,
    city character varying,
    phone character varying,
    email character varying,
    branchid character varying,
    outstanding numeric,
    status integer,
    region character varying,
    birthday date,
    job character varying,
    gender character varying,
    ktpno character varying,
    ktppublisher character varying,
    ktpdate date
);


ALTER TABLE public.customer OWNER TO aslimotor;

--
-- TOC entry 1539 (class 1259 OID 18525)
-- Dependencies: 3
-- Name: invoicesnapshot; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE invoicesnapshot (
    id uuid NOT NULL,
    customerid uuid,
    productid uuid,
    branchid character varying,
    invoicedate date,
    price numeric,
    transactiondate timestamp with time zone,
    status integer,
    totalkredit numeric,
    lamaangsuran integer,
    sukubunga numeric,
    angsuranbulanan numeric,
    duedate date,
    charge numeric,
    invoiceno bigint NOT NULL,
    outstanding numeric
);


ALTER TABLE public.invoicesnapshot OWNER TO postgres;

--
-- TOC entry 1548 (class 1259 OID 18619)
-- Dependencies: 1539 3
-- Name: invoicesnapshot_invoiceno_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE invoicesnapshot_invoiceno_seq
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;


ALTER TABLE public.invoicesnapshot_invoiceno_seq OWNER TO postgres;

--
-- TOC entry 1900 (class 0 OID 0)
-- Dependencies: 1548
-- Name: invoicesnapshot_invoiceno_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE invoicesnapshot_invoiceno_seq OWNED BY invoicesnapshot.invoiceno;


--
-- TOC entry 1901 (class 0 OID 0)
-- Dependencies: 1548
-- Name: invoicesnapshot_invoiceno_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('invoicesnapshot_invoiceno_seq', 15, true);


--
-- TOC entry 1535 (class 1259 OID 18472)
-- Dependencies: 3
-- Name: logoorganization; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE logoorganization (
    id character varying NOT NULL,
    image bytea
);


ALTER TABLE public.logoorganization OWNER TO aslimotor;

--
-- TOC entry 1534 (class 1259 OID 18464)
-- Dependencies: 3
-- Name: organization; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE organization (
    branchid character varying NOT NULL,
    organizationname character varying,
    organizationaddress character varying,
    city character varying,
    country character varying,
    telp character varying
);


ALTER TABLE public.organization OWNER TO postgres;

--
-- TOC entry 1544 (class 1259 OID 18587)
-- Dependencies: 3
-- Name: perjanjianautonumberconfig; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE perjanjianautonumberconfig (
    id character varying NOT NULL,
    mode integer,
    prefix character varying,
    branchid character varying
);


ALTER TABLE public.perjanjianautonumberconfig OWNER TO aslimotor;

--
-- TOC entry 1545 (class 1259 OID 18595)
-- Dependencies: 3
-- Name: perjanjianautonumbermonthly; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE perjanjianautonumbermonthly (
    id character varying NOT NULL,
    branchid character varying,
    year integer,
    month integer,
    value integer
);


ALTER TABLE public.perjanjianautonumbermonthly OWNER TO aslimotor;

--
-- TOC entry 1546 (class 1259 OID 18603)
-- Dependencies: 3
-- Name: perjanjianautonumberyearly; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE perjanjianautonumberyearly (
    id character varying NOT NULL,
    branchid character varying,
    year integer,
    value integer
);


ALTER TABLE public.perjanjianautonumberyearly OWNER TO aslimotor;

--
-- TOC entry 1528 (class 1259 OID 18415)
-- Dependencies: 3
-- Name: product; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE product (
    id uuid NOT NULL,
    merk character varying,
    type character varying,
    tahun character varying,
    warna character varying,
    norangka character varying,
    nomesin character varying,
    nobpkb character varying,
    hargabeli numeric,
    status character varying,
    nopolisi character varying,
    branchid character varying
);


ALTER TABLE public.product OWNER TO aslimotor;

--
-- TOC entry 1536 (class 1259 OID 18503)
-- Dependencies: 3
-- Name: productlog; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE productlog (
    id uuid NOT NULL,
    username character varying,
    productid uuid,
    action integer,
    merk character varying,
    type character varying,
    tahun character varying,
    warna character varying,
    norangka character varying,
    nomesin character varying,
    nobpkb character varying,
    hargabeli numeric,
    status character varying,
    nopolisi character varying,
    branchid character varying,
    datetime character varying
);


ALTER TABLE public.productlog OWNER TO aslimotor;

--
-- TOC entry 1543 (class 1259 OID 18561)
-- Dependencies: 3
-- Name: receive; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE receive (
    id uuid NOT NULL,
    invoiceid uuid,
    receiveno character varying,
    receivedate date,
    receivetype integer,
    charge numeric,
    total numeric,
    branchid character varying,
    denda numeric,
    month character varying
);


ALTER TABLE public.receive OWNER TO aslimotor;

--
-- TOC entry 1540 (class 1259 OID 18537)
-- Dependencies: 3
-- Name: receiveautonumberconfig; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE receiveautonumberconfig (
    id character varying NOT NULL,
    mode integer,
    prefix character varying,
    branchid character varying
);


ALTER TABLE public.receiveautonumberconfig OWNER TO aslimotor;

--
-- TOC entry 1541 (class 1259 OID 18545)
-- Dependencies: 3
-- Name: receiveautonumbermonthly; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE receiveautonumbermonthly (
    id character varying NOT NULL,
    branchid character varying,
    year integer,
    month integer,
    value integer
);


ALTER TABLE public.receiveautonumbermonthly OWNER TO aslimotor;

--
-- TOC entry 1542 (class 1259 OID 18553)
-- Dependencies: 3
-- Name: receiveautonumberyearly; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE receiveautonumberyearly (
    id character varying NOT NULL,
    branchid character varying,
    year integer,
    value integer
);


ALTER TABLE public.receiveautonumberyearly OWNER TO aslimotor;

--
-- TOC entry 1529 (class 1259 OID 18424)
-- Dependencies: 3
-- Name: siautonumberconfig; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE siautonumberconfig (
    id character varying NOT NULL,
    mode integer,
    prefix character varying,
    branchid character varying
);


ALTER TABLE public.siautonumberconfig OWNER TO aslimotor;

--
-- TOC entry 1530 (class 1259 OID 18432)
-- Dependencies: 3
-- Name: siautonumbermonthly; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE siautonumbermonthly (
    id character varying NOT NULL,
    branchid character varying,
    year integer,
    month integer,
    value integer
);


ALTER TABLE public.siautonumbermonthly OWNER TO aslimotor;

--
-- TOC entry 1531 (class 1259 OID 18440)
-- Dependencies: 3
-- Name: siautonumberyearly; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE siautonumberyearly (
    id character varying NOT NULL,
    branchid character varying,
    year integer,
    value integer
);


ALTER TABLE public.siautonumberyearly OWNER TO aslimotor;

--
-- TOC entry 1532 (class 1259 OID 18448)
-- Dependencies: 3
-- Name: supplierinvoice; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE supplierinvoice (
    id uuid NOT NULL,
    branchid character varying,
    transactiondate timestamp with time zone,
    supplierinvoicedate date,
    supplierinvoiceno character varying,
    suppliername character varying,
    supplierbillingaddress character varying,
    merk character varying,
    type character varying,
    tahun character varying,
    warna character varying,
    norangka character varying,
    nomesin character varying,
    nobpkb character varying,
    nopolisi character varying,
    hargabeli numeric,
    note character varying,
    notelp character varying,
    productid uuid
);


ALTER TABLE public.supplierinvoice OWNER TO postgres;

--
-- TOC entry 1537 (class 1259 OID 18511)
-- Dependencies: 3
-- Name: supplierinvoicelog; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE supplierinvoicelog (
    id uuid NOT NULL,
    username character varying,
    action integer,
    datetime character varying,
    branchid character varying,
    transactiondate timestamp with time zone,
    supplierinvoicedate date,
    supplierinvoiceno character varying,
    suppliername character varying,
    supplierbillingaddress character varying,
    merk character varying,
    type character varying,
    tahun character varying,
    warna character varying,
    norangka character varying,
    nomesin character varying,
    nobpkb character varying,
    nopolisi character varying,
    hargabeli numeric,
    note character varying,
    notelp character varying,
    productid uuid,
    supplierinvoiceid uuid
);


ALTER TABLE public.supplierinvoicelog OWNER TO aslimotor;

--
-- TOC entry 1547 (class 1259 OID 18611)
-- Dependencies: 3
-- Name: suratperjanjian; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE suratperjanjian (
    invoiceid uuid NOT NULL,
    suratperjanjianno character varying,
    suratperjanjiandate timestamp with time zone
);


ALTER TABLE public.suratperjanjian OWNER TO aslimotor;

--
-- TOC entry 1526 (class 1259 OID 18404)
-- Dependencies: 3 1527
-- Name: tbl_user_id_seq; Type: SEQUENCE; Schema: public; Owner: aslimotor
--

CREATE SEQUENCE tbl_user_id_seq
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;


ALTER TABLE public.tbl_user_id_seq OWNER TO aslimotor;

--
-- TOC entry 1902 (class 0 OID 0)
-- Dependencies: 1526
-- Name: tbl_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: aslimotor
--

ALTER SEQUENCE tbl_user_id_seq OWNED BY account.id;


--
-- TOC entry 1903 (class 0 OID 0)
-- Dependencies: 1526
-- Name: tbl_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: aslimotor
--

SELECT pg_catalog.setval('tbl_user_id_seq', 6, true);


--
-- TOC entry 1533 (class 1259 OID 18451)
-- Dependencies: 3
-- Name: typeproduct; Type: TABLE; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE TABLE typeproduct (
    id uuid NOT NULL,
    name character varying,
    branchid character varying
);


ALTER TABLE public.typeproduct OWNER TO aslimotor;

--
-- TOC entry 1815 (class 2604 OID 18409)
-- Dependencies: 1526 1527 1527
-- Name: id; Type: DEFAULT; Schema: public; Owner: aslimotor
--

ALTER TABLE account ALTER COLUMN id SET DEFAULT nextval('tbl_user_id_seq'::regclass);


--
-- TOC entry 1816 (class 2604 OID 18621)
-- Dependencies: 1548 1539
-- Name: invoiceno; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE invoicesnapshot ALTER COLUMN invoiceno SET DEFAULT nextval('invoicesnapshot_invoiceno_seq'::regclass);


--
-- TOC entry 1874 (class 0 OID 18406)
-- Dependencies: 1527
-- Data for Name: account; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY account (id, username, role, email, datecreated, lastactivitydate, lastlogindate, lastpasswordchangeddate, ownerid, branchid, password, name) FROM stdin;
5	dny	5	dny@gmail.com	2012-10-08	2012-10-08	2012-10-08	\N	dny@gmail.com	dny@gmail.com	123	Denny
6	dnywu	1	dnywu@gmail.com	2012-10-08	2012-10-08	2012-10-08	\N	dny@gmail.com	dnywu@gmail.com	123	Apin
\.


--
-- TOC entry 1885 (class 0 OID 18519)
-- Dependencies: 1538
-- Data for Name: customer; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY customer (id, name, address, city, phone, email, branchid, outstanding, status, region, birthday, job, gender, ktpno, ktppublisher, ktpdate) FROM stdin;
caae2d23-e352-4e23-a027-bb6dad17eea8	Denny Wu	Marok Tua Jln Soedirman No 1 RT 5 RW 10 	Dabo Singkeep	0856658914	deny@inforsys.co.id	dny@gmail.com	0	0	Lingga	\N	Wiraswasta	Laki-laki	34589634087569	Noordin	2009-09-29
\.


--
-- TOC entry 1886 (class 0 OID 18525)
-- Dependencies: 1539
-- Data for Name: invoicesnapshot; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY invoicesnapshot (id, customerid, productid, branchid, invoicedate, price, transactiondate, status, totalkredit, lamaangsuran, sukubunga, angsuranbulanan, duedate, charge, invoiceno, outstanding) FROM stdin;
415e7d2a-9455-427e-97aa-3b4466abfbb7	caae2d23-e352-4e23-a027-bb6dad17eea8	cc987714-20ea-4d20-b9e1-cc1bb167f51f	dny@gmail.com	2012-10-27	9000000	2012-10-27 04:15:51.35236+07	1	10050000	24	25	418750	2012-12-29	200000	1	\N
5729ffb8-c9e1-4599-96e2-f0ae8eef22ca	caae2d23-e352-4e23-a027-bb6dad17eea8	aef8500b-b143-4e61-badc-128c637458ec	dny@gmail.com	2012-10-27	9500000	2012-10-27 18:27:51.407891+07	1	11100000	24	24	462500	2012-11-29	200000	2	\N
11ad058b-5699-413c-8ab5-ebc7381eef53	caae2d23-e352-4e23-a027-bb6dad17eea8	aef8500b-b143-4e61-badc-128c637458ec	dny@gmail.com	2012-10-29	9000000	2012-10-29 00:03:35.490955+07	2	0	0	0	0	\N	0	4	\N
6baa5511-2bfb-4660-a4c7-f8693bb22b39	caae2d23-e352-4e23-a027-bb6dad17eea8	501b16d5-47e0-4594-ae3c-186ccc5d6870	dny@gmail.com	2012-10-29	9800000	2012-10-29 00:09:50.85934+07	2	0	0	0	0	\N	0	6	\N
aff7a01f-cc09-434b-9db9-fc0e4d9c87c8	caae2d23-e352-4e23-a027-bb6dad17eea8	501b16d5-47e0-4594-ae3c-186ccc5d6870	dny@gmail.com	2012-10-28	9500000	2012-10-28 23:53:37.074601+07	1	10656000	24	24	444000	2012-12-05	200000	3	\N
67036ec0-618e-4362-aa74-dbd244d71cc1	caae2d23-e352-4e23-a027-bb6dad17eea8	aef8500b-b143-4e61-badc-128c637458ec	dny@gmail.com	2012-11-02	7000000	2012-11-02 15:06:15.829963+07	2	0	0	0	0	\N	0	7	\N
673402e4-6f86-4f3c-8980-be537c8e6272	caae2d23-e352-4e23-a027-bb6dad17eea8	cc987714-20ea-4d20-b9e1-cc1bb167f51f	dny@gmail.com	2012-10-29	8900000	2012-10-29 00:06:42.863425+07	1	10148000	36	24	281889	2013-03-05	300000	5	\N
e1344023-e143-42be-84fb-d280c459c85f	caae2d23-e352-4e23-a027-bb6dad17eea8	56e3b5f3-a1c8-41fd-b0dc-59fdd697d50b	dny@gmail.com	2012-11-03	8300000	2012-11-03 18:03:58.502578+07	1	7844000	24	24	326833	2013-04-08	200000	11	\N
14ddceaf-6a68-4a35-9144-c7d1c3c8076b	caae2d23-e352-4e23-a027-bb6dad17eea8	a04335bf-3025-472f-b44c-1b62f44a82f7	dny@gmail.com	2012-11-03	9300000	2012-11-03 17:57:28.529273+07	2	0	0	0	0	\N	0	10	\N
00ccb07b-f0d4-42cf-b75c-857a4d267c7b	caae2d23-e352-4e23-a027-bb6dad17eea8	501b16d5-47e0-4594-ae3c-186ccc5d6870	dny@gmail.com	2012-11-03	7800000	2012-11-03 17:54:14.018147+07	2	0	0	0	0	\N	0	9	\N
a7755f91-e12f-44ec-a54c-19e598515098	caae2d23-e352-4e23-a027-bb6dad17eea8	cc987714-20ea-4d20-b9e1-cc1bb167f51f	dny@gmail.com	2012-11-02	9000000	2012-11-02 16:25:59.568748+07	2	0	0	0	0	\N	0	8	\N
57adc86a-42ec-4d69-bbff-3734d72b10db	caae2d23-e352-4e23-a027-bb6dad17eea8	1531cdc8-4d63-4a60-8f14-3ab6139932f4	dny@gmail.com	2012-11-03	8300000	2012-11-03 18:24:23.411368+07	2	0	0	0	0	\N	0	12	\N
316cd89b-f0d1-4654-8470-efadecbbb103	caae2d23-e352-4e23-a027-bb6dad17eea8	a98c62b3-8c80-4573-a696-465c58645e45	dny@gmail.com	2012-11-03	9000000	2012-11-03 18:26:05.154747+07	1	8800000	36	20	244444	0001-02-01	300000	13	\N
5717bd9a-14cc-49f7-8c3c-1bae0b37ce6a	caae2d23-e352-4e23-a027-bb6dad17eea8	5b796f18-09d7-408a-a4c7-c15ae063d6a4	dny@gmail.com	2012-11-04	8500000	2012-11-04 14:41:08.982981+07	1	5600000	36	20	155556	0001-02-01	300000	14	\N
ad3c0d1a-a74c-49f7-8b68-0408b5c58de2	caae2d23-e352-4e23-a027-bb6dad17eea8	d3ae9960-5031-4301-bb8b-19d2c9457cd2	dny@gmail.com	2012-11-04	8000000	2012-11-04 14:52:42.45428+07	1	10804000	24	23	450167	2012-12-04	200000	15	\N
\.


--
-- TOC entry 1882 (class 0 OID 18472)
-- Dependencies: 1535
-- Data for Name: logoorganization; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY logoorganization (id, image) FROM stdin;
dny@gmail.com	]\\000\\000\\200\\000bv\\000\\000\\000\\000\\000\\000\\000D\\224\\005\\304z'\\366\\367\\356\\211\\216P\\220\\210\\263\\252\\314\\033.\\206\\241\\353\\2654\\262\\3727\\326t\\324\\233 \\363S\\241\\242\\244\\325:i\\367g+*\\016\\251\\321$\\025\\246y\\034\\177|p\\205\\030[\\265\\357\\265|\\2018/\\3471(\\366\\357\\214\\302`\\006\\314'\\344q6\\000I\\221\\374\\0118\\237\\244SRM\\342\\220P\\361c\\274\\027\\227\\177\\357j\\227V\\361\\017\\030\\3157\\313\\206\\322\\034u\\027\\245,f\\200\\362\\351\\304P\\202X\\017\\022\\306O\\303\\233\\355O\\214\\004\\365R1\\022\\320x\\365J\\026\\272\\306_\\271\\257:\\020\\345U\\231gd\\312P()\\353QP\\306\\210\\3703\\011{\\306\\254nL2\\300\\020[*i\\2158\\207\\255b\\333#\\337<q\\003P\\200>\\306\\027 \\314d$D\\266\\246Vn%\\232(:\\372\\362&O\\031\\005['\\202*\\223]\\274$\\235\\314\\237N\\227 \\021\\244\\030\\220\\223\\012"\\013w\\351`\\266zA\\242j\\276\\003x^\\361\\342G\\220,&2\\220\\356p\\370\\376\\344\\353\\354\\3029\\225\\302\\307\\351F\\350\\337\\342\\321[\\206n\\252V\\225\\003,e)\\004\\245\\231P\\316\\207\\015\\341c\\236V\\033\\242\\352\\321\\320\\026D\\220\\366\\356\\272,\\345\\321\\013\\001\\243\\360\\315W0d\\2576H\\342\\323\\237\\035\\333lk=/\\246\\373\\015\\221\\022\\274\\236\\235\\261/v\\366\\255\\032\\324\\3720o\\224V\\267J\\034\\332\\3458\\374N\\227\\371lL\\326<5\\257\\200\\372\\276\\310\\234\\241\\256\\0076G\\333\\275\\021\\372\\030;+`NzRv|\\205f\\217\\337c+\\023H\\343\\370\\335IZ\\335 +\\357{\\353?]\\311R\\253?\\311\\260\\214\\325;bLBx\\253a[\\275'm\\032\\227\\340\\306\\247\\275\\267y`\\235\\253\\037\\333J\\264\\223\\367\\026\\221-\\024\\220\\213\\250\\255\\244\\366\\230\\023}\\014:\\332\\354!\\2205\\355\\357\\236.\\034\\203}\\177\\212\\025\\3060\\205#\\210\\252A\\025\\346\\003\\206\\307|Z`C[\\200?\\20322\\212v\\222\\232\\235")\\304"\\230\\233\\273\\363"En\\2612\\326\\361\\340U\\236\\272\\011\\215\\261[\\353\\347pR\\213\\251\\350A\\314a\\207\\351P\\366\\216\\034\\006I]\\361N\\253!\\3276\\363\\214\\034\\204\\202)\\361\\034\\001\\217l\\214\\362+5\\251xm(\\270\\2374\\340[\\002t)\\370\\275\\271\\215\\363\\217\\302?\\341\\230M\\300=\\225\\247\\020(w\\016\\004i\\213\\331\\343K\\364\\263U\\273\\277w\\226\\363(Z\\365E\\025!4|x\\244\\031B\\011\\034\\023\\214\\034\\303O\\345X\\330\\326i\\365\\032C<\\222\\350\\312\\004\\267\\227\\346:\\350\\325\\275ck\\350\\230\\322&\\251\\274$[\\243H\\206\\320\\247PBct\\206\\251\\312\\350\\242\\304K\\253\\255+7>\\272\\214\\356\\012\\267\\030\\262\\371\\344[\\001\\341*E\\014\\206\\2332\\217\\001\\227\\333\\364o>\\023\\244\\324\\207\\307\\005\\010n\\374\\365|[Hf\\374%\\220M*\\332)\\001\\341\\215\\000\\225\\205\\341\\024\\021\\235#\\220\\220\\350\\302LW\\300\\237A\\304>\\013\\211\\177h\\021\\014Z\\340G\\267\\345'%uu\\212\\311klt}\\254\\360*\\034\\211\\006\\266\\217\\247\\313\\005\\023"\\231\\224\\307\\346|&/b\\340\\351\\024\\371\\203\\363<\\3247\\007\\217\\273T\\257\\243\\323\\215\\330\\224\\374\\310e2e\\207a$\\177{\\365\\307\\252@\\253\\033\\220\\271\\264\\2628n\\270\\276\\340z\\300E\\000\\342\\320_\\3548\\204\\3240\\305\\340\\321O\\245\\354?\\354\\370Y\\006\\273x\\241/\\305\\013\\317\\212\\347\\335m\\346\\260v\\203.\\253_3;\\304\\006\\205N\\362\\022\\015\\250\\227\\335\\311\\375\\023\\311\\376\\215\\334\\036\\241\\265\\362\\320\\355\\204>\\204\\011\\247\\232\\326p\\363#O}\\333\\346\\000X\\271\\262\\311\\351D\\313\\266\\315\\027s"l\\316k#\\211"e\\001\\254\\232\\014O<;\\\\q\\246w\\305\\312\\2722\\012\\307y\\254\\341\\351.\\004c\\332\\372\\211#\\007\\003\\333\\337Xp\\252w\\033JX\\015\\263\\322p\\252\\270(-o&\\233`\\327\\012\\267\\212\\035~\\014\\013>\\202Q\\2748\\270;&Qy\\004\\325\\230\\330\\242'&p\\356\\366\\233\\200:]<\\200\\015\\016\\264\\264 \\302\\213?\\325\\265\\334\\3653\\007\\345\\203\\305{\\312\\322A\\247\\374]\\025\\255c\\317\\300\\262/\\216\\036\\245w>\\013ex\\016G\\002\\271`rO\\310\\037y\\250\\215\\005\\337\\033A\\213\\015E\\206\\210\\001\\210\\232\\237\\006\\013k\\356{\\332\\251\\305\\004\\357\\223\\273\\354!\\373Cv\\027\\020\\305\\2339r\\324\\000\\363\\235\\002\\362\\342\\302\\015\\244\\200\\022\\242X\\332`\\275\\031\\325-q\\254\\017\\220\\305&\\000\\330\\220\\336pbZ]JGO\\312:\\006\\003fl\\366fe\\277b\\316\\325\\247\\213Qd|=\\023\\000\\017\\0305p\\370\\253v\\264\\240\\014\\212\\372C0\\327Z\\010\\374Ft\\013\\366\\002\\264vJLY\\2546/\\230y\\221\\225\\006zo\\340\\237G\\363;x%R\\252\\035Qy\\220?K\\023W\\205i\\346\\254a\\367\\225\\352\\357\\304V\\311\\251oS\\035\\211\\322\\336\\333\\304\\005t;\\331\\264X\\373\\357\\222n\\023\\3051\\367\\346b\\264t\\222\\305e\\227\\373lu\\214Rk\\354\\334\\337<\\343\\361\\023\\256\\256x]\\377\\245\\272=:\\000\\012A\\363\\2406 .n\\230\\353\\320\\006h<\\351\\252\\232\\256w\\205\\263\\247\\264q\\211?\\376\\243Nt,\\004v\\362\\211\\015+\\314T\\355\\320\\236\\036z\\236\\011c\\271\\333&\\031\\320\\025F\\323\\272\\311s\\277Nr\\357M\\236\\376\\317s\\250\\251\\256\\310\\341\\314\\247x\\264yj\\365\\035\\230n\\252&\\220hF\\227~9)c\\207E\\021\\201\\336"\\014\\272\\272E9%\\235\\377%\\267\\231E\\332@\\272\\310\\322\\23491\\357\\033\\352b\\337zK\\200%N8\\034\\307\\261w>\\177\\265\\267R\\212\\354\\231\\361\\323\\217\\300l\\372\\223:\\271 \\253\\376\\346\\324\\177)\\272\\277,\\002R\\017~\\006\\217i\\336abQo\\357C3Xe\\312\\314m~o$ZGi\\223D\\206ev\\365Ue\\335{\\210\\361\\021\\025I\\377\\246q\\210\\020\\027}\\013\\017\\257\\371\\326\\221h\\022\\252\\271_\\016\\213q"~\\032\\022\\373\\265\\035\\225\\005-\\000u\\331o\\340T\\236\\202\\3151, E\\261\\237O\\363H\\011\\303\\372\\205p\\2336\\024\\014\\360-\\246\\241\\351/\\3554\\220kLP\\000f:jx[\\217B\\222\\331\\376\\363\\035j\\370\\213\\307\\3236\\225\\236V\\241\\025\\252)\\367\\373\\023a\\350\\231O\\363\\202\\034\\012\\366OZ\\355ZD\\021N\\003\\375\\360[D\\037\\036S#\\244H\\323\\217\\354}o-\\376\\354-\\256Q\\027=\\237\\374OU\\210\\272\\254n<v\\226\\224\\011\\313\\376G:o\\224&\\224\\233\\350\\236\\301N"\\220\\035M\\213\\313\\254\\363\\275\\302\\316\\275=\\205\\365\\361\\325\\374C\\373\\355\\310n\\3370o\\373 \\222,T1\\324|Z\\036\\217[k\\253m\\251j\\005\\357\\016X\\345<\\221\\226\\310\\333\\260j\\266%I\\245j\\002j\\224\\006I\\316\\260\\010\\204\\366`u\\304\\213\\024M\\323n_\\344\\013.\\251X\\233\\362\\231N\\333XH\\004<L\\205\\332+6\\331\\224-i\\023\\360e\\275\\314\\343>\\2266n\\316\\341\\001\\002\\213\\034\\356\\2555Y\\373\\253(}\\270\\331\\321Ce,b*\\374I\\00076Ch\\260\\232M\\0301\\3027\\214d!\\204\\013\\206W\\344\\260\\221Y\\206\\006\\344\\356\\021~\\367Q\\367]Va\\374\\355gE\\316(\\201\\270eb\\321v \\334\\2232\\222\\204\\252\\232i\\022w\\027Xa\\233\\347\\324\\025Z\\004\\001\\311\\345\\324\\266\\351,\\213\\222\\373~(He\\200)3\\332\\266\\337S\\271z\\302g\\214\\244\\252\\002np\\344\\006z\\347\\2334\\341\\231\\337c)\\251\\027\\342\\363\\305\\035\\242W\\367\\365DP\\276v\\233\\210\\375\\005\\302o\\036\\277\\022#\\015I\\253\\243\\314\\270\\322\\201\\340\\263\\251\\201\\243Fa(\\234\\275lB\\002\\316\\224Y\\247\\\\\\265E\\367\\020\\234\\037\\221Yy\\316Z\\000\\255\\236<\\214\\351\\030\\035(F .\\372\\222&\\010\\300\\243\\375H\\013\\364\\177\\256l\\252\\013Vi\\024\\250ql44\\337z\\245\\203s\\325\\215G\\337\\230\\265\\344\\022{\\011\\023\\342|\\276%\\214_+\\317\\217\\255\\032\\233\\255\\016\\243\\244\\000\\260\\326"\\367O\\002\\311\\302\\214\\352:]\\030\\330\\257\\251)$\\012##\\027\\301\\220O}B\\246,E\\216\\367{j\\263]v\\360\\245\\3322,da\\322\\306~[Fr\\000\\254\\177\\247\\201,\\203\\303g%\\336\\014\\024\\325\\216\\025\\304c\\013AY\\347I/\\030\\030J\\026\\376\\266g~\\345\\202E\\025\\233t\\012\\220\\212\\354\\311D\\006}U\\3126\\266|\\017\\345\\202\\300H\\334C\\223\\207\\351?0-}\\351e\\353\\033\\227\\346q]v\\307x\\027Y5\\271\\265b\\304\\242\\2737\\004o\\236\\222\\301{\\007\\301\\352%\\241\\024t\\233\\366\\317\\016\\342\\255\\0321S1c'VNG,~i/\\226\\344ve\\216\\242dQ\\207\\340y\\311\\324\\323+:\\237\\373\\307\\023T\\301X\\237\\335\\244\\306\\376\\270\\020!&\\211\\360^\\346\\206\\363z\\2716\\255(\\003\\217\\373\\372\\334\\177\\242\\354\\367}\\024\\002D&O\\240\\325^\\215\\023\\317n7\\212\\236\\202c@\\244jC\\254+jO\\342+si`\\025\\350\\333=\\254z\\007\\222\\026\\252\\350R\\346?)\\030\\313~\\275$\\211\\001x\\010\\307\\310\\2212\\224\\242F4j\\234#\\320\\373\\254\\206wsy\\330-z\\243\\014)?\\034\\311\\012\\310Ea\\214\\365\\005D\\2207\\026d\\233h\\344"\\220\\313\\264\\232VTi\\314u\\332\\223[VZ\\023\\245\\320\\212\\375\\207\\364\\237\\244\\023u3\\244\\341Z\\312\\255\\363\\254\\204\\351\\304o\\204#O\\010\\004!Y\\306BL\\315P\\376\\254\\345\\3704\\034sm\\341<>B\\014"d\\015\\243\\341\\007\\375\\027\\276^|\\3461\\346\\202%4\\340\\033\\001[\\247*\\321\\002j\\226\\012\\351a\\024\\253\\3442#{\\034p\\312\\355\\356))\\227\\362h\\2178d\\013\\2421\\037|\\273\\245X\\262\\001ax\\220\\2602h^\\355\\203\\317\\000\\353t \\246R\\003c\\035n{R\\235\\305\\225`M\\214GR\\0355\\323\\370\\264R\\365(0\\335\\240~\\251\\227p\\010\\036]\\322\\005\\322R\\201\\305\\004z\\350\\242 \\015\\366'\\213\\261\\265\\301\\344\\321\\301Z92\\255\\231\\213bq\\333\\230 \\321\\2001\\210\\341+\\005)Xb\\274Vm\\246Q\\\\\\202\\235\\011\\342\\335\\222 \\010\\305\\240s\\355R~zr\\305H\\000\\245\\300\\357\\026\\214\\325$6OG\\312\\316\\312\\346\\203P9\\201\\351\\315-G\\024LF\\347'\\336^\\001\\037\\232\\241#\\354`\\343\\\\N\\364\\247v\\336\\246\\323\\357+3\\033+\\211\\277\\330j\\376\\031p_\\020T\\206Yu\\247\\001\\306:\\036\\266\\237\\373\\335\\256\\275\\264\\011\\334\\313\\263\\377\\272\\223\\015\\323\\300\\267z\\327\\337\\240z"\\262\\324\\264\\271@pp\\315\\255\\232\\3067\\352\\025\\024$(%\\364\\177\\377s\\356N}/\\005\\322\\210L\\177\\306Q\\267Ml\\304=\\231pY\\250\\274\\235\\307\\366\\033\\204\\232\\272\\376dT\\320\\001=\\346\\241c\\223\\023\\335\\323\\250\\352\\361\\221Oz\\237\\213\\350\\375\\004\\363\\366i[\\322\\302\\336\\210w]\\304\\377\\260\\312\\201\\362\\367\\373a\\030\\215k\\022\\022\\257a\\327j\\253\\026\\304C\\177\\304\\332\\033\\221\\325^r\\2662\\344\\357\\326~V\\254\\311A)\\310\\263T8J\\204\\266\\372\\246\\256H0\\221'\\030v3\\036\\304\\205(\\340Ip\\362\\222\\262\\270\\212;\\214`\\222\\211y\\017JKW\\343\\310\\0355\\346\\324\\233\\024\\272\\036r\\210r\\353rw\\311\\017\\273%n\\377\\346\\306fiD\\200\\233!\\302\\271.s[\\241\\032\\261K\\311\\307\\276hA'No\\337\\244A\\032\\266\\020D_\\033\\250\\010k\\320\\311\\224\\001\\266\\3722\\241\\317\\300\\017\\3041h\\312\\322 \\206t\\005(\\311\\347^x\\270\\234|_\\242\\217b&\\373\\323\\323\\337J\\225C\\262\\320&\\252\\221\\335\\270\\021\\323\\354\\267\\225\\340\\251\\306\\370Kzr;\\352I\\363>\\363\\365"\\300\\312u0\\326HTF*\\032\\236\\314\\301\\271\\374\\037&x\\357\\304\\014\\276\\353\\245\\204V~\\357q\\243'\\007o44\\220\\014m\\330\\220[%`A\\031p\\204\\231\\262\\361x\\352I\\236v\\234\\355U=\\2404\\324a\\343\\233\\230\\327\\241-9\\271`[\\361\\232\\302Y\\200\\251\\202\\344EZ\\022Z\\341V\\267\\256\\024\\343\\321\\265Ow\\330\\223\\224\\215ZNPx\\362>\\326\\312\\005\\343\\340)\\011\\351\\261\\263[J\\207\\353\\270^\\3742\\274\\260\\341*\\254\\213\\226\\240$\\036\\366uuw\\265v\\211\\303\\027\\202C\\354\\260\\231\\336Y\\354\\377\\366\\377\\256\\257\\226}x\\230\\374\\252\\271\\233R\\013\\3543\\037E\\302\\255\\027\\227\\0327@9<bx\\214\\007\\321\\224)\\346\\223\\2720b\\307Nv\\237\\261SpJ#\\271Oce\\340+\\303|cF2\\037\\344\\177\\365\\374o\\030&\\302\\344\\314\\361\\205n\\361\\030\\350M\\273\\367\\272\\224 y\\252\\014\\330\\304H\\027y\\357\\220\\326\\365\\241\\030PN\\214\\353\\333,\\340\\232H\\354\\215(\\323R\\026\\217\\347S\\015\\225\\330\\014\\275_E\\252\\011\\007\\315\\306^\\215\\012F\\322\\222T\\333/\\035t\\335\\230S\\304\\267\\302j\\365\\327kcs9j/\\3677\\255\\316\\0271\\325\\315\\331\\036\\221\\013T\\0113\\021\\215\\361n\\204\\256\\022\\177sii\\275\\250~>\\247\\260\\364\\241\\255\\230*\\346U\\006\\242=d\\016\\314-\\237Cm\\317\\234im\\2506\\215&\\321b\\364\\255\\230\\312U\\266\\270\\260VHA\\363P_o\\325\\254\\235\\371\\22435\\363Z\\315\\376\\252\\3561\\253\\317\\032\\2414;]\\271^#\\314\\335\\240`#DW\\005\\214\\231X\\244\\301Hrq\\301`\\250\\340O2\\374*\\\\\\234\\031\\220\\235\\037\\335C\\26587\\223\\304,\\276\\377TX\\301\\3471a\\002'\\274\\031T\\331h\\252\\250\\346R\\305@\\315\\240\\223\\215\\2736\\277\\017\\245N\\204\\263\\227\\261,\\257x\\204\\307\\214lD\\341\\205\\334l\\3473\\226o\\265\\215\\2741\\364\\276E\\275nn\\344\\240\\033\\205\\227~\\014\\266(#=\\231\\024\\030\\350\\2628\\250\\364o\\023\\222[\\220z}\\256=\\354\\336\\032Y#\\305/\\3128m3\\322\\307\\255i\\235\\017\\225\\352\\2523'\\254\\234U\\001\\351\\003@\\016=\\3000\\0354oD\\375\\032\\2458\\306\\357\\326\\225\\232\\375\\036\\347\\350\\211\\345\\330}au\\363|r\\362\\252\\221\\332\\011#\\273\\351\\360\\327[u\\366\\304/}\\321\\347\\246\\374\\245\\325\\325\\002G#\\213\\026\\002J\\306\\361\\\\|\\361\\0032\\262<\\332\\017\\335p\\252\\214\\343s\\\\\\252\\0231\\311v+]Z;\\355,F7\\021By\\265\\021zb\\314-\\372\\342\\202\\324\\020?\\207\\253El',h\\210]\\332CK\\301\\030\\262\\262\\014\\234\\030K\\000\\030\\331l\\247\\265\\020\\240\\336\\306\\276\\303\\337%\\032\\207K\\011\\364\\021\\205\\206i\\366\\337\\247\\3700\\305\\240\\006\\355_\\227==\\332\\251\\306\\376\\354/\\001O\\323\\237\\3503\\334\\205\\177\\256E\\027\\332\\374\\331\\305\\221\\022\\265\\354L^~\\240\\265O\\020\\310{\\257\\220\\364\\372\\003\\214mc\\341\\345S>i\\232\\342L\\226\\202=\\302P_\\371\\266\\325@\\261\\006\\037\\264\\255o\\015R'\\215@m\\253\\016\\332\\352\\324\\330r\\354\\300\\305\\015~\\203\\017\\225S\\226\\235\\223V\\330s\\275\\254\\033\\011}Jn\\021\\251>\\207\\211fucr!V\\343\\250\\306Z\\242\\216\\274\\214\\305\\262\\364\\002\\364\\256k\\275\\213N\\344\\026\\026\\375\\004\\002/;\\027\\341\\271\\222\\306\\317\\234M'\\327\\302- \\322\\377\\275*\\247\\277rn\\037[\\023\\007\\345s\\237\\017\\250M\\237\\245\\356\\237\\026\\330\\002!3O\\220b\\225w%o\\233\\320\\250\\306\\320\\370YO:[\\313\\373\\232\\004"\\033\\254\\337\\006\\244\\275\\215\\034\\2759\\351\\334\\373\\257\\323\\326\\262\\376\\323F\\272\\240\\004\\005\\357\\315\\304\\274/I\\250\\243\\005\\362\\014ww\\015,i\\246\\325P\\357`\\346+\\032\\355\\016\\333U\\005\\306\\360|\\354\\206b\\303k\\033\\025\\223\\220\\262\\240^\\373tx8VP L\\267\\211d_\\214\\215\\201\\025p\\273\\374\\226E\\030:\\012A\\350\\347\\310\\006\\356\\255\\325\\343\\020\\032\\236\\240H\\277\\214\\341\\325\\345\\3362\\317:rr;\\370\\023\\032\\213\\247\\350\\215\\243\\362\\032ES\\3233\\342S\\327y\\262hJ\\333\\327@\\211\\317\\230\\256\\237\\273\\010c\\246\\017Y\\262\\256!isp6Z<\\374g\\337GQ\\315I\\224\\333\\007\\015u\\001\\373\\333u\\017\\264\\366\\000\\222j\\337\\022\\3701\\377\\003\\212\\240 \\266\\223U6\\206}\\371R'\\202\\232\\346\\361\\216\\314-\\233\\006\\016\\033\\222\\250\\356\\212\\225-_;\\026\\3650\\000~<iP\\273}qH\\362\\030\\223*\\276>\\336\\350\\025\\311\\366vO\\264/3\\323\\031v\\3475\\254\\203v\\364\\371|\\246\\3060Hk\\213\\226\\024\\2776+;\\325O\\333h\\243\\314\\235\\377\\264]F\\261:x\\304\\230=\\273i\\220f\\316N$\\357\\315\\371Ns\\327 \\012+\\223\\2206\\252Y2\\331\\322\\374\\306x\\021\\025A\\301t\\260]8!\\230\\307!Vnk\\264\\305\\215\\340\\361\\214\\177\\221;\\373H8\\366r\\220\\304\\244@^\\003\\005&\\323\\244S\\346Vs*\\354\\363|(\\264U\\320&\\177\\363^\\012\\336\\222b\\2459D\\3311\\006\\325\\244}\\223\\017\\356+M\\373\\311\\337\\363\\262\\256~n\\317TM]\\315\\372r2\\361lX3\\266# \\021\\357\\221/:\\213Y\\310J\\001HVe\\344\\012>I\\246\\323\\202\\330\\351\\225v\\312Q(2\\350\\241\\262\\356\\016\\366\\265Fp\\375\\310Y\\252\\010\\372\\313\\312\\340\\253\\257\\037\\212}\\200\\027l\\311\\372\\213\\336i\\030\\360\\362et\\362\\221\\301\\377\\002\\214\\253.:1\\252\\335\\205)\\250\\030uID,\\343\\215\\211\\252\\330\\333\\325\\363r\\365>d\\356\\035Y\\030q\\250\\325\\261\\016\\334\\276i\\316"\\265\\256\\307(\\372\\257K\\276{h\\035\\2670\\362O\\261\\027\\365\\342\\037K\\022\\375\\370MC\\313KDMC}6!5\\252\\371yO\\232\\014G\\340/[\\211\\333\\246A"\\333F\\230\\270^\\306K\\000.\\026\\237\\344\\364T}\\343\\011\\325)\\223\\303U\\265\\367\\027\\005~m\\342\\302{k\\0225j\\277\\316\\276h\\001&\\033\\\\#:\\330*\\317\\225\\022\\234\\270\\374\\346\\032\\022\\340\\377\\337\\002\\260\\250\\317\\304\\215&2\\032t\\240\\026C%c\\000\\362C\\027&\\\\\\332*@\\250\\254\\267\\031\\0221\\327\\350\\201\\361C\\005\\272$J\\034\\021i\\327\\025\\337$\\0021\\376\\340tm88\\230\\253\\334\\347\\004 U\\305A\\311\\213]\\300Ci\\364\\376yJU&\\266\\272;\\240\\237\\256"D\\3210\\036}zrjI^\\302\\353\\312l\\300\\330\\341\\020?\\314\\316\\301\\267\\215\\204\\013b\\230\\001\\276'N\\334\\261\\235\\010\\207\\227\\360\\000L\\337\\245\\314\\032\\375\\327\\3055\\0045z\\012\\340q2\\332o\\260^z\\021\\320\\341\\324u:\\016Jb\\254\\372\\353@3^\\200\\004\\007\\015\\036\\005\\032\\345\\233\\247y\\312\\336\\352\\212\\373\\255R\\315\\266\\272\\326H\\355\\2437m\\021\\324\\236\\326|a\\373\\363\\200\\000jX\\2748\\311h\\202 \\214\\2356\\016\\220\\373V\\234K\\360t\\005\\337l:\\034.*\\331\\035iZM[\\340j\\373`\\234\\317\\362EI\\320b\\225\\301\\371m\\363_W\\243p\\255\\321\\276E\\017\\222\\362.\\363\\016!=3\\305%\\315\\031\\206n\\212\\370_a\\010\\304\\230\\201\\331H\\356\\336,\\224\\232\\316\\002Q\\3405+q^U\\362\\204\\332\\213N\\332t\\311C\\210\\267\\352}\\227x\\001)\\304C\\355\\317{\\267#Q\\263D\\374A+\\024C\\272\\250\\335\\2627*\\243d\\213\\260\\331\\334\\007]\\011Q\\365\\030\\264m\\334\\015\\314\\313\\366\\343\\210\\011\\010mH0\\237\\342v\\024%\\343\\364\\205Q\\243\\007\\365~\\320?*\\206\\222\\337\\302\\231\\003\\225\\244\\326\\200\\020\\212\\005o\\337\\202\\015eMu\\177e:\\223\\031\\243\\010\\247J]o?J\\230\\313\\377\\251\\336\\223\\020\\233\\235\\023\\\\;f7\\322\\212\\264\\263\\360"kQ,\\335\\214#\\320NR<\\3706\\2314\\2442PK\\330\\274\\330\\003';\\266\\262c\\315?\\361p\\231](\\036\\250Jx\\026\\323\\212\\002\\030\\316\\231\\236Gq|\\203\\336!l\\236i5\\352\\036\\242UU\\346)\\361\\236\\233\\217DE\\0223I2d!\\347\\025Y\\025:\\252\\177\\312i\\024\\320\\3617O\\237\\360\\012\\250N\\000)~\\237%\\3040\\313\\203\\012\\032@\\233\\2019G\\274\\265\\3742\\234\\036y\\026\\010\\242{\\273\\325\\017\\3363\\372\\272\\001\\020\\022\\335\\014\\337\\242z\\337#\\324\\210\\232\\020\\370\\270\\277j\\367y\\200V\\253~\\277\\210\\302\\314\\011;\\244IB\\2139\\022\\220\\177\\376\\211H\\360\\222T\\021\\201\\314\\011\\346;g\\025\\2073\\227\\330\\235\\200}$\\376\\332`\\342n\\312\\222\\207\\266\\322I\\307\\212Y\\320\\037\\252!an\\332?\\235\\375\\360\\274\\227\\333&c\\010M\\374*\\225\\027\\3275\\202\\215\\341\\246k\\246%\\004=,\\037t\\352\\261,\\336XFt\\227>\\370\\310\\350S?\\350O[\\354\\027X\\001\\301\\203GB\\303\\233E\\177\\250\\303\\031-/(\\267\\325\\313\\253~\\226v\\031\\374,f\\2350i:#\\275\\243\\034\\325\\034l<E\\017\\033\\223\\365N9\\276\\\\\\306T\\005\\001d\\326\\012=\\027N\\367UdY\\320\\273\\377ji\\327t\\345(\\036R\\030\\353|U4\\325\\301|\\316\\330>\\030vO\\342AB\\343Di\\001\\375)"\\222\\274Y\\251\\012N:\\2331\\355tIs\\222\\216e\\273m\\205\\336-(\\234\\200\\034\\013\\274N(\\271)\\011\\276D\\231\\375\\231*\\332\\242\\212HnR\\262\\332\\003\\021\\023auw\\271ai7\\206\\030\\\\\\276\\335\\235\\006g\\340\\032\\366q\\341\\315+\\336\\202\\267\\013\\321g\\223\\223\\343q\\340\\345\\353\\350\\233\\351\\356{h\\005\\373\\345y'\\013<W'\\020\\030Ie\\361\\275*7\\270\\333\\177\\306X\\334\\246z\\027\\034@\\245\\261\\342m\\247\\354.\\366\\272\\313\\214\\365\\030\\327cX\\304\\2459\\231\\354\\350\\322\\016e\\266!\\342e\\311\\300\\021\\315\\321\\270\\376\\005\\327\\272\\012v\\373\\213\\271\\035N\\255\\204\\323$\\240y\\370b\\315/\\036\\256|#bC\\002\\341\\270\\205\\324\\304aQ\\\\\\036S\\357c\\254d@\\372>\\352VJY\\220Uet\\202\\317\\362\\264 \\206\\211\\227\\254T\\276\\024\\325-\\233\\257\\220\\011\\353\\351\\021\\367\\263\\260\\265c\\257\\211\\013\\\\X\\227\\277P\\314j\\367![\\367A\\257h\\351\\014\\376\\373\\337\\241\\005f\\275\\304\\272\\231d\\222"Nd\\364?\\334.\\344n6\\373\\246\\026\\274\\364<JJ@\\346&@\\017uA\\015\\274\\220\\357\\026\\364\\323\\303\\376\\0234\\265"\\234\\201\\3573\\030*R\\315\\017\\027\\206\\227\\241\\377\\000\\011\\015#\\034\\233r\\030\\266\\365<\\205\\331\\316\\213\\201\\235Ul\\221\\351}\\032\\202\\020c\\247\\262L\\343\\232\\242*\\347\\246\\265o\\245\\354\\3632\\232\\346\\340\\032\\330,\\210<\\256\\260c\\272\\023\\303P\\277#\\343rHS4\\271\\356\\345\\031\\252\\327,\\242h\\303\\236\\373\\321\\341*\\320\\245"\\323\\200c^R\\264\\214_`e\\333D\\031cJMJG\\370=\\224\\303\\262#\\353G4\\327\\244q2F\\314\\221Y!u\\251\\012\\333q\\361\\311x\\347,xM\\026\\021YrR@\\224\\217`}\\300wT\\001\\306\\223\\230\\212\\321(\\336k\\374\\220:\\277F`\\331N\\374\\305\\320\\004d\\317h\\361\\223\\204K\\216\\231=;bq\\225\\270Ct\\3048\\315[P=\\001K\\002\\366\\014\\376\\206\\037c1\\371\\330\\375o\\361J\\306,\\271\\260\\254\\233\\\\\\221\\273\\315i\\274\\353\\030\\367b\\250\\370\\234\\033\\263\\274\\031\\311]%\\307\\036\\203V\\274\\316,\\302\\376\\344(\\025'z\\273M\\334\\224\\227\\256\\210\\247D9v=\\022\\265\\020t\\030\\003\\366\\374-\\010\\334pEFSH\\352m\\323-\\033\\225v\\332O\\222\\267\\204\\212M\\303\\314\\265\\231,\\203\\004\\236\\377k\\302\\015\\311\\017m\\006\\304\\365\\336\\327Pk\\004.\\221h\\010\\310n\\036\\261)_\\005a\\305a\\240\\307\\302\\311D\\333\\213\\333Y\\305lU\\033a\\313\\263\\341M\\241>\\325r\\355\\336\\031\\177K\\023z\\372\\371\\223\\325{E\\264\\023O37gg\\035\\313s\\342c\\014\\034\\255\\351\\244Y\\225\\007\\237\\205\\257\\360\\2266\\367\\267z\\373\\003\\017\\347\\011\\252\\355\\312\\346\\273=\\216[\\356\\370\\321T\\215\\205ju\\347\\247\\213\\252\\002\\016\\361n\\241%\\265\\323\\274\\357\\025A\\362\\342\\246\\323\\2765\\354\\177\\341\\235\\302=\\017*\\320\\214\\000Q\\017\\261f\\032\\020\\\\\\236\\215\\345\\312\\257\\261\\320K\\351\\220:l\\214\\336\\360\\023\\347\\372\\311|\\316\\255\\213\\007c\\020\\347\\2720]\\026=\\003\\255\\352\\320o\\356\\350\\331\\301\\013j\\334\\305t\\252\\215\\243\\216\\365\\343Y\\345\\234\\270\\317\\323\\316-\\371\\207\\235|\\035\\012\\340\\332Q\\350\\251\\260p\\010cn\\314|\\210\\274\\355\\272\\221\\245\\005\\011{\\314\\321\\003\\326\\335,s\\257\\011\\377\\270Qi\\323\\372\\334\\021<\\226f\\265\\020ZP\\226\\211E\\341\\330\\256\\006\\245K2\\342\\245\\225X\\316'\\232\\353\\354\\262\\212\\217k&\\375_{J7pg\\271\\030\\236\\313\\267d\\255\\243\\231{\\266\\266\\235\\303G\\036\\253\\020\\377\\016\\251w\\006:~\\2245\\001fB\\020\\001\\363\\02258\\037sQ\\204(\\0304n'\\2031lh\\372]\\033>Y\\226P\\025\\021j\\245\\203\\236\\377w\\027\\000I\\314c:u\\322\\3024\\002\\201\\322\\266\\260\\226#\\300h\\273\\200\\237nP\\015"\\267*\\276\\351\\256\\376N\\322\\243\\236\\340\\233's3\\224\\307\\260oga\\311\\375\\344r\\240\\001\\262|\\360\\246czd\\317\\2537\\021\\001S8\\346\\262`c\\322\\2431\\361j\\226\\271\\376\\334\\367\\271\\276\\024\\311\\014\\3040`\\232\\227i \\326Ct\\302\\202\\210\\3157\\004$\\037\\377`\\341\\334\\006\\035\\367\\272\\027g \\251\\035\\370e}\\370Dm\\324\\362\\357\\371\\203\\016_\\231\\322\\305` \\317&{ ]\\201\\330\\261jn\\375\\261\\217\\360\\242t\\227\\355\\377a\\324\\\\\\352tq\\021H\\317\\373L\\232\\234\\376\\016\\217X\\012\\334~~\\033\\000\\353\\002\\254\\317V\\244\\024\\326HkG\\372!\\030\\307\\024\\005\\270\\2713\\357\\311\\306\\036\\210\\203\\031\\003b\\316\\030\\306\\321"\\021\\352\\001\\244\\364\\036\\367\\352\\206W\\000x\\012\\223.\\317\\231\\3461\\030\\242\\363Eh\\234\\207{Y4PM;&\\244\\234\\343*\\007\\312&S\\355\\252\\226\\225\\033Z'\\205\\345\\372\\315\\223Xs\\000\\2115'ql\\010?c=\\022"\\340\\275\\353\\035\\361\\273\\333\\016>J\\177\\355O\\370\\342JJ\\3024\\271\\234[\\251\\313\\335\\016\\302\\374\\272k\\027\\012\\240\\242k\\334\\233\\322q/\\013HT9\\324\\021Q\\264\\317\\354\\270\\351\\246\\347%\\330\\304\\375\\236\\357\\262\\177w)v\\317\\230\\236hGNf\\366PG\\245L5/\\2514\\210\\354\\324\\3269x\\231\\215\\327\\354\\354\\007\\343\\370\\374\\222\\253\\276\\265\\264\\344\\317\\336t\\0067!\\371q-\\026\\24524\\310|\\225\\2719o8\\346#;\\325\\376X\\020\\314SZ\\207\\213\\232\\227]\\242\\217\\317\\356\\326\\277q\\200\\037Q\\267\\032\\375-\\224\\217&\\257/\\274wB\\014\\212\\253\\014b\\325+`\\203+\\336\\343S,\\266\\357\\372\\245\\265(0~\\231\\216\\370\\207\\005\\037\\024\\017iJ\\341\\220h\\013\\332\\353\\217\\004\\020\\177w\\241\\365\\023\\230\\267h\\236!\\253\\322\\353`\\260c\\370\\325\\271\\243\\245\\212\\004x3\\334\\200Ky=;\\001\\022\\313\\317\\2115f\\235]K5j\\305\\334\\266O!\\302\\254\\210\\220s\\312;F{\\355\\274\\344\\031M\\337 \\340(\\317G\\003\\312\\332s\\0150\\304g\\271\\306(j\\331p\\313/d\\217\\035\\314O*\\014\\252\\320\\016\\360\\220x\\216\\355>c\\355o\\214\\003qOxW\\244P\\316\\011\\273\\324\\363+\\305\\317\\215)\\373\\000c\\000\\323&&\\006\\321\\256\\227\\373Z\\0276N\\303\\300U(\\030\\260\\300\\004X[K\\364\\300\\223\\203\\013\\253^\\200f\\224\\210\\205\\314M`\\324\\335|\\036E&l2\\220\\325a\\013i\\370J\\255\\205X%\\223\\2607\\037\\316e\\016\\243\\362\\357\\246\\032\\0367\\257\\235\\020\\340\\023\\007\\301\\370\\236\\014\\001\\357\\332\\362\\200g\\302^b\\350n\\370>\\257\\375\\321@f\\361\\274T\\230\\015\\026\\356T\\371\\265\\276*\\347l:Do\\362\\305\\260k\\255\\200\\014\\023c\\341E7\\343X\\314\\303\\361E\\247\\032\\320]\\203\\263\\372\\272lB\\260-\\201r\\334\\372u\\\\\\354\\315\\320\\306F7}.\\260^\\200^\\333\\274\\227\\314\\022\\315\\201\\020\\273?\\202\\002\\227\\014Hi\\020\\305\\007\\351\\333\\323Vp\\204\\247\\310Z\\366P\\362H\\002^W\\217\\317t\\233#\\220\\010\\221\\010j\\272\\035\\300$_\\001\\274\\345\\335g\\343\\301Ag;s\\332t\\275\\374\\326\\331\\317\\337\\222\\207[iW\\312\\267\\232Q\\364"\\3717\\330\\330\\012:\\034\\201\\022\\256@lw@G\\022J\\373\\217\\305\\300J\\006\\210 Y\\227\\345\\000\\202E;o\\013\\036NN6\\363&\\263\\327\\323\\257\\352\\346g\\375.\\354\\305\\313\\200\\310\\233\\325\\270\\004\\254n\\217Khf'\\251\\352s\\364\\344VC\\3533Je\\342\\267\\3721\\200\\243\\035\\370\\254!\\244^'\\005F\\232\\034\\203\\324\\347\\214\\004\\244L~\\263N\\252\\320\\270Zn\\342L\\032\\335k\\301\\240\\360\\367\\331\\231#\\211\\026;l\\200m|\\316l\\020\\263r 3\\271\\036{\\262FY5gY*\\241Ta_Q0\\035-Q$jp~1\\003\\033Z'n\\212\\351\\011\\006\\213\\207Nrf\\215\\036t\\034\\240\\255\\204\\244\\305\\323w\\245\\001\\210w *\\362@\\226E+\\013G\\266S;]\\271\\332#\\266\\341\\230\\253f-\\031\\032\\317\\323h\\372\\000\\021x\\016wR \\304\\226\\275|\\353j\\252\\306\\246ciZ\\021\\317\\232\\334\\013\\024\\235\\324\\025\\224\\270[\\265\\244\\006\\034`x\\220\\004MX\\322\\242\\251=\\202<\\360:\\020\\267\\265\\355\\001\\011ax\\312\\351\\304\\373\\247\\353\\352Y"\\235\\317\\374!.<<\\206\\355\\005W\\333)\\315%\\254]\\344\\326\\365\\023\\376U,!!Pc\\305\\352\\\\A\\330|\\177*\\236uu[(-\\312\\260\\244\\265\\245\\273M\\032\\037\\376t\\000\\303Y_o\\320\\245>\\273\\253,Dh|b\\235\\003\\004\\271\\200\\015_\\365\\336AM\\330E\\244\\\\?\\215O3U#\\271\\367\\026#\\273S\\252p%\\361)\\336\\244\\375\\314\\\\\\011\\340Q0\\347\\2307\\2116I\\264\\3429\\254,\\343\\301\\370\\267y\\2067\\322\\203\\374\\331\\024\\266\\256&\\007p\\346\\262\\303\\274\\230D\\306JZ\\217\\334L\\216\\034@\\255\\2217\\347D\\343e_"]\\022\\267\\345\\341\\2718\\001(\\346/\\373\\257\\215\\017}\\322UP\\215\\222!\\263\\3037,\\252\\005\\275f\\\\\\306Z\\303\\307y\\315\\030\\2779\\004w;\\341\\224\\322\\023"\\262\\2135\\207\\362\\027_\\305\\353\\3267\\235ddX\\375\\261+\\275\\244\\302\\230\\361\\023\\340\\255g\\3620\\252\\232\\243\\033\\254\\203#+<\\314\\253\\037y\\327\\333@\\025\\253\\037V\\377\\312\\250\\023\\273\\337\\303\\266?&\\225\\237\\375_\\247\\236\\260J\\353K\\251M\\252\\357\\367\\005\\205\\347\\3511\\201\\203\\266\\336\\001|\\367\\000\\034:)\\310\\0268\\246\\351\\255i5\\275$%\\376\\312H\\251_\\267\\264`9\\350\\352\\260,:\\266=\\375S\\357/\\313\\200\\367\\341\\356\\267\\232B\\312\\225$\\354P+\\344IP\\304\\214\\326rk\\314\\343\\245\\246\\373\\346\\260J\\306\\342\\216\\013b\\323\\212\\256\\331uP\\260\\251;\\177\\320\\0274\\225?\\370\\022\\256\\205\\367\\375 \\211cQ%$'\\325\\347\\325\\370\\330\\255\\217{\\344\\277\\231yq\\365J\\3609H=\\300S\\206\\374\\230\\341\\327\\026\\323)D\\277f~\\305\\367\\0307\\231'\\303\\305\\323\\234\\200v\\001\\303\\034\\371\\340B\\303\\255y\\326B8\\366\\220\\222~\\326\\261\\204\\3622\\217DbnDN\\312\\276\\307\\344$\\3026b4\\216k\\305\\342\\222\\337\\014\\241Jj\\230\\240\\366\\222\\205\\227\\203\\016\\247\\022\\270\\034`]\\230\\250\\317\\262\\233\\314A\\023\\327\\3041\\2763\\204S\\346e\\225\\3624h\\022*u@\\022\\246\\343-#-c\\223&]2\\346\\233CS\\326\\241jjj\\005\\217\\252-\\003\\032\\235\\336x\\312\\274\\320\\020\\246\\202\\351\\355\\307\\332\\200\\321\\216\\317\\357\\322\\213\\021\\266\\200X\\355\\351\\253?\\312*\\023\\240~\\271\\306*S\\206\\010\\257 \\3725\\266\\017i\\316\\253SY\\217\\370\\255\\273\\331\\370\\316\\032\\342l\\202<\\204\\342\\300\\006-k\\017\\234\\251\\277\\313\\300\\274\\212W\\310\\200u\\274\\214F\\227\\313\\352\\2048\\262\\303!\\363\\000\\004\\235\\370\\2346\\030\\023\\212\\336\\370\\330yd\\224\\024\\226\\341,\\202\\005\\213\\366\\226\\376d\\351\\302\\010\\310\\216\\253\\224\\224\\265\\237\\034h i$f~\\322\\033\\360\\252P`\\036\\027"\\250Gr\\375<#\\217\\366\\314<\\344\\262\\363\\300\\336\\253\\374s\\354f\\202/\\272\\026#U\\363\\315\\234\\315\\323\\364\\322h\\023\\313#O^?\\370T(5u\\325J\\334\\250\\330\\213\\265\\3235\\010\\227\\343\\243C\\031(}\\360\\007\\332\\307\\022\\374)\\353\\367G~\\031t\\036\\343\\271\\336\\355\\330\\305\\254\\036\\363\\\\\\222\\017>\\016\\010q\\272c\\301N\\277=\\372M\\362\\225:\\310\\005I\\275\\275Cb\\240\\017\\371\\205\\303I\\347n`O\\225<ch\\277\\372\\030\\246\\032,9\\271p\\236\\214\\236\\265\\003\\213\\224[\\323_\\024\\304\\3568\\005\\336x-\\222\\275\\323;b\\244gg|~Q\\272\\025M{'l\\316\\206\\241\\000\\013>f3\\2045\\003\\265'\\322\\022\\245\\026\\234\\364)\\320O\\210r\\275\\3054\\276k\\246\\335\\272\\201(\\216\\235I\\346\\331rEd\\361G(*Z\\374\\215\\015T\\277\\031\\250\\215\\327\\246\\014\\366\\323\\371\\3524.\\246\\304n[\\232?\\272|\\204\\267\\325\\237\\325\\205F\\347\\275(0:Aj\\012\\261XG\\212\\231N\\037\\313\\024T\\022\\213\\024Ojd&a%\\314%S[%\\257Rb\\277\\3773\\275\\351\\261\\266l\\376l`\\276LM\\257\\274\\370\\024\\376\\235\\036\\272[[\\266l_8\\015\\350\\363@J9:\\333\\240i\\262X\\222\\031\\221\\323a\\353\\362\\372\\354=\\0051y\\241t\\217\\236\\230\\304\\201\\350\\370*\\310\\300)L\\375\\3631\\303\\245\\277_\\301\\263\\365\\327^\\015\\230\\301;\\313]9\\262\\272\\036\\255 \\206\\317\\037\\262=e\\373\\227\\003|\\364|\\017\\272\\257\\003u\\232%\\024'\\256D\\323\\212d\\265\\025x\\235\\240R\\007\\237\\001`-\\013\\264m\\000\\220\\200\\235W7\\270\\355\\364W\\337\\243giSG\\221wC\\212\\311\\235m7\\306\\266\\250\\010\\234\\270[\\202W\\266UN|\\201F\\337\\305\\256\\323q\\330\\263\\250\\300z\\255\\030\\2565@TQ@\\355@\\270T\\205\\354s\\367\\341\\274\\272 \\363\\261\\\\J\\346\\026x\\304u^\\361\\215\\257v=\\343\\343\\323)\\361\\314\\216\\270[A\\205\\224y\\204\\210\\035\\241\\246 \\366\\277\\312\\255N\\261\\215\\200+k\\225y\\205B@\\001\\374b\\313\\021\\241\\274:;\\304\\255\\306\\304\\213\\024\\023?\\277\\221o#\\325\\246\\250\\267D\\365\\335\\324\\325\\240\\007\\241i5\\021\\214\\232$y>\\212&\\204v\\373\\233gY\\304\\016.`]\\261\\275\\246\\264\\266\\255\\261\\320\\361f$\\020E\\021\\311|o`\\337DL*\\322%8\\247\\330\\033\\034\\001\\357\\244Ba\\305\\251\\317u\\013\\222c\\304\\200;e@\\251\\336\\272\\253\\010k\\011\\241`\\211\\273#\\316\\233:\\204\\033K\\004\\016\\315HS=\\225Y\\245\\005\\0318\\324"\\371j\\231\\342\\276\\272b5\\262\\351T\\316\\342kG\\231X\\263\\252\\353#\\376P\\310\\026}X\\013\\2456\\025O\\212\\3202\\323\\360W<(\\331\\275\\263\\211 ,\\\\)\\374\\2341\\363*F\\000\\361D\\030\\272a\\246\\345\\225\\004\\356\\301|\\260\\275y\\346\\\\\\377\\204I\\244*P\\177\\252\\237\\254\\276\\371\\267wO\\234%)W\\351*\\001\\326\\313\\302\\331\\250\\317\\2155\\276|%\\332+\\350\\320\\220xF\\023\\231\\323\\377\\332AW\\207\\225\\255\\214\\244\\312\\233\\217\\333\\267\\323E\\257\\315Sd\\021CZ\\011\\243lP\\016\\362\\361t\\375\\306\\034\\314<j"N\\333\\022!\\226;CuL\\266aa\\320\\014xt_\\214\\037W\\225\\311\\214}\\3744\\035\\301\\361C\\264\\037\\326\\256\\006\\317\\241\\340\\266:\\247\\201-A\\310\\222,\\031\\376\\014\\313`\\272\\234A\\361I\\244e\\024o4\\311B\\025U\\233\\237\\177\\250\\371@}l\\022\\276&\\264\\036m\\356CK\\310\\331\\327\\241g\\310\\247\\240\\026N{\\261\\231_La\\001\\355\\2441\\245\\015\\255\\334-\\007\\365D\\036\\365\\301\\206\\300\\263\\346R\\300\\021\\2431\\006MA\\311\\360u\\315\\013e\\270E\\337\\003\\201\\207\\016."\\246\\211\\235\\006"\\215\\224\\37114\\237\\344 \\012\\321\\327\\206\\376o\\244GCW\\321h\\026\\213\\350\\366^\\002\\003\\223\\023\\177\\264\\036Z\\002\\214\\216\\350%\\355Ou\\272\\016\\314\\013\\220\\024\\246g?\\303\\203\\326A{\\266\\177\\\\E\\017\\370f\\201\\237{\\257H\\250\\305^\\366\\376o#\\304\\350\\007T\\002\\026X\\255\\212\\272\\251\\252]\\031R0\\243\\345@\\215\\250\\305\\264D\\236P\\231;\\021\\324\\2014\\012\\247\\000A\\316\\2039Y\\001eI\\236\\224AT^W,\\033\\221\\361\\011\\344ew\\020h\\034\\313\\353\\335\\255\\317\\030o\\003\\323\\227\\263X\\026m\\213\\264\\203wd\\261\\376dC\\321\\343\\205\\216\\205\\253\\326\\365\\037\\0258\\345AL\\326\\343R\\366\\325ZZ;=\\244\\341\\317\\2366\\373\\271g\\343\\341\\000G\\017o=\\370\\304\\324\\257\\277&\\306I\\336z\\266B\\206\\273\\177\\265\\014>q1[\\264E\\240\\334\\343\\371\\357\\030\\352\\267\\225\\0141ht\\024\\376\\372q\\216\\3413\\232\\2578L\\003(\\214w\\312\\246\\366\\350\\323\\201&\\275~\\013\\251\\215\\357\\337XC\\231\\371\\3350{\\253\\340\\247W\\312EHZ\\000\\025\\3441yX\\307\\035\\371\\365\\203\\340f\\313\\277\\237\\265\\027j\\333\\3051\\007\\231i\\2577\\205\\021\\037V\\253h\\263\\022\\220\\264\\323\\240\\214\\366\\212\\005\\316\\246\\037\\032\\013'\\331\\004\\177y\\210\\032\\350]\\036\\332\\217\\235\\252ic\\337\\347\\200i`\\366\\374\\331m\\010\\365>hT\\033\\373^\\204\\346?\\22488\\016D\\016yN\\351/\\261\\252\\005\\231\\2350\\027\\257~E\\351\\032\\370;\\232\\224i\\247\\025\\001E\\371U\\321\\015f\\312RX\\002Q\\034\\\\\\021\\256kF\\000n\\016\\273\\316r\\332\\322),\\023\\017/w\\226\\244\\374\\352~d'\\245\\344\\024i\\225\\212\\274\\271\\004\\255\\015\\214P\\314\\366\\324\\363\\200\\306\\230Y\\252\\036\\334\\323M\\352\\177D]N9\\340MN3\\005\\031\\302wu\\330\\212\\237\\357O=\\213F\\2155\\334H\\213\\241\\370\\213E\\3121\\360\\226\\344\\371\\007Q\\244\\345DL\\003\\011\\\\#\\254\\025\\025 k\\011x}J7\\277\\226\\330\\234\\005f\\225\\227\\217\\211\\017\\216\\030\\03092\\303\\341\\274^\\314\\234q\\204:\\201V\\326\\262\\2272\\372\\314\\304\\037 g\\2133\\326\\0145Y\\322(\\324\\272%{\\325\\306<3\\271X@\\353\\214\\260\\277\\242Ca*\\201\\372\\363\\340\\005\\233\\212\\305\\004A\\347\\323\\327\\25154HE\\251\\256\\206\\366/\\360\\037\\332\\002\\231\\365'\\023\\324\\035\\211\\032\\230\\002\\321\\3529\\326&a\\363\\334\\021\\006\\023\\264$\\340[Y\\223\\030?\\352%\\311h"$\\274\\354\\004h^\\267vy\\274\\033[\\341\\313\\303w.\\253\\213\\223&>\\025\\334\\223\\307\\347\\327\\324\\224\\\\\\207\\261"\\000s\\321 \\031\\324f\\326\\321e5J\\007\\012\\360Ua+\\036\\012\\266\\211\\230\\202\\333\\024\\273\\254\\377\\031\\352a\\201\\355\\336a\\247\\245\\210~4|\\014\\237H\\321\\267\\314=\\315n}\\335\\026r\\031\\343\\004~\\255u&\\316L\\361\\313\\251k\\234\\214:`;\\361\\2236tL\\201)\\261\\242A\\260\\353U8 X\\220\\315@\\365mci\\342\\013\\257\\356:\\003\\277\\341M\\016L\\347\\333O\\212h\\377n\\372x\\245\\377\\235\\301b\\327\\205\\265]\\212\\275cO\\353\\260>n\\276\\007\\230\\361\\237\\224y\\233\\325\\025\\000g\\334\\262\\270p\\342\\205d\\\\\\235\\251\\234w\\254\\267h\\2657\\016\\204\\264[\\360\\331\\256\\256Q|\\321\\200\\363Np\\333\\031bA[\\365v\\330\\363\\212\\304_G\\014D\\237\\224\\210\\362\\020\\202L\\262\\006\\363\\022Q\\260\\244\\310\\310z\\010\\252\\020k!\\030\\362L\\266\\012\\356\\373\\376\\303\\342e\\035TI\\005\\233=\\311D\\350N\\031\\353<\\352\\343Z\\326>\\007\\205u\\001a\\310$\\324\\242:^V\\230\\253\\207\\346\\301}\\024\\2556\\263Mt\\245\\371\\343W\\245G\\312\\202\\210mK\\003\\342\\\\^\\302\\260\\026\\201\\376\\344\\232\\217\\001\\310r\\246kNzn\\001\\242n)\\234\\034\\357`o?\\267.>\\316M\\205k[\\037\\234\\333cK\\274@\\226\\215\\343x\\304\\275\\336G\\347[x\\302\\202bUw\\266\\236\\246\\251*\\324\\010\\300w\\015NI\\213H\\034\\375\\331V\\352YQ\\343\\214,q\\222\\343\\014\\216-\\261\\025^\\324fr\\025\\270\\272?\\3139L\\301\\2129s\\200\\206\\324\\253\\355\\231cb$\\354\\313\\220[\\257.K\\007\\334tI\\036\\364\\334\\240$l\\203\\331\\201\\202Jh[\\034\\000\\231<\\272\\205c2\\364\\010\\025E\\217\\375%\\362\\221\\177\\335\\014\\256f\\204$2S\\\\Y~\\2048\\300k\\275\\204\\215M\\346\\305\\207\\346\\225\\272N\\003\\276z`6\\204f\\244\\203\\332GU\\234G\\313\\006\\215\\365\\221O\\013\\230\\272\\341\\273\\364\\330%\\3438*\\260\\367\\007\\204:\\275\\241\\263}\\337\\330W\\272F\\326G\\254\\232\\376\\014e\\243\\263b\\262\\272\\304\\363\\314*w\\270\\376\\311W\\027\\007\\353P\\326+\\274a\\355\\321VYj\\217\\312\\213\\007\\372\\206==\\264\\3635Bp\\244U\\3018\\003\\240~\\003V\\326\\355S\\211\\257\\276\\226ev\\331u\\007\\344\\011\\005\\216\\240V\\032\\203R\\307\\006\\263;\\346\\370\\3050v\\342\\302\\303\\356\\274\\311\\374\\302\\366\\277\\247\\330#AW f\\377\\357[7\\323\\332\\305W\\332jq\\346\\0056`\\311\\204\\275\\347\\216\\015\\314\\336@\\256(\\237\\232\\370\\222r`\\305\\3220\\241\\322J\\342Ue\\031\\271\\375f\\260\\366\\247\\366\\365Tu\\272\\325;\\312\\271\\235{;\\225\\223`\\317\\223\\177\\255\\262\\272\\263\\261aP\\245\\323h8:l=\\316\\330\\316h\\264\\366\\305\\351\\000\\244"\\357%\\035v[Hq\\335\\260\\354tg\\311\\377\\315O \\034\\374\\212\\257\\353J\\3641\\022\\305\\026\\033\\336\\350\\032\\351\\011\\327\\232\\264\\347\\301[\\347\\327\\215\\207\\272\\324\\376x3\\354\\357\\304\\367c\\031\\271\\330\\273K\\326\\326\\351\\244\\255F\\245D\\274S\\257w\\216\\030\\002\\357\\243\\372\\346\\3669\\372\\014\\344\\302*\\265\\214a\\372\\232\\222}>\\323T\\376g4P\\215\\317ce\\302Ap?\\212\\300\\353\\014\\261\\274\\275\\2200\\016E%\\363\\343\\305i?c\\006~\\013\\242\\325\\023|\\251L\\362\\252\\036\\316\\2553\\265>\\225jb\\330\\345\\333\\022W^\\336\\334\\316*\\020\\332\\335\\374\\015H\\347\\330ty\\030A\\334q\\010\\225\\244q\\203\\302g\\333\\012\\204\\2635\\225Fo$\\255i\\251.O]]\\360\\347\\324\\223\\257J+\\211H\\336\\213\\372\\207\\237\\003\\344\\205\\236\\352\\265h+ro+\\370/Y:j\\225\\002\\246\\315\\347\\213t<\\251Ej\\0212\\336\\366M\\363\\273\\321\\012sx\\211@#\\321\\315\\0177\\\\\\317\\301\\362;B\\012\\275\\333y]O\\243_\\204\\363y\\322U\\227;C\\360o.\\037\\337\\213\\327\\261\\266\\227\\025h\\266\\022\\005\\372\\253S\\235rw\\313\\003\\003O\\257\\361\\331\\320\\366a\\274 ]\\245\\223\\311b\\336\\373\\036\\234\\230\\035\\2170T~O7 \\241m\\304\\345\\225\\272s\\366O`\\376z\\345H\\300\\277\\321\\253\\302E\\317\\235\\036|\\037\\010A\\3318\\010T\\376\\026*rS\\355\\342\\032\\002\\363\\362\\214\\364?U\\333\\2714\\247\\216\\275\\347\\256F\\235H\\371\\211\\305\\030\\307\\336\\214}\\345n\\241"\\203iBA\\365\\331\\026%\\361^\\245\\320\\262k\\262\\362\\025\\376\\334\\246\\3429\\034\\235\\234\\003\\331;\\333A9$\\015\\273 u\\013\\211~x\\304\\350J\\006.\\025\\0139(;O\\331\\363\\003\\366\\233\\017_\\007\\307\\340\\271\\274\\200\\240*c\\267&3\\255\\220U*.\\213\\011c\\264\\335\\351{\\375xJ\\313xAw09 ?\\313A@29O\\307=\\3171\\004C\\3645\\203\\3757Zs.\\344\\370\\2331~\\177\\220\\362M\\011K5\\363\\224\\215\\002\\014{"\\257\\000x\\252M\\033\\303\\230\\217\\270\\363\\331\\277\\241\\353\\322\\213\\230\\266\\337\\372\\225W$\\006\\217\\353D\\240Hc\\355\\017\\203,\\265r\\266\\3009.\\313kq\\256\\272\\264\\233D>2\\370\\314Fm\\031\\305\\217\\346\\263g\\177\\370&\\213\\010\\225<\\372GG\\262Y\\020\\007\\237\\260\\326w\\311\\331!t\\325\\234\\226\\261\\2366x\\376\\343\\212\\317\\275+E\\240\\274\\351.\\267\\215\\273`*\\307D\\331\\342\\256xO@\\246\\223Q\\236-$O\\337\\355\\250\\177wks\\253\\243\\261\\375{5\\355\\004\\030\\007\\335K\\313K\\305\\034\\3625\\2566h;t\\252\\036W\\302\\236\\325\\342+\\276\\240\\312\\3367+\\035)8\\015\\017ks\\177\\341\\221\\255a\\243\\274n\\237\\177\\326 \\3677Z\\\\|\\3733\\210\\250\\021\\262\\252\\210GM\\020/\\225\\361^\\326I\\026\\203\\035\\304}OQ"V\\347\\341\\245\\325d\\033\\017~[{\\256\\242@\\233\\233>C(HT\\277/\\027\\243\\336Z\\207\\272#\\3534@*\\001\\366\\303\\263[\\311D\\313\\360\\313*\\240c\\363j\\233\\031\\352\\005?\\335!'"\\037)\\020\\232\\273\\317\\365j\\265\\237/\\251\\204=\\250\\273\\365\\367\\217,\\177\\022<*\\212\\226\\221\\305\\315\\217\\331\\031\\335\\020obc\\251\\237\\357e\\002\\024\\012\\207\\034\\226\\246`7\\362fU\\024\\336\\012$,1\\030\\357\\257RC\\304\\026O\\352\\015\\307\\371pL\\210\\252U\\360\\214%#\\353V4$\\213sG\\274\\375\\015\\260\\244\\017\\204\\240\\025\\253c\\370\\210\\022\\31183\\247\\376\\235y\\272\\261x\\015oL\\014\\220\\366\\021\\234\\247\\036\\247\\352\\333\\021\\244U\\234B\\236\\231\\271>\\224\\357\\341x\\334\\332\\304\\311\\331P5\\355\\003\\031K:\\014:\\203\\243\\313\\212\\347Njeb\\324\\263_\\\\gh\\271::\\024R\\200\\032\\255\\311\\337\\305R\\012`L7{\\024\\372V\\367Y\\311P,\\014\\235\\3458\\242A\\307UT\\2341\\375\\033\\254\\\\\\352O\\306t\\302\\307\\372p\\031\\030p\\315\\323m\\030\\227\\205\\272[\\030\\346\\376\\023h\\004\\374\\023\\371\\207\\033\\225\\256n,/Y\\213\\014I%\\274\\350(B@\\205D\\375\\214a\\277D\\266Wj\\036\\232\\376\\250\\2578\\325X\\205\\2315r\\344\\276ru\\345\\246\\250\\307\\3377\\205\\012X4B\\002{\\214\\201\\001w\\335,\\314\\201`:6};W\\315M\\226\\223\\351\\210\\334\\327zNg`\\226A\\372\\352\\221\\330\\007\\250\\263\\243\\247\\344\\373to\\376\\362\\261q[\\033.\\032'8#\\315\\263\\357s\\032\\333\\243\\362jS(\\015\\261\\221\\307^\\366\\266\\345\\375A\\316g\\011\\273|+I\\253\\226\\257y\\346\\006G^\\245\\206\\304\\003^\\265\\373\\000\\261G\\325\\240%\\206%\\270\\206\\370Z\\222\\253\\243\\316\\246\\260\\010\\003\\032\\361\\012y\\231j\\235\\251\\206)bI\\272\\2344\\305R\\377\\007O\\004\\260\\257\\246\\356\\0005/\\312U\\331\\317\\277 \\325\\013\\323\\356\\015\\217\\306\\304\\357\\024\\365\\024\\326`\\255\\266\\227\\243\\243RQ\\203q\\035\\325\\250\\333\\012R\\024\\317\\004Z\\224\\375\\347\\336\\015\\000\\366\\303\\362o\\207\\363S]\\\\\\375u\\260E\\312s,Sw\\247\\361\\216\\324\\012\\340\\177&\\345r\\345\\\\\\033\\255\\247\\215lM\\2104C10\\006pY\\215@\\353\\017\\351pe\\266\\037#\\371\\012\\203\\327\\352\\251\\273\\210\\345\\310\\202\\200\\332\\367i\\352\\275\\204u+(\\250\\027\\234w>\\255;E\\3633Cr\\326\\003\\016vA\\0173\\343\\307D\\257\\255\\301gC\\340_\\203j\\264\\253o\\316L\\020M\\000\\277\\350\\031\\256WE\\227{\\032\\362r\\215\\305$-Wv0\\232\\372\\237\\0278\\320\\025u\\307f\\232\\325Kz~'\\215\\012\\317$N\\376\\035\\356wQ2\\253\\320M\\332\\321\\221\\234\\255IU\\012*\\350\\353,\\010\\252l\\234g\\025\\342q/;E\\366\\262\\302\\277Z\\225/\\300O\\026\\304\\037\\202\\335\\227\\\\\\247\\021Jl\\273*\\001\\2161\\237\\377&:{\\347\\252\\364\\013V\\200\\013\\373R\\217z!I\\244=\\012\\3050\\374\\303\\033]y\\317\\342\\203\\332\\016\\012\\346f,~\\233\\232\\315n\\307\\345>\\014\\223L\\2648O/n\\250\\037\\241\\312\\355\\323o\\337\\321\\276\\211\\256\\0335\\221Q\\323\\220\\377_m^\\302v\\314\\331\\006_\\374L$\\350y]\\353\\275\\357*\\231j\\310\\3176\\200#\\222O{\\265\\231\\311\\013\\242_d\\304\\343\\020Ts\\236\\225\\260\\212\\205-=\\223\\255\\341\\327$\\253\\2778|w\\265\\252bS\\213_N9}O\\004\\216\\020\\317W\\200\\335\\223}\\001\\3675r\\201\\346P\\353\\214\\362,\\035\\261\\362\\223\\012\\275\\206\\252\\017\\223\\230Wd\\027\\245^\\271\\013+:\\331\\322nd+\\035ZMK10O(\\375"\\254~\\237\\256~\\274\\246\\322TK\\357\\265\\231\\014\\034\\012\\265\\001\\217>\\336'+l\\245\\321\\355\\211p\\326g\\243h\\366=I\\251c\\246G\\353\\225c"\\323j\\311\\007\\003{\\207!\\212E\\360\\366pg4\\334N\\353\\015F/\\036D\\325\\225V\\013\\017\\022\\314d\\200W\\032\\370w\\306\\365\\364\\313\\034\\021~\\3304\\355[O\\006\\221=\\034\\347\\300)U\\220P\\327|~\\377\\343\\025\\275\\200Z\\010\\020p\\030T\\305\\244J>\\316;\\364|\\267ZM\\3020y%n\\277]\\316\\213\\241\\347t\\252\\201\\247\\303\\367\\215=\\216\\006\\273+|\\332EQ+][)s\\372\\024\\324\\301y\\202x\\202\\332\\317\\320\\215\\261\\315\\251\\370{@\\304\\245:P\\245!B>\\\\\\364\\021le\\024\\311$[Z\\224\\315\\213\\242\\010\\373\\012\\222\\340x\\014\\327\\234y\\265\\320kXU\\024\\253\\210:\\363\\017\\261B9[\\301hkiW\\213\\337*\\226\\210E \\221V}\\253\\236!\\312\\250^\\034F\\336\\320si\\001J\\323\\357\\272\\370u\\201\\241\\361\\255\\006G"0\\362Z\\010\\011=f\\306\\015\\374\\354\\2770}\\020\\216\\256RS;K\\207\\311\\232@\\212\\260y\\364\\342\\331\\227(+,\\247\\361R\\032\\2454\\277\\035+|P\\363\\221,n)\\011k\\345\\235\\204\\027\\235\\032\\002u\\215\\011T\\305\\035\\371\\011\\02614Kl\\305A$\\206\\304~\\255P\\005\\303\\314\\314\\335}p\\300\\015\\367\\253s\\376\\000S\\356\\034j\\333\\345\\357\\3220s\\2636\\330F\\315\\373\\221\\223\\312{\\316Q\\276h\\\\\\220\\005\\327\\034\\023/\\254M\\3504\\020\\011\\370|\\231\\346\\222@\\265\\350\\010\\366\\344\\254\\256\\250\\372\\360\\217g\\371\\232\\272)z\\267,\\302\\262qH\\366\\217\\024\\327\\\\\\234\\261\\024\\372gp\\244\\304?\\213\\000\\014\\3629\\200\\200\\245|\\022W\\303\\325{\\361G\\337|o\\230v\\275=\\214\\244\\300\\002\\324\\230A\\032\\014\\243\\310\\012s\\366\\275\\007\\024FO\\212\\362\\007^xFc/\\270\\225\\315R\\022\\251\\203\\343\\252\\246\\303\\246Z\\222\\357\\322\\262*\\361\\252\\202\\357\\230\\035\\267\\251\\366\\343\\360t\\224VJ\\277}\\377\\371\\017\\277wm_\\210/\\352t\\274\\355]pZ\\333m\\371br\\206\\325\\035\\224\\336\\2740\\317\\330_g\\033\\250\\327\\255\\014\\012\\352\\332\\262B\\377w\\344\\002\\2520\\323YHr\\245`\\340\\2631\\006Qs\\0154~mu@\\215v\\\\\\023\\205\\250\\203\\210\\303wB,8\\022xI]\\354\\274\\315\\204\\251v\\226\\337\\333\\260\\233\\276\\010\\370\\005-\\265\\331\\354b)H\\374\\307`E\\327\\014\\211\\354\\210\\302y\\001\\312\\012\\364\\036\\251XYl\\345\\222k\\315o\\365\\366\\326.B\\263\\206\\242\\354\\221K\\275\\360zk\\350"N\\211\\2065\\321\\364\\200\\271\\245$wq\\241\\301\\305c\\304R\\304.3\\374\\014R |\\000\\177\\372K\\337\\203C\\2010a\\255l\\016:c h\\202\\245\\211\\355_}\\035^S\\342\\001\\346\\250\\012\\263\\303\\227\\227\\270\\011U\\251X;x+K\\366\\002O\\240v5\\253\\246r\\300\\373\\351\\230{v\\373c5p\\315\\304I\\330P:\\225\\303{\\337K\\211}\\177xA%zj\\234\\315\\222\\022\\266\\216;8\\347\\025\\320\\341 \\005\\375\\001\\\\\\346\\366-\\244\\355\\223$\\003`lpJ{:FD\\201 P.\\226\\330\\340\\2525r\\342\\001\\216\\211\\322\\022\\332\\270\\337"9}\\2420~\\375\\220\\231\\252h\\246\\031\\354^\\274\\001h\\341\\235\\247\\004\\347\\365K\\210y\\270SkT\\212\\226\\366\\204\\332\\270W\\374\\000@"\\216\\312\\001\\012D\\361\\304\\357\\342t\\201\\257\\256q\\246\\200\\257\\305\\204\\213\\322\\026\\036\\317\\007\\004\\370]\\345\\261O\\020\\272\\350xb\\206\\355\\361\\271l\\252\\201>\\233{\\305F\\2624V\\2652(\\247i\\226!:\\275\\027\\356\\022\\345+\\357\\025:\\333\\010\\253\\007)\\254\\225\\235\\377p\\230\\303 J\\325\\003J\\352~\\267>8)C\\277\\331\\022Qv\\273\\301\\0372\\374\\004\\177\\314\\211\\011\\370\\203qUt2\\362\\332\\332v\\346\\177\\0251lbR\\207\\321SX\\230/\\316\\344&\\216\\274\\025\\245$\\262f\\270X\\224rs\\034\\360\\0304\\236\\232\\256\\263x\\034\\226U\\2508Sj\\243\\334\\265Fn\\225@\\264\\017\\027\\336'\\367Z\\366t[\\320\\004'\\374z\\277Y\\\\\\356|\\206\\344\\353\\343m\\276\\214\\370\\211?\\226\\013\\270?h=\\337\\315\\241\\350\\242\\237\\335\\221\\32065;\\000i\\232\\217\\002O^\\021(`\\245\\177\\325i\\033\\356\\336\\372kV\\314\\212\\261y\\352\\006^\\230n\\303\\273\\032\\014j?_\\377%\\004\\342\\226+\\211w\\325D#\\016\\032\\363\\274\\334s\\302Q\\341U\\226\\377\\216k\\376\\002\\376\\235"\\311r@\\037\\246\\336W!7a\\027\\326\\256\\252TUN_\\315b\\340\\030>\\036\\016\\356\\324k}\\021\\330d\\222\\367+\\350\\212\\376\\346\\232+\\275\\036T;\\243\\356\\311/P\\033\\246\\200N\\220\\356\\243\\321\\277\\271\\274;\\343h\\304\\333\\320$U\\253\\370\\034\\271\\216Bs\\221\\247\\357\\205\\242\\3543\\014\\357#g\\373\\016\\260\\002c(\\252\\236\\376\\336Va\\221>\\303\\320O)\\374\\323\\352\\204R~\\004\\251\\267\\017\\331\\037\\255\\2755\\246<6\\021\\312\\203\\360\\011\\012H3O\\211\\227*\\372.\\2308\\351G\\347\\213\\364B.\\213\\026k\\225z\\337q\\013N\\264O0L\\223\\327>}\\245\\222c\\330\\240\\373\\321\\314\\233r\\346\\345:\\241rp2\\260\\267m\\217\\200\\011]\\245\\311\\3569\\036\\334\\362\\035\\321\\351\\336\\024\\275\\212\\255\\274\\006\\276\\230\\361\\361\\313\\222\\266W\\271\\003\\2302\\343s\\271\\274@\\007\\245\\2128\\034\\334\\000\\246\\314x\\231\\244\\011R\\222\\240\\212\\231B\\241\\334\\361\\010p\\346h\\217\\213\\033n\\333\\232\\342\\341*C?8/\\317Y\\021N\\014\\002\\334Sj\\2619_9\\317@*\\206\\274\\302f\\234\\300\\365C\\224\\265\\220\\362\\271\\025,\\324\\237M\\177\\232\\234Mx0\\216\\375\\022|>\\2444Q(\\274\\325 Y\\274Y8\\307G\\365Y|}\\013;\\362W4\\244\\357I\\027\\306\\366\\010\\002\\247\\256\\254ft\\372\\373\\3459\\034\\277-s\\261\\252_w\\340\\234\\274I%c\\027\\300S\\315\\305\\325\\255,hkb\\365n\\270X\\034\\273\\360|\\315\\273\\345\\027)\\235S\\206\\236y\\213\\325[\\034\\316\\222=\\243\\221\\213\\227\\244\\243\\024\\360\\314\\214\\034\\212\\317"\\022\\340\\262\\323W\\271\\225'\\343\\234M?BOs\\347\\257%\\217V>{j\\024\\027\\203[\\037x\\332\\317_\\350p1\\365}\\231\\255)\\211\\272Yo\\236z\\222\\262\\035\\372\\031\\235\\003\\005\\233\\011\\274T4\\007s\\317\\323\\013p\\376\\243\\312\\212\\345\\332\\330\\031\\332\\014A\\363R\\214\\2557jHS\\015\\371\\3760\\241,\\225\\271\\032\\000\\303\\366\\243\\026\\374T\\214Z\\027\\026\\323\\031\\330\\0119f\\274\\314\\017S8\\236\\257Vc\\324\\2356\\272\\236C"\\330KX\\250\\311\\337\\003\\277\\334\\303c\\227e\\001N\\307\\026\\001\\242\\026\\003\\016\\232\\226x\\250\\177\\363\\326\\021\\364r y\\325\\243\\334\\343\\322,~\\364%\\243\\356\\352yV4\\314\\347\\036\\273\\002`\\267\\023\\376$\\035\\373-$\\377&\\006\\243F\\334vh\\324\\336M\\300\\350\\206n/<\\247\\351\\363Czr\\366\\372\\323-G\\230{\\335\\270HSs'\\011\\217\\216\\241\\377$\\336HX\\355`9\\336g\\026|\\262\\376\\223\\033G\\244yY9H\\311\\020\\011\\030\\345!\\257\\306dV\\376\\024f4\\334T\\244\\313\\024\\275J\\333u\\360\\215\\363\\221\\304\\367\\346\\217\\336\\205]TB\\346\\3669y\\373\\272]\\271\\017\\032\\0372\\016\\211Um6F\\365!\\004\\031I\\037\\262\\301S\\307\\323\\210\\263@\\007\\34577Q;\\345uG\\336\\177\\3461\\026\\266\\220\\357\\302\\250'\\241\\255\\001\\016\\250\\0132\\355\\332\\300]\\231\\312C\\270)I\\324\\362\\016\\234\\255\\235/@\\250\\317\\365)j1r\\012MPB\\315a\\024\\215\\366WG\\374\\002\\355\\274\\241x\\005\\357\\337\\016,\\263r\\364\\207\\242\\201\\221\\025k\\031\\252R\\245\\263\\367\\013\\011\\253\\311\\370\\252#9N\\277\\266\\0221\\275\\272\\033[\\244\\274cA\\0116j\\217\\237+C\\324#\\005G\\345\\357\\336\\001d\\311\\256\\332zY\\021\\033?&zT\\243u\\261\\305hx\\355|\\312\\366\\263b\\206\\232\\334\\244\\331m\\362\\016X\\031Y\\177\\261b\\317\\372;\\316@\\275\\037\\337H\\227\\022\\201\\204\\235\\356\\370&\\016\\333\\254\\356\\006.i\\242_I"\\303\\250\\332\\262K\\230bA\\021\\000\\320^\\221\\201\\217\\273WVHG:\\270\\331&\\251'\\272\\331\\002m&\\022,\\260/\\201/\\250\\216Vr\\361\\343)\\253\\367\\242\\252\\312\\342\\005]\\242\\205B\\013\\367\\247\\333\\216\\274yq\\227N\\012\\277\\251\\221q\\2732\\311\\202\\253\\002Xn\\3506zM\\326\\307\\032-\\272H\\303o\\215\\250\\256\\200w\\243\\277\\034`\\341\\366B\\004\\342k\\206[D\\003\\337.j\\223N?[\\311\\034s\\244\\303\\354\\340r\\303<\\026\\037\\367\\344\\326>p\\215\\342\\217g\\232jAS6\\223\\200L\\037K)\\350\\001,\\371\\302g&~\\223/\\347^\\353\\274\\353\\367\\007d\\331\\372\\232?\\302\\022q\\320\\267\\016T$\\301\\3021\\264\\234\\355q\\326\\346vw\\267\\341\\271JxD\\232\\212\\244\\220\\2575\\\\\\200\\247\\011\\340\\277\\201\\0138\\216g\\262\\016\\315\\002\\243'\\262b\\347\\\\Y\\003\\377?"\\200\\245J\\374\\300\\020\\314\\230\\206\\221(\\211aV6\\201\\271\\371\\225\\3649Q\\207\\347\\253 \\204n\\236\\305\\020\\036\\322+\\343\\315A\\270\\215V\\246\\374\\216\\032\\264\\264\\337\\200r\\004%\\370'\\301\\361b \\244f\\364\\347\\340\\306'M\\274\\235\\336\\001Z\\374\\372\\201\\350\\201\\275l\\377\\334#\\015\\036`}\\266W\\230,\\312\\037\\346\\312\\012\\254Y\\270\\004\\242\\0059v\\253\\327\\327\\265\\215\\345\\\\\\333h(\\311\\030\\220\\255&v$E\\202,u\\210^\\010\\211\\220\\001b\\206R\\351\\262\\265\\363\\3131\\271\\342a\\002\\327\\364\\036y\\022\\350BR\\337\\203\\242\\027\\037-\\320\\000\\370B-8\\024r\\326\\015\\246\\312X\\006Y\\275ch\\272'\\230\\234k\\2156\\227\\246)\\264\\264%\\022K \\337U\\347ab;\\313\\0030\\304\\3424\\356\\372\\227\\221G\\366\\025\\024L\\036t\\342\\223X9\\205\\274b\\013\\017y\\201\\341x\\317fG-:\\245'\\226Wy[If\\227\\341K\\004\\272/\\313o\\262\\231\\325'/\\020\\372\\344\\342\\366\\216q{\\021\\351#\\231j\\332\\207\\253\\317\\006\\246\\237O\\344o\\034\\350\\362\\336\\273Z\\000\\3225ur\\3521\\273\\335\\026m\\014\\267\\347H\\011\\350&\\033\\353\\273\\006vx\\325\\346\\202\\315\\312\\320\\356p\\201\\304\\276\\361\\277R\\331\\312T\\027\\331\\025\\303\\366y\\231dD/\\313\\030D\\277v\\260e\\012`\\033Sh\\363P\\025\\276\\000\\244Te\\331\\346M\\265\\260\\226z\\370&\\361\\276\\236~\\376cf<A\\344u\\224R\\357\\224\\206\\233\\267<\\354J=\\364.!j\\352\\202h\\345\\350\\005\\361\\266\\245E\\257\\301r\\232 \\304\\330\\342\\022\\237\\031\\204\\216\\325\\024\\302\\233B\\355\\240sgwU\\373\\360J\\\\\\250ky\\230\\301e 8t\\275u\\314\\225KB<\\350"\\007aA\\321\\344Q+\\211=\\355\\317\\335F\\374zY\\276\\335E\\037\\213N\\225\\277\\304\\327\\325\\270\\021\\330\\202\\212\\012\\333\\305\\360c\\337\\246h\\2774\\337\\341h\\257\\230\\222T {\\326\\202/]?\\251\\332V8\\234M^l[\\222\\177M\\211\\330\\032\\206J\\012d\\342Kc\\005\\315{\\263}u\\360\\351E\\313\\033\\364]\\306v\\353\\343\\037\\244\\213\\271\\274Q\\327\\251\\037\\017\\335\\202\\233\\313\\270\\200k\\305d+lf\\034\\340&\\347\\\\>\\347K${\\012\\214C\\374\\024\\005:/\\255\\037\\322\\035\\020?\\3602OK0\\235\\371\\366MBw\\255\\245\\022f\\220s\\320\\\\\\254V\\227\\365<EI\\322\\360\\306\\236|<\\373\\315T\\264\\253\\303-\\177j\\214\\300\\272\\213\\326\\363\\357\\300S+\\210\\204\\3101M\\257\\304\\306\\342\\260\\324\\023\\341\\271\\016x\\025\\227\\240\\354\\325\\230lXP\\366\\021\\255-$'/\\343\\220\\245\\300\\340)}u]\\037B\\236\\315\\336\\001\\370\\251_"[^'\\341R\\357Y\\331/\\317\\304\\342\\2172\\305\\375\\025@\\221T\\251\\236\\267\\010\\312 \\371U\\277\\003\\243g\\302>a}\\222\\327\\246\\243N\\036bNPDc\\353\\362\\231!r\\223\\251\\324G}\\021\\347W\\304B,f\\303\\320wz\\215i\\252\\334n\\367\\356\\373\\260G;c)\\202'y37\\274r\\300\\\\\\341b0\\353C\\211\\360j0\\374\\371r9\\254\\010\\267O\\313\\274\\306\\211\\011\\316b\\301E\\300\\036\\246\\217\\323i\\367\\361\\212\\003?\\311h\\277\\006\\326\\307^lPdP]\\234\\363\\320t\\275\\324\\307#[\\352\\253\\350\\222\\261$\\277-\\023\\341)\\351\\307\\227\\347\\233(\\3021bG\\264\\334t\\006\\326,\\3058\\200\\242Q\\236\\000\\355\\2434\\334]\\243\\004j\\032\\022_\\306\\224\\\\\\005o\\014\\334\\260\\332\\250\\305\\326\\320O\\227\\226\\364\\331\\275\\324\\2261\\177'\\225\\306RHv)7\\226\\256\\306`\\330\\357\\317\\235\\304\\010\\304\\317\\311\\2115\\304O5\\333\\001B=\\226\\237\\217\\2618\\323\\325\\352\\177\\331Q\\311F\\340\\2053@\\001\\365j\\2562\\300\\343W\\341\\360T\\217\\311(\\375JI\\256\\223A\\2004\\323\\350u\\207P\\020\\366m\\241$-{\\320\\027\\001Fr\\015\\014_\\216\\304"b\\230D\\3577\\026\\306\\024,\\014\\270\\223\\210\\024\\353bK*\\004t\\200\\377;\\002?j\\326\\333Hr\\020T\\217\\001\\307}bJo\\001\\316>T\\235\\013\\010\\273\\\\5\\321J{-\\275l\\274\\005Z\\301e\\032\\263\\274\\215B\\3660\\244n\\001\\3518k\\232\\230\\334\\005\\025\\2160\\017\\323\\3124s.P\\327\\003P=\\317)\\335E\\337\\334B\\274\\214\\313\\332:c\\324 y\\032\\324\\271\\006\\011[\\235\\251\\240\\260\\244\\272\\205V{\\255k~\\363\\255\\322\\306\\350\\0300\\341\\360\\365\\006q\\3706L?\\355EZs^d(\\265H)\\202\\034n\\2426*\\270(.\\002[ih\\202%\\265\\271\\277\\033\\227t\\266\\337F\\003l%%-\\254\\212?\\275oc\\033\\023\\276\\317K\\277\\037\\330h\\243\\357\\3274\\011\\374V\\035\\306\\214[\\004\\306\\2023x\\035|\\233\\221YyW1\\366\\356\\2122\\367\\013\\014\\317\\227B\\031^\\251\\255\\373\\250p\\026\\025\\201\\227~\\033\\326\\376\\247&\\347\\363\\351\\262\\234I\\030\\020\\266\\200+E\\030\\313\\242\\335\\202{}\\313\\266\\266\\303\\345et;\\026\\3672j\\317\\305\\2150\\207\\015d<\\313\\341\\270i1\\210\\217\\012<[;l\\240\\025\\321\\361\\312\\0311\\211\\344\\034\\265\\2520n(\\260{\\307Y\\313\\261\\260S\\252\\2430\\35137\\006:\\246\\221\\320\\325\\237\\226q9Z\\315\\244\\201\\242\\360\\212(?yl\\350U\\3520\\336\\240"\\030\\312!O\\324\\334\\260\\310)%|\\356\\372\\010\\031L\\022:\\246g,\\014{\\321zE\\257\\217eO\\211{\\0103\\330\\303S\\337{\\005\\204\\370\\2049/o9\\310A\\313T\\256J\\026\\205\\014\\255<\\233\\227,Bz\\303\\275'\\033\\205\\354z'\\310a]\\350Z\\362nq5(\\203\\203\\312\\217\\324\\275\\335\\246f\\001\\022\\270,\\\\"\\235\\276\\264\\334\\323(xC\\345\\222S\\203p\\314\\364\\233\\301{\\320r0\\305\\300Q\\3067\\355QW\\034#R\\336\\037\\013\\310_L\\225%\\230\\001\\35198\\352\\350\\372X\\225)\\345\\013\\\\-P\\225\\202\\354\\340\\266WV&cN>\\264\\243\\270\\240\\233E\\010\\\\\\300m\\0343\\035\\312\\362\\210$\\000\\307\\376\\205\\230r\\333Ua\\023/\\031\\235X\\3467wT\\025\\332\\244\\303\\030o("\\204\\370\\304/\\226\\321F\\274\\304\\327\\344f\\006]1\\332\\312\\321\\246\\366\\274\\005\\320\\032w\\015B4\\353\\335Nv\\207SA\\222\\300\\254\\373\\304\\035Z\\001OU\\374gO\\224\\307\\363\\374\\204o\\271\\005\\356p\\3300\\366ZF\\263\\3122i@\\341\\003\\247Xj,\\264p\\265\\311,\\316\\\\\\334~x\\342\\252\\015\\324\\206z0Q6\\362\\300\\332\\361\\204\\354\\214\\262\\207$~\\022"\\3562\\255\\210\\\\\\245F\\307(\\304\\363m\\303}^\\314\\323^}\\364\\313\\037\\242]u\\244\\252"2\\375\\332@|a\\302X\\0151"A\\214B\\012\\246\\377KN\\340jsJ\\356\\316\\371ct\\336\\3676\\316kTN\\373\\011[m\\000:X\\255\\013\\251\\206\\307\\235\\257\\360\\227\\312\\210|\\177\\342\\375j\\027\\017\\0150`,\\267\\012RMU\\350\\240w\\357&\\351\\267\\027\\272\\372\\313\\360n\\310\\237\\350O\\223\\226O\\254\\305P\\211TXTI\\226%F\\015,\\233h\\262\\217\\352\\306Lo\\260\\215\\363\\352\\351\\341{\\332\\030\\351Q\\362\\213x\\304\\027\\316jU\\251\\300YC\\226\\340E\\261Z+\\244m\\355\\013\\354\\324\\256\\223\\302|\\245J\\240\\314\\204\\005\\351\\324\\243d\\036'\\035.\\365\\000=\\001z"\\325'<\\302\\324q:\\012}\\244\\307B\\325\\247\\363\\035\\376\\206b\\235m\\353V\\232fN\\232\\022\\234^w\\253\\013\\204$o\\013#\\341\\227!\\012x\\337\\032o\\310\\241\\022\\343\\240lT\\342\\\\\\223\\322 \\324d\\025*\\320\\312\\003l\\363\\226\\035}\\037\\227\\315#\\344t\\221,\\025_\\354\\321#\\372\\212F\\374/\\340\\230\\316\\206H'\\006A\\325}3\\236\\240\\012!\\221>\\375\\334\\2434eJ\\210\\332\\000/\\273\\373\\373\\325\\335\\217c\\363G\\027B=$\\341\\015U\\207\\251;\\343dT\\223TH=\\242\\245\\032\\256\\005\\373\\363\\214\\005\\022\\255\\225\\357\\272\\210\\033\\236\\272\\273\\027\\351\\2515M\\020\\232-\\356\\350\\013\\265\\013\\272\\2235\\273\\315PUxF\\342\\246\\371y\\240\\217\\030\\351E<\\333\\2510\\331<\\003bYr\\310\\327\\241\\304\\034?\\033\\222\\037\\024\\232\\217\\241\\275r\\213,.\\311T\\256?\\272\\256\\207\\243\\022\\363\\351\\307\\370\\373Y\\013\\343n\\337\\3336\\312\\010\\257\\265\\341\\335t\\220,\\202{p|v\\361f\\276\\231\\234HF\\025\\377Y\\343_{\\211\\010\\217\\346fu\\372\\241Q)T:\\344\\313\\214\\200\\217\\344\\3027\\303\\003"$\\272\\006\\033>\\343f\\253\\371\\304\\307\\355\\235\\377}G\\343N\\036<\\230w\\362\\234\\307\\322\\266\\342\\324\\261_\\014:]E\\201\\264\\255\\273\\261\\236\\276\\353\\337\\365(\\205\\231\\223W\\337\\032\\335\\372\\257\\336\\350\\363Yyu\\214\\334\\366b\\246\\245\\023\\275\\0011\\267\\272\\264\\324>\\345\\016\\317\\000\\346-oat&p\\026m"6\\006q\\332b7\\275Y\\243%\\243Vnm\\354\\256\\307@" \\246\\316nH\\3764g\\223NPKlXx\\340\\253\\356F\\022\\2662KcO\\336\\317\\016$M\\215\\035\\013\\312|\\356\\223\\235=\\000f\\003\\335\\376\\357\\316\\337\\015\\341j\\233\\373\\324\\251\\365u6\\365\\302Y\\346\\016\\242\\026$!{\\354\\015d\\346v5d]c36^\\376L\\026[YF\\0135\\212\\2424@\\002\\030H\\236?\\325\\221\\201|bW\\270\\241E\\275\\310\\2640\\265+e\\034\\215|\\334)^>\\261G\\2179\\350\\307\\324j\\360l\\253 \\251\\2319D\\367\\345\\3333r\\3513\\274\\222\\336\\017\\031\\217\\370\\214\\255/;\\223q\\315\\000\\306\\220\\212\\324\\335\\246\\214b\\212\\341\\343\\263\\317\\317Y%\\372\\001\\313\\011\\264\\340\\031-3\\007\\222\\340\\321;\\372"\\274\\\\`_x\\347\\354h\\202u\\217\\030\\274\\305\\210\\033i5\\031\\024\\262*\\332\\221?\\017>\\223\\204\\025\\222d\\357\\274\\306s\\3727\\000M|\\037\\034\\371\\203\\325\\001_f)=\\036$gM\\262\\335\\237Q\\020y\\3143\\372\\333T\\207\\356\\362\\031\\003\\206\\024Et|\\224R;\\275\\247V\\2566\\316zq\\353\\251~\\026 \\3156E\\200rh\\021\\253,C\\354\\221f\\261"\\253s\\341BD\\333\\233\\005 G\\211^\\340x;k*\\262\\302\\007\\305\\253\\017dV[7\\247\\245\\211\\316E\\231\\237y\\3333(\\035]\\234\\277\\340\\030B\\322\\356\\352\\012\\026J\\370T\\242Q \\205"\\202M\\220V1\\276\\330\\370\\332\\377\\360/j\\016\\0264gX\\335nT[\\037D\\353L\\027O\\301)\\350\\305\\312l\\264\\000a\\356Ii!\\023\\355{U?<\\3651\\011\\352\\300\\274l\\230t\\330\\261\\330*\\350-\\3353/\\010CpQ4\\304_\\007(!\\011\\376\\304\\007\\017\\366!\\252b\\267X\\307\\305~T\\350(\\345l\\353G]\\322\\240\\003\\230\\357\\037\\252\\363\\324\\342\\367M)~\\272\\270\\273>\\307\\222\\301Phi>[\\350\\306\\213\\326\\200\\246\\03617\\227o\\264\\331\\346\\335p\\021\\274\\225\\036\\273\\027\\200q\\206\\343c\\256A\\301\\327\\252=Q;\\257t\\233\\344\\233\\275\\363\\246\\032,\\316\\365\\246\\373\\367JH\\351\\274\\203\\371\\210\\211}g\\207V\\004`\\331B\\003\\367^dWL\\357!\\253\\337\\273\\274\\233;\\300\\361\\372\\036\\257\\320(F\\267;s\\316\\353\\275k1p\\032\\2501\\304\\267I\\225\\240\\006\\\\f\\263\\227<\\267\\332*\\243\\236\\\\z\\357\\245\\2623\\007\\322\\356\\2239\\246w\\2171\\035$\\367\\226C\\267\\322\\231\\262C\\277\\353\\363e\\274g\\346\\361n\\364\\325\\31049\\355\\227\\354\\246\\033G\\027k\\325g\\200DS\\341\\326z \\374N\\323\\024\\366\\372P4`\\031=a\\306\\264\\334\\027\\3049\\201\\366\\034\\245\\310\\370\\320\\336z\\273\\212;\\300\\012\\376C\\201\\366\\240j\\210\\231\\241\\222\\023\\227\\221\\335\\317\\021)\\200h5\\235\\377\\363f\\364\\3374\\224\\347\\030\\010\\023jx\\2233\\302I_\\235\\257\\020\\037\\307\\303`\\226\\034\\336\\005g\\323\\0153\\022F\\021\\313?\\213\\303\\245#\\325\\222\\273\\376\\030\\030n\\302\\203\\007\\320\\211\\267\\015\\002\\233\\022~q\\233\\324\\005A\\206l\\373\\224\\034\\244F\\201\\363\\330F\\243\\305\\033\\273\\2341A\\023\\27419_\\324~`\\326\\025:\\020\\355\\3748\\206Y\\020\\211\\356\\311\\275\\3211\\375\\003xv\\004\\306\\272\\236k\\013\\033\\223YQ\\006Tw\\365\\361m\\225\\202\\000?\\240\\256\\254[\\325]\\345\\333\\\\F\\361V\\251\\361\\025pR\\311N\\315\\374\\260\\335\\304{y\\312\\361\\365\\230q\\343\\035\\260\\236=a\\263b\\250\\200\\323MZ\\372\\361\\345\\217\\271|3\\327~T\\234e\\263>\\211\\324\\016p\\256\\177\\374\\0068\\343~\\202\\247{\\245^ux\\300\\341\\353\\364E\\377\\275\\240Y\\220<\\224\\242\\027\\303\\272c\\365\\231\\206\\261\\253\\026\\324\\262Hy\\2264D\\356HC5]y\\332O=\\005\\214\\265Z\\327!\\200\\317\\266\\264q\\330\\224\\253\\262T\\220\\006f\\360m\\027\\223\\026\\374Z\\306\\002\\036\\032\\034\\261)\\331j\\373\\031G\\005\\234.\\371l\\036\\227\\243\\225\\3155B7\\234\\036'\\257\\024F\\325\\250\\251\\314\\252\\341\\265\\273\\362\\225\\3655\\014e\\327,E}\\017\\005\\266\\272\\224=\\265\\031Wo\\254\\364\\270"\\021d\\206}mz\\033?\\205\\272Q\\261r\\261Pt\\354\\370b\\012\\261\\360\\300*\\2073\\224!A\\243R\\274\\232\\334:\\226\\242\\366\\221\\271\\354\\354\\3544zO\\245\\220\\030\\332\\231\\215\\250\\017\\350e\\037\\022\\303\\323JY<\\321:Kb\\015\\322H\\203\\027\\014El\\013\\025\\265\\247\\255/\\260@\\323\\346\\030k8\\312\\241\\203,.LT3\\337\\022\\221H\\037\\240\\177jp\\304\\231\\006)\\364\\310f!\\356\\357\\3549t(,X{\\177\\303\\317\\252\\323\\271x\\2732\\201\\2139\\323\\312\\032(\\260\\231/\\366\\364\\231\\263w\\3411b\\024IS\\210\\306.3l3na\\275R7\\252WA\\342\\345#\\0121.\\210q\\014[3\\277\\302\\320\\237\\263\\014\\345\\346\\243\\215\\373V\\253\\031\\253\\332c\\2351\\034\\304\\363\\253\\036R\\314\\034\\300\\270*j;\\203<*:\\177\\3272\\237s\\321f&"\\262\\270\\013G\\034\\370\\360\\231"W\\245\\037\\347\\300\\226\\216\\313\\304\\236\\276\\016>Q\\331yx\\277X;\\230\\341\\240\\2225\\010X\\247Q\\340\\015\\374\\221~\\033zR\\272\\221\\020\\330\\224\\\\;\\021\\321w\\206\\244u\\226s\\304^\\367qW\\032*\\351\\227@\\315\\227F\\303\\006\\350\\310}\\232v{\\3554\\243\\245\\222\\334b,\\022\\2169XS\\004U\\332Y\\257\\353\\177\\361\\343\\360-\\314\\262=\\002\\352\\013!\\357'\\022\\372\\002\\346r\\351Y\\212\\373\\263\\023-\\216\\231d[\\232\\361:\\247\\255\\311\\251zg\\255\\317M\\307\\345\\310\\034$\\011\\315-\\241fik|\\230\\312p\\275\\342\\224\\022\\367*\\3512vX\\260\\200W\\015v\\335\\330\\343\\241\\355\\021\\366\\210\\263\\376-\\246\\314\\207\\212Sef-\\311s\\204\\330\\213p\\223CUtwb\\337(l\\315)\\242p\\014\\237\\032\\205-\\305M4>\\370e\\220\\224\\012\\206\\275d9\\255\\323\\026D\\305\\002\\260b\\037\\023\\364M)\\351\\232=\\242\\242\\324'\\200\\360\\207K\\3514A\\374lm\\250tk\\303\\242\\2144iY\\\\\\017\\002\\024\\036\\276r\\314\\364[\\352\\230\\273Rr\\243\\254\\233>\\214\\240\\356\\222\\017{k\\371\\336h\\014\\263\\201\\333\\244+\\202\\350\\301\\307\\320\\306\\330Q\\212mI\\025\\315\\257\\221\\375^kr\\333\\215\\320CZbl\\260\\011\\217\\300\\245\\003\\222\\005:\\220\\375\\244\\213\\364\\321jK\\303\\363J\\2478\\312\\227|\\277f0u\\352\\250%\\322\\227\\223\\324M\\231\\321\\011\\024w\\266@\\276\\361\\242qi=\\310\\023u\\225Z[@\\013\\307\\273\\014\\266}\\203\\235\\217\\227\\335\\320j\\224\\303'`,\\015\\351I\\0055\\004\\360\\351M\\245^\\310\\264\\216\\303s\\331\\314\\345:Y\\352\\017F\\215}v\\210\\203th\\212\\265\\373\\021^\\244^;\\2734Q\\332E!\\333\\300\\205\\312\\234W\\034\\317u@\\013N\\244\\261\\212w\\254^/\\221\\326\\254s\\346\\372\\035\\211\\007Z\\026\\303\\015\\343'"\\3049s\\037\\007\\245\\366C;A\\261\\337\\213#>!\\247\\033S\\022`B\\013\\266\\355 I\\321\\010s\\325U\\3750\\327)\\330\\2244O\\202\\323#\\212\\313b[\\234\\320U.\\370$%\\353\\363%e\\031W\\232\\367v\\254l\\031+\\177W\\212$1-4\\254R\\022[3\\326\\241\\013\\357d\\037\\344\\27092fa\\254\\262\\373V\\306\\016\\216\\025\\014S\\026\\257\\2116\\036]\\026\\023\\344\\337\\251m\\032Z\\027\\347\\230\\315PE\\330S3V\\367\\371\\271X\\015{\\032\\223\\212\\001\\021\\237\\014bm\\205^HU\\347\\220\\207\\260l\\276\\365\\016;\\266\\024\\015\\300)\\331\\005\\306\\311\\341\\346\\277p5N\\241\\351\\253 \\353\\256\\304\\211\\202n\\316\\026Xm\\374\\203\\010Y:\\345\\320(I/\\210f\\333zg\\247w[\\234\\3447\\004\\014\\344\\360\\243\\356\\024\\265\\232\\230\\344\\0229,\\004Ytwn\\\\\\230\\367\\251g\\330O\\0027\\271P>9\\377\\206\\346v\\221\\377L=\\202\\204\\006\\251\\343\\321\\265\\315\\332\\336\\327=\\235A\\001\\004y\\253\\232C\\213*n}%\\\\\\223zc\\177eQ\\307\\324\\316\\177)\\312@\\374\\215ih\\027\\355\\231\\251]e\\222\\036\\231\\241\\216-w\\313;\\234\\234q\\037\\227\\260E\\371iY\\357\\210\\024T\\232\\015.\\020\\331\\307\\341\\334\\302\\377\\341i=e\\207\\363\\262\\034w\\327\\200\\234\\260\\220\\311YO$\\275\\240\\313^\\027\\207\\313[vW\\226\\200\\302\\201\\302\\270\\346\\346\\276\\312\\363\\224b\\257V\\367>g0\\204\\225RP\\217[\\027\\354\\300\\206h\\366\\303\\267B&\\320{\\210\\007^\\217\\002\\\\\\035i^\\253tJi\\254f$\\320\\010\\313r\\333\\374\\205\\217\\017\\331\\217.\\306]SR{\\332\\001\\343\\207Q*\\265s\\021\\263\\200\\031dWZ\\270\\373\\304\\3450\\215\\323\\350.?0kn<$\\263\\274K\\262\\260(KB\\024\\012X1:\\372\\225\\036m\\377\\306Fc\\227\\202\\376\\241\\201\\343\\354\\001\\355\\270\\0326*\\356\\254\\315T\\323\\320\\205\\237Kj\\220\\033\\274*|v\\210\\327\\236\\333\\000\\336\\027\\322.\\266\\256l\\235\\352\\036V\\205\\276\\322\\316\\324\\275\\306\\316\\032\\203\\252\\232\\245l\\367\\235\\256\\314\\231A\\345\\0061/\\333\\253,\\240\\377\\367\\327b\\330\\030\\371\\375U$!H\\021\\375 \\215b\\223\\221\\323\\257\\377\\300\\275\\272\\030\\201\\364\\214\\024)q\\332X\\261\\260\\207J\\275\\2265\\3361\\024\\177Q\\275C\\003_\\244/\\246\\2315]\\252[T\\320i\\303\\211J\\2623\\333&SV~T\\224\\273\\306\\310\\310F\\302\\007\\216+\\377-\\315\\204B,`l\\335BO\\357\\310\\222:$\\3327\\033\\234\\366\\274wI\\351U\\006\\005M\\010z\\257\\241\\201\\234\\236cHK\\274\\364\\322F\\3772\\250\\236\\014\\011\\345\\272\\221\\322\\2746n \\256\\263\\211\\304\\360\\322\\024\\2052\\005[\\016<{\\203\\267\\374\\337[\\000\\216m?s\\037\\031\\372\\310\\362\\217\\035\\012\\3152\\322\\361%n\\023\\370\\252f\\013\\335[0\\265\\205\\226\\267\\360\\233\\262R\\000\\300BGi\\206%\\203R\\250a\\310\\206\\302\\244r2\\202Ib\\365\\315s\\006\\366B\\365_N}\\221_\\344\\347\\354~\\307\\303u\\271T\\234\\203KX*\\210C\\035\\242{\\333\\231\\300\\373\\\\\\212\\363=!\\234w=^m\\027\\360\\220\\245L\\243\\316\\025wz\\020x\\265;\\254=m\\365\\001\\373\\272b\\303>\\003\\243R\\016q\\217\\313\\362\\025\\271\\256\\314\\333\\335\\227\\221|m%f\\261\\332Bb(\\360\\304\\013'k\\363\\001\\027BN/r\\342\\357\\246fU\\340\\375N\\334\\0118\\245\\013677\\217\\021\\370\\203_\\017u\\356\\254\\321\\374\\226\\273rt\\0100\\265\\316\\031\\251/\\212FP\\223*1\\336\\215\\266\\243\\374\\250\\373P\\235\\273\\266\\222>\\025$L\\371\\367\\252\\210\\031\\0055B'L\\010S;8X\\233\\002\\010\\207\\030\\204\\230\\354\\231{y\\246\\244\\014\\253\\275\\341\\004*\\006\\024\\226dBS\\205\\030|5lfA\\305\\352ep\\311\\032\\276\\\\\\223\\365^\\200\\035k`\\005\\345\\224U6DD[*\\200\\357+\\242\\227G\\331\\020\\235\\237\\227\\347\\002\\367\\324Rs\\241\\235 \\370\\366KN\\232\\006#\\216\\032F\\237\\011\\006h\\272\\305*\\261\\315\\300\\025\\257\\366\\267\\372:\\204Ew\\260\\213B:]\\2624\\027\\015\\203Z\\206\\320\\240\\303\\264f\\032yP\\027\\037\\027g\\034\\031D\\232MJ\\006\\314\\342\\027\\201.\\220M\\336s\\0017F\\374[\\0272{i$\\212@L\\354\\277(\\353-&\\216\\010P\\305\\344yA\\035YxE\\021\\220\\335\\304\\010x\\\\\\251\\355)^\\002l\\254\\004\\036h1)'\\252\\333*\\270\\003\\230\\356g(j\\304A\\321\\203\\211\\027\\307\\345\\351c\\232\\347\\3652S\\262\\237\\311#\\036\\240\\265\\323\\013\\360\\362&mjR\\001\\005\\012\\347\\335\\255LB\\232R\\367\\254\\262\\034S\\3528\\270\\202\\312$\\372~\\200\\324m\\275:_\\332zB\\305\\212\\332!\\340\\3446\\037\\016\\032uP\\000\\010\\363bi\\011)\\247\\361\\004l\\252\\310\\373\\324\\302\\321FiC\\232\\037\\325=\\234\\204\\241\\304J*\\264\\246\\310L{\\331\\374\\000\\342\\223\\260\\370\\014\\327\\2777kH\\356\\013{\\023\\231\\356:\\002\\302;G\\331z\\267\\227\\320d\\335jQI/!\\314\\275\\277(\\265p\\331\\252\\276\\212A\\247\\265{\\342\\370\\021h3\\374\\235*\\031z\\217\\307\\312\\337O\\201\\263\\242?\\363\\235:q\\013\\030\\262\\321\\023-\\371\\336\\267f\\334\\226\\334\\305\\251\\210\\302 :\\256\\256s,xNe\\322|\\254\\246\\247]\\317\\270\\355\\361\\221\\260\\315\\023K\\341\\207\\365aZ\\270\\246\\227\\251\\320D\\236>\\270\\303\\240j\\036\\317%\\366R\\022\\267\\230\\327\\321\\210\\253J\\321\\200\\200\\270B\\023i\\365\\377;\\233JZ\\356{\\233\\006\\221\\254cW\\377e}\\031k\\313_\\312\\014\\225@\\341\\257e\\316\\236P[\\300\\311\\3420\\327\\331\\342J\\373\\350\\241\\374\\357\\014f\\025U\\375\\355\\274"4\\377\\010\\33391\\223\\233\\242\\317\\350*\\320\\203\\034\\237YD\\2017\\341\\211\\342\\024\\234\\0201\\272\\3548\\262\\334\\351e\\335\\20409\\226\\354\\005\\377\\013\\002?\\212\\362\\007L\\355\\026'8\\316\\223Gtle\\0223\\346;r\\343o}\\364\\235\\335\\3449\\313\\\\{<\\307\\215QXm\\021\\\\s\\027Q\\344\\004\\3028\\327p\\027\\274\\2206cY\\264\\026\\330\\225\\311\\002\\367\\321\\334\\316\\021[)\\011\\353\\327\\177\\273\\353\\026=\\356\\356\\231\\010!\\262\\247[Cvy\\007\\250\\332\\356f\\267\\237\\330\\327\\357\\224\\023\\021<\\2243\\002~\\005\\255\\004\\232\\334+\\322\\261\\012\\354\\244[\\031\\347\\241\\320\\217\\301)\\233\\307\\026$.\\0063\\2434\\373g^<\\231k\\337\\335\\370\\216\\377TO8}9\\201\\217\\374\\275\\240ox\\217\\365\\011V\\313\\231u\\357\\300\\206$\\206\\353\\242\\301\\026~\\212\\014{\\352\\264\\002\\254>\\232\\203\\204:c\\335\\336\\277\\314\\376\\336\\330\\224\\247\\263\\010%?lH\\025_?.7O\\343\\206Mbj\\220\\324\\327\\247\\203\\200Dqp=\\3156\\215\\226\\364\\005[\\242yvw\\037p\\251n\\263\\210\\177j0\\206*^\\315\\271\\312\\303\\323ou\\023\\007\\224\\305J\\207\\216h\\232\\177\\204\\007X\\214\\205I\\021;\\272\\0166\\264\\0121\\271\\012\\346u\\265O\\357\\206\\204\\010\\030M\\321>B)I\\226\\377\\017\\247TT\\347V\\225\\032\\031\\235G\\017\\250joO1\\177\\036w\\361\\326\\177@\\220\\013\\375\\200i%X\\357\\210\\270W\\252\\302f\\362\\361\\373\\320\\357O\\364\\365\\363\\1779\\233\\215Ee\\016]\\246X\\014\\307r\\271\\237evy\\322\\021\\272\\2442\\341\\207\\301\\267\\252\\034W\\220\\273\\260?\\302YS\\012[\\316\\217|h\\245\\240h\\252\\335\\304\\033\\323\\223\\257*\\013\\232\\276\\271H\\370\\217\\027\\361\\251\\320\\362\\277Q2S\\275\\021\\231\\245\\301\\240H\\017{3D<\\257\\264I\\377\\277%\\015\\326F\\341"\\357&\\014\\221\\257\\212\\007H\\370  \\237\\372\\230\\001\\247\\356\\300\\214\\207\\213l\\265\\355\\261]-\\201\\221\\213\\274\\223R\\364\\\\[.\\331pJ~\\202\\315\\223\\256j\\211\\252\\232DbX\\030\\357E\\017\\276\\202*/QXWW\\006\\244\\360\\005\\230\\207\\014\\246/\\352(\\312S\\032?\\355B\\240&c\\336VFh\\207\\266\\177A9\\344\\267\\343?\\370Jn\\224\\010\\330\\236\\300\\230\\367\\263\\021\\225EW\\304\\207l~E\\206\\266\\000\\375\\2701\\324\\252Ph>\\262\\322\\263\\002vn\\312\\370\\034\\370\\237\\305\\252E|\\020\\347#\\344\\347\\026_\\261><\\025\\254b`*\\320\\275\\366\\023\\320%\\325\\312\\223x\\304.\\201\\177\\326h,\\021\\032\\260\\257\\017\\017\\215\\214\\216\\246Z\\234\\362\\321\\300\\264\\213\\007\\020\\222\\330PL[\\364o\\006Mj\\033\\332\\247\\343\\312RdU1\\217\\261\\033'\\207\\236=~\\262\\216\\206TL}\\303w\\251Z\\341\\020[\\204\\366%]\\270RvL\\177\\340\\207\\353Y\\252\\334\\270$\\377\\375\\227\\210:\\321\\253\\370\\210\\212\\344\\035\\376\\224M'\\207\\361v\\310\\366\\207\\354\\\\\\352\\354\\325\\015\\274\\242\\356\\260h\\274\\334\\350\\361\\246{J\\007\\223a\\336\\233\\026\\022\\261\\261\\224\\022\\225d\\363\\330\\030\\343\\241\\203V\\361b&\\273\\002u\\273T\\013\\206=\\347\\224S\\344we\\215\\305\\236\\023\\354rcq\\323\\376\\226\\257\\212\\264D\\351\\010\\245\\270~\\265\\327dk\\255\\323\\227\\333e\\264\\012\\212\\373Uu\\204\\315\\270Z\\020fzNL\\275\\315.xgl\\326\\375(\\011\\326f\\376$\\335\\336\\205[0Jn\\270\\330\\011\\260\\254\\320\\302\\030\\377\\011\\203q\\353\\030pD/\\340\\177\\370W\\031y\\302Z>\\027$\\225H\\030\\274Zl\\015\\335\\350\\241\\201\\016\\023\\377\\336\\001\\021\\\\bg\\002\\322\\220\\035s\\250\\320\\263Q\\004"\\323\\016\\326\\273c\\264\\374\\301\\325\\264\\272$\\322\\207\\311\\216\\263zx~\\345(\\252\\361\\366\\257\\353\\016|\\331\\033l\\021\\354\\366\\210\\340\\215\\344_o=\\202\\372e\\245X\\271_\\276\\362rA\\234v\\2646\\276\\341\\204\\203P\\013\\\\\\343\\247\\214Z\\320v\\201\\323\\377\\017\\367\\011\\310\\001\\203/\\347\\007u\\237e\\306D\\332\\245\\275\\370e\\2355\\036\\333:\\370\\177x^\\211j\\252\\341\\251\\343\\243\\334\\025\\035\\256!\\334OS6%$b\\241\\277_7N\\267\\227q\\212\\011\\264rcGw\\201\\260%\\275\\001\\020\\021\\000\\325\\217\\366\\371\\024$\\267)\\211\\313\\247O\\252-\\254\\345\\341\\003\\262\\270\\333y\\262\\005\\335=\\340\\007B\\257\\217\\217\\016\\333\\373gn\\007=H\\222\\254\\261\\004\\211,St\\006\\310\\302wu8\\232\\202\\274\\0058\\361\\206\\222L^\\026\\243\\300#<\\367ec\\340\\216I\\012du\\300\\317\\256\\365\\324\\243l\\\\9\\341\\311\\270\\377\\363\\211\\202\\264H\\0133\\016W\\315\\342\\353L\\2107\\306A\\327\\177\\341R\\204\\364\\2620\\252"\\343\\321\\034P\\3715\\006@\\227j%%\\204\\027\\205\\205D\\032\\217\\224o\\005\\027\\210p'\\346\\216\\037\\262S\\243\\365\\355\\373\\2139\\0116\\377\\255P3\\247\\0128\\232y\\311\\231\\343\\250I\\270\\247\\000\\223\\325k\\302n\\001\\031\\201\\366_Egc\\030N\\254\\205\\300\\201\\015\\256\\330FX\\005t\\336\\005\\177\\372\\241\\245\\350\\022T-J\\216#\\353A\\352\\027\\300\\237\\024\\372"\\302b\\372\\354\\344\\022\\342\\331\\322\\327\\267\\246\\343\\343S\\301\\256\\373"\\016\\277B\\003\\252\\332s\\374\\272GL\\305\\037J\\033d\\260@\\036Z\\364\\306\\2000\\017\\316\\241\\303\\374\\214\\007\\261\\320\\217\\246\\235\\244,(\\362M\\017P\\037\\024\\244\\350\\354\\001\\314\\015]+~$\\352\\260\\241i\\265\\350\\235\\234\\1779\\250\\364v\\246\\205\\276\\206\\273*\\203`\\364\\237\\2276\\355\\005\\322\\\\\\373p&B\\242!\\275\\232\\010B6j\\020\\321\\337\\355!\\321\\361\\334\\271\\365g\\245\\346\\334\\215\\302\\263\\327\\236\\2226!~_\\014n{i\\335\\224\\317\\243\\330\\306]\\245\\253e\\247\\333\\3559 \\034A\\220\\217U'\\340I\\3130\\0075\\014\\260\\276m\\202\\360\\261ciI\\340{\\000\\212\\031\\377T\\177Qv\\230I\\243G\\265\\370\\246\\303\\314\\333\\374\\257kJ\\336"\\362P\\337\\214\\223\\016c<X`\\312\\323y\\306_\\316\\200\\246\\037&\\345\\203O/f\\231\\234{x\\366\\215\\314y\\330\\244\\200\\005\\205[5\\337Vn+\\363t\\0115Z}\\024\\011\\263\\370\\345j\\375%\\025\\273b\\335\\315i\\353\\342\\324g{\\367\\3279\\014]\\353\\335V\\303\\325Tv\\026\\224B\\376WY\\336\\225\\316\\215\\324`\\016\\264\\211s\\327s\\367;\\243)Z[\\270y\\336MB\\366\\313AV\\270\\346?\\217Rg\\002\\237XS\\034R\\277\\003B\\256kF\\254K\\364\\233p\\203 \\375\\235\\245\\374)_\\215\\005\\303$\\036R\\004\\\\1\\343)\\027\\231l\\007\\270<\\265sAv(\\242\\231\\277z\\244\\341\\366\\264\\270\\256\\022B\\011\\346\\314w\\366Q\\306\\372'\\354@\\323\\311\\347f\\255\\023\\374\\006\\021\\222\\266=\\003\\177\\342\\317\\251\\0048\\002\\340\\227\\023\\375w\\230_AH{'^T\\2545s\\001}_\\223\\251\\237\\302E\\340\\347\\243)\\272\\332>\\204\\373\\014\\320W\\206R\\303\\024\\301x5\\013h5\\312\\252#\\234=\\210\\344\\\\;3n]%2\\342\\226,*\\312\\236\\371Q>\\363\\367Y\\327b\\202s\\002\\003\\005\\371\\0050\\277\\277\\372\\177B\\244\\323\\326\\207\\225\\030\\312\\215(\\221G\\014\\367\\203\\265 \\366<[\\355\\372\\342S\\364\\221\\370\\372\\372\\203\\251\\032t\\202Sf\\320=b\\3279Z\\221\\277QZ6\\035\\364\\273\\263\\365\\027O\\222\\264\\234I\\331r\\373Du\\353(\\017\\026\\232\\035\\220\\254\\3079\\222\\230\\244\\212\\0270\\262\\232\\236\\346A-\\\\S\\212\\000#\\352\\007\\350\\023\\241\\257\\234c\\323P\\344\\264"\\313\\356/d\\017\\005\\031\\001\\362\\333\\0103\\346\\373\\201\\315:\\264I\\267E)\\242f/Ree\\3346<\\211`\\024\\236\\325\\311]\\334\\242\\203_p\\376\\007\\010K\\346\\011K\\202Y\\367A\\201\\371\\252F\\223k5n\\011\\201u\\035\\255)\\270\\341\\224\\020<\\242\\351t\\201\\007\\274\\317\\035\\021\\346]\\244\\361\\354\\200\\377\\347\\203\\320=\\264br\\331\\374uA\\006yvw[xk\\274\\211\\023ZNF\\2258\\241\\306\\304\\317Mj\\243e;\\265\\325d\\360,l\\345\\0379\\216\\364C\\014\\337KN\\364\\276\\253\\001\\321r0\\350\\321N\\243w\\224\\317\\323\\347\\244\\015D\\200\\356\\022\\277i\\021\\313O\\370\\301\\227\\214u\\022\\322\\034\\265\\362ri\\026:\\273\\271\\027\\003\\006\\314\\335z\\255\\3234\\030\\236\\264%\\253\\373\\244\\266\\331\\231\\005\\233\\232\\367\\354\\245\\216\\344\\010\\363\\251*\\003\\265$=o\\355\\0044#5xE\\224\\300j\\357w\\226\\344\\217\\267UM\\277\\\\i\\372-\\265u4<\\005\\217\\0136\\234\\255Mu\\352\\332h\\013\\351\\234\\372BU\\035?\\236\\263p\\256\\004\\222t\\012\\340w\\335\\301\\372a\\002t\\301\\302"Sq\\301\\235;\\275V\\365\\374\\272\\204r,\\354\\267\\256Y\\270E\\221\\247\\342\\3052!\\277oL\\020\\254j!\\335\\3233\\271\\032o\\240\\034\\276O\\207\\026\\364|\\220\\303\\016\\220vzX\\276M\\234\\347-e\\303\\211t\\255\\353\\3066\\346`\\035\\247\\226H8\\314\\226)^\\233)C\\220s\\223\\272\\203\\250\\304\\032\\336\\224\\246\\207|Qk\\252\\223!r\\375\\345[\\270W\\032\\227\\364Yc_\\236\\001\\277\\3522)\\001\\214\\266\\263\\3029U\\237w\\237\\303\\261(\\3222\\246\\252\\3020\\361B\\320\\224\\267)\\217\\264\\353\\362\\363\\373@X)\\244\\230X\\002\\374\\310\\245\\271Pf\\200m\\304\\011fC\\276\\357\\236\\227\\267\\310N\\216\\236\\267[\\240\\263\\305\\007c\\216\\002\\257s64\\237x\\\\\\317\\023\\353\\355\\204.\\226\\032\\016C\\012;p\\263i\\307GnyJ\\263\\272\\210-m\\204\\016*\\272t\\341\\224\\177\\375\\313\\314\\352\\\\\\201\\326\\003w\\004\\375\\316\\244&|\\240\\222S\\334\\266\\\\\\346\\355\\\\\\232\\217#z\\007\\035k\\364\\365\\315\\236\\343\\007\\232D\\307\\323\\230\\367\\312\\331\\267\\027\\322/\\353\\323"\\236\\021\\337~U\\302\\3379\\327>c\\346AN1\\361\\367\\024\\360\\304\\206G\\025\\033\\337\\316Lu\\237u\\013\\317\\024\\331\\361\\217\\370I\\215\\375G\\3555Dr~\\323K\\3335x\\017r\\315u\\206\\021\\005\\374F\\304\\267y\\254C_\\347N\\262\\271\\363\\012Z\\324\\036\\271\\024\\224\\323\\236\\361@\\261q\\273L\\014#\\356kk,\\303\\224\\252Bd\\010\\324\\020\\323R\\244l\\220\\340\\3627d\\332\\032\\277\\\\\\007\\\\\\035y\\336\\260\\235#\\002Ml\\340u\\244\\377\\227\\355vN\\240a\\307\\016r9\\014\\236\\3765\\350\\004\\324 \\2731ks\\323\\307\\200\\351\\001\\334%\\265\\304:v\\037(\\225K\\035\\220v\\021s\\366\\2711Sf/\\011t\\230\\300x6\\251 \\315;\\334_\\270F\\030\\333\\232\\244\\345|\\215I<H\\324\\02299\\006\\261\\331\\035\\274;\\2268\\206 \\345\\360\\316\\021\\217T\\266\\363\\36644\\204\\233\\225\\215\\011\\346\\227\\006\\340\\316\\0249\\373\\027\\220\\212\\250G\\226\\240\\216\\250DW\\357\\360U\\361L\\347\\254\\375\\212\\346Z\\362\\223\\276` \\000.\\375\\235\\003\\263\\273\\200\\376\\235\\274m\\275^M\\301\\307\\353$\\224\\201_\\005(\\265\\237)0\\235\\300\\177\\021\\2044"\\011\\316\\217(9\\252\\213@Q\\313N\\314q\\225I\\277\\347\\002<\\001\\3534\\1774]\\225^\\333\\221\\254\\027\\000\\350\\224\\212\\204\\032\\342\\251\\022 )n\\370\\177\\213\\273Y\\264\\204\\261\\246v\\304\\231\\225\\372\\177\\335=U\\366\\027\\205z\\271\\243W\\261\\012BE\\330B\\357\\001M\\307\\025=d\\311c\\253<\\031:F*\\007\\311\\354\\037\\316\\212\\32183\\330K\\221\\360}\\310xZ\\021||\\255\\003P\\301T-\\011\\023a\\203\\002\\266d\\253\\007\\207\\354g\\235c\\240h)\\016\\265\\232Q\\012\\230|\\016\\032'H!*\\212\\335\\254\\242\\227\\326\\\\\\004\\255\\254\\306O\\034\\261sD\\247F\\212#\\345Il\\004R\\201\\200W\\356\\273R`.\\351^;\\220eZ\\213\\345\\032\\021\\271\\300u\\337\\373GE\\225wMH\\013l\\320>\\023\\372k>\\256\\210\\004\\243n-\\335\\360I\\345\\356$\\2026\\274\\314V\\003\\375j\\234\\347\\015\\264e\\264\\330{\\311\\354\\026\\013\\223J\\243\\255bz\\031\\024\\\\\\002\\231J\\246\\320#\\330q\\371\\215uQ\\367\\2209\\245\\025g\\270\\276\\253\\003\\220\\372\\205\\217\\274\\305\\215-.\\3209\\325\\333\\020<[\\2213\\353\\267\\2451\\331\\325h\\015\\371,\\227N\\334UZ1\\266\\343\\3775\\325\\342d\\032YT\\335jCJo\\347\\253\\375\\204\\201\\014\\261\\266R(w\\234\\370w\\227\\202{\\255\\366\\304\\007{\\003\\301\\013\\271\\177\\244\\215\\204b\\261\\234B\\377g&\\203\\243\\376;-\\271\\220\\220\\326\\354\\211\\\\\\227\\335\\247\\206\\334\\337\\365\\251\\340#,\\200u\\265\\240\\007\\271k\\2762\\000\\017\\230\\217\\3505Z\\374)\\350\\325\\336T\\326\\361\\362u\\365\\370\\256\\374\\216\\221\\015\\013s\\344\\311\\322\\037t\\346\\265\\314\\373O3\\201\\265f\\234\\256\\342\\012\\275\\354\\213)l]1Y\\224_\\260\\267\\323,\\245\\232\\336\\240'A\\252\\261\\215\\200\\373\\334\\360\\266~\\324\\220\\241O\\377\\342\\014\\223p\\2422$#*\\246Ed\\312\\356\\254\\223\\013\\251\\3271\\236\\232\\231\\256C\\217\\254\\037\\3679K9bDl\\000\\355)\\345\\031e]O\\250\\274\\003\\347\\177j\\012\\324\\225\\013F\\306\\031\\331Y\\364\\211\\314h\\321\\332n\\224\\025\\375\\3164f&\\016.\\265\\3068\\0246\\262\\371o\\034\\010G\\\\#\\207\\232!\\205\\304w\\222*\\341\\277yU)\\267*\\200>\\\\lp"@h\\377\\337\\012\\016LIxz)\\223\\003&\\0162\\337\\322\\210u\\244\\217\\224Lb\\262W\\206\\210\\205Z\\027\\001\\364R9\\343\\274\\246\\301DG1\\200z'\\240%\\360\\000\\3613\\006\\215\\000\\254d\\251\\234\\327\\221\\313\\240<\\300-eS\\014\\325\\020\\276\\272yb\\370H\\371W\\222\\241)\\010E\\311\\363\\340\\346B9\\202\\\\H\\026\\316\\265\\224\\004\\327|0\\007\\326\\3406\\177\\255\\321\\212o\\035Df\\244\\222dos\\007\\005x\\270y\\313\\363\\263\\3166\\247\\210=\\226\\237|R\\266c\\016\\011\\203C\\277\\220\\2003Yp?@\\013\\\\\\233'\\275P\\232\\270YT\\252\\345W}wN\\231U\\250\\205\\361;\\3354$(uB\\351\\247z\\277\\025\\033zTs[\\371\\2453\\261\\355\\370\\327\\327\\342\\212\\316\\240\\307\\240?\\372\\301\\352=\\307|4/\\234\\346>\\373b\\027\\311MeM\\361\\032,K\\207\\350\\006\\212\\354\\315\\343\\357\\334\\362\\005KL\\374\\312\\242\\250\\374\\345\\305\\177\\332\\246\\225\\317\\300K\\344s\\314i\\330k>\\\\W\\326[\\213Gr~N~\\254q\\3730\\307O\\210\\240\\343-``)\\264\\266\\000v\\266+y>k<s\\343\\327RA\\035+\\320\\017q&taTUi\\364\\356\\265\\2664*Nx\\237\\272\\240\\343\\337\\315#\\337"\\356\\022>\\277_\\210C\\037kRE+\\254\\236\\206\\312\\316\\036\\276\\325\\244T#\\337d\\360\\027\\2127\\240\\0040\\244v\\226\\201xV\\322\\327\\363^\\335\\357\\213\\333\\340\\037\\257%Qj\\267\\222k\\223\\005\\333E\\322\\217\\242q\\265\\034A\\201\\320>12<\\0247 WF\\257\\327\\305\\036P>\\237\\316jk\\254\\320\\246\\320{5B\\226\\027\\007L^\\022\\251d^\\265\\254\\250v\\356\\241i\\226x\\236\\363s[\\275\\340D\\227\\274\\030\\234uDGe(\\321~~#t;\\360\\202*gB\\340\\344/|\\354+[\\377g@J\\304\\333\\267\\355jO\\036/!\\372\\304\\005\\355^\\202\\212\\252\\261|\\270/\\304\\2619\\331V\\015\\252+\\214\\006\\260,\\276\\212\\033V\\231\\324\\224\\177EV\\230\\362\\220\\201\\251\\251\\200[o\\250,ze\\353u\\371\\012}\\367\\201\\034\\252\\203\\247\\206\\360\\316)\\307\\272\\357:$\\220\\323\\016(kL\\377f^\\333\\022W\\030u[\\026Y\\342\\302\\306\\351~=N?\\351\\357\\222\\012\\007\\214\\313\\002\\235\\351~z\\275b\\026L\\022\\323(\\333\\340\\356\\311\\206\\351\\202\\370\\024Jz~\\332\\367\\212a\\330D\\012\\377\\242\\251\\246\\036$\\236~\\225E1L\\317\\303B\\224\\266\\342\\361\\376m\\234*L\\010$\\327V\\006L\\334\\322b.\\260l\\223\\020\\371\\241\\316\\375\\007$\\231D\\230s<cVLy\\361\\015\\271\\035\\243\\364P\\177=\\363@\\323\\256\\322\\011kuu\\256-\\200\\326{!*\\032\\211\\332\\361\\003\\000\\033>\\304\\354\\370\\347\\264\\335\\243y\\015\\316\\265(\\015\\003\\012\\267\\320\\376\\306\\017\\346\\235\\370\\374\\367\\362\\225\\212\\242\\242\\2450b\\335,\\236\\006\\361\\343\\3019\\036\\263i\\006\\01247\\203=a\\2552\\240\\204\\216\\207\\340\\037\\356n\\024\\373T\\323\\347\\025\\260L]_]W\\355\\200\\216\\353j\\317k\\227h\\223;S\\0221\\264\\226\\335f\\254:\\307\\253\\356\\006\\244\\307*\\357p:\\205\\270\\026\\035\\367B\\264\\364\\272T\\0173"\\323\\037\\3248\\333\\242\\005$\\225og\\241\\231\\017\\035U]\\324\\014\\360F\\364\\200\\020"\\305Ia\\333\\363\\354T\\000\\306\\306\\355U\\217\\032@=\\233x$\\262\\276l\\353\\325.?\\327\\321Wj\\253\\011\\274|\\365\\010)N}\\221\\244\\341\\002"/b\\206\\247\\001\\325Y\\223C0/@Pz\\026\\216\\337I&\\345\\026\\224\\354\\243J\\315o\\000\\020\\013\\\\\\211[\\232wc\\231\\311\\001\\343xfM\\261K.r\\234"\\211]\\355U\\014a\\363c\\2679%\\245>`X\\220\\271\\243\\330\\024\\316/Z\\0012\\262\\334\\013Th\\371\\274\\3220\\363\\032\\033P>\\241\\033\\3616fWkv\\307\\271\\372\\306|\\376\\371{\\0219\\327\\274@\\200w\\267\\\\|s#\\274^5\\320.\\253\\224c\\371\\307\\315\\317\\206\\366\\363\\351\\360\\017\\227\\212\\322\\325\\012iF\\352\\276P\\241@\\376\\034w\\202\\343\\317\\363y\\336\\222p\\331\\246\\225Mn1\\204a\\266X\\017\\205c\\266\\322\\254\\251\\237G\\022\\036\\033\\322{] 9y\\231`3\\212}f\\322\\207Q\\235|\\005\\332\\323*\\220\\013\\316^[\\341\\276\\364\\271>\\277\\312\\021\\027\\022\\012\\006\\375\\210\\202\\014\\276\\213\\320\\027\\321\\235\\030\\222\\202\\177:O\\335\\350\\004\\264]\\342D\\331\\347^\\312\\271\\273'c;.\\014\\370\\2329N\\272C\\013\\346\\223\\013\\230\\031\\200\\253\\302-\\250N\\024$<;j^)\\307\\325\\271i\\273\\037\\002en\\354e\\312T\\366\\224\\321n\\367\\\\ \\024\\241\\274\\005\\024AX]K[\\332\\033\\324\\252p\\241q\\346\\263\\254D\\227\\3216q\\240\\260-\\242I\\024\\3356\\236\\343zY\\003\\017\\355\\3474\\304\\010K\\300p\\007\\322\\321_\\344\\370|4L*\\030hq\\004\\376\\020\\346\\250\\266/r\\345\\222\\003\\177v\\365\\012\\314\\032\\002\\255}V{^\\362wg\\262\\246\\032X+\\317\\011\\360\\363^q\\323\\204Rc\\353\\330\\341\\231\\200\\247|\\255\\376\\032\\271\\370\\264pY\\213\\024\\234\\2549\\306\\007\\266\\251\\\\\\016\\370\\024!\\315;0<\\030\\312E\\325B D\\232;l\\274\\231\\340\\224\\272\\022b\\240\\002-\\270\\311\\331r\\375\\213\\273\\002\\3365\\356\\352\\217\\336\\264\\025\\234\\241k\\317\\313\\021\\014^\\250o\\372[\\234\\212\\375\\311KY\\257\\016my\\027\\372p\\021\\372\\211Oi\\355[\\325V\\001\\252|m\\245l\\2138\\232\\035QXW!\\370m\\260O\\315\\025\\207\\2265[\\340\\026\\227\\211\\347\\307\\366\\346\\007\\024\\351,\\234\\350%\\310_\\350\\034\\024\\254C\\011M\\345\\273\\255T\\014B\\365*}S*5\\204\\240\\013\\220\\361k\\336\\343\\317iRqp\\313VP!\\327\\337y\\0069\\203\\231\\205\\313,\\037\\245\\241\\016\\267 \\326\\362YXe\\004g\\205\\307\\013@{\\261\\374\\301U9p\\263J[\\216\\325D\\252\\371\\232\\237\\275\\244=\\256&\\273\\010H7I@a\\215\\341\\300\\234\\346\\332\\322\\010{~r\\036\\316\\004\\234\\331\\206\\373?\\004w\\261\\002\\316\\370\\211\\335\\3432T\\365\\367\\327\\261S\\212\\252,\\374\\275\\336av\\340B\\022\\325i]\\213\\367\\014\\211T\\304\\351S\\275\\225\\367\\306\\\\\\245bU\\2737M\\177\\362\\022`\\372D\\363\\361\\311U\\2029\\326\\016\\202\\340\\273\\332M\\360\\334\\262\\001A\\262\\252ew\\2006\\210t\\226\\251]K\\212\\334iGHl\\237S\\215~O\\324\\214\\312\\012^O\\274&\\266\\3143J\\216\\301~\\3457Am\\265\\353\\310\\034U\\343\\023SA\\026%\\372\\036\\353\\242\\036*Y\\225u\\311\\177\\006,p\\311^\\241\\353\\223\\307G\\325\\025\\302\\354$\\337\\320\\036\\014+\\025\\30012\\303\\265\\323\\316\\341,%\\271L_\\270\\034\\200b8\\177r\\031\\224\\307A~\\330\\250\\356\\372\\373\\310\\370\\251\\036\\377\\261)\\216\\311K\\035M\\222\\334\\333T\\343\\015\\2749\\320\\377\\254\\253b\\214o\\025(\\301n\\225\\216D\\302\\360\\037E\\030(\\215\\323\\272\\266\\253\\3607\\332\\206\\353%\\344\\015\\363\\022\\373\\263p#\\326u\\226&c\\3357;\\355v\\037\\001t\\031\\011C\\203\\000\\007\\236.\\227"\\305\\267\\237\\234\\226\\273aan\\355]&l\\012Q\\256K.\\376;\\006@\\274ue\\367\\305%G]\\244r6}\\022\\350\\200;_*\\001\\177\\014\\377\\020S\\037\\026\\020\\341zU\\241\\241J\\344\\215\\266\\302\\243\\342\\364\\265!\\014\\011>\\314o\\020\\237\\037\\230\\327\\337S.\\242\\275\\231\\3025\\313{\\277\\255\\014\\335\\217\\254\\272\\223rT\\212\\233|\\224\\002\\236BXN\\036\\277\\350(kow\\267\\320\\214/\\306!yz\\214\\177\\014\\302\\253qw\\004\\212\\036L\\015\\33552w\\206i\\237#\\212\\370?\\324\\265\\002U\\244L\\006\\232\\333\\311\\013L\\261\\262\\347;\\270\\015=@\\266\\322\\002 \\341\\367\\011\\333\\210J\\343\\000\\333\\267\\204\\206\\242D\\013x^x\\323\\364q\\265/\\363\\023\\\\\\260\\201\\376\\037&T\\260\\311\\251\\217\\024T^k\\006`\\247U\\345\\365\\247\\214\\241\\247\\270\\332)\\343F\\315\\014\\014\\265\\007\\316\\303\\224\\320&\\226\\013b\\017\\267\\015\\244\\257\\366\\007y\\2768\\341v\\266\\366\\244}\\267\\270\\301\\0159\\345L\\346\\324\\355/\\313M\\302$f\\214\\312\\223%MQ\\230\\333\\365?3Y\\033\\0238Jym\\215O\\021i\\200\\273k\\224]\\021\\247\\264\\012\\336\\342\\372\\215\\300\\201\\3740c\\031\\275\\003A\\361r\\307\\243 6\\360}\\263`\\304\\010W\\036&\\254\\224\\335$\\013\\331\\202\\250\\031\\350\\016yC\\000\\226\\270%:}\\033z{\\342\\256\\366\\277\\340\\312)$y\\010P*I\\236\\215\\201\\003\\373\\257\\225\\205y\\032\\332k\\273\\326\\252$\\032\\250\\226\\203\\241\\322bgC!\\217W\\1778\\340;G\\320z\\352\\306\\016\\245\\330\\322\\305\\214"'\\321_\\346\\213\\204=\\001S\\227P\\326\\332\\364\\006\\240\\223\\230\\216X\\362.\\017\\251'v\\032Jh(\\320\\367\\344\\364\\334b.\\336:\\363\\026\\276\\010\\212Y\\236(\\206HN\\362\\2709k\\271\\276\\315\\232\\361\\320B`\\241\\262%\\232gW\\2157m\\350\\033\\011\\313\\211G\\324\\2100K\\347}rW\\006\\177\\221-D\\253\\214\\004WP\\027\\036)\\230\\337\\363=-|)Du~\\013\\220+\\353\\021\\270\\334\\256\\015}\\033\\035\\232\\225\\003\\241dE\\273\\303\\372\\251\\014c$\\370\\226}\\026\\212D\\277\\221~\\247\\223)\\270\\207\\216\\230\\3629\\242S\\236\\204\\205Z\\000QB\\355\\367HF\\033\\037\\354\\224\\337\\015<\\223\\363\\361\\215Di\\252u&`4\\263\\340Nn\\331\\341\\237\\364\\273:\\336\\032\\\\\\035t\\204\\23138Em\\361\\345J\\261\\012k{\\241\\352\\316\\214\\277\\000\\361\\371/\\325{!\\355\\253 \\016c/v\\002\\362C\\250X'[[;\\273a3\\265#\\341\\365vP\\340\\337\\205)|S9\\016\\346Q/R(\\250\\253\\374I\\345$\\263)\\210ef\\267\\244\\306\\006^\\226\\266(\\021\\260\\271W1\\213\\015\\252T\\023\\240/\\210\\222\\327/\\341\\332E\\223J\\024\\236\\344y\\262c'\\033\\010\\320\\336a\\277K\\335\\376\\351)\\326\\346\\022=\\353%\\242'\\200\\026.P\\204\\231=\\022\\3740@\\237H\\362y35PU\\006\\232\\202\\035*\\203\\257YY1v\\314\\354{\\316\\345\\327\\306\\375\\263)0\\301F\\211\\030\\012\\345t.\\305\\312\\273\\231&\\256\\300\\261j\\326\\314QOn\\001\\376K@\\243\\240\\227\\336\\364\\013'j\\0138\\357\\244A\\277\\340\\227\\241\\251\\2607\\343QO\\014\\177\\345\\300l\\315\\307L]\\366\\313=3v\\220\\200\\015\\335\\234\\363\\033[\\220\\217\\264\\020\\225\\352\\330%\\350\\231\\312\\210\\340\\261\\035\\366\\312\\343\\236y>\\030V<\\351\\364\\223\\027U\\345+D\\333:\\367\\313\\361=\\235\\023cg\\270\\035*\\242)L1\\300\\233\\252?\\231\\014\\211\\370\\376j\\006\\021L\\013\\273%\\221\\006C\\323\\325\\301j(\\004\\365\\320\\300K\\251L\\215MM\\205\\215\\260H\\244t\\342\\004\\027\\244\\217\\033\\250\\365\\264\\325BZ\\255\\002:+]O\\210\\211n \\346L7\\356\\355G~\\011\\236\\365N\\022\\332b\\277b\\255\\243u\\202\\215\\276qK\\023;\\241\\024]\\371\\361Pw^\\3124ph\\030\\025\\254\\366\\212M\\223\\342x@E\\201\\256\\027\\036\\320\\265/\\004^\\345Z\\301\\377\\252#\\003l\\332\\305\\324k\\003cR\\377+[T\\356\\272\\300\\215\\357W\\222L\\256\\351\\272\\244\\336\\374\\325X$\\340\\242'\\035k+\\360<1\\016\\2178\\271gIjV\\237S4Mj\\270\\255\\2735\\215r\\214\\323"\\370\\376\\334\\305\\010]\\372\\34092%0~\\221,F\\371z\\\\>\\377\\231\\217\\027L\\017\\177c\\010\\263\\356,\\377\\371\\324\\001!h\\363@\\376\\001\\331\\356\\305dd\\362S%\\340\\011Rx\\347\\334\\027\\337\\346\\243VT\\257\\017|\\201n+\\333\\021)ZZdP\\377ek\\327\\203}UvQAj<\\340F\\266\\347\\261#\\323m\\202\\337\\004\\247\\222hW\\340\\352\\302\\226Qmo\\270P\\366AO\\261\\370\\366\\217\\333\\262-d\\217'\\266_oW\\337Q\\226"n9\\026\\016\\327\\2675\\261\\342\\244\\014e#\\321N#\\023P\\262\\342\\273R\\211Z\\221EP\\374\\012\\364\\253\\342J\\010\\314\\257\\010(F\\316\\353\\331\\321T\\210;\\236\\030'\\000\\261\\022\\013\\210\\255+\\252\\3513\\257\\322\\233\\216\\323~\\177\\212\\005\\261\\265as&(\\014\\273\\250~c\\015t\\215\\334\\265\\012m\\360\\370\\272\\330\\211\\333y\\201z2\\003\\025\\003s\\351K\\271m\\374\\213[m\\025\\020\\315\\257\\211C`\\320f\\335\\020b\\251o\\316\\252,O'\\316\\023T\\300\\371\\012l\\030\\200\\341V\\362*9\\357\\366\\255\\240g\\357\\031q\\002\\225W\\252\\214&\\262xe30l\\277\\352\\337\\000\\223\\241F\\246>\\366e\\224\\372\\314\\325\\016\\244U /f\\033\\311B\\257\\243\\265G\\363\\273\\322\\200p\\365$\\352\\021\\274-aw\\261\\350W\\3304\\0356\\250\\004\\327\\224?\\030\\230\\177=<\\342\\015\\372\\331!+&\\361\\314\\242\\300A\\305\\331z\\236!\\332\\202k\\366\\026M\\201Z?\\341\\366\\240\\337\\376\\323Zq\\355\\350\\313D\\331\\250\\230vo{?\\370\\011\\002:\\364CP\\013,\\321\\376\\346\\002 \\353?\\311O\\220\\342\\233\\215\\362\\025\\027I\\252\\202\\210\\022\\305\\243&\\334g\\037\\034\\231\\275\\217\\272"\\036\\331\\023\\3515Z*\\364\\313\\260\\024\\270\\310/\\267+\\0009\\223\\001\\024\\377j\\017\\316\\032x\\361\\\\\\267=R\\0255\\317\\3710\\336\\324\\210\\353D{\\013\\351k1v\\235\\312-\\305\\310p\\244\\311\\351\\005\\211E7?6\\020\\353\\316\\346Q:\\011\\2170\\002P\\223\\275N\\333\\373}\\230\\356v\\242r&2z6\\037\\017\\274=\\320\\022\\033<\\335\\007%]pp\\344%\\021\\307\\017\\2363\\323\\227\\025\\0004\\245\\253\\270\\270G\\300N=\\345\\342\\332U\\315[\\315-\\247\\276\\307\\212Mg\\215\\276\\012\\023\\266l\\354\\263\\223*\\006\\334B\\230\\211\\251\\001\\305\\203\\361\\376\\177\\337\\222\\246\\2738\\363p\\366(\\350\\254\\261\\217o\\351\\224\\307\\223\\214\\216\\235/\\3432\\216\\013\\325\\325\\343\\317\\243\\236\\331\\025N\\347a\\035\\0354 \\356ql\\362#\\306\\232\\324\\211\\357\\322\\312\\375$L'\\326\\244\\272\\275B38j\\233\\265W\\254\\271*\\023\\373!\\004\\031s\\011e\\270\\350\\015\\026\\275!\\242\\201\\307b\\020\\034\\232*\\367u\\373\\366_\\021\\020l\\312`\\336\\215\\360\\303\\342\\204\\354\\203tj00T$\\002\\264\\010.\\030\\011\\037\\271\\273\\223?\\234~\\362\\006hTRG\\250\\261|~E<\\213\\377\\007\\342\\341ea\\206\\263Vl\\260\\014\\255\\033\\353\\357\\234\\002\\265\\252U\\316\\226\\010\\274\\245"\\031\\347\\344C\\231\\322\\304\\201\\334/\\223\\252X\\324e\\326\\215\\343\\353{\\011\\336k\\036\\346\\273,\\275\\332\\203\\012\\273y\\235fz'=\\032}O6\\276\\335\\035^\\226'\\370\\236\\242\\261`O.\\240\\206|_\\326#\\034\\267\\271\\274p\\364\\007\\340qm\\360*\\205\\263+\\317p\\334\\221\\304N\\221\\020\\207&\\002\\234\\0306\\032\\373\\027X\\257\\003\\001\\202Hm\\005\\351$\\330\\320\\214\\036}\\320\\255\\304\\250tF\\002\\217x\\036U3\\307lCVR!\\301\\277\\032\\373\\343|)\\204i\\307\\364W\\203\\375\\352T4pa\\020\\\\\\307\\314\\332_\\022\\201W\\326y;\\374\\316\\333p\\322\\013A\\204Z\\252\\3768\\277\\257\\3231\\364\\251\\250\\010\\355\\320U1F\\272\\245\\342|\\004\\222\\005\\206C\\023\\252]\\252\\334\\303\\274\\256\\012\\260\\204,\\303[A\\215\\316Rd\\345p^\\232u\\266\\343\\325\\304\\027\\241\\353\\335\\020\\220\\037\\344\\202?)'\\353\\236\\267\\261\\252<\\362\\353\\375\\273\\247,\\3133\\302\\372\\377\\205\\275Z\\316\\225\\033?!\\263\\370\\326Ho\\022\\302\\322d:\\374\\303\\273\\251Q\\330m\\271\\020\\336\\276\\372Y\\233\\\\\\037E\\365\\371\\261\\264\\306\\013u\\241\\376\\361\\205\\231m\\336\\301\\315\\250\\024\\233X\\317\\016\\264\\017\\310\\326\\374\\316}K3\\372\\034aS\\360Er\\2621\\346%]\\333\\200\\312Y\\313I+,\\320\\250r4G\\2668\\256$\\177\\374n0\\363\\230\\330\\237k!\\331S8\\014=qu\\353\\010-\\011\\303\\257\\016;\\307\\373\\250\\254\\221\\203V\\327\\232\\307\\020\\012S\\321,D\\022\\342\\232\\265\\271?\\371\\201:\\332g\\034FTDtd\\325+\\007\\246|7\\015h(k\\262\\276\\356\\304i\\344\\004 {J\\324\\305\\302x\\011\\331\\225k3\\372\\341\\220.:\\266r\\012\\321]Fr\\322\\275\\206\\237J\\206\\265\\241_g\\302\\341\\365\\376\\340,\\0044\\362\\267%D5\\250\\200\\222\\345\\251\\270 \\250m\\365$\\220\\221\\033\\321&\\256\\004\\367\\242\\352*\\325A\\000\\234\\342?\\313\\004\\232\\352\\033\\230Ca\\014\\245\\206G\\372\\254{r\\343q\\246D\\262\\274',\\310\\037Co\\366f9\\262n\\3100\\316\\374Q\\325\\020\\201Z\\002\\0016\\346Q\\243P\\323\\322h\\374\\233\\227Ik\\205\\033+\\310\\376u|\\206\\360B\\306\\2762\\316C\\353\\235\\307\\343\\223\\364\\2336\\034\\201\\333\\012E-\\227\\221\\013PTc\\233\\370\\352\\261\\205\\370\\345\\244\\217\\260\\221Q\\215w\\200\\246\\002\\273r\\200,\\017\\227\\002\\214\\330\\313#a(\\320T|m\\337\\276\\252\\262\\252\\212\\037\\312>\\362\\211B\\275\\203\\375R\\301T\\210\\304\\260\\207p\\300t\\240.\\253M\\033\\011e\\267\\3215\\206\\2362.\\312H"\\366=\\022\\002\\207\\300\\350\\033\\321\\347\\310\\314\\004\\375\\351\\244\\223\\010l\\2643\\261`\\324o,\\241\\015\\236\\250\\373\\277\\010z\\013;U2\\000\\247\\215\\350\\346a\\240I\\276\\222\\306\\202\\226e\\352n\\247\\272\\367`\\232<\\331rx\\362\\037\\313\\033\\317X%#~ \\215\\361\\212\\337\\247\\265\\270m\\337\\233\\026\\366\\260\\367&\\327\\367\\205\\2610>\\206\\033z\\305\\275.\\014\\262T\\365\\363\\004\\301u-\\013\\246\\004E\\313\\270dky\\027\\000\\233\\245\\034\\251[j\\273\\330\\341dQ)\\275As\\213\\351\\020\\202\\230\\270\\200\\014-\\225;\\312ug\\200\\232]\\302'\\314\\2448\\275i\\215\\220\\327%\\264\\332\\020\\347\\344\\321\\207\\375\\237\\330G\\011i\\246\\242\\333!u\\010IC\\231&\\341-;%\\263\\257\\367\\014H2\\263)\\264R5\\334\\310gy\\324\\200P \\324\\220B\\346\\253@\\211\\023\\217\\002\\224\\330\\226\\013&\\200\\220\\363\\343\\034\\237\\222\\002\\245\\305\\241H\\021\\305\\267\\022\\321\\234\\033\\301\\217\\005\\254\\002\\215N R\\360\\222Q\\351\\007\\033_\\016\\374\\233\\317\\354_\\006\\323$m\\033t\\230K\\337\\374\\015t*\\275\\3751\\375\\2631\\265\\277$\\241\\020v!6=A\\266\\302E\\301_\\360P\\346#\\011\\307\\267\\177J}}\\373o\\217\\221\\243,\\337)On\\331\\355K`\\211\\037\\3607,\\340T\\327\\334\\201g\\263\\217i\\264K\\224\\331\\363\\317\\334!\\206\\226\\255\\300\\214\\323:\\207\\\\K\\217rL\\231)/&\\037\\231\\231\\327\\232\\243s\\256\\372n\\252N\\346i\\356p\\350I\\002\\277\\212\\374\\213\\324\\266\\210\\007\\217(\\271}\\013\\346\\017\\3474\\2316\\377$\\030\\234h\\306gi\\347`\\243A\\252\\332KgvTQ\\275\\332#\\032)\\352\\355\\305'?\\275\\343\\337\\017\\312\\277>\\354\\031\\003\\\\\\246\\021\\210G5\\256\\032\\331%s\\011*\\036+\\223\\203\\270\\032n\\247c,RF\\214g\\374\\233+\\200b\\277\\012E"%\\255\\006\\262\\227\\215H\\227\\371%sdw\\236\\244\\237&\\024`\\241\\325\\002\\213Y\\375\\234\\374\\343ZoT\\364\\223u\\344\\030\\232\\216>]\\035*`k\\273\\211\\310j\\230\\276\\015k\\311L\\012\\353\\274\\37421\\332l\\2674\\030\\373\\016\\310\\327\\024\\376\\235<\\362\\202\\223\\332eh\\330\\377w\\205\\346\\033\\250\\252\\243\\255f\\277Y\\262q\\233\\322Au;\\211\\256R\\227]5\\012\\015\\007\\203\\372\\244G>\\220K\\324\\343\\020\\257!\\256\\302\\005\\242!\\206\\353\\306\\261N/t\\201t\\336vL\\002\\012\\265y;\\310\\243\\310*\\253b\\375@-\\326l\\260VZ\\325\\355a\\336\\222\\210\\252\\371\\237\\377\\334\\355\\360l@W\\022,r\\2370t\\212\\262\\203\\213\\354Jt\\274\\024\\215\\375\\360I\\221\\272/\\267\\344\\277\\235\\244\\227\\000^]\\275\\242\\315\\273\\213\\2125ii\\242\\203\\215\\236\\033\\177;\\206\\257\\024mzXC}"\\311\\210\\243Q<T\\311^\\304Y\\242\\224q\\201\\327\\244K\\330\\255\\230b\\301\\352}*\\237\\266v\\220\\311\\315\\3430c\\011)r\\257:\\014\\014\\011\\341-\\377\\022b\\222r\\356\\3709\\012\\312;\\032!"\\027\\337ZsKS\\\\}\\256\\012\\361\\322-\\031\\021#\\032d\\220PN}I\\207\\305\\376\\022\\223N\\313\\017\\245\\374\\247\\264<w\\016at\\306\\320\\214\\007%$\\021\\177\\013T\\362\\313H\\351\\371}b\\216t\\301n\\375\\225L\\324\\240\\306\\367\\033\\265\\372,s\\243\\232\\203[\\214'\\367\\326\\264\\303Lg"~\\3744\\314\\005,\\260\\270\\000)k\\034l\\230]'\\034\\363\\032\\026\\305\\263\\000\\303\\2333-P?\\274?\\344\\022\\251\\370OG\\357\\324J\\253\\232\\342\\312\\327\\360\\270\\022\\260\\266:\\014;\\300\\312j\\305sE\\302\\213}/\\263r\\274-\\346\\304W\\024\\331\\027\\303\\254WC\\313"\\035\\003\\3154\\334\\322\\261\\337\\200\\217\\265\\217\\276\\345\\217\\270\\217\\262\\311\\270\\027\\244\\014\\227\\003!\\374\\200\\224\\371i;\\300\\364\\022v,\\345\\3047\\326\\3057\\015\\005%\\240\\322\\272\\206\\031#\\311>\\230n\\275\\303\\023\\361\\275\\235\\273O\\034[\\001\\305H\\277\\263=oRw\\\\<\\203\\245\\011(\\315\\253K\\300\\375\\257\\326\\2636*\\230K_l\\014Z-\\355\\312\\215\\263\\336C\\352,\\214\\023g\\357_1\\225\\266\\210hT\\037\\247\\200\\265\\034Mu9$f\\226\\356\\205\\307\\362 \\314\\302\\366$^tVX\\321\\356\\017\\030\\002\\326\\012b\\2552\\377\\001`H\\276\\225\\267\\012\\334X\\214b\\307\\025\\212\\217\\235Z\\250\\341\\330(\\361e(w\\326\\222J\\0031J\\274/\\233+\\320\\237\\267\\303/\\035\\244\\224\\357\\226\\324<\\263\\343\\377\\033Aj\\214\\215\\273\\256V\\2672ph*\\376\\212\\2625\\014[\\271r4\\346+a\\277\\240mq\\320\\002V|\\27329S\\327\\301%\\354m-@\\213\\002J\\330*\\265i\\204Y\\\\\\216\\312:"\\271$~W\\337\\005\\201\\316\\036\\254\\343$\\225b\\321\\310\\025\\325\\332\\335AcMk\\342\\241\\314tl\\215\\360o~\\277\\237Cm\\361n)t\\020\\332f\\323Y]\\010\\323\\277\\303\\345(Nu\\211\\232\\0302\\370'U\\233s\\013\\300\\325\\251\\254[\\364\\202\\200\\345'\\225\\264\\026\\256\\340 \\367\\002\\333\\227\\333$\\346\\206\\263\\345e\\233|.\\363s\\276R\\204\\010\\246\\311H\\225\\304\\330\\254|a\\214\\243\\267Z\\350\\312\\266G\\214\\232\\203\\265\\312\\3555(\\346*\\320\\261J;}o\\011WG\\031\\347\\217U\\\\'I\\203\\236\\327\\014\\217\\302\\302\\230o\\001\\0155U\\033\\026\\231\\332!-\\352\\247\\333\\177\\015}\\272\\264Z\\031]\\312\\360\\300\\200\\007\\356\\222\\2645\\246\\274\\267\\241\\0246\\300\\213\\003\\240-\\006\\014\\224\\301V\\226\\372\\206\\320\\243n6l;\\037\\370\\2215\\213\\245O\\312m\\232(\\372f+X\\006\\005R\\340\\231s\\251\\206\\365\\202\\252\\222\\177=\\370O\\242\\247\\036\\230\\036\\312C\\260z\\222\\370$\\000{\\227\\357Veuz<V`\\022dIQ\\245u\\241\\253D\\213\\265\\272\\337\\031!\\252\\323\\237\\244\\026\\307\\234{\\217t\\353j\\351\\346\\001{\\321\\017\\273_\\313\\245\\361\\036\\206]!\\202@\\005c\\326;\\355\\0224\\343\\346\\334\\022\\266\\352\\271\\303\\302\\022\\366\\242OO\\325 z%?T\\020!\\351\\035\\3026\\012W\\207\\240L\\024\\342\\253M\\027\\265\\260x\\334\\327&c\\272\\225F#\\003\\210\\275\\320"\\212H0\\205(z\\024\\337rD~F\\335\\355y\\017]\\235C\\241\\270\\314\\377\\232\\221\\025\\211\\216s\\211\\247n\\035\\321b\\016\\365\\034\\243]p\\363\\315\\265\\026\\327Y\\360\\231\\222\\367\\314\\277R\\376H$~\\025i\\255\\216\\020\\273\\033\\026\\304\\034\\375A\\351\\254\\265Gs\\275\\027\\002}\\033\\207\\272\\211\\344\\365\\016\\342,Rp\\332\\000WMO.*\\253\\352\\030*\\274Wj\\213\\272\\347\\226\\215\\220\\004\\362F\\305\\266\\330\\323(\\224I\\264\\250\\305\\001c\\274<\\243\\233-m\\337\\367\\201\\317\\273\\340j\\206\\237\\324,\\230\\013)\\313\\006\\002\\341\\363.W\\002\\343\\310\\334\\305(N\\254\\367_\\377\\233T\\202\\026\\030jp\\237]\\330b\\004,\\237\\202\\030\\012( 4Xd\\376\\327u\\362'\\217\\000\\266\\336\\021\\237uSMt~&_\\306\\3363\\321\\3520\\343Y\\3562\\224I\\351\\275\\270\\016`G\\266\\302q#3m\\344\\347\\231D|7n\\251\\323\\314\\351}X\\030\\360\\250V\\036u\\262\\336\\371\\263\\253\\377\\240:\\207s\\222i\\316\\250\\364L\\347\\215g\\341\\247]R\\213\\201\\357\\364\\354\\301\\356h\\017Ue\\004#v\\005\\013\\020\\240&.\\243.8\\036\\326r\\020`BI?\\014\\306(c\\220\\000\\211Re\\025\\303h\\017{\\340\\275\\372\\237\\025\\206rk\\333aSr\\265\\264\\012\\257:\\030\\260\\024\\341\\372>\\207\\024\\367]\\002\\002\\301\\307\\361\\276\\344\\213\\246@x\\012\\277P\\014\\2362\\346\\263^\\310&-\\313\\303P\\222\\314\\257M\\031\\003\\315dA\\302?d\\006/'\\006a\\303\\260\\250n\\017s:\\363\\367\\315Q\\344\\313\\246T\\255R\\231\\305K\\276]\\2669\\251zKzihE\\313\\373$\\336\\311"CQ{q\\337\\033BM|\\213\\24421\\223\\235\\266\\2356\\313\\264^\\1772l\\252\\367G\\357i\\264\\220u\\257\\002\\242.\\243~\\316\\263+\\344\\241\\015\\302G\\2546\\245\\261Q9\\356\\257&\\035FPF\\232\\200\\221\\274=\\276s\\300\\357\\006\\303=\\026\\242\\240\\210|\\202\\213+\\272\\312\\276\\370\\343\\221f\\037\\310\\334\\177\\270\\223s \\346]\\316\\236O\\355\\024EF\\370\\330{\\311XC\\260\\203\\224\\016i\\245\\020\\343\\\\|\\266MF\\177\\244o\\353\\246\\226t\\305?\\362X%\\375\\320\\001G\\235\\345K$\\202\\216!\\354;s\\213\\026B\\272^vI\\351\\241\\377\\025\\254\\360\\203GD<\\222\\357(\\211\\252\\004\\013\\241oPh\\013Q\\202\\321\\363\\356F\\002(\\251\\326\\350\\007\\321-\\314,\\243*\\010\\340,\\344,\\223\\321\\302q\\024\\216\\214\\376J\\253\\326\\011bB1\\257\\270^\\030\\320\\017\\273\\237\\220#X|\\365~\\025&7\\340~\\320\\210\\024}\\016\\232C\\273\\347BV+\\206\\177\\361\\\\\\034y\\211\\201\\265\\012\\266-6<\\301\\327\\357L@\\022\\263\\316\\224\\264\\370\\300\\011\\037\\27605g=\\266\\222Q\\023\\3431\\240\\374_%A\\231\\003\\247K/K ?\\014C\\256=;\\\\\\306\\241\\034\\334\\235X`\\206\\213\\267\\207q:k\\270\\224n!\\310\\007\\3263B\\017\\373\\302\\004s\\374\\212\\012\\277\\001\\325\\306\\243\\262\\276?\\246S\\3616QR\\350?sl\\235,\\246\\306q\\3344[0\\035\\201CSa\\357\\267\\256#R8]\\207,B%\\241\\013\\027\\233\\371\\232\\247\\375fv\\027\\231b\\267\\331&\\322 v\\231"%zH)\\\\\\346\\367\\304\\023\\244\\333\\241\\207g\\372r\\030A\\346I\\2370\\303\\2027\\020\\351M\\251z\\032V\\014\\233\\355\\225\\023O}\\222j\\203\\032OY\\260\\3048M.$"h&0\\223\\2216\\352V\\262nz{y\\233\\361\\0011o\\222u\\257\\011$\\310\\346(W\\366\\307\\010;\\263/\\235\\351\\300v;\\377\\223\\217\\020\\011f2rh\\233\\224\\236\\333\\240V\\257\\261\\305\\320\\365\\2560\\3070\\230=\\240H1\\277\\020\\374\\202-\\345\\236\\351\\221\\234|k\\310\\342\\254\\030\\363e\\015\\347\\2672\\215\\360b\\306\\026\\316\\344w^6\\327X\\325\\037\\201\\223\\314\\336\\354\\250\\007Ep\\346\\231VG@[M\\352\\346\\256N\\\\\\240\\257\\272s#\\231H8G\\221\\3331\\336\\211M\\204\\000m\\010\\335AF\\224\\242Vs\\233\\247\\032\\014l\\315\\311\\272\\370\\037\\177!\\373J\\336\\3023\\0312\\245\\017\\\\?\\376\\255\\255o\\325\\227\\243%\\253H\\221Z\\030%!~O\\024\\272\\367\\225\\267\\262\\317\\266\\036\\361\\325y\\265\\231h\\206\\272\\263\\366T\\325M\\323\\357\\256:\\004\\011)&\\346>\\301\\211\\301 \\333\\213\\231o\\324\\224?n/;Z\\312\\357}\\337<@\\002\\261\\240\\341QFs5\\204?\\222\\344\\007\\327\\236T6\\276v\\222n^egFFV\\230'^$\\267o1\\376\\030\\374\\246\\2102s\\203#\\276\\313\\273\\347=\\013\\025A\\376\\251\\027x\\360\\017z>\\246\\317\\014(\\235`\\231[/R\\276lS)-\\202\\261\\345W\\004A\\225\\262\\343\\027\\014\\031\\251\\024]`\\012\\031}.I\\3678\\376y\\366,\\372\\235\\323\\335|h\\273\\207\\206\\232\\011\\312\\347\\305\\224M=\\025\\376\\203\\320"\\376\\257\\361\\206\\234\\260\\030;\\215\\376\\201\\306nl\\305\\312a)5\\200\\0027^LjMU"\\344\\227\\246\\204\\005\\313\\001\\227:\\235\\177\\331\\243\\305\\206\\257\\353\\227\\236J\\346B\\347\\226\\022\\246\\024\\012\\236\\245Y\\256G,\\220G\\200\\015\\330x\\217\\331$J'<t\\252\\035\\304\\001\\274VR\\264\\357\\3303\\351\\302\\224\\203\\226@m\\220;[\\372s\\370\\327}\\356\\021e\\274}\\221\\367\\372\\260\\240Q%\\023\\226e.\\377:\\317\\327\\317\\253\\345\\000\\223\\\\N\\351\\214\\177\\304\\034\\263\\352\\226\\255\\226\\353\\252\\315\\014\\221+\\360K\\302\\320\\346\\322\\351\\367\\241\\235\\323\\030\\342\\366\\320\\2629\\365V\\354\\320\\266\\2250M&\\364j\\250o\\331\\243\\352\\244\\263\\2100;\\024\\361P\\034#\\237\\035\\257\\220\\276B\\200\\207U\\366\\305\\244\\224\\3706\\037\\200K\\215\\252\\216~B\\362P\\376\\025 \\225\\345\\007\\322\\245\\207\\006"\\367P3\\323\\373\\365\\220\\347l\\366?n\\013#\\3446L\\003\\017\\204B#&l\\322\\326\\215\\365\\225\\224\\036\\2377t\\266\\013#\\337\\314'\\250\\277\\234\\227[\\216\\362!\\226v\\374-]\\320\\005yr\\372u:f\\217\\336\\365T\\321\\350\\026\\251\\221
\.


--
-- TOC entry 1881 (class 0 OID 18464)
-- Dependencies: 1534
-- Data for Name: organization; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY organization (branchid, organizationname, organizationaddress, city, country, telp) FROM stdin;
dny@gmail.com	Asli Motor SIQ	JL. Dabo Lama No. 1,2,3	Dabo Singkep	Indonesia	(0776) 322135
\.


--
-- TOC entry 1891 (class 0 OID 18587)
-- Dependencies: 1544
-- Data for Name: perjanjianautonumberconfig; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY perjanjianautonumberconfig (id, mode, prefix, branchid) FROM stdin;
dny@gmail.com-PerjanjianAutoNumberConfig	0	SP-AM6	dny@gmail.com
\.


--
-- TOC entry 1892 (class 0 OID 18595)
-- Dependencies: 1545
-- Data for Name: perjanjianautonumbermonthly; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY perjanjianautonumbermonthly (id, branchid, year, month, value) FROM stdin;
dny@gmail.com-201210	dny@gmail.com	2012	10	7
dny@gmail.com-201211	dny@gmail.com	2012	11	5
\.


--
-- TOC entry 1893 (class 0 OID 18603)
-- Dependencies: 1546
-- Data for Name: perjanjianautonumberyearly; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY perjanjianautonumberyearly (id, branchid, year, value) FROM stdin;
\.


--
-- TOC entry 1875 (class 0 OID 18415)
-- Dependencies: 1528
-- Data for Name: product; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY product (id, merk, type, tahun, warna, norangka, nomesin, nobpkb, hargabeli, status, nopolisi, branchid) FROM stdin;
beba02ed-c72e-41e3-b119-318f8d3cddf0	Yamaha	terst	2012	\N	\N	\N	\N	4567876	Aktif	ests	dny@gmail.com
aef8500b-b143-4e61-badc-128c637458ec	Yamaha	MIO SOUL	2012					6500000	Terjual	BP 1111 HI	dny@gmail.com
cc987714-20ea-4d20-b9e1-cc1bb167f51f	Yamaha	MIO CW	2009	Merah				8900000	Terjual	BP 5038 FO	dny@gmail.com
501b16d5-47e0-4594-ae3c-186ccc5d6870	Yamaha	MIO CW	2012					9800000	Terjual	BP 5645 FO	dny@gmail.com
a04335bf-3025-472f-b44c-1b62f44a82f7	Yamaha	XEON	2012					8500000	Terjual	BP 6435 PO	dny@gmail.com
56e3b5f3-a1c8-41fd-b0dc-59fdd697d50b	Yamaha	XEON	2012					7800000	Terjual	BP 6453 PI	dny@gmail.com
1531cdc8-4d63-4a60-8f14-3ab6139932f4	Yamaha	JUPITER	2012					6800000	Terjual	BP 6753 II	dny@gmail.com
a98c62b3-8c80-4573-a696-465c58645e45	Yamaha	JUPITER MX	2012					8000000	Terjual	BP 7845 UU	dny@gmail.com
5b796f18-09d7-408a-a4c7-c15ae063d6a4	Yamaha	JUPITER Z	2012					8200000	Terjual	BP 3546 IO	dny@gmail.com
d3ae9960-5031-4301-bb8b-19d2c9457cd2	Yamaha	MIO SOUL	2012					7800000	Terjual	BP 4353 PO	dny@gmail.com
\.


--
-- TOC entry 1883 (class 0 OID 18503)
-- Dependencies: 1536
-- Data for Name: productlog; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY productlog (id, username, productid, action, merk, type, tahun, warna, norangka, nomesin, nobpkb, hargabeli, status, nopolisi, branchid, datetime) FROM stdin;
dbd4ffb0-b00e-4db2-8549-14b37b56d249	dny	cc987714-20ea-4d20-b9e1-cc1bb167f51f	0	Yamaha	MIO CW	2010	Merah	\N	\N	\N	8900000	Aktif	BP 5038 FO	dny@gmail.com	23-12-2012 02:12:56
d99cc4bc-51f3-4f7f-b304-a36bedf17783	dny	cc987714-20ea-4d20-b9e1-cc1bb167f51f	1	Yamaha	MIO CW	2010	Meraah	\N	\N	\N	8900000	Aktif	BP 5038 FO	dny@gmail.com	23-13-2012 02:13:41
2bcfa522-f4c3-44a8-a110-5dd239db5cc6	dny	cc987714-20ea-4d20-b9e1-cc1bb167f51f	1	Yamaha	MIO CW	2010	Merah	\N	\N	\N	8900000	Aktif	BP 5038 FO	dny@gmail.com	23-14-2012 02:14:14
\.


--
-- TOC entry 1890 (class 0 OID 18561)
-- Dependencies: 1543
-- Data for Name: receive; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY receive (id, invoiceid, receiveno, receivedate, receivetype, charge, total, branchid, denda, month) FROM stdin;
eac9010e-cc50-4af3-8862-16e979ecaecc	415e7d2a-9455-427e-97aa-3b4466abfbb7	00026/10/RCV-AM6/2012	2012-10-27	0	0	300000	dny@gmail.com	0	\N
1a64eb84-4c3b-4518-b073-4d95780301cb	415e7d2a-9455-427e-97aa-3b4466abfbb7	00027/10/RCV-AM6/2012	2012-10-27	1	200000	2000000	dny@gmail.com	0	\N
2b0241f4-0915-4ca4-9f34-095897c27213	415e7d2a-9455-427e-97aa-3b4466abfbb7	00028/10/RCV-AM6/2012	2012-10-27	3	0	418750.000	dny@gmail.com	0.000	112012
ad2ea922-41e3-44ea-a0b7-366f29ae4012	5729ffb8-c9e1-4599-96e2-f0ae8eef22ca	00029/10/RCV-AM6/2012	2012-10-27	1	200000	2000000	dny@gmail.com	0	\N
54fc987c-d5ce-458a-8e27-529f65c63e71	aff7a01f-cc09-434b-9db9-fc0e4d9c87c8	00030/10/RCV-AM6/2012	2012-10-28	0	0	300000	dny@gmail.com	0	\N
5069191a-4541-42db-86b0-f8699f23c88f	11ad058b-5699-413c-8ab5-ebc7381eef53	00031/10/RCV-AM6/2012	2012-10-29	2	0	9000000	dny@gmail.com	0	\N
031f72b0-599d-4636-8432-3c76c07f8d22	673402e4-6f86-4f3c-8980-be537c8e6272	00032/10/RCV-AM6/2012	2012-10-29	1	300000	3000000	dny@gmail.com	0	\N
1b1c16b3-fbef-40ec-b8c3-82262d347b8a	6baa5511-2bfb-4660-a4c7-f8693bb22b39	00033/10/RCV-AM6/2012	2012-10-29	2	0	9800000	dny@gmail.com	0	\N
42a0c6c3-58a3-4e75-b1b9-195ddd638573	aff7a01f-cc09-434b-9db9-fc0e4d9c87c8	00001/11/RCV-AM6/2012	2012-11-02	1	200000	2000000	dny@gmail.com	0	\N
d222e4cc-35c8-4ec1-98ce-92ada8b0557f	67036ec0-618e-4362-aa74-dbd244d71cc1	00002/11/RCV-AM6/2012	2012-11-02	0	0	500000	dny@gmail.com	0	\N
ad4a1547-8579-41ea-9257-e9b48f2241d9	67036ec0-618e-4362-aa74-dbd244d71cc1	00003/11/RCV-AM6/2012	2012-11-02	2	0	6500000	dny@gmail.com	0	\N
35aff59a-4cae-4cbd-a8d6-befd19a1a5ba	a7755f91-e12f-44ec-a54c-19e598515098	00004/11/RCV-AM6/2012	2012-11-02	0	0	300000	dny@gmail.com	0	\N
2c52d36f-ee6e-46f8-a0e9-a9513744e9fc	673402e4-6f86-4f3c-8980-be537c8e6272	00005/11/RCV-AM6/2012	2012-11-15	3	0	295983	dny@gmail.com	14094	112012
13c9768a-dfb5-48d6-9509-229b2065732b	673402e4-6f86-4f3c-8980-be537c8e6272	00006/11/RCV-AM6/2012	2012-11-03	3	0	281889	dny@gmail.com	0	122012
bc0b5b13-fad2-479d-8286-ccbc9ef0ca0a	673402e4-6f86-4f3c-8980-be537c8e6272	00007/11/RCV-AM6/2012	2013-01-01	3	0	281889	dny@gmail.com	0	012013
d9988a28-50af-4603-b16e-bdc87c4513ad	673402e4-6f86-4f3c-8980-be537c8e6272	00008/11/RCV-AM6/2012	2013-02-12	3	0	291755	dny@gmail.com	9866	022013
e73252e4-aa0c-4077-8b50-039f155d6bf2	00ccb07b-f0d4-42cf-b75c-857a4d267c7b	00009/11/RCV-AM6/2012	2012-11-03	0	0	800000	dny@gmail.com	0	\N
2c30d6dc-dff3-4c9d-bf42-a399d0c521cb	14ddceaf-6a68-4a35-9144-c7d1c3c8076b	00010/11/RCV-AM6/2012	2012-11-03	0	0	300000	dny@gmail.com	0	\N
40c8beba-1e7b-4796-bf0e-b25032454680	e1344023-e143-42be-84fb-d280c459c85f	00011/11/RCV-AM6/2012	2012-11-03	1	200000	3000000	dny@gmail.com	0	\N
fba3aac4-77e9-4901-91e1-7ffaf370e4fa	e1344023-e143-42be-84fb-d280c459c85f	00012/11/RCV-AM6/2012	2012-11-03	3	0	326833	dny@gmail.com	0	112012
78e19c47-5811-4d40-9b52-8a8089e5c6a1	e1344023-e143-42be-84fb-d280c459c85f	00013/11/RCV-AM6/2012	2012-12-10	3	0	330101	dny@gmail.com	3268	122012
10bb514c-9a3a-4797-a65e-ad81b4b7779d	e1344023-e143-42be-84fb-d280c459c85f	00014/11/RCV-AM6/2012	2013-02-05	3	0	372590	dny@gmail.com	45757	012013
f454f20e-e9ae-4d86-aa29-b9f5a2b128db	e1344023-e143-42be-84fb-d280c459c85f	00015/11/RCV-AM6/2012	2013-03-05	3	0	367687	dny@gmail.com	40854	022013
75197229-94e4-4f0c-8f0e-023eef2a5e00	e1344023-e143-42be-84fb-d280c459c85f	00016/11/RCV-AM6/2012	2013-04-15	3	0	388931	dny@gmail.com	62098	032013
749afaa0-5931-4e16-bd5d-79e8b3d2090e	14ddceaf-6a68-4a35-9144-c7d1c3c8076b	00017/11/RCV-AM6/2012	2012-11-03	2	0	9000000	dny@gmail.com	0	\N
ba639837-6fe5-4d14-a336-f5630f3b2c7c	00ccb07b-f0d4-42cf-b75c-857a4d267c7b	00018/11/RCV-AM6/2012	2012-11-03	2	0	7000000	dny@gmail.com	0	\N
3dfb8b62-dfd2-4d47-b6f9-de0805875ac0	a7755f91-e12f-44ec-a54c-19e598515098	00019/11/RCV-AM6/2012	2012-11-03	2	0	8700000	dny@gmail.com	0	\N
343ede32-e23d-4b8d-af3c-cc15c4010d13	57adc86a-42ec-4d69-bbff-3734d72b10db	00020/11/RCV-AM6/2012	2012-11-03	0	0	100000	dny@gmail.com	0	\N
386e8e40-df36-4844-96c3-06e348c49e59	57adc86a-42ec-4d69-bbff-3734d72b10db	00021/11/RCV-AM6/2012	2012-11-03	2	0	8200000	dny@gmail.com	0	\N
6e56fe4e-f463-4b96-947b-838199f4a600	316cd89b-f0d1-4654-8470-efadecbbb103	00022/11/RCV-AM6/2012	2012-11-03	0	0	500000	dny@gmail.com	0	\N
810ae1c5-5c63-4d51-b050-e4aaf43e3135	316cd89b-f0d1-4654-8470-efadecbbb103	00024/11/RCV-AM6/2012	2012-11-04	1	300000	3000000	dny@gmail.com	0	
5f8502f5-be81-48e7-9a56-62563bede3e6	5717bd9a-14cc-49f7-8c3c-1bae0b37ce6a	00025/11/RCV-AM6/2012	2012-11-04	1	300000	5000000	dny@gmail.com	0	
b50aea26-2523-4901-a691-16ed6593a50b	5717bd9a-14cc-49f7-8c3c-1bae0b37ce6a	00026/11/RCV-AM6/2012	2012-11-04	3	0	571676078	dny@gmail.com	571520522	010001
dc7e11cb-dac4-4cdc-abf0-f91a43c4a6e3	ad3c0d1a-a74c-49f7-8b68-0408b5c58de2	00027/11/RCV-AM6/2012	2012-11-04	1	200000	600000	dny@gmail.com	0	
66a904fa-5d13-4b9a-8d65-b65967150178	ad3c0d1a-a74c-49f7-8b68-0408b5c58de2	00028/11/RCV-AM6/2012	2012-11-04	3	0	450167	dny@gmail.com	0	112012
\.


--
-- TOC entry 1887 (class 0 OID 18537)
-- Dependencies: 1540
-- Data for Name: receiveautonumberconfig; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY receiveautonumberconfig (id, mode, prefix, branchid) FROM stdin;
dny@gmail.com-ReceiveAutoNumberConfig	0	RCV-AM6	dny@gmail.com
\.


--
-- TOC entry 1888 (class 0 OID 18545)
-- Dependencies: 1541
-- Data for Name: receiveautonumbermonthly; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY receiveautonumbermonthly (id, branchid, year, month, value) FROM stdin;
dny@gmail.com-201212	dny@gmail.com	2012	12	1
dny@gmail.com-201302	dny@gmail.com	2013	2	1
dny@gmail.com-201306	dny@gmail.com	2013	6	1
dny@gmail.com-201210	dny@gmail.com	2012	10	33
dny@gmail.com-201211	dny@gmail.com	2012	11	28
\.


--
-- TOC entry 1889 (class 0 OID 18553)
-- Dependencies: 1542
-- Data for Name: receiveautonumberyearly; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY receiveautonumberyearly (id, branchid, year, value) FROM stdin;
\.


--
-- TOC entry 1876 (class 0 OID 18424)
-- Dependencies: 1529
-- Data for Name: siautonumberconfig; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY siautonumberconfig (id, mode, prefix, branchid) FROM stdin;
dny@gmail.com-SIAutoNumberConfig	0	SI-AM6	dny@gmail.com
\.


--
-- TOC entry 1877 (class 0 OID 18432)
-- Dependencies: 1530
-- Data for Name: siautonumbermonthly; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY siautonumbermonthly (id, branchid, year, month, value) FROM stdin;
dny@gmail.com-201210	dny@gmail.com	2012	10	3
dny@gmail.com-201211	dny@gmail.com	2012	11	7
\.


--
-- TOC entry 1878 (class 0 OID 18440)
-- Dependencies: 1531
-- Data for Name: siautonumberyearly; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY siautonumberyearly (id, branchid, year, value) FROM stdin;
\.


--
-- TOC entry 1879 (class 0 OID 18448)
-- Dependencies: 1532
-- Data for Name: supplierinvoice; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY supplierinvoice (id, branchid, transactiondate, supplierinvoicedate, supplierinvoiceno, suppliername, supplierbillingaddress, merk, type, tahun, warna, norangka, nomesin, nobpkb, nopolisi, hargabeli, note, notelp, productid) FROM stdin;
7d77dcb4-933c-409f-ae35-fddee0111fd7	dny@gmail.com	2012-11-03 18:01:59.636779+07	2012-11-03	00003/11/SI-AM6/2012	\N	\N	Yamaha	XEON	2012	\N	\N	\N	\N	BP 6453 PI	7800000	\N	\N	56e3b5f3-a1c8-41fd-b0dc-59fdd697d50b
d16dde21-0512-483f-a8ab-dfe4759c264f	dny@gmail.com	2012-11-03 18:23:55.62772+07	2012-11-03	00004/11/SI-AM6/2012	\N	\N	Yamaha	JUPITER	2012	\N	\N	\N	\N	BP 6753 II	6800000	\N	\N	1531cdc8-4d63-4a60-8f14-3ab6139932f4
071534ae-ce67-4d24-a331-79f74fd05685	dny@gmail.com	2012-11-03 18:25:35.109094+07	2012-11-03	00005/11/SI-AM6/2012	\N	\N	Yamaha	JUPITER MX	2012	\N	\N	\N	\N	BP 7845 UU	8000000	\N	\N	a98c62b3-8c80-4573-a696-465c58645e45
9df39f0b-c52c-48da-9fa7-d1466063ef63	dny@gmail.com	2012-11-04 14:40:23.370306+07	2012-11-04	00006/11/SI-AM6/2012	\N	\N	Yamaha	JUPITER Z	2012	\N	\N	\N	\N	BP 3546 IO	8200000	\N	\N	5b796f18-09d7-408a-a4c7-c15ae063d6a4
989936de-b3fd-43b9-833d-4f040ba0eefd	dny@gmail.com	2012-11-04 14:52:04.633916+07	2012-11-04	00007/11/SI-AM6/2012	\N	\N	Yamaha	MIO SOUL	2012	\N	\N	\N	\N	BP 4353 PO	7800000	\N	\N	d3ae9960-5031-4301-bb8b-19d2c9457cd2
acc24148-9b45-4c11-a932-6f114b2cf9de	dny@gmail.com	2012-10-23 14:12:56.569678+07	2012-02-10	00001/10/SI-AM6/2012	Denny	Baloi View, Batam	Yamaha	MIO CW	2009	Merah	\N	\N	\N	BP 5038 FO	8900000	\N	08566584915	cc987714-20ea-4d20-b9e1-cc1bb167f51f
1b76ecfd-46b7-4fab-8e36-4f1a0971865d	dny@gmail.com	2012-10-23 21:03:25.413366+07	2012-10-23	00003/10/SI-AM6/2012	\N	\N	Yamaha	MIO SOUL	2012	\N	\N	\N	\N	BP 1111 HI	6000000	\N	\N	aef8500b-b143-4e61-badc-128c637458ec
854f8d28-cfe6-445e-96ea-917fbe5b58cb	dny@gmail.com	2012-11-01 13:45:17.666692+07	2012-11-01	00001/11/SI-AM6/2012	\N	\N	Yamaha	terst	2012	\N	\N	\N	\N	ests	4567876	\N	\N	beba02ed-c72e-41e3-b119-318f8d3cddf0
23982501-cd26-480b-9be9-fe3e55787fec	dny@gmail.com	2012-10-23 21:00:39.260863+07	2012-10-23	00002/10/SI-AM6/2012	\N	\N	Yamaha	MIO CW	2012	\N	\N	\N	\N	BP 8897 PO	9800000	\N	\N	501b16d5-47e0-4594-ae3c-186ccc5d6870
d32c6afb-89b2-4f0d-a40b-a1d5e6c4246d	dny@gmail.com	2012-11-03 17:56:53.847289+07	2012-11-03	00002/11/SI-AM6/2012	\N	\N	Yamaha	XEON	2012	\N	\N	\N	\N	BP 6435 PO	8500000	\N	\N	a04335bf-3025-472f-b44c-1b62f44a82f7
\.


--
-- TOC entry 1884 (class 0 OID 18511)
-- Dependencies: 1537
-- Data for Name: supplierinvoicelog; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY supplierinvoicelog (id, username, action, datetime, branchid, transactiondate, supplierinvoicedate, supplierinvoiceno, suppliername, supplierbillingaddress, merk, type, tahun, warna, norangka, nomesin, nobpkb, nopolisi, hargabeli, note, notelp, productid, supplierinvoiceid) FROM stdin;
8cd89b6b-3fbf-4e13-9e2c-d6427632b376	dny	0	23-12-2012 02:12:56	dny@gmail.com	2012-10-23 14:12:56.569678+07	2012-10-23	00001/10/SI-AM6/2012	Denny	Baloi View, Batam	Yamaha	MIO CW	2010	Merah	\N	\N	\N	BP 5038 FO	8900000	\N	08566584915	cc987714-20ea-4d20-b9e1-cc1bb167f51f	acc24148-9b45-4c11-a932-6f114b2cf9de
90964da7-d3c1-4080-a6ef-713eb739fcc5	dny	1	23-14-2012 02:14:14	dny@gmail.com	2012-10-23 14:12:56.569678+07	2012-10-23	00001/10/SI-AM6/2012	Denny	Baloi View, Batam	Yamaha	MIO CW	2010	Merah	\N	\N	\N	BP 5038 FO	8900000	\N	08566584915	cc987714-20ea-4d20-b9e1-cc1bb167f51f	acc24148-9b45-4c11-a932-6f114b2cf9de
\.


--
-- TOC entry 1894 (class 0 OID 18611)
-- Dependencies: 1547
-- Data for Name: suratperjanjian; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY suratperjanjian (invoiceid, suratperjanjianno, suratperjanjiandate) FROM stdin;
415e7d2a-9455-427e-97aa-3b4466abfbb7	00005/10/SP-AM6/2012	2012-11-04 21:59:37.733+07
5729ffb8-c9e1-4599-96e2-f0ae8eef22ca	00006/10/SP-AM6/2012	2012-11-04 21:59:37.733+07
673402e4-6f86-4f3c-8980-be537c8e6272	00007/10/SP-AM6/2012	2012-11-04 21:59:37.733+07
aff7a01f-cc09-434b-9db9-fc0e4d9c87c8	00001/11/SP-AM6/2012	2012-11-04 21:59:37.733+07
e1344023-e143-42be-84fb-d280c459c85f	00002/11/SP-AM6/2012	2012-11-04 21:59:37.733+07
316cd89b-f0d1-4654-8470-efadecbbb103	00003/11/SP-AM6/2012	2012-11-04 21:59:37.733+07
5717bd9a-14cc-49f7-8c3c-1bae0b37ce6a	00004/11/SP-AM6/2012	2012-11-04 21:59:37.733+07
ad3c0d1a-a74c-49f7-8b68-0408b5c58de2	00005/11/SP-AM6/2012	2012-11-04 21:59:37.733+07
\.


--
-- TOC entry 1880 (class 0 OID 18451)
-- Dependencies: 1533
-- Data for Name: typeproduct; Type: TABLE DATA; Schema: public; Owner: aslimotor
--

COPY typeproduct (id, name, branchid) FROM stdin;
ece97de4-1e5b-44d9-9ef7-dd1c10baeb5a	MIO CW	dny@gmail.com
b111c89c-49a4-4c56-bc7b-8a647621d3f5	MIO SOUL	dny@gmail.com
c725c3ef-5cbb-4625-b132-b2a7c9f48fcc	terst	dny@gmail.com
828020cd-a8d6-42a0-97f9-c8628ab90f01	XEON	dny@gmail.com
5e7ab898-b555-4432-b92a-7037bec4acce	JUPITER	dny@gmail.com
9a0f6914-2f41-47ea-9252-ec43a0ab578e	JUPITER MX	dny@gmail.com
f25fc4cc-29a3-46e1-9f6e-6f0aaece4e48	JUPITER Z	dny@gmail.com
\.


--
-- TOC entry 1839 (class 2606 OID 18471)
-- Dependencies: 1534 1534
-- Name: companyprofile_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY organization
    ADD CONSTRAINT companyprofile_pkey PRIMARY KEY (branchid);


--
-- TOC entry 1849 (class 2606 OID 18570)
-- Dependencies: 1538 1538
-- Name: customer_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY customer
    ADD CONSTRAINT customer_pkey PRIMARY KEY (id);


--
-- TOC entry 1852 (class 2606 OID 18568)
-- Dependencies: 1539 1539
-- Name: invoicesnapshot_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY invoicesnapshot
    ADD CONSTRAINT invoicesnapshot_pkey PRIMARY KEY (id);


--
-- TOC entry 1841 (class 2606 OID 18479)
-- Dependencies: 1535 1535
-- Name: logocompanyprofile_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY logoorganization
    ADD CONSTRAINT logocompanyprofile_pkey PRIMARY KEY (id);


--
-- TOC entry 1867 (class 2606 OID 18594)
-- Dependencies: 1544 1544
-- Name: perjanjianautonumberconfig_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY perjanjianautonumberconfig
    ADD CONSTRAINT perjanjianautonumberconfig_pkey PRIMARY KEY (id);


--
-- TOC entry 1869 (class 2606 OID 18602)
-- Dependencies: 1545 1545
-- Name: perjanjianautonumbermonthly_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY perjanjianautonumbermonthly
    ADD CONSTRAINT perjanjianautonumbermonthly_pkey PRIMARY KEY (id);


--
-- TOC entry 1871 (class 2606 OID 18610)
-- Dependencies: 1546 1546
-- Name: perjanjianautonumberyearly_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY perjanjianautonumberyearly
    ADD CONSTRAINT perjanjianautonumberyearly_pkey PRIMARY KEY (id);


--
-- TOC entry 1822 (class 2606 OID 18422)
-- Dependencies: 1528 1528
-- Name: product_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY product
    ADD CONSTRAINT product_pkey PRIMARY KEY (id);


--
-- TOC entry 1844 (class 2606 OID 18510)
-- Dependencies: 1536 1536
-- Name: productlog_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY productlog
    ADD CONSTRAINT productlog_pkey PRIMARY KEY (id);


--
-- TOC entry 1865 (class 2606 OID 18572)
-- Dependencies: 1543 1543
-- Name: receive_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY receive
    ADD CONSTRAINT receive_pkey PRIMARY KEY (id);


--
-- TOC entry 1856 (class 2606 OID 18544)
-- Dependencies: 1540 1540
-- Name: receiveautonumberconfig_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY receiveautonumberconfig
    ADD CONSTRAINT receiveautonumberconfig_pkey PRIMARY KEY (id);


--
-- TOC entry 1859 (class 2606 OID 18552)
-- Dependencies: 1541 1541
-- Name: receiveautonumbermonthly_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY receiveautonumbermonthly
    ADD CONSTRAINT receiveautonumbermonthly_pkey PRIMARY KEY (id);


--
-- TOC entry 1862 (class 2606 OID 18560)
-- Dependencies: 1542 1542
-- Name: receiveautonumberyearly_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY receiveautonumberyearly
    ADD CONSTRAINT receiveautonumberyearly_pkey PRIMARY KEY (id);


--
-- TOC entry 1825 (class 2606 OID 18431)
-- Dependencies: 1529 1529
-- Name: siautonumberconfig_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY siautonumberconfig
    ADD CONSTRAINT siautonumberconfig_pkey PRIMARY KEY (id);


--
-- TOC entry 1828 (class 2606 OID 18439)
-- Dependencies: 1530 1530
-- Name: siautonumbermonthly_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY siautonumbermonthly
    ADD CONSTRAINT siautonumbermonthly_pkey PRIMARY KEY (id);


--
-- TOC entry 1831 (class 2606 OID 18447)
-- Dependencies: 1531 1531
-- Name: siautonumberyearly_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY siautonumberyearly
    ADD CONSTRAINT siautonumberyearly_pkey PRIMARY KEY (id);


--
-- TOC entry 1834 (class 2606 OID 18463)
-- Dependencies: 1532 1532
-- Name: supplierinvoice_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY supplierinvoice
    ADD CONSTRAINT supplierinvoice_pkey PRIMARY KEY (id);


--
-- TOC entry 1847 (class 2606 OID 18518)
-- Dependencies: 1537 1537
-- Name: supplierinvoicelog_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY supplierinvoicelog
    ADD CONSTRAINT supplierinvoicelog_pkey PRIMARY KEY (id);


--
-- TOC entry 1873 (class 2606 OID 18618)
-- Dependencies: 1547 1547
-- Name: suratperjanjian_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY suratperjanjian
    ADD CONSTRAINT suratperjanjian_pkey PRIMARY KEY (invoiceid);


--
-- TOC entry 1819 (class 2606 OID 18414)
-- Dependencies: 1527 1527
-- Name: tbl_user_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY account
    ADD CONSTRAINT tbl_user_pkey PRIMARY KEY (id);


--
-- TOC entry 1836 (class 2606 OID 18458)
-- Dependencies: 1533 1533
-- Name: type_pkey; Type: CONSTRAINT; Schema: public; Owner: aslimotor; Tablespace: 
--

ALTER TABLE ONLY typeproduct
    ADD CONSTRAINT type_pkey PRIMARY KEY (id);


--
-- TOC entry 1820 (class 1259 OID 18423)
-- Dependencies: 1528 1528 1528 1528
-- Name: index_searching; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX index_searching ON product USING btree (id, nomesin, nobpkb, norangka);


--
-- TOC entry 1817 (class 1259 OID 18573)
-- Dependencies: 1527
-- Name: login; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX login ON account USING btree (username);


--
-- TOC entry 1842 (class 1259 OID 18576)
-- Dependencies: 1536
-- Name: productlog_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX productlog_index ON productlog USING btree (id);


--
-- TOC entry 1863 (class 1259 OID 18577)
-- Dependencies: 1543 1543 1543
-- Name: receive_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX receive_index ON receive USING btree (invoiceid, branchid, receivetype);


--
-- TOC entry 1854 (class 1259 OID 18578)
-- Dependencies: 1540
-- Name: receiveautonumberconfig_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX receiveautonumberconfig_index ON receiveautonumberconfig USING btree (branchid);


--
-- TOC entry 1857 (class 1259 OID 18579)
-- Dependencies: 1541
-- Name: receiveautonumbermonthly_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX receiveautonumbermonthly_index ON receiveautonumbermonthly USING btree (branchid);


--
-- TOC entry 1860 (class 1259 OID 18580)
-- Dependencies: 1542
-- Name: receiveautonumberyearly_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX receiveautonumberyearly_index ON receiveautonumberyearly USING btree (branchid);


--
-- TOC entry 1850 (class 1259 OID 18574)
-- Dependencies: 1538 1538
-- Name: searching; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX searching ON customer USING btree (id, email);


--
-- TOC entry 1853 (class 1259 OID 18575)
-- Dependencies: 1539 1539
-- Name: searchinvoice; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX searchinvoice ON invoicesnapshot USING btree (id, customerid);


--
-- TOC entry 1823 (class 1259 OID 18581)
-- Dependencies: 1529
-- Name: siautonumberconfig_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX siautonumberconfig_index ON siautonumberconfig USING btree (branchid);


--
-- TOC entry 1826 (class 1259 OID 18582)
-- Dependencies: 1530
-- Name: siautonumbermonthly_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX siautonumbermonthly_index ON siautonumbermonthly USING btree (branchid);


--
-- TOC entry 1829 (class 1259 OID 18583)
-- Dependencies: 1531
-- Name: siautonumberyearly_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX siautonumberyearly_index ON siautonumberyearly USING btree (branchid);


--
-- TOC entry 1832 (class 1259 OID 18584)
-- Dependencies: 1532 1532 1532 1532 1532
-- Name: supplierinvoice_index; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX supplierinvoice_index ON supplierinvoice USING btree (id, branchid, suppliername, supplierinvoiceno, nopolisi);


--
-- TOC entry 1845 (class 1259 OID 18585)
-- Dependencies: 1537
-- Name: supplierinvoicelog_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX supplierinvoicelog_index ON supplierinvoicelog USING btree (id);


--
-- TOC entry 1837 (class 1259 OID 18586)
-- Dependencies: 1533 1533
-- Name: typeproduct_index; Type: INDEX; Schema: public; Owner: aslimotor; Tablespace: 
--

CREATE INDEX typeproduct_index ON typeproduct USING btree (branchid, name);


--
-- TOC entry 1899 (class 0 OID 0)
-- Dependencies: 3
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2012-11-05 17:08:52

--
-- PostgreSQL database dump complete
--

