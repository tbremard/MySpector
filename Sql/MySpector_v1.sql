-- MySQL Script generated by MySQL Workbench
-- Wed Mar 24 22:28:05 2021
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema MYSPECTOR
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `MYSPECTOR` ;

-- -----------------------------------------------------
-- Schema MYSPECTOR
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `MYSPECTOR` DEFAULT CHARACTER SET utf8 ;
USE `MYSPECTOR` ;

-- -----------------------------------------------------
-- Table `MYSPECTOR`.`TARGET_TYPE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`TARGET_TYPE` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`TARGET_TYPE` (
  `ID_TYPE` INT NOT NULL AUTO_INCREMENT,
  `NAME` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_TYPE`, `NAME`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`TARGET`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`TARGET` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`TARGET` (
  `ID_TARGET` INT NOT NULL AUTO_INCREMENT,
  `ID_TARGET_TYPE` INT NOT NULL,
  `NAME` VARCHAR(100) NULL,
  PRIMARY KEY (`ID_TARGET`),
  INDEX `FK_ID_WEB_TARGET_TYPE_idx` (`ID_TARGET_TYPE` ASC) VISIBLE,
  CONSTRAINT `FK_ID_WEB_TARGET_TYPE`
    FOREIGN KEY (`ID_TARGET_TYPE`)
    REFERENCES `MYSPECTOR`.`TARGET_TYPE` (`ID_TYPE`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`TROX`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`TROX` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`TROX` (
  `ID_TROX` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `NAME` VARCHAR(100) NOT NULL,
  `ENABLED` TINYINT NULL DEFAULT 1,
  `IS_DIRECTORY` TINYINT NULL DEFAULT 0,
  `ID_TARGET` INT NULL,
  `DESCRIPTION` TEXT NULL,
  PRIMARY KEY (`ID_TROX`),
  INDEX `FK_ID_WEB_TARGET_idx` (`ID_TARGET` ASC) VISIBLE,
  CONSTRAINT `FK_ID_WEB_TARGET`
    FOREIGN KEY (`ID_TARGET`)
    REFERENCES `MYSPECTOR`.`TARGET` (`ID_TARGET`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`TARGET_HTTP`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`TARGET_HTTP` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`TARGET_HTTP` (
  `ID_TARGET` INT NOT NULL,
  `METHOD` ENUM('GET', 'POST', 'PUT', 'DELETE') NOT NULL DEFAULT 'GET',
  `URI` TEXT NOT NULL,
  `VERSION` VARCHAR(5) NOT NULL DEFAULT '1.1',
  `HEADERS` TEXT NULL,
  `CONTENT` TEXT NULL,
  INDEX `FK_ID_WEB_TARGET_idx` (`ID_TARGET` ASC) VISIBLE,
  PRIMARY KEY (`ID_TARGET`),
  CONSTRAINT `FK_ID_WEB_TARGET1`
    FOREIGN KEY (`ID_TARGET`)
    REFERENCES `MYSPECTOR`.`TARGET` (`ID_TARGET`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`XTRAX_TYPE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`XTRAX_TYPE` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`XTRAX_TYPE` (
  `ID_TYPE` INT NOT NULL AUTO_INCREMENT,
  `NAME` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_TYPE`, `NAME`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`XTRAX_DEF`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`XTRAX_DEF` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`XTRAX_DEF` (
  `ID_XTRAX_DEF` INT NOT NULL AUTO_INCREMENT,
  `ID_TROX` INT UNSIGNED NOT NULL,
  `ORDER` INT NULL,
  `ID_XTRAX_TYPE` INT NOT NULL,
  `ARG` TEXT NULL,
  PRIMARY KEY (`ID_XTRAX_DEF`),
  INDEX `FK_ID_XTRAX_TYPE_idx` (`ID_XTRAX_TYPE` ASC) VISIBLE,
  INDEX `FK_ID_PIPELINE_idx` (`ID_TROX` ASC) VISIBLE,
  CONSTRAINT `FK_ID_XTRAX_TYPE`
    FOREIGN KEY (`ID_XTRAX_TYPE`)
    REFERENCES `MYSPECTOR`.`XTRAX_TYPE` (`ID_TYPE`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ID_TROX2`
    FOREIGN KEY (`ID_TROX`)
    REFERENCES `MYSPECTOR`.`TROX` (`ID_TROX`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`CHECKER_TYPE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`CHECKER_TYPE` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`CHECKER_TYPE` (
  `ID_TYPE` INT NOT NULL AUTO_INCREMENT,
  `NAME` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_TYPE`, `NAME`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`CHECKER_DEF`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`CHECKER_DEF` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`CHECKER_DEF` (
  `ID_CHECKER_DEF` INT NOT NULL AUTO_INCREMENT,
  `ID_TROX` INT UNSIGNED NOT NULL,
  `ORDER` INT NULL,
  `ID_CHECKER_TYPE` INT NOT NULL,
  `ARG` TEXT NULL,
  PRIMARY KEY (`ID_CHECKER_DEF`),
  INDEX `FK_ID_XTRAX_TYPE_idx` (`ID_CHECKER_TYPE` ASC) VISIBLE,
  INDEX `FK_ID_PIPELINE_idx` (`ID_TROX` ASC) VISIBLE,
  CONSTRAINT `FK_ID_CHECKER_TYPE`
    FOREIGN KEY (`ID_CHECKER_TYPE`)
    REFERENCES `MYSPECTOR`.`CHECKER_TYPE` (`ID_TYPE`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ID_TROX3`
    FOREIGN KEY (`ID_TROX`)
    REFERENCES `MYSPECTOR`.`TROX` (`ID_TROX`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`NOTIFY_TYPE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`NOTIFY_TYPE` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`NOTIFY_TYPE` (
  `ID_TYPE` INT NOT NULL AUTO_INCREMENT,
  `NAME` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_TYPE`, `NAME`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`NOTIFY_DEF`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`NOTIFY_DEF` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`NOTIFY_DEF` (
  `ID_NOTIFY_DEF` INT NOT NULL AUTO_INCREMENT,
  `ID_TROX` INT UNSIGNED NOT NULL,
  `ORDER` INT NULL,
  `ID_NOTIFY_TYPE` INT NOT NULL,
  `ARG` TEXT NULL,
  PRIMARY KEY (`ID_NOTIFY_DEF`),
  INDEX `FK_ID_PIPELINE_idx` (`ID_TROX` ASC) VISIBLE,
  INDEX `FK_ID_NOTIFY_TYPE_idx` (`ID_NOTIFY_TYPE` ASC) VISIBLE,
  CONSTRAINT `FK_ID_NOTIFY_TYPE`
    FOREIGN KEY (`ID_NOTIFY_TYPE`)
    REFERENCES `MYSPECTOR`.`NOTIFY_TYPE` (`ID_TYPE`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ID_TROX4`
    FOREIGN KEY (`ID_TROX`)
    REFERENCES `MYSPECTOR`.`TROX` (`ID_TROX`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`TARGET_SQL`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`TARGET_SQL` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`TARGET_SQL` (
  `ID_TARGET` INT NOT NULL,
  `CONNECTION_STRING` TEXT NOT NULL,
  `QUERY` TEXT NOT NULL,
  `PROVIDER` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ID_TARGET`),
  INDEX `FK_ID_WEB_TARGET_idx` (`ID_TARGET` ASC) VISIBLE,
  CONSTRAINT `FK_ID_WEB_TARGET2`
    FOREIGN KEY (`ID_TARGET`)
    REFERENCES `MYSPECTOR`.`TARGET` (`ID_TARGET`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`TROX_CLOSURE`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`TROX_CLOSURE` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`TROX_CLOSURE` (
  `ID_PARENT` INT UNSIGNED NOT NULL,
  `ID_CHILD` INT UNSIGNED NOT NULL,
  INDEX `FK_ID_PARENT_idx` (`ID_PARENT` ASC) VISIBLE,
  INDEX `FK_ID_CHILD_idx` (`ID_CHILD` ASC) VISIBLE,
  CONSTRAINT `FK_ID_PARENT`
    FOREIGN KEY (`ID_PARENT`)
    REFERENCES `MYSPECTOR`.`TROX` (`ID_TROX`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ID_CHILD`
    FOREIGN KEY (`ID_CHILD`)
    REFERENCES `MYSPECTOR`.`TROX` (`ID_TROX`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `MYSPECTOR`.`RESULT_HISTORY`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `MYSPECTOR`.`RESULT_HISTORY` ;

CREATE TABLE IF NOT EXISTS `MYSPECTOR`.`RESULT_HISTORY` (
  `ID_RESULT` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `ID_TROX` INT UNSIGNED NOT NULL,
  `TIMESTAMP` DATETIME NOT NULL,
  `LATENCY_MS` INT UNSIGNED NOT NULL DEFAULT 0,
  `IN_DATA` TEXT NULL,
  `OUT_TEXT` TEXT NULL,
  `OUT_NUMBER` DECIMAL(20,10) NULL,
  `GRAB_SUCCESS` TINYINT(1) NULL,
  `XTRAX_SUCCESS` TINYINT(1) NULL,
  `IS_SIGNALED` TINYINT(1) NULL,
  `ERROR_MSG` TEXT NULL,
  PRIMARY KEY (`ID_RESULT`),
  INDEX `FK_ID_TROX_789_idx` (`ID_TROX` ASC) VISIBLE,
  CONSTRAINT `FK_ID_TROX_789`
    FOREIGN KEY (`ID_TROX`)
    REFERENCES `MYSPECTOR`.`TROX` (`ID_TROX`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
