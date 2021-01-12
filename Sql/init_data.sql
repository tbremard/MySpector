-- https://shop.westerndigital.com/de-de/products/internal-drives/wd-red-sata-ssd#WDS200T1R0A
use MYSPECTOR;
SELECT * FROM myspector.trox;
SELECT * FROM myspector.trox_closure;
-- ----------- init enum/ types
INSERT INTO WEB_TARGET_TYPE(NAME) values('HTTP'), ('SQL');
INSERT INTO XTRAX_TYPE(NAME) VALUES ('Xpath'),
									('After'),
									('Before'),
									('TextToNumber'),
									('Between');
INSERT INTO CHECKER_TYPE(NAME) VALUES ('IsLess'),
									  ('IsGreater');
INSERT INTO NOTIFY_TYPE(NAME) VALUES ('Stub'),
									  ('Mail'),
                                      ('WebCallBack'),
                                      ('Twitter');
-- -----------------------------
select * from WEB_TARGET_TYPE;
select * from XTRAX_TYPE;
select * from CHECKER_TYPE;


delete from trox where ID_TROX <100;
INSERT INTO TROX(NAME, ENABLED, IS_DIRECTORY) values('Root', 1, 1);
INSERT INTO TROX(NAME, ENABLED, IS_DIRECTORY) values('AllianzOblig', 1, 0);
INSERT INTO TROX_CLOSURE(ID_PARENT, ID_CHILD) values(3, 4);
INSERT INTO myspector.web_target(ID_TROX, ID_WEB_TARGET_TYPE) values(4, 1);
select * from myspector.web_target;
INSERT into myspector.web_target_http(ID_WEB_TARGET, METHOD, URI, VERSION, HEADERS, CONTENT) 
			values(1, 'GET', 'https://allianz-fonds.webfg.net/sheet/fund/FR0013192572/730?date_entree=2018-04-04', null, null,null);
select * from myspector.web_target_http;

            
INSERT INTO xtrax_def(ID_TROX, `ORDER`, ID_XTRAX_TYPE, ARG)
			values(4, 0, 1, '{"Path":"/html/body/div[2]/div/header/div/div/div/div/div/div[1]/div[2]/div[1]/div[1]/div/span[3]"}');
INSERT INTO xtrax_def(ID_TROX, `ORDER`, ID_XTRAX_TYPE, ARG)
			values(4, 1, 4, null);


INSERT INTO checker_def(ID_TROX, `ORDER`, ID_CHECKER_TYPE, ARG) 
VALUES(4, 0, 2, '{"Reference":105, "OrEqual":true}');

INSERT INTO NOTIFY_DEF(ID_TROX, `ORDER`, ID_NOTIFY_TYPE, ARG) 
VALUES(4, 0, 1, '{"Message":"This message comes from DB"}');


-- display http target of trox 
select * FROM TROX 
inner join web_target web on trox.ID_TROX = web.ID_TROX
inner join WEB_TARGET_TYPE web_type on web_type.ID_WEB_TARGET_TYPE = web.ID_WEB_TARGET_TYPE
inner join web_target_http http on http.ID_WEB_TARGET = web.ID_WEB_TARGET ;
-- display Xtrax Components of trox 
select * from xtrax_def def 
	INNER JOIN xtrax_type typ on def.ID_XTRAX_TYPE = typ.ID_XTRAX_TYPE
    INNER JOIN  trox on trox.ID_TROX = def.ID_TROX;
--
select * from checker_def def 
	INNER JOIN checker_type typ on def.ID_CHECKER_TYPE = typ.ID_CHECKER_TYPE 
    INNER JOIN  trox on trox.ID_TROX = def.ID_TROX;
--
select * from NOTIFY_DEF def 
	INNER JOIN NOTIFY_TYPE typ on def.ID_NOTIFY_TYPE = typ.ID_NOTIFY_TYPE
    INNER JOIN  trox on trox.ID_TROX = def.ID_TROX;





create table tjson(param JSON);
insert into tjson values('{"Reference":105, "OrEqual":true}');
select * from tjson;


