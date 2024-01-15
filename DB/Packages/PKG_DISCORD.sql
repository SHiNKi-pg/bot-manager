CREATE OR REPLACE PACKAGE PKG_DISCORD AS
	-- MisskeyからDiscordへ移行
	PROCEDURE TRANSFER_FROM_MISSKEY(IN_USERID IN NUMBER, IN_MISSKEYUSERID IN VARCHAR2);
END;
/

CREATE OR REPLACE PACKAGE BODY PKG_DISCORD AS
	PROCEDURE TRANSFER_FROM_MISSKEY(IN_USERID IN NUMBER, IN_MISSKEYUSERID IN VARCHAR2) IS
		CURSOR CUR_MISSKEY_USER(I_USERID IN NUMBER) IS
			SELECT
				USERID
			FROM
				MST_MISSKEY_USER
			WHERE
				MISSKEYUSERID = I_USERID
		;
		M_USERID NUMBER(12);
	BEGIN
		FOR rec IN CUR_MISSKEY_USER(IN_MISSKEYUSERID) LOOP
			-- USERIDの取得
			M_USERID := rec.USERID;

			-- MISSKEYの更新
			UPDATE MST_MISSKEY_USER
			SET USERID = IN_USERID
			WHERE MISSKEYUSERID = IN_MISSKEYUSERID;

			-- 感情テーブルの更新
			UPDATE TBL_EMOTION
			SET USERID = IN_USERID
			WHERE USERID = M_USERID;

			-- 最初の1回だけ実行する
			EXIT;
		END LOOP;
	
	-- マテリアライズドビューの強制リフレッシュ
	DBMS_MVIEW.REFRESH('MV_EMOTION', '?');
	
	END TRANSFER_FROM_MISSKEY;

END PKG_DISCORD;
/