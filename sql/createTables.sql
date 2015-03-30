-- tables
-- Table Round
CREATE TABLE Round (
    roundId int    NOT NULL ,
    potJson text    NOT NULL ,
    skimItemsJson text    NOT NULL ,
    timestarted timestamp    NOT NULL ,
    timefinished timestamp    NOT NULL ,
    winnerSteamId int    NOT NULL ,
    IsRoundRunning bool    NOT NULL ,
    CONSTRAINT Round_pk PRIMARY KEY (roundId)
);

-- Table bets
CREATE TABLE bets (
    betId int    NOT NULL ,
    itemsJson char(12)    NOT NULL ,
    SteamId int    NOT NULL ,
    itemsvalue numeric(15,2)    NOT NULL ,
    roundId int    NOT NULL ,
    CONSTRAINT bets_pk PRIMARY KEY (betId)
);





-- foreign keys
-- Reference:  RoundBets (table: bets)


ALTER TABLE bets ADD CONSTRAINT RoundBets FOREIGN KEY RoundBets (roundId)
    REFERENCES Round (roundId);
