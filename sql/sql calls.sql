select * from round;
#Start round
insert into round (timeStarted) values (Now());


#Get active round
select roundId from round where roundActive = 'true';
#Count active rounds should be <= 1
select count(roundId) from round where roundActive = 'true';

#Add Bets

#Double check only 1 active round
select count(roundId) from round where roundActive = 'true';

#Add Bets
insert into bets (roundId, steamId, steamTradeId, itemsJson, itemsValue) values ((select roundId from round where roundActive = 'true'), 00000123, 1230000, '{asdf}', 12.40); 

#Update round potJson and pot Value after every bet is entered
update round set potJson = 'CURRENT POT JSON + THIS BET JSON GONNNA HAVE TO WORK THAT OUT', potValue = 1234.45 where roundId = 1; #active round ; #'UPDATE POT VALUE GONNA HAVE TO WORK THIS OUT TOO'

#Round Ends
update round set skimItemsJson = '{skim}', winnerSteamId = 1002304, winnerSteamTradeId = 1111110, timeEnded = NOW();


select * from bets;
select * from round;


#truncate table round


#Misc
#Get active round
select roundId from round where roundActive = 'true';
#Count active rounds should be <= 1
select count(roundId) from round where roundActive = 'true';
#check if player has made bet in this round already
select steamId from bets where roundId = (select roundId from round where roundActive = 'true') and steamId = 0000;# 'playerSteamID';



#misc betting
insert into bets (roundId, steamId, steamTradeId, itemsJson, itemsValue) values ((select roundId from round where roundActive = 'true'), 00000124, 1230001, '{qwer}', 1.10); 
insert into bets (roundId, steamId, steamTradeId, itemsJson, itemsValue) values ((select roundId from round where roundActive = 'true'), 00000125, 1230002, '{bbbb}', 2.10); 
insert into bets (roundId, steamId, steamTradeId, itemsJson, itemsValue) values ((select roundId from round where roundActive = 'true'), 00000126, 1230003, '{poiu}', 6.10);
insert into bets (roundId, steamId, steamTradeId, itemsJson, itemsValue) values ((select roundId from round where roundActive = 'true'), 00000127, 1230004, '{poiu}', 6.10); 
