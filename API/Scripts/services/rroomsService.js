app.service('rroomsService', function($http){
	var url = '/api/rroom';

	this.add = function(rroom){
		return $http.post(url, rroom);
	}

	this.checkSeats = function(id, ETA, ETD) {
		var fakeETA = angular.copy(ETA);//.setHours(ETA.getHours() + 2);
		var fakeETD = angular.copy(ETD);//.setHours(ETD.getHours() + 2);
		fakeETA.setHours(ETA.getHours() - 2);
		fakeETD.setHours(ETD.getHours() - 2);
		return $http.get(url + "/" + id + "/schema?ETA=" + fakeETA.toUTCString() + "&ETD=" + fakeETD.toUTCString());
	}

	this.getAllSchemas = function(ETA, ETD) {
		var fakeETA = angular.copy(ETA);
		var fakeETD = angular.copy(ETD);
		fakeETA.setHours(ETA.getHours() - 2);
		fakeETD.setHours(ETD.getHours() - 2);
		return $http.get(url + "/schemas?ETA=" + fakeETA.toUTCString() + "&ETD=" + fakeETD.toUTCString());
	}
});
