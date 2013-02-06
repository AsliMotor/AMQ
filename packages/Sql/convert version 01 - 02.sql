-- Table: invoiceautonumberconfig

-- DROP TABLE invoiceautonumberconfig;

CREATE TABLE invoiceautonumberconfig
(
  id character varying NOT NULL,
  "mode" integer,
  prefix character varying,
  branchid character varying,
  CONSTRAINT invoiceautonumberconfig_pkey PRIMARY KEY (id)
)
WITH (OIDS=FALSE);
ALTER TABLE invoiceautonumberconfig OWNER TO aslimotor;

-- Index: invoiceautonumberconfig_index

-- DROP INDEX invoiceautonumberconfig_index;

CREATE INDEX invoiceautonumberconfig_index
  ON invoiceautonumberconfig
  USING btree
  (branchid);

-- Table: invoiceautonumberdefault

-- DROP TABLE invoiceautonumberdefault;

CREATE TABLE invoiceautonumberdefault
(
  id character varying NOT NULL,
  branchid character varying,
  "year" integer,
  "value" integer,
  CONSTRAINT invoiceautonumberdefault_pkey PRIMARY KEY (id)
)
WITH (OIDS=FALSE);
ALTER TABLE invoiceautonumberdefault OWNER TO aslimotor;

-- Index: invoiceautonumberdefault_index

-- DROP INDEX invoiceautonumberdefault_index;

CREATE INDEX invoiceautonumberdefault_index
  ON invoiceautonumberdefault
  USING btree
  (branchid);

-- Table: invoiceautonumbermonthly

-- DROP TABLE invoiceautonumbermonthly;

CREATE TABLE invoiceautonumbermonthly
(
  id character varying NOT NULL,
  branchid character varying,
  "year" integer,
  "month" integer,
  "value" integer,
  CONSTRAINT invoiceautonumbermonthly_pkey PRIMARY KEY (id)
)
WITH (OIDS=FALSE);
ALTER TABLE invoiceautonumbermonthly OWNER TO aslimotor;

-- Index: invoiceautonumbermonthly_index

-- DROP INDEX invoiceautonumbermonthly_index;

CREATE INDEX invoiceautonumbermonthly_index
  ON invoiceautonumbermonthly
  USING btree
  (branchid);

-- Table: invoiceautonumberyearly

-- DROP TABLE invoiceautonumberyearly;

CREATE TABLE invoiceautonumberyearly
(
  id character varying NOT NULL,
  branchid character varying,
  "year" integer,
  "value" integer,
  CONSTRAINT invoiceautonumberyearly_pkey PRIMARY KEY (id)
)
WITH (OIDS=FALSE);
ALTER TABLE invoiceautonumberyearly OWNER TO aslimotor;

-- Index: invoiceautonumberyearly_index

-- DROP INDEX invoiceautonumberyearly_index;

CREATE INDEX invoiceautonumberyearly_index
  ON invoiceautonumberyearly
  USING btree
  (branchid);

-- Table: paymentterm

-- DROP TABLE paymentterm;

CREATE TABLE paymentterm
(
  id uuid,
  termname character varying,
  "value" integer,
  ownerid character varying
)
WITH (OIDS=FALSE);
ALTER TABLE paymentterm OWNER TO aslimotor;

INSERT INTO paymentterm (id,termname,value,ownerid) values ('59abfe79-37b1-49f7-a272-c41a66300fb3','30 Hari',30,'aslimotor@aslimotorsiq.com');
INSERT INTO paymentterm (id,termname,value,ownerid) values ('9bab761c-4e23-4c8b-952b-9cb68aab8b84','60 Hari',60,'aslimotor@aslimotorsiq.com');
INSERT INTO paymentterm (id,termname,value,ownerid) values ('7239864e-b5b5-4645-a210-87cdcc3aa1e7','90 Hari',90,'aslimotor@aslimotorsiq.com');
INSERT INTO paymentterm (id,termname,value,ownerid) values ('e454d129-eb40-4649-9e98-f1e39bafca16','120 Hari',120,'aslimotor@aslimotorsiq.com');
INSERT INTO paymentterm (id,termname,value,ownerid) values ('d36e86fa-b464-4c53-88e8-1558049427b3','150 Hari',150,'aslimotor@aslimotorsiq.com');
INSERT INTO paymentterm (id,termname,value,ownerid) values ('1845e6a7-af1c-4a3d-9cee-91e547d29c35','180 Hari',180,'aslimotor@aslimotorsiq.com');

ALTER TABLE invoicesnapshot ADD COLUMN termid uuid;
ALTER TABLE invoicesnapshot ADD COLUMN termvalue integer;
ALTER TABLE invoicesnapshot ADD COLUMN banyakcicilan integer;
ALTER TABLE invoicesnapshot ADD COLUMN discount numeric;
ALTER TABLE invoicesnapshot DROP COLUMN invoiceno;
ALTER TABLE invoicesnapshot ADD COLUMN invoiceno character varying;

UPDATE invoicesnapshot set termid = '59abfe79-37b1-49f7-a272-c41a66300fb3', termvalue = 30 where status = 1;
UPDATE invoicesnapshot set banyakcicilan = lamaangsuran, discount = 0;

ALTER TABLE receive ADD COLUMN transactiondate timestamp with time zone;
ALTER TABLE receive ADD COLUMN deposit numeric;

ALTER TABLE customer ADD COLUMN deposit numeric;