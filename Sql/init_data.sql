-- https://shop.westerndigital.com/de-de/products/internal-drives/wd-red-sata-ssd#WDS200T1R0A
use MYSPECTOR;
-- --------- init enum/ types
INSERT INTO TARGET_TYPE(NAME, ID_TYPE) values 
        ('HTTP'   , 1), 
        ('SQL'    , 2), 
        ('FILE'   , 3),
        ('PROCESS', 4);
                                         
INSERT INTO XTRAX_TYPE(NAME, ID_TYPE) VALUES 
        ('Before'      ,  1),
        ('After'       ,  2),
        ('Between'     ,  3),
        ('TextToNumber',  4),
        ('Empty'       ,  5),
        ('TextReplace' ,  6),
        ('LengthOfText',  7),
        ('HtmlXpath'   ,  8),
        ('XmlXpath'    ,  9),
        ('JsonXpath'   , 10);
                                    
INSERT INTO CHECKER_TYPE(NAME) VALUES   ('NumberIsLess'),
									    ('NumberIsGreater'),
										('NumberIsEqual'),
										('NumberIsDifferent'),
										('TextDoContain'),
										('TextDoNotContain');
                                        
INSERT INTO NOTIFY_TYPE(NAME) VALUES ('Stub'),
									  ('Mail'),
                                      ('WebCallBack'),
                                      ('Twitter');
-- -----------------------------
select * from TARGET_TYPE;
select * from XTRAX_TYPE;
select * from CHECKER_TYPE;
select * from NOTIFY_TYPE;

--------------
-- delete from trox where ID_TROX <100;
INSERT INTO TROX(NAME, ENABLED, IS_DIRECTORY) values('Root'        , 1, 1);
SET  @ID_ROOT =LAST_INSERT_ID();
INSERT INTO TROX(NAME, ENABLED, IS_DIRECTORY) values('AllianzOblig', 1, 0);
SET  @ID_TROX =LAST_INSERT_ID();
INSERT INTO TROX_CLOSURE(ID_PARENT, ID_CHILD) values(@ID_ROOT, @ID_TROX);
------------------
SELECT * FROM TROX;
SELECT * FROM TROX_closure;

-- UPDATE TROX SET ENABLED=0 WHERE ID_TROX=7;
----------------------
INSERT INTO TARGET(ID_TARGET_TYPE) values(1);-- http
SET  @ID_HTTP =LAST_INSERT_ID();
INSERT INTO TARGET(ID_TARGET_TYPE) values(2);-- sql
SET  @ID_SQL =LAST_INSERT_ID();

INSERT into TARGET_HTTP(ID_TARGET, METHOD, URI) 
			values(@ID_HTTP, 'GET', 'https://allianz-fonds.webfg.net/sheet/fund/FR0013192572/730?date_entree=2018-04-04');
INSERT INTO TARGET_SQL(ID_TARGET, CONNECTION_STRING, QUERY, PROVIDER)
			values(@ID_SQL, 'demo connectionstring', 'demo sql query', 'SQL');
UPDATE TROX SET ID_TARGET=@ID_HTTP WHERE ID_TROX=@ID_TROX;

-- SELECT  LAST_INSERT_ID();
-- SET  @MY_ID =LAST_INSERT_ID();
-- SELECT  @MY_ID;

select * from TARGET tar
    inner join TARGET_TYPE typ on tar.ID_TARGET_TYPE = typ.ID_TYPE
    inner join TARGET_HTTP http on tar.ID_TARGET= http.ID_TARGET;
select * from TARGET tar
    inner join TARGET_TYPE typ on tar.ID_TARGET_TYPE = typ.ID_TYPE
    inner join TARGET_SQL _sql on tar.ID_TARGET= _sql.ID_TARGET;

------------------       
SET @ID_TROX=2;
SELECT @ID_TROX;

select * from xtrax_def where ID_TROX=@ID_TROX;
-- update xtrax_def set ID_xtrax_type = 8 where ID_xtrax_def = 1;

INSERT INTO xtrax_def(ID_TROX, `ORDER`, ID_XTRAX_TYPE, ARG)
			values(@ID_TROX, 0, 8, '{"Path":"/html/body/div[2]/div/header/div/div/div/div/div/div[1]/div[2]/div[1]/div[1]/div/span[3]"}');

INSERT INTO xtrax_def(ID_TROX, `ORDER`, ID_XTRAX_TYPE, ARG)
			values(@ID_TROX, 1, 4, null);

INSERT INTO checker_def(ID_TROX, `ORDER`, ID_CHECKER_TYPE, ARG) 
VALUES(@ID_TROX, 0, 2, '{"Reference":105, "OrEqual":true}');

INSERT INTO NOTIFY_DEF(ID_TROX, `ORDER`, ID_NOTIFY_TYPE, ARG) 
VALUES(@ID_TROX, 0, 1, '{"Message":"This message comes from DB"}');


-- display http target of trox 
select  @ID_TROX;
select * from target web 
inner join TARGET_TYPE web_type on web_type.ID_TYPE = web.ID_TARGET_TYPE
INNER JOIN TROX trox on trox.ID_TARGET = web.ID_TARGET
where trox.ID_TROX = @ID_TROX;
--
select * from target_http http where http.ID_TARGET = 1;
select * from target_sql sq where sq.ID_TARGET = 2;

-- Display all checkers
select * from checker_type;
select * from checker_def def 
	INNER JOIN checker_type typ on def.ID_CHECKER_TYPE = typ.ID_TYPE;

SELECT * FROM TROX;
update TROX set enabled = 1 where ID_TROX IN (2,6,7,8,9,10);
update TROX set enabled = 0 where ID_TROX IN (3);

-- display DEPENDENCIES of trox 
SET @ID_TROX = 37;
select *,@HTTP_ID=web.ID_WEB_TARGET from web_target web 
inner join WEB_TARGET_TYPE web_type on web_type.ID_TYPE = web.ID_WEB_TARGET_TYPE
INNER JOIN TROX trox on trox.ID_WEB_TARGET = web.ID_WEB_TARGET
where trox.ID_TROX = @ID_TROX;
SET @HTTP_ID = 48;
select * from web_target_http http where http.ID_WEB_TARGET = @HTTP_ID;

select * from xtrax_def def 
INNER JOIN xtrax_type typ on def.ID_XTRAX_TYPE = typ.ID_TYPE
WHERE def.ID_TROX = @ID_TROX   
ORDER BY `ORDER` ASC;
--
select * 
from checker_def def 
	INNER JOIN checker_type typ on def.ID_CHECKER_TYPE = typ.ID_TYPE 
    WHERE def.ID_TROX = @ID_TROX;  
--
select * from NOTIFY_DEF def 
	INNER JOIN NOTIFY_TYPE typ on def.ID_NOTIFY_TYPE = typ.ID_TYPE
    WHERE def.ID_TROX = @ID_TROX;  

INSERT INTO result_history(ID_TROX, ZE_TEXT, ZE_NUMBER, TIMESTAMP, LATENCY_MS)
VALUES(@ID_TROX, 'result text', 123, now(), 456);

select * from result_history where id_trox = 2;

alter TABLE `MYSPECTOR`.`RESULT_HISTORY` 
add column   `LOG` TEXT NULL;

select * from result_history where ID_RESULT = 87;