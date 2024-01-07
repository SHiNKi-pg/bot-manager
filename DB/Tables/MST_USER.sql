CREATE TABLE MST_USER(
	USERID NUMBER(12) DEFAULT SEQ_MST_USER.NEXTVAL NOT NULL,
	USERNAME NVARCHAR2(64) NOT NULL,
	CREATEDAT DATE DEFAULT SYSDATE NOT NULL,
	PRIMARY KEY(USERID)
);