-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Oct 25, 2017 at 03:43 PM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `todo`
--
CREATE DATABASE IF NOT EXISTS `todo` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `todo`;

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`id`, `name`) VALUES
(94, 'Asian');

-- --------------------------------------------------------

--
-- Table structure for table `tasks`
--

CREATE TABLE `tasks` (
  `id` int(11) NOT NULL,
  `description` varchar(255) NOT NULL,
  `category_id` int(11) DEFAULT NULL,
  `duedate` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tasks`
--

INSERT INTO `tasks` (`id`, `description`, `category_id`, `duedate`) VALUES
(32, 'Sleep', 43, '0000-00-00'),
(33, 'drink', 46, '0000-00-00'),
(34, 'Homework', 53, '0000-00-00'),
(35, 'Study', 53, '0000-00-00'),
(36, 'study', 54, '0000-00-00'),
(37, 'aaaa', 66, '0000-00-00'),
(38, 'ccccc', 66, '0000-00-00'),
(42, 'aifjoafj', 67, '0000-00-00'),
(43, 'Study', 86, '0000-00-00'),
(44, 'Chores', 86, '0000-00-00'),
(45, 'Doghouse', 86, '0000-00-00'),
(46, 'iewfjoewifj', 86, '0000-00-00'),
(48, 'Homework', 87, '2010-12-01'),
(49, 'Homework', 89, '2010-12-01'),
(50, 'Eat', 89, '0000-00-00'),
(51, 'hhhh', 90, '0000-00-00'),
(52, 'hhhh', 90, '2017-12-12'),
(53, 'homework', 91, '2000-01-02'),
(54, 'fff', 92, '2017-10-11'),
(55, 'aaa', 92, '2017-12-27'),
(56, 'iqejwii', 92, '2017-01-04'),
(57, 'klkl', 92, '2017-10-03');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `id` (`id`);

--
-- Indexes for table `tasks`
--
ALTER TABLE `tasks`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=95;
--
-- AUTO_INCREMENT for table `tasks`
--
ALTER TABLE `tasks`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
