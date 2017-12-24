CREATE TABLE `transactions_btc` (
	`sn` INT(11) NOT NULL AUTO_INCREMENT,
	`time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	`price` INT(11) NOT NULL,
	`units` FLOAT NOT NULL,
	`type` VARCHAR(3) NOT NULL,
	PRIMARY KEY (`sn`),
	UNIQUE INDEX `time_price_units_type` (`time`, `price`, `units`, `type`)
)
COMMENT='빗썸 비트코인 거래내역'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
AUTO_INCREMENT=3921
;
