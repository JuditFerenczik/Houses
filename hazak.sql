-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2021. Dec 01. 00:08
-- Kiszolgáló verziója: 10.4.19-MariaDB
-- PHP verzió: 8.0.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `hazak`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `csaladi`
--

CREATE TABLE `csaladi` (
  `csid` int(11) NOT NULL,
  `ottelok` int(11) NOT NULL,
  `garazs` tinyint(1) NOT NULL,
  `teto` varchar(20) NOT NULL,
  `hid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `csaladi`
--

INSERT INTO `csaladi` (`csid`, `ottelok`, `garazs`, `teto`, `hid`) VALUES
(1, 11, 1, 'zsindely', 3),
(2, 12, 0, 'cserép', 4);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `haz`
--

CREATE TABLE `haz` (
  `id` int(11) NOT NULL,
  `cim` varchar(40) NOT NULL,
  `alapterulet` int(11) NOT NULL,
  `epitesianyag` varchar(20) NOT NULL,
  `mkezdete` date NOT NULL,
  `mvege` date NOT NULL,
  `csaladi` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `haz`
--

INSERT INTO `haz` (`id`, `cim`, `alapterulet`, `epitesianyag`, `mkezdete`, `mvege`, `csaladi`) VALUES
(1, 'Kerekerdő 25', 67, 'tégla', '2021-12-08', '2022-01-25', 0),
(2, 'Kishatár 4', 42, 'pala', '2021-12-16', '2022-01-20', 0),
(3, 'Csapó 32', 51, 'fa', '2021-12-24', '2022-02-05', 1),
(4, 'Sosehol 21', 42, 'pala', '2021-12-23', '2022-01-20', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `tomb`
--

CREATE TABLE `tomb` (
  `tid` int(11) NOT NULL,
  `lakasok` int(11) NOT NULL,
  `fenntartas` varchar(20) NOT NULL,
  `lift` tinyint(1) NOT NULL,
  `hid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- A tábla adatainak kiíratása `tomb`
--

INSERT INTO `tomb` (`tid`, `lakasok`, `fenntartas`, `lift`, `hid`) VALUES
(1, 4, 'egyéni', 0, 1),
(2, 7, 'szövetkezet', 1, 2);

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `csaladi`
--
ALTER TABLE `csaladi`
  ADD PRIMARY KEY (`csid`),
  ADD KEY `hid` (`hid`);

--
-- A tábla indexei `haz`
--
ALTER TABLE `haz`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `tomb`
--
ALTER TABLE `tomb`
  ADD PRIMARY KEY (`tid`),
  ADD KEY `hid` (`hid`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `csaladi`
--
ALTER TABLE `csaladi`
  MODIFY `csid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `haz`
--
ALTER TABLE `haz`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT a táblához `tomb`
--
ALTER TABLE `tomb`
  MODIFY `tid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
