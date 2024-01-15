CREATE TABLE MST_USER_NAME (
	USERID NUMBER(12) NOT NULL,
	LEFTNAME NVARCHAR2(64),
	MIDDLENAME NVARCHAR2(64),
	RIGHTNAME NVARCHAR2(64),
	PRIMARY KEY(USERID),
	FOREIGN KEY(USERID) REFERENCES MST_USER(USERID)
);