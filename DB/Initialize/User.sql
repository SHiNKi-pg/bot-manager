CREATE USER BOTMANAGER
IDENTIFIED BY "botmanage" --TODO: パスワードは適宜変更する
DEFAULT TABLESPACE TS_BOTMANAGE
;

-- 表領域 TS_BOTMANAGEを無制限に使用できるようにする
ALTER USER BOTMANAGER QUOTA UNLIMITED ON TS_BOTMANAGE;