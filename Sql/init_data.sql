-- https://shop.westerndigital.com/de-de/products/internal-drives/wd-red-sata-ssd#WDS200T1R0A
use MYSPECTOR;
-- --------- init enum/ types
INSERT INTO TARGET_TYPE(NAME) values 
        ('HTTP'   , 1), 
        ('SQL'    , 2), 
        ('FILE'   , 3),
        ('PROCESS', 4);
                                         
INSERT INTO XTRAX_TYPE(NAME) VALUES 
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
select * from WEB_TARGET_TYPE;
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

UPDATE TROX SET ENABLED=0 WHERE ID_TROX=7;
----------------------
INSERT INTO WEB_TARGET(ID_WEB_TARGET_TYPE) values(1);-- http
SET  @ID_HTTP =LAST_INSERT_ID();
INSERT INTO WEB_TARGET(ID_WEB_TARGET_TYPE) values(2);-- sql
INSERT into WEB_TARGET_HTTP(ID_WEB_TARGET, METHOD, URI) 
			values(1, 'GET', 'https://allianz-fonds.webfg.net/sheet/fund/FR0013192572/730?date_entree=2018-04-04');
INSERT INTO WEB_TARGET_SQL(ID_WEB_TARGET, CONNECTION_STRING, QUERY, PROVIDER)
			values(2, 'demo connectionstring', 'demo sql query', 'SQL');
UPDATE TROX SET ID_WEB_TARGET=@ID_HTTP WHERE ID_TROX=@ID_TROX;

-- SELECT  LAST_INSERT_ID();
-- SET  @MY_ID =LAST_INSERT_ID();
-- SELECT  @MY_ID;

select * from WEB_TARGET tar
    inner join WEB_TARGET_TYPE typ on tar.ID_WEB_TARGET_TYPE = typ.ID_TYPE;           
select * from WEB_TARGET_HTTP;
select * from WEB_TARGET_SQL;


------------------       
SET @ID_TROX=2;
SELECT @ID_TROX;

INSERT INTO xtrax_def(ID_TROX, `ORDER`, ID_XTRAX_TYPE, ARG)
			values(@ID_TROX, 0, 1, '{"Path":"/html/body/div[2]/div/header/div/div/div/div/div/div[1]/div[2]/div[1]/div[1]/div/span[3]"}');

INSERT INTO xtrax_def(ID_TROX, `ORDER`, ID_XTRAX_TYPE, ARG)
			values(@ID_TROX, 1, 4, null);

INSERT INTO checker_def(ID_TROX, `ORDER`, ID_CHECKER_TYPE, ARG) 
VALUES(@ID_TROX, 0, 2, '{"Reference":105, "OrEqual":true}');

INSERT INTO NOTIFY_DEF(ID_TROX, `ORDER`, ID_NOTIFY_TYPE, ARG) 
VALUES(@ID_TROX, 0, 1, '{"Message":"This message comes from DB"}');


-- display http target of trox 
select  @ID_TROX;
select * from web_target web 
inner join WEB_TARGET_TYPE web_type on web_type.ID_TYPE = web.ID_WEB_TARGET_TYPE
INNER JOIN TROX trox on trox.ID_WEB_TARGET = web.ID_WEB_TARGET
where trox.ID_TROX = @ID_TROX;
--
select * from web_target_http http where http.ID_WEB_TARGET = 1;
select * from web_target_sql sq where sq.ID_WEB_TARGET = 2;


-- Display all checkers
select * from checker_type;
select * from checker_def def 
	INNER JOIN checker_type typ on def.ID_CHECKER_TYPE = typ.ID_TYPE;

SELECT * FROM TROX;
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
--    INNER JOIN  trox on trox.ID_TROX = def.ID_TROX
    WHERE def.ID_TROX = @ID_TROX;  


                                    