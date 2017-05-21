CREATE DATABASE  IF NOT EXISTS `db_dailydoing` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `db_dailydoing`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: 10.10.1.3    Database: db_dailydoing
-- ------------------------------------------------------
-- Server version	5.5.54-0+deb8u1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_contacts`
--

DROP TABLE IF EXISTS `tbl_contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_contacts` (
  `cid` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL,
  `name` text NOT NULL,
  `firstname` text NOT NULL,
  `mail` text,
  `street` text,
  `housenr` varchar(4) DEFAULT NULL,
  `postcode` varchar(5) DEFAULT NULL,
  `city` text,
  `tel` varchar(20) DEFAULT NULL,
  `mobile` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`cid`),
  KEY `tbl_contacts_uid_idx` (`uid`),
  CONSTRAINT `tbl_contacts_uid` FOREIGN KEY (`uid`) REFERENCES `tbl_user` (`uid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=46 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_contacts`
--

LOCK TABLES `tbl_contacts` WRITE;
/*!40000 ALTER TABLE `tbl_contacts` DISABLE KEYS */;
INSERT INTO `tbl_contacts` VALUES (5,2,'Hödl','Marcel','mhoedl@bos.de','Marienstraße','11','70736','Fellbach-Oeffingen','071193348938','015785419350'),(11,2,'Hödl','Alex','ahoedl@gerok.de','Danneralle','33','15913','Briesensee','04654231','55555'),(16,2,'Schietinger','Achim','aschietinger@bos.de','Bachweg','1','70756','Hintertupfingen','666','88'),(18,2,'Persin','Armando','apersin@bos.de','Stricherstreet','6','12345','Stuttgart','666','88'),(19,2,'Rostan','Maurice','maurice.rostan@gmx.de','Lortzingstraße','3','70736','Fellbach','1234567890','00'),(20,2,'Schmelzer','Richard','rschmelzer@bos.de','lalastreet','00','45454','Tupfingen','666','88'),(21,2,'Hödl','Caro','choedl@hotmail.de','Marienstraße','17','70736','Fellbach-Oeffingen','071193348938','-'),(22,2,'Sagolla','Michael','msagolla@bos.de','Hinter der Mauer','8','70794','Filderstadt','2312312312323','56564'),(34,2,'Grundhaußer','Jessica','','afdas','546','456','Fellbach-Schmiden','',''),(40,2,'Beyerle','Nina','','<3 <3','','','Fellbach-Oeffingen','',''),(41,3,'anton','bert','','','','','','',''),(43,2,'Zschorsch','Dorian','','','','','Fellbach','',''),(44,2,'Masurek','Felix','felix@bla.de','Chillartistenstreet','35','08145','Bauernhausen','018054646','0800 666666'),(45,6,'Rothe','Niclas','','','','','Leipzig','','');
/*!40000 ALTER TABLE `tbl_contacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_lendings`
--

DROP TABLE IF EXISTS `tbl_lendings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_lendings` (
  `lid` int(11) NOT NULL AUTO_INCREMENT,
  `uid` int(11) NOT NULL,
  `cid` int(11) NOT NULL,
  `title` varchar(45) NOT NULL,
  `description` text,
  `category` enum('Pornos','DVD','Schulunterlagen','Kasette','Sonstiges') DEFAULT NULL,
  `priority` enum('high','medium','low') DEFAULT NULL,
  `timestamp_lend` datetime NOT NULL,
  `timestamp_lendback` datetime DEFAULT NULL,
  `get_back` enum('false','true') NOT NULL DEFAULT 'false',
  PRIMARY KEY (`lid`),
  UNIQUE KEY `lid_UNIQUE` (`lid`),
  KEY `tbl_lendings_ne_idx` (`uid`),
  KEY `tbl_lendings_new_cid_idx` (`cid`),
  CONSTRAINT `lendings_new_cid` FOREIGN KEY (`cid`) REFERENCES `tbl_contacts` (`cid`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `lendings_new_uid` FOREIGN KEY (`uid`) REFERENCES `tbl_user` (`uid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_lendings`
--

LOCK TABLES `tbl_lendings` WRITE;
/*!40000 ALTER TABLE `tbl_lendings` DISABLE KEYS */;
INSERT INTO `tbl_lendings` VALUES (1,2,16,'Metallica Ticket','wird fett','Sonstiges','high','2009-05-20 17:00:00','2028-04-20 18:00:00','true'),(3,2,11,'CoD','34','DVD','','2017-04-27 00:00:00','2017-04-27 00:00:00','false'),(5,2,16,'Kaffee','1 Euro für Kaffee','Sonstiges','low','2009-05-20 17:00:00','2009-05-20 17:00:00','true'),(7,2,19,'Gin','Gin für fette fette Party','Pornos','low','2010-05-20 17:00:00','2010-05-20 17:00:00','true'),(8,2,18,'Dildo','der Große (gewaschen zurück)','Pornos','high','2010-05-20 17:00:00','2011-05-20 17:00:00','true'),(9,2,22,'Sonnenbrille','ReyBerry','Sonstiges','low','2011-05-20 17:00:00','2011-05-20 17:00:00','true'),(11,2,21,'Hut','mit Krampe','Pornos','medium','2017-05-11 00:00:00','2011-05-20 17:00:00','false'),(13,2,34,'Kekes','Schokiii','Pornos','medium','2012-05-20 17:00:00','2012-05-20 17:00:00','false'),(14,2,11,'Drohne','speedy mit Blechschild xD','Sonstiges','low','2017-05-12 00:00:00','2012-05-20 17:00:00','false'),(15,2,40,'Mein Herz','','Pornos','medium','2017-05-12 00:00:00','2012-05-20 17:00:00','false'),(16,3,41,'ARI 10 €','will haben','','','2017-05-12 00:00:00','2012-05-20 17:00:00','false'),(20,6,45,'Payback','15.00€ Kinoticket ( Guardians of the Galaxy Vol.2)','Sonstiges','low','2017-05-15 00:00:00','2017-07-15 00:00:00','false'),(21,2,40,'1313','13123','DVD','','2017-05-18 00:00:00','2017-05-18 00:00:00','true');
/*!40000 ALTER TABLE `tbl_lendings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_user`
--

DROP TABLE IF EXISTS `tbl_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_user` (
  `uid` int(11) NOT NULL AUTO_INCREMENT,
  `username` text NOT NULL,
  `password` text NOT NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_user`
--

LOCK TABLES `tbl_user` WRITE;
/*!40000 ALTER TABLE `tbl_user` DISABLE KEYS */;
INSERT INTO `tbl_user` VALUES (1,'mv','123'),(2,'mhdl','sae123'),(3,'aschietinger','Testen123'),(4,'rschmelzer','Testen123'),(5,'apersin','Testen123'),(6,'Kartoffelkönig','123'),(7,'cberg','123');
/*!40000 ALTER TABLE `tbl_user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-18 23:01:23
