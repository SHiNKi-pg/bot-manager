CREATE MATERIALIZED VIEW LOG ON TBL_EMOTION
WITH ROWID, SEQUENCE(BOTID, USERID, EMOTIONVALUE, CREATEDAT)
INCLUDING NEW VALUES;