-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: 103.107.66.140    Database: klmpnhomestay_db
-- ------------------------------------------------------
-- Server version	5.6.51-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tm_block`
--

DROP TABLE IF EXISTS `tm_block`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_block` (
  `Block_Id` char(36) NOT NULL,
  `Block_Code` int(10) NOT NULL,
  `Block_Name` varchar(50) NOT NULL,
  `Country_Id` char(36) NOT NULL,
  `State_Id` char(36) NOT NULL,
  `District_Id` char(36) NOT NULL,
  `Is_Active` bit(1) NOT NULL DEFAULT b'1',
  `Created_By` char(36) NOT NULL,
  `Created_On` datetime NOT NULL,
  `Modified_By` char(36) DEFAULT NULL,
  `Modified_On` datetime DEFAULT NULL,
  PRIMARY KEY (`Block_Id`),
  UNIQUE KEY `BlockCode_UNIQUE` (`Block_Code`),
  UNIQUE KEY `BlockName_UNIQUE` (`Block_Name`),
  KEY `FK_tmBlock_Created_By_Idx` (`Created_By`),
  KEY `FK_tmBlock_Modified_By_Idx` (`Modified_By`),
  KEY `FK_tmBlock_Country_Id_Idx` (`Country_Id`),
  KEY `FK_tmBlock_State_Id_Idx` (`State_Id`),
  KEY `FK_tmBlock_District_Id_Idx` (`District_Id`),
  CONSTRAINT `FK1_tmBlock_tmUser` FOREIGN KEY (`Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK2_tmBlock_tmUser` FOREIGN KEY (`Modified_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmBlock_tmCountry` FOREIGN KEY (`Country_Id`) REFERENCES `tm_country` (`Country_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmBlock_tmDistrict` FOREIGN KEY (`District_Id`) REFERENCES `tm_district` (`District_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmBlock_tmState` FOREIGN KEY (`State_Id`) REFERENCES `tm_state` (`State_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_block_village`
--

DROP TABLE IF EXISTS `tm_block_village`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_block_village` (
  `Vill_Id` char(36) NOT NULL,
  `Vill_Code` int(10) NOT NULL,
  `Vill_Name` varchar(50) NOT NULL,
  `Country_Id` char(36) NOT NULL,
  `State_Id` char(36) NOT NULL,
  `District_Id` char(36) NOT NULL,
  `Block_Id` char(36) NOT NULL,
  `Is_Active` bit(1) NOT NULL DEFAULT b'1',
  `Created_By` char(36) NOT NULL,
  `Created_On` datetime NOT NULL,
  `Modified_By` char(36) DEFAULT NULL,
  `Modified_On` datetime DEFAULT NULL,
  PRIMARY KEY (`Vill_Id`),
  UNIQUE KEY `VillCode_UNIQUE` (`Vill_Code`),
  UNIQUE KEY `VillName_UNIQUE` (`Vill_Name`),
  KEY `FK_tmBlockVillage_Created_By_Idx` (`Created_By`),
  KEY `FK_tmBlockVillage_Modified_By_Idx` (`Modified_By`),
  KEY `FK_tmBlockVillage_Country_Id_Idx` (`Country_Id`),
  KEY `FK_tmBlockVillage_State_Id_Idx` (`State_Id`),
  KEY `FK_tmBlockVillage_District_Id_Idx` (`District_Id`),
  KEY `FK_tmBlockVillage_Block_Id_Idx` (`Block_Id`),
  CONSTRAINT `FK1_tmBlockVillage_tmUser` FOREIGN KEY (`Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK2_tmBlockVillage_tmUser` FOREIGN KEY (`Modified_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmBlockVillage_tmBlock` FOREIGN KEY (`Block_Id`) REFERENCES `tm_block` (`Block_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmBlockVillage_tmCountry` FOREIGN KEY (`Country_Id`) REFERENCES `tm_country` (`Country_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmBlockVillage_tmDistrict` FOREIGN KEY (`District_Id`) REFERENCES `tm_district` (`District_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmBlockVillage_tmState` FOREIGN KEY (`State_Id`) REFERENCES `tm_state` (`State_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_country`
--

DROP TABLE IF EXISTS `tm_country`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_country` (
  `Country_Id` char(36) NOT NULL,
  `Country_Code` int(10) NOT NULL,
  `Country_Name` varchar(50) DEFAULT NULL,
  `Is_Active` bit(1) NOT NULL DEFAULT b'1',
  `Created_By` char(36) NOT NULL,
  `Created_On` datetime NOT NULL,
  `Modified_By` char(36) DEFAULT NULL,
  `Modified_On` datetime DEFAULT NULL,
  PRIMARY KEY (`Country_Id`),
  UNIQUE KEY `CountryCode_UNIQUE` (`Country_Code`),
  UNIQUE KEY `CountryName_UNIQUE` (`Country_Name`),
  KEY `FK_tmCountry_Created_By_Idx` (`Created_By`),
  KEY `FK_tmCountry_Modified_By_Idx` (`Modified_By`),
  CONSTRAINT `FK1_tmCountry_tmUser` FOREIGN KEY (`Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK2_tmCountry_tmUser` FOREIGN KEY (`Modified_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_district`
--

DROP TABLE IF EXISTS `tm_district`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_district` (
  `District_Id` char(36) NOT NULL,
  `District_Code` int(10) NOT NULL,
  `District_Name` varchar(50) NOT NULL,
  `Country_Id` char(36) NOT NULL,
  `State_Id` char(36) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  `Created_By` char(36) NOT NULL,
  `Created_On` datetime NOT NULL,
  `Modified_By` char(36) DEFAULT NULL,
  `Modified_On` datetime DEFAULT NULL,
  PRIMARY KEY (`District_Id`),
  UNIQUE KEY `DistrictCode_UNIQUE` (`District_Code`),
  UNIQUE KEY `DistrictName_UNIQUE` (`District_Name`),
  KEY `FK_tmDistrict_Created_By_Idx` (`Created_By`),
  KEY `FK_tmDistrict_Modified_By_Idx` (`Modified_By`),
  KEY `FK_tmDistrict_Country_Id_Idx` (`Country_Id`),
  KEY `FK_tmDistrict_State_Id_Idx` (`State_Id`),
  CONSTRAINT `FK1_tmDistrict_tmUser` FOREIGN KEY (`Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK2_tmDistrict_tmUser` FOREIGN KEY (`Modified_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmDistrict_tmCountry` FOREIGN KEY (`Country_Id`) REFERENCES `tm_country` (`Country_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmDistrict_tmState` FOREIGN KEY (`State_Id`) REFERENCES `tm_state` (`State_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_guest_user`
--

DROP TABLE IF EXISTS `tm_guest_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_guest_user` (
  `GU_Id` char(36) NOT NULL,
  `GU_Name` varchar(50) NOT NULL,
  `GU_Address1` varchar(50) NOT NULL,
  `GU_Address2` varchar(50) NOT NULL,
  `GU_Address3` varchar(50) DEFAULT NULL,
  `GU_State` varchar(50) NOT NULL,
  `GU_Country` varchar(50) NOT NULL,
  `GU_DOB` date NOT NULL,
  `GU_Sex` char(1) NOT NULL,
  `GU_Password` varchar(50) NOT NULL,
  `GU_Mobile_No` int(10) NOT NULL,
  `GU_Email_Id` varchar(40) NOT NULL,
  `GU_Created_On` datetime NOT NULL,
  `GU_Last_Activity` datetime NOT NULL,
  `GU_IsActive` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`GU_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_homestay`
--

DROP TABLE IF EXISTS `tm_homestay`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_homestay` (
  `HS_Id` char(36) NOT NULL,
  `HS_Name` varchar(50) NOT NULL,
  `HS_Address1` varchar(60) NOT NULL,
  `HS_Address2` varchar(60) NOT NULL,
  `HS_Address3` varchar(60) DEFAULT NULL,
  `HS_Vill_Id` char(36) NOT NULL,
  `HS_Block_Id` char(36) NOT NULL,
  `HS_District_Id` char(36) NOT NULL,
  `HS_State_Id` char(36) NOT NULL,
  `HS_Country_Id` char(36) NOT NULL,
  `HS_Contact_Name` varchar(50) NOT NULL,
  `HS_Contact_Mob1` int(10) NOT NULL,
  `HS_Contact_Mob2` int(10) DEFAULT NULL,
  `HS_Contact_Email` varchar(50) NOT NULL,
  `HS_No_of_Rooms` int(3) NOT NULL,
  `HS_Bank_Name` varchar(50) NOT NULL,
  `HS_Bank_Branch` varchar(50) NOT NULL,
  `HS_Account_No` int(11) NOT NULL,
  `HS_Account_Type` char(10) NOT NULL,
  `HS_IFSC` char(20) NOT NULL,
  `HS_MICR` char(10) NOT NULL,
  `Created_By` char(36) NOT NULL,
  `Created_On` datetime NOT NULL,
  `Approved_By` char(36) DEFAULT NULL,
  `Approved_On` datetime DEFAULT NULL,
  `Is_Active` bit(1) NOT NULL DEFAULT b'1',
  `Active_Since` date NOT NULL,
  `Deactivated_By` char(36) DEFAULT NULL,
  `Deactivated_On` date DEFAULT NULL,
  `Modified_By` char(36) DEFAULT NULL,
  `Modified_On` datetime DEFAULT NULL,
  PRIMARY KEY (`HS_Id`),
  UNIQUE KEY `HSAccountNo_UNIQUE` (`HS_Account_No`),
  KEY `FK_tmHomestay_Created_By_Idx` (`Created_By`),
  KEY `FK_tmHomestay_Modified_By_Idx` (`Modified_By`),
  KEY `FK_tmHomestay_HS_Country_Id_Idx` (`HS_Country_Id`),
  KEY `FK_tmHomestay_HS_State_Id_Idx` (`HS_State_Id`),
  KEY `FK_tmHomestay_HS_District_Id_Idx` (`HS_District_Id`),
  KEY `FK_tmHomestay_HS_Block_Id_Idx` (`HS_Block_Id`),
  KEY `FK_tmHomestay_Approved_By_Idx` (`Approved_By`),
  KEY `FK_tmHomestay_Deactivated_By_Idx` (`Deactivated_By`),
  KEY `FK_tmHomestay_tmmBlockVilage_idx` (`HS_Vill_Id`),
  CONSTRAINT `FK1_tmHomestay_tmUser` FOREIGN KEY (`Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK2_tmHomestay_tmUser` FOREIGN KEY (`Modified_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK3_tmHomestay_tmUser` FOREIGN KEY (`Approved_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK4_tmHomestay_tmUser` FOREIGN KEY (`Deactivated_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmHomestay_tmBlock` FOREIGN KEY (`HS_Block_Id`) REFERENCES `tm_block` (`Block_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmHomestay_tmCountry` FOREIGN KEY (`HS_Country_Id`) REFERENCES `tm_country` (`Country_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmHomestay_tmDistrict` FOREIGN KEY (`HS_District_Id`) REFERENCES `tm_district` (`District_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmHomestay_tmState` FOREIGN KEY (`HS_State_Id`) REFERENCES `tm_state` (`State_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmHomestay_tmmBlockVilage` FOREIGN KEY (`HS_Vill_Id`) REFERENCES `tm_block_village` (`Vill_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_hs_facilities`
--

DROP TABLE IF EXISTS `tm_hs_facilities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_hs_facilities` (
  `HS_Facility_Id` char(36) NOT NULL,
  `HS_facility_Name` char(30) NOT NULL,
  PRIMARY KEY (`HS_Facility_Id`),
  UNIQUE KEY `HSFacilityName_UNIQUE` (`HS_facility_Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_hs_gallery`
--

DROP TABLE IF EXISTS `tm_hs_gallery`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_hs_gallery` (
  `HS_Id` char(36) NOT NULL,
  `HS_LI_1` text NOT NULL,
  `HS_LI_2` text,
  `HS_LI_3` text,
  `HS_LI_4` text,
  `HS_LI_5` text,
  `HS_LI_6` text,
  `HS_LI_7` text,
  `HS_LI_8` text,
  `HS_LI_9` text,
  `HS_LI_10` text,
  `HS_RI_1` text NOT NULL,
  `HS_RI_2` text,
  `HS_RI_3` text,
  `HS_RI_4` text,
  `HS_RI_5` text,
  `HS_RI_6` text,
  `HS_RI_7` text,
  `HS_RI_8` text,
  `HS_RI_9` text,
  `HS_RI_10` text,
  `HS_Map_lat` decimal(8,6) DEFAULT NULL,
  `HS_Map_long` decimal(9,6) DEFAULT NULL,
  PRIMARY KEY (`HS_Id`),
  KEY `FK_tmHsGallery_HS_Id_Idx` (`HS_Id`),
  CONSTRAINT `FK_tmHsGallery_tmHomestay` FOREIGN KEY (`HS_Id`) REFERENCES `tm_homestay` (`HS_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_hs_room_category`
--

DROP TABLE IF EXISTS `tm_hs_room_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_hs_room_category` (
  `HS_Category_Id` char(36) NOT NULL,
  `HS_Category_Name` char(30) NOT NULL,
  PRIMARY KEY (`HS_Category_Id`),
  UNIQUE KEY `HSCategoryName_UNIQUE` (`HS_Category_Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_hs_rooms`
--

DROP TABLE IF EXISTS `tm_hs_rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_hs_rooms` (
  `HS_Id` char(36) NOT NULL,
  `HS_Room_No` tinyint(4) NOT NULL,
  `HS_Room_Category_Id` char(36) NOT NULL,
  `HS_Room_Rate` int(11) NOT NULL,
  `HS_Room_Floor` char(15) DEFAULT NULL,
  `HS_Room_No_Beds` tinyint(4) NOT NULL,
  `HS_Room_Size` char(10) DEFAULT NULL,
  `HS_Room_Image` text,
  `HS_Room_Facility1` int(11) NOT NULL,
  `HS_Room_Facility2` int(11) DEFAULT NULL,
  `HS_Room_Facility3` int(11) DEFAULT NULL,
  `HS_Room_Facility4` int(11) DEFAULT NULL,
  `HS_Room_Facility5` int(11) DEFAULT NULL,
  `HS_Room_Facility6` int(11) DEFAULT NULL,
  `HS_Room_Facility7` int(11) DEFAULT NULL,
  `HS_Room_Facility8` int(11) DEFAULT NULL,
  `HS_Room_Facility9` int(11) DEFAULT NULL,
  `HS_Room_Facility10` int(11) DEFAULT NULL,
  `HS_Room_Facility11` int(11) DEFAULT NULL,
  `HS_Room_Facility12` int(11) DEFAULT NULL,
  `HS_Room_Facility13` int(11) DEFAULT NULL,
  `HS_Room_Facility14` int(11) DEFAULT NULL,
  `HS_Room_Facility15` int(11) DEFAULT NULL,
  `HS_Room_Available` bit(1) NOT NULL DEFAULT b'0',
  `Created_By` char(36) NOT NULL,
  `Created_On` datetime NOT NULL,
  `IsAvailable` bit(1) NOT NULL DEFAULT b'0',
  `Modified_By` char(36) DEFAULT NULL,
  `Modified_On` datetime DEFAULT NULL,
  PRIMARY KEY (`HS_Id`,`HS_Room_No`),
  KEY `FK_tmHsRooms_Created_By_Idx` (`Created_By`),
  KEY `FK_tmHsRooms_Modified_By_Idx` (`Modified_By`),
  KEY `FK_tmHsRooms_HS_Room_Category_Id_Idx` (`HS_Room_Category_Id`),
  CONSTRAINT `FK1_tmHsRooms_tmUser` FOREIGN KEY (`Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK2_tmHsRooms_tmUser` FOREIGN KEY (`Modified_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmHsRooms_tmHomestay` FOREIGN KEY (`HS_Id`) REFERENCES `tm_homestay` (`HS_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmHsRooms_tmHsRoomCategory` FOREIGN KEY (`HS_Room_Category_Id`) REFERENCES `tm_hs_room_category` (`HS_Category_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_state`
--

DROP TABLE IF EXISTS `tm_state`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_state` (
  `State_Id` char(36) NOT NULL,
  `State_Code` int(10) NOT NULL,
  `State_Name` varchar(50) NOT NULL,
  `Country_Id` char(36) NOT NULL,
  `Is_Active` bit(1) NOT NULL DEFAULT b'1',
  `Created_By` char(36) NOT NULL,
  `Created_On` datetime NOT NULL,
  `Modified_By` char(36) DEFAULT NULL,
  `Modified_On` datetime DEFAULT NULL,
  PRIMARY KEY (`State_Id`),
  UNIQUE KEY `StateCode_UNIQUE` (`State_Code`),
  UNIQUE KEY `StateName_UNIQUE` (`State_Name`),
  KEY `FK_tmState_tmCountry_idx` (`Country_Id`),
  KEY `FK1_tmState_tmUser_idx` (`Created_By`),
  KEY `FK2_tmState_tmUser_idx` (`Modified_By`),
  CONSTRAINT `FK1_tmState_tmUser` FOREIGN KEY (`Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK2_tmState_tmUser` FOREIGN KEY (`Modified_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmState_tmCountry` FOREIGN KEY (`Country_Id`) REFERENCES `tm_country` (`Country_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_user`
--

DROP TABLE IF EXISTS `tm_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_user` (
  `User_Id` char(36) NOT NULL COMMENT '36 character GUID/UUID',
  `User_Name` varchar(50) NOT NULL,
  `User_Role_Id` char(36) NOT NULL,
  `User_DOB` date NOT NULL,
  `User_Sex` char(1) NOT NULL,
  `User_Password` varchar(2000) NOT NULL,
  `User_Mobile_No` int(10) NOT NULL,
  `User_Email_Id` varchar(40) NOT NULL,
  `User_Last_Activity` datetime NOT NULL,
  `User_IsActive` bit(1) NOT NULL DEFAULT b'1',
  `Is_System_Defined` bit(1) NOT NULL DEFAULT b'0',
  `Password_Last_Changed` date DEFAULT NULL,
  `Previous_Passwords` varchar(2000) DEFAULT NULL,
  `Invalid_Login_Attempts` int(7) DEFAULT NULL,
  `Lockout_Enabled` tinyint(1) NOT NULL,
  `Locked_Out_Until` date DEFAULT NULL,
  `Last_Login` date DEFAULT NULL,
  `Reset_Token` varchar(100) DEFAULT NULL,
  `Reset_Token_Expires` datetime DEFAULT NULL,
  `User_Created_On` datetime NOT NULL,
  `User_Created_By` char(36) NOT NULL,
  PRIMARY KEY (`User_Id`),
  UNIQUE KEY `UserName_UNIQUE` (`User_Name`),
  KEY `FK_tmUser_User_Id` (`User_Created_By`),
  KEY `FK_tmUser_tmUserRole` (`User_Role_Id`),
  CONSTRAINT `FK_tmUser_User_Created_By` FOREIGN KEY (`User_Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmUser_tmUserRole` FOREIGN KEY (`User_Role_Id`) REFERENCES `tm_user_role` (`Role_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tm_user_role`
--

DROP TABLE IF EXISTS `tm_user_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tm_user_role` (
  `Role_Id` char(36) NOT NULL,
  `Role_Name` varchar(15) NOT NULL,
  `Role_IsActive` bit(1) NOT NULL DEFAULT b'1',
  `Is_System_Defined` bit(1) NOT NULL DEFAULT b'1',
  `Is_Deleted` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Role_Id`),
  UNIQUE KEY `Role_Name` (`Role_Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tt_bank_transaction`
--

DROP TABLE IF EXISTS `tt_bank_transaction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tt_bank_transaction` (
  `Booking_Id` char(36) NOT NULL,
  `User_Id` char(36) NOT NULL,
  `HS_Id` char(36) NOT NULL,
  `Transaction_Date` datetime NOT NULL,
  `Transaction_Type` char(1) NOT NULL DEFAULT '',
  `Trans_Amount` int(11) DEFAULT NULL,
  `Trans_Mode` varchar(10) NOT NULL,
  `Trans_Status` char(10) NOT NULL,
  `IsMailed` tinyint(1) NOT NULL,
  PRIMARY KEY (`Booking_Id`,`Transaction_Date`,`Transaction_Type`),
  KEY `FK_ttBankTransaction_User_Id_Idx` (`User_Id`),
  KEY `FK_ttBankTransaction_HS_Id_Idx` (`HS_Id`),
  CONSTRAINT `FK_ttBankTransaction_tmHomestay` FOREIGN KEY (`HS_Id`) REFERENCES `tm_homestay` (`HS_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttBankTransaction_tmUser` FOREIGN KEY (`User_Id`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tt_booking`
--

DROP TABLE IF EXISTS `tt_booking`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tt_booking` (
  `GU_Id` char(36) NOT NULL,
  `HS_Id` char(36) NOT NULL,
  `HS_Booking_Id` char(36) NOT NULL,
  `BK_Booking_Date` datetime NOT NULL,
  `BK_Date_from` datetime NOT NULL,
  `BK_Date_To` datetime NOT NULL,
  `BK_No_Pers` tinyint(4) NOT NULL,
  `BK_Room_Number` varchar(50) NOT NULL,
  `BK_Is_Availed` tinyint(1) NOT NULL,
  `BK_Payment_Mode` varchar(10) NOT NULL,
  `BK_Payment_Status` char(10) NOT NULL,
  `BK_Payment_Amount` int(11) NOT NULL,
  `BK_Pmt_Vouchar_No` char(15) NOT NULL,
  `BK_Pmt_Vouchar_Date` date NOT NULL,
  `BK_Is_Cancelled` bit(1) NOT NULL DEFAULT b'0',
  `BK_Cancelled_Date` date NOT NULL,
  `BK_Refund_Mode` varchar(10) DEFAULT NULL,
  `BK_Refund_Status` char(10) DEFAULT NULL,
  `BK_Refund_Amount` int(11) DEFAULT NULL,
  `BK_Rfd_Vouchar_No` char(15) DEFAULT NULL,
  `BK_Rfd_Vouchar_Date` date DEFAULT NULL,
  PRIMARY KEY (`GU_Id`,`HS_Booking_Id`),
  KEY `FK_ttBooking_GU_Id_Idx` (`GU_Id`),
  KEY `FK_ttBooking_HS_Id_Idx` (`HS_Id`),
  CONSTRAINT `FK_ttBooking_tmGuestUser` FOREIGN KEY (`GU_Id`) REFERENCES `tm_guest_user` (`GU_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttBooking_tmHomestay` FOREIGN KEY (`HS_Id`) REFERENCES `tm_homestay` (`HS_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tt_hs_feedback`
--

DROP TABLE IF EXISTS `tt_hs_feedback`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tt_hs_feedback` (
  `Feedback_Id` char(36) NOT NULL,
  `GU_Id` char(36) NOT NULL,
  `HS_Ratings` int(11) DEFAULT NULL,
  `HS_Feedback` text,
  `Is_Viewed` bit(1) NOT NULL DEFAULT b'1',
  `Is_Action_Taken` bit(1) NOT NULL DEFAULT b'1',
  `Action_Description` text,
  `Action_Taken_by` char(36) NOT NULL,
  `Action_Date` datetime NOT NULL,
  PRIMARY KEY (`Feedback_Id`),
  KEY `FK_ttHsFeedback_tmUser` (`Action_Taken_by`),
  KEY `FK_ttHsFeedback_tmGuestUser_idx` (`GU_Id`),
  CONSTRAINT `FK_ttHsFeedback_tmGuestUser` FOREIGN KEY (`GU_Id`) REFERENCES `tm_guest_user` (`GU_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttHsFeedback_tmUser` FOREIGN KEY (`Action_Taken_by`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tt_hs_popularity`
--

DROP TABLE IF EXISTS `tt_hs_popularity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tt_hs_popularity` (
  `HS_Id` char(36) NOT NULL,
  `HS_Search_count` int(11) NOT NULL,
  PRIMARY KEY (`HS_Id`),
  CONSTRAINT `FK_ttHsPopularity_tmHomestay` FOREIGN KEY (`HS_Id`) REFERENCES `tm_homestay` (`HS_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-08-24 13:00:14
