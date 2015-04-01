-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Apr 02, 2015 at 01:21 AM
-- Server version: 5.6.17
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `esportcasino`
--

-- --------------------------------------------------------

--
-- Table structure for table `bet`
--

CREATE TABLE IF NOT EXISTS `bet` (
  `BetId` int(11) NOT NULL,
  `RoundId` int(11) NOT NULL,
  `SteamId` int(11) NOT NULL,
  `ItemsJson` text NOT NULL,
  `ItemsValue` int(11) NOT NULL,
  `TradeId` int(11) DEFAULT NULL,
  PRIMARY KEY (`BetId`),
  KEY `RoundId` (`RoundId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `bet`
--

INSERT INTO `bet` (`BetId`, `RoundId`, `SteamId`, `ItemsJson`, `ItemsValue`, `TradeId`) VALUES
(1, 1, 66167, '{ALL THE ITEMS}', 67, 1616);

-- --------------------------------------------------------

--
-- Table structure for table `round`
--

CREATE TABLE IF NOT EXISTS `round` (
  `RoundId` int(11) NOT NULL,
  `SkimItem` text,
  `StartTime` date NOT NULL,
  `EndTime` date DEFAULT NULL,
  `WinnerSteamId` int(11) DEFAULT NULL,
  `IsRoundRunning` tinyint(1) NOT NULL,
  PRIMARY KEY (`RoundId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `round`
--

INSERT INTO `round` (`RoundId`, `SkimItem`, `StartTime`, `EndTime`, `WinnerSteamId`, `IsRoundRunning`) VALUES
(1, '{nothing}', '2015-04-01', NULL, NULL, 1);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `bet`
--
ALTER TABLE `bet`
  ADD CONSTRAINT `bet_ibfk_1` FOREIGN KEY (`RoundId`) REFERENCES `round` (`RoundId`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
