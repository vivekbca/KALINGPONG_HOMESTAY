--Change DB Script for Home Stay Table for Contact Number On 24-08-2021--
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
CHANGE COLUMN `HS_Contact_Mob1` `HS_Contact_Mob1` CHAR(10) NOT NULL ,
CHANGE COLUMN `HS_Contact_Mob2` `HS_Contact_Mob2` CHAR(10) NULL DEFAULT NULL ;
--End--

--Change DB Script for tm_guest_user Table for GU_Mobile_No On 25-08-2021--
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ALTER TABLE `klmpnhomestay_db`.`tm_guest_user` 
CHANGE COLUMN `GU_Mobile_No` `GU_Mobile_No` CHAR(10) NOT NULL ;
--End--

--Change DB Script for tt_booking and tt_hs_feedback Table for HS_Booking_Id as a foreign of tt_hs_feedback On 26-08-2021--
ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
DROP PRIMARY KEY,
ADD PRIMARY KEY (`HS_Booking_Id`);
;

ALTER TABLE `klmpnhomestay_db`.`tt_hs_feedback` 
ADD COLUMN `HS_Booking_Id` CHAR(36) NOT NULL AFTER `Action_Date`;

ALTER TABLE `klmpnhomestay_db`.`tt_hs_feedback` 
DROP COLUMN `GU_Id`,
DROP INDEX `FK_ttHsFeedback_tmGuestUser_idx` ;
;

ALTER TABLE `klmpnhomestay_db`.`tt_hs_feedback` 
ADD CONSTRAINT `FK_ttHsFeedback_ttBooking`
  FOREIGN KEY (`HS_Booking_Id`)
  REFERENCES `klmpnhomestay_db`.`tt_booking` (`HS_Booking_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE `klmpnhomestay_db`.`tt_hs_feedback` 
CHANGE COLUMN `HS_Booking_Id` `HS_Booking_Id` CHAR(36) NOT NULL DEFAULT '' AFTER `Feedback_Id`;
--End--



--Change DB Script for tm_hs_rooms Table as a foreign of HS_Room_Facility On 26-08-2021--
ALTER TABLE `klmpnhomestay_db`.`tm_hs_rooms` 
CHANGE COLUMN `HS_Room_Facility1` `HS_Room_Facility1` CHAR(36) NOT NULL ,
CHANGE COLUMN `HS_Room_Facility2` `HS_Room_Facility2` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility3` `HS_Room_Facility3` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility4` `HS_Room_Facility4` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility5` `HS_Room_Facility5` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility6` `HS_Room_Facility6` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility7` `HS_Room_Facility7` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility8` `HS_Room_Facility8` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility9` `HS_Room_Facility9` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility10` `HS_Room_Facility10` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility11` `HS_Room_Facility11` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility12` `HS_Room_Facility12` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility13` `HS_Room_Facility13` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility14` `HS_Room_Facility14` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `HS_Room_Facility15` `HS_Room_Facility15` CHAR(36) NULL DEFAULT NULL ;

ALTER TABLE `klmpnhomestay_db`.`tm_hs_rooms` 
ADD INDEX `FK_tmHsRooms_tmHsFacilities_1_idx` (`HS_Room_Facility1` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_2_idx` (`HS_Room_Facility2` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_3_idx` (`HS_Room_Facility3` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_4_idx` (`HS_Room_Facility4` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_5_idx` (`HS_Room_Facility5` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_6_idx` (`HS_Room_Facility6` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_7_idx` (`HS_Room_Facility7` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_8_idx` (`HS_Room_Facility8` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_9_idx` (`HS_Room_Facility9` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_10_idx` (`HS_Room_Facility10` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_11_idx` (`HS_Room_Facility11` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_12_idx` (`HS_Room_Facility12` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_13_idx` (`HS_Room_Facility13` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_14_idx` (`HS_Room_Facility14` ASC),
ADD INDEX `FK_tmHsRooms_tmHsFacilities_15_idx` (`HS_Room_Facility15` ASC);
;
ALTER TABLE `klmpnhomestay_db`.`tm_hs_rooms` 
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_1`
  FOREIGN KEY (`HS_Room_Facility1`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_2`
  FOREIGN KEY (`HS_Room_Facility2`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_3`
  FOREIGN KEY (`HS_Room_Facility3`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_4`
  FOREIGN KEY (`HS_Room_Facility4`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_5`
  FOREIGN KEY (`HS_Room_Facility5`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_6`
  FOREIGN KEY (`HS_Room_Facility6`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_7`
  FOREIGN KEY (`HS_Room_Facility7`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_8`
  FOREIGN KEY (`HS_Room_Facility8`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_9`
  FOREIGN KEY (`HS_Room_Facility9`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_10`
  FOREIGN KEY (`HS_Room_Facility10`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_11`
  FOREIGN KEY (`HS_Room_Facility11`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_12`
  FOREIGN KEY (`HS_Room_Facility12`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_13`
  FOREIGN KEY (`HS_Room_Facility13`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_14`
  FOREIGN KEY (`HS_Room_Facility14`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION,
ADD CONSTRAINT `FK_tmHsRooms_tmHsFacilities_15`
  FOREIGN KEY (`HS_Room_Facility15`)
  REFERENCES `klmpnhomestay_db`.`tm_hs_facilities` (`HS_Facility_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  --End--

  --Change DB Script for tt_hs_feedback Table for nullable On 26-08-2021--
ALTER TABLE `klmpnhomestay_db`.`tt_hs_feedback` 
DROP FOREIGN KEY `FK_ttHsFeedback_tmUser`;
ALTER TABLE `klmpnhomestay_db`.`tt_hs_feedback` 
CHANGE COLUMN `Action_Taken_by` `Action_Taken_by` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `Action_Date` `Action_Date` DATETIME NULL DEFAULT NULL ;
ALTER TABLE `klmpnhomestay_db`.`tt_hs_feedback` 
ADD CONSTRAINT `FK_ttHsFeedback_tmUser`
  FOREIGN KEY (`Action_Taken_by`)
  REFERENCES `klmpnhomestay_db`.`tm_user` (`User_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `klmpnhomestay_db`.`tt_hs_feedback` 
CHANGE COLUMN `Is_Viewed` `Is_Viewed` BIT(1) NOT NULL DEFAULT 0 ,
CHANGE COLUMN `Is_Action_Taken` `Is_Action_Taken` BIT(1) NOT NULL DEFAULT 0 ;
--End--

--Change DB Script for newly added Tables On 04-09-2021--
CREATE TABLE `klmpnhomestay_db`.`tm_notice` (
  `Notice_Id` CHAR(36) NOT NULL,
  `Heading` VARCHAR(200) NOT NULL,
  `Subject` VARCHAR(500) NOT NULL,
  `Publishing_Date` DATE NOT NULL,
  `File_Name` VARCHAR(45) NULL,
  `Is_Deleted` BIT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Notice_Id`));
    
  CREATE TABLE `klmpnhomestay_db`.`tm_financial_year` (
  `Financial_Year_Id` CHAR(36) NOT NULL,
  `Financial_Year` CHAR(9) NOT NULL,
  PRIMARY KEY (`Financial_Year_Id`));
    
  CREATE TABLE `klmpnhomestay_db`.`tm_tender` (
  `Tender_Id` CHAR(36) NOT NULL,
  `Subject` VARCHAR(500) NOT NULL,
  `Financial_Year_Id` CHAR(36) NOT NULL,
  `Publishing_Date` DATE NOT NULL,
  `Memo_No` CHAR(30) NULL,
  `Is_Published` BIT(1) NOT NULL DEFAULT 1,
  `ClosingDate` DATE NOT NULL,
  `File_Name` VARCHAR(45) NULL,
  PRIMARY KEY (`Tender_Id`));
  
ALTER TABLE `klmpnhomestay_db`.`tm_tender` 
ADD CONSTRAINT `FK_tmTender_tmFinancialYear`
  FOREIGN KEY (`Financial_Year_Id`)
  REFERENCES `klmpnhomestay_db`.`tm_financial_year` (`Financial_Year_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION
--End--

 --Change DB Script for tm_permission,tt_role_permission_map,tm_village_category Table added and Village_Category_Id as a foreign key added in table tm_homestay  On 14-09-2021--
 CREATE TABLE `tm_permission` (
  `Permission_Id` char(36) NOT NULL,
  `Permission_Name` varchar(50) NOT NULL,
  PRIMARY KEY (`Permission_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tt_role_permission_map` (
  `Id` char(36) NOT NULL,
  `Role_Id` char(36)  DEFAULT NULL,
  `Permission_Id` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_ttRolePermissionMap_Role_Id_By_Idx` (`Role_Id`),
  KEY `FK_ttRolePermissionMap_Permission_Id_By_Idx` (`Permission_Id`),
  CONSTRAINT `FK_ttRolePermissionMap_RoleId` FOREIGN KEY (`Role_Id`) REFERENCES `tm_user_role` (`Role_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttRolePermissionMap_tmPermission` FOREIGN KEY (`Permission_Id`) REFERENCES `tm_permission` (`Permission_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `tm_village_category` (
  `Village_Category_Id` char(36) NOT NULL,
  `Village_Category_Name` varchar(50) NOT NULL,
  PRIMARY KEY (`Village_Category_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD COLUMN `Village_Category_Id` CHAR(36) NULL DEFAULT NULL AFTER `HS_Address3`;

ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD INDEX `FK_tmHomestay_tmVillageCategory_idx` (`Village_Category_Id` ASC);
;
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD CONSTRAINT `FK_tmHomestay_tmVillageCategory`
  FOREIGN KEY (`Village_Category_Id`)
  REFERENCES `klmpnhomestay_db`.`tm_village_category` (`Village_Category_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

  ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
DROP FOREIGN KEY `FK_tmHomestay_tmVillageCategory`;
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
CHANGE COLUMN `Village_Category_Id` `Village_Category_Id` CHAR(36) NOT NULL ;
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD CONSTRAINT `FK_tmHomestay_tmVillageCategory`
  FOREIGN KEY (`Village_Category_Id`)
  REFERENCES `klmpnhomestay_db`.`tm_village_category` (`Village_Category_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
 --End--
 --Change DB Script for newly property added into tm_hs_facilities On 16-09-2021--
 ALTER TABLE `klmpnhomestay_db`.`tm_hs_facilities` 
ADD COLUMN `File_Name` VARCHAR(45) NULL DEFAULT NULL AFTER `HS_facility_Name`;
--End--

--Change DB Script for new property added into tm_homestay On 17-09-2021--
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD COLUMN `Homestay_Description` VARCHAR(1000) NULL DEFAULT NULL AFTER `HS_Name`,
ADD COLUMN `Local_Attraction` VARCHAR(800) NULL DEFAULT NULL AFTER `Homestay_Description`,
ADD COLUMN `HWT_Reach` VARCHAR(500) NULL DEFAULT NULL AFTER `Local_Attraction`,
ADD COLUMN `Addon_Services` VARCHAR(500) NULL DEFAULT NULL AFTER `HS_Address3`;


ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
CHANGE COLUMN `Homestay_Description` `Homestay_Description` VARCHAR(1000) NOT NULL ,
CHANGE COLUMN `Local_Attraction` `Local_Attraction` VARCHAR(800) NOT NULL ,
CHANGE COLUMN `HWT_Reach` `HWT_Reach` VARCHAR(500) NOT NULL ;

--End--

--Change DB Script On 25-09-2021--
ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
CHANGE COLUMN `BK_Payment_Mode` `BK_Payment_Mode` VARCHAR(10) NULL DEFAULT NULL ,
CHANGE COLUMN `BK_Payment_Status` `BK_Payment_Status` CHAR(10) NULL DEFAULT NULL ,
CHANGE COLUMN `BK_Payment_Amount` `BK_Payment_Amount` INT(11) NULL ,
CHANGE COLUMN `BK_Pmt_Vouchar_No` `BK_Pmt_Vouchar_No` CHAR(15) NULL DEFAULT NULL ,
CHANGE COLUMN `BK_Pmt_Vouchar_Date` `BK_Pmt_Vouchar_Date` DATE NULL ,
CHANGE COLUMN `BK_Is_Cancelled` `BK_Is_Cancelled` BIT(1) NULL DEFAULT b'0' ,
CHANGE COLUMN `BK_Cancelled_Date` `BK_Cancelled_Date` DATE NULL DEFAULT NULL;

ALTER TABLE `klmpnhomestay_db`.`tm_guest_user` 
ADD COLUMN `GU_Pincode` VARCHAR(6) NOT NULL AFTER `GU_Address3`,
ADD COLUMN `GU_Identity_Proof` VARCHAR(50) NOT NULL AFTER `GU_Pincode`,
ADD COLUMN `GU_IdentityNo` VARCHAR(20) NOT NULL AFTER `GU_Identity_Proof`,
ADD COLUMN `GU_City` VARCHAR(45) NOT NULL AFTER `GU_IdentityNo`;

ALTER TABLE `klmpnhomestay_db`.`tm_guest_user` 
CHANGE COLUMN `GU_State` `GU_State_Id` CHAR(36) NOT NULL ,
CHANGE COLUMN `GU_Country` `GU_Country_Id` CHAR(36) NOT NULL ;

ALTER TABLE `klmpnhomestay_db`.`tm_guest_user` 
ADD CONSTRAINT `FK_tm_GuestUser_CountryId`
  FOREIGN KEY (`GU_Country_Id`)
  REFERENCES `klmpnhomestay_db`.`tm_country` (`Country_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


  ALTER TABLE `klmpnhomestay_db`.`tm_guest_user` 
ADD CONSTRAINT `FK_tm_GuestUser_StateId`
  FOREIGN KEY (`GU_State_Id`)
  REFERENCES `klmpnhomestay_db`.`tm_state` (`State_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

  ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD COLUMN `Pincode` VARCHAR(6) NOT NULL AFTER `HS_Address3`;

CREATE TABLE `tm_PopularDestination` (
  `Destination_Id` char(36) NOT NULL,
  `Destination_Name` varchar(50) NOT NULL,
  `Is_Active` bit(1) NOT NULL DEFAULT b'1',
  
  PRIMARY KEY (`Destination_Id`),
  UNIQUE KEY `BlockCode_UNIQUE` (`Destination_Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD COLUMN `Destination_Id` CHAR(36) NOT NULL AFTER `HS_Country_Id`;

ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD CONSTRAINT `FK_tmHomestay_tmPopularDestination`
  FOREIGN KEY (`Destination_Id`)
  REFERENCES `klmpnhomestay_db`.`tm_populardestination` (`Destination_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  --End--
  --Change DB Script On 01-10-2021--
  ALTER TABLE `klmpnhomestay_db`.`tm_block_village` 
ADD COLUMN `Vill_Image` VARCHAR(50) NULL DEFAULT NULL AFTER `Block_Id`;


ALTER TABLE `klmpnhomestay_db`.`tm_populardestination` 
ADD COLUMN `Populardestination_Image` VARCHAR(50) NULL DEFAULT NULL AFTER `Destination_Name`;
--End--
--Change DB Script On 01-10-2021--
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD COLUMN `User_Id` CHAR(36) NULL DEFAULT NULL AFTER `Destination_Id`;
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
CHANGE COLUMN `User_Id` `User_Id` CHAR(36) NOT NULL ;
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD INDEX `FK_tmHomestay_tmuser_idx` (`User_Id` ASC);
;
ALTER TABLE `klmpnhomestay_db`.`tm_homestay` 
ADD CONSTRAINT `FK_tmHomestay_tmuser`
  FOREIGN KEY (`User_Id`)
  REFERENCES `klmpnhomestay_db`.`tm_user` (`User_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  --End--

  --Change DB Script On 04-10-2021--
  CREATE TABLE `tm_marquee` (
  `Marquee_Id` char(36) COLLATE utf8_bin NOT NULL,
  `Heading` varchar(200) COLLATE utf8_bin NOT NULL,
  `Desc` varchar(500) COLLATE utf8_bin NOT NULL,
  `Is_Active` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`Marquee_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD COLUMN `Bank_Name` VARCHAR(25) NULL DEFAULT NULL AFTER `BK_Is_Availed`,
ADD COLUMN `Bank_Branch` VARCHAR(25) NULL DEFAULT NULL AFTER `Bank_Name`,
ADD COLUMN `Account_No` NVARCHAR(16) NULL DEFAULT NULL AFTER `Bank_Branch`,
ADD COLUMN `Account_Type` VARCHAR(10) NULL DEFAULT NULL AFTER `Account_No`,
ADD COLUMN `IFSC` VARCHAR(15) NULL DEFAULT NULL AFTER `Account_Type`;
   --End--

--Change DB Script On 06-10-2021--
   CREATE TABLE `tm_tour` (
`Id` char(36) NOT NULL,
`Name` varchar(50) NOT NULL,
`Destination` varchar(50) NOT NULL,
`Destination_Day` int NOT NULL,
`Destination_Night` int NOT NULL,
`Subject` varchar(200) NOT NULL,
`Description` varchar(500) NOT NULL,
`Cost` int(11)  NOT NULL,
`Contact_Person_Name` varchar(50) default NULL,
`Contact_Person_Name_Email` varchar(40) default NULL,
`Contact_Person_Mobile` int(10) default NULL,
`Facility_Id1` char(36) default NULL,
`Facility_Id2` char(36) default NULL,
`Facility_Id3` char(36) default NULL,
`Facility_Id4` char(36) default NULL,
`Facility_Id5` char(36) default NULL,
`Image1` varchar(50) default NULL,
`Image2` varchar(50) default NULL,
`Image3` varchar(50) default NULL,
`Image4` varchar(50) default NULL,
`Image5` varchar(50) default NULL,
`TourPDF_File` varchar(50) default NULL,
`Is_Active` bit(1) NOT NULL DEFAULT b'1',
`Created_By` char(36) NOT NULL,
`Created_On` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_tmTour_Facility_Id1_Idx` (`Facility_Id1`),
  KEY `FK_tmTour_Facility_Id2_Idx` (`Facility_Id2`),
  KEY `FK_tmTour_Facility_Id3_Idx` (`Facility_Id3`),
  KEY `FK_tmTour_Facility_Id4_Idx` (`Facility_Id4`),
  KEY `FK_tmTour_Facility_Id5_Idx` (`Facility_Id5`),
  CONSTRAINT `FK_tmTour_tmHsFacilities1` FOREIGN KEY (`Facility_Id1`) REFERENCES `tm_hs_facilities` (`HS_Facility_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmTour_tmHsFacilities2` FOREIGN KEY (`Facility_Id2`) REFERENCES `tm_hs_facilities` (`HS_Facility_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmTour_tmHsFacilities3` FOREIGN KEY (`Facility_Id3`) REFERENCES `tm_hs_facilities` (`HS_Facility_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmTour_tmHsFacilities4` FOREIGN KEY (`Facility_Id4`) REFERENCES `tm_hs_facilities` (`HS_Facility_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmTour_tmHsFacilities5` FOREIGN KEY (`Facility_Id5`) REFERENCES `tm_hs_facilities` (`HS_Facility_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_tmTour_tmUser` FOREIGN KEY (`Created_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `tt_tour_date` (
`Id` char(36) NOT NULL,
`Tour_Id` char(36) NOT NULL,
`From_Date` datetime NOT NULL,
`To_Date` datetime NOT NULL,
`Is_Active` bit(1) NOT NULL DEFAULT b'1',
	PRIMARY KEY (`Id`),
	KEY `FK_ttTourDate_Tour_Id_Idx` (`Tour_Id`),
	CONSTRAINT `FK_ttTourDate_tmTour` FOREIGN KEY (`Tour_Id`) REFERENCES `tm_tour` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `tt_tour_booking` (
`Id` char(36) NOT NULL,
`Tour_Id` char(36) NOT NULL,
`Tour_Date_Id` char(36) NOT NULL,
`GU_Id` char(36) NOT NULL,
`Booking_Date` datetime NOT NULL,
`Total_Rate` INT(11) NOT NULL,
`No_Of_Person` INT NOT NULL,
`Is_Completed`  bit(1) NOT NULL DEFAULT b'0',
`Bank_Name` varchar(25) default null,
`Bank_Branch` varchar(25) default null,
`Account_No` varchar(16) default null,
`Account_Type` varchar(10) default null,
`IFSC` varchar(15) default null,
`Payment_Mode` varchar(10) default null,
`Payment_Status` char(10) default null,
`Payment_Amount` int(11) default null,
`Payment_Voucher_No` char(15) default null,
`Payment_Voucher_Date` date default null,
`Is_Cancel` bit(1) NOT NULL DEFAULT b'0',
`Cancelled_By` char(36) NOT null,
`Cancelled_Date` date NOT null,
`Rfd_Voucher_Mode` varchar(10) DEFAULT null,
`Rfd_Voucher_Status` char(10) DEFAULT null,
`Rfd_Voucher_Amount` INT(11) DEFAULT null,
`Rfd_Voucher_No` CHAR(10) DEFAULT null,
`Rfd_Voucher_Date` DATE DEFAULT null,
 PRIMARY KEY (`Id`),
  KEY `FK_ttTourBooking_Tour_Id_Idx` (`Tour_Id`),
  KEY `FK_ttTourBooking_Tour_Date_Id_Idx` (`Tour_Date_Id`),
  KEY `FK_ttTourBooking_GU_Id_Idx` (`GU_Id`),
  CONSTRAINT `FK_ttTourBooking_tmTour` FOREIGN KEY (`Tour_Id`) REFERENCES `tm_tour` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttTourBooking_ttTourDate` FOREIGN KEY (`Tour_Date_Id`) REFERENCES `tt_tour_date` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttTourBooking_tmGuestUser` FOREIGN KEY (`GU_Id`) REFERENCES `tm_guest_user` (`GU_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttTourBooking_tmUser` FOREIGN KEY (`Cancelled_By`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `tt_tour_booking_detail` (
`Id` char(36) NOT NULL,
`Tour_Booking_Id` char(36) NOT NULL,
`First_Name` varchar(30) NOT NULL,
`Last_Name` varchar(20) NOT NULL null,
`DOB` DATE default null,
`Sex` CHAR(1) default null,
PRIMARY KEY (`Id`),
  KEY `FK_ttTourBookingDetail_Tour_Booking_Id_Idx` (`Tour_Booking_Id`),
  CONSTRAINT `FK_ttTourBookingDetail_tmTour` FOREIGN KEY (`Tour_Booking_Id`) REFERENCES `tt_tour_booking` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



ALTER TABLE `klmpnhomestay_db`.`tt_tour_date` 
DROP FOREIGN KEY `FK_ttTourDate_tmTour`;
ALTER TABLE `klmpnhomestay_db`.`tt_tour_date` 
ADD INDEX `FK_ttTourDate_tmTour_idx` (`Tour_Id` ASC),
DROP PRIMARY KEY;
;
ALTER TABLE `klmpnhomestay_db`.`tt_tour_date` 
ADD CONSTRAINT `FK_ttTourDate_tmTour`
  FOREIGN KEY (`Tour_Id`)
  REFERENCES `klmpnhomestay_db`.`tm_tour` (`Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
--END--


--18-10-2021--
ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
DROP FOREIGN KEY `FK_ttTourBooking_tmUser`;
ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
CHANGE COLUMN `Cancelled_By` `Cancelled_By` CHAR(36) NULL DEFAULT NULL ,
CHANGE COLUMN `Cancelled_Date` `Cancelled_Date` DATE NULL DEFAULT NULL ;
ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
ADD CONSTRAINT `FK_ttTourBooking_tmUser`
  FOREIGN KEY (`Cancelled_By`)
  REFERENCES `klmpnhomestay_db`.`tm_user` (`User_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  --END--



  --21-10-2021--
  CREATE TABLE `tt_package_feedback` (
  `Feedback_Id` char(36) NOT NULL,
  `Tour_Booking_Id` char(36) NOT NULL,
  `HS_Ratings` int(11) DEFAULT NULL,
  `HS_Feedback` text,
  `Is_Viewed` bit(1) NOT NULL DEFAULT b'0',
  `Is_Action_Taken` bit(1) NOT NULL DEFAULT b'0',
  `Action_Description` text,
  `Action_Taken_by` char(36) DEFAULT NULL,
  `Action_Date` datetime DEFAULT NULL,
  PRIMARY KEY (`Feedback_Id`),
  KEY `FK_ttPackageFeedback_Tour_Booking_Id_idx` (`Tour_Booking_Id`),
  KEY `FK_ttPackageFeedback_tmUser` (`Action_Taken_by`),
  CONSTRAINT `FK_ttPackageFeedback_tmUser` FOREIGN KEY (`Action_Taken_by`) REFERENCES `tm_user` (`User_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttPackageFeedback_ttTourBooking` FOREIGN KEY (`Tour_Booking_Id`) REFERENCES `tt_tour_booking` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD COLUMN `Cancellation_Reason` VARCHAR(200) NULL AFTER `BK_Is_Cancelled`,
ADD COLUMN `BK_Is_Refund_Status` BIT(1) NOT NULL DEFAULT b'0' AFTER `BK_Refund_Mode`,
ADD COLUMN `BK_Refund_By` CHAR(36) NULL DEFAULT NULL AFTER `BK_Rfd_Vouchar_Date`,
ADD COLUMN `BK_Refund_On` DATE NULL DEFAULT NULL AFTER `BK_Refund_By`,
ADD COLUMN `Is_CheckedBy_Admin` BIT(1) NOT NULL DEFAULT b'0' AFTER `BK_Refund_On`,
ADD COLUMN `Is_CheckedBy_BankUser` BIT(1) NOT NULL DEFAULT b'0' AFTER `Is_CheckedBy_Admin`,
ADD INDEX `FK_ttBooking_tmUser_idx` (`BK_Refund_By` ASC);
;
ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD CONSTRAINT `FK_ttBooking_tmUser`
  FOREIGN KEY (`BK_Refund_By`)
  REFERENCES `klmpnhomestay_db`.`tm_user` (`User_Name`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
ADD COLUMN `Cancellation_Reason` VARCHAR(200) NULL DEFAULT NULL AFTER `Is_Cancel`,
ADD COLUMN `Refund_Status` BIT(1) NOT NULL DEFAULT b'0' AFTER `Cancelled_Date`,
ADD COLUMN `Refund_On` DATE NULL DEFAULT NULL AFTER `Refund_Status`,
ADD COLUMN `Refund_By` CHAR(36) NULL DEFAULT NULL AFTER `Refund_On`,
ADD COLUMN `Is_CheckedBy_Admin` BIT(1) NOT NULL DEFAULT b'0' AFTER `Rfd_Voucher_Date`,
ADD COLUMN `Is_CheckedBy_BankUser` BIT(1) NOT NULL DEFAULT b'0' AFTER `Is_CheckedBy_Admin`,
ADD INDEX `FK_ttTourBooking_tmUser_idx` (`Refund_By` ASC);

ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
ADD CONSTRAINT `FK_ttTourBooking_refundBy`
  FOREIGN KEY (`Refund_By`)
  REFERENCES `klmpnhomestay_db`.`tm_user` (`User_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
CHANGE COLUMN `Cancellation_Reason` `BK_Cancellation_Reason` VARCHAR(200) NULL DEFAULT NULL ;


ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD COLUMN `Cancelled_By` CHAR(36) NULL DEFAULT NULL AFTER `BK_Cancellation_Reason`,
ADD INDEX `FK_ttBooking_cancelBy_idx` (`Cancelled_By` ASC);
;
ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD CONSTRAINT `FK_ttBooking_cancelBy`
  FOREIGN KEY (`Cancelled_By`)
  REFERENCES `klmpnhomestay_db`.`tm_user` (`User_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  --END--

  --25-10-2021--
  ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
DROP FOREIGN KEY `FK_ttTourBooking_tmUser`;
ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
DROP INDEX `FK_ttTourBooking_tmUser` ,
ADD INDEX `FK_ttTourBooking_cancelledBy_idx` (`Cancelled_By` ASC);
;
ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
ADD CONSTRAINT `FK_ttTourBooking_cancelledBy`
  FOREIGN KEY (`Cancelled_By`)
  REFERENCES `klmpnhomestay_db`.`tm_guest_user` (`GU_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  
  
  ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
DROP FOREIGN KEY `FK_ttBooking_cancelBy`;
ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD INDEX `FK_ttBooking_cancelBy_idx` (`Cancelled_By` ASC),
DROP INDEX `FK_ttBooking_cancelBy_idx` ;
;
ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD CONSTRAINT `FK_ttBooking_cancelBy`
  FOREIGN KEY (`Cancelled_By`)
  REFERENCES `klmpnhomestay_db`.`tm_guest_user` (`GU_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
--End--
 --28-10-2021--
CREATE TABLE `tt_booking_room_detail` (
  `Id` char(36) NOT NULL,
  `Booking_Id` char(36) NOT NULL,
  `HS_Id` char(36) NOT NULL,
  `From_Dt` DateTime NOT NULL,
  `To_Dt` DateTime DEFAULT NULL,
  `Room_No`  tinyint(4) NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_ttBookingRoomDetail_Booking_Id_Idx` (`Booking_Id`),
  KEY `FK_ttBookingRoomDetail_HS_Id_Idx` (`HS_Id`),
  CONSTRAINT `FK_ttBookingRoomDetail_HSBookingId` FOREIGN KEY (`Booking_Id`) REFERENCES `tt_booking` (`HS_Booking_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_ttBookingRoomDetail_HSId` FOREIGN KEY (`HS_Id`) REFERENCES `tm_homestay` (`HS_Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD COLUMN `Is_CancelChecked_ByAdmin` BIT(1) NOT NULL DEFAULT b'0' AFTER `Is_CheckedBy_BankUser`,
ADD COLUMN `Is_CancelChecked_ByBankUser` BIT(1) NOT NULL DEFAULT b'0' AFTER `Is_CancelChecked_ByAdmin`;

ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
ADD COLUMN `Is_CancelChecked_ByAdmin` BIT(1) NOT NULL DEFAULT b'0' AFTER `Is_CheckedBy_BankUser`,
ADD COLUMN `Is_CancelChecked_ByBankUser` BIT(1) NOT NULL DEFAULT b'0' AFTER `Is_CancelChecked_ByAdmin`;


ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD COLUMN `TotalCost` INT(11) NULL DEFAULT NULL AFTER `BK_Is_Availed`,
ADD COLUMN `IsReportChecked` BIT(1) NULL DEFAULT b'0' AFTER `Is_CancelChecked_ByBankUser`;


ALTER TABLE `klmpnhomestay_db`.`tt_tour_booking` 
ADD COLUMN `Is_ReportChecked` BIT(1) NULL DEFAULT b'0' AFTER `Is_CancelChecked_ByBankUser`;
--End--
--02-11-2021--
ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
DROP FOREIGN KEY `FK_ttBooking_tmUser`;
ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
DROP INDEX `FK_ttBooking_tmUser_idx` ;


ALTER TABLE `klmpnhomestay_db`.`tt_booking` 
ADD CONSTRAINT `FK_ttBooking_tmUser`
  FOREIGN KEY (`BK_Refund_By`)
  REFERENCES `klmpnhomestay_db`.`tm_user` (`User_Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;
  --End--

  --03-11-2021--
ALTER TABLE `klmpnhomestay_db`.`tm_guest_user` 
ADD COLUMN `Reset_Token` VARCHAR(100) NULL DEFAULT NULL AFTER `GU_Password`,
ADD COLUMN `Reset_Token_Expires` DATETIME NULL DEFAULT NULL AFTER `Reset_Token`;
  --End--

   --09-11-2021--
  ALTER TABLE `klmpnhomestay_db`.`tm_user` 
ADD UNIQUE INDEX `Reset_Token_UNIQUE` (`Reset_Token` ASC),
ADD UNIQUE INDEX `User_Email_Id_UNIQUE` (`User_Email_Id` ASC);
;

ALTER TABLE `klmpnhomestay_db`.`tm_guest_user` 
ADD UNIQUE INDEX `GU_Email_Id_UNIQUE` (`GU_Email_Id` ASC);
;
ALTER TABLE `klmpnhomestay_db`.`tm_guest_user` 
ADD UNIQUE INDEX `Reset_Token_UNIQUE` (`Reset_Token` ASC);
;

ALTER TABLE `klmpnhomestay_db`.`tt_tour_date` 
ADD PRIMARY KEY (`Id`);
;

  --End--