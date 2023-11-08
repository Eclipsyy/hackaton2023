mergeInto(LibraryManager.library, {
	
	SaveExtern: function(date) {
		var dateString = UTF8ToString(date);
		var myobj = JSON.parse(dateString);
		player.setData(myobj);
	},

	LoadExtern: function(){
		player.getData().then(_date => {
			const myJSON = JSON.stringify(_date);
			myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
		});
	},

	SetToLeaderboard: function(n, value){
		var name = UTF8ToString(n);
		ysdk.getLeaderboards()
		.then(lb => {
			lb.setLeaderboardScore(name, value);
		});
	},

	GetLang: function(){
		var lang = ysdk.environment.i18n.lang;
		var bufferSize = lengthBytesUTF8(lang) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(lang, buffer, bufferSize);
		return buffer;
	},

	FullAdvExtern: function() {
		ysdk.adv.showFullscreenAdv({
			callbacks: {
				onClose: function(wasShown) {
					myGameInstance.SendMessage("Progress", "ContinOnClosed");
				},
				onError: function(error) {
					myGameInstance.SendMessage("Progress", "ContinOnClosed");
				}
			}
		})
	},

	RewardedAdvExtern: function() {
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
				},
				onRewarded: () => {
					myGameInstance.SendMessage("Progress", "GetReward");
				},
				onClose: () => {
					myGameInstance.SendMessage("Progress", "ContinOnClosed");
				}, 
				onError: (e) => {
					console.log('Error while open video ad:', e);
				}
			}
		})

	},

});