-- https://shop.westerndigital.com/de-de/products/internal-drives/wd-red-sata-ssd#WDS200T1R0A
use MYSPECTOR;

-- ----------- init enum/ types
INSERT INTO WEB_TARGET_TYPE(NAME) values('HTTP'), ('SQL');
INSERT INTO XTRAX_TYPE(NAME) VALUES ('Xpath'),
									('After'),
									('Before'),
									('TextToNumber'),
									('Between');

select * FROM CHECKER_TYPE; 
TRUNCATE TABLE CHECKER_TYPE; -- deletes the data inside a table, but not the table itself.                                   
INSERT INTO CHECKER_TYPE(NAME) VALUES   ('IsLess'),
									    ('IsGreater'),
										('NumberIsDifferent'),
										('NumberIsEqual'),
										('NumberIsGreater'),
										('NumberIsLess'),
										('TextDoNotContain'),
										('TextDoContain');
                                        
INSERT INTO NOTIFY_TYPE(NAME) VALUES ('Stub'),
									  ('Mail'),
                                      ('WebCallBack'),
                                      ('Twitter');
-- -----------------------------
select * from WEB_TARGET_TYPE;
select * from XTRAX_TYPE;
select * from CHECKER_TYPE;

-- delete from trox where ID_TROX <100;
INSERT INTO TROX(NAME, ENABLED, IS_DIRECTORY) values('Root'        , 1, 1);
INSERT INTO TROX(NAME, ENABLED, IS_DIRECTORY) values('AllianzOblig', 1, 0);
INSERT INTO TROX_CLOSURE(ID_PARENT, ID_CHILD) values(1, 2);
SELECT * FROM TROX;
SELECT * FROM TROX_closure;
----------------------

INSERT INTO WEB_TARGET(ID_WEB_TARGET_TYPE) values(1);-- http
INSERT INTO WEB_TARGET(ID_WEB_TARGET_TYPE) values(2);-- sql

INSERT into WEB_TARGET_HTTP(ID_WEB_TARGET, METHOD, URI) 
			values(1, 'GET', 'https://allianz-fonds.webfg.net/sheet/fund/FR0013192572/730?date_entree=2018-04-04');
INSERT INTO WEB_TARGET_SQL(ID_WEB_TARGET, CONNECTION_STRING, QUERY, PROVIDER)
			values(2, 'demo connectionstring', 'demo sql query', 'SQL');

SELECT  LAST_INSERT_ID();
select * from WEB_TARGET tar
    inner join WEB_TARGET_TYPE typ on tar.ID_WEB_TARGET_TYPE = typ.ID_WEB_TARGET_TYPE;           
select * from WEB_TARGET_HTTP;
select * from WEB_TARGET_SQL;

UPDATE TROX SET ID_WEB_TARGET=1 WHERE ID_TROX=2;
------------------       
           
INSERT INTO xtrax_def(ID_TROX, `ORDER`, ID_XTRAX_TYPE, ARG)
			values(2, 0, 1, '{"Path":"/html/body/div[2]/div/header/div/div/div/div/div/div[1]/div[2]/div[1]/div[1]/div/span[3]"}');
INSERT INTO xtrax_def(ID_TROX, `ORDER`, ID_XTRAX_TYPE, ARG)
			values(2, 1, 4, null);

--------------------
INSERT INTO checker_def(ID_TROX, `ORDER`, ID_CHECKER_TYPE, ARG) 
VALUES(2, 0, 2, '{"Reference":105, "OrEqual":true}');

INSERT INTO NOTIFY_DEF(ID_TROX, `ORDER`, ID_NOTIFY_TYPE, ARG) 
VALUES(2, 0, 1, '{"Message":"This message comes from DB"}');


-- display http target of trox 
select * from web_target web 
inner join WEB_TARGET_TYPE web_type on web_type.ID_WEB_TARGET_TYPE = web.ID_WEB_TARGET_TYPE
INNER JOIN TROX trox on trox.ID_WEB_TARGET = web.ID_WEB_TARGET
where trox.ID_TROX = 2;
--
select * from web_target_http http where http.ID_WEB_TARGET = 1;
select * from web_target_sql sq where sq.ID_WEB_TARGET = 2;

-- Display all checkers
select * from checker_type;
select *
from checker_def def 
	INNER JOIN checker_type typ on def.ID_CHECKER_TYPE = typ.ID_CHECKER_TYPE ;


-- display Xtrax Components of trox 
SET @ID_TROX = 19;
select * from xtrax_def def 
INNER JOIN xtrax_type typ on def.ID_XTRAX_TYPE = typ.ID_XTRAX_TYPE
WHERE def.ID_TROX = @ID_TROX;    
--
select * -- def.ID_TROX, def.ORDER, def.ARG, typ.ID_CHECKER_TYPE, typ.NAME 
from checker_def def 
	INNER JOIN checker_type typ on def.ID_CHECKER_TYPE = typ.ID_CHECKER_TYPE 
    WHERE def.ID_TROX = @ID_TROX;  
--
select * from NOTIFY_DEF def 
	INNER JOIN NOTIFY_TYPE typ on def.ID_NOTIFY_TYPE = typ.ID_NOTIFY_TYPE
--    INNER JOIN  trox on trox.ID_TROX = def.ID_TROX
    WHERE def.ID_TROX = @ID_TROX;  

---------------------

create table tjson(param JSON);
insert into tjson values('{"Reference":105, "OrEqual":true}');
select * from tjson;
----------------------

